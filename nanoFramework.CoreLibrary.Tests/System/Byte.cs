//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	public class ByteTests : ITestClass
	{
		public void Parse()
		{
			Assert.AreEqual(42, byte.Parse("42"));
		}

		public void ToString1()
		{
			Assert.AreEqual("42", ((byte)42).ToString());
		}

		public void ToString2()
		{
			Assert.AreEqual("2A", ((byte)42).ToString("X2"));
		}
	}
}
