//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;
using System.Collections;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	public class ArrayTests : ITestClass
	{
		private readonly IComparer _charComparer = new CharComparer();

		public void BinarySearch1()
		{
			char[] o = new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' };
			Assert.AreEqual(7, Array.BinarySearch(o, 'm', _charComparer));
			Assert.AreEqual(-14, Array.BinarySearch(o, 'x', _charComparer));
		}

		public void BinarySearch2()
		{
			char[] o = new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' };
			Assert.AreEqual(-10, Array.BinarySearch(o, 9, 4, 'm', _charComparer));
			Assert.AreEqual(7, Array.BinarySearch(o, 5, 4, 'm', _charComparer));
		}

		public void Clear()
		{
			char[] o = new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' };
			Array.Clear(o, 4, 5);
			Assert.AreEqual(13, o.Length);
			Assert.AreEqual('o', o[3]);
			Assert.AreEqual(0, o[4]);
			Assert.AreEqual(0, o[5]);
			Assert.AreEqual(0, o[6]);
			Assert.AreEqual(0, o[7]);
			Assert.AreEqual(0, o[8]);
			Assert.AreEqual('w', o[9]);
		}

		public void Clone()
		{
			char[] o = new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' };
			char[] c = (char[])o.Clone();
			Assert.AreEqual(13, c.Length);
			Assert.AreEqual('F', c[4]);
			Assert.AreEqual("nanoFramework", new string(c));
		}

		public void Copy1()
		{
			char[] o = new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' };
			char[] c = new char[13];
			Array.Copy(o, c, 3);
			Assert.AreEqual('n', c[2]);
			Assert.AreEqual(0, c[3]);
		}

		public void Copy2()
		{
			char[] o = new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' };
			char[] c = new char[13];
			Array.Copy(o, 4, c, 2, 5);
			Assert.AreEqual(0, c[0]);
			Assert.AreEqual(0, c[1]);
			Assert.AreEqual('F', c[2]);
			Assert.AreEqual('e', c[6]);
			Assert.AreEqual(0, c[7]);
		}

		public void CopyTo()
		{
			char[] o = new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' };
			char[] c = new char[15];
			o.CopyTo(c, 2);
			Assert.AreEqual(15, c.Length);
			Assert.AreEqual(0, c[0]);
			Assert.AreEqual(0, c[1]);
			Assert.AreEqual('n', c[2]);
			Assert.AreEqual('F', c[6]);
		}

		public void CreateInstance()
		{
			char[] o = (char[])Array.CreateInstance(typeof(char), 3);
			Assert.AreEqual(3, o.Length);
			Assert.AreEqual(0, o[1]);
			Exception[] e = (Exception[])Array.CreateInstance(typeof(Exception), 5);
			Assert.AreEqual(5, e.Length);
			Assert.IsNull(e[3]);
		}

		public void GetEnumerator()
		{
			char[] o = new char[] { 'n', 'f', 'C', 'L', 'R' };
			IEnumerator e = o.GetEnumerator();
			Assert.IsNotNull(e);
			Assert.IsTrue(e.MoveNext());
			Assert.AreEqual('n', e.Current);
			Assert.IsTrue(e.MoveNext());
			Assert.AreEqual('f', e.Current);
			Assert.IsTrue(e.MoveNext());
			Assert.AreEqual('C', e.Current);
			Assert.IsTrue(e.MoveNext());
			Assert.AreEqual('L', e.Current);
			Assert.IsTrue(e.MoveNext());
			Assert.AreEqual('R', e.Current);
			Assert.IsFalse(e.MoveNext());
			e.Reset();
			Assert.IsTrue(e.MoveNext());
			Assert.AreEqual('n', e.Current);
		}

		public void GetValue()
		{
			char[] o = new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' };
			Assert.AreEqual('r', o.GetValue(5));
		}

		public void IndexOf1()
		{
			char[] o = new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' };
			Assert.AreEqual(3, Array.IndexOf(o, 'o'));
			Assert.AreEqual(-1, Array.IndexOf(o, 'x'));
		}

		public void IndexOf2()
		{
			char[] o = new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' };
			Assert.AreEqual(4, Array.IndexOf(o, 'F', 3));
			Assert.AreEqual(-1, Array.IndexOf(o, 'F', 5));
		}

		public void IndexOf3()
		{
			char[] o = new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' };
			Assert.AreEqual(1, Array.IndexOf(o, 'a', 0, 4));
			Assert.AreEqual(6, Array.IndexOf(o, 'a', 4, 4));
			Assert.AreEqual(-1, Array.IndexOf(o, 'a', 8, 4));
		}

		public void IsFixed()
		{
			char[] o = new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' };
			Assert.IsTrue(o.IsFixedSize);
		}

		public void IsReadOnly()
		{
			char[] o = new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' };
			Assert.IsFalse(o.IsReadOnly);
		}

		public void IsSynchronized()
		{
			char[] o = new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' };
			Assert.IsFalse(o.IsSynchronized);
		}

		public void Indexer()
		{
			char[] o = new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' };
			Assert.AreEqual('o', o[3]);
		}

		public void Lenght()
		{
			char[] o = new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' };
			Assert.AreEqual(13, o.Length);
		}

		public void SyncRoot()
		{
			char[] o = new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' };
			Assert.AreSame(o, o.SyncRoot);
		}
	}
}
