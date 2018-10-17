//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System.Reflection;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.ReflectionTests
{
	[TestClass]
	public class MemberInfoTests
	{
		private FieldInfo _info;

		[ClassInitialize]
		public void ClassInitialize()
		{
			_info = typeof(TestType7).GetField("PublicField");
			Assert.IsNotNull(_info);
		}

		[TestMethod]
		public void DeclaringType()
		{
			Assert.AreEqual(typeof(TestType4).Name, _info.DeclaringType.Name);
		}

		[TestMethod]
		public void MemberType()
		{
			Assert.AreEqual(MemberTypes.Field, _info.MemberType);
		}

		[TestMethod]
		public void Name()
		{
			Assert.AreEqual("PublicField", _info.Name);
		}
	}
}
