//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System.Reflection;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.ReflectionTests
{
	public class MemberInfoTests : ITestClass, IClassInitialize
	{
		private FieldInfo _info;

		public void ClassInitialize()
		{
			_info = typeof(TestType7).GetField("PublicField");
			Assert.IsNotNull(_info);
		}

		public void DeclaringType()
		{
			Assert.AreEqual(typeof(TestType4).Name, _info.DeclaringType.Name);
		}

		public void MemberType()
		{
			Assert.AreEqual(MemberTypes.Field, _info.MemberType);
		}

		public void Name()
		{
			Assert.AreEqual("PublicField", _info.Name);
		}
	}
}
