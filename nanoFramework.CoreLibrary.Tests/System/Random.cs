//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;

namespace nanoFramework.CoreLibrary.Tests.SystemTests
{
	public class RandomTests : ITestClass
	{
		public void Constructor()
		{
			Random r = new Random(4711);
			Assert.IsNotNull(r);
		}

		public void Next1()
		{
			Random r = new Random(4711);
			Assert.IsTrue(r.Next() >= 0);
		}

		public void Next2()
		{
			Random r = new Random(4711);
			int n = r.Next(815);
			Assert.IsTrue(n >= 0 && n < 815);
		}

		public void NextBytes()
		{
			Random r = new Random(4711);
			byte[] x = new byte[10];
			r.NextBytes(x);
			Assert.IsTrue((x[0] + x[1] + x[2] + x[3] + x[4] + x[5] + x[6] + x[7] + x[8] + x[9]) > 0);
		}

		public void NextDouble()
		{
			Random r = new Random(4711);
			double d = r.NextDouble();
			Assert.IsTrue(d >= 0.0 && d < 1.0);
		}
	}
}
