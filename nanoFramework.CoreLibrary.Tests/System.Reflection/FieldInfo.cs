//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;
using System.Reflection;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.ReflectionTests
{
	public class FieldInfoTests : ITestClass, IClassInitialize
	{
		private FieldInfo _info;

		public void ClassInitialize()
		{
			_info = typeof(TestType1).GetField("PublicField");
			Assert.IsNotNull(_info);
		}

		public void GetValue()
		{
			int x = (int)_info.GetValue(new TestType1());
			Assert.AreEqual(-1, x);
		}

		public void SetValue()
		{
			TestType1 i = new TestType1();
			_info.SetValue(i, 4711);
			int x = (int)_info.GetValue(i);
			Assert.AreEqual(4711, x);
		}

		public void FieldType()
		{
			Type t = _info.FieldType;
			Assert.IsNotNull(t);
			Assert.AreEqual(typeof(int).Name, t.Name);
		}

		public void MemberType()
		{
			Assert.AreEqual(MemberTypes.Field, _info.MemberType);
		}
	}
}
