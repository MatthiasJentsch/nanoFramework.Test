//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	[TestClass]
	public class ConvertTests
	{
		[TestMethod]
		public void FromBase64CharArray()
		{
			try
			{
				// FromBase64CharArray is not implemented
				char[] x = new char[] { 'b', '8', '2', 'P', 'W', 'D', 'R', 'v', 'z', 'c', 'E', 'A', 'A', 'a', 'w', 'g', 'V', 'g', '=', '=' };
				byte[] b = Convert.FromBase64CharArray(x, 4, x.Length - 4);
				Assert.Fail();
				Assert.AreEqual(10, b.Length);
				Assert.AreEqual(88, b[0]);
				Assert.AreEqual(86, b[9]);
			}
			catch (NotImplementedException) { }
		}

		[TestMethod]
		public void FromBase64String()
		{
			try
			{
				// FromBase64CharArray is not implemented
				string x = "b82PWDRvzcEAAawgVg==";
				byte[] b = Convert.FromBase64String(x);
				Assert.Fail();
				Assert.AreEqual(13, b.Length);
				Assert.AreEqual(111, b[0]);
				Assert.AreEqual(86, b[12]);
			}
			catch (NotImplementedException) { }
		}

		[TestMethod]
		public void ToBase64String1()
		{
			try
			{
				// ToBase64String is not implemented
				byte[] x = new byte[] { 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86 };
				Assert.AreEqual("b82PWDRvzcEAAawgVg==", Convert.ToBase64String(x));
				Assert.Fail();
			}
			catch (NotImplementedException) { }
		}

		[TestMethod]
		public void ToBase64String2()
		{
			try
			{
				// ToBase64String is not implemented
				byte[] x1 = new byte[] { 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86 };
				Assert.AreEqual("b82PWDRvzcEAAawgVg==", Convert.ToBase64String(x1, Base64FormattingOptions.None));
				Assert.Fail();
				Assert.AreEqual("b82PWDRvzcEAAawgVg==", Convert.ToBase64String(x1, Base64FormattingOptions.InsertLineBreaks));
				byte[] x2 = new byte[] { 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86 };
				Assert.AreEqual("b82PWDRvzcEAAawgVm/Nj1g0b83BAAGsIFZvzY9YNG/NwQABrCBWb82PWDRvzcEAAawgVm/Nj1g0b83BAAGsIFY=", Convert.ToBase64String(x2, Base64FormattingOptions.None));
				Assert.AreEqual("b82PWDRvzcEAAawgVm/Nj1g0b83BAAGsIFZvzY9YNG/NwQABrCBWb82PWDRvzcEAAawgVm/Nj1g0\r\nb83BAAGsIFY=", Convert.ToBase64String(x2, Base64FormattingOptions.InsertLineBreaks));
			}
			catch (NotImplementedException) { }
		}

		[TestMethod]
		public void ToBase64String3()
		{
			try
			{
				// ToBase64String is not implemented
				byte[] x = new byte[] { 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86 };
				Assert.AreEqual("NG/NwQA=", Convert.ToBase64String(x, 4, 5));
				Assert.Fail();
			}
			catch (NotImplementedException) { }
		}

		[TestMethod]
		public void ToBase64String4()
		{
			try
			{
				// ToBase64String is not implemented
				byte[] x1 = new byte[] { 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86 };
				Assert.AreEqual("NG/NwQA=", Convert.ToBase64String(x1, 4, 5, Base64FormattingOptions.None));
				Assert.Fail();
				Assert.AreEqual("NG/NwQA=", Convert.ToBase64String(x1, 4, 5, Base64FormattingOptions.InsertLineBreaks));
				byte[] x2 = new byte[] { 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86, 111, 205, 143, 88, 52, 111, 205, 193, 0, 1, 172, 32, 86 };
				Assert.AreEqual("NG/NwQABrCBWb82PWDRvzcEAAawgVm/Nj1g0b83BAAGsIFZvzY9YNG/NwQABrCBWb82PWDRvzcEAAQ==", Convert.ToBase64String(x2, 4, x2.Length - 7, Base64FormattingOptions.None));
				Assert.AreEqual("NG/NwQABrCBWb82PWDRvzcEAAawgVm/Nj1g0b83BAAGsIFZvzY9YNG/NwQABrCBWb82PWDRvzcEA\r\nAQ==", Convert.ToBase64String(x2, 4, x2.Length - 7, Base64FormattingOptions.InsertLineBreaks));
			}
			catch (NotImplementedException) { }
		}

		[TestMethod]
		public void ToByte()
		{
			// binary
			Assert.AreEqual(202, Convert.ToByte("11001010", 2));
			// octal
			Assert.AreEqual(83, Convert.ToByte("123", 8));
			// decimal
			Assert.AreEqual(123, Convert.ToByte("123"));
			// hexadecimal
			Assert.AreEqual(172, Convert.ToByte("AC", 16));
		}

		[TestMethod]
		public void ToChar()
		{
			Assert.AreEqual('A', Convert.ToChar(65));
			Assert.AreEqual('€', Convert.ToChar(8364));
		}

		[TestMethod]
		public void ToDouble()
		{
			Assert.AreEqual(4711E+81, Convert.ToDouble("4711E+81"));
			Assert.AreEqual(-815E-47, Convert.ToDouble("-815E-47"));
		}

		[TestMethod]
		public void ToInt16()
		{
			// binary
			Assert.AreEqual(-17078, Convert.ToInt16("1011110101001010", 2));
			// octal
			Assert.AreEqual(-17078, Convert.ToInt16("136512", 8));
			// decimal
			Assert.AreEqual(-17078, Convert.ToInt16("-17078"));
			// hexadecimal
			Assert.AreEqual(-17078, Convert.ToInt16("BD4A", 16));
		}

		[TestMethod]
		public void ToInt32()
		{
			// binary
			Assert.AreEqual(-1234567890, Convert.ToInt32("10110110011010011111110100101110", 2));
			// octal
			Assert.AreEqual(-1234567890, Convert.ToInt32("26632376456", 8));
			// decimal
			Assert.AreEqual(-1234567890, Convert.ToInt32("-1234567890"));
			// hexadecimal
			Assert.AreEqual(-1234567890, Convert.ToInt32("B669FD2E", 16));
		}

		[TestMethod]
		public void ToInt64()
		{
			// binary
			Assert.AreEqual(-4446419168141639693, Convert.ToInt64("1100001001001011001001010101001100100000011011011111111111110011", 2));
			// octal
			Assert.AreEqual(-4446419168141639693, Convert.ToInt64("1411131125144033377763", 8));
			// decimal
			Assert.AreEqual(-4446419168141639693, Convert.ToInt64("-4446419168141639693"));
			// hexadecimal
			Assert.AreEqual(-4446419168141639693, Convert.ToInt64("C24B2553206DFFF3", 16));
		}

		[TestMethod]
		public void ToSByte()
		{
			// binary
			Assert.AreEqual(-42, Convert.ToSByte("11010110", 2));
			// octal
			Assert.AreEqual(-42, Convert.ToSByte("326", 8));
			// decimal
			Assert.AreEqual(-42, Convert.ToSByte("-42"));
			// hexadecimal
			Assert.AreEqual(-42, Convert.ToSByte("D6", 16));
		}

		[TestMethod]
		public void ToUInt16()
		{
			// binary
			Assert.AreEqual(48458, Convert.ToUInt16("1011110101001010", 2));
			// octal
			Assert.AreEqual(48458, Convert.ToUInt16("136512", 8));
			// decimal
			Assert.AreEqual(48458, Convert.ToUInt16("48458"));
			// hexadecimal
			Assert.AreEqual(48458, Convert.ToUInt16("BD4A", 16));
		}

		[TestMethod]
		public void ToUInt32()
		{
			// binary
			Assert.AreEqual(3060399406, Convert.ToUInt32("10110110011010011111110100101110", 2));
			// octal
			Assert.AreEqual(3060399406, Convert.ToUInt32("26632376456", 8));
			// decimal
			Assert.AreEqual(3060399406, Convert.ToUInt32("3060399406"));
			// hexadecimal
			Assert.AreEqual(3060399406, Convert.ToUInt32("B669FD2E", 16));
		}

		[TestMethod]
		public void ToUInt64()
		{
			// binary
			Assert.AreEqual(14000324905567911923, Convert.ToUInt64("1100001001001011001001010101001100100000011011011111111111110011", 2));
			// octal
			Assert.AreEqual(14000324905567911923, Convert.ToUInt64("1411131125144033377763", 8));
			// decimal
			Assert.AreEqual(14000324905567911923, Convert.ToUInt64("14000324905567911923"));
			// hexadecimal
			Assert.AreEqual(14000324905567911923, Convert.ToUInt64("C24B2553206DFFF3", 16));
		}
	}
}