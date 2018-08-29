//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System.Collections;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.CollectionsTests
{
	public class StackTests : ITestClass, ITestInitialize
	{
		private Stack _theStack = null;

		public void TestInitialize()
		{
			_theStack = new Stack();
			_theStack.Push('n');
			_theStack.Push('a');
			_theStack.Push('n');
			_theStack.Push('o');
			_theStack.Push('F');
			_theStack.Push('r');
			_theStack.Push('a');
			_theStack.Push('m');
			_theStack.Push('e');
			_theStack.Push('w');
			_theStack.Push('o');
			_theStack.Push('r');
			_theStack.Push('k');
		}

		public void Clear()
		{
			_theStack.Clear();
			Assert.AreEqual(0, _theStack.Count);
		}

		public void Clone()
		{
			Stack c = _theStack.Clone() as Stack;
			Assert.IsNotNull(c);
			Assert.AreEqual(13, c.Count);
		}

		public void Contains()
		{
			Assert.IsTrue(_theStack.Contains('F'));
			Assert.IsFalse(_theStack.Contains('A'));
		}

		public void CopyTo()
		{
			char[] c = new char[15];
			_theStack.CopyTo(c, 2);
			Assert.AreEqual(15, c.Length);
			Assert.AreEqual(0, c[0]);
			Assert.AreEqual(0, c[1]);
			Assert.AreEqual('k', c[2]);
			Assert.AreEqual('F', c[10]);
		}

		public void GetEnumerator()
		{
			IEnumerator e = _theStack.GetEnumerator();
			Assert.IsNotNull(e);
			Assert.IsTrue(e.MoveNext());
			Assert.AreEqual('k', e.Current);
			Assert.IsTrue(e.MoveNext());
			Assert.AreEqual('r', e.Current);
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
			Assert.AreEqual('n', e.Current);
			Assert.IsFalse(e.MoveNext());
			e.Reset();
			Assert.IsTrue(e.MoveNext());
			Assert.AreEqual('k', e.Current);
		}

		public void Peek()
		{
			object x = _theStack.Peek();
			Assert.IsNotNull(x);
			Assert.AreEqual('k', x);
			Assert.AreEqual(13, _theStack.Count);
		}

		public void Pop()
		{
			object x = _theStack.Pop();
			Assert.IsNotNull(x);
			Assert.AreEqual('k', x);
			Assert.AreEqual(12, _theStack.Count);
		}

		public void Push()
		{
			_theStack.Push('!');
			Assert.AreEqual(14, _theStack.Count);
		}
		
		public void ToArray()
		{
			object[] a = _theStack.ToArray();
			Assert.AreEqual(13, a.Length);
			Assert.AreEqual('k', a[0]);
			Assert.AreEqual('n', a[12]);
		}

		public void Count()
		{
			Assert.AreEqual(13, _theStack.Count);
		}

		public void IsSynchronized()
		{
			Assert.IsFalse(_theStack.IsSynchronized);
		}
	}
}
