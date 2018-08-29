//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System.Collections;

namespace nanoFramework.CoreLibrary.Tests.SystemTests.CollectionsTests
{
	public class DictionaryEntryTests : ITestClass
	{
		public void Constructor()
		{
			DictionaryEntry e = new DictionaryEntry(1, "CLR");
			Assert.AreEqual(1, e.Key);
			Assert.AreEqual("CLR", e.Value);
		}
	}
}
