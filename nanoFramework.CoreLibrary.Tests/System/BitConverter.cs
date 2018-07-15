//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;

namespace nanoFramework.CoreLibrary.Tests.System
{
	public class BitConverter : ITestClass
	{
		public void DoubleToInt64Bits()
		{
			double d1 = 3.1415;
			Assert.AreEqual(4614256447914709615, global::System.BitConverter.DoubleToInt64Bits(d1));
			double d2 = -815.4711;
			Assert.AreEqual(-4572987861383272150, global::System.BitConverter.DoubleToInt64Bits(d2));
		}

		public void GetBytesShort()
		{
			short x1 = 4711;
			byte[] b1 = global::System.BitConverter.GetBytes(x1);
			short x2 = -815;
			byte[] b2 = global::System.BitConverter.GetBytes(x2);
			
			
		}

		public void GetBytesLong()
		{

		}

		public void GetBytesUint()
		{

		}

		public void GetBytesUshort()
		{

		}

		public void GetBytesUlong()
		{

		}

		public void GetBytesChar()
		{

		}

		public void GetBytesBool()
		{

		}

		public void GetBytesDouble()
		{

		}

		public void GetBytesInt()
		{

		}

		public void GetBytesFloat()
		{

		}

		public void Int64BitsToDouble()
		{

		}

		public void ToBoolean()
		{

		}

		public void ToChar()
		{

		}

		public void ToDouble()
		{

		}

		public void ToInt16()
		{

		}

		public void ToInt32()
		{

		}

		public void ToInt64()
		{

		}

		public void ToSingle()
		{

		}

		public void ToString1()
		{

		}

		public void ToString2()
		{

		}

		public void ToString3()
		{

		}

		public void ToUInt16()
		{

		}

		public void ToUInt32()
		{

		}

		public void ToUInt64()
		{

		}
	}
}
