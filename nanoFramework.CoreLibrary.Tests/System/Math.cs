//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	[TestClass]
	public class MathTests
	{
		private const int MaxIntValueDifferenceDouble = 10;
		private const int MaxIntValueDifferenceFloat = 10;

		private bool floatImplemented = false;
		private bool doubleImplemented = false;

		[TestInitialize]
		public void TestInitialize()
		{
			try
			{
				Math.Abs(-0.3);
				doubleImplemented = true;
			}
			catch (NotImplementedException) { }
			try
			{
				Math.Abs(-0.3f);
				floatImplemented = true;
			}
			catch (NotImplementedException) { }

			if (!floatImplemented && !doubleImplemented)
			{
				throw new NotSupportedException();
			}
		}

		[TestMethod]
		public void AbsDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(815.4711, Math.Abs(815.4711));
				AssertIsDoubleCloseEnough(815.4711, Math.Abs(-815.4711));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void AbsFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(815.4711f, Math.Abs(815.4711f));
				AssertIsFloatCloseEnough(815.4711f, Math.Abs(-815.4711f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void AbsInt32()
		{
			Assert.AreEqual(4711, Math.Abs(4711));
			Assert.AreEqual(4711, Math.Abs(-4711));
		}

		[TestMethod]
		public void AcosDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(1.0801665132267018391887988383441, Math.Acos(0.4711815));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void AcosFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(1.0801665132267018391887988383441f, (float)Math.Acos(0.4711815f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void AsinDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(0.4906298135681947800425228532957, Math.Asin(0.4711815));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void AsinFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(0.4906298135681947800425228532957f, (float)Math.Asin(0.4711815f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void AtanDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(0.44032817575325219404857976978333, Math.Atan(0.4711815));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void AtanFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(0.44032817575325219404857976978333f, (float)Math.Atan(0.4711815f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void Atan2Double()
		{
			try
			{
				AssertIsDoubleCloseEnough(0.17132086544315422, Math.Atan2(815.1, 4711.1));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void Atan2Float()
		{
			try
			{
				AssertIsFloatCloseEnough(0.17132086544315422f, (float)Math.Atan2(815.1f, 4711.1f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void CeilingDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(4712, Math.Ceiling(4711.815));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void CeilingFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(4712f, (float)Math.Ceiling(4711.815f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void CosDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(0.83974636838455308, Math.Cos(4711.815));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void CosFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(0.8397145f, (float)Math.Cos(4711.815f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void CoshDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(55.631445525388421244878354021623, Math.Cosh(4.711815));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void CoshFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(55.631445525388421244878354021623f, (float)Math.Cosh(4.711815f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void ExpDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(111.25390260204280555587401815578, Math.Exp(4.711815));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void ExpFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(111.253891f, (float)Math.Exp(4.711815f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void FloorDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(4711, Math.Floor(4711.815));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void FloorFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(4711f, (float)Math.Floor(4711.815f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void IEEERemainderDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(-181.01160000000027, Math.IEEERemainder(4711.815, 815.4711));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void IEEERemainderFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(-181.011841f, (float)Math.IEEERemainder(4711.815f, 815.4711f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void LogDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(8.4578284631005687, Math.Log(4711.815));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void LogFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(8.4578284631005687f, (float)Math.Log(4711.815f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void Log10Double()
		{
			try
			{
				AssertIsDoubleCloseEnough(3.6731882304088384851672447367624, Math.Log10(4711.815));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void Log10Float()
		{
			try
			{
				AssertIsFloatCloseEnough(3.6731882304088384851672447367624f, (float)Math.Log10(4711.815f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void MaxDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(815.4711, Math.Max(-4711.815, 815.4711));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void MaxFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(815.4711f, (float)Math.Max(-4711.815f, 815.4711f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void MaxInt32()
		{
			Assert.AreEqual(815, Math.Max(-4711, 815));
		}

		[TestMethod]
		public void MinDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(-4711.815, Math.Min(-4711.815, 815.4711));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void MinFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(-4711.815f, (float)Math.Min(-4711.815f, 815.4711f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void MinInt32()
		{
			Assert.AreEqual(-4711, Math.Min(-4711, 815));
		}

		[TestMethod]
		public void PowDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(43238617505380.005785314240004362, Math.Pow(47.11, 8.15));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void PowFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(4.323856E+13f, (float)Math.Pow(47.11f, 8.15f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void RoundDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(-8, Math.Round(-8.15));
				AssertIsDoubleCloseEnough(-9, Math.Round(-8.65));
				AssertIsDoubleCloseEnough(-8, Math.Round(-8.5));
				AssertIsDoubleCloseEnough(-10, Math.Round(-9.5));
				AssertIsDoubleCloseEnough(47, Math.Round(47.11));
				AssertIsDoubleCloseEnough(48, Math.Round(47.93));
				AssertIsDoubleCloseEnough(48, Math.Round(47.5));
				AssertIsDoubleCloseEnough(50, Math.Round(50.5));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void RoundFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(-8f, (float)Math.Round(-8.15f));
				AssertIsFloatCloseEnough(-9f, (float)Math.Round(-8.65f));
				AssertIsFloatCloseEnough(-8f, (float)Math.Round(-8.5f));
				AssertIsFloatCloseEnough(-10f, (float)Math.Round(-9.5f));
				AssertIsFloatCloseEnough(47f, (float)Math.Round(47.11f));
				AssertIsFloatCloseEnough(48f, (float)Math.Round(47.93f));
				AssertIsFloatCloseEnough(48f, (float)Math.Round(47.5f));
				AssertIsFloatCloseEnough(50f, (float)Math.Round(50.5f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void SignDouble()
		{
			try
			{
				Assert.AreEqual(-1, Math.Sign(-8.15));
				Assert.AreEqual(0, Math.Sign(0));
				Assert.AreEqual(1, Math.Sign(47.11));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void SignFloat()
		{
			try
			{
				Assert.AreEqual(-1, Math.Sign(-8.15f));
				Assert.AreEqual(0, Math.Sign(0f));
				Assert.AreEqual(1, Math.Sign(47.11f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void SinDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(-0.54297885482305341, Math.Sin(4711.815));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void SinFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(-0.543028057f, (float)Math.Sin(4711.815f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void SinhDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(55.622457076654384310995664134154, Math.Sinh(4.711815));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void SinhFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(55.622457076654384310995664134154f, (float)Math.Sinh(4.711815f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void SqrtDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(68.642661661680922904331726478901, Math.Sqrt(4711.815));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void SqrtFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(68.642661661680922904331726478901f, (float)Math.Sqrt(4711.815f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void TanDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(-0.64659863414187679, Math.Tan(4711.815));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void TanFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(-0.6466817f, (float)Math.Tan(4711.815f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void TanhDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(0.99983842863241914337923439329847, Math.Tanh(4.711815));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void TanhFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(0.99983842863241914337923439329847f, (float)Math.Tanh(4.711815f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void TruncateDouble()
		{
			try
			{
				AssertIsDoubleCloseEnough(4711, Math.Truncate(4711.815));
				AssertIsDoubleCloseEnough(-815, Math.Truncate(-815.4711));
			}
			catch (NotImplementedException)
			{
				if (doubleImplemented)
				{
					throw;
				}
			}
		}

		[TestMethod]
		public void TruncateFloat()
		{
			try
			{
				AssertIsFloatCloseEnough(4711f, (float)Math.Truncate(4711.815f));
				AssertIsFloatCloseEnough(-815f, (float)Math.Truncate(-815.4711f));
			}
			catch (NotImplementedException)
			{
				if (floatImplemented)
				{
					throw;
				}
			}
		}

		private void AssertIsDoubleCloseEnough(double value1, double value2)
		{
			long long1 = BitConverter.DoubleToInt64Bits(value1);
			long long2 = BitConverter.DoubleToInt64Bits(value2);
			long diff = long1 > long2 ? long1 - long2 : long2 - long1;
			Assert.IsTrue(diff <= MaxIntValueDifferenceDouble);
		}

		private void AssertIsFloatCloseEnough(float value1, float value2)
		{
			byte[] bytes1 = BitConverter.GetBytes(value1);
			int int1 = bytes1[0] + (bytes1[1] << 8) + (bytes1[1] << 16) + (bytes1[2] << 24);
			byte[] bytes2 = BitConverter.GetBytes(value2);
			int int2 = bytes2[0] + (bytes2[1] << 8) + (bytes2[1] << 16) + (bytes2[2] << 24);
			int diff = int1 > int2 ? int1 - int2 : int2 - int1;
			Assert.IsTrue(diff <= MaxIntValueDifferenceFloat);
		}
	}
}