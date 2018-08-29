//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	public class CharTests : ITestClass
	{
		public void ToLower1()
		{
			// TODO!
			/*Assert.AreEqual('a', 'A'.ToLower());
			Assert.AreEqual(':', ':'.ToLower());
			Assert.AreEqual('€', '€'.ToLower());*/
		}

		public void ToString1()
		{
			Assert.AreEqual("A", 'A'.ToString());
			Assert.AreEqual("€", '€'.ToString());
		}

		public void ToUpper()
		{
			// TODO!
			/*Assert.AreEqual('C', 'c'.ToUpper());
			Assert.AreEqual('+', '+'.ToUpper());
			Assert.AreEqual('µ', 'µ'.ToUpper());*/
		}
	}
}
