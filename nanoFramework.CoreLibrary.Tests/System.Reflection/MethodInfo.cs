//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;
using System.Reflection;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.ReflectionTests
{
	[TestClass]
	public class MethodInfoTests
	{
		private MethodInfo _info;

		[ClassInitialize]
		public void ClassInitialize()
		{
			_info = typeof(TestType7).GetMethod("PublicMethod");
			Assert.IsNotNull(_info);
		}

		[TestMethod]
		public void MemberType()
		{
			Assert.AreEqual(MemberTypes.Method, _info.MemberType);
		}

		[TestMethod]
		public void ReturnType()
		{
			Type t = _info.ReturnType;
			Assert.IsNotNull(t);
			Assert.AreEqual(typeof(int).Name, t.Name);
		}
	}
}