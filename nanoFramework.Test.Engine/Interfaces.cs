//
// Copyright (c) 2018 The nanoFramework project contributors
// Portions Copyright (c) https://github.com/4egod/TinyCLRTest All rights reserved.
// See LICENSE file in the project root for full license information.
//

namespace nanoFramework.Test.Engine
{
	/// <summary>
	/// Each test class must implement this interface; that's the "marker" that the class is a test class
	/// </summary>
	public interface ITestClass
	{
	}

	/// <summary>
	/// One test class per assembly can implement this interface to run code in the AssemblyInitialize method.
	/// </summary>
	public interface IAssemblyInitialize
	{
		/// <summary>
		/// That code will be executed before any test of this assembly will be executed.
		/// </summary>
		void AssemblyInitialize();
	}

	/// <summary>
	/// One test class per assembly can implement this interface to run code in the AssemblyCleanup method.
	/// </summary>
	public interface IAssemblyCleanup
	{
		/// <summary>
		/// That code will be executed after all tests of this assembly has been executed.
		/// </summary>
		void AssemblyCleanup();
	}

	/// <summary>
	/// Each test class can implement this interface to run code in the ClassInitialize method.
	/// </summary>
	public interface IClassInitialize
	{
		/// <summary>
		/// That code will be executed before any test of this test class will be executed.
		/// </summary>
		void ClassInitialize();
	}

	/// <summary>
	/// Each test class can implement this interface to run code in the ClassCleanup method.
	/// </summary>
	public interface IClassCleanup
	{
		/// <summary>
		/// That code will be executed after all tests of this test class has been executed.
		/// </summary>
		void ClassCleanup();
	}

	/// <summary>
	/// Each test class can implement this interface to run code in the TestInitialize method.
	/// </summary>
	public interface ITestInitialize
	{
		/// <summary>
		/// That code will be executed before each test of this test class will be executed.
		/// </summary>
		void TestInitialize();
	}

	/// <summary>
	/// Each test class can implement this interface to run code in the TestCleanup method.
	/// </summary>
	public interface ITestCleanup
	{
		/// <summary>
		/// That code will be executed after each test of this test class has been executed.
		/// </summary>
		void TestCleanup();
	}
}
