//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace nanoFramework.Tools.UnitTester
{
	/// <summary>
	/// Delegate type for the message that is send from the device to the debugger
	/// </summary>
	/// <param name="message">Message that was send to the debugger</param>
	public delegate void DebugMessageEventHandler(string message);

	/// <summary>
	/// The interface for communicating with the nanoFramework debugger
	/// </summary>
	public interface IDebugEngine
	{
		/// <summary>
		/// Called if a message for the nanoFramework debugger arrives
		/// </summary>
		event DebugMessageEventHandler OnDebugMessage;

		/// <summary>
		/// Connects the debugger to the device
		/// </summary>
		/// <param name="comPort">COM-Port name</param>
		/// <returns>true if connected successfully</returns>
		bool Connect(string comPort);

		/// <summary>
		/// Disconnects the debugger from the device
		/// </summary>
		void Disconnect();

		/// <summary>
		/// Deploys the assemblies to the device
		/// </summary>
		/// <param name="assemblies">List of all assemblies that should be deployed; each byte[] contains one assembly pe-file as binary</param>
		/// <returns>true if deployed successfully</returns>
		bool DeploymentExecute(List<byte[]> assemblies);
	}

	/// <summary>
	/// Kind of the debug engine instantiation
	/// </summary>
	internal enum DebugEngineKind
	{
		/// <summary>
		/// The debug engine runs in the same app domain as the caller; the members are early bind
		/// </summary>
		DirectSameAppDomain,
		/// <summary>
		/// The debug engine runs in a new app domain; the members are early bind
		/// </summary>
		DirectNewAppDomain,
		/// <summary>
		/// The debug engine runs in the same app domain as the caller; the members are called via reflection
		/// </summary>
		ReflectionSameAppDomain,
		/// <summary>
		/// The debug engine runs in a new app domain; the members are called via reflection
		/// </summary>
		ReflectionNewAppDomain
	}

	/// <summary>
	/// A factory for IDebugEngine instance creation
	/// </summary>
	internal static class DebugEngineFactory
	{
		#region fields
		/// <summary>
		/// The debugger app domain if the nanoFramework debugger should run a separate app domain
		/// </summary>
		private static AppDomain _debuggerAppDomain = null;
		#endregion

		#region internal methods 
		/// <summary>
		/// Creates an IDebugEngine instance of the given kind
		/// </summary>
		/// <param name="debuggerAssemblyPath">Full path to the nanoFramework debugger assembly</param>
		/// <param name="kind">Kind of the debug engine instantiation</param>
		/// <returns>The IDebugEngine instance</returns>
		internal static IDebugEngine Get(string debuggerAssemblyPath, DebugEngineKind kind)
		{
			switch (kind)
			{
				// The debug engine runs in the same app domain as the caller; the members are early bind
				case DebugEngineKind.DirectSameAppDomain:
					// Delete a preexisting nanoFramework debugger assembly
					if (File.Exists("nanoFramework.Tools.Debugger.dll"))
					{
						File.Delete("nanoFramework.Tools.Debugger.dll");
					}
					// Attach a event handler for finding the nanoFramework debugger assembly
					AppDomain.CurrentDomain.SetData("_debuggerAssemblyPath", debuggerAssemblyPath);
					AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;
					return new DebugEngineDirect();

				// The debug engine runs in a new app domain; the members are early bind
				case DebugEngineKind.DirectNewAppDomain:
					// Delete a preexisting nanoFramework debugger assembly
					if (File.Exists("nanoFramework.Tools.Debugger.dll"))
					{
						File.Delete("nanoFramework.Tools.Debugger.dll");
					}
					// create the app domain
					_debuggerAppDomain = AppDomain.CreateDomain("nanoFramework.Tools.Debugger");
					// Attach a event handler for finding the nanoFramework debugger assembly
					_debuggerAppDomain.SetData("_debuggerAssemblyPath", debuggerAssemblyPath);
					_debuggerAppDomain.AssemblyResolve += AssemblyResolve;
					// create the debugger instance in the app domain
					return _debuggerAppDomain.CreateInstanceFromAndUnwrap(Assembly.GetExecutingAssembly().CodeBase, typeof(DebugEngineDirect).FullName) as IDebugEngine;

				// The debug engine runs in the same app domain as the caller; the members are called via reflection
				case DebugEngineKind.ReflectionSameAppDomain:
					AppDomain.CurrentDomain.SetData("_debuggerAssemblyPath", debuggerAssemblyPath);
					return new DebugEngineReflection(debuggerAssemblyPath);

				// The debug engine runs in a new app domain; the members are called via reflection
				case DebugEngineKind.ReflectionNewAppDomain:
					// create the app domain
					_debuggerAppDomain = AppDomain.CreateDomain("nanoFramework.Tools.Debugger");
					_debuggerAppDomain.SetData("_debuggerAssemblyPath", debuggerAssemblyPath);
					// create the debugger instance in the app domain
					return _debuggerAppDomain.CreateInstanceFromAndUnwrap(Assembly.GetExecutingAssembly().CodeBase, typeof(DebugEngineReflection).FullName, false, BindingFlags.Default, null, new object[] { debuggerAssemblyPath }, null, null) as IDebugEngine;

				default:
					throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Unloads the debugger app domain
		/// </summary>
		internal static void UnloadAppDomain()
		{
			// only if a app domain is created
			if (_debuggerAppDomain != null)
			{
				AppDomain.Unload(_debuggerAppDomain);
			}
		}
		#endregion

		#region private methods
		/// <summary>
		/// Gets called if an Assembly was not found
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="args">Event arguments</param>
		/// <returns>The assembly that was found by a custom mechanism</returns>
		private static Assembly AssemblyResolve(object sender, ResolveEventArgs args)
		{
			// only the nanoFramework.Tools.Debugger can be loaded here
			if (args.Name.StartsWith("nanoFramework.Tools.Debugger"))
			{
				// the path to the nanoFramework.Tools.Debugger.dll is set in the current domain data
				return Assembly.LoadFrom((string)AppDomain.CurrentDomain.GetData("_debuggerAssemblyPath"));
			}
			return null;
		}
		#endregion
	}
}
