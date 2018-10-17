//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	[TestClass]
	public class Int16Tests
	{
		[TestMethod]
		public void Parse()
		{
			Assert.AreEqual(-4711, short.Parse("-4711"));
		}

		[TestMethod]
		public void ToString1()
		{
			Assert.AreEqual("-4711", ((short)-4711).ToString());
		}

		[TestMethod]
		public void ToString2()
		{
			Assert.AreEqual("ED99", ((short)-4711).ToString("X4"));
		}
	}
}
