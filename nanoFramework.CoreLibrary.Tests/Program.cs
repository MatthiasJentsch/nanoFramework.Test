//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Test.Engine;
using System;
using System.Reflection;

namespace nanoFramework.CoreLibrary.Tests
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				TestManager.RunTests(Assembly.GetExecutingAssembly());
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
			}
		}
	}
}
