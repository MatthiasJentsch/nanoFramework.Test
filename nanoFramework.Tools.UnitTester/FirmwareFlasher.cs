//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Diagnostics;
using System.IO;

namespace nanoFramework.Tools.UnitTester
{
	/// <summary>
	/// The interface that all FirmwareFlasher instances should fulfill
	/// </summary>
	internal interface IFirmwareFlasher
	{
		/// <summary>
		/// Flashes the latest firmware into the device that is connected to the delivered COM port
		/// </summary>
		/// <param name="comPort">The COM port on which a device is connected</param>
		void FlashLatestFirmware(string comPort);

		/// <summary>
		/// Flashes the latest firmware into the device that is connected to the delivered COM port
		/// </summary>
		/// <param name="comPort">The COM port on which a device is connected</param>
		/// <param name="firmwareTag">The firmware tag which should be flahsed; for ESP32 e.g. 0.1.0-preview.738</param>
		void FlashFirmwareImage(string comPort, string firmwareTag);
	}

	/// <summary>
	/// The class that flashes a ESP32 device with the nanoCLR firmware
	/// </summary>
	internal class Esp32FirmwareFlasher : IFirmwareFlasher
	{
		#region fields
		/// <summary>
		/// full path to the EspFirmwareFlasher.exe
		/// </summary>
		private readonly string _espFirmwareFlasherPath = null;
		#endregion

		#region constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="espFirmwareFlasherDirectory">The directory where we can find the EspFirmwareFlasher.exe</param>
		internal Esp32FirmwareFlasher(string espFirmwareFlasherDirectory)
		{
			_espFirmwareFlasherPath = Path.Combine(espFirmwareFlasherDirectory, "EspFirmwareFlasher.exe");
			if (!File.Exists(_espFirmwareFlasherPath))
			{
				throw new FileNotFoundException(_espFirmwareFlasherPath);
			}
		}
		#endregion

		#region public methods
		/// <summary>
		/// Flashes the latest firmware into the ESP32 that is connected to the delivered COM port
		/// </summary>
		/// <param name="comPort">The COM port on which an ESP32 is connected</param>
		public void FlashLatestFirmware(string comPort)
		{
			// using the EspFirmwareFlasher tool
			Process firmwareFlasher = Process.Start(_espFirmwareFlasherPath, $"--port={comPort}");
			firmwareFlasher.WaitForExit();
			if (firmwareFlasher.ExitCode != 0)
			{
				throw new ApplicationException($"The Esp32FirmwareFlasher process has exited with exit code {firmwareFlasher.ExitCode}");
			}
		}

		/// <summary>
		/// Flashes the latest firmware into the device that is connected to the delivered COM port
		/// </summary>
		/// <param name="comPort">The COM port on which a device is connected</param>
		/// <param name="firmwareTag">The firmware tag which should be flahsed; for ESP32 e.g. 0.1.0-preview.738</param>
		public void FlashFirmwareImage(string comPort, string firmwareTag)
		{
			// using the EspFirmwareFlasher tool
			Process firmwareFlasher = Process.Start(_espFirmwareFlasherPath, $"--port={comPort} --firmware_tag={firmwareTag}");
			firmwareFlasher.WaitForExit();
			if (firmwareFlasher.ExitCode != 0)
			{
				throw new ApplicationException($"The Esp32FirmwareFlasher process has exited with exit code {firmwareFlasher.ExitCode}");
			}
		}
		#endregion
	}
}
