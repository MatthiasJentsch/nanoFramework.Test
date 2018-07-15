//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace nanoFramework.Tools.UnitTester
{
	/// <summary>
	/// The wrapper class that communicates with the nanoFramework debugger via reflection
	/// </summary>
	public class DebugEngineReflection : MarshalByRefObject, IDebugEngine
	{
		#region constants
		private const uint c_Monitor_Message = 0x00000001; // The payload is composed of the string characters, no zero at the end.
		#endregion

		#region fields
		/// <summary>
		/// The nanoFramework.Tools.Debugger.PortBase.CreateInstanceForSerial method
		/// </summary>
		private readonly MethodInfo _portBaseCreateInstanceForSerial = null;
		/// <summary>
		/// The nanoFramework.Tools.Debugger.PortBase.IsDevicesEnumerationComplete property
		/// </summary>
		private readonly PropertyInfo _portBaseIsDevicesEnumerationComplete = null;
		/// <summary>
		/// The nanoFramework.Tools.Debugger.PortBase.NanoFrameworkDevices property
		/// </summary>
		private readonly PropertyInfo _portBaseNanoFrameworkDevices = null;
		/// <summary>
		/// The nanoFramework.Tools.Debugger.NanoDeviceBase.Description property
		/// </summary>
		private readonly PropertyInfo _nanoDeviceBaseDescription = null;
		/// <summary>
		/// The nanoFramework.Tools.Debugger.NanoDeviceBase.DebugEngine property
		/// </summary>
		private readonly PropertyInfo _nanoDeviceBaseDebugEngine = null;
		/// <summary>
		/// The nanoFramework.Tools.Debugger.NanoDeviceBase.CreateDebugEngine method
		/// </summary>
		private readonly MethodInfo _nanoDeviceBaseCreateDebugEngine = null;
		/// <summary>
		/// The nanoFramework.Tools.Debugger.NanoDeviceBase.Disconnect method
		/// </summary>
		private readonly MethodInfo _nanoDeviceBaseDisconnect = null;
		/// <summary>
		/// The nanoFramework.Tools.Debugger.Engine.ConnectAsync method
		/// </summary>
		private readonly MethodInfo _engineConnectAsync = null;
		/// <summary>
		/// The nanoFramework.Tools.Debugger.Engine.DeploymentExecute method
		/// </summary>
		private readonly MethodInfo _engineDeploymentExecute = null;
		/// <summary>
		/// The nanoFramework.Tools.Debugger.Engine.OnMessage event
		/// </summary>
		private readonly EventInfo _engineOnMessage = null;
		/// <summary>
		/// The nanoFramework.Tools.Debugger.Engine.OnMessage event handler
		/// </summary>
		private readonly Delegate _engineOnMessageHandler = null;
		/// <summary>
		/// The nanoFramework.Tools.Debugger.WireProtocol.IncomingMessage.Header property
		/// </summary>
		private readonly PropertyInfo _incomingMessageHeader = null;
		/// <summary>
		/// The nanoFramework.Tools.Debugger.WireProtocol.Packet.Cmd field
		/// </summary>
		private readonly FieldInfo _packetCmd = null;
		/// <summary>
		/// An instance of the nanoFramework.Tools.Debugger.PortBase class
		/// </summary>
		private object _portBase = null;
		/// <summary>
		/// An instance of the nanoFramework.Tools.Debugger.Engine class
		/// </summary>
		private object _engine = null;
		/// <summary>
		/// An instance of the nanoFramework.Tools.Debugger.NanoDeviceBase class
		/// </summary>
		private object _connectedDevice = null;
		#endregion

		#region events
		/// <summary>
		/// Raised when a c_Monitor_Message from the connected device arrives the debugger
		/// </summary>
		public event DebugMessageEventHandler OnDebugMessage;
		#endregion

		#region constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="debuggerAssemblyPath">Full path to the nanoFramework debugger assembly</param>
		public DebugEngineReflection(string debuggerAssemblyPath)
		{
			// load the debugger assembly via reflection
			Assembly debuggerAssembly = Assembly.LoadFrom(debuggerAssemblyPath);

			// Find all used members of nanoFramework.Tools.Debugger.PortBase
			Type portBaseType = debuggerAssembly.GetType("nanoFramework.Tools.Debugger.PortBase", true);
			_portBaseCreateInstanceForSerial = portBaseType.GetMethod("CreateInstanceForSerial");
			if (_portBaseCreateInstanceForSerial == null)
			{
				throw new MemberAccessException("Method nanoFramework.Tools.Debugger.PortBase.CreateInstanceForSerial not found");
			}
			_portBaseIsDevicesEnumerationComplete = portBaseType.GetProperty("IsDevicesEnumerationComplete");
			if (_portBaseIsDevicesEnumerationComplete == null)
			{
				throw new MemberAccessException("Property nanoFramework.Tools.Debugger.PortBase.IsDevicesEnumerationComplete not found");
			}
			_portBaseNanoFrameworkDevices = portBaseType.GetProperty("NanoFrameworkDevices");
			if (_portBaseNanoFrameworkDevices == null)
			{
				throw new MemberAccessException("Property nanoFramework.Tools.Debugger.PortBase.IsDevicesEnumerationComplete not found");
			}

			// Find all used members of nanoFramework.Tools.Debugger.NanoDeviceBase
			Type nanoDeviceBaseType = debuggerAssembly.GetType("nanoFramework.Tools.Debugger.NanoDeviceBase", true);
			_nanoDeviceBaseDescription = nanoDeviceBaseType.GetProperty("Description");
			if (_nanoDeviceBaseDescription == null)
			{
				throw new MemberAccessException("Property nanoFramework.Tools.Debugger.NanoDeviceBase.Description not found");
			}
			_nanoDeviceBaseDebugEngine = nanoDeviceBaseType.GetProperty("DebugEngine");
			if (_nanoDeviceBaseDebugEngine == null)
			{
				throw new MemberAccessException("Property nanoFramework.Tools.Debugger.NanoDeviceBase.DebugEngine not found");
			}
			_nanoDeviceBaseCreateDebugEngine = nanoDeviceBaseType.GetMethod("CreateDebugEngine");
			if (_nanoDeviceBaseCreateDebugEngine == null)
			{
				throw new MemberAccessException("Method nanoFramework.Tools.Debugger.NanoDeviceBase.CreateDebugEngine not found");
			}
			_nanoDeviceBaseDisconnect = nanoDeviceBaseType.GetMethod("Disconnect");
			if (_nanoDeviceBaseDisconnect == null)
			{
				throw new MemberAccessException("Method nanoFramework.Tools.Debugger.NanoDeviceBase.Disconnect not found");
			}

			// Find all used members of nanoFramework.Tools.Debugger.Engine
			Type engineType = debuggerAssembly.GetType("nanoFramework.Tools.Debugger.Engine", true);
			_engineConnectAsync = engineType.GetMethod("ConnectAsync");
			if (_engineConnectAsync == null)
			{
				throw new MemberAccessException("Method nanoFramework.Tools.Debugger.Engine.ConnectAsync not found");
			}
			_engineDeploymentExecute = engineType.GetMethod("DeploymentExecute");
			if (_engineDeploymentExecute == null)
			{
				throw new MemberAccessException("Method nanoFramework.Tools.Debugger.Engine.DeploymentExecute not found");
			}
			_engineOnMessage = engineType.GetEvent("OnMessage");
			if (_engineOnMessage == null)
			{
				throw new MemberAccessException("Event nanoFramework.Tools.Debugger.Engine.OnMessage not found");
			}

			// Create a delegate for the OnMessage event
			_engineOnMessageHandler = Delegate.CreateDelegate(_engineOnMessage.EventHandlerType, this, GetType().GetMethod(nameof(OnMessage), BindingFlags.NonPublic | BindingFlags.Instance));
			if (_engineOnMessageHandler == null)
			{
				throw new MemberAccessException("EventHandler for nanoFramework.Tools.Debugger.Engine.OnMessage couldn't created");
			}

			// Find all used members of nanoFramework.Tools.Debugger.WireProtocol.IncomingMessage
			Type incomingMessageType = debuggerAssembly.GetType("nanoFramework.Tools.Debugger.WireProtocol.IncomingMessage", true);
			_incomingMessageHeader = incomingMessageType.GetProperty("Header");
			if (_incomingMessageHeader == null)
			{
				throw new MemberAccessException("Property nanoFramework.Tools.Debugger.WireProtocol.IncomingMessage.Header not found");
			}
			
			// Find all used members of nanoFramework.Tools.Debugger.WireProtocol.Packet
			Type _packetType = debuggerAssembly.GetType("nanoFramework.Tools.Debugger.WireProtocol.Packet", true);
			_packetCmd = _packetType.GetField("Cmd");
			if (_packetCmd == null)
			{
				throw new MemberAccessException("Field nanoFramework.Tools.Debugger.WireProtocol.Packet.Cmd not found");
			}
		}
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
				// PortBase.CreateInstanceForSerial("")
				_portBase = _portBaseCreateInstanceForSerial.Invoke(null, new object[] { "", Type.Missing });
			}

			// wait until portBase.IsDevicesEnumerationComplete returns true
			while (!(bool)_portBaseIsDevicesEnumerationComplete.GetValue(_portBase))
			{
				Thread.Sleep(100);
			}

			// get the NanoFramework device list (portBase.NanoFrameworkDevices)
			IList nanoFrameworkDevices = _portBaseNanoFrameworkDevices.GetValue(_portBase) as IList;
			if (nanoFrameworkDevices.Count == 0)
			{
				return false;
			}
			// iterate through the NanoFramework device list
			object nanoFrameworkDeviceForConnection = null;
			foreach (object nanoFrameworkDevice in nanoFrameworkDevices)
			{
				// does the description contains the delivered port name? (device.Description.ToUpperInvariant().Contains(comPort.ToUpperInvariant())
				if (((string)_nanoDeviceBaseDescription.GetValue(nanoFrameworkDevice)).ToUpperInvariant().Contains(comPort.ToUpperInvariant()))
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

			// get the device.DebugEngine
			_engine = _nanoDeviceBaseDebugEngine.GetValue(nanoFrameworkDeviceForConnection);
			// if null call device.CreateDebugEngine and get the DebugEngine again
			if (_engine == null)
			{
				_nanoDeviceBaseCreateDebugEngine.Invoke(nanoFrameworkDeviceForConnection, null);
				_engine = _nanoDeviceBaseDebugEngine.GetValue(nanoFrameworkDeviceForConnection);
			}
			if (_engine == null)
			{
				return false;
			}
			// connect the nanoFramework debugger to the device (device.DebugEngine.ConnectAsync)
			bool result = ((Task<bool>)_engineConnectAsync.Invoke(_engine, new object[] { 5000, Type.Missing, Type.Missing })).Result;
			// connected?
			if (result)
			{
				// add the OnMessage event handler and store the device instance for later use
				_engineOnMessage.AddEventHandler(this, _engineOnMessageHandler);
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
			_nanoDeviceBaseDisconnect.Invoke(_connectedDevice, null);
		}

		/// <summary>
		/// Deploys the assemblies to the device
		/// </summary>
		/// <param name="assemblies">List of all assemblies that should be deployed; each byte[] contains one assembly pe-file as binary</param>
		/// <returns>true if deployed successfully</returns>
		public bool DeploymentExecute(List<byte[]> assemblies)
		{
			// only if the DebugEngine is available: DebugEngine.DeploymentExecute(<assemblies>)
			return _engine == null ? (bool)_engineDeploymentExecute.Invoke(_engine, new object[] { assemblies, Type.Missing, Type.Missing }) : false;
		}
		#endregion

		#region private methods
		/// <summary>
		/// The event handler gets called when a message from the device arrives at the debugger
		/// </summary>
		/// <param name="message"></param>
		/// <param name="text"></param>
		private void OnMessage(object message, string text)
		{
			// only the c_Monitor_Message's are interesting
			if ((uint)_packetCmd.GetValue(_incomingMessageHeader.GetValue(message)) == c_Monitor_Message)
			{
				// if a event handler is attached deliver the message to him
				OnDebugMessage?.Invoke(text);
			}
		}
		#endregion
	}
}
