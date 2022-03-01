using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling
{
	[NativeHeader("Runtime/Profiler/Marker.h")]
	[NativeHeader("Runtime/Profiler/ScriptBindings/Sampler.bindings.h")]
	[UsedByNativeCode]
	public class Sampler
	{
		internal IntPtr m_Ptr;

		internal static Sampler s_InvalidSampler = new Sampler();

		public bool isValid => m_Ptr != IntPtr.Zero;

		public string name => (!isValid) ? null : GetSamplerName();

		internal Sampler()
		{
		}

		internal Sampler(IntPtr ptr)
		{
			m_Ptr = ptr;
		}

		public Recorder GetRecorder()
		{
			IntPtr recorderInternal = GetRecorderInternal(m_Ptr);
			if (recorderInternal == IntPtr.Zero)
			{
				return Recorder.s_InvalidRecorder;
			}
			return new Recorder(recorderInternal);
		}

		public static Sampler Get(string name)
		{
			IntPtr samplerInternal = GetSamplerInternal(name);
			if (samplerInternal == IntPtr.Zero)
			{
				return s_InvalidSampler;
			}
			return new Sampler(samplerInternal);
		}

		public static int GetNames(List<string> names)
		{
			return GetSamplerNamesInternal(names);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("ENABLE_PROFILER")]
		[NativeMethod(Name = "GetName", IsThreadSafe = true)]
		private extern string GetSamplerName();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "ProfilerBindings::GetRecorderInternal", IsFreeFunction = true)]
		private static extern IntPtr GetRecorderInternal(IntPtr ptr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "ProfilerBindings::GetSamplerInternal", IsFreeFunction = true)]
		private static extern IntPtr GetSamplerInternal([NotNull] string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "ProfilerBindings::GetSamplerNamesInternal", IsFreeFunction = true)]
		private static extern int GetSamplerNamesInternal(List<string> namesScriptingPtr);
	}
}
