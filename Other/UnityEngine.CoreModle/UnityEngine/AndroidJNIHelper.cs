using System;
using UnityEngine.Internal;

namespace UnityEngine
{
	public class AndroidJNIHelper
	{
		public static bool debug
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		private AndroidJNIHelper()
		{
		}

		public static IntPtr GetConstructorID(IntPtr javaClass)
		{
			return IntPtr.Zero;
		}

		public static IntPtr GetConstructorID(IntPtr javaClass, [DefaultValue("")] string signature)
		{
			return IntPtr.Zero;
		}

		public static IntPtr GetMethodID(IntPtr javaClass, string methodName)
		{
			return IntPtr.Zero;
		}

		public static IntPtr GetMethodID(IntPtr javaClass, string methodName, [DefaultValue("")] string signature)
		{
			return IntPtr.Zero;
		}

		public static IntPtr GetMethodID(IntPtr javaClass, string methodName, [DefaultValue("")] string signature, [DefaultValue("false")] bool isStatic)
		{
			return IntPtr.Zero;
		}

		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName)
		{
			return IntPtr.Zero;
		}

		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName, [DefaultValue("")] string signature)
		{
			return IntPtr.Zero;
		}

		public static IntPtr GetFieldID(IntPtr javaClass, string fieldName, [DefaultValue("")] string signature, [DefaultValue("false")] bool isStatic)
		{
			return IntPtr.Zero;
		}

		public static IntPtr CreateJavaRunnable(AndroidJavaRunnable jrunnable)
		{
			return IntPtr.Zero;
		}

		public static IntPtr CreateJavaProxy(AndroidJavaProxy proxy)
		{
			return IntPtr.Zero;
		}

		public static IntPtr ConvertToJNIArray(Array array)
		{
			return IntPtr.Zero;
		}

		public static jvalue[] CreateJNIArgArray(object[] args)
		{
			return null;
		}

		public static void DeleteJNIArgArray(object[] args, jvalue[] jniArgs)
		{
		}

		public static IntPtr GetConstructorID(IntPtr jclass, object[] args)
		{
			return IntPtr.Zero;
		}

		public static IntPtr GetMethodID(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return IntPtr.Zero;
		}

		public static string GetSignature(object obj)
		{
			return "";
		}

		public static string GetSignature(object[] args)
		{
			return "";
		}

		public static ArrayType ConvertFromJNIArray<ArrayType>(IntPtr array)
		{
			return default(ArrayType);
		}

		public static IntPtr GetMethodID<ReturnType>(IntPtr jclass, string methodName, object[] args, bool isStatic)
		{
			return IntPtr.Zero;
		}

		public static IntPtr GetFieldID<FieldType>(IntPtr jclass, string fieldName, bool isStatic)
		{
			return IntPtr.Zero;
		}

		public static string GetSignature<ReturnType>(object[] args)
		{
			return "";
		}
	}
}
