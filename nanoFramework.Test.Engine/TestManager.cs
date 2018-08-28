//
// Copyright (c) 2018 The nanoFramework project contributors
// Portions Copyright (c) https://github.com/4egod/TinyCLRTest All rights reserved.
// See LICENSE file in the project root for full license information.
//

using System;
using System.Collections;
using System.Reflection;
using System.Threading;

namespace nanoFramework.Test.Engine
{
	/// <summary>
	/// The TestManager executes all tests that are within an assembly or all loaded assemblies
	/// </summary>
	/// <remarks>
	/// Via reflection all public classes with a parameterless constructor will be examined. Only classes that
	/// implement the (empty) interface <see cref="T:nfUnit.ITestClass" /> will be considered as classes that contain test.
	/// All classes that doesn't implement <see cref="T:nfUnit.ITestClass" /> will be ignored.
	/// 
	/// It's possible to run code before and after test execution at certain levels:
	/// - Implement <see cref="T:nfUnit.IAssemblyInitialize" /> additionally to <see cref="T:nfUnit.ITestClass" />
	///		to run code in the AssemblyInitialize method. That code will be executed before any test of this assembly will be executed.
	///	- Implement <see cref="T:nfUnit.IAssemblyCleanup" /> additionally to <see cref="T:nfUnit.ITestClass" />
	///		to run code in the AssemblyCleanup method. That code will be executed after all tests of this assembly has been executed.
	/// - Implement <see cref="T:nfUnit.IClassInitialize" /> additionally to <see cref="T:nfUnit.ITestClass" />
	///		to run code in the ClassInitialize method. That code will be executed before any test of this test class will be executed.
	///	- Implement <see cref="T:nfUnit.IClassCleanup" /> additionally to <see cref="T:nfUnit.ITestClass" />
	///		to run code in the ClassCleanup method. That code will be executed after all tests of this test class has been executed.
	/// - Implement <see cref="T:nfUnit.ITestInitialize" /> additionally to <see cref="T:nfUnit.ITestClass" />
	///		to run code in the TestInitialize method.	That code will be executed before each test of this test class will be executed.
	///	- Implement <see cref="T:nfUnit.ITestCleanup" /> additionally to <see cref="T:nfUnit.ITestClass" />
	///		to run code in the TestCleanup method. That code will be executed after each test of this test class has been executed.
	///		
	/// All other public parameterless instance methods that are directly declared in classes that are implement <see cref="T:nfUnit.ITestClass" />
	/// will be treated as test methods. The methods shouldn't have a return parameter. The return parameter will be ignored. The test methods are executed
	/// in the order in witch they are found in the class type. No particular execution order is guarantied. For each test method
	/// the possible exception will be caught and recorded. If the method executes without an exception the execution time will
	/// be recorded. After each test the result will be posted via Console.WriteLine.
	/// </remarks>
	public static class TestManager
	{
		#region constants
		/// <summary>
		/// Name of the IAssemblyInitialize.AssemblyInitialize method; the interface has only this one method
		/// </summary>
		private static readonly string ASSEMBLY_INITIALIZE_METHOD_NAME = typeof(IAssemblyInitialize).GetMethods()[0].Name;
		/// <summary>
		/// Name of the IAssemblyCleanup.AssemblyCleanup method; the interface has only this one method
		/// </summary>
		private static readonly string ASSEMBLY_CLEANUP_METHOD_NAME = typeof(IAssemblyCleanup).GetMethods()[0].Name;
		/// <summary>
		/// Name of the IClassInitialize.ClassInitialize method; the interface has only this one method
		/// </summary>
		private static readonly string CLASS_INITIALIZE_METHOD_NAME = typeof(IClassInitialize).GetMethods()[0].Name;
		/// <summary>
		/// Name of the IClassCleanup.ClassCleanup method; the interface has only this one method
		/// </summary>
		private static readonly string CLASS_CLEANUP_METHOD_NAME = typeof(IClassCleanup).GetMethods()[0].Name;
		/// <summary>
		/// Name of the ITestInitialize.TestInitialize method; the interface has only this one method
		/// </summary>
		private static readonly string TEST_INITIALIZE_METHOD_NAME = typeof(ITestInitialize).GetMethods()[0].Name;
		/// <summary>
		/// Name of the ITestCleanup.TestCleanup method; the interface has only this one method
		/// </summary>
		private static readonly string TEST_CLEANUP_METHOD_NAME = typeof(ITestCleanup).GetMethods()[0].Name;
		#endregion

		#region public methods
		/// <summary>
		/// Runs all tests from all loaded assemblies form the current app domain
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
		/// Runs all tests that are in an assembly
		/// </summary>
		/// <param name="assembly">The test assembly</param>
		public static void RunTests(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			string assemblyName = assembly.FullName;
			
			// Get all test class types
			Hashtable testClassTypes = GetTestClassTypes(assembly);
			// No test classes in this assembly?
			if (testClassTypes.Count == 0)
			{
				return;
			}

			// wait 5 seconds to lets the listener establish the connection before outputting anything
			Thread.Sleep(5000);

			// iterate through all test class types and find the types that implements IAssemblyInitialize/IAssemblyCleanup
			Type assemblyInitializeInstance = null;
			Type assemblyCleanupInstance = null;
			foreach (DictionaryEntry entry in testClassTypes)
			{
				foreach (Type oneInterface in (Type[])entry.Value)
				{
					if (oneInterface == typeof(IAssemblyInitialize))
					{
						// warning if already found in another class
						if (assemblyInitializeInstance != null)
						{
							PrintHelper.PrintMessage(string.Concat(assemblyName, " : More than one class implements the ", typeof(IAssemblyInitialize).Name, " interface"));
						}
						// store/overwrite the type for IAssemblyInitialize
						assemblyInitializeInstance = (Type)entry.Key;
					}
					else if (oneInterface == typeof(IAssemblyCleanup))
					{
						// warning if already found in another class
						if (assemblyCleanupInstance != null)
						{
							PrintHelper.PrintMessage(string.Concat(assemblyName, " : More than one class implements the ", typeof(IAssemblyCleanup).Name, " interface"));
						}
						// store/overwrite the type for IAssemblyCleanup
						assemblyCleanupInstance = (Type)entry.Key;
					}
				}
			}

			// print the start message
			PrintHelper.PrintMessage(string.Concat(assemblyName, " : Tests started (", TimeSpan.TicksPerMillisecond, " ticks = 1 millisecond)"));

			// execute the AssemblyInitialize method
			if (assemblyInitializeInstance != null)
			{
				PrintHelper.PrintMessage(string.Concat(assemblyName, " : Calling ", ASSEMBLY_INITIALIZE_METHOD_NAME));
				((IAssemblyInitialize)assemblyInitializeInstance.GetConstructor(new Type[] { })).AssemblyInitialize();
			}

			// iterate through all test class types and execute the tests
			foreach (DictionaryEntry entry in testClassTypes)
			{
				RunTests((Type)entry.Key, (Type[])entry.Value);
			}

			// execute the AssemblyCleanup method
			if (assemblyCleanupInstance != null)
			{
				PrintHelper.PrintMessage(string.Concat(assemblyName, " : Calling ", ASSEMBLY_CLEANUP_METHOD_NAME));
				((IAssemblyCleanup)assemblyCleanupInstance.GetConstructor(new Type[] { })).AssemblyCleanup();
			}

			// print the end message
			PrintHelper.PrintMessage(string.Concat(assemblyName, " : Tests finished"));
		}

		/// <summary>
		/// Gets a Hashtable which contains all test class types as keys. The values are the list of interfaces that the test classes implements.
		/// </summary>
		/// <param name="assembly">The test assembly</param>
		/// <returns>Hashtable: keys of type Type; values of type Type[]</returns>
		public static Hashtable GetTestClassTypes(Assembly assembly)
		{
			// examine all types
			Hashtable testClassTypes = new Hashtable();
			Type[] types = assembly.GetTypes();
			foreach (Type type in types)
			{
				// skip all types that are not a public class
				if (!type.IsClass || !type.IsPublic)
				{
					continue;
				}

				// skip if the class has no a public parameterless constructor
				ConstructorInfo constructor = type.GetConstructor(new Type[] { });
				if (constructor == null)
				{
					continue;
				}

				// Get the implemented interfaces and test if the ITestClass interface is implemented
				Type[] interfaces = type.GetInterfaces();
				foreach (Type oneInterface in interfaces)
				{
					if (oneInterface.Name == typeof(ITestClass).Name)
					{
						// found! => add to the result
						testClassTypes.Add(type, interfaces);
						break;
					}
				}
			}
			return testClassTypes;
		}

		/// <summary>
		/// Gets all test methods of one test class type
		/// </summary>
		/// <param name="testClassType">The test class type</param>
		/// <param name="testClassInterfaces">The list of interfaces that the test classes implements.</returns>
		public static ArrayList GetTestMethods(Type testClassType, Type[] testClassInterfaces)
		{
			ArrayList testMethods = new ArrayList();
			ArrayList excludedMethodNames = GetExcludedMethodNames(testClassInterfaces);

			// examine all public instance methods that are declared directly in this class (no inheritance)
			MethodInfo[] methods = testClassType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
			foreach (MethodInfo method in methods)
			{
				// is this method in the list of the excluded methods?
				bool exclude = false;
				if (excludedMethodNames != null && excludedMethodNames.Count > 0)
				{
					foreach (string excludedMethodName in excludedMethodNames)
					{
						if (method.Name == excludedMethodName)
						{
							exclude = true;
							break;
						}
					}
				}
				// skip this method if it should be excluded
				if (exclude)
				{
					continue;
				}

				// test if we don't have already a method found with this name
				bool alreadyFound = false;
				foreach (MethodInfo foundTestMethod in testMethods)
				{
					if (foundTestMethod.Name == method.Name)
					{
						alreadyFound = true;
						break;
					}
				}
				// continue if method already found
				if (alreadyFound)
				{
					continue;
				}

				// find the parameterless overload of this method and add it to the test methods
				MethodInfo testMethod = testClassType.GetMethod(method.Name, new Type[] { });
				if (testMethod != null)
				{
					testMethods.Add(testMethod);
				}
			}
			return testMethods;
		}
		#endregion

		#region private methods
		/// <summary>
		/// Run all tests from one test class type
		/// </summary>
		/// <param name="testClassType">The test class type</param>
		/// <param name="testClassInterfaces">The list of interfaces that the test classes implements.</returns>
		private static void RunTests(Type testClassType, Type[] testClassInterfaces)
		{
			// create the instance and cast to the interfaces that has the initialize/cleanup methods
			object testClassInstance = testClassType.GetConstructor(new Type[] { }).Invoke(new object[] { });
			
			// Find the test methods
			ArrayList testMethods = GetTestMethods(testClassType, testClassInterfaces);

			// Execute "ClassInitialize" before all tests
			IClassInitialize classInitialize = testClassInstance as IClassInitialize;
			if (classInitialize != null)
			{
				PrintHelper.PrintMessage(string.Concat(testClassType.FullName, " : Calling ", CLASS_INITIALIZE_METHOD_NAME));
				classInitialize.ClassInitialize();
			}

			// Execute all test methods
			ITestInitialize testInitialize = testClassInstance as ITestInitialize;
			ITestCleanup testCleanup = testClassInstance as ITestCleanup;
			foreach (MethodInfo testMethod in testMethods)
			{
				// Execute "TestInitialize" before each test
				if (testInitialize != null)
				{
					PrintHelper.PrintMessage(string.Concat(testClassType.FullName, ".", testMethod.Name, " : Calling ", TEST_INITIALIZE_METHOD_NAME));
					testInitialize.TestInitialize();
				}

				long startTicks = DateTime.UtcNow.Ticks;
				TestStatus status;
				Exception exception;
				ExecuteTestMethod(testMethod, testClassInstance, out status, out exception);
				long stopTicks = DateTime.UtcNow.Ticks;
				// send the result to the debugger
				PrintHelper.PrintTestResult(testClassType.FullName, testMethod.Name, status, exception, stopTicks - startTicks);

				// Execute "TestCleanup" after each test
				if (testCleanup != null)
				{
					PrintHelper.PrintMessage(string.Concat(testClassType.FullName, ".", testMethod.Name, " : Calling ", TEST_CLEANUP_METHOD_NAME));
					testCleanup.TestCleanup();
				}
			}

			// Execute "ClassCleanup" after all tests
			IClassCleanup classCleanup = testClassInstance as IClassCleanup;
			if (classCleanup != null)
			{
				PrintHelper.PrintMessage(string.Concat(testClassType.FullName, " : Calling ", CLASS_CLEANUP_METHOD_NAME));
				classCleanup.ClassCleanup();
			}
		}

		/// <summary>
		/// Executes one test method
		/// </summary>
		/// <param name="method">The parameterless method</param>
		/// <param name="instance">The test class instance</param>
		/// <param name="testStatus">The test result</param>
		/// <param name="testException">The exception that the test method has thrown or null if the test has passed without a failure</param>
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
		/// Gets the names of the methods that are not test methods. For each implemented known interface one method
		/// (the interface method) will be excluded.
		/// </summary>
		/// <param name="testClassInterfaces">The list of interfaces that the test classes implements.</param>
		/// <returns></returns>
		private static ArrayList GetExcludedMethodNames(Type[] testClassInterfaces)
		{
			ArrayList excludedMethodNames = new ArrayList();
			foreach (Type oneInterface in testClassInterfaces)
			{
				// for each know interface one method will be excluded
				if (oneInterface.Name == typeof(IAssemblyInitialize).Name)
				{
					excludedMethodNames.Add(ASSEMBLY_INITIALIZE_METHOD_NAME);
				}
				else if (oneInterface.Name == typeof(IAssemblyCleanup).Name)
				{
					excludedMethodNames.Add(ASSEMBLY_CLEANUP_METHOD_NAME);
				}
				else if (oneInterface.Name == typeof(IClassInitialize).Name)
				{
					excludedMethodNames.Add(CLASS_INITIALIZE_METHOD_NAME);
				}
				else if (oneInterface.Name == typeof(IClassCleanup).Name)
				{
					excludedMethodNames.Add(CLASS_CLEANUP_METHOD_NAME);
				}
				else if (oneInterface.Name == typeof(ITestInitialize).Name)
				{
					excludedMethodNames.Add(TEST_INITIALIZE_METHOD_NAME);
				}
				else if (oneInterface.Name == typeof(ITestCleanup).Name)
				{
					excludedMethodNames.Add(TEST_CLEANUP_METHOD_NAME);
				}
			}
			return excludedMethodNames;
		}
		#endregion
	}
}