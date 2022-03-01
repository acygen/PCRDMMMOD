using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling
{
	[UsedByNativeCode]
	[NativeHeader("Runtime/Profiler/ScriptBindings/Sampler.bindings.h")]
	[NativeHeader("Runtime/Profiler/Marker.h")]
	public sealed class CustomSampler : Sampler
	{
		internal static CustomSampler s_InvalidCustomSampler = new CustomSampler();

		internal CustomSampler()
		{
		}

		internal CustomSampler(IntPtr ptr)
		{
			m_Ptr = ptr;
		}

		public static CustomSampler Create(string name)
		{
			IntPtr intPtr = CreateInternal(name);
			if (intPtr == IntPtr.Zero)
			{
				return s_InvalidCustomSampler;
			}
			return new CustomSampler(intPtr);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "ProfilerBindings::CreateCustomSamplerInternal", IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		private static extern IntPtr CreateInternal([NotNull] string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Conditional("ENABLE_PROFILER")]
		[NativeMethod(Name = "ProfilerBindings::CustomSampler_Begin", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		public extern void Begin();

		[Conditional("ENABLE_PROFILER")]
		public void Begin(Object targetObject)
		{
			BeginWithObject(targetObject);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "ProfilerBindings::CustomSampler_BeginWithObject", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private extern void BeginWithObject(Object targetObject);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "ProfilerBindings::CustomSampler_End", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		[Conditional("ENABLE_PROFILER")]
		public extern void End();
	}
}
