using System;

namespace UnityEngine
{
	public class AndroidJavaObject : IDisposable
	{
		protected static AndroidJavaClass JavaLangClass => null;

		internal AndroidJavaObject()
		{
		}

		public AndroidJavaObject(string className, params object[] args)
		{
		}

		public void Dispose()
		{
		}

		public void Call(string methodName, params object[] args)
		{
		}

		public void CallStatic(string methodName, params object[] args)
		{
		}

		public FieldType Get<FieldType>(string fieldName)
		{
			return default(FieldType);
		}

		public void Set<FieldType>(string fieldName, FieldType val)
		{
		}

		public FieldType GetStatic<FieldType>(string fieldName)
		{
			return default(FieldType);
		}

		public void SetStatic<FieldType>(string fieldName, FieldType val)
		{
		}

		public IntPtr GetRawObject()
		{
			return IntPtr.Zero;
		}

		public IntPtr GetRawClass()
		{
			return IntPtr.Zero;
		}

		public ReturnType Call<ReturnType>(string methodName, params object[] args)
		{
			return default(ReturnType);
		}

		public ReturnType CallStatic<ReturnType>(string methodName, params object[] args)
		{
			return default(ReturnType);
		}

		protected void DebugPrint(string msg)
		{
		}

		protected void DebugPrint(string call, string methodName, string signature, object[] args)
		{
		}

		~AndroidJavaObject()
		{
		}

		protected virtual void Dispose(bool disposing)
		{
		}

		protected void _Dispose()
		{
		}

		protected void _Call(string methodName, params object[] args)
		{
		}

		protected ReturnType _Call<ReturnType>(string methodName, params object[] args)
		{
			return default(ReturnType);
		}

		protected FieldType _Get<FieldType>(string fieldName)
		{
			return default(FieldType);
		}

		protected void _Set<FieldType>(string fieldName, FieldType val)
		{
		}

		protected void _CallStatic(string methodName, params object[] args)
		{
		}

		protected ReturnType _CallStatic<ReturnType>(string methodName, params object[] args)
		{
			return default(ReturnType);
		}

		protected FieldType _GetStatic<FieldType>(string fieldName)
		{
			return default(FieldType);
		}

		protected void _SetStatic<FieldType>(string fieldName, FieldType val)
		{
		}

		protected IntPtr _GetRawObject()
		{
			return IntPtr.Zero;
		}

		protected IntPtr _GetRawClass()
		{
			return IntPtr.Zero;
		}

		protected static AndroidJavaObject FindClass(string name)
		{
			return null;
		}
	}
}
