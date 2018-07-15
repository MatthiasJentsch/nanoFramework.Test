using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
	}

	/// <summary>
	/// Class the represents a test job.
	/// </summary>
	public class TestJob
	{
		/// <summary>
		/// A unique test id
		/// </summary>
		public Guid TestId { get; set; }

		/// <summary>
		/// The device name on which the test should be executed
		/// </summary>
		public string DeviceName { get; set; }

		/// <summary>
		/// The device type on which the test should be executed
		/// </summary>
		public string DeviceType { get; set; }

		/// <summary>
		/// Firmware image version; only "latest" is currently supported
		/// </summary>
		public string FirmwareImage { get; set; }

		/// <summary>
		/// List of test library assemblies; each test assembly references the nanoFramework.Test.Engine and executes
		/// via TestManager.RunTests(Assembly.GetExecutingAssembly()) all tests from one assembly
		/// </summary>
		public string[] TestLibraries { get; set; }
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
		private readonly List<ConnectedDevice> _connectedDevices;
		private readonly DirectoryInfo _jobDirectory;
		private readonly DirectoryInfo _resultsDirectory;
		private readonly List<ConnectedDevice> _availableDevices;
		private readonly List<RunningTest> _runningTests = new List<RunningTest>();
		private readonly PhysicalAddress _wakeOnLanMacAddress = null;
		public string[] ConnectedDeviceTypes
		{
			get
			{
				if (_connectedDevices == null || _connectedDevices.Count == 0)
				{
					return new string[] { };
				}
				return _connectedDevices.Select(device => device.DeviceType).Distinct().ToArray();
			}
		}

		public TestService(List<ConnectedDevice> connectedDevices, string jobDirectory, string resultsDirectory, string wakeOnLanMacAddress)
		{
			_connectedDevices = connectedDevices;
			_jobDirectory = new DirectoryInfo(jobDirectory);
			_resultsDirectory = new DirectoryInfo(resultsDirectory);
			if (!string.IsNullOrEmpty(wakeOnLanMacAddress))
			{
				_wakeOnLanMacAddress = PhysicalAddress.Parse(wakeOnLanMacAddress);
			}
			_availableDevices = new List<ConnectedDevice>(connectedDevices);
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
			ConnectedDevice testDevice = _availableDevices.Find(device => device.DeviceType == deviceType);
			if (testDevice !=  null)
			{
				TestJob job = new TestJob
				{
					TestId = Guid.NewGuid(),
					DeviceType = deviceType,
					DeviceName = testDevice.DeviceName,
					FirmwareImage = "latest",
					TestLibraries = new string[] { "nanoFramework.CoreLibrary.Tests" }
				};
				File.WriteAllText(Path.Combine(_jobDirectory.FullName, string.Concat(job.TestId, ".json~")), JsonConvert.SerializeObject(job, Formatting.Indented));
				File.Move(Path.Combine(_jobDirectory.FullName, string.Concat(job.TestId, ".json~")), Path.Combine(_jobDirectory.FullName, string.Concat(job.TestId, ".json")));
				if (_wakeOnLanMacAddress != null)
				{
					WakeOnLan();
				}

				RunningTest test = new RunningTest
				{
					DeviceName = testDevice.DeviceName,
					DeviceType = deviceType,
					TestId = job.TestId,
					EstimatedRemainingTime = new TimeSpan(0, 1, 0)
				};
				_runningTests.Add(test);
				return test;
			}
			return null;
		}

		public TestResult GetTestResults(Guid testId)
		{
			TestResult result = new TestResult
			{
				TestId = testId
			};
			RunningTest running = _runningTests.Find(test => test.TestId == testId);
			if (running != null)
			{
				result.DeviceName = running.DeviceName;
				result.DeviceType = running.DeviceType;
				result.Result = "Currently running. Not finished.";
				result.Details = new string[] { "n.a." };
			}
			else
			{
				// TODO: Find test results
			}
			return result;
		}

		private void WakeOnLan()
		{
			// WOL packet is sent over UDP 255.255.255.0:40000.
			UdpClient client = new UdpClient();
			client.Connect(IPAddress.Broadcast, 40000);

			// WOL packet contains a 6-bytes trailer and 16 times a 6-bytes sequence containing the MAC address.
			byte[] packet = new byte[17 * 6];

			// Trailer of 6 times 0xFF.
			for (int i = 0; i < 6; i++)
				packet[i] = 0xFF;

			// Body of magic packet contains 16 times the MAC address.
			byte[] mac = _wakeOnLanMacAddress.GetAddressBytes();
			for (int i = 1; i <= 16; i++)
				for (int j = 0; j < 6; j++)
					packet[i * 6 + j] = mac[j];

			// Send WOL packet.
			client.Send(packet, packet.Length);
		}
	}
}
