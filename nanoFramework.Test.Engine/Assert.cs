//
// Copyright (c) 2018 The nanoFramework project contributors
// Portions Copyright (c) https://github.com/4egod/TinyCLRTest All rights reserved.
// See LICENSE file in the project root for full license information.
//

using System;

namespace nanoFramework.Test.Engine
{
	/// <summary>
	///     The tests can use these assert methods to assert something
	/// </summary>
	public static class Assert
	{
		#region public methods

		/// <summary>
		///     Assert that two objects are equal
		/// </summary>
		/// <param name="expected">excepted object</param>
		/// <param name="actual">actual object</param>
		/// <exception cref="T:nfUnit.AssertFailedException">If the two objects are not equal</exception>
		public static void AreEqual(object expected, object actual)
		{
			if (!InternalEqual(expected, actual)) Fail("Assert.AreEqual", expected, actual);
		}

		/// <summary>
		///     Assert that two objects are not equal
		/// </summary>
		/// <param name="expected">excepted object</param>
		/// <param name="actual">actual object</param>
		/// <exception cref="T:nfUnit.AssertFailedException">If the two objects are equal</exception>
		public static void AreNotEqual(object expected, object actual)
		{
			if (InternalEqual(expected, actual)) Fail("Assert.AreNotEqual", expected, actual);
		}

		/// <summary>
		///     Assert that two objects are same
		/// </summary>
		/// <param name="expected">excepted object</param>
		/// <param name="actual">actual object</param>
		/// <exception cref="T:nfUnit.AssertFailedException">If the two objects are not same</exception>
		public static void AreSame(object expected, object actual)
		{
			if (!ReferenceEquals(expected, actual)) Fail("Assert.AreSame", expected, actual);
		}

		/// <summary>
		///     Assert that two objects are not same
		/// </summary>
		/// <param name="expected">excepted object</param>
		/// <param name="actual">actual object</param>
		/// <exception cref="T:nfUnit.AssertFailedException">If the two objects are same</exception>
		public static void AreNotSame(object expected, object actual)
		{
			if (ReferenceEquals(expected, actual)) Fail("Assert.AreNotSame", expected, actual);
		}

		/// <summary>
		///     Assert that an object is null
		/// </summary>
		/// <param name="value">the object</param>
		/// <exception cref="T:nfUnit.AssertFailedException">If the object is not null</exception>
		public static void IsNull(object value)
		{
			if (value != null) Fail("Assert.IsNull", value);
		}

		/// <summary>
		///     Assert that an object is not null
		/// </summary>
		/// <param name="value">the object</param>
		/// <exception cref="T:nfUnit.AssertFailedException">If the object is null</exception>
		public static void IsNotNull(object value)
		{
			if (value == null) Fail("Assert.IsNotNull", value);
		}

		/// <summary>
		///     Assert that a condition is true
		/// </summary>
		/// <param name="condition">The condition</param>
		/// <exception cref="T:nfUnit.AssertFailedException">If the condition is false</exception>
		public static void IsTrue(bool condition)
		{
			if (!condition) Fail("Assert.IsTrue", condition);
		}

		/// <summary>
		///     Assert that a condition is false
		/// </summary>
		/// <param name="condition">The condition</param>
		/// <exception cref="T:nfUnit.AssertFailedException">If the condition is true</exception>
		public static void IsFalse(bool condition)
		{
			if (condition) Fail("Assert.IsFalse", condition);
		}

		/// <summary>
		///     Throws the AssertFailedException
		/// </summary>
		/// <exception cref="T:nfUnit.AssertFailedException">Always</exception>
		public static void Fail()
		{
			Fail(string.Empty, null);
		}

		/// <summary>
		///     Throws the AssertFailedException with a message
		/// </summary>
		/// <param name="message">The message for the AssertFailedException</param>
		/// <exception cref="T:nfUnit.AssertFailedException">Always</exception>
		public static void Fail(string message)
		{
			Fail(message, null);
		}

		/// <summary>
		///     Throws the AssertFailedException with a message
		/// </summary>
		/// <param name="message">The message for the AssertFailedException</param>
		/// <param name="parameters">The message parameters that should be appended to the message</param>
		/// <exception cref="T:nfUnit.AssertFailedException">Always</exception>
		public static void Fail(string message, params object[] parameters)
		{
			throw new AssertFailedException(BuildMessage("Assert.Fail", message, parameters));
		}

		#endregion

		#region private methods

		/// <summary>
		///     The test if two objects are equal
		/// </summary>
		/// <param name="expected">excepted object</param>
		/// <param name="actual">actual object</param>
		/// <returns>true if the objects are equal; false if not</returns>
		private static bool InternalEqual(object expected, object actual)
		{
			if (expected == null && actual == expected) return true;

			if (expected == null || actual == null) return false;

			Type expectedType = expected.GetType();
			Type actualType = actual.GetType();

			if (expectedType.IsValueType && !expectedType.IsEnum && actualType.IsValueType && !actualType.IsEnum)
			{
				long i1 = 0;
				long i2 = 0;
				ulong ui1 = 0;
				ulong ui2 = 0;
				double d1 = 0;
				double d2 = 0;
				bool b1 = false;
				bool b2 = false;
				char c1 = '\0';
				char c2 = '\0';
				DateTime dt1 = new DateTime(), dt2 = new DateTime();
				TimeSpan tm1 = new TimeSpan(), tm2 = new TimeSpan();

				switch (expectedType.Name)
				{
					case "SByte":
						i1 = (sbyte) expected;
						break;
					case "Byte":
						i1 = (byte) expected;
						break;
					case "Int16":
						i1 = (short) expected;
						break;
					case "UInt16":
						i1 = (ushort) expected;
						break;
					case "Int32":
						i1 = (int) expected;
						break;
					case "UInt32":
						i1 = (uint) expected;
						break;
					case "Int64":
						i1 = (long) expected;
						break;
					case "UInt64":
						ui1 = (ulong) expected;
						break;
					case "Single":
						d1 = (float) expected;
						break;
					case "Double":
						d1 = (double) expected;
						break;
					case "Boolean":
						b1 = (bool) expected;
						break;
					case "Char":
						c1 = (char) expected;
						break;
					case "DateTime":
						dt1 = (DateTime) expected;
						break;
					case "TimeSpan":
						tm1 = (TimeSpan) expected;
						break;
					default:
						Fail("ASSERT EXCEPTION: Unsupported value type");
						break;
				}

				switch (actualType.Name)
				{
					case "SByte":
						i2 = (sbyte) actual;
						break;
					case "Byte":
						i2 = (byte) actual;
						break;
					case "Int16":
						i2 = (short) actual;
						break;
					case "UInt16":
						i2 = (ushort) actual;
						break;
					case "Int32":
						i2 = (int) actual;
						break;
					case "UInt32":
						i2 = (uint) actual;
						break;
					case "Int64":
						i2 = (long) actual;
						break;
					case "UInt64":
						ui2 = (ulong) actual;
						break;
					case "Single":
						d2 = (float) actual;
						break;
					case "Double":
						d2 = (double) actual;
						break;
					case "Boolean":
						b2 = (bool) actual;
						break;
					case "Char":
						c2 = (char) actual;
						break;
					case "DateTime":
						dt2 = (DateTime) actual;
						break;
					case "TimeSpan":
						tm2 = (TimeSpan) actual;
						break;
					default:
						Fail("ASSERT EXCEPTION: Unsupported value type");
						break;
				}

				if (i1 == i2 && ui1 == ui2 && d1 == d2 && b1 == b2 && c1 == c2 && dt1.Ticks == dt2.Ticks &&
				    tm1.Ticks == tm2.Ticks) return true;
			}
			else
			{
				if (Equals(expected, actual)) return true;
			}

			return false;
		}

		/// <summary>
		///     Builds a message
		/// </summary>
		/// <param name="messageType">Message type like "Assert.Fail"</param>
		/// <param name="message">Message</param>
		/// <param name="parameters">Message parameters that should be added to the message</param>
		/// <returns>The concatenated string</returns>
		private static string BuildMessage(string messageType, string message, params object[] parameters)
		{
			string result = messageType;

			if (message != null && message.Length != 0) result += ": " + message;

			if (parameters != null)
			{
				result += " (";

				for (int i = 0; i < parameters.Length; i++)
				{
					if (parameters[i] != null)
						result += parameters[i].ToString();
					else
						result += "null";

					if (i < parameters.Length - 1) result += ", ";
				}

				result += ")";
			}

			return result;
		}

		#endregion;
	}
}