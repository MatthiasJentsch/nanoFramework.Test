//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace nanoFramework.Tools.UnitTester
{
	/// <summary>
	/// Represents a configured and connected nanoFramework device
	/// </summary>
	public class ConnectedDevice
	{
		/// <summary>
		/// The unique device name
		/// </summary>
		public string DeviceName { get; set; }

		/// <summary>
		/// The device type; e.g. ESP32_DEVKITC
		/// </summary>
		public string DeviceType { get; set; }

		/// <summary>
		/// The COM-Port to which the device is connected to
		/// </summary>
		public string Port { get; set; }

		/// <summary>
		/// The directory where we can find the tool that flashes the nanoCLR into the device
		/// </summary>
		public string FirmwareFlasherDirectory { get; set; }
	}

	/// <summary>
	/// Class the represents a test job. The Execute method executes the test job on the device
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

		/// <summary>
		/// This executes all tests from all test assemblies
		/// </summary>
		/// <param name="connectedDevices">The list of connected devices that can be used for testing</param>
		/// <param name="testAssembliesRoot">The root directory where the test assemblies are stored</param>
		/// <returns>The list of test results. One entry for each TestLibraries entry</returns>
		internal IList<UnitTestAssemblyResult> Execute(IList<ConnectedDevice> connectedDevices, string testAssembliesRoot)
		{
			// Try to get the device by name and type
			ConnectedDevice connectedDevice = connectedDevices.Where(device => device.DeviceName == DeviceName && device.DeviceType == DeviceType).SingleOrDefault();
			if (connectedDevice == null)
			{
				// device not found!
				throw new ArgumentException($"Device '{DeviceName}' of type '{DeviceType}' not found!");
			}

			// Flash the firmware
			if (!string.IsNullOrEmpty(FirmwareImage))
			{
				switch (DeviceType)
				{
					case "ESP32_DEVKITC":
						if (FirmwareImage == "latest")
						{
							new Esp32FirmwareFlasher(connectedDevice.FirmwareFlasherDirectory).FlashLatestFirmware(connectedDevice.Port);
						}
						else
						{
							new Esp32FirmwareFlasher(connectedDevice.FirmwareFlasherDirectory).FlashFirmwareImage(connectedDevice.Port, FirmwareImage);
						}
						break;
					default:
						throw new NotSupportedException($"Flashing the nanoCLR into a {DeviceType} is not supported!");
				}
			}

			// Download the debugger assembly
			NuGetPackageDownloader packageDownloader = new NuGetPackageDownloader(@"https://www.myget.org/F/nanoframework-dev/api/v3/index.json", Path.Combine(Environment.CurrentDirectory, "packages"));
			packageDownloader.CleanDestinationDirectory();
			DirectoryInfo debuggerAssembly = packageDownloader.DownloadAndExtractPackage("nanoFramework.Tools.Debugger.Net");

			// Download the mscorlib, because all packages need this library
			DirectoryInfo coreLibrary = packageDownloader.DownloadAndExtractPackage("nanoFramework.CoreLibrary");
			// At least the mscorlib and the nfUnit assemblies must be deployed to the device
			IDictionary<string, DirectoryInfo> knownAssemblies = new Dictionary<string, DirectoryInfo>()
			{
				{ "mscorlib", coreLibrary },
				{ "nanoFramework.Test.Engine", new DirectoryInfo(Path.Combine(testAssembliesRoot, "nanoFramework.Test.Engine")) }
			};


			// create results list and the debugger instance
			IList<UnitTestAssemblyResult> results = new List<UnitTestAssemblyResult>();
			IDebugEngine debugger = DebugEngineFactory.Get(Path.Combine(debuggerAssembly.FullName, "nanoFramework.Tools.Debugger.dll"), DebugEngineKind.DirectSameAppDomain);
			try
			{
				// each test library must be executed standalone.
				foreach (string testLibrary in TestLibraries)
				{
					// the test library must exists as exe and pe file!
					FileInfo testLibraryExeFile = new FileInfo(Path.Combine(testAssembliesRoot, testLibrary, string.Concat(testLibrary, ".exe")));
					if (!testLibraryExeFile.Exists)
					{
						throw new FileNotFoundException(testLibraryExeFile.FullName);
					}
					FileInfo testLibraryPeFile = new FileInfo(Path.Combine(testAssembliesRoot, testLibrary, string.Concat(testLibrary, ".pe")));
					if (!testLibraryPeFile.Exists)
					{
						throw new FileNotFoundException(testLibraryPeFile.FullName);
					}

					// download the assemblies that are referenced by the test assembly
					IDictionary<string, DirectoryInfo> deploymentAssemblies = packageDownloader.DownloadReferencedAssemblyPackages(testLibraryExeFile, knownAssemblies);
					// the test assembly itself is also needed for deployment
					deploymentAssemblies.Add(testLibrary, new DirectoryInfo(Path.Combine(testAssembliesRoot, testLibrary)));

					// load all the needed binaries for deployment
					List<byte[]> binaries = new List<byte[]>();
					foreach (KeyValuePair<string, DirectoryInfo> assemblyInfo in deploymentAssemblies)
					{
						FileInfo[] peFiles = assemblyInfo.Value.GetFiles(assemblyInfo.Key + ".pe");
						if (peFiles != null && peFiles.Length == 1)
						{
							binaries.Add(File.ReadAllBytes(peFiles[0].FullName));
						}
					}

					// connect the nanoFramework debugger to the device
					if (!debugger.Connect(connectedDevice.Port))
					{
						throw new IOException($"Connecting the debugger to device on {connectedDevice.Port} not possible");
					}

					// register the test observer
					TestObserver observer = new TestObserver(new List<string>() { testLibraryExeFile.FullName }, knownAssemblies);
					observer.ListenToDebugEngine(debugger);

					// deploy all assemblies to the device
					if (!debugger.DeploymentExecute(binaries))
					{
						throw new IOException($"Deploying the assemblies to device on {connectedDevice.Port} not possible");
					}

					// disconnect and reconnect the nanoFramework debugger; that causes the nanoCLR to restart the CLR and executes the test assembly
					debugger.Disconnect();
					if (!debugger.Connect(connectedDevice.Port))
					{
						throw new IOException($"Reconnecting the debugger to device on {connectedDevice.Port} not possible");
					}

					// wait until the tests has finished and add the first result to the results list
					results.Add(observer.ObserveExecution().Result[0]);

					// disconnect the debugger
					debugger.Disconnect();
				}

				return results;
			}
			finally
			{
				DebugEngineFactory.UnloadAppDomain();
			}
		}
	}
}
