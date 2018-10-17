//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	[TestClass]
	public class BitConverterTests
	{
		[TestMethod]
		public void DoubleToInt64Bits()
		{
			double d1 = 3.1415;
			Assert.AreEqual(4614256447914709615, BitConverter.DoubleToInt64Bits(d1));
			double d2 = -815.4711;
			Assert.AreEqual(-4572987861383272150, BitConverter.DoubleToInt64Bits(d2));
		}

		[TestMethod]
		public void GetBytesShort()
		{
			short x = -815;
			byte[] b = BitConverter.GetBytes(x);
			Assert.AreEqual(2, b.Length);
			Assert.AreEqual(209, b[0]);
			Assert.AreEqual(252, b[1]);
		}

		[TestMethod]
		public void GetBytesLong()
		{
			long x = -594775110748111512;
			byte[] b = BitConverter.GetBytes(x);
			Assert.AreEqual(8, b.Length);
			Assert.AreEqual(104, b[0]);
			Assert.AreEqual(125, b[1]);
			Assert.AreEqual(157, b[2]);
			Assert.AreEqual(34, b[3]);
			Assert.AreEqual(48, b[4]);
			Assert.AreEqual(239, b[5]);
			Assert.AreEqual(190, b[6]);
			Assert.AreEqual(247, b[7]);
		}

		[TestMethod]
		public void GetBytesUint()
		{
			uint x = 3248111512;
			byte[] b = BitConverter.GetBytes(x);
			Assert.AreEqual(4, b.Length);
			Assert.AreEqual(152, b[0]);
			Assert.AreEqual(63, b[1]);
			Assert.AreEqual(154, b[2]);
			Assert.AreEqual(193, b[3]);
		}

		[TestMethod]
		public void GetBytesUshort()
		{
			ushort x = 64521;
			byte[] b = BitConverter.GetBytes(x);
			Assert.AreEqual(2, b.Length);
			Assert.AreEqual(9, b[0]);
			Assert.AreEqual(252, b[1]);
		}

		[TestMethod]
		public void GetBytesUlong()
		{
			ulong x = 18094775110748111512;
			byte[] b = BitConverter.GetBytes(x);
			Assert.AreEqual(8, b.Length);
			Assert.AreEqual(152, b[0]);
			Assert.AreEqual(130, b[1]);
			Assert.AreEqual(184, b[2]);
			Assert.AreEqual(206, b[3]);
			Assert.AreEqual(23, b[4]);
			Assert.AreEqual(142, b[5]);
			Assert.AreEqual(29, b[6]);
			Assert.AreEqual(251, b[7]);
		}

		[TestMethod]
		public void GetBytesChar()
		{
			char x1 = 'A';
			byte[] b1 = BitConverter.GetBytes(x1);
			Assert.AreEqual(2, b1.Length);
			Assert.AreEqual(65, b1[0]);
			Assert.AreEqual(0, b1[1]);
			char x2 = '€';
			byte[] b2 = BitConverter.GetBytes(x2);
			Assert.AreEqual(2, b2.Length);
			Assert.AreEqual(172, b2[0]);
			Assert.AreEqual(32, b2[1]);
		}

		[TestMethod]
		public void GetBytesBool()
		{
			bool x1 = true;
			byte[] b1 = BitConverter.GetBytes(x1);
			Assert.AreEqual(1, b1.Length);
			Assert.AreEqual(1, b1[0]);
			bool x2 = false;
			byte[] b2 = BitConverter.GetBytes(x2);
			Assert.AreEqual(1, b2.Length);
			Assert.AreEqual(0, b2[0]);
		}

		[TestMethod]
		public void GetBytesDouble()
		{
			double d1 = 123456789.987654321;
			byte[] b1 = BitConverter.GetBytes(d1);
			Assert.AreEqual(8, b1.Length);
			Assert.AreEqual(168, b1[0]);
			Assert.AreEqual(91, b1[1]);
			Assert.AreEqual(243, b1[2]);
			Assert.AreEqual(87, b1[3]);
			Assert.AreEqual(52, b1[4]);
			Assert.AreEqual(111, b1[5]);
			Assert.AreEqual(157, b1[6]);
			Assert.AreEqual(65, b1[7]);
			double d2 = -987654321.123456789;
			byte[] b2 = BitConverter.GetBytes(d2);
			Assert.AreEqual(8, b2.Length);
			Assert.AreEqual(111, b2[0]);
			Assert.AreEqual(205, b2[1]);
			Assert.AreEqual(143, b2[2]);
			Assert.AreEqual(88, b2[3]);
			Assert.AreEqual(52, b2[4]);
			Assert.AreEqual(111, b2[5]);
			Assert.AreEqual(205, b2[6]);
			Assert.AreEqual(193, b2[7]);
		}

		[TestMethod]
		public void GetBytesInt()
		{
			int x = -1748111512;
			byte[] b = BitConverter.GetBytes(x);
			Assert.AreEqual(4, b.Length);
			Assert.AreEqual(104, b[0]);
			Assert.AreEqual(239, b[1]);
			Assert.AreEqual(205, b[2]);
			Assert.AreEqual(151, b[3]);
		}

		[TestMethod]
		public void GetBytesFloat()
		{
			float f1 = 123456789.987654321F;
			byte[] b1 = BitConverter.GetBytes(f1);
			Assert.AreEqual(4, b1.Length);
			Assert.AreEqual(163, b1[0]);
			Assert.AreEqual(121, b1[1]);
			Assert.AreEqual(235, b1[2]);
			Assert.AreEqual(76, b1[3]);
			float f2 = -987654321.123456789F;
			byte[] b2 = BitConverter.GetBytes(f2);
			Assert.AreEqual(4, b2.Length);
			Assert.AreEqual(163, b2[0]);
			Assert.AreEqual(121, b2[1]);
			Assert.AreEqual(107, b2[2]);
			Assert.AreEqual(206, b2[3]);
		}

		[TestMethod]
		public void Int64BitsToDouble()
		{
			long x1 = -594775110748111512;
			Assert.AreEqual(-6.3837642308436171E+268, BitConverter.Int64BitsToDouble(x1));
			long x2 = 594775110748111512;
			Assert.AreEqual(6.4606656927577285E-269, BitConverter.Int64BitsToDouble(x2));
		}

		[TestMethod]
		public void ToBoolean()
		{
			byte[] x = new byte[] { 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 14, 73, 64 };
			Assert.IsFalse(BitConverter.ToBoolean(x, 8));
			Assert.IsTrue(BitConverter.ToBoolean(x, 9));
		}

		[TestMethod]
		public void ToChar()
		{
			byte[] x = new byte[] { 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 14, 73, 64 };
			Assert.AreEqual('€', BitConverter.ToChar(x, 10));
		}

		[TestMethod]
		public void ToDouble()
		{
			byte[] x = new byte[] { 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 14, 73, 64 };
			Assert.AreEqual(-9.95029283019641E-97, BitConverter.ToDouble(x, 3));
		}

		[TestMethod]
		public void ToInt16()
		{
			byte[] x = new byte[] { 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 14, 73, 64 };
			Assert.AreEqual(-28723, BitConverter.ToInt16(x, 1));
		}

		[TestMethod]
		public void ToInt32()
		{
			byte[] x = new byte[] { 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 14, 73, 64 };
			Assert.AreEqual(16826829, BitConverter.ToInt32(x, 6));
		}

		[TestMethod]
		public void ToInt64()
		{
			byte[] x = new byte[] { 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 14, 73, 64 };
			Assert.AreEqual(-6052555591833930664, BitConverter.ToInt64(x, 3));
		}

		[TestMethod]
		public void ToSingle()
		{
			byte[] x = new byte[] { 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 14, 73, 64 };
			Assert.AreEqual(3.1415F, BitConverter.ToSingle(x, 12));
		}

		[TestMethod]
		public void ToString1()
		{
			byte[] x = new byte[] { 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 14, 73, 64 };
			Assert.AreEqual("6F-CD-8F-58-34-6F-CD-C1-00-01-AC-20-56-0E-49-40", BitConverter.ToString(x));
		}

		[TestMethod]
		public void ToString2()
		{
			byte[] x = new byte[] { 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 14, 73, 64 };
			Assert.AreEqual("34-6F-CD-C1-00-01-AC-20-56-0E-49-40", BitConverter.ToString(x, 4));
		}

		[TestMethod]
		public void ToString3()
		{
			byte[] x = new byte[] { 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 14, 73, 64 };
			Assert.AreEqual("34-6F-CD-C1-00", BitConverter.ToString(x, 4, 5));
		}

		[TestMethod]
		public void ToUInt16()
		{
			byte[] x = new byte[] { 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 14, 73, 64 };
			Assert.AreEqual((ushort)13400, BitConverter.ToUInt16(x, 3));
		}

		[TestMethod]
		public void ToUInt32()
		{
			byte[] x = new byte[] { 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 14, 73, 64 };
			Assert.AreEqual(3251466036, BitConverter.ToUInt32(x, 4));
		}

		[TestMethod]
		public void ToUInt64()
		{
			byte[] x = new byte[] { 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 14, 73, 64 };
			Assert.AreEqual((ulong)4632249454805385472, BitConverter.ToUInt64(x, 8));
		}

		[TestMethod]
		public void IsLittleEndian()
		{
			Assert.IsTrue(BitConverter.IsLittleEndian);
		}
	}
}
