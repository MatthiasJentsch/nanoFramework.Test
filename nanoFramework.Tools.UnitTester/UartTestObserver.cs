//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace nanoFramework.Tools.UnitTester
{
	/// <summary>
	/// The test observer class listens to the c_Monitor_Message's of the nanoFramework debugger and records the test results
	/// </summary>
	internal class UartTestObserver : MarshalByRefObject
	{
		#region enumerations
		/// <summary>
		/// Constant for the regex parsing
		/// </summary>
		private enum MessageParsing
		{
			/// <summary>
			/// Constant for the regex Started
			/// </summary>
			Started,
			/// <summary>
			/// Constant for the regex Finished
			/// </summary>
			Finished,
			/// <summary>
			/// Constant for the regex Passed
			/// </summary>
			Passed,
			/// <summary>
			/// Constant for the regex Failed
			/// </summary>
			Failed
		}
		#endregion

		#region fields
		/// <summary>
		/// All test results
		/// </summary>
		private readonly IList<UnitTestAssemblyResult> _results = new List<UnitTestAssemblyResult>();
		/// <summary>
		/// A semaphore for waiting until all tests has finished
		/// </summary>
		private readonly SemaphoreSlim _waitForFinished = new SemaphoreSlim(0, 1);
		/// <summary>
		/// The regex parsing strings for parsing the debug message
		/// </summary>
		private readonly Dictionary<MessageParsing, string> _regexParsing = new Dictionary<MessageParsing, string>()
		{
			{ MessageParsing.Started, @"(?<assemblyName>[^,]*)\D*(?<assemblyVersion>[\d.]*) : Tests started \((?<ticksPerMillisecond>\d*) ticks = 1 millisecond\)" },
			{ MessageParsing.Passed, @"(?<fullClassName>\S+(?=\.))\.(?<methodName>\S+) : Passed \((?<elapsedTicks>\d*) ticks\)" },
			{ MessageParsing.Failed, @"(?<fullClassName>\S+(?=\.))\.(?<methodName>\S+) : Failed \((?<errorMessage>.+)\)" },
			{ MessageParsing.Finished, @"(?<assemblyName>[^,]*)\D*(?<assemblyVersion>[\d.]*) : Tests finished" }
		};
		private readonly IDictionary<string, DirectoryInfo> _knownAssemblies;
		/// <summary>
		/// The reference to the IDebugEngine for attaching a event handler to the OnDebugMessage event
		/// </summary>
		private SerialPort _serialPort = null;
		/// <summary>
		/// The ticks per milliseconds that the device uses
		/// </summary>
		private decimal TicksPerMillisecond = 1;
		/// <summary>
		/// This field contains the test results instance from the assembly that executes the tests currently
		/// </summary>
		private UnitTestAssemblyResult _currentResult = null;
		/// <summary>
		/// The count of the assemblies which the tests has finished
		/// </summary>
		private int _testedAssemblyCount = 0;

		private StringBuilder _output = null;
		#endregion

		#region constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="testAssemblies">The full paths to the test assemblies (the dll- or exe-files; not the pe-file</param>
		internal UartTestObserver(IList<string> testAssemblies, IDictionary<string, DirectoryInfo> knownAssemblies)
		{
			_knownAssemblies = knownAssemblies;
			AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += ReflectionOnlyAssemblyResolve;
			foreach (string testAssembly in testAssemblies)
			{
				// find all tests that should be executed on the device
				_results.Add(AnalyseTestAssembly(testAssembly));
			}
		}

		private Assembly ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
		{
			string assemblyName = args.Name;
			int delimiter = assemblyName.IndexOf(',');
			if (delimiter > 0)
			{
				assemblyName = assemblyName.Substring(0, delimiter);
			}
			DirectoryInfo extractedPackage;
			if (_knownAssemblies.TryGetValue(assemblyName, out extractedPackage))
			{
				return Assembly.ReflectionOnlyLoadFrom(Path.Combine(extractedPackage.FullName, string.Concat(assemblyName, ".dll")));
			}
			return null;
		}
		#endregion

		#region internal methods
		/// <summary>
		/// Attaches the event handler on the debug engine for listening to the debug messages
		/// </summary>
		/// <param name="debugEngine">The reference to the IDebugEngine for attaching a event handler to the OnDebugMessage event</param>
		internal void ListenToUart(SerialPort comPort)
		{
			_output = new StringBuilder();
			_serialPort = comPort;
			_serialPort.Open();
			_serialPort.DataReceived += OnDebugMessage;
		}

		/// <summary>
		/// Observe the test execution
		/// </summary>
		/// <returns>A task the returns the list of UnitTestAssemblyResult; one entry for each test assembly that was delivered to the constructor</returns>
		internal async Task<IList<UnitTestAssemblyResult>> ObserveExecution()
		{
			// wait for the semaphore and returns the results
			bool test = await _waitForFinished.WaitAsync(120000);
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
		/// Analyses the test assembly and find all tests. The tests will be added to the UnitTestAssemblyResult
		/// </summary>
		/// <param name="testAssembly">Assembly that contains tests</param>
		/// <returns>UnitTestAssemblyResult as placeholder for each test</returns>
		private UnitTestAssemblyResult AnalyseTestAssembly(string testAssembly)
		{
			// load the assembly for reflection
			UnitTestAssemblyResult results = new UnitTestAssemblyResult(Path.GetFileNameWithoutExtension(testAssembly));
			Assembly assembly = Assembly.ReflectionOnlyLoadFrom(testAssembly);
			// store the assembly version
			results.TestAssemblyVersion = assembly.GetName().Version;
			// and iterate through all public classes which have an parameterless constructor and
			// implements the ITestClass interface; get the list from the TestManager
			foreach (DictionaryEntry entry in TestManager.GetTestClassTypes(assembly))
			{
				Type type = (Type)entry.Key;
				Type[] interfaces = (Type[])entry.Value;
				UnitTestClassResult classResult = new UnitTestClassResult(type.FullName);
				// iterate through all public parameterless instance methods that are declared in
				// this class and filter out the "...Initialize" and "...Cleanup" methods; get the list from the TestManager
				foreach (MethodInfo method in TestManager.GetTestMethods(type, interfaces))
				{
					// add a new test result for the test method
					classResult.MethodResults.Add(new UnitTestResult(method.Name));
				}
				// add the new class result; only if min one test found
				if (classResult.MethodResults.Count > 0)
				{
					results.ClassResults.Add(classResult);
				}
			}
			return results;
		}

		/// <summary>
		/// Event handler gets called the a debug message from the device arrives the nanoFramework debugger
		/// </summary>
		/// <param name="message">The debug message</param>
		private void OnDebugMessage(object sender, SerialDataReceivedEventArgs e)
		{
			string className = null;
			UnitTestResult result = null;
			string received = _serialPort.ReadExisting();
			_output.Append(received);
			Console.Write(received);
			if (!_output.ToString().Contains("Tests finished"))
			{
				return;
			}

			foreach (string message in _output.ToString().Split(Environment.NewLine.ToCharArray()))
			{
				foreach (KeyValuePair<MessageParsing, string> entry in _regexParsing)
				{
					result = null;

					// test the message with all possible regex
					Match match = Regex.Match(message, entry.Value);
					if (match.Success)
					{
						// what have we found?
						switch (entry.Key)
						{
							// Tests started: => get the ticks per milliseconds
							case MessageParsing.Started:
								TicksPerMillisecond = decimal.Parse(match.Groups["ticksPerMillisecond"].ToString());
								// activate the current results
								_currentResult = _results.Where(allResults => allResults.TestAssemblyName == match.Groups["assemblyName"].ToString()).Single();
								_currentResult.TimeStamp = DateTime.Now.ToString(new System.Globalization.CultureInfo("en-US"));
								break;
							// One test passed: Create the UnitTestResult
							case MessageParsing.Passed:
								className = match.Groups["fullClassName"].ToString();
								result = new UnitTestResult(match.Groups["methodName"].ToString(), UnitTestStatus.Passed, decimal.Parse(match.Groups["elapsedTicks"].ToString()) / TicksPerMillisecond, null);
								break;
							// One test failed: Create the UnitTestResult
							case MessageParsing.Failed:
								className = match.Groups["fullClassName"].ToString();
								result = new UnitTestResult(match.Groups["methodName"].ToString(), UnitTestStatus.Failed, 0, match.Groups["errorMessage"].ToString());
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
										_serialPort.DataReceived -= OnDebugMessage;
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
					UnitTestClassResult classResult = _currentResult.ClassResults.Where(entry => entry.TestClassName == className).FirstOrDefault();
					// something went wrong if we don't find the test class in the results; maybe the pe-file doesn't match the corresponding dll/exe file
					if (classResult == null)
					{
						throw new ArgumentException($"TestClass {className} not found in the test results");
					}
					// remove the placeholder result and add the current result
					classResult.MethodResults.RemoveAll(entry => entry.TestMethodName == result.TestMethodName);
					classResult.MethodResults.Add(result);
					if (result.TestStatus == UnitTestStatus.Failed)
					{
						classResult.ClassStatus = UnitTestStatus.Failed;
						_currentResult.AssemblyStatus = UnitTestStatus.Failed;
					}
					// cumulate the execution time to the class and the assembly
					classResult.ExecutionDurationMilliseconds += result.ExecutionDurationMilliseconds;
					_currentResult.ExecutionDurationMilliseconds += result.ExecutionDurationMilliseconds;
				}
			}
		}
		#endregion
	}
}
