//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	[TestClass]
	public class Int32Tests
	{
		[TestMethod]
		public void Parse()
		{
			Assert.AreEqual(-471108153, int.Parse("-471108153"));
		}

		[TestMethod]
		public void ToString1()
		{
			Assert.AreEqual("-471108153", ((int)-471108153).ToString());
		}

		[TestMethod]
		public void ToString2()
		{
			Assert.AreEqual("E3EB75C7", ((int)-471108153).ToString("X8"));
		}
	}
}
