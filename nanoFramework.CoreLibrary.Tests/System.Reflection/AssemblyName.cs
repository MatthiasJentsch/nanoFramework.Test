//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using System;
using nanoFramework.Test.Engine;
using System.Reflection;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.ReflectionTests
{
	public class AssemblyNameTests : ITestClass, IClassInitialize
	{
		private AssemblyName _name;

		public void ClassInitialize()
		{
			Assembly a = Assembly.GetExecutingAssembly();
			_name = a.GetName();
		}

		public void FullName()
		{
			Assert.IsTrue(_name.FullName.IndexOf("CoreLibrary") >= 0);
		}

		public void Name()
		{
			Assert.IsTrue(_name.Name.IndexOf("CoreLibrary") >= 0);
		}

		public void Version()
		{
			Version v = _name.Version;
			Assert.IsTrue(v.Major >= 0 && v.Minor >= 0);
		}
	}
}
