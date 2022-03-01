using System;

namespace UnityEngine
{
	public class AndroidJNI
	{
		private AndroidJNI()
		{
		}

		public static int AttachCurrentThread()
		{
			return 0;
		}

		public static int DetachCurrentThread()
		{
			return 0;
		}

		public static int GetVersion()
		{
			return 0;
		}

		public static IntPtr FindClass(string name)
		{
			return IntPtr.Zero;
		}

		public static IntPtr FromReflectedMethod(IntPtr refMethod)
		{
			return IntPtr.Zero;
		}

		public static IntPtr FromReflectedField(IntPtr refField)
		{
			return IntPtr.Zero;
		}

		public static IntPtr ToReflectedMethod(IntPtr clazz, IntPtr methodID, bool isStatic)
		{
			return IntPtr.Zero;
		}

		public static IntPtr ToReflectedField(IntPtr clazz, IntPtr fieldID, bool isStatic)
		{
			return IntPtr.Zero;
		}

		public static IntPtr GetSuperclass(IntPtr clazz)
		{
			return IntPtr.Zero;
		}

		public static bool IsAssignableFrom(IntPtr clazz1, IntPtr clazz2)
		{
			return false;
		}

		public static int Throw(IntPtr obj)
		{
			return 0;
		}

		public static int ThrowNew(IntPtr clazz, string message)
		{
			return 0;
		}

		public static IntPtr ExceptionOccurred()
		{
			return IntPtr.Zero;
		}

		public static void ExceptionDescribe()
		{
		}

		public static void ExceptionClear()
		{
		}

		public static void FatalError(string message)
		{
		}

		public static int PushLocalFrame(int capacity)
		{
			return 0;
		}

		public static IntPtr PopLocalFrame(IntPtr ptr)
		{
			return IntPtr.Zero;
		}

		public static IntPtr NewGlobalRef(IntPtr obj)
		{
			return IntPtr.Zero;
		}

		public static void DeleteGlobalRef(IntPtr obj)
		{
		}

		public static IntPtr NewLocalRef(IntPtr obj)
		{
			return IntPtr.Zero;
		}

		public static void DeleteLocalRef(IntPtr obj)
		{
		}

		public static bool IsSameObject(IntPtr obj1, IntPtr obj2)
		{
			return false;
		}

		public static int EnsureLocalCapacity(int capacity)
		{
			return 0;
		}

		public static IntPtr AllocObject(IntPtr clazz)
		{
			return IntPtr.Zero;
		}

		public static IntPtr NewObject(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return IntPtr.Zero;
		}

		public static IntPtr GetObjectClass(IntPtr obj)
		{
			return IntPtr.Zero;
		}

		public static bool IsInstanceOf(IntPtr obj, IntPtr clazz)
		{
			return false;
		}

		public static IntPtr GetMethodID(IntPtr clazz, string name, string sig)
		{
			return IntPtr.Zero;
		}

		public static IntPtr GetFieldID(IntPtr clazz, string name, string sig)
		{
			return IntPtr.Zero;
		}

		public static IntPtr GetStaticMethodID(IntPtr clazz, string name, string sig)
		{
			return IntPtr.Zero;
		}

		public static IntPtr GetStaticFieldID(IntPtr clazz, string name, string sig)
		{
			return IntPtr.Zero;
		}

		public static IntPtr NewStringUTF(string bytes)
		{
			return IntPtr.Zero;
		}

		public static int GetStringUTFLength(IntPtr str)
		{
			return 0;
		}

		public static string GetStringUTFChars(IntPtr str)
		{
			return "";
		}

		public static string CallStringMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return "";
		}

		public static IntPtr CallObjectMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return IntPtr.Zero;
		}

		public static int CallIntMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return 0;
		}

		public static bool CallBooleanMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return false;
		}

		public static short CallShortMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return 0;
		}

		public static byte CallByteMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return 0;
		}

		public static char CallCharMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return '0';
		}

		public static float CallFloatMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return 0f;
		}

		public static double CallDoubleMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return 0.0;
		}

		public static long CallLongMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return 0L;
		}

		public static void CallVoidMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
		}

		public static string GetStringField(IntPtr obj, IntPtr fieldID)
		{
			return "";
		}

		public static IntPtr GetObjectField(IntPtr obj, IntPtr fieldID)
		{
			return IntPtr.Zero;
		}

		public static bool GetBooleanField(IntPtr obj, IntPtr fieldID)
		{
			return false;
		}

		public static byte GetByteField(IntPtr obj, IntPtr fieldID)
		{
			return 0;
		}

		public static char GetCharField(IntPtr obj, IntPtr fieldID)
		{
			return '0';
		}

		public static short GetShortField(IntPtr obj, IntPtr fieldID)
		{
			return 0;
		}

		public static int GetIntField(IntPtr obj, IntPtr fieldID)
		{
			return 0;
		}

		public static long GetLongField(IntPtr obj, IntPtr fieldID)
		{
			return 0L;
		}

		public static float GetFloatField(IntPtr obj, IntPtr fieldID)
		{
			return 0f;
		}

		public static double GetDoubleField(IntPtr obj, IntPtr fieldID)
		{
			return 0.0;
		}

		public static void SetStringField(IntPtr obj, IntPtr fieldID, string val)
		{
		}

		public static void SetObjectField(IntPtr obj, IntPtr fieldID, IntPtr val)
		{
		}

		public static void SetBooleanField(IntPtr obj, IntPtr fieldID, bool val)
		{
		}

		public static void SetByteField(IntPtr obj, IntPtr fieldID, byte val)
		{
		}

		public static void SetCharField(IntPtr obj, IntPtr fieldID, char val)
		{
		}

		public static void SetShortField(IntPtr obj, IntPtr fieldID, short val)
		{
		}

		public static void SetIntField(IntPtr obj, IntPtr fieldID, int val)
		{
		}

		public static void SetLongField(IntPtr obj, IntPtr fieldID, long val)
		{
		}

		public static void SetFloatField(IntPtr obj, IntPtr fieldID, float val)
		{
		}

		public static void SetDoubleField(IntPtr obj, IntPtr fieldID, double val)
		{
		}

		public static string CallStaticStringMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return "";
		}

		public static IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return IntPtr.Zero;
		}

		public static int CallStaticIntMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return 0;
		}

		public static bool CallStaticBooleanMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return false;
		}

		public static short CallStaticShortMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return 0;
		}

		public static byte CallStaticByteMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return 0;
		}

		public static char CallStaticCharMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return '0';
		}

		public static float CallStaticFloatMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return 0f;
		}

		public static double CallStaticDoubleMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return 0.0;
		}

		public static long CallStaticLongMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return 0L;
		}

		public static void CallStaticVoidMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
		}

		public static string GetStaticStringField(IntPtr clazz, IntPtr fieldID)
		{
			return "";
		}

		public static IntPtr GetStaticObjectField(IntPtr clazz, IntPtr fieldID)
		{
			return IntPtr.Zero;
		}

		public static bool GetStaticBooleanField(IntPtr clazz, IntPtr fieldID)
		{
			return false;
		}

		public static byte GetStaticByteField(IntPtr clazz, IntPtr fieldID)
		{
			return 0;
		}

		public static char GetStaticCharField(IntPtr clazz, IntPtr fieldID)
		{
			return '0';
		}

		public static short GetStaticShortField(IntPtr clazz, IntPtr fieldID)
		{
			return 0;
		}

		public static int GetStaticIntField(IntPtr clazz, IntPtr fieldID)
		{
			return 0;
		}

		public static long GetStaticLongField(IntPtr clazz, IntPtr fieldID)
		{
			return 0L;
		}

		public static float GetStaticFloatField(IntPtr clazz, IntPtr fieldID)
		{
			return 0f;
		}

		public static double GetStaticDoubleField(IntPtr clazz, IntPtr fieldID)
		{
			return 0.0;
		}

		public static void SetStaticStringField(IntPtr clazz, IntPtr fieldID, string val)
		{
		}

		public static void SetStaticObjectField(IntPtr clazz, IntPtr fieldID, IntPtr val)
		{
		}

		public static void SetStaticBooleanField(IntPtr clazz, IntPtr fieldID, bool val)
		{
		}

		public static void SetStaticByteField(IntPtr clazz, IntPtr fieldID, byte val)
		{
		}

		public static void SetStaticCharField(IntPtr clazz, IntPtr fieldID, char val)
		{
		}

		public static void SetStaticShortField(IntPtr clazz, IntPtr fieldID, short val)
		{
		}

		public static void SetStaticIntField(IntPtr clazz, IntPtr fieldID, int val)
		{
		}

		public static void SetStaticLongField(IntPtr clazz, IntPtr fieldID, long val)
		{
		}

		public static void SetStaticFloatField(IntPtr clazz, IntPtr fieldID, float val)
		{
		}

		public static void SetStaticDoubleField(IntPtr clazz, IntPtr fieldID, double val)
		{
		}

		public static IntPtr ToBooleanArray(bool[] array)
		{
			return IntPtr.Zero;
		}

		public static IntPtr ToByteArray(byte[] array)
		{
			return IntPtr.Zero;
		}

		public static IntPtr ToCharArray(char[] array)
		{
			return IntPtr.Zero;
		}

		public static IntPtr ToShortArray(short[] array)
		{
			return IntPtr.Zero;
		}

		public static IntPtr ToIntArray(int[] array)
		{
			return IntPtr.Zero;
		}

		public static IntPtr ToLongArray(long[] array)
		{
			return IntPtr.Zero;
		}

		public static IntPtr ToFloatArray(float[] array)
		{
			return IntPtr.Zero;
		}

		public static IntPtr ToDoubleArray(double[] array)
		{
			return IntPtr.Zero;
		}

		public static IntPtr ToObjectArray(IntPtr[] array, IntPtr arrayClass)
		{
			return IntPtr.Zero;
		}

		public static IntPtr ToObjectArray(IntPtr[] array)
		{
			return IntPtr.Zero;
		}

		public static bool[] FromBooleanArray(IntPtr array)
		{
			return null;
		}

		public static byte[] FromByteArray(IntPtr array)
		{
			return null;
		}

		public static char[] FromCharArray(IntPtr array)
		{
			return null;
		}

		public static short[] FromShortArray(IntPtr array)
		{
			return null;
		}

		public static int[] FromIntArray(IntPtr array)
		{
			return null;
		}

		public static long[] FromLongArray(IntPtr array)
		{
			return null;
		}

		public static float[] FromFloatArray(IntPtr array)
		{
			return null;
		}

		public static double[] FromDoubleArray(IntPtr array)
		{
			return null;
		}

		public static IntPtr[] FromObjectArray(IntPtr array)
		{
			return null;
		}

		public static int GetArrayLength(IntPtr array)
		{
			return 0;
		}

		public static IntPtr NewBooleanArray(int size)
		{
			return IntPtr.Zero;
		}

		public static IntPtr NewByteArray(int size)
		{
			return IntPtr.Zero;
		}

		public static IntPtr NewCharArray(int size)
		{
			return IntPtr.Zero;
		}

		public static IntPtr NewShortArray(int size)
		{
			return IntPtr.Zero;
		}

		public static IntPtr NewIntArray(int size)
		{
			return IntPtr.Zero;
		}

		public static IntPtr NewLongArray(int size)
		{
			return IntPtr.Zero;
		}

		public static IntPtr NewFloatArray(int size)
		{
			return IntPtr.Zero;
		}

		public static IntPtr NewDoubleArray(int size)
		{
			return IntPtr.Zero;
		}

		public static IntPtr NewObjectArray(int size, IntPtr clazz, IntPtr obj)
		{
			return IntPtr.Zero;
		}

		public static bool GetBooleanArrayElement(IntPtr array, int index)
		{
			return false;
		}

		public static byte GetByteArrayElement(IntPtr array, int index)
		{
			return 0;
		}

		public static char GetCharArrayElement(IntPtr array, int index)
		{
			return '0';
		}

		public static short GetShortArrayElement(IntPtr array, int index)
		{
			return 0;
		}

		public static int GetIntArrayElement(IntPtr array, int index)
		{
			return 0;
		}

		public static long GetLongArrayElement(IntPtr array, int index)
		{
			return 0L;
		}

		public static float GetFloatArrayElement(IntPtr array, int index)
		{
			return 0f;
		}

		public static double GetDoubleArrayElement(IntPtr array, int index)
		{
			return 0.0;
		}

		public static IntPtr GetObjectArrayElement(IntPtr array, int index)
		{
			return IntPtr.Zero;
		}

		public static void SetBooleanArrayElement(IntPtr array, int index, byte val)
		{
		}

		public static void SetByteArrayElement(IntPtr array, int index, sbyte val)
		{
		}

		public static void SetCharArrayElement(IntPtr array, int index, char val)
		{
		}

		public static void SetShortArrayElement(IntPtr array, int index, short val)
		{
		}

		public static void SetIntArrayElement(IntPtr array, int index, int val)
		{
		}

		public static void SetLongArrayElement(IntPtr array, int index, long val)
		{
		}

		public static void SetFloatArrayElement(IntPtr array, int index, float val)
		{
		}

		public static void SetDoubleArrayElement(IntPtr array, int index, double val)
		{
		}

		public static void SetObjectArrayElement(IntPtr array, int index, IntPtr obj)
		{
		}
	}
}
