#define UNITY_ASSERTIONS
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine.Assertions.Comparers;

namespace UnityEngine.Assertions
{
	[DebuggerStepThrough]
	public static class Assert
	{
		internal const string UNITY_ASSERTIONS = "UNITY_ASSERTIONS";

		public static bool raiseExceptions = false;

		private static void Fail(string message, string userMessage)
		{
			if (Debugger.IsAttached)
			{
				throw new AssertionException(message, userMessage);
			}
			if (raiseExceptions)
			{
				throw new AssertionException(message, userMessage);
			}
			if (message == null)
			{
				message = "Assertion has failed\n";
			}
			if (userMessage != null)
			{
				message = userMessage + '\n' + message;
			}
			Debug.LogAssertion(message);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Assert.Equals should not be used for Assertions", true)]
		public new static bool Equals(object obj1, object obj2)
		{
			throw new InvalidOperationException("Assert.Equals should not be used for Assertions");
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Assert.ReferenceEquals should not be used for Assertions", true)]
		public new static bool ReferenceEquals(object obj1, object obj2)
		{
			throw new InvalidOperationException("Assert.ReferenceEquals should not be used for Assertions");
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void IsTrue(bool condition)
		{
			if (!condition)
			{
				IsTrue(condition, null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void IsTrue(bool condition, string message)
		{
			if (!condition)
			{
				Fail(AssertionMessageUtil.BooleanFailureMessage(expected: true), message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void IsFalse(bool condition)
		{
			if (condition)
			{
				IsFalse(condition, null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void IsFalse(bool condition, string message)
		{
			if (condition)
			{
				Fail(AssertionMessageUtil.BooleanFailureMessage(expected: false), message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual)
		{
			AreEqual(expected, actual, null, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual, string message)
		{
			AreEqual(expected, actual, message, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual, float tolerance)
		{
			AreApproximatelyEqual(expected, actual, tolerance, null);
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual, float tolerance, string message)
		{
			AreEqual(expected, actual, message, new FloatComparer(tolerance));
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual)
		{
			AreNotEqual(expected, actual, null, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual, string message)
		{
			AreNotEqual(expected, actual, message, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual, float tolerance)
		{
			AreNotApproximatelyEqual(expected, actual, tolerance, null);
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual, float tolerance, string message)
		{
			AreNotEqual(expected, actual, message, new FloatComparer(tolerance));
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual<T>(T expected, T actual)
		{
			AreEqual(expected, actual, null);
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual<T>(T expected, T actual, string message)
		{
			AreEqual(expected, actual, message, EqualityComparer<T>.Default);
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual<T>(T expected, T actual, string message, IEqualityComparer<T> comparer)
		{
			if (typeof(Object).IsAssignableFrom(typeof(T)))
			{
				AreEqual(expected as Object, actual as Object, message);
			}
			else if (!comparer.Equals(actual, expected))
			{
				Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, expectEqual: true), message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(Object expected, Object actual, string message)
		{
			if (actual != expected)
			{
				Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, expectEqual: true), message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual<T>(T expected, T actual)
		{
			AreNotEqual(expected, actual, null);
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual<T>(T expected, T actual, string message)
		{
			AreNotEqual(expected, actual, message, EqualityComparer<T>.Default);
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual<T>(T expected, T actual, string message, IEqualityComparer<T> comparer)
		{
			if (typeof(Object).IsAssignableFrom(typeof(T)))
			{
				AreNotEqual(expected as Object, actual as Object, message);
			}
			else if (comparer.Equals(actual, expected))
			{
				Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, expectEqual: false), message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(Object expected, Object actual, string message)
		{
			if (actual == expected)
			{
				Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, expectEqual: false), message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNull<T>(T value) where T : class
		{
			IsNull(value, null);
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNull<T>(T value, string message) where T : class
		{
			if (typeof(Object).IsAssignableFrom(typeof(T)))
			{
				IsNull(value as Object, message);
			}
			else if (value != null)
			{
				Fail(AssertionMessageUtil.NullFailureMessage(value, expectNull: true), message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNull(Object value, string message)
		{
			if (value != null)
			{
				Fail(AssertionMessageUtil.NullFailureMessage(value, expectNull: true), message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNotNull<T>(T value) where T : class
		{
			IsNotNull(value, null);
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNotNull<T>(T value, string message) where T : class
		{
			if (typeof(Object).IsAssignableFrom(typeof(T)))
			{
				IsNotNull(value as Object, message);
			}
			else if (value == null)
			{
				Fail(AssertionMessageUtil.NullFailureMessage(value, expectNull: false), message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNotNull(Object value, string message)
		{
			if (value == null)
			{
				Fail(AssertionMessageUtil.NullFailureMessage(value, expectNull: false), message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(sbyte expected, sbyte actual)
		{
			if (expected != actual)
			{
				Assert.AreEqual<sbyte>(expected, actual, (string)null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(sbyte expected, sbyte actual, string message)
		{
			if (expected != actual)
			{
				Assert.AreEqual<sbyte>(expected, actual, message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(sbyte expected, sbyte actual)
		{
			if (expected == actual)
			{
				Assert.AreNotEqual<sbyte>(expected, actual, (string)null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(sbyte expected, sbyte actual, string message)
		{
			if (expected == actual)
			{
				Assert.AreNotEqual<sbyte>(expected, actual, message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(byte expected, byte actual)
		{
			if (expected != actual)
			{
				Assert.AreEqual<byte>(expected, actual, (string)null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(byte expected, byte actual, string message)
		{
			if (expected != actual)
			{
				Assert.AreEqual<byte>(expected, actual, message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(byte expected, byte actual)
		{
			if (expected == actual)
			{
				Assert.AreNotEqual<byte>(expected, actual, (string)null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(byte expected, byte actual, string message)
		{
			if (expected == actual)
			{
				Assert.AreNotEqual<byte>(expected, actual, message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(char expected, char actual)
		{
			if (expected != actual)
			{
				Assert.AreEqual<char>(expected, actual, (string)null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(char expected, char actual, string message)
		{
			if (expected != actual)
			{
				Assert.AreEqual<char>(expected, actual, message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(char expected, char actual)
		{
			if (expected == actual)
			{
				Assert.AreNotEqual<char>(expected, actual, (string)null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(char expected, char actual, string message)
		{
			if (expected == actual)
			{
				Assert.AreNotEqual<char>(expected, actual, message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(short expected, short actual)
		{
			if (expected != actual)
			{
				Assert.AreEqual<short>(expected, actual, (string)null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(short expected, short actual, string message)
		{
			if (expected != actual)
			{
				Assert.AreEqual<short>(expected, actual, message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(short expected, short actual)
		{
			if (expected == actual)
			{
				Assert.AreNotEqual<short>(expected, actual, (string)null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(short expected, short actual, string message)
		{
			if (expected == actual)
			{
				Assert.AreNotEqual<short>(expected, actual, message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(ushort expected, ushort actual)
		{
			if (expected != actual)
			{
				Assert.AreEqual<ushort>(expected, actual, (string)null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(ushort expected, ushort actual, string message)
		{
			if (expected != actual)
			{
				Assert.AreEqual<ushort>(expected, actual, message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(ushort expected, ushort actual)
		{
			if (expected == actual)
			{
				Assert.AreNotEqual<ushort>(expected, actual, (string)null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(ushort expected, ushort actual, string message)
		{
			if (expected == actual)
			{
				Assert.AreNotEqual<ushort>(expected, actual, message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(int expected, int actual)
		{
			if (expected != actual)
			{
				Assert.AreEqual<int>(expected, actual, (string)null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(int expected, int actual, string message)
		{
			if (expected != actual)
			{
				Assert.AreEqual<int>(expected, actual, message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(int expected, int actual)
		{
			if (expected == actual)
			{
				Assert.AreNotEqual<int>(expected, actual, (string)null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(int expected, int actual, string message)
		{
			if (expected == actual)
			{
				Assert.AreNotEqual<int>(expected, actual, message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(uint expected, uint actual)
		{
			if (expected != actual)
			{
				Assert.AreEqual<uint>(expected, actual, (string)null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(uint expected, uint actual, string message)
		{
			if (expected != actual)
			{
				Assert.AreEqual<uint>(expected, actual, message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(uint expected, uint actual)
		{
			if (expected == actual)
			{
				Assert.AreNotEqual<uint>(expected, actual, (string)null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(uint expected, uint actual, string message)
		{
			if (expected == actual)
			{
				Assert.AreNotEqual<uint>(expected, actual, message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(long expected, long actual)
		{
			if (expected != actual)
			{
				Assert.AreEqual<long>(expected, actual, (string)null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(long expected, long actual, string message)
		{
			if (expected != actual)
			{
				Assert.AreEqual<long>(expected, actual, message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(long expected, long actual)
		{
			if (expected == actual)
			{
				Assert.AreNotEqual<long>(expected, actual, (string)null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(long expected, long actual, string message)
		{
			if (expected == actual)
			{
				Assert.AreNotEqual<long>(expected, actual, message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(ulong expected, ulong actual)
		{
			if (expected != actual)
			{
				Assert.AreEqual<ulong>(expected, actual, (string)null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(ulong expected, ulong actual, string message)
		{
			if (expected != actual)
			{
				Assert.AreEqual<ulong>(expected, actual, message);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(ulong expected, ulong actual)
		{
			if (expected == actual)
			{
				Assert.AreNotEqual<ulong>(expected, actual, (string)null);
			}
		}

		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(ulong expected, ulong actual, string message)
		{
			if (expected == actual)
			{
				Assert.AreNotEqual<ulong>(expected, actual, message);
			}
		}
	}
}
