//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;
using System.Threading;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	public class DateTimeTests : ITestClass
	{
		public void Constructor1()
		{
			DateTime dt = new DateTime(47110815);
			Assert.AreEqual(47110815, dt.Ticks);
		}

		public void Constructor2()
		{
			DateTime dt = new DateTime(47110815, DateTimeKind.Utc);
			Assert.AreEqual(47110815, dt.Ticks);
		}

		public void Constructor3()
		{
			DateTime dt = new DateTime(2010, 1, 19);
			Assert.AreEqual(2010, dt.Year);
			Assert.AreEqual(1, dt.Month);
			Assert.AreEqual(19, dt.Day);
		}

		public void Constructor4()
		{
			DateTime dt = new DateTime(2010, 1, 19, 14, 35, 21);
			Assert.AreEqual(2010, dt.Year);
			Assert.AreEqual(1, dt.Month);
			Assert.AreEqual(19, dt.Day);
			Assert.AreEqual(14, dt.Hour);
			Assert.AreEqual(35, dt.Minute);
			Assert.AreEqual(21, dt.Second);
		}

		public void Constructor5()
		{
			DateTime dt = new DateTime(2010, 1, 19, 14, 35, 21, 815);
			Assert.AreEqual(2010, dt.Year);
			Assert.AreEqual(1, dt.Month);
			Assert.AreEqual(19, dt.Day);
			Assert.AreEqual(14, dt.Hour);
			Assert.AreEqual(35, dt.Minute);
			Assert.AreEqual(21, dt.Second);
			Assert.AreEqual(815, dt.Millisecond);
		}

		public void Add()
		{
			DateTime dt = new DateTime(2010, 1, 19);
			dt = dt.Add(new TimeSpan(3, 4, 5, 6));
			Assert.AreEqual(2010, dt.Year);
			Assert.AreEqual(1, dt.Month);
			Assert.AreEqual(22, dt.Day);
			Assert.AreEqual(4, dt.Hour);
			Assert.AreEqual(5, dt.Minute);
			Assert.AreEqual(6, dt.Second);
			Assert.AreEqual(0, dt.Millisecond);
		}

		public void AddDays()
		{
			DateTime dt = new DateTime(2010, 1, 28);
			dt = dt.AddDays(7);
			Assert.AreEqual(2010, dt.Year);
			Assert.AreEqual(2, dt.Month);
			Assert.AreEqual(4, dt.Day);
		}

		public void AddHours()
		{
			DateTime dt = new DateTime(2010, 1, 28, 22, 0, 0);
			dt = dt.AddHours(7);
			Assert.AreEqual(2010, dt.Year);
			Assert.AreEqual(1, dt.Month);
			Assert.AreEqual(29, dt.Day);
			Assert.AreEqual(5, dt.Hour);
			Assert.AreEqual(0, dt.Minute);
			Assert.AreEqual(0, dt.Second);
			Assert.AreEqual(0, dt.Millisecond);
		}

		public void AddMilliseconds()
		{
			DateTime dt = new DateTime(2010, 1, 28, 22, 0, 0);
			dt = dt.AddMilliseconds(4711);
			Assert.AreEqual(2010, dt.Year);
			Assert.AreEqual(1, dt.Month);
			Assert.AreEqual(28, dt.Day);
			Assert.AreEqual(22, dt.Hour);
			Assert.AreEqual(0, dt.Minute);
			Assert.AreEqual(4, dt.Second);
			Assert.AreEqual(711, dt.Millisecond);
		}

		public void AddMinutes()
		{
			DateTime dt = new DateTime(2010, 1, 28, 22, 54, 0);
			dt = dt.AddMinutes(13);
			Assert.AreEqual(2010, dt.Year);
			Assert.AreEqual(1, dt.Month);
			Assert.AreEqual(28, dt.Day);
			Assert.AreEqual(23, dt.Hour);
			Assert.AreEqual(7, dt.Minute);
			Assert.AreEqual(0, dt.Second);
			Assert.AreEqual(0, dt.Millisecond);
		}

		public void AddSeconds()
		{
			DateTime dt = new DateTime(2010, 1, 28, 22, 54, 45);
			dt = dt.AddSeconds(42);
			Assert.AreEqual(2010, dt.Year);
			Assert.AreEqual(1, dt.Month);
			Assert.AreEqual(28, dt.Day);
			Assert.AreEqual(22, dt.Hour);
			Assert.AreEqual(55, dt.Minute);
			Assert.AreEqual(27, dt.Second);
			Assert.AreEqual(0, dt.Millisecond);
		}

		public void AddTicks()
		{
			DateTime dt = new DateTime(47110815);
			dt = dt.AddTicks(42);
			Assert.AreEqual(47110857, dt.Ticks);
		}

		public void Compare()
		{
			DateTime dt1 = new DateTime(2010, 1, 28);
			DateTime dt2 = new DateTime(2010, 1, 19);
			DateTime dt3 = new DateTime(2010, 1, 28);
			Assert.IsTrue(DateTime.Compare(dt1, dt2) > 0);
			Assert.IsTrue(DateTime.Compare(dt2, dt3) < 0);
			Assert.IsTrue(DateTime.Compare(dt1, dt3) == 0);
		}

		public void CompareTo()
		{
			DateTime dt1 = new DateTime(2010, 1, 28);
			DateTime dt2 = new DateTime(2010, 1, 19);
			DateTime dt3 = new DateTime(2010, 1, 28);
			Assert.IsTrue(dt1.CompareTo(dt2) > 0);
			Assert.IsTrue(dt2.CompareTo(dt3) < 0);
			Assert.IsTrue(dt1.CompareTo(dt3) == 0);
		}

		public void DaysInMonth()
		{
			Assert.AreEqual(28, DateTime.DaysInMonth(1900, 2));
			Assert.AreEqual(29, DateTime.DaysInMonth(2000, 2));
			Assert.AreEqual(29, DateTime.DaysInMonth(2016, 2));
			Assert.AreEqual(28, DateTime.DaysInMonth(2018, 2));
			Assert.AreEqual(31, DateTime.DaysInMonth(2018, 12));
		}

		public void Equals1()
		{
			DateTime dt1 = new DateTime(2010, 1, 28);
			DateTime dt2 = new DateTime(2010, 1, 19);
			DateTime dt3 = new DateTime(2010, 1, 28);
			Assert.IsFalse(dt1.Equals(dt2));
			Assert.IsTrue(dt1.Equals(dt3));
		}

		public void Equals2()
		{
			DateTime dt1 = new DateTime(2010, 1, 28);
			DateTime dt2 = new DateTime(2010, 1, 19);
			DateTime dt3 = new DateTime(2010, 1, 28);
			Assert.IsFalse(DateTime.Equals(dt1, dt2));
			Assert.IsTrue(DateTime.Equals(dt1, dt3));
		}

		public void Subtract1()
		{
			DateTime dt1 = new DateTime(2010, 1, 28);
			DateTime dt2 = new DateTime(2010, 1, 19);
			TimeSpan ts = dt1.Subtract(dt2);
			Assert.AreEqual(9, ts.Days);
		}

		public void Subtract2()
		{
			DateTime dt1 = new DateTime(2010, 1, 28);
			DateTime dt2 = dt1.Subtract(new TimeSpan(9, 0, 0, 0));
			Assert.AreEqual(19, dt2.Day);
		}

		public void ToString1()
		{
			string dt = new DateTime(2010, 1, 19).ToString();
			Assert.IsNotNull(dt);
			Assert.IsTrue(dt.IndexOf("2010") >= 0);
		}

		public void ToString2()
		{
			string dt = new DateTime(2010, 1, 19).ToString("yyyyMMdd");
			Assert.AreEqual("20100119", dt);
		}

		public void Date()
		{
			DateTime dt1 = new DateTime(2010, 1, 28, 22, 54, 45);
			DateTime dt2 = dt1.Date;
			Assert.AreEqual(2010, dt2.Year);
			Assert.AreEqual(1, dt2.Month);
			Assert.AreEqual(28, dt2.Day);
			Assert.AreEqual(0, dt2.Hour);
			Assert.AreEqual(0, dt2.Minute);
			Assert.AreEqual(0, dt2.Second);
			Assert.AreEqual(0, dt2.Millisecond);
		}

		public void Day()
		{
			DateTime dt = new DateTime(2010, 1, 28, 22, 54, 45);
			Assert.AreEqual(28, dt.Day);
		}

		public void DayOfWeek1()
		{
			DateTime dt = new DateTime(2018, 6, 28, 22, 54, 45);
			Assert.AreEqual(DayOfWeek.Thursday, dt.DayOfWeek);
		}

		public void DayOfYear()
		{
			DateTime dt = new DateTime(2018, 6, 28, 22, 54, 45);
			Assert.AreEqual(179, dt.DayOfYear);
		}

		public void Hour()
		{
			DateTime dt = new DateTime(2010, 1, 28, 22, 54, 45);
			Assert.AreEqual(22, dt.Hour);
		}

		public void Kind()
		{
			DateTime dt = new DateTime(47110815, DateTimeKind.Utc);
			Assert.AreEqual(DateTimeKind.Utc, dt.Kind);
		}

		public void Millisecond()
		{
			DateTime dt = new DateTime(2010, 1, 28, 22, 54, 45);
			Assert.AreEqual(0, dt.Millisecond);
		}

		public void Minute()
		{
			DateTime dt = new DateTime(2010, 1, 28, 22, 54, 45);
			Assert.AreEqual(54, dt.Minute);
		}

		public void Month()
		{
			DateTime dt = new DateTime(2010, 1, 28, 22, 54, 45);
			Assert.AreEqual(1, dt.Month);
		}

		public void Second()
		{
			DateTime dt = new DateTime(2010, 1, 28, 22, 54, 45);
			Assert.AreEqual(45, dt.Second);
		}

		public void Ticks()
		{
			DateTime dt = new DateTime(47110815);
			Assert.AreEqual(47110815, dt.Ticks);
		}

		public void TimeOfDay()
		{
			DateTime dt = new DateTime(2010, 1, 28, 22, 54, 45);
			TimeSpan ts = dt.TimeOfDay;
			Assert.AreEqual(22, ts.Hours);
			Assert.AreEqual(54, ts.Minutes);
			Assert.AreEqual(45, ts.Seconds);
			Assert.AreEqual(0, ts.Milliseconds);
		}

		public void Today()
		{
			DateTime dt = DateTime.Today;
			Assert.AreEqual(0, dt.Hour);
			Assert.AreEqual(0, dt.Minute);
			Assert.AreEqual(0, dt.Second);
			Assert.AreEqual(0, dt.Millisecond);
		}

		public void UtcNow()
		{
			DateTime dt1 = DateTime.UtcNow;
			Thread.Sleep(1000);
			DateTime dt2 = DateTime.UtcNow;
			Assert.IsTrue(dt2 > dt1);
		}

		public void Year()
		{
			DateTime dt = new DateTime(2010, 1, 28, 22, 54, 45);
			Assert.AreEqual(2010, dt.Year);
		}

		public void OperatorPlus()
		{
			DateTime dt = new DateTime(2010, 1, 19);
			dt = dt + new TimeSpan(3, 4, 5, 6);
			Assert.AreEqual(2010, dt.Year);
			Assert.AreEqual(1, dt.Month);
			Assert.AreEqual(22, dt.Day);
			Assert.AreEqual(4, dt.Hour);
			Assert.AreEqual(5, dt.Minute);
			Assert.AreEqual(6, dt.Second);
			Assert.AreEqual(0, dt.Millisecond);
		}

		public void OperatorEqual()
		{
			DateTime dt1 = new DateTime(2010, 1, 28);
			DateTime dt2 = new DateTime(2010, 1, 19);
			DateTime dt3 = new DateTime(2010, 1, 28);
			Assert.IsFalse(dt1 == dt2);
			Assert.IsTrue(dt1 == dt3);
		}

		public void OperatorGreater()
		{
			DateTime dt1 = new DateTime(2010, 1, 28);
			DateTime dt2 = new DateTime(2010, 1, 19);
			DateTime dt3 = new DateTime(2010, 1, 28);
			Assert.IsTrue(dt1 > dt2);
			Assert.IsFalse(dt2 > dt3);
		}

		public void OperatorGreaterOrEqual()
		{
			DateTime dt1 = new DateTime(2010, 1, 28);
			DateTime dt2 = new DateTime(2010, 1, 19);
			DateTime dt3 = new DateTime(2010, 1, 28);
			Assert.IsTrue(dt1 >= dt2);
			Assert.IsTrue(dt1 >= dt3);
		}

		public void OperatorNotEqual()
		{
			DateTime dt1 = new DateTime(2010, 1, 28);
			DateTime dt2 = new DateTime(2010, 1, 19);
			DateTime dt3 = new DateTime(2010, 1, 28);
			Assert.IsTrue(dt1 != dt2);
			Assert.IsFalse(dt1 != dt3);
		}

		public void OperatorLesser()
		{
			DateTime dt1 = new DateTime(2010, 1, 28);
			DateTime dt2 = new DateTime(2010, 1, 19);
			DateTime dt3 = new DateTime(2010, 1, 28);
			Assert.IsTrue(dt2 < dt1);
			Assert.IsFalse(dt3 < dt2);
		}

		public void OperatorLesserOrEqual()
		{
			DateTime dt1 = new DateTime(2010, 1, 28);
			DateTime dt2 = new DateTime(2010, 1, 19);
			DateTime dt3 = new DateTime(2010, 1, 28);
			Assert.IsTrue(dt2 <= dt1);
			Assert.IsTrue(dt3 <= dt1);
		}

		public void OperatorMinus1()
		{
			DateTime dt1 = new DateTime(2010, 1, 28);
			DateTime dt2 = new DateTime(2010, 1, 19);
			TimeSpan ts = dt1 - dt2;
			Assert.AreEqual(9, ts.Days);
		}

		public void OperatorMinus2()
		{
			DateTime dt1 = new DateTime(2010, 1, 28);
			DateTime dt2 = dt1 - new TimeSpan(9, 0, 0, 0);
			Assert.AreEqual(19, dt2.Day);
		}
	}
}
