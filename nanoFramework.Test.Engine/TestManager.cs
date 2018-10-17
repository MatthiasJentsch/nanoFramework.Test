//
// Copyright (c) 2018 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Collections;
using System.Reflection;
using System.Threading;

namespace nanoFramework.Test.Engine
{
	/// <summary>
	///     The TestManager executes all tests that are within an assembly or all loaded assemblies
	/// </summary>
	/// <remarks>
	///     Via reflection all public classes with a parameterless constructor will be examined. Only classes that
	///     are marked with the attribute <see cref="T:nanoFramework.Test.Engine.TestClassAttribute" /> will be considered as
	///     classes that contain tests.
	///     All classes that doesn't have the attribute <see cref="T:nanoFramework.Test.Engine.TestClassAttribute" /> will be
	///     ignored.
	///     It's possible to run code before and after test execution at certain levels:
	///     - Mark a method within a test class with <see cref="T:nanoFramework.Test.Engine.AssemblyInitializeAttribute" />
	///     to run assembly level init code. That code will be executed before any test of this assembly will be executed.
	///     - Mark a method within a test class with <see cref="T:nanoFramework.Test.Engine.AssemblyCleanupAttribute" />
	///     to run assembly level cleanup code. That code will be executed after all tests of this assembly has been executed.
	///     - Mark a method within a test class with <see cref="T:nanoFramework.Test.Engine.ClassInitializeAttribute" />
	///     to run class level init code. That code will be executed before any test of this test class will be executed.
	///     - Mark a method within a test class with <see cref="T:nanoFramework.Test.Engine.ClassCleanupAttribute" />
	///     to run class level cleanup code. That code will be executed after all tests of this test class has been executed.
	///     - Mark a method within a test class with <see cref="T:nanoFramework.Test.Engine.TestInitializeAttribute" />
	///     to run test level init code. That code will be executed before each test of this test class will be executed.
	///     - Mark a method within a test class with <see cref="T:nanoFramework.Test.Engine.TestCleanupAttribute" />
	///     to run test level cleanup code. That code will be executed after each test of this test class has been executed.
	///     All other public parameterless instance methods that are merked with the
	///     <see cref="T:nanoFramework.Test.Engine.TestMethodAttribute" />
	///     will be treated as test methods. The methods shouldn't have a return parameter. The return parameter will be
	///     ignored. The test methods are executed
	///     in the order in witch they are found in the class type. No particular execution order is guarantied. For each test
	///     method
	///     the possible exception will be caught and recorded. If the method executes without an exception the execution time
	///     will
	///     be recorded. After each test the result will be posted via Console.WriteLine.
	/// </remarks>
	public static class TestManager
	{
		#region public methods

		/// <summary>
		///     Runs all tests from all loaded assemblies form the current app domain
		/// </summary>
		/// <exception cref="System.NotImplementedException">If the target doesn't support AppDomains!</exception>
		public static void RunTests()
		{
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) RunTests(assembly);
		}

		/// <summary>
		///     Runs all tests that are in an assembly
		/// </summary>
		/// <param name="assembly">The test assembly</param>
		public static void RunTests(Assembly assembly)
		{
			if (assembly == null) throw new ArgumentNullException("assembly");
			var assemblyName = assembly.FullName;

			// Get all test class types
			var testClassTypes = GetTestClassTypes(assembly);
			// No test classes in this assembly?
			if (testClassTypes.Count == 0) return;

			// wait 5 seconds to lets the listener establish the connection before outputting anything
			Console.Write("5.");
			Thread.Sleep(1000);
			Console.Write("4.");
			Thread.Sleep(1000);
			Console.Write("3.");
			Thread.Sleep(1000);
			Console.Write("2.");
			Thread.Sleep(1000);
			Console.Write("1.");
			Thread.Sleep(1000);
			Console.WriteLine(" - GO!");

			// iterate through all test class types and find the types that have the [AssemblyInitialize]/[IAssemblyCleanup] attributes
			Type assemblyInitializeType = null;
			Type assemblyCleanupType = null;
			foreach (Type entry in testClassTypes)
			{
				if (GetMethodsWithAttribute(entry, typeof(AssemblyInitializeAttribute)) != null)
				{
					// warning if already found in another class
					if (assemblyInitializeType != null)
						PrintHelper.PrintMessage(string.Concat(assemblyName, " : More than one class has the ",
							typeof(AssemblyInitializeAttribute).Name));
					// store/overwrite the type for AssemblyInitializeAttribute
					assemblyInitializeType = entry;
				}

				if (GetMethodsWithAttribute(entry, typeof(AssemblyCleanupAttribute)) != null)
				{
					// warning if already found in another class
					if (assemblyCleanupType != null)
						PrintHelper.PrintMessage(string.Concat(assemblyName, " : More than one class has the ",
							typeof(AssemblyCleanupAttribute).Name));
					// store/overwrite the type for AssemblyCleanupAttribute
					assemblyCleanupType = entry;
				}
			}

			// print the start message
			PrintHelper.PrintMessage(string.Concat(assemblyName, " : Tests started (", TimeSpan.TicksPerMillisecond,
				" ticks = 1 millisecond)"));

			// execute the AssemblyInitialize method
			if (assemblyInitializeType != null)
			{
				var assemblyInitializeMethod =
					(MethodInfo) GetMethodsWithAttribute(assemblyCleanupType, typeof(AssemblyCleanupAttribute))[0];
				PrintHelper.PrintMessage(string.Concat(assemblyName, " : Calling ", assemblyInitializeMethod.Name,
					" for assembly initialize"));
				var testClassInstance = assemblyInitializeType.GetConstructor(new Type[] { }).Invoke(new object[] { });
				assemblyInitializeMethod.Invoke(testClassInstance, new object[] { });
			}

			// iterate through all test class types and execute the tests
			foreach (Type entry in testClassTypes)
			{
				RunTests(entry);
				// Wait a second to let all the output flow to the listening host
				Thread.Sleep(1000);
			}

			// execute the AssemblyCleanup method
			if (assemblyCleanupType != null)
			{
				var assemblyCleanupMethod =
					(MethodInfo) GetMethodsWithAttribute(assemblyCleanupType, typeof(AssemblyCleanupAttribute))[0];
				PrintHelper.PrintMessage(string.Concat(assemblyName, " : Calling ", assemblyCleanupMethod.Name,
					" for assembly cleanup"));
				var testClassInstance = assemblyCleanupType.GetConstructor(new Type[] { }).Invoke(new object[] { });
				assemblyCleanupMethod.Invoke(testClassInstance, new object[] { });
			}

			// print the end message
			PrintHelper.PrintMessage(string.Concat(assemblyName, " : Tests finished"));
		}

		/// <summary>
		///     Gets a List which contains all test class types.
		/// </summary>
		/// <param name="assembly">The test assembly</param>
		/// <returns>List of test types</returns>
		public static ArrayList GetTestClassTypes(Assembly assembly)
		{
			// examine all types
			var testClassTypes = new ArrayList();
			var types = assembly.GetTypes();
			foreach (var type in types)
			{
				// skip all types that are not a public class
				if (!type.IsClass || !type.IsPublic) continue;

				// skip if the class has no a public parameterless constructor
				var constructor = type.GetConstructor(new Type[] { });
				if (constructor == null) continue;

				// Test if this class has the [TestClass] attribute
				foreach (var attribute in type.GetCustomAttributes(false))
					if (typeof(TestClassAttribute).Equals(attribute))
					{
						// found! => add to the result
						testClassTypes.Add(type);
						break;
					}
			}

			return testClassTypes;
		}

		/// <summary>
		///     Gets all methods that are marked with teh given attributeType
		/// </summary>
		/// <param name="testClassType">Class type</param>
		/// <param name="attributeType">Attribute type</param>
		/// <returns>MethodInfo's as List</returns>
		public static ArrayList GetMethodsWithAttribute(Type testClassType, Type attributeType)
		{
			var result = new ArrayList();

			// examine all public instance methods that are declared directly in this class (no inheritance)
			var methods =
				testClassType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
			foreach (var method in methods)
				// interate thru all attributes
			foreach (var attribute in method.GetCustomAttributes(false))
				// is it the desired attriube?
				if (attributeType.Equals(attribute))
					result.Add(method);

			return result.Count > 0 ? result : null;
		}

		#endregion

		#region private methods

		/// <summary>
		///     Run all tests from one test class type
		/// </summary>
		/// <param name="testClassType">The test class type</param>
		private static void RunTests(Type testClassType)
		{
			// create the instance and cast to the interfaces that has the initialize/cleanup methods
			var testClassInstance = testClassType.GetConstructor(new Type[] { }).Invoke(new object[] { });

			// Find the test methods
			var testMethods = GetMethodsWithAttribute(testClassType, typeof(TestMethodAttribute));

			// Execute "ClassInitialize" before all tests
			var classInitialize = GetMethodsWithAttribute(testClassType, typeof(ClassInitializeAttribute));
			if (classInitialize != null)
			{
				var classInitializeMethod = (MethodInfo) classInitialize[0];
				PrintHelper.PrintMessage(string.Concat(testClassType.FullName, " : Calling ",
					classInitializeMethod.Name, " for class initialize"));
				classInitializeMethod.Invoke(testClassInstance, new object[] { });
			}

			// Execute all test methods
			var testInitialize = GetMethodsWithAttribute(testClassType, typeof(TestInitializeAttribute));
			var testInitializeMethod = testInitialize != null ? (MethodInfo) testInitialize[0] : null;
			var testCleanup = GetMethodsWithAttribute(testClassType, typeof(TestCleanupAttribute));
			var testCleanupMethod = testCleanup != null ? (MethodInfo) testCleanup[0] : null;
			foreach (MethodInfo testMethod in testMethods)
			{
				// Execute "TestInitialize" before each test
				if (testInitializeMethod != null)
				{
					PrintHelper.PrintMessage(string.Concat(testClassType.FullName, ".", testMethod.Name, " : Calling ",
						testInitializeMethod.Name, " for test initialize"));
					testInitializeMethod.Invoke(testClassInstance, new object[] { });
				}

				var startTicks = DateTime.UtcNow.Ticks;
				TestStatus status;
				Exception exception;
				ExecuteTestMethod(testMethod, testClassInstance, out status, out exception);
				var stopTicks = DateTime.UtcNow.Ticks;
				// send the result to the debugger
				PrintHelper.PrintTestResult(testClassType.FullName, testMethod.Name, status, exception,
					stopTicks - startTicks);

				// Execute "TestCleanup" after each test
				if (testCleanupMethod != null)
				{
					PrintHelper.PrintMessage(string.Concat(testClassType.FullName, ".", testMethod.Name, " : Calling ",
						testCleanupMethod.Name, " for test cleanup"));
					testCleanupMethod.Invoke(testClassInstance, new object[] { });
				}
			}

			// Execute "ClassCleanup" after all tests
			var classCleanup = GetMethodsWithAttribute(testClassType, typeof(ClassCleanupAttribute));
			if (classCleanup != null)
			{
				var classCleanupMethod = (MethodInfo) classInitialize[0];
				PrintHelper.PrintMessage(string.Concat(testClassType.FullName, " : Calling ", classCleanupMethod.Name,
					" for class cleanup"));
				classCleanupMethod.Invoke(testClassInstance, new object[] { });
			}
		}

		/// <summary>
		///     Executes one test method
		/// </summary>
		/// <param name="method">The parameterless method</param>
		/// <param name="instance">The test class instance</param>
		/// <param name="testStatus">The test result</param>
		/// <param name="testException">
		///     The exception that the test method has thrown or null if the test has passed without a
		///     failure
		/// </param>
		private static void ExecuteTestMethod(MethodInfo method, object instance, out TestStatus testStatus,
			out Exception testException)
		{
			try
			{
				method.Invoke(instance, null);
				testStatus = TestStatus.Passed;
				testException = null;
			}
			catch (Exception exception)
			{
				if (exception.InnerException != null) exception = exception.InnerException;
				testStatus = TestStatus.Failed;
				testException = exception;
			}
		}

		#endregion
	}
}