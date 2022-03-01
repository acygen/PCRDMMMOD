using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine
{
	[NativeHeader("Runtime/Misc/ResourceManagerUtility.h")]
	[NativeHeader("Runtime/Export/Resources.bindings.h")]
	public sealed class Resources
	{
		internal static T[] ConvertObjects<T>(Object[] rawObjects) where T : Object
		{
			if (rawObjects == null)
			{
				return null;
			}
			T[] array = new T[rawObjects.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (T)rawObjects[i];
			}
			return array;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[FreeFunction("Resources_Bindings::FindObjectsOfTypeAll")]
		public static extern Object[] FindObjectsOfTypeAll(Type type);

		public static T[] FindObjectsOfTypeAll<T>() where T : Object
		{
			return ConvertObjects<T>(FindObjectsOfTypeAll(typeof(T)));
		}

		public static Object Load(string path)
		{
			return Load(path, typeof(Object));
		}

		public static T Load<T>(string path) where T : Object
		{
			return (T)Load(path, typeof(T));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		[FreeFunction("Resources_Bindings::Load")]
		[NativeThrows]
		public static extern Object Load(string path, [NotNull] Type systemTypeInstance);

		public static ResourceRequest LoadAsync(string path)
		{
			return LoadAsync(path, typeof(Object));
		}

		public static ResourceRequest LoadAsync<T>(string path) where T : Object
		{
			return LoadAsync(path, typeof(T));
		}

		public static ResourceRequest LoadAsync(string path, Type type)
		{
			ResourceRequest resourceRequest = LoadAsyncInternal(path, type);
			resourceRequest.m_Path = path;
			resourceRequest.m_Type = type;
			return resourceRequest;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Resources_Bindings::LoadAsyncInternal")]
		internal static extern ResourceRequest LoadAsyncInternal(string path, Type type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		[FreeFunction("Resources_Bindings::LoadAll")]
		public static extern Object[] LoadAll([NotNull] string path, [NotNull] Type systemTypeInstance);

		public static Object[] LoadAll(string path)
		{
			return LoadAll(path, typeof(Object));
		}

		public static T[] LoadAll<T>(string path) where T : Object
		{
			return ConvertObjects<T>(LoadAll(path, typeof(T)));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[FreeFunction("GetScriptingBuiltinResource")]
		public static extern Object GetBuiltinResource([NotNull] Type type, string path);

		public static T GetBuiltinResource<T>(string path) where T : Object
		{
			return (T)GetBuiltinResource(typeof(T), path);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Scripting::UnloadAssetFromScripting")]
		public static extern void UnloadAsset(Object assetToUnload);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Resources_Bindings::UnloadUnusedAssets")]
		public static extern AsyncOperation UnloadUnusedAssets();
	}
}
