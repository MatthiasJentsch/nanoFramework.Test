//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	public class UInt16Tests : ITestClass
	{
		public void Parse()
		{
			Assert.AreEqual(47110, ushort.Parse("47110"));
		}

		public void ToString1()
		{
			Assert.AreEqual("47110", ((ushort)47110).ToString());
		}

		public void ToString2()
		{
			Assert.AreEqual("B806", ((ushort)47110).ToString("X4"));
		}
	}
}
