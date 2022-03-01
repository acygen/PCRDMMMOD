using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Scripting/DelayedCallUtility.h")]
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	[ExtensionOfNativeClass]
	[RequiredByNativeCode]
	public class MonoBehaviour : Behaviour
	{
		public extern bool useGUILayout
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public bool IsInvoking()
		{
			return Internal_IsInvokingAll(this);
		}

		public void CancelInvoke()
		{
			Internal_CancelInvokeAll(this);
		}

		public void Invoke(string methodName, float time)
		{
			InvokeDelayed(this, methodName, time, 0f);
		}

		public void InvokeRepeating(string methodName, float time, float repeatRate)
		{
			if (repeatRate <= 1E-05f && repeatRate != 0f)
			{
				throw new UnityException("Invoke repeat rate has to be larger than 0.00001F)");
			}
			InvokeDelayed(this, methodName, time, repeatRate);
		}

		public void CancelInvoke(string methodName)
		{
			CancelInvoke(this, methodName);
		}

		public bool IsInvoking(string methodName)
		{
			return IsInvoking(this, methodName);
		}

		[ExcludeFromDocs]
		public Coroutine StartCoroutine(string methodName)
		{
			object value = null;
			return StartCoroutine(methodName, value);
		}

		public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
		{
			if (string.IsNullOrEmpty(methodName))
			{
				throw new NullReferenceException("methodName is null or empty");
			}
			if (!IsObjectMonoBehaviour(this))
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			return StartCoroutineManaged(methodName, value);
		}

		public Coroutine StartCoroutine(IEnumerator routine)
		{
			if (routine == null)
			{
				throw new NullReferenceException("routine is null");
			}
			if (!IsObjectMonoBehaviour(this))
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			return StartCoroutineManaged2(routine);
		}

		[Obsolete("StartCoroutine_Auto has been deprecated. Use StartCoroutine instead (UnityUpgradable) -> StartCoroutine([mscorlib] System.Collections.IEnumerator)", false)]
		public Coroutine StartCoroutine_Auto(IEnumerator routine)
		{
			return StartCoroutine(routine);
		}

		public void StopCoroutine(IEnumerator routine)
		{
			if (routine == null)
			{
				throw new NullReferenceException("routine is null");
			}
			if (!IsObjectMonoBehaviour(this))
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			StopCoroutineFromEnumeratorManaged(routine);
		}

		public void StopCoroutine(Coroutine routine)
		{
			if (routine == null)
			{
				throw new NullReferenceException("routine is null");
			}
			if (!IsObjectMonoBehaviour(this))
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			StopCoroutineManaged(routine);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopCoroutine(string methodName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopAllCoroutines();

		public static void print(object message)
		{
			Debug.Log(message);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CancelInvoke")]
		private static extern void Internal_CancelInvokeAll(MonoBehaviour self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("IsInvoking")]
		private static extern bool Internal_IsInvokingAll(MonoBehaviour self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		private static extern void InvokeDelayed(MonoBehaviour self, string methodName, float time, float repeatRate);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		private static extern void CancelInvoke(MonoBehaviour self, string methodName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		private static extern bool IsInvoking(MonoBehaviour self, string methodName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		private static extern bool IsObjectMonoBehaviour(Object obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Coroutine StartCoroutineManaged(string methodName, object value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Coroutine StartCoroutineManaged2(IEnumerator enumerator);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void StopCoroutineManaged(Coroutine routine);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void StopCoroutineFromEnumeratorManaged(IEnumerator routine);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string GetScriptClassName();
	}
}
