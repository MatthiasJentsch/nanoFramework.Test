//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using nanoFramework.Test.Engine;
using nanoFramework.Tools.UnitTestManager;

namespace nanoFramework.Tools.UnitTester
{
	/// <summary>
	///   The test observer class listens to the c_Monitor_Message's of the nanoFramework debugger and records the test results
	/// </summary>
	internal class UartTestObserver : MarshalByRefObject
	{
		#region enumerations

		/// <summary>
		///   Constant for the regex parsing
		/// </summary>
		private enum MessageParsing
		{
			/// <summary>
			///   Constant for the regex Started
			/// </summary>
			Started,

			/// <summary>
			///   Constant for the regex Finished
			/// </summary>
			Finished,

			/// <summary>
			///   Constant for the regex Passed
			/// </summary>
			Passed,

			/// <summary>
			///   Constant for the regex Failed
			/// </summary>
			Failed,

			/// <summary>
			/// class was marked for ignore with the IgnoreAttribute
			/// </summary>
			IgnoredClass,

			/// <summary>
			/// test was marked for ignore with the IgnoreAttribute
			/// </summary>
			IgnoredMethod
		}

		#endregion

		#region fields

		/// <summary>
		///   All test results
		/// </summary>
		private readonly IList<UnitTestAssemblyResult> _results = new List<UnitTestAssemblyResult>();

		/// <summary>
		///   A semaphore for waiting until all tests has finished
		/// </summary>
		private readonly SemaphoreSlim _waitForFinished = new SemaphoreSlim(0, 1);

		/// <summary>
		///   The regex parsing strings for parsing the debug message
		/// </summary>
		private readonly Dictionary<MessageParsing, string> _regexParsing = new Dictionary<MessageParsing, string>
		{
			{MessageParsing.Started, @"(?<assemblyName>[^,]*)\D*(?<assemblyVersion>[\d.]*) : Tests started \((?<ticksPerMillisecond>\d*) ticks = 1 millisecond\)"},
			{MessageParsing.Passed, @"(?<fullClassName>\S+(?=\.))\.(?<methodName>\S+) : Passed \((?<elapsedTicks>\d*) ticks\)"},
			{MessageParsing.Failed, @"(?<fullClassName>\S+(?=\.))\.(?<methodName>\S+) : Failed \((?<errorMessage>.+)\)"},
			{MessageParsing.IgnoredClass, @"(?<fullClassName>\S+) : Ignored with message: (?<ignoreMessage>.+)"},
			{MessageParsing.IgnoredMethod, @"(?<fullClassName>\S+(?=\.))\.(?<methodName>\S+) : Ignored with message: (?<ignoreMessage>.+)"},
			{MessageParsing.Finished, @"(?<assemblyName>[^,]*)\D*(?<assemblyVersion>[\d.]*) : Tests finished"}
		};

		/// <summary>
		///   The reference to the IDebugEngine for attaching a event handler to the OnDebugMessage event
		/// </summary>
		private SerialPort _serialPort;

		/// <summary>
		///   The ticks per milliseconds that the device uses
		/// </summary>
		private decimal _ticksPerMillisecond = 1;

		/// <summary>
		///   This field contains the test results instance from the assembly that executes the tests currently
		/// </summary>
		private UnitTestAssemblyResult _currentResult;

		/// <summary>
		///   The count of the assemblies which the tests has finished
		/// </summary>
		private int _testedAssemblyCount;

		private StringBuilder _output;

		private readonly Timer _timeout = null;

		private void TimeoutCallback(object state)
		{
			_waitForFinished.Release();
		}
		#endregion

		#region constructor

		/// <summary>
		///   Constructor
		/// </summary>
		/// <param name="testAssemblies">The full paths to the test assemblies (the dll- or exe-files; not the pe-file</param>
		internal UartTestObserver(IList<string> testAssemblies)
		{
			AssemblyLoadContext.Default.Resolving += AssemblyResolving;
			foreach (var testAssembly in testAssemblies)
				// find all tests that should be executed on the device
				_results.Add(AnalyseTestAssembly(testAssembly));
			_timeout = new Timer(TimeoutCallback, null, Timeout.Infinite, Timeout.Infinite);
		}

		private Assembly AssemblyResolving(AssemblyLoadContext assemblyLoadContext, AssemblyName assemblyName)
		{
			var name = assemblyName.Name;
			var delimiter = name.IndexOf(',');
			if (delimiter > 0) name = name.Substring(0, delimiter);

			return AppDomain.CurrentDomain.GetAssemblies().AsEnumerable().FirstOrDefault(assembly => assembly.GetName().Name == name);
		}

		#endregion

		#region internal methods

		/// <summary>
		///   Attaches the event handler on the debug engine for listening to the debug messages
		/// </summary>
		/// <param name="comPort">The reference to the IDebugEngine for attaching a event handler to the OnDebugMessage event</param>
		internal void ListenToUart(SerialPort comPort)
		{
			_output = new StringBuilder();
			_serialPort = comPort;
			_serialPort.Open();
			_serialPort.DataReceived += OnDataReceived;
			// Reset the ESP32
			_serialPort.DtrEnable = true;
			Thread.Sleep(100);
			_serialPort.DtrEnable = false;
			// Start the timeout timer
			_timeout.Change(10000, Timeout.Infinite);
			Console.Write("Waiting for serial data ...");
		}

		/// <summary>
		///   Observe the test execution
		/// </summary>
		/// <returns>
		///   A task the returns the list of UnitTestAssemblyResult; one entry for each test assembly that was delivered to
		///   the constructor
		/// </returns>
		internal async Task<IList<UnitTestAssemblyResult>> ObserveExecution()
		{
			// wait for the semaphore and returns the results
			var test = await _waitForFinished.WaitAsync(120000);
			if (!test)
			{
				//throw new TimeoutException();
			}

			return _results;
		}

		/// <summary>
		/// Parses the test results for a captured string.
		/// </summary>
		/// <param name="capturedOutput">A captured output that contains the debug messages that the test manager on the device has emitted.</param>
		/// <returns>The list of UnitTestAssemblyResult; one entry for each test assembly that was delivered to the constructor</returns>
		/*internal IList<UnitTestAssemblyResult> ParseCapturedOutput(string capturedOutput)
		{
			// parse the captured output line by line
			foreach (string line in capturedOutput.Split(Environment.NewLine.ToArray()))
			{
				if (line == string.Empty)
				{
					continue;
				}
				OnDebugMessage(line);
			}
			return _results;
		}*/

		#endregion

		#region private methods

		/// <summary>
		///   Analyses the test assembly and find all tests. The tests will be added to the UnitTestAssemblyResult
		/// </summary>
		/// <param name="testAssembly">Assembly that contains tests</param>
		/// <returns>UnitTestAssemblyResult as placeholder for each test</returns>
		private UnitTestAssemblyResult AnalyseTestAssembly(string testAssembly)
		{
			// load the assembly for reflection
			var results = new UnitTestAssemblyResult(Path.GetFileNameWithoutExtension(testAssembly));
			var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(testAssembly);
			//Assembly assembly = Assembly.ReflectionOnlyLoadFrom(testAssembly);
			//Assembly assembly = TypeLoader.Default.Load
			// store the assembly version
			results.TestAssemblyVersion = assembly.GetName().Version;
			// and iterate through all public classes which have an parameterless constructor and
			// implements the ITestClass interface; get the list from the TestManager
			ArrayList types = TestManager.GetTestClassTypes(assembly);
			if (types != null)
				foreach (Type type in types)
				{
					var classResult = new UnitTestClassResult(type.FullName);
					// iterate through all public parameterless instance methods that are declared in
					// this class and filter out the "...Initialize" and "...Cleanup" methods; get the list from the TestManager
					ArrayList methods = TestManager.GetMethodsWithAttribute(type, typeof(TestMethodAttribute));
					if (methods != null)
					{
						foreach (MethodInfo method in methods)
							// add a new test result for the test method
							classResult.MethodResults.Add(new UnitTestResult(method.Name));
					}
					// add the new class result
					results.ClassResults.Add(classResult);
				}

			return results;
		}

		internal void ParseCapturedResult(string capturedResult)
		{
			_output = new StringBuilder(File.ReadAllText(capturedResult));
			OnDataReceived(null, null);
		}

		/// <summary>
		///   Event handler gets called when data was received via serial port
		/// </summary>
		/// <param name="message">The debug message</param>
		private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			// Reset timer
			_timeout.Change(10000, Timeout.Infinite);
			string className = null;
			UnitTestResult result = null;
			if (e != null)
			{
				var received = _serialPort.ReadExisting();
				_output.Append(received);
				Console.Write(received);
				if (_output.ToString().IndexOfAny(Environment.NewLine.ToCharArray()) >= 0) ProcessNextMessageLines();
			}
		}

		/// <summary>
		///   Event handler gets called when data was received via serial port
		/// </summary>
		private void ProcessNextMessageLines()
		{
			string className = null;
			UnitTestResult result = null;
			string ignoreMessage = null;

			int i;
			while ((i = _output.ToString().IndexOfAny(Environment.NewLine.ToCharArray())) >= 0)
			{
				if (i == 0)
				{
					_output.Remove(0, 1);
					continue;
				}

				string message = _output.ToString().Substring(0, i);

				foreach (var entry in _regexParsing)
				{
					result = null;

					// test the message with all possible regex
					var match = Regex.Match(message, entry.Value);
					if (match.Success)
					{
						// what have we found?
						switch (entry.Key)
						{
							// Tests started: => get the ticks per milliseconds
							case MessageParsing.Started:
								_ticksPerMillisecond = decimal.Parse(match.Groups["ticksPerMillisecond"].ToString());
								// activate the current results
								_currentResult = _results.Where(allResults => allResults.TestAssemblyName == match.Groups["assemblyName"].ToString()).Single();
								_currentResult.TimeStamp = DateTime.Now.ToString(new CultureInfo("en-US"));
								break;
							// One test passed: Create the UnitTestResult
							case MessageParsing.Passed:
								className = match.Groups["fullClassName"].ToString();
								result = new UnitTestResult(match.Groups["methodName"].ToString(), UnitTestStatus.Passed, decimal.Parse(match.Groups["elapsedTicks"].ToString()) / _ticksPerMillisecond, null);
								break;
							// One test failed: Create the UnitTestResult
							case MessageParsing.Failed:
								className = match.Groups["fullClassName"].ToString();
								result = new UnitTestResult(match.Groups["methodName"].ToString(), UnitTestStatus.Failed, 0, match.Groups["errorMessage"].ToString());
								break;
							// The whole test class has the ignore attribute
							case MessageParsing.IgnoredClass:
								className = match.Groups["fullClassName"].ToString();
								ignoreMessage = match.Groups["ignoreMessage"].ToString();
								break;
							// One test method has the ignore attribute
							case MessageParsing.IgnoredMethod:
								className = match.Groups["fullClassName"].ToString();
								result = new UnitTestResult(match.Groups["methodName"].ToString(), UnitTestStatus.Ignored, 0, match.Groups["ignoreMessage"].ToString());
								break;
							// All tests finished: one more assembly has finished the tests; done?
							case MessageParsing.Finished:
								_currentResult = null;
								_testedAssemblyCount++;
								// all assemblies tested? => detach the event handler and release the semaphore
								if (_testedAssemblyCount == _results.Count)
								{
									if (_serialPort != null)
									{
										_serialPort.DataReceived -= OnDataReceived;
										_serialPort.Close();
									}

									_waitForFinished.Release();
								}

								break;
						}

						break;
					}
				}

				// UnitTestResult created?
				if (result != null)
				{
					// find the class results
					var classResult = _currentResult.ClassResults.FirstOrDefault(entry => entry.TestClassName == className);
					// something went wrong if we don't find the test class in the results; maybe the pe-file doesn't match the corresponding dll/exe file
					if (classResult == null) throw new ArgumentException($"TestClass {className} not found in the test results");
					// remove the placeholder result and add the current result
					classResult.MethodResults.RemoveAll(entry => entry.TestMethodName == result.TestMethodName);
					classResult.MethodResults.Add(result);
					if (result.TestStatus == UnitTestStatus.Failed)
					{
						classResult.ClassStatus = UnitTestStatus.Failed;
						_currentResult.AssemblyStatus = UnitTestStatus.Failed;
					}
					else
					{
						if (classResult.ClassStatus != UnitTestStatus.Failed) classResult.ClassStatus = UnitTestStatus.Passed;
						if (_currentResult.AssemblyStatus != UnitTestStatus.Failed) _currentResult.AssemblyStatus = UnitTestStatus.Passed;
					}

					// cumulate the execution time to the class and the assembly
					classResult.ExecutionDurationMilliseconds += result.ExecutionDurationMilliseconds;
					_currentResult.ExecutionDurationMilliseconds += result.ExecutionDurationMilliseconds;
				}

				// whole class ignored?
				if (ignoreMessage != null)
				{
					// find the class results
					var classResult = _currentResult.ClassResults.FirstOrDefault(entry => entry.TestClassName == className);
					// something went wrong if we don't find the test class in the results; maybe the pe-file doesn't match the corresponding dll/exe file
					if (classResult == null) throw new ArgumentException($"TestClass {className} not found in the test results");
					classResult.ClassStatus = UnitTestStatus.Ignored;
					foreach (UnitTestResult entry in classResult.MethodResults)
					{
						entry.TestStatus = UnitTestStatus.Ignored;
						entry.Message = ignoreMessage;
					}
				}
				_output.Remove(0, i + 1);
			}
		}
		#endregion
	}
}