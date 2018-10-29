//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using System;

namespace nanoFramework.Test.Engine
{
	/// <summary>
	///   Each test class should have this attribute; that's the "marker" that the class is a test class
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class TestClassAttribute : Attribute
	{
	}

	/// <summary>
	///   Each test method should have this attribute; that's the "marker" that the method is a test method
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class TestMethodAttribute : Attribute
	{
	}

	/// <summary>
	///   If a single test or all tests of a test class should be ignored; use this attribute and
	///   optionally give a reason for ignoring the test
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public sealed class IgnoreAttribute : Attribute
	{
		public IgnoreAttribute() : this(null)
		{
		}

		public IgnoreAttribute(string ignoreMessage)
		{
			IgnoreMessage = ignoreMessage;
		}

		public string IgnoreMessage { get; }
	}

	/// <summary>
	///   One method of one test class per assembly can have this attribute to run per assembly init code.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class AssemblyInitializeAttribute : Attribute
	{
	}

	/// <summary>
	///   One method of one test class per assembly can have this attribute to run per assembly cleanup code.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class AssemblyCleanupAttribute : Attribute
	{
	}

	/// <summary>
	///   Each test class can have one method that is marked with this attribute to run per class init code.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class ClassInitializeAttribute : Attribute
	{
	}

	/// <summary>
	///   Each test class can have one method that is marked with this attribute to run per class cleanup code.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class ClassCleanupAttribute : Attribute
	{
	}

	/// <summary>
	///   Each test class can have one method that is marked with this attribute to run per test init code.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class TestInitializeAttribute : Attribute
	{
	}

	/// <summary>
	///   Each test class can have one method that is marked with this attribute to run per test cleanup code.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class TestCleanupAttribute : Attribute
	{
	}
}