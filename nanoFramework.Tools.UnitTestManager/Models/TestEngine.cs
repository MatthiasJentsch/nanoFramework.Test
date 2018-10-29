using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using nanoFramework.Tools.UnitTester;
using Newtonsoft.Json;

namespace nanoFramework.Tools.UnitTestManager.Models
{
	public class AvailableTestDevice
	{
		public string DeviceType { get; set; }
		public bool IsReadyForNewTest { get; set; }
	}

	public class RunningTest
	{
		public string DeviceName { get; set; }
		public string DeviceType { get; set; }
		public Guid TestId { get; set; }
		public TimeSpan EstimatedRemainingTime { get; set; }
	}

	public class TestResult
	{
		public string DeviceName { get; set; }
		public string DeviceType { get; set; }
		public Guid TestId { get; set; }
		public string Result { get; set; }
		public string[] Details { get; set; }
	}

	public class ConnectedDevice
	{
		public string DeviceName { get; set; }
		public string DeviceType { get; set; }
		public string Port { get; set; }
		public string FirmwareFlasherDirectory { get; set; }
	}

	/// <summary>
	///     Class the represents a test job.
	/// </summary>
	public class TestJob
	{
		/// <summary>
		///     A unique test id
		/// </summary>
		public Guid TestId { get; set; }

		/// <summary>
		///     The device name on which the test should be executed
		/// </summary>
		public string DeviceName { get; set; }

		/// <summary>
		///     The device type on which the test should be executed
		/// </summary>
		public string DeviceType { get; set; }

		/// <summary>
		///     Firmware image version; only "latest" is currently supported
		/// </summary>
		public string FirmwareImage { get; set; }

		/// <summary>
		///     List of test library assemblies; each test assembly references the nanoFramework.Test.Engine and executes
		///     via TestManager.RunTests(Assembly.GetExecutingAssembly()) all tests from one assembly
		/// </summary>
		public string[] TestLibraries { get; set; }

		/// <summary>
		///     This executes all tests from all test assemblies
		/// </summary>
		/// <param name="connectedDevices">The list of connected devices that can be used for testing</param>
		/// <param name="testAssembliesRoot">The root directory where the test assemblies are stored</param>
		/// <returns>The list of test results. One entry for each TestLibraries entry</returns>
		internal IList<UnitTestAssemblyResult> Execute(IList<ConnectedDevice> connectedDevices, string testAssembliesRoot)
		{
			// Try to get the device by name and type
			var connectedDevice = connectedDevices.Where(device => device.DeviceName == DeviceName && device.DeviceType == DeviceType).SingleOrDefault();
			if (connectedDevice == null) throw new ArgumentException($"Device '{DeviceName}' of type '{DeviceType}' not found!");

			// create results list
			IList<UnitTestAssemblyResult> results = new List<UnitTestAssemblyResult>();

			// each test library must be executed standalone.
			foreach (var testLibrary in TestLibraries)
			{
				// the test library must exists as exe, pe and bin file!
				var testLibraryExeFile = new FileInfo(Path.Combine(testAssembliesRoot, testLibrary, string.Concat(testLibrary, ".exe")));
				if (!testLibraryExeFile.Exists) throw new FileNotFoundException(testLibraryExeFile.FullName);
				var testLibraryPeFile = new FileInfo(Path.Combine(testAssembliesRoot, testLibrary, string.Concat(testLibrary, ".pe")));
				if (!testLibraryPeFile.Exists) throw new FileNotFoundException(testLibraryPeFile.FullName);
				var testLibraryBinFile = new FileInfo(Path.Combine(testAssembliesRoot, testLibrary, string.Concat(testLibrary, ".bin")));
				if (!testLibraryPeFile.Exists) throw new FileNotFoundException(testLibraryBinFile.FullName);

				// Flash the firmware and test library
				if (!string.IsNullOrEmpty(FirmwareImage))
					switch (DeviceType)
					{
						case "ESP32_DEVKITC":
							if (FirmwareImage == "latest")
							{
								new Esp32FirmwareFlasher(connectedDevice.FirmwareFlasherDirectory).FlashLatestFirmware(connectedDevice.Port, testLibraryBinFile.FullName);
							}
							else
							{
								new Esp32FirmwareFlasher(connectedDevice.FirmwareFlasherDirectory).FlashFirmwareImage(connectedDevice.Port, FirmwareImage, testLibraryBinFile.FullName);
							}

							break;
						default:
							throw new NotSupportedException($"Flashing the nanoCLR into a {DeviceType} is not supported!");
					}

				// register the test observer
				var observer = new UartTestObserver(new List<string> {testLibraryExeFile.FullName});
				observer.ListenToUart(new SerialPort(connectedDevice.Port, 115200, Parity.None, 8, StopBits.One));
				//observer.ParseCapturedResult(@"D:\Downloads\Test-Results-4-ST_NUCLEO64_F401RE_NF.txt");

				// wait until the tests has finished and add the first result to the results list
				results.Add(observer.ObserveExecution().Result[0]);
			}

			return results;
		}
	}

	public interface ITestService
	{
		string[] ConnectedDeviceTypes { get; }
		bool IsDeviceTypeConnected(string deviceType);
		bool IsDeviceTypeAvailable(string deviceType);
		RunningTest StartNewTest(string deviceType, string artifactname, string artifactDownloadPath);
		TestResult GetTestResults(Guid testId);
	}

	public class TestService : ITestService
	{
		private readonly List<ConnectedDevice> _availableDevices;
		private readonly List<ConnectedDevice> _connectedDevices;
		private readonly DirectoryInfo _jobDirectory;
		private readonly DirectoryInfo _resultsDirectory;
		private readonly bool _runAtSameMachine;
		private readonly List<RunningTest> _runningTests = new List<RunningTest>();
		private readonly DirectoryInfo _testAssembliesRootDirectory;
		private readonly PhysicalAddress _wakeOnLanMacAddress;

		public TestService(List<ConnectedDevice> connectedDevices, string jobDirectory, string resultsDirectory, string testAssembliesRoot, string wakeOnLanMacAddress, bool runAtSameMachine)
		{
			_connectedDevices = connectedDevices;
			_jobDirectory = new DirectoryInfo(jobDirectory);
			_resultsDirectory = new DirectoryInfo(resultsDirectory);
			_testAssembliesRootDirectory = new DirectoryInfo(testAssembliesRoot);
			if (!string.IsNullOrEmpty(wakeOnLanMacAddress)) _wakeOnLanMacAddress = PhysicalAddress.Parse(wakeOnLanMacAddress);
			_availableDevices = new List<ConnectedDevice>(connectedDevices);
			_runAtSameMachine = runAtSameMachine;
			if (_runAtSameMachine) new Thread(TestExecuter).Start();
			new Thread(TestObserver).Start();
		}

		public string[] ConnectedDeviceTypes
		{
			get
			{
				if (_connectedDevices == null || _connectedDevices.Count == 0) return new string[] { };
				return _connectedDevices.Select(device => device.DeviceType).Distinct().ToArray();
			}
		}

		public bool IsDeviceTypeConnected(string deviceType)
		{
			return _connectedDevices != null && _connectedDevices.Find(device => device.DeviceType == deviceType) != null;
		}

		public bool IsDeviceTypeAvailable(string deviceType)
		{
			return _availableDevices != null && _availableDevices.Find(device => device.DeviceType == deviceType) != null;
		}

		public RunningTest StartNewTest(string deviceType, string artifactname, string artifactDownloadPath)
		{
			var testDevice = _availableDevices.Find(device => device.DeviceType == deviceType);
			if (testDevice != null)
			{
				var job = new TestJob
				{
					TestId = Guid.NewGuid(),
					DeviceType = deviceType,
					DeviceName = testDevice.DeviceName,
					FirmwareImage = "latest",
					TestLibraries = new[] {"nanoFramework.CoreLibrary.Tests"}
				};
				File.WriteAllText(Path.Combine(_jobDirectory.FullName, string.Concat(job.TestId, ".json~")), JsonConvert.SerializeObject(job, Formatting.Indented));
				File.Move(Path.Combine(_jobDirectory.FullName, string.Concat(job.TestId, ".json~")), Path.Combine(_jobDirectory.FullName, string.Concat(job.TestId, ".json")));
				if (!_runAtSameMachine && _wakeOnLanMacAddress != null) WakeOnLan();

				var test = new RunningTest
				{
					DeviceName = testDevice.DeviceName,
					DeviceType = deviceType,
					TestId = job.TestId,
					EstimatedRemainingTime = new TimeSpan(0, 1, 0)
				};
				_runningTests.Add(test);
				_availableDevices.Remove(_availableDevices.Find(device => device.DeviceName == testDevice.DeviceName));
				return test;
			}

			return null;
		}

		public TestResult GetTestResults(Guid testId)
		{
			var result = new TestResult
			{
				TestId = testId
			};
			var running = _runningTests.Find(test => test.TestId == testId);
			if (running != null)
			{
				result.DeviceName = running.DeviceName;
				result.DeviceType = running.DeviceType;
				result.Result = "Currently running. Not finished.";
				result.Details = new[] {"n.a."};
			}
			else
			{
				var files = _resultsDirectory.GetFiles($"*{testId}.json");
				if (files.Length > 0)
				{
					result.DeviceName = running.DeviceName;
					result.DeviceType = running.DeviceType;
					result.Result = "Finished!";
					result.Details = File.ReadAllLines(files[0].FullName);
				}
			}

			return result;
		}

		private void WakeOnLan()
		{
			// WOL packet is sent over UDP 255.255.255.0:40000.
			var client = new UdpClient();
			client.Connect(IPAddress.Broadcast, 40000);

			// WOL packet contains a 6-bytes trailer and 16 times a 6-bytes sequence containing the MAC address.
			var packet = new byte[17 * 6];

			// Trailer of 6 times 0xFF.
			for (var i = 0; i < 6; i++)
				packet[i] = 0xFF;

			// Body of magic packet contains 16 times the MAC address.
			var mac = _wakeOnLanMacAddress.GetAddressBytes();
			for (var i = 1; i <= 16; i++)
			for (var j = 0; j < 6; j++)
				packet[i * 6 + j] = mac[j];

			// Send WOL packet.
			client.Send(packet, packet.Length);
		}

		private void TestObserver()
		{
			while (true)
			{
				foreach (var runningTest in _runningTests.ToArray())
					if (_resultsDirectory.GetFiles($"*{runningTest.TestId}.json").Length > 0)
						_runningTests.Remove(runningTest);
				Thread.Sleep(10000);
			}
		}

		private void TestExecuter()
		{
			while (true)
			{
				// All .json files in the jobs directory are job's
				foreach (var jobFile in _jobDirectory.GetFiles("*.json"))
				{
					// read the job
					var job = JsonConvert.DeserializeObject<TestJob>(File.ReadAllText(jobFile.FullName));
					// execute the job
					IList<UnitTestAssemblyResult> results = job.Execute(_connectedDevices, _testAssembliesRootDirectory.FullName);
					// store the results
					File.WriteAllText(Path.Combine(_resultsDirectory.FullName, string.Concat(DateTime.Now.ToString("yyyyMMddHHmmss"), "#", job.TestId, ".json")), JsonConvert.SerializeObject(results, Formatting.Indented));
					// delete the job file
					jobFile.Delete();
				}

				Thread.Sleep(10000);
			}
		}
	}
}