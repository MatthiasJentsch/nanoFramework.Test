//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	[TestClass]
	public class Int64Tests
	{
		[TestMethod]
		public void Parse()
		{
			Assert.AreEqual(-1234567899876543210, long.Parse("-1234567899876543210"));
		}

		[TestMethod]
		public void ToString1()
		{
			Assert.AreEqual("-1234567899876543210", ((long)-1234567899876543210).ToString());
		}

		[TestMethod]
		public void ToString2()
		{
			Assert.AreEqual("EEDDEF093CC23516", ((long)-1234567899876543210).ToString("X16"));
		}
	}
}
