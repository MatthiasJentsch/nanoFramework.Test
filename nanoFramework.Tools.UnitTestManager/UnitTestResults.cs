//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;

namespace nanoFramework.Tools.UnitTestManager
{
	/// <summary>
	///   All results of all tests of an assembly
	/// </summary>
	public class UnitTestAssemblyResult
	{
		#region constructor

		/// <summary>
		///   Constructor
		/// </summary>
		/// <param name="testAssemblyName">The test assembly name</param>
		public UnitTestAssemblyResult(string testAssemblyName)
		{
			TestAssemblyName = testAssemblyName;
			ClassResults = new List<UnitTestClassResult>();
			AssemblyStatus = UnitTestStatus.Unknown;
		}

		#endregion

		#region properties

		/// <summary>
		///   The test assembly name
		/// </summary>
		public string TestAssemblyName { get; }

		/// <summary>
		///   The test assembly version
		/// </summary>
		public Version TestAssemblyVersion { get; internal set; }

		/// <summary>
		///   The test execution time stamp
		/// </summary>
		public string TimeStamp { get; internal set; }

		/// <summary>
		///   The test assembly status
		/// </summary>
		public UnitTestStatus AssemblyStatus { get; internal set; }

		/// <summary>
		///   The list of class test results
		/// </summary>
		public List<UnitTestClassResult> ClassResults { get; }

		/// <summary>
		///   Overall execution time of all tests
		/// </summary>
		public decimal ExecutionDurationMilliseconds { get; internal set; }

		#endregion
	}

	/// <summary>
	///   All results of all tests of a class
	/// </summary>
	public class UnitTestClassResult
	{
		#region constructor

		/// <summary>
		///   Constructor
		/// </summary>
		/// <param name="testClassName">The test class name</param>
		public UnitTestClassResult(string testClassName)
		{
			TestClassName = testClassName;
			MethodResults = new List<UnitTestResult>();
			ClassStatus = UnitTestStatus.Unknown;
		}

		#endregion

		#region properties

		/// <summary>
		///   The test class name
		/// </summary>
		public string TestClassName { get; }

		/// <summary>
		///   The test class status
		/// </summary>
		public UnitTestStatus ClassStatus { get; internal set; }

		/// <summary>
		///   The list of the single test results
		/// </summary>
		public List<UnitTestResult> MethodResults { get; }

		/// <summary>
		///   The overall test execution time of all tests of this class
		/// </summary>
		public decimal ExecutionDurationMilliseconds { get; internal set; }

		#endregion
	}

	/// <summary>
	///   A single test result
	/// </summary>
	public class UnitTestResult
	{
		#region properties

		/// <summary>
		///   The test method name
		/// </summary>
		public string TestMethodName { get; }

		/// <summary>
		///   The test execution status (Passed / Failed)
		/// </summary>
		public UnitTestStatus TestStatus { get; internal set; }

		/// <summary>
		///   The test execution time in milliseconds
		/// </summary>
		public decimal ExecutionDurationMilliseconds { get; internal set; }

		/// <summary>
		///   If the test was failing or was ignored: The exception message; null if the test has passed; or the ignore message if the test was ignored
		/// </summary>
		public string Message { get; internal set; }

		#endregion

		#region constructor

		/// <summary>
		///   Constructor
		/// </summary>
		/// <param name="testMethodName">The test method name</param>
		public UnitTestResult(string testMethodName)
		{
			TestMethodName = testMethodName;
			TestStatus = UnitTestStatus.Unknown;
		}

		/// <summary>
		///   Constructor
		/// </summary>
		/// <param name="testMethodName">The test method name</param>
		/// <param name="testStatus">The test execution status (Passed / Failed)</param>
		/// <param name="executionDurationMilliseconds">The test execution time in milliseconds</param>
		/// <param name="message">If the test was failing or was ignored: The exception message; null if the test has passed; or the ignore message if the test was ignored</param>
		public UnitTestResult(string testMethodName, UnitTestStatus testStatus, decimal executionDurationMilliseconds, string message)
		{
			TestMethodName = testMethodName;
			TestStatus = testStatus;
			ExecutionDurationMilliseconds = executionDurationMilliseconds;
			Message = message;
		}

		#endregion
	}

	/// <summary>
	///   The status of a single test
	/// </summary>
	public enum UnitTestStatus
	{
		/// <summary>
		///   Unknown, not executed yet
		/// </summary>
		Unknown = 0,

		/// <summary>
		///   Test passed without a failure
		/// </summary>
		Passed = 1,

		/// <summary>
		///   Test failed with an exception
		/// </summary>
		Failed = 2,

		/// <summary>
		///   Single test or test class has the ignored attribute
		/// </summary>
		Ignored = 3
	}
}