//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	public class SingleTests : ITestClass
	{
		public void ToString1()
		{
			Assert.AreEqual("4E+23", (4E+23f).ToString());
			Assert.AreEqual("-8E-23", (-8E-23f).ToString());
		}

		public void ToString2()
		{
			Assert.AreEqual("30000000", 3E7f.ToString("F0"));
		}
	}
}
