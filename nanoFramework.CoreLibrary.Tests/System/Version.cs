//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	public class VersionTests : ITestClass
	{
		public void Constructor1()
		{
			Version v = new Version(1, 2);
			Assert.AreEqual(1, v.Major);
			Assert.AreEqual(2, v.Minor);
			Assert.AreEqual(-1, v.Build);
			Assert.AreEqual(-1, v.Revision);
		}

		public void Constructor2()
		{
			Version v = new Version(5, 6, 7, 8);
			Assert.AreEqual(5, v.Major);
			Assert.AreEqual(6, v.Minor);
			Assert.AreEqual(7, v.Build);
			Assert.AreEqual(8, v.Revision);
		}

		public void Equals1()
		{
			Version v1 = new Version(1, 2);
			Version v2 = new Version(5, 6, 7, 8);
			Version v3 = new Version(1, 2);
			Assert.IsFalse(v1.Equals(v2));
			Assert.IsTrue(v1.Equals(v3));
		}

		public void ToString1()
		{
			Version v = new Version(5, 6, 7, 8);
			Assert.AreEqual("5.6.7.8", v.ToString());
		}

		public void Major()
		{
			Version v = new Version(5, 6, 7, 8);
			Assert.AreEqual(5, v.Major);
		}

		public void Minor()
		{
			Version v = new Version(5, 6, 7, 8);
			Assert.AreEqual(6, v.Minor);
		}

		public void Build()
		{
			Version v = new Version(5, 6, 7, 8);
			Assert.AreEqual(7, v.Build);
		}

		public void Revision()
		{
			Version v = new Version(5, 6, 7, 8);
			Assert.AreEqual(8, v.Revision);
		}
	}
}
