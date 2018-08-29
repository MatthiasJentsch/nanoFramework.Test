//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	public class UInt32Tests : ITestClass
	{
		public void Parse()
		{
			Assert.AreEqual(4071108153, uint.Parse("4071108153"));
		}

		public void ToString1()
		{
			Assert.AreEqual("4071108153", ((uint)4071108153).ToString());
		}

		public void ToString2()
		{
			Assert.AreEqual("F2A82E39", ((uint)4071108153).ToString("X8"));
		}
	}
}
