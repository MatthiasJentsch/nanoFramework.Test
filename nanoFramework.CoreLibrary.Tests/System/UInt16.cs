//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	[TestClass]
	public class UInt16Tests
	{
		[TestMethod]
		public void Parse()
		{
			Assert.AreEqual(47110, ushort.Parse("47110"));
		}

		[TestMethod]
		public void ToString1()
		{
			Assert.AreEqual("47110", ((ushort)47110).ToString());
		}

		[TestMethod]
		public void ToString2()
		{
			Assert.AreEqual("B806", ((ushort)47110).ToString("X4"));
		}
	}
}
