//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	public class Int32Tests : ITestClass
	{
		public void Parse()
		{
			Assert.AreEqual(-471108153, int.Parse("-471108153"));
		}

		public void ToString1()
		{
			Assert.AreEqual("-471108153", ((int)-471108153).ToString());
		}

		public void ToString2()
		{
			Assert.AreEqual("E3EB75C7", ((int)-471108153).ToString("X8"));
		}
	}
}
