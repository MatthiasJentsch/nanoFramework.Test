//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System.Globalization;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.GlobalizationTests
{
	public class CultureInfoTests : ITestClass
	{
		public void Constructor()
		{
			CultureInfo ci = new CultureInfo("de-DE");
			Assert.AreEqual("de-DE", ci.ToString());
		}

		public void ToString1()
		{
			CultureInfo ci = new CultureInfo("de-DE");
			Assert.AreEqual("de-DE", ci.ToString());
		}

		public void CurrentUICulture()
		{
			CultureInfo ci = CultureInfo.CurrentUICulture;
			Assert.IsNotNull(ci);
		}

		public void DateTimeFormat()
		{
			Assert.IsNotNull(CultureInfo.CurrentUICulture.DateTimeFormat);
		}

		public void Name()
		{
			CultureInfo ci = new CultureInfo("de-DE");
			Assert.AreEqual("de-DE", ci.Name);
		}

		public void NumberFormat()
		{
			Assert.IsNotNull(CultureInfo.CurrentUICulture.NumberFormat);
		}

		public void Parent()
		{
			CultureInfo ci = new CultureInfo("de-DE").Parent;
			Assert.IsNotNull(ci);
			Assert.AreEqual("de", ci.Name);
		}
	}
}
