//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System.Text;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.TextTests
{
	[TestClass]
	public class StringBuilderTests
	{
		private StringBuilder _theStringBuilder;

		[TestInitialize]
		public void TestInitialize()
		{
			_theStringBuilder = new StringBuilder("nanoFramework");
		}

		[TestMethod]
		public void Constructor1()
		{
			StringBuilder sb = new StringBuilder(15);
			Assert.AreEqual(15, sb.Capacity);
		}

		[TestMethod]
		public void Constructor2()
		{
			StringBuilder sb = new StringBuilder("nanoFramework");
			Assert.AreEqual(13, sb.Length);
		}

		[TestMethod]
		public void Constructor3()
		{
			StringBuilder sb = new StringBuilder("nanoFramework", 17);
			Assert.AreEqual(13, sb.Length);
			Assert.AreEqual(17, sb.Capacity);
		}

		[TestMethod]
		public void Constructor4()
		{
			StringBuilder sb = new StringBuilder(15, 17);
			Assert.AreEqual(15, sb.Capacity);
			Assert.AreEqual(17, sb.MaxCapacity);
		}

		[TestMethod]
		public void Constructor5()
		{
			StringBuilder sb = new StringBuilder("nanoFramework", 4, 5, 7);
			Assert.AreEqual(5, sb.Length);
			Assert.AreEqual(7, sb.Capacity);
		}

		[TestMethod]
		public void AppendObject()
		{
			Assert.AreEqual("nanoFrameworkSystem.Object", _theStringBuilder.Append(new object()).ToString());
		}

		[TestMethod]
		public void AppendString()
		{
			Assert.AreEqual("nanoFramework - is the best!", _theStringBuilder.Append(" - is the best!").ToString());
		}

		[TestMethod]
		public void AppendLong()
		{
			Assert.AreEqual("nanoFramework1234567899876543210", _theStringBuilder.Append(1234567899876543210).ToString());
		}

		[TestMethod]
		public void AppendCharArray()
		{
			Assert.AreEqual("nanoFramework321", _theStringBuilder.Append(new char[] { '3', '2', '1' }).ToString());
		}

		[TestMethod]
		public void AppendInt()
		{
			Assert.AreEqual("nanoFramework1234567899", _theStringBuilder.Append(1234567899).ToString());
		}

		[TestMethod]
		public void AppendFloat()
		{
			Assert.AreEqual("nanoFramework4711", _theStringBuilder.Append(4711f).ToString());
		}

		[TestMethod]
		public void AppendSbyte()
		{
			Assert.AreEqual("nanoFramework-42", _theStringBuilder.Append((sbyte)-42).ToString());
		}

		[TestMethod]
		public void AppendUshort()
		{
			Assert.AreEqual("nanoFramework65000", _theStringBuilder.Append((ushort)65000).ToString());
		}

		[TestMethod]
		public void AppendUlong()
		{
			Assert.AreEqual("nanoFramework12345678998765432100", _theStringBuilder.Append(12345678998765432100).ToString());
		}

		[TestMethod]
		public void AppendUint()
		{
			Assert.AreEqual("nanoFramework4234567899", _theStringBuilder.Append((uint)4234567899).ToString());
		}

		[TestMethod]
		public void AppendShort()
		{
			Assert.AreEqual("nanoFramework12000", _theStringBuilder.Append((short)12000).ToString());
		}

		[TestMethod]
		public void AppendByte()
		{
			Assert.AreEqual("nanoFramework42", _theStringBuilder.Append((byte)42).ToString());
		}

		[TestMethod]
		public void AppendBool()
		{
			Assert.AreEqual("nanoFrameworkTrue", _theStringBuilder.Append(true).ToString());
		}

		[TestMethod]
		public void AppendDouble()
		{
			Assert.AreEqual("nanoFramework81500", _theStringBuilder.Append(815E+2).ToString());
		}

		[TestMethod]
		public void AppendChar()
		{
			Assert.AreEqual("nanoFramework!", _theStringBuilder.Append('!').ToString());
		}

		[TestMethod]
		public void AppendChar2()
		{
			Assert.AreEqual("nanoFramework!!!", _theStringBuilder.Append('!', 3).ToString());
		}

		[TestMethod]
		public void AppendString2()
		{
			Assert.AreEqual("nanoFramework best", _theStringBuilder.Append(" - is the best!", 9, 5).ToString());
		}

		[TestMethod]
		public void AppendCharArray2()
		{
			Assert.AreEqual("nanoFrameworkFrame", _theStringBuilder.Append(new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' }, 4, 5).ToString());
		}

		[TestMethod]
		public void AppendLine1()
		{
			Assert.AreEqual("nanoFramework\r\n", _theStringBuilder.AppendLine().ToString());
		}

		[TestMethod]
		public void AppendLine2()
		{
			Assert.AreEqual("nanoFramework - is the best!\r\n", _theStringBuilder.AppendLine(" - is the best!").ToString());
		}

		[TestMethod]
		public void Clear()
		{
			Assert.AreEqual(0, _theStringBuilder.Clear().Length);
		}

		[TestMethod]
		public void Insert1()
		{
			Assert.AreEqual("nano(*)(*)(*)Framework", _theStringBuilder.Insert(4, "(*)", 3).ToString());
		}

		[TestMethod]
		public void Insert2()
		{
			Assert.AreEqual("nanoFrameFramework", _theStringBuilder.Insert(4, new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' }, 4, 5).ToString());
		}

		[TestMethod]
		public void Remove()
		{
			Assert.AreEqual("nawork", _theStringBuilder.Remove(2, 7).ToString());
		}

		[TestMethod]
		public void Replace1()
		{
			Assert.AreEqual("-a-oFramework", _theStringBuilder.Replace("n", "-").ToString());
			Assert.AreEqual("-a-o***work", _theStringBuilder.Replace("Frame", "***").ToString());
		}

		[TestMethod]
		public void Replace2()
		{
			Assert.AreEqual("-a-oFramework", _theStringBuilder.Replace('n', '-').ToString());
		}

		[TestMethod]
		public void Replace3()
		{
			Assert.AreEqual("na-oFramework", _theStringBuilder.Replace('n', '-', 1, 10).ToString());
		}

		[TestMethod]
		public void Replace4()
		{
			Assert.AreEqual("nanoFramework", _theStringBuilder.Replace("Frame", "***", 6, 5).ToString());
			Assert.AreEqual("nano***work", _theStringBuilder.Replace("Frame", "***", 2, 10).ToString());
		}

		[TestMethod]
		public void ToString1()
		{
			Assert.AreEqual("nanoFramework", _theStringBuilder.ToString());
		}

		[TestMethod]
		public void ToString2()
		{
			Assert.AreEqual("Frame", _theStringBuilder.ToString(4, 5));
		}

		[TestMethod]
		public void Capacity()
		{
			StringBuilder sb = new StringBuilder(15);
			Assert.AreEqual(15, sb.Capacity);
		}

		[TestMethod]
		public void Indexer()
		{
			Assert.AreEqual('F', _theStringBuilder[4]);
		}

		[TestMethod]
		public void Length()
		{
			Assert.AreEqual(13, _theStringBuilder.Length);
		}

		[TestMethod]
		public void MaxCapacity()
		{
			StringBuilder sb = new StringBuilder(15, 17);
			Assert.AreEqual(17, sb.MaxCapacity);
		}
	}
}
