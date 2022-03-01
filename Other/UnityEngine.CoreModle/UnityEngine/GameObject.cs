using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/GameObject.bindings.h")]
	[ExcludeFromPreset]
	[UsedByNativeCode]
	public sealed class GameObject : Object
	{
		public extern Transform transform
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GameObjectBindings::GetTransform", HasExplicitThis = true)]
			get;
		}

		public extern int layer
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("GameObject.active is obsolete. Use GameObject.SetActive(), GameObject.activeSelf or GameObject.activeInHierarchy.")]
		public extern bool active
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "IsActive")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "SetSelfActive")]
			set;
		}

		public extern bool activeSelf
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "IsSelfActive")]
			get;
		}

		public extern bool activeInHierarchy
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "IsActive")]
			get;
		}

		public extern bool isStatic
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "GetIsStaticDeprecated")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "SetIsStaticDeprecated")]
			set;
		}

		internal extern bool isStaticBatchable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "IsStaticBatchable")]
			get;
		}

		public extern string tag
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GameObjectBindings::GetTag", HasExplicitThis = true, ThrowsException = true)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GameObjectBindings::SetTag", HasExplicitThis = true)]
			set;
		}

		public Scene scene
		{
			[FreeFunction("GameObjectBindings::GetScene", HasExplicitThis = true)]
			get
			{
				get_scene_Injected(out var ret);
				return ret;
			}
		}

		public GameObject gameObject => this;

		public GameObject(string name)
		{
			Internal_CreateGameObject(this, name);
		}

		public GameObject()
		{
			Internal_CreateGameObject(this, null);
		}

		public GameObject(string name, params Type[] components)
		{
			Internal_CreateGameObject(this, name);
			foreach (Type componentType in components)
			{
				AddComponent(componentType);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GameObjectBindings::CreatePrimitive")]
		public static extern GameObject CreatePrimitive(PrimitiveType type);

		[SecuritySafeCritical]
		public unsafe T GetComponent<T>()
		{
			CastHelper<T> castHelper = default(CastHelper<T>);
			GetComponentFastPath(typeof(T), new IntPtr(&castHelper.onePointerFurtherThanT));
			return castHelper.t;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "GameObjectBindings::GetComponentFromType", HasExplicitThis = true, ThrowsException = true)]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public extern Component GetComponent(Type type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "GameObjectBindings::GetComponentFastPath", HasExplicitThis = true, ThrowsException = true)]
		[NativeWritableSelf]
		internal extern void GetComponentFastPath(Type type, IntPtr oneFurtherThanResultValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Scripting::GetScriptingWrapperOfComponentOfGameObject", HasExplicitThis = true)]
		internal extern Component GetComponentByName(string type);

		public Component GetComponent(string type)
		{
			return GetComponentByName(type);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "GameObjectBindings::GetComponentInChildren", HasExplicitThis = true, ThrowsException = true)]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public extern Component GetComponentInChildren(Type type, bool includeInactive);

		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type type)
		{
			return GetComponentInChildren(type, includeInactive: false);
		}

		[ExcludeFromDocs]
		public T GetComponentInChildren<T>()
		{
			bool includeInactive = false;
			return GetComponentInChildren<T>(includeInactive);
		}

		public T GetComponentInChildren<T>([DefaultValue("false")] bool includeInactive)
		{
			return (T)(object)GetComponentInChildren(typeof(T), includeInactive);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "GameObjectBindings::GetComponentInParent", HasExplicitThis = true, ThrowsException = true)]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public extern Component GetComponentInParent(Type type);

		public T GetComponentInParent<T>()
		{
			return (T)(object)GetComponentInParent(typeof(T));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "GameObjectBindings::GetComponentsInternal", HasExplicitThis = true, ThrowsException = true)]
		private extern Array GetComponentsInternal(Type type, bool useSearchTypeAsArrayReturnType, bool recursive, bool includeInactive, bool reverse, object resultList);

		public Component[] GetComponents(Type type)
		{
			return (Component[])GetComponentsInternal(type, useSearchTypeAsArrayReturnType: false, recursive: false, includeInactive: true, reverse: false, null);
		}

		public T[] GetComponents<T>()
		{
			return (T[])GetComponentsInternal(typeof(T), useSearchTypeAsArrayReturnType: true, recursive: false, includeInactive: true, reverse: false, null);
		}

		public void GetComponents(Type type, List<Component> results)
		{
			GetComponentsInternal(type, useSearchTypeAsArrayReturnType: false, recursive: false, includeInactive: true, reverse: false, results);
		}

		public void GetComponents<T>(List<T> results)
		{
			GetComponentsInternal(typeof(T), useSearchTypeAsArrayReturnType: false, recursive: false, includeInactive: true, reverse: false, results);
		}

		[ExcludeFromDocs]
		public Component[] GetComponentsInChildren(Type type)
		{
			bool includeInactive = false;
			return GetComponentsInChildren(type, includeInactive);
		}

		public Component[] GetComponentsInChildren(Type type, [DefaultValue("false")] bool includeInactive)
		{
			return (Component[])GetComponentsInternal(type, useSearchTypeAsArrayReturnType: false, recursive: true, includeInactive, reverse: false, null);
		}

		public T[] GetComponentsInChildren<T>(bool includeInactive)
		{
			return (T[])GetComponentsInternal(typeof(T), useSearchTypeAsArrayReturnType: true, recursive: true, includeInactive, reverse: false, null);
		}

		public void GetComponentsInChildren<T>(bool includeInactive, List<T> results)
		{
			GetComponentsInternal(typeof(T), useSearchTypeAsArrayReturnType: true, recursive: true, includeInactive, reverse: false, results);
		}

		public T[] GetComponentsInChildren<T>()
		{
			return GetComponentsInChildren<T>(includeInactive: false);
		}

		public void GetComponentsInChildren<T>(List<T> results)
		{
			GetComponentsInChildren(includeInactive: false, results);
		}

		[ExcludeFromDocs]
		public Component[] GetComponentsInParent(Type type)
		{
			bool includeInactive = false;
			return GetComponentsInParent(type, includeInactive);
		}

		public Component[] GetComponentsInParent(Type type, [DefaultValue("false")] bool includeInactive)
		{
			return (Component[])GetComponentsInternal(type, useSearchTypeAsArrayReturnType: false, recursive: true, includeInactive, reverse: true, null);
		}

		public void GetComponentsInParent<T>(bool includeInactive, List<T> results)
		{
			GetComponentsInternal(typeof(T), useSearchTypeAsArrayReturnType: true, recursive: true, includeInactive, reverse: true, results);
		}

		public T[] GetComponentsInParent<T>(bool includeInactive)
		{
			return (T[])GetComponentsInternal(typeof(T), useSearchTypeAsArrayReturnType: true, recursive: true, includeInactive, reverse: true, null);
		}

		public T[] GetComponentsInParent<T>()
		{
			return GetComponentsInParent<T>(includeInactive: false);
		}

		public static GameObject FindWithTag(string tag)
		{
			return FindGameObjectWithTag(tag);
		}

		public void SendMessageUpwards(string methodName, SendMessageOptions options)
		{
			SendMessageUpwards(methodName, null, options);
		}

		public void SendMessage(string methodName, SendMessageOptions options)
		{
			SendMessage(methodName, null, options);
		}

		public void BroadcastMessage(string methodName, SendMessageOptions options)
		{
			BroadcastMessage(methodName, null, options);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MonoAddComponent", HasExplicitThis = true)]
		internal extern Component AddComponentInternal(string className);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MonoAddComponentWithType", HasExplicitThis = true)]
		private extern Component Internal_AddComponentWithType(Type componentType);

		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component AddComponent(Type componentType)
		{
			return Internal_AddComponentWithType(componentType);
		}

		public T AddComponent<T>() where T : Component
		{
			return AddComponent(typeof(T)) as T;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "SetSelfActive")]
		public extern void SetActive(bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("gameObject.SetActiveRecursively() is obsolete. Use GameObject.SetActive(), which is now inherited by children.")]
		[NativeMethod(Name = "SetActiveRecursivelyDeprecated")]
		public extern void SetActiveRecursively(bool state);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "GameObjectBindings::CompareTag", HasExplicitThis = true)]
		public extern bool CompareTag(string tag);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "GameObjectBindings::FindGameObjectWithTag", ThrowsException = true)]
		public static extern GameObject FindGameObjectWithTag(string tag);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "GameObjectBindings::FindGameObjectsWithTag", ThrowsException = true)]
		public static extern GameObject[] FindGameObjectsWithTag(string tag);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Scripting::SendScriptingMessageUpwards", HasExplicitThis = true)]
		public extern void SendMessageUpwards(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName, object value)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			SendMessageUpwards(methodName, value, options);
		}

		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object value = null;
			SendMessageUpwards(methodName, value, options);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Scripting::SendScriptingMessage", HasExplicitThis = true)]
		public extern void SendMessage(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		[ExcludeFromDocs]
		public void SendMessage(string methodName, object value)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			SendMessage(methodName, value, options);
		}

		[ExcludeFromDocs]
		public void SendMessage(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object value = null;
			SendMessage(methodName, value, options);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Scripting::BroadcastScriptingMessage", HasExplicitThis = true)]
		public extern void BroadcastMessage(string methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName, object parameter)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			BroadcastMessage(methodName, parameter, options);
		}

		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName)
		{
			SendMessageOptions options = SendMessageOptions.RequireReceiver;
			object parameter = null;
			BroadcastMessage(methodName, parameter, options);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "GameObjectBindings::Internal_CreateGameObject")]
		private static extern void Internal_CreateGameObject([Writable] GameObject self, string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "GameObjectBindings::Find")]
		public static extern GameObject Find(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_scene_Injected(out Scene ret);
	}
}
