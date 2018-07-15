//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Tools.Debugger;
using nanoFramework.Tools.Debugger.WireProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace nanoFramework.Tools.UnitTester
{
	/// <summary>
	/// The wrapper class that communicates with the nanoFramework debugger via early bindings
	/// </summary>
	public class DebugEngineDirect : MarshalByRefObject, IDebugEngine
	{
		#region constants
		private const uint c_Monitor_Message = 0x00000001; // The payload is composed of the string characters, no zero at the end.
		#endregion

		#region fields
		/// <summary>
		/// An instance of the nanoFramework.Tools.Debugger.PortBase class
		/// </summary>
		private PortBase _portBase = null;
		/// <summary>
		/// An instance of the nanoFramework.Tools.Debugger.Engine class
		/// </summary>
		private Engine _engine = null;
		/// <summary>
		/// An instance of the nanoFramework.Tools.Debugger.NanoDeviceBase class
		/// </summary>
		private NanoDeviceBase _connectedDevice = null;
		#endregion

		#region events
		/// <summary>
		/// Raised when a c_Monitor_Message from the connected device arrives the debugger
		/// </summary>
		public event DebugMessageEventHandler OnDebugMessage;
		#endregion

		#region public methods
		/// <summary>
		/// Connects the debugger to the device
		/// </summary>
		/// <param name="comPort">COM-Port name</param>
		/// <returns>true if connected successfully</returns>
		public bool Connect(string comPort)
		{
			if (_portBase == null)
			{
				_portBase = PortBase.CreateInstanceForSerial("");
			}

			// wait until all devices are found
			while (!_portBase.IsDevicesEnumerationComplete)
			{
				Thread.Sleep(100);
			}

			// get the NanoFramework device list
			IList nanoFrameworkDevices = _portBase.NanoFrameworkDevices;
			if (nanoFrameworkDevices.Count == 0)
			{
				return false;
			}
			// iterate through the NanoFramework device list
			NanoDeviceBase nanoFrameworkDeviceForConnection = null;
			foreach (NanoDeviceBase nanoFrameworkDevice in nanoFrameworkDevices)
			{
				// does the description contains the delivered port name?
				if (nanoFrameworkDevice.Description.ToUpperInvariant().Contains(comPort.ToUpperInvariant()))
				{
					// device found
					nanoFrameworkDeviceForConnection = nanoFrameworkDevice;
					break;
				}
			}
			// device not found?
			if (nanoFrameworkDeviceForConnection == null)
			{
				return false;
			}

			// get the DebugEngine
			_engine = nanoFrameworkDeviceForConnection.DebugEngine;
			// if null create the DebugEngine get the DebugEngine again
			if (_engine == null)
			{
				nanoFrameworkDeviceForConnection.CreateDebugEngine();
				_engine = nanoFrameworkDeviceForConnection.DebugEngine;
			}
			if (_engine == null)
			{
				return false;
			}
			// connect the nanoFramework debugger to the device
			bool result = _engine.ConnectAsync(5000).Result;
			// connected?
			if (result)
			{
				// add the OnMessage event handler and store the device instance for later use
				_engine.OnMessage += OnMessage;
				_connectedDevice = nanoFrameworkDeviceForConnection;
			}
			return result;
		}

		/// <summary>
		/// Disconnects the debugger from the device
		/// </summary>
		public void Disconnect()
		{
			// no connected device?
			if (_connectedDevice == null)
			{
				return;
			}
			// device.Disconnect
			_connectedDevice.Disconnect();
		}

		/// <summary>
		/// Deploys the assemblies to the device
		/// </summary>
		/// <param name="assemblies">List of all assemblies that should be deployed; each byte[] contains one assembly pe-file as binary</param>
		/// <returns>true if deployed successfully</returns>
		public bool DeploymentExecute(List<byte[]> assemblies)
		{
			// only if the DebugEngine is available
			return _engine != null ? _engine.DeploymentExecute(assemblies) : false;
		}
		#endregion

		#region private methods
		/// <summary>
		/// The event handler gets called when a message from the device arrives at the debugger
		/// </summary>
		/// <param name="message"></param>
		/// <param name="text"></param>
		private void OnMessage(IncomingMessage message, string text)
		{
			// only the c_Monitor_Message's are interesting
			if (message.Header.Cmd == c_Monitor_Message)
			{
				// if a event handler is attached deliver the message to him
				OnDebugMessage?.Invoke(text);
			}
		}
		#endregion
	}
}
