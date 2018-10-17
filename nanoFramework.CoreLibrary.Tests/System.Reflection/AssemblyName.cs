//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using System;
using nanoFramework.Test.Engine;
using System.Reflection;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.ReflectionTests
{
	[TestClass]
	public class AssemblyNameTests
	{
		private AssemblyName _name;

		[ClassInitialize]
		public void ClassInitialize()
		{
			Assembly a = Assembly.GetExecutingAssembly();
			_name = a.GetName();
		}

		[TestMethod]
		public void FullName()
		{
			Assert.IsTrue(_name.FullName.IndexOf("CoreLibrary") >= 0);
		}

		[TestMethod]
		public void Name()
		{
			Assert.IsTrue(_name.Name.IndexOf("CoreLibrary") >= 0);
		}

		[TestMethod]
		public void Version()
		{
			Version v = _name.Version;
			Assert.IsTrue(v.Major >= 0 && v.Minor >= 0);
		}
	}
}
