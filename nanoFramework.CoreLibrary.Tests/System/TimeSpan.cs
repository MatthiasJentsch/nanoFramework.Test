//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	public class TimeSpanTests : ITestClass
	{
		public void Constructor1()
		{
			TimeSpan ts = new TimeSpan(47110815);
			Assert.AreEqual(47110815, ts.Ticks);
		}

		public void Constructor2()
		{
			TimeSpan ts = new TimeSpan(14, 35, 21);
			Assert.AreEqual(0, ts.Days);
			Assert.AreEqual(14, ts.Hours);
			Assert.AreEqual(35, ts.Minutes);
			Assert.AreEqual(21, ts.Seconds);
			Assert.AreEqual(0, ts.Milliseconds);
		}

		public void Constructor3()
		{
			TimeSpan ts = new TimeSpan(5, 14, 35, 21);
			Assert.AreEqual(5, ts.Days);
			Assert.AreEqual(14, ts.Hours);
			Assert.AreEqual(35, ts.Minutes);
			Assert.AreEqual(21, ts.Seconds);
			Assert.AreEqual(0, ts.Milliseconds);
		}

		public void Constructor4()
		{
			TimeSpan ts = new TimeSpan(5, 14, 35, 21, 815);
			Assert.AreEqual(5, ts.Days);
			Assert.AreEqual(14, ts.Hours);
			Assert.AreEqual(35, ts.Minutes);
			Assert.AreEqual(21, ts.Seconds);
			Assert.AreEqual(815, ts.Milliseconds);
		}

		public void Add()
		{
			TimeSpan ts = new TimeSpan(14, 35, 21);
			ts = ts.Add(new TimeSpan(11, 30, 40));
			Assert.AreEqual(1, ts.Days);
			Assert.AreEqual(2, ts.Hours);
			Assert.AreEqual(6, ts.Minutes);
			Assert.AreEqual(1, ts.Seconds);
			Assert.AreEqual(0, ts.Milliseconds);
		}

		public void Compare()
		{
			TimeSpan ts1 = new TimeSpan(14, 35, 21);
			TimeSpan ts2 = new TimeSpan(14, 35, 15);
			TimeSpan ts3 = new TimeSpan(14, 35, 21);
			Assert.IsTrue(TimeSpan.Compare(ts1, ts2) > 0);
			Assert.IsTrue(TimeSpan.Compare(ts2, ts3) < 0);
			Assert.IsTrue(TimeSpan.Compare(ts1, ts3) == 0);
		}

		public void CompareTo()
		{
			TimeSpan ts1 = new TimeSpan(14, 35, 21);
			TimeSpan ts2 = new TimeSpan(14, 35, 15);
			TimeSpan ts3 = new TimeSpan(14, 35, 21);
			Assert.IsTrue(ts1.CompareTo(ts2) > 0);
			Assert.IsTrue(ts2.CompareTo(ts3) < 0);
			Assert.IsTrue(ts1.CompareTo(ts3) == 0);
		}

		public void Duration()
		{
			TimeSpan ts1 = new TimeSpan(47110815);
			TimeSpan ts2 = new TimeSpan(-47110815);
			Assert.AreEqual(47110815, ts1.Duration().Ticks);
			Assert.AreEqual(47110815, ts2.Duration().Ticks);
		}

		public void Equals1()
		{
			TimeSpan ts1 = new TimeSpan(14, 35, 21);
			TimeSpan ts2 = new TimeSpan(14, 35, 15);
			TimeSpan ts3 = new TimeSpan(14, 35, 21);
			Assert.IsFalse(ts1.Equals(ts2));
			Assert.IsTrue(ts1.Equals(ts3));
		}

		public void Equals2()
		{
			TimeSpan ts1 = new TimeSpan(14, 35, 21);
			TimeSpan ts2 = new TimeSpan(14, 35, 15);
			TimeSpan ts3 = new TimeSpan(14, 35, 21);
			Assert.IsFalse(TimeSpan.Equals(ts1, ts2));
			Assert.IsTrue(TimeSpan.Equals(ts1, ts3));
		}

		public void FromTicks()
		{
			TimeSpan ts = TimeSpan.FromTicks(47110815);
			Assert.AreEqual(47110815, ts.Ticks);
		}

		public void Negate()
		{
			TimeSpan ts1 = new TimeSpan(47110815);
			TimeSpan ts2 = new TimeSpan(-47110815);
			Assert.AreEqual(-47110815, ts1.Negate().Ticks);
			Assert.AreEqual(47110815, ts2.Negate().Ticks);
		}

		public void Subtract()
		{
			TimeSpan ts = new TimeSpan(14, 35, 21);
			ts = ts.Subtract(new TimeSpan(11, 30, 40));
			Assert.AreEqual(0, ts.Days);
			Assert.AreEqual(3, ts.Hours);
			Assert.AreEqual(4, ts.Minutes);
			Assert.AreEqual(41, ts.Seconds);
			Assert.AreEqual(0, ts.Milliseconds);
		}

		public void ToString1()
		{
			TimeSpan ts = new TimeSpan(5, 14, 35, 21, 815);
			Assert.AreEqual("5.14:35:21.8150000", ts.ToString());
		}
	}
}
