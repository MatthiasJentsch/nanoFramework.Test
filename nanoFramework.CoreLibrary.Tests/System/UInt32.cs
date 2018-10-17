//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	[TestClass]
	public class UInt32Tests
	{
		[TestMethod]
		public void Parse()
		{
			Assert.AreEqual(4071108153, uint.Parse("4071108153"));
		}

		[TestMethod]
		public void ToString1()
		{
			Assert.AreEqual("4071108153", ((uint)4071108153).ToString());
		}

		[TestMethod]
		public void ToString2()
		{
			Assert.AreEqual("F2A82E39", ((uint)4071108153).ToString("X8"));
		}
	}
}
