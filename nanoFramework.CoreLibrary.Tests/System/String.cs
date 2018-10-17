//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	[TestClass]
	public class StringTests
	{
		[TestMethod]
		public void Constructor1()
		{
			string s = new string(new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' });
			Assert.AreEqual("nanoFramework", s);
		}

		[TestMethod]
		public void Constructor2()
		{
			string s = new string('x', 42);
			Assert.AreEqual("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", s);
		}

		[TestMethod]
		public void Constructor3()
		{
			string s = new string(new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' }, 2, 7);
			Assert.AreEqual("noFrame", s);
		}

		[TestMethod]
		public void Compare()
		{
			Assert.IsTrue(string.Compare("nanoFramework", "nanoCLR") > 0);
			Assert.IsTrue(string.Compare("nanoCLR", "nanoFramework") < 0);
			Assert.IsTrue(string.Compare("nanoFramework", "nanoFramework") == 0);
		}

		[TestMethod]
		public void CompareTo1()
		{
			string s = "nanoFramework";
			Assert.IsTrue(s.CompareTo("nanoCLR") > 0);
			Assert.IsTrue(s.CompareTo("other") < 0);
			Assert.IsTrue(s.CompareTo("nanoFramework") == 0);
		}

		[TestMethod]
		public void CompareTo2()
		{
			string s = "nanoFramework";
			Assert.IsTrue(s.CompareTo((object)"nanoCLR") > 0);
			Assert.IsTrue(s.CompareTo((object)"other") < 0);
			Assert.IsTrue(s.CompareTo((object)"nanoFramework") == 0);
		}

		[TestMethod]
		public void Concat1()
		{
			Assert.AreEqual("nanoFramework", string.Concat((object)"nanoFramework"));
		}

		[TestMethod]
		public void Concat2()
		{
			Assert.AreEqual("nanoFramework", string.Concat((object)"nano", (object)"Framework"));
		}

		[TestMethod]
		public void Concat3()
		{
			Assert.AreEqual("nanoFramework", string.Concat((object)"nano", (object)"Frame", (object)"work"));
		}

		[TestMethod]
		public void Concat4()
		{
			Assert.AreEqual("nanoFramework", string.Concat((object)"na", (object)"no", (object)"Frame", (object)"work"));
		}

		[TestMethod]
		public void Concat5()
		{
			Assert.AreEqual("nanoFrameworkTest", string.Concat("nanoFramework", "Test"));
		}

		[TestMethod]
		public void Concat6()
		{
			Assert.AreEqual("nanoFrameworkTest", string.Concat("nano", "Framework", "Test"));
		}

		[TestMethod]
		public void Concat7()
		{
			Assert.AreEqual("nanoFrameworkTest", string.Concat("nano", "Frame", "work", "Test"));
		}

		[TestMethod]
		public void Concat8()
		{
			Assert.AreEqual("nanoFrameworkTest", string.Concat("na", "no", "Frame", "work", "Test"));
		}

		[TestMethod]
		public void Format()
		{
			Assert.AreEqual("nanoFramework tests are running!", string.Format("nanoFramework {0} are {1}!", "tests", "running"));
		}

		[TestMethod]
		public void IndexOf1()
		{
			string s = "nanoFramework tests are running!";
			Assert.AreEqual(14, s.IndexOf('t'));
			Assert.AreEqual(-1, s.IndexOf('x'));
		}

		[TestMethod]
		public void IndexOf2()
		{
			string s = "nanoFramework tests are running!";
			Assert.AreEqual(15, s.IndexOf("est"));
			Assert.AreEqual(-1, s.IndexOf("micro"));
		}

		[TestMethod]
		public void IndexOf3()
		{
			string s = "nanoFramework tests are running!";
			Assert.AreEqual(17, s.IndexOf('t', 15));
			Assert.AreEqual(-1, s.IndexOf('t', 19));
		}

		[TestMethod]
		public void IndexOf4()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(35, s.IndexOf("nano", 10));
			Assert.AreEqual(-1, s.IndexOf("nano", 36));
			Assert.AreEqual(-1, s.IndexOf("nano", 42));
		}

		[TestMethod]
		public void IndexOf5()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(39, s.IndexOf('C', 30, 10));
			Assert.AreEqual(-1, s.IndexOf('C', 25, 10));
			Assert.AreEqual(-1, s.IndexOf('C', 40, 3));
		}

		[TestMethod]
		public void IndexOf6()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(35, s.IndexOf("nano", 30, 10));
			Assert.AreEqual(-1, s.IndexOf("nano", 25, 10));
			Assert.AreEqual(-1, s.IndexOf("nano", 40, 3));
		}

		[TestMethod]
		public void IndexOfAny1()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(8, s.IndexOfAny(new char[] { 'e', 's', 't', 'l' }));
			Assert.AreEqual(-1, s.IndexOfAny(new char[] { 'b', 'c', 'd', 'f', 'h', 'j' }));
		}

		[TestMethod]
		public void IndexOfAny2()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(11, s.IndexOfAny(new char[] { 'e', 'r', 'n', 's', 't', 'l' }, 10));
			Assert.AreEqual(-1, s.IndexOfAny(new char[] { 'e', 'r', 'n', 's', 't', 'l' }, 38));
			Assert.AreEqual(-1, s.IndexOfAny(new char[] { 'e', 'r', 'n', 's', 't', 'l' }, 42));
		}

		[TestMethod]
		public void IndexOfAny3()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(14, s.IndexOfAny(new char[] { 'e', 'n', 's', 't', 'l' }, 9, 10));
			Assert.AreEqual(-1, s.IndexOfAny(new char[] { 'e', 'n', 's', 't', 'l' }, 9, 4));
			Assert.AreEqual(-1, s.IndexOfAny(new char[] { 'e', 'n', 's', 't', 'l' }, 40, 3));
		}

		[TestMethod]
		public void Intern()
		{
			Assert.AreEqual("nanoFramework tests", string.Intern("nanoFramework tests"));
		}

		[TestMethod]
		public void IsInterned()
		{
			Assert.IsNotNull(string.IsInterned("aramsamsam"));
		}

		[TestMethod]
		public void LastIndexOf1()
		{
			string s = "nanoFramework tests are running!";
			Assert.AreEqual(17, s.LastIndexOf('t'));
			Assert.AreEqual(-1, s.LastIndexOf('x'));
		}

		[TestMethod]
		public void LastIndexOf2()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(35, s.LastIndexOf("nano"));
			Assert.AreEqual(-1, s.LastIndexOf("micro"));
		}

		[TestMethod]
		public void LastIndexOf3()
		{
			string s = "nanoFramework tests are running!";
			Assert.AreEqual(14, s.LastIndexOf('t', 15));
			Assert.AreEqual(-1, s.LastIndexOf('t', 13));
		}

		[TestMethod]
		public void LastIndexOf4()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(0, s.LastIndexOf("nano", 10));
			Assert.AreEqual(35, s.LastIndexOf("nano", 40));
			Assert.AreEqual(0, s.LastIndexOf("nano", 3));
			Assert.AreEqual(-1, s.LastIndexOf("nano", 2));
			Assert.AreEqual(-1, s.LastIndexOf("are", 10));
		}

		[TestMethod]
		public void LastIndexOf5()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(2, s.LastIndexOf('n', 20, 20));
			Assert.AreEqual(37, s.LastIndexOf('n', 40, 10));
			Assert.AreEqual(-1, s.LastIndexOf('w', 5, 3));
		}

		[TestMethod]
		public void LastIndexOf6()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(35, s.LastIndexOf("nano", 40, 10));
			Assert.AreEqual(-1, s.LastIndexOf("nano", 25, 10));
			Assert.AreEqual(-1, s.LastIndexOf("nano", 3, 3));
		}

		[TestMethod]
		public void LastIndexOfAny1()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(22, s.LastIndexOfAny(new char[] { 'e', 's', 't', 'l' }));
			Assert.AreEqual(-1, s.LastIndexOfAny(new char[] { 'b', 'c', 'd', 'f', 'h', 'j' }));
		}

		[TestMethod]
		public void LastIndexOfAny2()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(8, s.LastIndexOfAny(new char[] { 'e', 'r', 'n', 's', 't', 'l' }, 10));
			Assert.AreEqual(37, s.LastIndexOfAny(new char[] { 'e', 'r', 'n', 's', 't', 'l' }, 38));
			Assert.AreEqual(-1, s.LastIndexOfAny(new char[] { 'e', 'r', 's', 't', 'l' }, 4));
		}

		[TestMethod]
		public void LastIndexOfAny3()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(18, s.LastIndexOfAny(new char[] { 'e', 'n', 's', 't', 'l' }, 20, 10));
			Assert.AreEqual(-1, s.LastIndexOfAny(new char[] { 'e', 'n', 's', 't', 'l' }, 12, 4));
			Assert.AreEqual(-1, s.LastIndexOfAny(new char[] { 'e', 's', 't', 'l' }, 5, 3));
		}

		[TestMethod]
		public void PadLeft()
		{
			string s = "nanoFramework";
			Assert.AreEqual("*******nanoFramework", s.PadLeft(20, '*'));
		}

		[TestMethod]
		public void PadRight()
		{
			string s = "nanoFramework";
			Assert.AreEqual("nanoFramework*******", s.PadRight(20, '*'));
		}

		[TestMethod]
		public void Split1()
		{
			string s = "nanoFramework tests: are running on nanoCLR!";
			string[] p = s.Split(' ', ':');
			Assert.IsNotNull(p);
			Assert.AreEqual(7, p.Length);
			Assert.AreEqual("nanoFramework", p[0]);
			Assert.AreEqual("tests", p[1]);
			Assert.AreEqual(string.Empty, p[2]);
			Assert.AreEqual("are", p[3]);
			Assert.AreEqual("running", p[4]);
			Assert.AreEqual("on", p[5]);
			Assert.AreEqual("nanoCLR!", p[6]);
		}

		[TestMethod]
		public void Split2()
		{
			string s = "nanoFramework tests: are running on nanoCLR!";
			string[] p = s.Split(new char[] { ' ', ':' }, 4);
			Assert.IsNotNull(p);
			Assert.AreEqual(4, p.Length);
			Assert.AreEqual("nanoFramework", p[0]);
			Assert.AreEqual("tests", p[1]);
			Assert.AreEqual(string.Empty, p[2]);
			Assert.AreEqual("are running on nanoCLR!", p[3]);
		}

		[TestMethod]
		public void Substring1()
		{
			string s = "nanoFramework tests: are running on nanoCLR!";
			Assert.AreEqual("running on nanoCLR!", s.Substring(25));
		}

		[TestMethod]
		public void Substring2()
		{
			string s = "nanoFramework tests: are running on nanoCLR!";
			Assert.AreEqual("running on", s.Substring(25, 10));
		}

		[TestMethod]
		public void ToCharArray1()
		{
			string s = "nanoFramework";
			char[] c = s.ToCharArray();
			Assert.IsNotNull(c);
			Assert.AreEqual(13, c.Length);
			Assert.AreEqual('F', c[4]);
		}

		[TestMethod]
		public void ToCharArray2()
		{
			string s = "nanoFramework";
			char[] c = s.ToCharArray(4, 5);
			Assert.IsNotNull(c);
			Assert.AreEqual(5, c.Length);
			Assert.AreEqual('a', c[2]);
		}

		[TestMethod]
		public void ToLower()
		{
			string s = "nanoCLR!";
			Assert.AreEqual("nanoclr!", s.ToLower());
		}

		[TestMethod]
		public void ToString1()
		{
			string s = "nanoCLR!";
			Assert.AreEqual("nanoCLR!", s.ToString());
		}

		[TestMethod]
		public void ToUpper()
		{
			string s = "nanoCLR!";
			Assert.AreEqual("NANOCLR!", s.ToUpper());
		}

		[TestMethod]
		public void Trim1()
		{
			string s = "   ***nanoCLR!   ";
			Assert.AreEqual("***nanoCLR!", s.Trim());
		}

		[TestMethod]
		public void Trim2()
		{
			string s = "   ***nanoCLR!   ";
			Assert.AreEqual("***nanoCLR!", s.Trim(null));
			Assert.AreEqual("nanoCLR", s.Trim(' ', '*', '!'));
		}

		[TestMethod]
		public void TrimEnd()
		{
			string s = "   ***nanoCLR!   ";
			Assert.AreEqual("   ***nanoCLR!", s.TrimEnd(null));
			Assert.AreEqual("   ***nanoCLR", s.TrimEnd(' ', '*', '!'));
		}

		[TestMethod]
		public void TrimStart()
		{
			string s = "   ***nanoCLR!   ";
			Assert.AreEqual("***nanoCLR!   ", s.TrimStart(null));
			Assert.AreEqual("nanoCLR!   ", s.TrimStart(' ', '*', '!'));
		}

		[TestMethod]
		public void Indexer()
		{
			string s = "   ***nanoCLR!   ";
			Assert.AreEqual('C', s[10]);
		}

		[TestMethod]
		public void Lenght()
		{
			string s = "   ***nanoCLR!   ";
			Assert.AreEqual(17, s.Length);
		}

		[TestMethod]
		public void Empty()
		{
			Assert.AreEqual(0, string.Empty.Length);
			Assert.AreEqual("", string.Empty);
		}

		[TestMethod]
		public void OperatorEqual()
		{
			Assert.IsFalse("nanoFramework" == "nanoCLR");
			Assert.IsTrue("nanoFramework" == "nanoFramework");
		}

		[TestMethod]
		public void OperatorNotEqual()
		{
			Assert.IsTrue("nanoFramework" != "nanoCLR");
			Assert.IsFalse("nanoFramework" != "nanoFramework");
		}
	}
}
