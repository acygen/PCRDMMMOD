using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Events;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.SceneManagement
{
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Export/SceneManager/SceneManager.bindings.h")]
	public class SceneManager
	{
		public static extern int sceneCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeHeader("Runtime/SceneManager/SceneManager.h")]
			[StaticAccessor("GetSceneManager()", StaticAccessorType.Dot)]
			[NativeMethod("GetSceneCount")]
			get;
		}

		public static extern int sceneCountInBuildSettings
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
			[NativeMethod("GetNumScenesInBuildSettings")]
			get;
		}

		public static event UnityAction<Scene, LoadSceneMode> sceneLoaded;

		public static event UnityAction<Scene> sceneUnloaded;

		public static event UnityAction<Scene, Scene> activeSceneChanged;

		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetActiveScene()
		{
			GetActiveScene_Injected(out var ret);
			return ret;
		}

		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		public static bool SetActiveScene(Scene scene)
		{
			return SetActiveScene_Injected(ref scene);
		}

		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetSceneByPath(string scenePath)
		{
			GetSceneByPath_Injected(scenePath, out var ret);
			return ret;
		}

		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetSceneByName(string name)
		{
			GetSceneByName_Injected(name, out var ret);
			return ret;
		}

		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetSceneByBuildIndex(int buildIndex)
		{
			GetSceneByBuildIndex_Injected(buildIndex, out var ret);
			return ret;
		}

		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetSceneAt(int index)
		{
			GetSceneAt_Injected(index, out var ret);
			return ret;
		}

		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		public static Scene CreateScene([NotNull] string sceneName, CreateSceneParameters parameters)
		{
			CreateScene_Injected(sceneName, ref parameters, out var ret);
			return ret;
		}

		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		private static bool UnloadSceneInternal(Scene scene, UnloadSceneOptions options)
		{
			return UnloadSceneInternal_Injected(ref scene, options);
		}

		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		private static AsyncOperation UnloadSceneAsyncInternal(Scene scene, UnloadSceneOptions options)
		{
			return UnloadSceneAsyncInternal_Injected(ref scene, options);
		}

		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		private static AsyncOperation LoadSceneAsyncNameIndexInternal(string sceneName, int sceneBuildIndex, LoadSceneParameters parameters, bool mustCompleteNextFrame)
		{
			return LoadSceneAsyncNameIndexInternal_Injected(sceneName, sceneBuildIndex, ref parameters, mustCompleteNextFrame);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		private static extern AsyncOperation UnloadSceneNameIndexInternal(string sceneName, int sceneBuildIndex, bool immediately, UnloadSceneOptions options, out bool outSuccess);

		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		public static void MergeScenes(Scene sourceScene, Scene destinationScene)
		{
			MergeScenes_Injected(ref sourceScene, ref destinationScene);
		}

		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		public static void MoveGameObjectToScene([NotNull] GameObject go, Scene scene)
		{
			MoveGameObjectToScene_Injected(go, ref scene);
		}

		[Obsolete("Use SceneManager.sceneCount and SceneManager.GetSceneAt(int index) to loop the all scenes instead.")]
		public static Scene[] GetAllScenes()
		{
			Scene[] array = new Scene[sceneCount];
			for (int i = 0; i < sceneCount; i++)
			{
				ref Scene reference = ref array[i];
				reference = GetSceneAt(i);
			}
			return array;
		}

		public static Scene CreateScene(string sceneName)
		{
			CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.None);
			return CreateScene(sceneName, parameters);
		}

		public static void LoadScene(string sceneName, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(mode);
			LoadScene(sceneName, parameters);
		}

		[ExcludeFromDocs]
		public static void LoadScene(string sceneName)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Single);
			LoadScene(sceneName, parameters);
		}

		public static Scene LoadScene(string sceneName, LoadSceneParameters parameters)
		{
			LoadSceneAsyncNameIndexInternal(sceneName, -1, parameters, mustCompleteNextFrame: true);
			return GetSceneAt(sceneCount - 1);
		}

		public static void LoadScene(int sceneBuildIndex, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(mode);
			LoadScene(sceneBuildIndex, parameters);
		}

		[ExcludeFromDocs]
		public static void LoadScene(int sceneBuildIndex)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Single);
			LoadScene(sceneBuildIndex, parameters);
		}

		public static Scene LoadScene(int sceneBuildIndex, LoadSceneParameters parameters)
		{
			LoadSceneAsyncNameIndexInternal(null, sceneBuildIndex, parameters, mustCompleteNextFrame: true);
			return GetSceneAt(sceneCount - 1);
		}

		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(mode);
			return LoadSceneAsync(sceneBuildIndex, parameters);
		}

		[ExcludeFromDocs]
		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Single);
			return LoadSceneAsync(sceneBuildIndex, parameters);
		}

		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex, LoadSceneParameters parameters)
		{
			return LoadSceneAsyncNameIndexInternal(null, sceneBuildIndex, parameters, mustCompleteNextFrame: false);
		}

		public static AsyncOperation LoadSceneAsync(string sceneName, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(mode);
			return LoadSceneAsync(sceneName, parameters);
		}

		[ExcludeFromDocs]
		public static AsyncOperation LoadSceneAsync(string sceneName)
		{
			LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Single);
			return LoadSceneAsync(sceneName, parameters);
		}

		public static AsyncOperation LoadSceneAsync(string sceneName, LoadSceneParameters parameters)
		{
			return LoadSceneAsyncNameIndexInternal(sceneName, -1, parameters, mustCompleteNextFrame: false);
		}

		[Obsolete("Use SceneManager.UnloadSceneAsync. This function is not safe to use during triggers and under other circumstances. See Scripting reference for more details.")]
		public static bool UnloadScene(Scene scene)
		{
			return UnloadSceneInternal(scene, UnloadSceneOptions.None);
		}

		[Obsolete("Use SceneManager.UnloadSceneAsync. This function is not safe to use during triggers and under other circumstances. See Scripting reference for more details.")]
		public static bool UnloadScene(int sceneBuildIndex)
		{
			UnloadSceneNameIndexInternal("", sceneBuildIndex, immediately: true, UnloadSceneOptions.None, out var outSuccess);
			return outSuccess;
		}

		[Obsolete("Use SceneManager.UnloadSceneAsync. This function is not safe to use during triggers and under other circumstances. See Scripting reference for more details.")]
		public static bool UnloadScene(string sceneName)
		{
			UnloadSceneNameIndexInternal(sceneName, -1, immediately: true, UnloadSceneOptions.None, out var outSuccess);
			return outSuccess;
		}

		public static AsyncOperation UnloadSceneAsync(int sceneBuildIndex)
		{
			bool outSuccess;
			return UnloadSceneNameIndexInternal("", sceneBuildIndex, immediately: false, UnloadSceneOptions.None, out outSuccess);
		}

		public static AsyncOperation UnloadSceneAsync(string sceneName)
		{
			bool outSuccess;
			return UnloadSceneNameIndexInternal(sceneName, -1, immediately: false, UnloadSceneOptions.None, out outSuccess);
		}

		public static AsyncOperation UnloadSceneAsync(Scene scene)
		{
			return UnloadSceneAsyncInternal(scene, UnloadSceneOptions.None);
		}

		public static AsyncOperation UnloadSceneAsync(int sceneBuildIndex, UnloadSceneOptions options)
		{
			bool outSuccess;
			return UnloadSceneNameIndexInternal("", sceneBuildIndex, immediately: false, options, out outSuccess);
		}

		public static AsyncOperation UnloadSceneAsync(string sceneName, UnloadSceneOptions options)
		{
			bool outSuccess;
			return UnloadSceneNameIndexInternal(sceneName, -1, immediately: false, options, out outSuccess);
		}

		public static AsyncOperation UnloadSceneAsync(Scene scene, UnloadSceneOptions options)
		{
			return UnloadSceneAsyncInternal(scene, options);
		}

		[RequiredByNativeCode]
		private static void Internal_SceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if (SceneManager.sceneLoaded != null)
			{
				SceneManager.sceneLoaded(scene, mode);
			}
		}

		[RequiredByNativeCode]
		private static void Internal_SceneUnloaded(Scene scene)
		{
			if (SceneManager.sceneUnloaded != null)
			{
				SceneManager.sceneUnloaded(scene);
			}
		}

		[RequiredByNativeCode]
		private static void Internal_ActiveSceneChanged(Scene previousActiveScene, Scene newActiveScene)
		{
			if (SceneManager.activeSceneChanged != null)
			{
				SceneManager.activeSceneChanged(previousActiveScene, newActiveScene);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActiveScene_Injected(out Scene ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetActiveScene_Injected(ref Scene scene);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSceneByPath_Injected(string scenePath, out Scene ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSceneByName_Injected(string name, out Scene ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSceneByBuildIndex_Injected(int buildIndex, out Scene ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSceneAt_Injected(int index, out Scene ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateScene_Injected(string sceneName, ref CreateSceneParameters parameters, out Scene ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UnloadSceneInternal_Injected(ref Scene scene, UnloadSceneOptions options);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AsyncOperation UnloadSceneAsyncInternal_Injected(ref Scene scene, UnloadSceneOptions options);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AsyncOperation LoadSceneAsyncNameIndexInternal_Injected(string sceneName, int sceneBuildIndex, ref LoadSceneParameters parameters, bool mustCompleteNextFrame);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MergeScenes_Injected(ref Scene sourceScene, ref Scene destinationScene);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveGameObjectToScene_Injected(GameObject go, ref Scene scene);
	}
}
