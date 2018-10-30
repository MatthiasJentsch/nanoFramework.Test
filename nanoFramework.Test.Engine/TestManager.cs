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
	///   The TestManager executes all tests that are within an assembly or all loaded assemblies
	/// </summary>
	/// <remarks>
	///   Via reflection all public classes with a parameterless constructor will be examined. Only classes that
	///   are marked with the attribute <see cref="T:nanoFramework.Test.Engine.TestClassAttribute" /> will be considered as
	///   classes that contain tests.
	///   All classes that doesn't have the attribute <see cref="T:nanoFramework.Test.Engine.TestClassAttribute" /> will be
	///   ignored.
	///   It's possible to run code before and after test execution at certain levels:
	///   - Mark a method within a test class with <see cref="T:nanoFramework.Test.Engine.AssemblyInitializeAttribute" />
	///   to run assembly level init code. That code will be executed before any test of this assembly will be executed.
	///   - Mark a method within a test class with <see cref="T:nanoFramework.Test.Engine.AssemblyCleanupAttribute" />
	///   to run assembly level cleanup code. That code will be executed after all tests of this assembly has been executed.
	///   - Mark a method within a test class with <see cref="T:nanoFramework.Test.Engine.ClassInitializeAttribute" />
	///   to run class level init code. That code will be executed before any test of this test class will be executed.
	///   - Mark a method within a test class with <see cref="T:nanoFramework.Test.Engine.ClassCleanupAttribute" />
	///   to run class level cleanup code. That code will be executed after all tests of this test class has been executed.
	///   - Mark a method within a test class with <see cref="T:nanoFramework.Test.Engine.TestInitializeAttribute" />
	///   to run test level init code. That code will be executed before each test of this test class will be executed.
	///   - Mark a method within a test class with <see cref="T:nanoFramework.Test.Engine.TestCleanupAttribute" />
	///   to run test level cleanup code. That code will be executed after each test of this test class has been executed.
	///   All other public parameter less instance methods that are merked with the
	///   <see cref="T:nanoFramework.Test.Engine.TestMethodAttribute" />
	///   will be treated as test methods. The methods shouldn't have a return parameter. The return parameter will be
	///   ignored. The test methods are executed
	///   in the order in witch they are found in the class type. No particular execution order is guarantied. For each test
	///   method
	///   the possible exception will be caught and recorded. If the method executes without an exception the execution time
	///   will
	///   be recorded. After each test the result will be posted via Console.WriteLine.
	/// </remarks>
	public static class TestManager
	{
		#region public methods

		/// <summary>
		///   Runs all tests from all loaded assemblies form the current app domain
		/// </summary>
		/// <exception cref="System.NotImplementedException">If the target doesn't support AppDomains!</exception>
		public static void RunTests()
		{
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				RunTests(assembly);
			}
		}

		/// <summary>
		///   Runs all tests that are in an assembly
		/// </summary>
		/// <param name="assembly">The test assembly</param>
		public static void RunTests(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException(nameof(assembly));
			}

			string assemblyName = assembly.FullName;

			// Get all test class types
			ArrayList testClassTypes = GetTestClassTypes(assembly);
			// No test classes in this assembly?
			if (testClassTypes.Count == 0)
			{
				return;
			}

			// wait 5 seconds to lets the listener establish the connection before outputting anything
			Console.WriteLine("5.....");
			Thread.Sleep(1000);
			Console.WriteLine("4....");
			Thread.Sleep(1000);
			Console.WriteLine("3...");
			Thread.Sleep(1000);
			Console.WriteLine("2..");
			Thread.Sleep(1000);
			Console.WriteLine("1.");
			Thread.Sleep(1000);
			Console.WriteLine("GO!");

			// iterate through all test class types and find the types that have the [AssemblyInitialize]/[IAssemblyCleanup] attributes
			Type assemblyInitializeType = null;
			Type assemblyCleanupType = null;
			foreach (Type entry in testClassTypes)
			{
				if (GetMethodsWithAttribute(entry, typeof(AssemblyInitializeAttribute)) != null)
				{
					// warning if already found in another class
					if (assemblyInitializeType != null)
					{
						PrintHelper.PrintMessage(string.Concat(assemblyName, " : More than one class has the ", typeof(AssemblyInitializeAttribute).Name));
					}

					// store/overwrite the type for AssemblyInitializeAttribute
					assemblyInitializeType = entry;
				}

				if (GetMethodsWithAttribute(entry, typeof(AssemblyCleanupAttribute)) != null)
				{
					// warning if already found in another class
					if (assemblyCleanupType != null)
					{
						PrintHelper.PrintMessage(string.Concat(assemblyName, " : More than one class has the ", typeof(AssemblyCleanupAttribute).Name));
					}

					// store/overwrite the type for AssemblyCleanupAttribute
					assemblyCleanupType = entry;
				}
			}

			// print the start message
			PrintHelper.PrintMessage(string.Concat(assemblyName, " : Tests started (", TimeSpan.TicksPerMillisecond, " ticks = 1 millisecond)"));

			// execute the AssemblyInitialize method
			if (assemblyInitializeType != null)
			{
				MethodInfo assemblyInitializeMethod = (MethodInfo) GetMethodsWithAttribute(assemblyCleanupType, typeof(AssemblyCleanupAttribute))[0];
				PrintHelper.PrintMessage(string.Concat(assemblyName, " : Calling ", assemblyInitializeMethod.Name, " for assembly initialize"));
				object testClassInstance = assemblyInitializeType.GetConstructor(new Type[] { })?.Invoke(new object[] { });
				if (testClassInstance != null)
				{
					assemblyInitializeMethod.Invoke(testClassInstance, new object[] { });
				}
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
				MethodInfo assemblyCleanupMethod = (MethodInfo) GetMethodsWithAttribute(assemblyCleanupType, typeof(AssemblyCleanupAttribute))[0];
				PrintHelper.PrintMessage(string.Concat(assemblyName, " : Calling ", assemblyCleanupMethod.Name, " for assembly cleanup"));
				object testClassInstance = assemblyCleanupType.GetConstructor(new Type[] { })?.Invoke(new object[] { });
				if (testClassInstance != null)
				{
					assemblyCleanupMethod.Invoke(testClassInstance, new object[] { });
				}
			}

			// print the end message
			PrintHelper.PrintMessage(string.Concat(assemblyName, " : Tests finished"));
		}

		/// <summary>
		///   Gets a List which contains all test class types.
		/// </summary>
		/// <param name="assembly">The test assembly</param>
		/// <returns>List of test types</returns>
		public static ArrayList GetTestClassTypes(Assembly assembly)
		{
			// examine all types
			ArrayList testClassTypes = new ArrayList();
			Type[] types = assembly.GetTypes();
			foreach (Type type in types)
			{
				// skip all types that are not a public class
				if (!type.IsClass || !type.IsPublic)
				{
					continue;
				}

				// skip if the class has no a public parameter less constructor
				ConstructorInfo constructor = type.GetConstructor(new Type[] { });
				if (constructor == null)
				{
					continue;
				}

				// Test if this class has the [TestClass] attribute
				foreach (object attribute in type.GetCustomAttributes(false))
				{
					// Check by type equality or type name equality!
					if (typeof(TestClassAttribute).Equals(attribute) || typeof(TestClassAttribute).Name == attribute.GetType().Name)
					{
						// found! => add to the result
						testClassTypes.Add(type);
						break;
					}
				}
			}

			return testClassTypes;
		}

		/// <summary>
		///   Gets all methods that are marked with teh given attributeType
		/// </summary>
		/// <param name="testClassType">Class type</param>
		/// <param name="attributeType">Attribute type</param>
		/// <returns>MethodInfo's as List</returns>
		public static ArrayList GetMethodsWithAttribute(Type testClassType, Type attributeType)
		{
			ArrayList result = new ArrayList();

			// examine all public instance methods that are declared directly in this class (no inheritance)
			// and iterate thru all attributes
			foreach (MethodInfo method in testClassType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
			{
				foreach (object attribute in method.GetCustomAttributes(false))
				{
					// is it the desired attribute? Check by type equality or type name equality!
					if (attributeType.Equals(attribute) || attributeType.Name == attribute.GetType().Name)
					{
						result.Add(method);
					}
				}
			}

			return result.Count > 0 ? result : null;
		}

		#endregion

		#region private methods

		/// <summary>
		///   Run all tests from one test class type
		/// </summary>
		/// <param name="testClassType">The test class type</param>
		private static void RunTests(Type testClassType)
		{
			// should the class be ignored?
			if (HasIgnoreAttribute(testClassType))
			{
				PrintHelper.PrintMessage(string.Concat(testClassType.FullName, " : Ignored"));
				return;
			}

			// create the instance and cast to the interfaces that has the initialize/cleanup methods
			object testClassInstance = testClassType.GetConstructor(new Type[] { })?.Invoke(new object[] { });
			if (testClassInstance == null)
			{
				PrintHelper.PrintMessage(string.Concat(testClassType.FullName, " has no default constructor!"));
				return;
			}

			// Find the test methods
			ArrayList testMethods = GetMethodsWithAttribute(testClassType, typeof(TestMethodAttribute));
			if (testMethods == null)
			{
				return;
			}

			// Execute "ClassInitialize" before all tests
			ArrayList classInitialize = GetMethodsWithAttribute(testClassType, typeof(ClassInitializeAttribute));
			if (classInitialize != null)
			{
				MethodInfo classInitializeMethod = (MethodInfo) classInitialize[0];
				PrintHelper.PrintMessage(string.Concat(testClassType.FullName, " : Calling ", classInitializeMethod.Name, " for class initialize"));
				classInitializeMethod.Invoke(testClassInstance, new object[] { });
			}

			// Execute all test methods
			ArrayList testInitialize = GetMethodsWithAttribute(testClassType, typeof(TestInitializeAttribute));
			MethodInfo testInitializeMethod = (MethodInfo) testInitialize?[0];
			ArrayList testCleanup = GetMethodsWithAttribute(testClassType, typeof(TestCleanupAttribute));
			MethodInfo testCleanupMethod = (MethodInfo) testCleanup?[0];
			foreach (MethodInfo testMethod in testMethods)
			{
				// should the test be ignored?
				if (HasIgnoreAttribute(testMethod))
				{
					PrintHelper.PrintMessage(string.Concat(testClassType.FullName, ".", testMethod.Name, " : Ignored"));
					continue;
				}

				// Execute "TestInitialize" before each test
				if (testInitializeMethod != null)
				{
					PrintHelper.PrintMessage(string.Concat(testClassType.FullName, ".", testMethod.Name, " : Calling ", testInitializeMethod.Name, " for test initialize"));
					testInitializeMethod.Invoke(testClassInstance, new object[] { });
				}

				long startTicks = DateTime.UtcNow.Ticks;
				TestStatus status;
				Exception exception;
				ExecuteTestMethod(testMethod, testClassInstance, out status, out exception);
				long stopTicks = DateTime.UtcNow.Ticks;
				// send the result to the debugger
				PrintHelper.PrintTestResult(testClassType.FullName, testMethod.Name, status, exception, stopTicks - startTicks);

				// Execute "TestCleanup" after each test
				if (testCleanupMethod != null)
				{
					PrintHelper.PrintMessage(string.Concat(testClassType.FullName, ".", testMethod.Name, " : Calling ", testCleanupMethod.Name, " for test cleanup"));
					testCleanupMethod.Invoke(testClassInstance, new object[] { });
				}
			}

			// Execute "ClassCleanup" after all tests
			ArrayList classCleanup = GetMethodsWithAttribute(testClassType, typeof(ClassCleanupAttribute));
			if (classCleanup != null)
			{
				MethodInfo classCleanupMethod = (MethodInfo) classCleanup?[0];
				PrintHelper.PrintMessage(string.Concat(testClassType.FullName, " : Calling ", classCleanupMethod.Name, " for class cleanup"));
				classCleanupMethod.Invoke(testClassInstance, new object[] { });
			}
		}

		/// <summary>
		///   Executes one test method
		/// </summary>
		/// <param name="method">The parameter less method</param>
		/// <param name="instance">The test class instance</param>
		/// <param name="testStatus">The test result</param>
		/// <param name="testException">
		///   The exception that the test method has thrown or null if the test has passed without a
		///   failure
		/// </param>
		private static void ExecuteTestMethod(MethodInfo method, object instance, out TestStatus testStatus, out Exception testException)
		{
			try
			{
				method.Invoke(instance, null);
				testStatus = TestStatus.Passed;
				testException = null;
			}
			catch (Exception exception)
			{
				if (exception.InnerException != null)
				{
					exception = exception.InnerException;
				}

				testStatus = TestStatus.Failed;
				testException = exception;
			}
		}

		/// <summary>
		///   Checks if a type has the <see cref="IgnoreAttribute" /> set
		/// </summary>
		/// <param name="type">The type</param>
		/// <returns>true if the <see cref="IgnoreAttribute" /> is present; false otherwise</returns>
		private static bool HasIgnoreAttribute(Type type)
		{
			return HasIgnoreAttribute(type.GetCustomAttributes(false));
		}

		/// <summary>
		///   Checks if a method has the <see cref="IgnoreAttribute" /> set
		/// </summary>
		/// <param name="method">The method</param>
		/// <param name="ignoreMessage">If the <see cref="IgnoreAttribute" /> is present then the IgnoreMessage will be returned</param>
		/// <returns>true if the <see cref="IgnoreAttribute" /> is present; false otherwise</returns>
		private static bool HasIgnoreAttribute(MethodInfo method)
		{
			return HasIgnoreAttribute(method.GetCustomAttributes(false));
		}

		/// <summary>
		///   Checks if the attributes array has the <see cref="IgnoreAttribute" /> in it
		/// </summary>
		/// <param name="attributes">The attributes array</param>
		/// <param name="ignoreMessage">If the <see cref="IgnoreAttribute" /> is present then the IgnoreMessage will be returned</param>
		/// <returns>true if the <see cref="IgnoreAttribute" /> is present; false otherwise</returns>
		private static bool HasIgnoreAttribute(object[] attributes)
		{
			foreach (object attribute in attributes)
			{
				// check for IgnoreAttribute
				if (typeof(IgnoreAttribute).Equals(attribute) || typeof(IgnoreAttribute).Name == attribute.GetType().Name)
				{
					return true;
				}
			}

			return false;
		}

		#endregion
	}
}