//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System.Text;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.TextTests
{
	[TestClass]
	public class UTF8EncodingTests
	{
		private Encoding _encoding;

		[ClassInitialize]
		public void ClassInitialize()
		{
			_encoding = Encoding.UTF8;
		}

		[TestMethod]
		public void GetBytes1()
		{
			Assert.AreEqual(13, _encoding.GetBytes("nanoFramework").Length);
		}

		[TestMethod]
		public void GetBytes2()
		{
			byte[] r = new byte[7];
			Assert.AreEqual(5, _encoding.GetBytes("nanoFramework", 4, 5, r, 1));
		}

		[TestMethod]
		public void GetChars1()
		{
			Assert.AreEqual("ABCDEF", new string(_encoding.GetChars(new byte[] { 65, 66, 67, 68, 69, 70 })));
		}

		[TestMethod]
		public void GetChars2()
		{
			Assert.AreEqual("BCD", new string(_encoding.GetChars(new byte[] { 65, 66, 67, 68, 69, 70 }, 1, 3)));
		}

		[TestMethod]
		public void GetDecoder()
		{
			Assert.IsNotNull(_encoding.GetDecoder());
		}
	}
}
