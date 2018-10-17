//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System.Collections;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.CollectionsTests
{
	[TestClass]
	public class QueueTests
	{
		private Queue _theQueue = null;

		[TestInitialize]
		public void TestInitialize()
		{
			_theQueue = new Queue();
			_theQueue.Enqueue('n');
			_theQueue.Enqueue('a');
			_theQueue.Enqueue('n');
			_theQueue.Enqueue('o');
			_theQueue.Enqueue('F');
			_theQueue.Enqueue('r');
			_theQueue.Enqueue('a');
			_theQueue.Enqueue('m');
			_theQueue.Enqueue('e');
			_theQueue.Enqueue('w');
			_theQueue.Enqueue('o');
			_theQueue.Enqueue('r');
			_theQueue.Enqueue('k');
		}

		[TestMethod]
		public void Clear()
		{
			_theQueue.Clear();
			Assert.AreEqual(0, _theQueue.Count);
		}

		[TestMethod]
		public void Clone()
		{
			Queue c = _theQueue.Clone() as Queue;
			Assert.IsNotNull(c);
			Assert.AreEqual(13, c.Count);
		}

		[TestMethod]
		public void Contains()
		{
			Assert.IsTrue(_theQueue.Contains('F'));
			Assert.IsFalse(_theQueue.Contains('A'));
		}

		[TestMethod]
		public void CopyTo()
		{
			char[] c = new char[15];
			_theQueue.CopyTo(c, 2);
			Assert.AreEqual(15, c.Length);
			Assert.AreEqual(0, c[0]);
			Assert.AreEqual(0, c[1]);
			Assert.AreEqual('n', c[2]);
			Assert.AreEqual('F', c[6]);
		}

		[TestMethod]
		public void Dequeue()
		{
			object x = _theQueue.Dequeue();
			Assert.IsNotNull(x);
			Assert.AreEqual('n', x);
			Assert.AreEqual(12, _theQueue.Count);
		}

		[TestMethod]
		public void Enqueue()
		{
			_theQueue.Enqueue('!');
			Assert.AreEqual(14, _theQueue.Count);
		}

		[TestMethod]
		public void GetEnumerator()
		{
			IEnumerator e = _theQueue.GetEnumerator();
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

		[TestMethod]
		public void Peek()
		{
			object x = _theQueue.Peek();
			Assert.IsNotNull(x);
			Assert.AreEqual('n', x);
			Assert.AreEqual(13, _theQueue.Count);
		}

		[TestMethod]
		public void ToArray()
		{
			object[] a = _theQueue.ToArray();
			Assert.AreEqual(13, a.Length);
			Assert.AreEqual('n', a[0]);
			Assert.AreEqual('k', a[12]);
		}

		[TestMethod]
		public void Count()
		{
			Assert.AreEqual(13, _theQueue.Count);
		}

		[TestMethod]
		public void IsSynchronized()
		{
			Assert.IsFalse(_theQueue.IsSynchronized);
		}
	}
}
