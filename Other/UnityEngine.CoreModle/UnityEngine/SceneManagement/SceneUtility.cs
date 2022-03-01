using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.SceneManagement
{
	[NativeHeader("Runtime/Export/SceneManager/SceneUtility.bindings.h")]
	public static class SceneUtility
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("SceneUtilityBindings", StaticAccessorType.DoubleColon)]
		public static extern string GetScenePathByBuildIndex(int buildIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("SceneUtilityBindings", StaticAccessorType.DoubleColon)]
		public static extern int GetBuildIndexByScenePath(string scenePath);
	}
}
