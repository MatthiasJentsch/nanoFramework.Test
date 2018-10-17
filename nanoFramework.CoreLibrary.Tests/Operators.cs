//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;

namespace nanoFramework.CoreLibrary.Tests
{
	[TestClass]
	public class OperatorTests
	{
		[TestMethod]
		public void Add()
		{
			Assert.AreEqual(4711, 3896 + 815);
		}

		[TestMethod]
		public void Subtract()
		{
			Assert.AreEqual(68, 110 - 42);
		}

		[TestMethod]
		public void Multiply()
		{
			Assert.AreEqual(3839465, 4711 * 815);
		}

		[TestMethod]
		public void Divide()
		{
			Assert.AreEqual(48, 2352 / 49);
		}

		[TestMethod]
		public void Modulo()
		{
			Assert.AreEqual(30, 2352 % 43);
		}

		[TestMethod]
		public void Increment()
		{
			int x = 2351;
			Assert.AreEqual(2351, x++);
			Assert.AreEqual(2352, x);
			Assert.AreEqual(2353, ++x);
			Assert.AreEqual(2353, x);
		}

		[TestMethod]
		public void Decrement()
		{
			int x = 2353;
			Assert.AreEqual(2353, x--);
			Assert.AreEqual(2352, x);
			Assert.AreEqual(2351, --x);
			Assert.AreEqual(2351, x);
		}

		[TestMethod]
		public void NegationMinus()
		{
			int x = 4711;
			Assert.AreEqual(-4711, -x);
		}

		[TestMethod]
		public void NegationNot()
		{
			bool x = true;
			x = !x;
			Assert.IsFalse(x);
			x = !x;
			Assert.IsTrue(x);
		}

		[TestMethod]
		public void BinaryOr()
		{
			int x = 8;
			x = x | 4 | 16;
			Assert.AreEqual(28, x);
		}

		[TestMethod]
		public void BinaryAnd()
		{
			int x = 8 & 8;
			Assert.AreEqual(8, x);
			x = 8 & 16;
			Assert.AreEqual(0, x);
		}

		[TestMethod]
		public void BinaryXor()
		{
			int x = 8 ^ 12;
			Assert.AreEqual(4, x);
		}

		[TestMethod]
		public void BinaryShiftLeft()
		{
			int x = 256;
			Assert.AreEqual(4096, x << 4);
		}

		[TestMethod]
		public void BinaryShiftRight()
		{
			int x = 256;
			Assert.AreEqual(16, x >> 4);
		}
	}
}
