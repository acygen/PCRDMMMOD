using System;
using System.Collections;
using System.Reflection;
using System.Security;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[RequiredByNativeCode]
	internal class SetupCoroutine
	{
		[RequiredByNativeCode]
		[SecuritySafeCritical]
		public unsafe static void InvokeMoveNext(IEnumerator enumerator, IntPtr returnValueAddress)
		{
			if (returnValueAddress == IntPtr.Zero)
			{
				throw new ArgumentException("Return value address cannot be 0.", "returnValueAddress");
			}
			*(bool*)(void*)returnValueAddress = enumerator.MoveNext();
		}

		[RequiredByNativeCode]
		public static object InvokeMember(object behaviour, string name, object variable)
		{
			object[] args = null;
			if (variable != null)
			{
				args = new object[1] { variable };
			}
			return behaviour.GetType().InvokeMember(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, behaviour, args, null, null, null);
		}

		public static object InvokeStatic(Type klass, string name, object variable)
		{
			object[] args = null;
			if (variable != null)
			{
				args = new object[1] { variable };
			}
			return klass.InvokeMember(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, null, args, null, null, null);
		}
	}
}
