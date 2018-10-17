//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	[TestClass]
	public class SByteTests
	{
		[TestMethod]
		public void Parse()
		{
			Assert.AreEqual(-42, sbyte.Parse("-42"));
		}

		[TestMethod]
		public void ToString1()
		{
			Assert.AreEqual("-42", ((sbyte)-42).ToString());
		}

		[TestMethod]
		public void ToString2()
		{
			Assert.AreEqual("D6", ((sbyte)-42).ToString("X2"));
		}
	}
}
