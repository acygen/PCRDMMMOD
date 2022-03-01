using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[StaticAccessor("GetUncheckedRealGfxDevice().GetFrameTimingManager()", StaticAccessorType.Dot)]
	public static class FrameTimingManager
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CaptureFrameTimings();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetLatestTimings(uint numFrames, FrameTiming[] timings);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetVSyncsPerSecond();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong GetGpuTimerFrequency();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong GetCpuTimerFrequency();
	}
}
