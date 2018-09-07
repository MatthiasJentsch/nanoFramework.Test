//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;
using System.Reflection;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	public class TypeTests : ITestClass
	{
		public void GetConstructor()
		{
			Assert.IsNotNull(typeof(TestType1).GetConstructor(new Type[] { }));
			Assert.IsNotNull(typeof(TestType1).GetConstructor(new Type[] { typeof(string) }));
			Assert.IsNotNull(typeof(TestType1).GetConstructor(new Type[] { typeof(string), typeof(int) }));
			Assert.IsNull(typeof(TestType1).GetConstructor(new Type[] { typeof(bool) }));
		}

		public void GetElementType()
		{
			Assert.AreEqual("TestType1", typeof(TestType1[]).GetElementType().Name);
		}

		public void GetField1()
		{
			Assert.IsNotNull(typeof(TestType1).GetField("PublicField"));
			Assert.IsNull(typeof(TestType1).GetField("PrivateField"));
		}

		public void GetField2()
		{
			Assert.IsNull(typeof(TestType1).GetField("PublicField", BindingFlags.NonPublic | BindingFlags.Instance));
			Assert.IsNotNull(typeof(TestType1).GetField("PrivateField", BindingFlags.NonPublic | BindingFlags.Instance));
		}

		public void GetFields1()
		{
			FieldInfo[] f = typeof(TestType1).GetFields();
			Assert.IsNotNull(f);
			Assert.AreEqual(1, f.Length);
		}

		public void GetFields2()
		{
			FieldInfo[] f1 = typeof(TestType1).GetFields(BindingFlags.Public | BindingFlags.Instance);
			Assert.IsNotNull(f1);
			Assert.AreEqual(1, f1.Length);
			FieldInfo[] f2 = typeof(TestType1).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
			Assert.IsNotNull(f2);
			Assert.AreEqual(1, f2.Length);
			FieldInfo[] f3 = typeof(TestType1).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			Assert.IsNotNull(f3);
			Assert.AreEqual(2, f3.Length);
		}

		public void GetInterfaces()
		{
			Type[] i1 = typeof(TestType1).GetInterfaces();
			Assert.IsNotNull(i1);
			Assert.AreEqual(0, i1.Length);
			Type[] i2 = typeof(TestType4).GetInterfaces();
			Assert.IsNotNull(i2);
			Assert.AreEqual(1, i2.Length);
			Type[] i3 = typeof(TestType5).GetInterfaces();
			Assert.IsNotNull(i3);
			Assert.AreEqual(2, i3.Length);
			Type[] i4 = typeof(TestType6).GetInterfaces();
			Assert.IsNotNull(i4);
			Assert.AreEqual(2, i4.Length);
			Type[] i5 = typeof(TestType7).GetInterfaces();
			Assert.IsNotNull(i5);
			Assert.AreEqual(2, i5.Length);
		}

		public void GetMethod1()
		{
			MethodInfo m1 = typeof(TestType1).GetMethod("PublicMethod1");
			Assert.IsNotNull(m1);
			try
			{
				// PublicMethod2 has overloads! GetMethod should throw exception
				MethodInfo m2 = typeof(TestType1).GetMethod("PublicMethod2");
				Assert.Fail();
			}
			catch (Exception) { }
			MethodInfo m3 = typeof(TestType1).GetMethod("PrivateMethod1");
			Assert.IsNull(m3);
			MethodInfo m4 = typeof(TestType1).GetMethod("NotExistingMethod");
			Assert.IsNull(m4);
		}

		public void GetMethod2()
		{
			MethodInfo m1 = typeof(TestType1).GetMethod("PublicMethod2", new Type[] { });
			Assert.IsNotNull(m1);
			MethodInfo m2 = typeof(TestType1).GetMethod("PublicMethod2", new Type[] { typeof(int) });
			Assert.IsNotNull(m2);
			MethodInfo m3 = typeof(TestType1).GetMethod("PublicMethod2", new Type[] { typeof(string) });
			Assert.IsNull(m3);
		}

		public void GetMethod3()
		{
			MethodInfo m1 = typeof(TestType1).GetMethod("PublicMethod1", BindingFlags.NonPublic | BindingFlags.Instance);
			Assert.IsNull(m1);
			MethodInfo m2 = typeof(TestType1).GetMethod("PrivateMethod1", BindingFlags.NonPublic | BindingFlags.Instance);
			Assert.IsNotNull(m2);
			try
			{
				// PrivateMethod2 has overloads! GetMethod should throw exception
				MethodInfo m3 = typeof(TestType1).GetMethod("PrivateMethod2", BindingFlags.NonPublic | BindingFlags.Instance);
				Assert.Fail();
			}
			catch (Exception) { }
			MethodInfo m4 = typeof(TestType1).GetMethod("NotExistingMethod", BindingFlags.NonPublic | BindingFlags.Instance);
			Assert.IsNull(m4);
		}

		public void GetMethods1()
		{
			MethodInfo[] m = typeof(TestType1).GetMethods();
			Assert.IsNotNull(m);
			// 3 own methods plus 2 property methods (get/set) plus the inherited methods from object
			Assert.AreEqual(3 + 2 + typeof(object).GetMethods(BindingFlags.Public | BindingFlags.Instance).Length, m.Length);
		}

		public void GetMethods2()
		{
			MethodInfo[] m = typeof(TestType1).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
			Assert.IsNotNull(m);
			// 3 own methods
			Assert.AreEqual(3, m.Length);
		}

		public void GetType1()
		{
			Type t1 = Type.GetType(typeof(TestType1).FullName);
			Assert.IsNotNull(t1);
			Assert.AreEqual("TestType1", t1.Name);
			Type t2 = Type.GetType("NotExistingType");
			Assert.IsNull(t2);
		}

		public void InvokeMember()
		{
			// InvokeMember is not implemented
			try
			{
				object i = typeof(TestType1).GetConstructor(new Type[] { }).Invoke(new object[] { });
				object r = typeof(TestType1).InvokeMember("PublicMethod2", BindingFlags.InvokeMethod, null, i, new object[] { 42 });
				/*Assert.IsNotNull(r);
				Assert.IsTrue((int)r == 42 * 2);*/
				Assert.Fail();
			}
			catch (NotImplementedException) { }
		}

		public void IsInstanceOfType()
		{
			object i1 = typeof(TestType4).GetConstructor(new Type[] { }).Invoke(new object[] { });
			Assert.IsTrue(typeof(object).IsInstanceOfType(i1));
			Assert.IsTrue(typeof(ITestType2).IsInstanceOfType(i1));
			Assert.IsFalse(typeof(ITestType3).IsInstanceOfType(i1));
			object i2 = typeof(TestType7).GetConstructor(new Type[] { }).Invoke(new object[] { });
			Assert.IsTrue(typeof(object).IsInstanceOfType(i2));
			Assert.IsFalse(typeof(TestType1).IsInstanceOfType(i2));
			Assert.IsTrue(typeof(ITestType2).IsInstanceOfType(i2));
			Assert.IsTrue(typeof(ITestType3).IsInstanceOfType(i2));
			Assert.IsTrue(typeof(ITestType3).IsInstanceOfType(i2));
			Assert.IsTrue(typeof(TestType4).IsInstanceOfType(i2));
			Assert.IsFalse(typeof(TestType5).IsInstanceOfType(i2));
			Assert.IsTrue(typeof(TestType6).IsInstanceOfType(i2));
		}

		public void IsSubclassOf()
		{
			Assert.IsTrue(typeof(TestType4).IsSubclassOf(typeof(object)));
			Assert.IsTrue(typeof(TestType7).IsSubclassOf(typeof(object)));
			Assert.IsFalse(typeof(TestType7).IsSubclassOf(typeof(TestType1)));
			Assert.IsTrue(typeof(TestType7).IsSubclassOf(typeof(TestType4)));
			Assert.IsFalse(typeof(TestType7).IsSubclassOf(typeof(TestType5)));
			Assert.IsTrue(typeof(TestType7).IsSubclassOf(typeof(TestType6)));
		}

		public void ToString1()
		{
			Assert.AreEqual(typeof(TestType1).FullName, typeof(TestType1).ToString());
		}

		public void Assembly1()
		{
			Assert.IsTrue(typeof(TestType1).Assembly == Assembly.GetExecutingAssembly());
		}

		public void AssemblyQualifiedName()
		{
			string n = typeof(TestType1).AssemblyQualifiedName;
			Assert.IsNotNull(n);
			Assert.IsTrue(n.IndexOf(typeof(TestType1).Name) >= 0);
		}

		public void BaseType()
		{
			Type b = typeof(TestType7).BaseType;
			Assert.IsNotNull(b);
			Assert.AreEqual(typeof(TestType6).FullName, b.FullName);
		}

		public void DeclaringType()
		{
			Type d = typeof(TestType7.TestType8).DeclaringType;
			Assert.IsNotNull(d);
			Assert.AreEqual(typeof(TestType7).FullName, d.FullName);
		}

		public void FullName()
		{
			Assert.AreEqual("nanoFramework.CoreLibrary.Tests.TestType1", typeof(TestType1).FullName);
		}

		public void IsAbstract()
		{
			Assert.IsTrue(typeof(TestType9).IsAbstract);
			Assert.IsFalse(typeof(TestType1).IsAbstract);
		}

		public void IsArray()
		{
			Assert.IsTrue(typeof(TestType1[]).IsArray);
			Assert.IsFalse(typeof(TestType1).IsArray);
		}

		public void IsClass()
		{
			Assert.IsTrue(typeof(TestType1).IsClass);
			Assert.IsFalse(typeof(ITestType2).IsClass);
		}

		public void IsEnum()
		{
			Assert.IsTrue(typeof(TestType10).IsEnum);
			Assert.IsFalse(typeof(TestType1).IsEnum);
		}

		public void IsInterface()
		{
			Assert.IsTrue(typeof(ITestType2).IsInterface);
			Assert.IsFalse(typeof(TestType1).IsInterface);
		}

		public void IsNotPublic()
		{
			Assert.IsTrue(typeof(TestType11).IsNotPublic);
			Assert.IsFalse(typeof(TestType1).IsNotPublic);
		}

		public void IsPublic()
		{
			Assert.IsTrue(typeof(TestType1).IsPublic);
			Assert.IsFalse(typeof(TestType11).IsPublic);
		}

		public void IsValueType()
		{
			Assert.IsTrue(typeof(TestType12).IsValueType);
			Assert.IsFalse(typeof(TestType1).IsValueType);
		}
	}
}