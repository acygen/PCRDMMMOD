using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	[UsedByNativeCode]
	[NativeHeader("Runtime/Profiler/ScriptBindings/ProfilerMarker.bindings.h")]
	public struct ProfilerMarker
	{
		[UsedByNativeCode]
		public struct AutoScope : IDisposable
		{
			[NativeDisableUnsafePtrRestriction]
			internal IntPtr m_Ptr;

			internal AutoScope(IntPtr markerPtr)
			{
				m_Ptr = markerPtr;
				Internal_Begin(markerPtr);
			}

			public void Dispose()
			{
				Internal_End(m_Ptr);
			}
		}

		[NativeDisableUnsafePtrRestriction]
		internal IntPtr m_Ptr;

		public ProfilerMarker(string name)
		{
			m_Ptr = Internal_Create(name, MarkerFlags.Default);
		}

		[Conditional("ENABLE_PROFILER")]
		public void Begin()
		{
			Internal_Begin(m_Ptr);
		}

		[Conditional("ENABLE_PROFILER")]
		public void Begin(UnityEngine.Object contextUnityObject)
		{
			Internal_BeginWithObject(m_Ptr, contextUnityObject);
		}

		[Conditional("ENABLE_PROFILER")]
		public void End()
		{
			Internal_End(m_Ptr);
		}

		public AutoScope Auto()
		{
			return new AutoScope(m_Ptr);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("ENABLE_PROFILER", "NULL")]
		[ThreadSafe]
		private static extern IntPtr Internal_Create(string name, MarkerFlags flags);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("ENABLE_PROFILER")]
		[ThreadSafe]
		private static extern void Internal_Begin(IntPtr markerPtr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("ENABLE_PROFILER")]
		[ThreadSafe]
		private static extern void Internal_BeginWithObject(IntPtr markerPtr, UnityEngine.Object contextUnityObject);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		[NativeConditional("ENABLE_PROFILER")]
		private static extern void Internal_End(IntPtr markerPtr);
	}
}
