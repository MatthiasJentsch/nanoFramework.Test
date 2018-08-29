//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	public class GuidTests : ITestClass
	{
		public void Constructor1()
		{
			byte[] b = new byte[] { 29, 244, 133, 99, 41, 224, 228, 66, 145, 164, 109, 255, 161, 165, 48, 13 };
			Guid g = new Guid(b);
			Assert.AreEqual("6385f41d-e029-42e4-91a4-6dffa1a5300d", g.ToString());
		}

		public void Constructor2()
		{
			Guid g = new Guid((int)1669723165, (short)-8151, (short)17124, 145, 164, 109, 255, 161, 165, 48, 13);
			Assert.AreEqual("6385f41d-e029-42e4-91a4-6dffa1a5300d", g.ToString());
		}

		public void Constructor3()
		{
			Guid g = new Guid((uint)1669723165, (ushort)57385, (ushort)17124, 145, 164, 109, 255, 161, 165, 48, 13);
			Assert.AreEqual("6385f41d-e029-42e4-91a4-6dffa1a5300d", g.ToString());
		}

		public void CompareTo()
		{
			byte[] b1 = new byte[] { 29, 244, 133, 99, 41, 224, 228, 66, 145, 164, 109, 255, 161, 165, 48, 13 };
			Guid g1 = new Guid(b1);
			byte[] b2 = new byte[] { 29, 244, 133, 99, 41, 224, 228, 66, 145, 164, 109, 255, 161, 165, 48, 14 };
			Guid g2 = new Guid(b2);
			byte[] b3 = new byte[] { 29, 244, 133, 99, 41, 224, 228, 66, 145, 164, 109, 255, 161, 165, 48, 13 };
			Guid g3 = new Guid(b3);
			Assert.IsTrue(g1.CompareTo(g2) < 0);
			Assert.IsTrue(g2.CompareTo(g3) > 0);
			Assert.IsTrue(g1.CompareTo(g3) == 0);
		}

		public void Equals1()
		{
			byte[] b1 = new byte[] { 29, 244, 133, 99, 41, 224, 228, 66, 145, 164, 109, 255, 161, 165, 48, 13 };
			Guid g1 = new Guid(b1);
			byte[] b2 = new byte[] { 29, 244, 133, 99, 41, 224, 228, 66, 145, 164, 109, 255, 161, 165, 48, 14 };
			Guid g2 = new Guid(b2);
			byte[] b3 = new byte[] { 29, 244, 133, 99, 41, 224, 228, 66, 145, 164, 109, 255, 161, 165, 48, 13 };
			Guid g3 = new Guid(b3);
			Assert.IsFalse(g1.Equals(g2));
			Assert.IsFalse(g2.Equals(g3));
			Assert.IsTrue(g1.Equals(g3));
		}

		public void GetHashCode1()
		{
			byte[] b1 = new byte[] { 29, 244, 133, 99, 41, 224, 228, 66, 145, 164, 109, 255, 161, 165, 48, 13 };
			Guid g1 = new Guid(b1);
			int h1 = g1.GetHashCode();
			byte[] b2 = new byte[] { 29, 244, 133, 99, 41, 224, 228, 66, 145, 164, 109, 255, 161, 165, 48, 14 };
			Guid g2 = new Guid(b2);
			int h2 = g2.GetHashCode();
			byte[] b3 = new byte[] { 29, 244, 133, 99, 41, 224, 228, 66, 145, 164, 109, 255, 161, 165, 48, 13 };
			Guid g3 = new Guid(b3);
			int h3 = g3.GetHashCode();
			Assert.AreNotEqual(h1, h2);
			Assert.AreNotEqual(h2, h3);
			Assert.AreEqual(h1, h3);
		}

		public void NewGuid()
		{
			Guid g1 = Guid.NewGuid();
			Guid g2 = Guid.NewGuid();
			Assert.AreNotEqual(g1.GetHashCode(), g2.GetHashCode());
		}

		public void ToByteArray()
		{
			byte[] r = new Guid((int)1669723165, (short)-8151, (short)17124, 145, 164, 109, 255, 161, 165, 48, 13).ToByteArray();
			Assert.AreEqual(16, r.Length);
			Assert.AreEqual(29, r[0]);
			Assert.AreEqual(244, r[1]);
			Assert.AreEqual(133, r[2]);
			Assert.AreEqual(99, r[3]);
			Assert.AreEqual(41, r[4]);
			Assert.AreEqual(224, r[5]);
			Assert.AreEqual(228, r[6]);
			Assert.AreEqual(66, r[7]);
			Assert.AreEqual(145, r[8]);
			Assert.AreEqual(164, r[9]);
			Assert.AreEqual(109, r[10]);
			Assert.AreEqual(255, r[11]);
			Assert.AreEqual(161, r[12]);
			Assert.AreEqual(165, r[13]);
			Assert.AreEqual(48, r[14]);
			Assert.AreEqual(13, r[15]);
		}

		public void ToString1()
		{
			Guid g = new Guid((uint)1669723165, (ushort)57385, (ushort)17124, 145, 164, 109, 255, 161, 165, 48, 13);
			Assert.AreEqual("6385f41d-e029-42e4-91a4-6dffa1a5300d", g.ToString());
		}
	}
}
