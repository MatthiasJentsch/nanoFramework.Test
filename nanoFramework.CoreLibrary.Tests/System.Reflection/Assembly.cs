//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;
using System.Globalization;
using System.Reflection;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.ReflectionTests
{
	public class AssemblyTests : ITestClass
	{
		public void GetAssembly()
		{
			Assembly a = Assembly.GetAssembly(typeof(ITestClass));
			Assert.IsNotNull(a);
			Assert.AreEqual(typeof(ITestClass).Assembly, a);
		}

		public void GetExecutingAssembly()
		{
			Assembly a = Assembly.GetExecutingAssembly();
			Assert.IsNotNull(a);
			Assert.AreEqual(this.GetType().Assembly, a);
			Assert.AreNotEqual(typeof(ITestClass).Assembly, a);
		}

		public void GetName()
		{
			Assembly a = Assembly.GetExecutingAssembly();
			AssemblyName n = a.GetName();
			Assert.IsNotNull(n);
			Assert.IsTrue(n.ToString().IndexOf("CoreLibrary") >= 0);
		}

		public void GetSatelliteAssembly()
		{
			Assembly a = Assembly.GetExecutingAssembly();
			try
			{
				// GetSatelliteAssembly should throw exception because there is no satellite assembly available
				Assembly s = a.GetSatelliteAssembly(CultureInfo.CurrentUICulture);
				Assert.Fail();
			}
			catch (Exception) { }
		}

		public void GetType1()
		{
			Assembly a = Assembly.GetAssembly(typeof(ITestClass));
			Type t1 = a.GetType(typeof(ITestClass).FullName);
			Assert.IsNotNull(t1);
			try
			{
				Type t2 = a.GetType("NotExistingClass");
				Assert.Fail();
			}
			catch (Exception) { }
		}

		public void GetType2()
		{
			Assembly a = Assembly.GetAssembly(typeof(ITestClass));
			Type t1 = a.GetType(typeof(ITestClass).FullName, false);
			Assert.IsNotNull(t1);
			Type t2 = a.GetType("NotExistingClass", false);
			Assert.IsNull(t2);
			try
			{
				Type t3 = a.GetType("NotExistingClass", true);
				Assert.Fail();
			}
			catch (Exception) { }
		}

		public void GetTypes()
		{
			Assembly a = Assembly.GetAssembly(typeof(ITestClass));
			Type[] t = a.GetTypes();
			Assert.IsNotNull(t);
			Assert.IsTrue(t.Length > 0);
		}

		public void FullName()
		{
			Assembly a = Assembly.GetExecutingAssembly();
			string n = a.FullName;
			Assert.IsNotNull(n);
			Assert.IsTrue(n.ToString().IndexOf("CoreLibrary") >= 0);
		}
	}
}
