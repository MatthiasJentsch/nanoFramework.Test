//
// Copyright (c) 2018 The nanoFramework project contributors
// Portions Copyright (c) https://github.com/4egod/TinyCLRTest All rights reserved.
// See LICENSE file in the project root for full license information.
//

using System;

namespace nanoFramework.Test.Engine
{
	/// <summary>
	///     Methods for sending the results and info messages vie Console.WriteLine to the attached debugger
	/// </summary>
	internal static class PrintHelper
	{
		#region internal methods

		/// <summary>
		///     Prints a message
		/// </summary>
		/// <param name="message">The message</param>
		internal static void PrintMessage(string message)
		{
			Console.WriteLine(message);
		}

		/// <summary>
		///     Prints the result of one executed test
		/// </summary>
		/// <param name="fullTestClassName">Fully qualified test class name</param>
		/// <param name="testMethodName">Test method name</param>
		/// <param name="testStatus">Status of the executed test (Passed / Failed)</param>
		/// <param name="testException">If the test has failed: The exception that the test has thrown; null if the test has passed</param>
		/// <param name="elapsedTicks">The elapsed time in ticks that the test has consumed</param>
		internal static void PrintTestResult(string fullTestClassName, string testMethodName, TestStatus testStatus,
			Exception testException, long elapsedTicks)
		{
			var result = string.Concat(fullTestClassName, ".", testMethodName, " : ");

			switch (testStatus)
			{
				case TestStatus.Passed:
					// write <Testclass>.<Testmethod> : Passed (<elapsed time as human readable string>)
					result = string.Concat(result, "Passed (", elapsedTicks, " ticks)");
					break;
				case TestStatus.Failed:
					// write <Testclass>.<Testmethod> : Failed (<exception message>)
					result += "Failed";
					if (testException.Message != null || testException.Message.Length != 0)
						result = string.Concat(result, " (", testException.Message, ")");
					break;
				default:
					result += "Undefined";
					break;
			}

			Console.WriteLine(result);
		}

		#endregion
	}
}