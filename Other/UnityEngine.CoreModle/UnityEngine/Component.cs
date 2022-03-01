using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/Component.bindings.h")]
	[NativeClass("Unity::Component")]
	[RequiredByNativeCode]
	public class Component : Object
	{
		public extern Transform transform
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetTransform", HasExplicitThis = true, ThrowsException = true)]
			get;
		}

		public extern GameObject gameObject
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetGameObject", HasExplicitThis = true)]
			get;
		}

		public string tag
		{
			get
			{
				return gameObject.tag;
			}
			set
			{
				gameObject.tag = value;
			}
		}

		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponent(Type type)
		{
			return gameObject.GetComponent(type);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(HasExplicitThis = true, ThrowsException = true)]
		internal extern void GetComponentFastPath(Type type, IntPtr oneFurtherThanResultValue);

		[SecuritySafeCritical]
		public unsafe T GetComponent<T>()
		{
			CastHelper<T> castHelper = default(CastHelper<T>);
			GetComponentFastPath(typeof(T), new IntPtr(&castHelper.onePointerFurtherThanT));
			return castHelper.t;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(HasExplicitThis = true)]
		public extern Component GetComponent(string type);

		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type t, bool includeInactive)
		{
			return gameObject.GetComponentInChildren(t, includeInactive);
		}

		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type t)
		{
			return GetComponentInChildren(t, includeInactive: false);
		}

		public T GetComponentInChildren<T>([DefaultValue("false")] bool includeInactive)
		{
			return (T)(object)GetComponentInChildren(typeof(T), includeInactive);
		}

		[ExcludeFromDocs]
		public T GetComponentInChildren<T>()
		{
			return (T)(object)GetComponentInChildren(typeof(T), includeInactive: false);
		}

		public Component[] GetComponentsInChildren(Type t, bool includeInactive)
		{
			return gameObject.GetComponentsInChildren(t, includeInactive);
		}

		[ExcludeFromDocs]
		public Component[] GetComponentsInChildren(Type t)
		{
			return gameObject.GetComponentsInChildren(t, includeInactive: false);
		}

		public T[] GetComponentsInChildren<T>(bool includeInactive)
		{
			return gameObject.GetComponentsInChildren<T>(includeInactive);
		}

		public void GetComponentsInChildren<T>(bool includeInactive, List<T> result)
		{
			gameObject.GetComponentsInChildren(includeInactive, result);
		}

		public T[] GetComponentsInChildren<T>()
		{
			return GetComponentsInChildren<T>(includeInactive: false);
		}

		public void GetComponentsInChildren<T>(List<T> results)
		{
			GetComponentsInChildren(includeInactive: false, results);
		}

		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInParent(Type t)
		{
			return gameObject.GetComponentInParent(t);
		}

		public T GetComponentInParent<T>()
		{
			return (T)(object)GetComponentInParent(typeof(T));
		}

		public Component[] GetComponentsInParent(Type t, [DefaultValue("false")] bool includeInactive)
		{
			return gameObject.GetComponentsInParent(t, includeInactive);
		}

		[ExcludeFromDocs]
		public Component[] GetComponentsInParent(Type t)
		{
			return GetComponentsInParent(t, includeInactive: false);
		}

		public T[] GetComponentsInParent<T>(bool includeInactive)
		{
			return gameObject.GetComponentsInParent<T>(includeInactive);
		}

		public void GetComponentsInParent<T>(bool includeInactive, List<T> results)
		{
			gameObject.GetComponentsInParent(includeInactive, results);
		}

		public T[] GetComponentsInParent<T>()
		{
			return GetComponentsInParent<T>(includeInactive: false);
		}

		public Component[] GetComponents(Type type)
		{
			return gameObject.GetComponents(type);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(HasExplicitThis = true, ThrowsException = true)]
		private extern void GetComponentsForListInternal(Type searchType, object resultList);

		public void GetComponents(Type type, List<Component> results)
		{
			GetComponentsForListInternal(type, results);
		}

		public void GetComponents<T>(List<T> results)
		{
			GetComponentsForListInternal(typeof(T), results);
		}

		public T[] GetComponents<T>()
		{
			return gameObject.GetComponents<T>();
		}

		public bool CompareTag(string tag)
		{
			return gameObject.CompareTag(tag);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(HasExplicitThis = true)]
		public extern void SendMessageUpwards(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName, object value)
		{
			SendMessageUpwards(methodName, value, SendMessageOptions.RequireReceiver);
		}

		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName)
		{
			SendMessageUpwards(methodName, null, SendMessageOptions.RequireReceiver);
		}

		public void SendMessageUpwards(string methodName, SendMessageOptions options)
		{
			SendMessageUpwards(methodName, null, options);
		}

		public void SendMessage(string methodName, object value)
		{
			SendMessage(methodName, value, SendMessageOptions.RequireReceiver);
		}

		public void SendMessage(string methodName)
		{
			SendMessage(methodName, null, SendMessageOptions.RequireReceiver);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("SendMessage", HasExplicitThis = true)]
		public extern void SendMessage(string methodName, object value, SendMessageOptions options);

		public void SendMessage(string methodName, SendMessageOptions options)
		{
			SendMessage(methodName, null, options);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("BroadcastMessage", HasExplicitThis = true)]
		public extern void BroadcastMessage(string methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName, object parameter)
		{
			BroadcastMessage(methodName, parameter, SendMessageOptions.RequireReceiver);
		}

		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName)
		{
			BroadcastMessage(methodName, null, SendMessageOptions.RequireReceiver);
		}

		public void BroadcastMessage(string methodName, SendMessageOptions options)
		{
			BroadcastMessage(methodName, null, options);
		}
	}
}
