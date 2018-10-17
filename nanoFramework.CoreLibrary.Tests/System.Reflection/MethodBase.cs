//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System.Reflection;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.ReflectionTests
{
	[TestClass]
	public class MethodBaseTests
	{
		private MethodInfo _info;

		[ClassInitialize]
		public void ClassInitialize()
		{
			_info = typeof(TestType7).GetMethod("PublicMethod");
			Assert.IsNotNull(_info);
		}

		[TestMethod]
		public void Invoke()
		{
			Assert.AreEqual(42, (int)_info.Invoke(new TestType7(), new object[] { }));
		}

		[TestMethod]
		public void DeclaringType()
		{
			Assert.AreEqual(typeof(TestType4).Name, _info.DeclaringType.Name);
		}

		[TestMethod]
		public void IsAbstract()
		{
			Assert.IsFalse(_info.IsAbstract);
		}

		[TestMethod]
		public void IsFinal()
		{
			Assert.IsFalse(_info.IsFinal);
		}

		[TestMethod]
		public void IsPublic()
		{
			Assert.IsTrue(_info.IsPublic);
		}

		[TestMethod]
		public void IsStatic()
		{
			Assert.IsFalse(_info.IsStatic);
		}

		[TestMethod]
		public void IsVirtual()
		{
			Assert.IsFalse(_info.IsVirtual);
		}

		[TestMethod]
		public void Name()
		{
			Assert.AreEqual("PublicMethod", _info.Name);
		}
	}
}
