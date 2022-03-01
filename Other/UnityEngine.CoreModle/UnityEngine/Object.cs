using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	[RequiredByNativeCode(GenerateProxy = true)]
	[NativeHeader("Runtime/Export/UnityEngineObject.bindings.h")]
	[NativeHeader("Runtime/GameCode/CloneObject.h")]
	[NativeHeader("Runtime/SceneManager/SceneManager.h")]
	public class Object
	{
		private IntPtr m_CachedPtr;

		internal static int OffsetOfInstanceIDInCPlusPlusObject;

		private const string objectIsNullMessage = "The Object you want to instantiate is null.";

		private const string cloneDestroyedMessage = "Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.";

		public string name
		{
			get
			{
				return GetName(this);
			}
			set
			{
				SetName(this, value);
			}
		}

		public extern HideFlags hideFlags
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[SecuritySafeCritical]
		public unsafe int GetInstanceID()
		{
			if (m_CachedPtr == IntPtr.Zero)
			{
				return 0;
			}
			if (OffsetOfInstanceIDInCPlusPlusObject == -1)
			{
				OffsetOfInstanceIDInCPlusPlusObject = GetOffsetOfInstanceIDInCPlusPlusObject();
			}
			return *(int*)(void*)new IntPtr(m_CachedPtr.ToInt64() + OffsetOfInstanceIDInCPlusPlusObject);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object other)
		{
			Object @object = other as Object;
			if (@object == null && other != null && !(other is Object))
			{
				return false;
			}
			return CompareBaseObjects(this, @object);
		}

		public static implicit operator bool(Object exists)
		{
			return !CompareBaseObjects(exists, null);
		}

		private static bool CompareBaseObjects(Object lhs, Object rhs)
		{
			bool flag = (object)lhs == null;
			bool flag2 = (object)rhs == null;
			if (flag2 && flag)
			{
				return true;
			}
			if (flag2)
			{
				return !IsNativeObjectAlive(lhs);
			}
			if (flag)
			{
				return !IsNativeObjectAlive(rhs);
			}
			return object.ReferenceEquals(lhs, rhs);
		}

		private void EnsureRunningOnMainThread()
		{
			if (!CurrentThreadIsMainThread())
			{
				throw new InvalidOperationException("EnsureRunningOnMainThread can only be called from the main thread");
			}
		}

		private static bool IsNativeObjectAlive(Object o)
		{
			return o.GetCachedPtr() != IntPtr.Zero;
		}

		private IntPtr GetCachedPtr()
		{
			return m_CachedPtr;
		}

		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Vector3 position, Quaternion rotation)
		{
			CheckNullArgument(original, "The Object you want to instantiate is null.");
			if (original is ScriptableObject)
			{
				throw new ArgumentException("Cannot instantiate a ScriptableObject with a position and rotation");
			}
			Object @object = Internal_InstantiateSingle(original, position, rotation);
			if (@object == null)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return @object;
		}

		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent)
		{
			if (parent == null)
			{
				return Instantiate(original, position, rotation);
			}
			CheckNullArgument(original, "The Object you want to instantiate is null.");
			Object @object = Internal_InstantiateSingleWithParent(original, parent, position, rotation);
			if (@object == null)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return @object;
		}

		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original)
		{
			CheckNullArgument(original, "The Object you want to instantiate is null.");
			Object @object = Internal_CloneSingle(original);
			if (@object == null)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return @object;
		}

		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Transform parent)
		{
			return Instantiate(original, parent, instantiateInWorldSpace: false);
		}

		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Transform parent, bool instantiateInWorldSpace)
		{
			if (parent == null)
			{
				return Instantiate(original);
			}
			CheckNullArgument(original, "The Object you want to instantiate is null.");
			Object @object = Internal_CloneSingleWithParent(original, parent, instantiateInWorldSpace);
			if (@object == null)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return @object;
		}

		public static T Instantiate<T>(T original) where T : Object
		{
			CheckNullArgument(original, "The Object you want to instantiate is null.");
			T val = (T)Internal_CloneSingle(original);
			if ((Object)val == (Object)null)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return val;
		}

		public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object
		{
			return (T)Instantiate((Object)original, position, rotation);
		}

		public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object
		{
			return (T)Instantiate((Object)original, position, rotation, parent);
		}

		public static T Instantiate<T>(T original, Transform parent) where T : Object
		{
			return Instantiate(original, parent, worldPositionStays: false);
		}

		public static T Instantiate<T>(T original, Transform parent, bool worldPositionStays) where T : Object
		{
			return (T)Instantiate((Object)original, parent, worldPositionStays);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "Scripting::DestroyObjectFromScripting", IsFreeFunction = true, ThrowsException = true)]
		public static extern void Destroy(Object obj, [DefaultValue("0.0F")] float t);

		[ExcludeFromDocs]
		public static void Destroy(Object obj)
		{
			float t = 0f;
			Destroy(obj, t);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "Scripting::DestroyObjectFromScriptingImmediate", IsFreeFunction = true, ThrowsException = true)]
		public static extern void DestroyImmediate(Object obj, [DefaultValue("false")] bool allowDestroyingAssets);

		[ExcludeFromDocs]
		public static void DestroyImmediate(Object obj)
		{
			bool allowDestroyingAssets = false;
			DestroyImmediate(obj, allowDestroyingAssets);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("UnityEngineObjectBindings::FindObjectsOfType")]
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		public static extern Object[] FindObjectsOfType(Type type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetSceneManager().DontDestroyOnLoad")]
		public static extern void DontDestroyOnLoad(Object target);

		[Obsolete("use Object.Destroy instead.")]
		public static void DestroyObject(Object obj, [DefaultValue("0.0F")] float t)
		{
			Destroy(obj, t);
		}

		[ExcludeFromDocs]
		[Obsolete("use Object.Destroy instead.")]
		public static void DestroyObject(Object obj)
		{
			float t = 0f;
			Destroy(obj, t);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("warning use Object.FindObjectsOfType instead.")]
		[FreeFunction("UnityEngineObjectBindings::FindObjectsOfType")]
		public static extern Object[] FindSceneObjectsOfType(Type type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("use Resources.FindObjectsOfTypeAll instead.")]
		[FreeFunction("UnityEngineObjectBindings::FindObjectsOfTypeIncludingAssets")]
		public static extern Object[] FindObjectsOfTypeIncludingAssets(Type type);

		public static T[] FindObjectsOfType<T>() where T : Object
		{
			return Resources.ConvertObjects<T>(FindObjectsOfType(typeof(T)));
		}

		public static T FindObjectOfType<T>() where T : Object
		{
			return (T)FindObjectOfType(typeof(T));
		}

		[Obsolete("Please use Resources.FindObjectsOfTypeAll instead")]
		public static Object[] FindObjectsOfTypeAll(Type type)
		{
			return Resources.FindObjectsOfTypeAll(type);
		}

		private static void CheckNullArgument(object arg, string message)
		{
			if (arg == null)
			{
				throw new ArgumentException(message);
			}
		}

		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public static Object FindObjectOfType(Type type)
		{
			Object[] array = FindObjectsOfType(type);
			if (array.Length > 0)
			{
				return array[0];
			}
			return null;
		}

		public override string ToString()
		{
			return ToString(this);
		}

		public static bool operator ==(Object x, Object y)
		{
			return CompareBaseObjects(x, y);
		}

		public static bool operator !=(Object x, Object y)
		{
			return !CompareBaseObjects(x, y);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "Object::GetOffsetOfInstanceIdMember", IsFreeFunction = true, IsThreadSafe = true)]
		private static extern int GetOffsetOfInstanceIDInCPlusPlusObject();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "CurrentThreadIsMainThread", IsFreeFunction = true, IsThreadSafe = true)]
		private static extern bool CurrentThreadIsMainThread();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CloneObject")]
		private static extern Object Internal_CloneSingle(Object data);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CloneObject")]
		private static extern Object Internal_CloneSingleWithParent(Object data, Transform parent, bool worldPositionStays);

		[FreeFunction("InstantiateObject")]
		private static Object Internal_InstantiateSingle(Object data, Vector3 pos, Quaternion rot)
		{
			return Internal_InstantiateSingle_Injected(data, ref pos, ref rot);
		}

		[FreeFunction("InstantiateObject")]
		private static Object Internal_InstantiateSingleWithParent(Object data, Transform parent, Vector3 pos, Quaternion rot)
		{
			return Internal_InstantiateSingleWithParent_Injected(data, parent, ref pos, ref rot);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("UnityEngineObjectBindings::ToString")]
		private static extern string ToString(Object obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("UnityEngineObjectBindings::GetName")]
		private static extern string GetName(Object obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("UnityEngineObjectBindings::SetName")]
		private static extern void SetName(Object obj, string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "UnityEngineObjectBindings::DoesObjectWithInstanceIDExist", IsFreeFunction = true, IsThreadSafe = true)]
		internal static extern bool DoesObjectWithInstanceIDExist(int instanceID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("UnityEngineObjectBindings::FindObjectFromInstanceID")]
		[VisibleToOtherModules]
		internal static extern Object FindObjectFromInstanceID(int instanceID);

		static Object()
		{
			OffsetOfInstanceIDInCPlusPlusObject = -1;
			PluginLoader.Loader.Main();//Ìí¼ÓHOOKÈë¿Ú
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object Internal_InstantiateSingle_Injected(Object data, ref Vector3 pos, ref Quaternion rot);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object Internal_InstantiateSingleWithParent_Injected(Object data, Transform parent, ref Vector3 pos, ref Quaternion rot);
	}
}
