//
// Copyright (c) 2018 The nanoFramework project contributors
// Portions Copyright (c) https://github.com/4egod/TinyCLRTest All rights reserved.
// See LICENSE file in the project root for full license information.
//

namespace nanoFramework.Test.Engine
{
	/// <summary>
	///     The status of a single test
	/// </summary>
	internal enum TestStatus
	{
		/// <summary>
		///     Undefined, not executed yet
		/// </summary>
		Undefined = 0,

		/// <summary>ne
		///     Test passed without a failure
		/// </summary>
		Passed = 1,

		/// <summary>
		///     Test failed with an exception
		/// </summary>
		Failed = 2,

		/// <summary>
		/// test was marked for ignore with the <see cref="IgnoreAttribute"/>
		/// </summary>
		Ignored = 3
	}
}