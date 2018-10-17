//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	[TestClass]
	public class UInt64Tests
	{
		[TestMethod]
		public void Parse()
		{
			Assert.AreEqual(12345678998765432101, ulong.Parse("12345678998765432101"));
		}

		[TestMethod]
		public void ToString1()
		{
			Assert.AreEqual("12345678998765432101", ((ulong)12345678998765432101).ToString());
		}

		[TestMethod]
		public void ToString2()
		{
			Assert.AreEqual("AB54A9A3A069ED25", ((ulong)12345678998765432101).ToString("X16"));
		}
	}
}
