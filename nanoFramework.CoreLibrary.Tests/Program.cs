//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;
using System.Collections;
using System.Reflection;

namespace nanoFramework.CoreLibrary.Tests
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				TestManager.RunTests(Assembly.GetExecutingAssembly());
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
			}
		}
	}

	public class TestType1
	{
		private int PrivateField = -1;
		public int PublicField = -1;
		public int PublicProperty { get { return PublicField; } set { PublicField = value; } }
		public TestType1() { }
		public TestType1(string parameter1) { }
		public TestType1(string parameter1, int parameter2) { }
		public void PublicMethod1() { }
		public void PublicMethod2() { }
		public int PublicMethod2(int parameter1) { return parameter1 * 2; }
		private void PrivateMethod1() { }
		private void PrivateMethod2() { }
		private void PrivateMethod2(int parameter1) { }
	}
	public interface ITestType2 { }
	public interface ITestType3 { }
	public class TestType4 : ITestType2
	{
		public int PublicField = -1;
		public int PublicMethod() { return 42; }
	}
	public class TestType5 : ITestType2, ITestType3 { }
	public class TestType6 : TestType4, ITestType3 { }
	public class TestType7 : TestType6
	{
		public class TestType8 { }
	}
	public abstract class TestType9 { }
	public enum TestType10 { }
	internal class TestType11 { }
	public struct TestType12 { }

	public class CharComparer : IComparer
	{
		public int Compare(object x, object y)
		{
			char cx = (char)x;
			char cy = (char)y;
			return cx < cy ? -1 : cx > cy ? 1 : 0;
		}
	}
}
