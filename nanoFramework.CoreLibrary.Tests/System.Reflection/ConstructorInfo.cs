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
	public class ConstructorInfoTests
	{
		private ConstructorInfo _info;

		[ClassInitialize]
		public void ClassInitialize()
		{
			_info = typeof(TestType1).GetConstructor(new Type[] { });
			Assert.IsNotNull(_info);
		}

		[TestMethod]
		public void Invoke()
		{
			TestType1 cii = _info.Invoke(new object[] { }) as TestType1;
			Assert.IsNotNull(cii);
		}

		[TestMethod]
		public void MemberType()
		{
			Assert.AreEqual(MemberTypes.Constructor, _info.MemberType);
		}
	}
}
