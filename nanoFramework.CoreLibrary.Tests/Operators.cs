//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;

namespace nanoFramework.CoreLibrary.Tests
{
	public class Operator : ITestClass
	{
		public void Add()
		{
			Assert.AreEqual(4711, 3896 + 815);
		}

		public void Subtract()
		{
			Assert.AreEqual(68, 110 - 42);
		}

		public void Multiply()
		{
			Assert.AreEqual(3839465, 4711 * 815);
		}

		public void Divide()
		{
			Assert.AreEqual(48, 2352 / 49);
		}

		public void Modulo()
		{
			Assert.AreEqual(30, 2352 % 43);
		}

		public void Increment()
		{
			int x = 2351;
			Assert.AreEqual(2351, x++);
			Assert.AreEqual(2352, x);
			Assert.AreEqual(2353, ++x);
			Assert.AreEqual(2353, x);
		}

		public void Decrement()
		{
			int x = 2353;
			Assert.AreEqual(2353, x--);
			Assert.AreEqual(2352, x);
			Assert.AreEqual(2351, --x);
			Assert.AreEqual(2351, x);
		}

		public void NegationMinus()
		{
			int x = 4711;
			Assert.AreEqual(-4711, -x);
		}

		public void NegationNot()
		{
			bool x = true;
			x = !x;
			Assert.IsFalse(x);
			x = !x;
			Assert.IsTrue(x);
		}

		public void BinaryOr()
		{
			int x = 8;
			x = x | 4 | 16;
			Assert.AreEqual(28, x);
		}

		public void BinaryAnd()
		{
			int x = 8 & 8;
			Assert.AreEqual(8, x);
			x = 8 & 16;
			Assert.AreEqual(0, x);
		}

		public void BinaryXor()
		{
			int x = 8 ^ 12;
			Assert.AreEqual(4, x);
		}

		public void BinaryShiftLeft()
		{
			int x = 256;
			Assert.AreEqual(4096, x << 4);
		}

		public void BinaryShiftRight()
		{
			int x = 256;
			Assert.AreEqual(16, x >> 4);
		}
	}
}
