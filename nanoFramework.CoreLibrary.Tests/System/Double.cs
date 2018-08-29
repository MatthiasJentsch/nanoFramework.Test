//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	public class DoubleTests : ITestClass
	{
		public void CompareTo()
		{
			double d1 = 4711E+81;
			double d2 = -815E-47;
			double d3 = 4711E+81;
			// TODO!
			/*Assert.IsTrue(double.CompareTo(d1, d2) > 0);
			Assert.IsTrue(double.CompareTo(d2, d3) < 0);
			Assert.IsTrue(double.CompareTo(d1, d3) == 0);*/
		}

		public void IsInfinity()
		{
			Assert.IsTrue(double.IsInfinity(double.PositiveInfinity));
			Assert.IsTrue(double.IsInfinity(double.NegativeInfinity));
			Assert.IsFalse(double.IsInfinity(double.NaN));
			Assert.IsFalse(double.IsInfinity(double.Epsilon));
		}

		public void IsNaN()
		{
			Assert.IsFalse(double.IsNaN(double.PositiveInfinity));
			Assert.IsFalse(double.IsNaN(double.NegativeInfinity));
			Assert.IsTrue(double.IsNaN(double.NaN));
			Assert.IsFalse(double.IsNaN(double.Epsilon));
		}

		public void IsNegativeInfinity()
		{
			Assert.IsFalse(double.IsNegativeInfinity(double.PositiveInfinity));
			Assert.IsTrue(double.IsNegativeInfinity(double.NegativeInfinity));
			Assert.IsFalse(double.IsNegativeInfinity(double.NaN));
			Assert.IsFalse(double.IsNegativeInfinity(double.Epsilon));
		}

		public void IsPositiveInfinity()
		{
			Assert.IsTrue(double.IsPositiveInfinity(double.PositiveInfinity));
			Assert.IsFalse(double.IsPositiveInfinity(double.NegativeInfinity));
			Assert.IsFalse(double.IsPositiveInfinity(double.NaN));
			Assert.IsFalse(double.IsPositiveInfinity(double.Epsilon));
		}

		public void Parse()
		{
			Assert.AreEqual(4711E+81, Convert.ToDouble("4711E+81"));
			Assert.AreEqual(-815E-47, Convert.ToDouble("-815E-47"));
		}

		public void ToString1()
		{
			Assert.AreEqual("4E+81", 4E+81.ToString());
			Assert.AreEqual("-8E-47", (-8E-47).ToString());
		}

		public void ToString2()
		{
			Assert.AreEqual("30000000", 3E7.ToString("F0"));
		}

		public void TryParse()
		{
			double d;
			Assert.IsTrue(double.TryParse("4711E+81", out d));
			Assert.IsFalse(double.TryParse("4711X+81", out d));
		}
	}
}
