//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;

namespace nanoFramework.CoreLibrary.Tests.System
{
	public class String : ITestClass
	{
		public void Constructor1()
		{
			string s = new string(new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' });
			Assert.AreEqual("nanoFramework", s);
		}

		public void Constructor2()
		{
			string s = new string('x', 42);
			Assert.AreEqual("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", s);
		}

		public void Constructor3()
		{
			string s = new string(new char[] { 'n', 'a', 'n', 'o', 'F', 'r', 'a', 'm', 'e', 'w', 'o', 'r', 'k' }, 2, 7);
			Assert.AreEqual("noFrame", s);
		}

		public void Compare()
		{
			Assert.IsTrue(string.Compare("nanoFramework", "nanoCLR") > 0);
			Assert.IsTrue(string.Compare("nanoCLR", "nanoFramework") < 0);
			Assert.IsTrue(string.Compare("nanoFramework", "nanoFramework") == 0);
		}

		public void CompareTo1()
		{
			string s = "nanoFramework";
			Assert.IsTrue(s.CompareTo("nanoCLR") > 0);
			Assert.IsTrue(s.CompareTo("other") < 0);
			Assert.IsTrue(s.CompareTo("nanoFramework") == 0);
		}

		public void CompareTo2()
		{
			string s = "nanoFramework";
			Assert.IsTrue(s.CompareTo((object)"nanoCLR") > 0);
			Assert.IsTrue(s.CompareTo((object)"other") < 0);
			Assert.IsTrue(s.CompareTo((object)"nanoFramework") == 0);
		}

		public void Concat1()
		{
			Assert.AreEqual("nanoFramework", string.Concat((object)"nanoFramework"));
		}

		public void Concat2()
		{
			Assert.AreEqual("nanoFramework", string.Concat((object)"nano", (object)"Framework"));
		}

		public void Concat3()
		{
			Assert.AreEqual("nanoFramework", string.Concat((object)"nano", (object)"Frame", (object)"work"));
		}

		public void Concat4()
		{
			Assert.AreEqual("nanoFramework", string.Concat((object)"na", (object)"no", (object)"Frame", (object)"work"));
		}

		public void Concat5()
		{
			Assert.AreEqual("nanoFrameworkTest", string.Concat("nanoFramework", "Test"));
		}

		public void Concat6()
		{
			Assert.AreEqual("nanoFrameworkTest", string.Concat("nano", "Framework", "Test"));
		}

		public void Concat7()
		{
			Assert.AreEqual("nanoFrameworkTest", string.Concat("nano", "Frame", "work", "Test"));
		}

		public void Concat8()
		{
			Assert.AreEqual("nanoFrameworkTest", string.Concat("na", "no", "Frame", "work", "Test"));
		}

		public void Format()
		{
			Assert.AreEqual("nanoFramework tests are running!", string.Format("nanoFramework {0} are {1}!", "tests", "running"));
		}

		public void IndexOf1()
		{
			string s = "nanoFramework tests are running!";
			Assert.AreEqual(14, s.IndexOf('t'));
			Assert.AreEqual(-1, s.IndexOf('x'));
		}

		public void IndexOf2()
		{
			string s = "nanoFramework tests are running!";
			Assert.AreEqual(15, s.IndexOf("est"));
			Assert.AreEqual(-1, s.IndexOf("micro"));
		}

		public void IndexOf3()
		{
			string s = "nanoFramework tests are running!";
			Assert.AreEqual(17, s.IndexOf('t', 15));
			Assert.AreEqual(-1, s.IndexOf('t', 19));
		}

		public void IndexOf4()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(35, s.IndexOf("nano", 10));
			Assert.AreEqual(-1, s.IndexOf("nano", 36));
			Assert.AreEqual(-1, s.IndexOf("nano", 42));
		}

		public void IndexOf5()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(39, s.IndexOf('C', 30, 10));
			Assert.AreEqual(-1, s.IndexOf('C', 25, 10));
			Assert.AreEqual(-1, s.IndexOf('C', 40, 3));
		}

		public void IndexOf6()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(35, s.IndexOf("nano", 30, 10));
			Assert.AreEqual(-1, s.IndexOf("nano", 25, 10));
			Assert.AreEqual(-1, s.IndexOf("nano", 40, 3));
		}

		public void IndexOfAny1()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(8, s.IndexOfAny(new char[] { 'e', 's', 't', 'l' }));
			Assert.AreEqual(-1, s.IndexOfAny(new char[] { 'b', 'c', 'd', 'f', 'h', 'j' }));
		}

		public void IndexOfAny2()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(11, s.IndexOfAny(new char[] { 'e', 'r', 'n', 's', 't', 'l' }, 10));
			Assert.AreEqual(-1, s.IndexOfAny(new char[] { 'e', 'r', 'n', 's', 't', 'l' }, 38));
			Assert.AreEqual(-1, s.IndexOfAny(new char[] { 'e', 'r', 'n', 's', 't', 'l' }, 42));
		}

		public void IndexOfAny3()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(14, s.IndexOfAny(new char[] { 'e', 'n', 's', 't', 'l' }, 9, 10));
			Assert.AreEqual(-1, s.IndexOfAny(new char[] { 'e', 'n', 's', 't', 'l' }, 9, 4));
			Assert.AreEqual(-1, s.IndexOfAny(new char[] { 'e', 'n', 's', 't', 'l' }, 40, 3));
		}

		public void Intern()
		{
			Assert.AreEqual("nanoFramework tests", string.Intern("nanoFramework tests"));
		}

		public void IsInterned()
		{
			Assert.IsNotNull(string.IsInterned("aramsamsam"));
		}

		public void LastIndexOf1()
		{
			string s = "nanoFramework tests are running!";
			Assert.AreEqual(17, s.LastIndexOf('t'));
			Assert.AreEqual(-1, s.LastIndexOf('x'));
		}

		public void LastIndexOf2()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(35, s.LastIndexOf("nano"));
			Assert.AreEqual(-1, s.LastIndexOf("micro"));
		}

		public void LastIndexOf3()
		{
			string s = "nanoFramework tests are running!";
			Assert.AreEqual(14, s.LastIndexOf('t', 15));
			Assert.AreEqual(-1, s.LastIndexOf('t', 13));
		}

		public void LastIndexOf4()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(0, s.LastIndexOf("nano", 10));
			Assert.AreEqual(35, s.LastIndexOf("nano", 40));
			Assert.AreEqual(0, s.LastIndexOf("nano", 3));
			Assert.AreEqual(-1, s.LastIndexOf("nano", 2));
			Assert.AreEqual(-1, s.LastIndexOf("are", 10));
		}

		public void LastIndexOf5()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(2, s.LastIndexOf('n', 20, 20));
			Assert.AreEqual(37, s.LastIndexOf('n', 40, 10));
			Assert.AreEqual(-1, s.LastIndexOf('w', 5, 3));
		}

		public void LastIndexOf6()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(35, s.LastIndexOf("nano", 40, 10));
			Assert.AreEqual(-1, s.LastIndexOf("nano", 25, 10));
			Assert.AreEqual(-1, s.LastIndexOf("nano", 3, 3));
		}

		public void LastIndexOfAny1()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(22, s.LastIndexOfAny(new char[] { 'e', 's', 't', 'l' }));
			Assert.AreEqual(-1, s.LastIndexOfAny(new char[] { 'b', 'c', 'd', 'f', 'h', 'j' }));
		}

		public void LastIndexOfAny2()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(8, s.LastIndexOfAny(new char[] { 'e', 'r', 'n', 's', 't', 'l' }, 10));
			Assert.AreEqual(37, s.LastIndexOfAny(new char[] { 'e', 'r', 'n', 's', 't', 'l' }, 38));
			Assert.AreEqual(-1, s.LastIndexOfAny(new char[] { 'e', 'r', 's', 't', 'l' }, 4));
		}

		public void LastIndexOfAny3()
		{
			string s = "nanoFramework tests are running on nanoCLR!";
			Assert.AreEqual(18, s.LastIndexOfAny(new char[] { 'e', 'n', 's', 't', 'l' }, 20, 10));
			Assert.AreEqual(-1, s.LastIndexOfAny(new char[] { 'e', 'n', 's', 't', 'l' }, 12, 4));
			Assert.AreEqual(-1, s.LastIndexOfAny(new char[] { 'e', 's', 't', 'l' }, 5, 3));
		}

		public void PadLeft()
		{
			string s = "nanoFramework";
			Assert.AreEqual("*******nanoFramework", s.PadLeft(20, '*'));
		}

		public void PadRight()
		{
			string s = "nanoFramework";
			Assert.AreEqual("nanoFramework*******", s.PadRight(20, '*'));
		}

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

		public void Substring1()
		{
			string s = "nanoFramework tests: are running on nanoCLR!";
			Assert.AreEqual("running on nanoCLR!", s.Substring(25));
		}

		public void Substring2()
		{
			string s = "nanoFramework tests: are running on nanoCLR!";
			Assert.AreEqual("running on", s.Substring(25, 10));
		}

		public void ToCharArray1()
		{
			string s = "nanoFramework";
			char[] c = s.ToCharArray();
			Assert.IsNotNull(c);
			Assert.AreEqual(13, c.Length);
			Assert.AreEqual('F', c[4]);
		}

		public void ToCharArray2()
		{
			string s = "nanoFramework";
			char[] c = s.ToCharArray(4, 5);
			Assert.IsNotNull(c);
			Assert.AreEqual(5, c.Length);
			Assert.AreEqual('a', c[2]);
		}

		public void ToLower()
		{
			string s = "nanoCLR!";
			Assert.AreEqual("nanoclr!", s.ToLower());
		}

		public void ToString1()
		{
			string s = "nanoCLR!";
			Assert.AreEqual("nanoCLR!", s.ToString());
		}

		public void ToUpper()
		{
			string s = "nanoCLR!";
			Assert.AreEqual("NANOCLR!", s.ToUpper());
		}

		public void Trim1()
		{
			string s = "   ***nanoCLR!   ";
			Assert.AreEqual("***nanoCLR!", s.Trim());
		}

		public void Trim2()
		{
			string s = "   ***nanoCLR!   ";
			Assert.AreEqual("***nanoCLR!", s.Trim(null));
			Assert.AreEqual("nanoCLR", s.Trim(' ', '*', '!'));
		}

		public void TrimEnd()
		{
			string s = "   ***nanoCLR!   ";
			Assert.AreEqual("   ***nanoCLR!", s.TrimEnd(null));
			Assert.AreEqual("   ***nanoCLR", s.TrimEnd(' ', '*', '!'));
		}

		public void TrimStart()
		{
			string s = "   ***nanoCLR!   ";
			Assert.AreEqual("***nanoCLR!   ", s.TrimStart(null));
			Assert.AreEqual("nanoCLR!   ", s.TrimStart(' ', '*', '!'));
		}

		public void Indexer()
		{
			string s = "   ***nanoCLR!   ";
			Assert.AreEqual('C', s[10]);
		}

		public void Lenght()
		{
			string s = "   ***nanoCLR!   ";
			Assert.AreEqual(17, s.Length);
		}

		public void Empty()
		{
			Assert.AreEqual(0, string.Empty.Length);
			Assert.AreEqual("", string.Empty);
		}

		public void OperatorEqual()
		{
			Assert.IsFalse("nanoFramework" == "nanoCLR");
			Assert.IsTrue("nanoFramework" == "nanoFramework");
		}

		public void OperatorNotEqual()
		{
			Assert.IsTrue("nanoFramework" != "nanoCLR");
			Assert.IsFalse("nanoFramework" != "nanoFramework");
		}
	}
}
