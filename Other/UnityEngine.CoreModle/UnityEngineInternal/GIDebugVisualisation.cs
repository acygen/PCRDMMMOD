using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngineInternal
{
	[NativeHeader("Runtime/Export/GI/GIDebugVisualisation.bindings.h")]
	public static class GIDebugVisualisation
	{
		public static extern bool cycleMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction]
			get;
		}

		public static extern bool pauseCycleMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction]
			get;
		}

		public static extern GITextureType texType
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern void ResetRuntimeInputTextures();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern void PlayCycleMode();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern void PauseCycleMode();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern void StopCycleMode();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern void CycleSkipSystems(int skip);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern void CycleSkipInstances(int skip);
	}
}
