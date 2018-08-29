//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System.Reflection;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.ReflectionTests
{
	public class MethodBaseTests : ITestClass, IClassInitialize
	{
		private MethodInfo _info;

		public void ClassInitialize()
		{
			_info = typeof(TestType7).GetMethod("PublicMethod");
			Assert.IsNotNull(_info);
		}

		public void Invoke()
		{
			Assert.AreEqual(42, (int)_info.Invoke(new TestType7(), new object[] { }));
		}

		public void DeclaringType()
		{
			Assert.AreEqual(typeof(TestType4).Name, _info.DeclaringType.Name);
		}

		public void IsAbstract()
		{
			Assert.IsFalse(_info.IsAbstract);
		}

		public void IsFinal()
		{
			Assert.IsFalse(_info.IsFinal);
		}

		public void IsPublic()
		{
			Assert.IsTrue(_info.IsPublic);
		}

		public void IsStatic()
		{
			Assert.IsFalse(_info.IsStatic);
		}

		public void IsVirtual()
		{
			Assert.IsFalse(_info.IsVirtual);
		}

		public void Name()
		{
			Assert.AreEqual("PublicMethod", _info.Name);
		}
	}
}
