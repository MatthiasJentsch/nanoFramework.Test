//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System.Collections;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.CollectionsTests
{
	[TestClass]
	public class HashtableTests
	{
		private Hashtable _theHashtable = null;

		[TestInitialize]
		public void TestInitialize()
		{
			_theHashtable = new Hashtable();
			_theHashtable.Add(2, 'n');
			_theHashtable.Add(3, 'a');
			_theHashtable.Add(5, 'n');
			_theHashtable.Add(7, 'o');
			_theHashtable.Add(11, 'F');
			_theHashtable.Add(13, 'r');
			_theHashtable.Add(17, 'a');
			_theHashtable.Add(19, 'm');
			_theHashtable.Add(23, 'e');
			_theHashtable.Add(29, 'w');
			_theHashtable.Add(31, 'o');
			_theHashtable.Add(37, 'r');
			_theHashtable.Add(41, 'k');
		}

		[TestMethod]
		public void Constructor1()
		{
			Hashtable ht = new Hashtable(12);
			Assert.AreEqual(0, ht.Count);
		}

		[TestMethod]
		public void Constructor2()
		{
			Hashtable ht = new Hashtable(12, 1);
			Assert.AreEqual(0, ht.Count);
		}

		[TestMethod]
		public void Add()
		{
			_theHashtable.Add(43, '!');
			Assert.AreEqual(14, _theHashtable.Count);
		}

		[TestMethod]
		public void Clear()
		{
			_theHashtable.Clear();
			Assert.AreEqual(0, _theHashtable.Count);
		}

		[TestMethod]
		public void Clone()
		{
			Hashtable c = _theHashtable.Clone() as Hashtable;
			Assert.IsNotNull(c);
			Assert.AreEqual(13, c.Count);
			Assert.AreEqual('F', c[11]);
		}

		[TestMethod]
		public void Contains()
		{
			Assert.IsTrue(_theHashtable.Contains(11));
			Assert.IsFalse(_theHashtable.Contains(10));
		}

		[TestMethod]
		public void CopyTo()
		{
			object[] c = new object[15];
			_theHashtable.CopyTo(c, 2);
			Assert.AreEqual(15, c.Length);
			Assert.IsNull(c[0]);
			Assert.IsNull(c[1]);
			Assert.IsNotNull(c[2]);
			Assert.IsNotNull(c[14]);
		}

		[TestMethod]
		public void GetEnumerator()
		{
			IEnumerator e = _theHashtable.GetEnumerator();
			Assert.IsNotNull(e);
			Assert.IsTrue(e.MoveNext());
			Assert.IsNotNull(e.Current);
			e.Reset();
			Assert.IsTrue(e.MoveNext());
			Assert.IsNotNull(e.Current);
		}

		[TestMethod]
		public void Remove()
		{
			Assert.IsTrue(_theHashtable.Contains(11));
			_theHashtable.Remove(11);
			Assert.IsFalse(_theHashtable.Contains(11));
		}

		[TestMethod]
		public void Count()
		{
			Assert.AreEqual(13, _theHashtable.Count);
		}

		[TestMethod]
		public void IsFixedSize()
		{
			Assert.IsFalse(_theHashtable.IsFixedSize);
		}

		[TestMethod]
		public void IsReadOnly()
		{
			Assert.IsFalse(_theHashtable.IsReadOnly);
		}

		[TestMethod]
		public void IsSynchronized()
		{
			Assert.IsFalse(_theHashtable.IsSynchronized);
		}

		[TestMethod]
		public void Indexer()
		{
			Assert.AreEqual('F', _theHashtable[11]);
		}

		[TestMethod]
		public void Keys()
		{
			ICollection k = _theHashtable.Keys;
			Assert.IsNotNull(k);
			Assert.AreEqual(13, k.Count);
		}

		[TestMethod]
		public void Values()
		{
			ICollection v = _theHashtable.Values;
			Assert.IsNotNull(v);
			Assert.AreEqual(13, v.Count);
		}
	}
}
