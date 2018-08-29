//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	public class BooleanTests : ITestClass
	{
		public void ToString1()
		{
			Assert.AreEqual("True", true.ToString());
			Assert.AreEqual("False", false.ToString());
		}
	}
}
