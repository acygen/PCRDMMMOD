using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.SceneManagement
{
	[Serializable]
	[NativeHeader("Runtime/Export/SceneManager/Scene.bindings.h")]
	public struct Scene
	{
		internal enum LoadingState
		{
			NotLoaded,
			Loading,
			Loaded,
			Unloading
		}

		[SerializeField]
		private int m_Handle;

		public int handle => m_Handle;

		internal LoadingState loadingState => GetLoadingStateInternal(handle);

		internal string guid => GetGUIDInternal(handle);

		public string path => GetPathInternal(handle);

		public string name
		{
			get
			{
				return GetNameInternal(handle);
			}
			set
			{
				SetNameInternal(handle, value);
			}
		}

		public bool isLoaded => GetIsLoadedInternal(handle);

		public int buildIndex => GetBuildIndexInternal(handle);

		public bool isDirty => GetIsDirtyInternal(handle);

		internal int dirtyID => GetDirtyID(handle);

		public int rootCount => GetRootCountInternal(handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		private static extern bool IsValidInternal(int sceneHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		private static extern string GetPathInternal(int sceneHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		private static extern string GetNameInternal(int sceneHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		private static extern void SetNameInternal(int sceneHandle, string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		private static extern string GetGUIDInternal(int sceneHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		private static extern bool GetIsLoadedInternal(int sceneHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		private static extern LoadingState GetLoadingStateInternal(int sceneHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		private static extern bool GetIsDirtyInternal(int sceneHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		private static extern int GetDirtyID(int sceneHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		private static extern int GetBuildIndexInternal(int sceneHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		private static extern int GetRootCountInternal(int sceneHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		private static extern void GetRootGameObjectsInternal(int sceneHandle, object resultRootList);

		public bool IsValid()
		{
			return IsValidInternal(handle);
		}

		public GameObject[] GetRootGameObjects()
		{
			List<GameObject> list = new List<GameObject>(rootCount);
			GetRootGameObjects(list);
			return list.ToArray();
		}

		public void GetRootGameObjects(List<GameObject> rootGameObjects)
		{
			if (rootGameObjects.Capacity < rootCount)
			{
				rootGameObjects.Capacity = rootCount;
			}
			rootGameObjects.Clear();
			if (!IsValid())
			{
				throw new ArgumentException("The scene is invalid.");
			}
			if (!Application.isPlaying && !isLoaded)
			{
				throw new ArgumentException("The scene is not loaded.");
			}
			if (rootCount != 0)
			{
				GetRootGameObjectsInternal(handle, rootGameObjects);
			}
		}

		public static bool operator ==(Scene lhs, Scene rhs)
		{
			return lhs.handle == rhs.handle;
		}

		public static bool operator !=(Scene lhs, Scene rhs)
		{
			return lhs.handle != rhs.handle;
		}

		public override int GetHashCode()
		{
			return m_Handle;
		}

		public override bool Equals(object other)
		{
			if (!(other is Scene))
			{
				return false;
			}
			Scene scene = (Scene)other;
			return handle == scene.handle;
		}
	}
}
