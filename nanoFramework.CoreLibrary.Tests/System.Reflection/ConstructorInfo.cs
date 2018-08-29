//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;
using System.Reflection;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.ReflectionTests
{
	public class ConstructorInfoTests : ITestClass, IClassInitialize
	{
		private ConstructorInfo _info;

		public void ClassInitialize()
		{
			_info = typeof(TestType1).GetConstructor(new Type[] { });
			Assert.IsNotNull(_info);
		}

		public void Invoke()
		{
			TestType1 cii = _info.Invoke(new object[] { }) as TestType1;
			Assert.IsNotNull(cii);
		}

		public void MemberType()
		{
			Assert.AreEqual(MemberTypes.Constructor, _info.MemberType);
		}
	}
}
