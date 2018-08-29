//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;
using System.Collections;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.CollectionsTests
{
	public class ArrayListTests : ITestClass, ITestInitialize
	{
		private ArrayList _theList = null;

		public void TestInitialize()
		{
			_theList = new ArrayList();
			_theList.Add('n');
			_theList.Add('a');
			_theList.Add('n');
			_theList.Add('o');
			_theList.Add('F');
			_theList.Add('r');
			_theList.Add('a');
			_theList.Add('m');
			_theList.Add('e');
			_theList.Add('w');
			_theList.Add('o');
			_theList.Add('r');
			_theList.Add('k');
		}

		public void Add()
		{
			_theList.Add('!');
			Assert.AreEqual(14, _theList.Count);
		}

		public void BinarySearch()
		{
			ArrayList a = new ArrayList();
			a.Add('a');
			a.Add('n');
			a.Add('n');
			a.Add('o');
			Assert.AreEqual(3, a.BinarySearch('o', null));
			Assert.AreEqual(-1, a.BinarySearch('A', null));
		}

		public void Clear()
		{
			_theList.Clear();
			Assert.AreEqual(0, _theList.Count);
		}

		public void Clone()
		{
			ArrayList c = _theList.Clone() as ArrayList;
			Assert.IsNotNull(c);
			Assert.AreEqual(13, c.Count);
			Assert.AreEqual('F', c[4]);
		}

		public void Contains()
		{
			Assert.IsTrue(_theList.Contains('F'));
			Assert.IsFalse(_theList.Contains('A'));
		}

		public void CopyTo1()
		{
			char[] c = new char[13];
			_theList.CopyTo(c);
			Assert.AreEqual(13, c.Length);
			Assert.AreEqual('n', c[0]);
			Assert.AreEqual('F', c[4]);
			Assert.AreEqual("nanoFramework", new string(c));
		}

		public void CopyTo2()
		{
			char[] c = new char[15];
			_theList.CopyTo(c, 2);
			Assert.AreEqual(15, c.Length);
			Assert.AreEqual(0, c[0]);
			Assert.AreEqual(0, c[1]);
			Assert.AreEqual('n', c[2]);
			Assert.AreEqual('F', c[6]);
		}

		public void GetEnumerator()
		{
			IEnumerator e = _theList.GetEnumerator();
			Assert.IsNotNull(e);
			Assert.IsTrue(e.MoveNext());
			Assert.AreEqual('n', e.Current);
			Assert.IsTrue(e.MoveNext());
			Assert.AreEqual('a', e.Current);
			Assert.IsTrue(e.MoveNext());
			Assert.IsTrue(e.MoveNext());
			Assert.IsTrue(e.MoveNext());
			Assert.IsTrue(e.MoveNext());
			Assert.IsTrue(e.MoveNext());
			Assert.IsTrue(e.MoveNext());
			Assert.IsTrue(e.MoveNext());
			Assert.IsTrue(e.MoveNext());
			Assert.IsTrue(e.MoveNext());
			Assert.IsTrue(e.MoveNext());
			Assert.IsTrue(e.MoveNext());
			Assert.AreEqual('k', e.Current);
			Assert.IsFalse(e.MoveNext());
			e.Reset();
			Assert.IsTrue(e.MoveNext());
			Assert.AreEqual('n', e.Current);
		}

		public void IndexOf1()
		{
			Assert.AreEqual(3, _theList.IndexOf('o'));
			Assert.AreEqual(-1, _theList.IndexOf('x'));
		}

		public void IndexOf2()
		{
			Assert.AreEqual(4, _theList.IndexOf('F', 3));
			Assert.AreEqual(-1, _theList.IndexOf('F', 5));
		}

		public void IndexOf3()
		{
			Assert.AreEqual(1, _theList.IndexOf('a', 0, 4));
			Assert.AreEqual(6, _theList.IndexOf('a', 4, 4));
			Assert.AreEqual(-1, _theList.IndexOf('a', 8, 4));
		}

		public void Insert()
		{
			_theList.Insert(4, '-');
			Assert.AreEqual(14, _theList.Count);
			Assert.AreEqual('n', _theList[0]);
			Assert.AreEqual('-', _theList[4]);
			Assert.AreEqual('F', _theList[5]);
		}

		public void Remove()
		{
			_theList.Remove('o');
			Assert.AreEqual(12, _theList.Count);
			Assert.AreEqual('n', _theList[2]);
			Assert.AreEqual('F', _theList[3]);
			Assert.AreEqual('o', _theList[9]);
		}

		public void RemoveAt()
		{
			_theList.RemoveAt(4);
			Assert.AreEqual(12, _theList.Count);
			Assert.AreEqual('n', _theList[0]);
			Assert.AreEqual('r', _theList[4]);
			Assert.AreEqual('k', _theList[11]);
		}

		public void ToArray1()
		{
			object[] a = _theList.ToArray();
			Assert.AreEqual(13, a.Length);
			Assert.AreEqual('n', a[0]);
			Assert.AreEqual('k', a[12]);
		}

		public void ToArray2()
		{
			Array a = _theList.ToArray(typeof(char));
			Assert.AreEqual(13, a.Length);
			Assert.AreEqual('n', a.GetValue(0));
			Assert.AreEqual('k', a.GetValue(12));
		}

		public void Count()
		{
			Assert.AreEqual(13, _theList.Count);
		}

		public void IsFixedSize()
		{
			Assert.IsFalse(_theList.IsFixedSize);
		}

		public void IsReadOnly()
		{
			Assert.IsFalse(_theList.IsReadOnly);
		}

		public void IsSynchronized()
		{
			Assert.IsFalse(_theList.IsSynchronized);
		}

		public void Indexer()
		{
			Assert.AreEqual('o', _theList[3]);
		}
	}
}
