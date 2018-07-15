//
// Copyright (c) 2018 The nanoFramework project contributors
// Portions Copyright (c) https://github.com/4egod/TinyCLRTest All rights reserved.
// See LICENSE file in the project root for full license information.
//

using System;

namespace nanoFramework.Test.Engine
{
	/// <summary>
	/// That's the exception that is thrown when an assert fails
	/// </summary>
	public class AssertFailedException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:nfUnit.AssertFailedException" /> class.
		/// </summary>
		public AssertFailedException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:nfUnit.AssertFailedException" /> class that uses with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public AssertFailedException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:nfUnit.AssertFailedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="exception">The exception that is the cause of the current exception. If the <paramref name="exception" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		public AssertFailedException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
