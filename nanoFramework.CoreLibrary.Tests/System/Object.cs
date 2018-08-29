//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	public class ObjectTests : ITestClass
	{
		public void Equals1()
		{
			object a = new object();
			object b = new object();
			object c = a;
			Assert.IsFalse(a.Equals(b));
			Assert.IsTrue(a.Equals(c));
		}

		public void Equals2()
		{
			object a = new object();
			object b = new object();
			object c = a;
			Assert.IsFalse(object.Equals(a, b));
			Assert.IsTrue(object.Equals(a, c));
		}

		public void GetHashCode1()
		{
			object a = new object();
			object b = new object();
			object c = a;
			Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
			Assert.AreEqual(a.GetHashCode(), c.GetHashCode());
		}

		public void GetType1()
		{
			object a = new object();
			object b = new DateTime();
			Assert.AreEqual("System.Object", a.GetType().FullName);
			Assert.AreEqual("System.DateTime", b.GetType().FullName);
		}

		public void ReferenceEquals1()
		{
			object a = new object();
			object b = new object();
			object c = a;
			Assert.IsFalse(object.ReferenceEquals(a, b));
			Assert.IsTrue(object.ReferenceEquals(a, c));
		}

		public void ToString1()
		{
			object a = new object();
			Assert.AreEqual("System.Object", a.ToString());
		}
	}
}
