//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System.Text;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.TextTests
{
	[TestClass]
	public class UTF8DecoderTests
	{
		[TestMethod]
		public void Convert()
		{
			Decoder d = Encoding.UTF8.GetDecoder();
			char[] r = new char[7];
			int bu;
			int cu;
			bool c;
			d.Convert(new byte[] { 65, 66, 67, 68, 69, 70 }, 1, 5, r, 2, 3, true, out bu, out cu, out c);
			Assert.IsFalse(c);
			Assert.AreEqual(3, bu);
			Assert.AreEqual(3, cu);
			Assert.AreEqual(0, r[0]);
			Assert.AreEqual(0, r[1]);
			Assert.AreEqual('B', r[2]);
			Assert.AreEqual('C', r[3]);
			Assert.AreEqual('D', r[4]);
			Assert.AreEqual(0, r[5]);
			Assert.AreEqual(0, r[6]);
		}
	}
}
