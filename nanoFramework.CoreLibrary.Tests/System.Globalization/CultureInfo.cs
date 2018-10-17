//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System.Globalization;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.GlobalizationTests
{
	[TestClass]
	public class CultureInfoTests
	{
		[TestMethod]
		public void Constructor()
		{
			CultureInfo ci = new CultureInfo("de-DE");
			Assert.AreEqual("de-DE", ci.ToString());
		}

		[TestMethod]
		public void ToString1()
		{
			CultureInfo ci = new CultureInfo("de-DE");
			Assert.AreEqual("de-DE", ci.ToString());
		}

		[TestMethod]
		public void CurrentUICulture()
		{
			CultureInfo ci = CultureInfo.CurrentUICulture;
			Assert.IsNotNull(ci);
		}

		[TestMethod]
		public void DateTimeFormat()
		{
			Assert.IsNotNull(CultureInfo.CurrentUICulture.DateTimeFormat);
		}

		[TestMethod]
		public void Name()
		{
			CultureInfo ci = new CultureInfo("de-DE");
			Assert.AreEqual("de-DE", ci.Name);
		}

		[TestMethod]
		public void NumberFormat()
		{
			Assert.IsNotNull(CultureInfo.CurrentUICulture.NumberFormat);
		}

		[TestMethod]
		public void Parent()
		{
			CultureInfo ci = new CultureInfo("de-DE").Parent;
			Assert.IsNotNull(ci);
			Assert.AreEqual("de", ci.Name);
		}
	}
}
