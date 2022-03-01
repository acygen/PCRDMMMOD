using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Profiling
{
	[NativeHeader("Runtime/Utilities/MemoryUtilities.h")]
	[NativeHeader("Runtime/Profiler/Profiler.h")]
	[NativeHeader("Runtime/Profiler/ScriptBindings/Profiler.bindings.h")]
	[NativeHeader("Runtime/Allocator/MemoryManager.h")]
	[UsedByNativeCode]
	[MovedFrom("UnityEngine")]
	[NativeHeader("Runtime/ScriptingBackend/ScriptingApi.h")]
	public sealed class Profiler
	{
		internal const uint invalidProfilerArea = uint.MaxValue;

		public static extern bool supported
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "profiler_is_available", IsFreeFunction = true)]
			get;
		}

		[StaticAccessor("ProfilerBindings", StaticAccessorType.DoubleColon)]
		public static extern string logFile
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool enableBinaryLog
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "ProfilerBindings::IsBinaryLogEnabled", IsFreeFunction = true)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "ProfilerBindings::SetBinaryLogEnabled", IsFreeFunction = true)]
			set;
		}

		public static extern int maxUsedMemory
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "ProfilerBindings::GetMaxUsedMemory", IsFreeFunction = true)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "ProfilerBindings::SetMaxUsedMemory", IsFreeFunction = true)]
			set;
		}

		public static extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeConditional("ENABLE_PROFILER")]
			[NativeMethod(Name = "profiler_is_enabled", IsFreeFunction = true)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "ProfilerBindings::SetProfilerEnabled", IsFreeFunction = true)]
			set;
		}

		public static int areaCount => Enum.GetNames(typeof(ProfilerArea)).Length;

		[Obsolete("maxNumberOfSamplesPerFrame has been depricated. Use maxUsedMemory instead")]
		public static int maxNumberOfSamplesPerFrame
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		[Obsolete("usedHeapSize has been deprecated since it is limited to 4GB. Please use usedHeapSizeLong instead.")]
		public static uint usedHeapSize => (uint)usedHeapSizeLong;

		public static extern long usedHeapSizeLong
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "GetUsedHeapSize", IsFreeFunction = true)]
			get;
		}

		private Profiler()
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Conditional("ENABLE_PROFILER")]
		[FreeFunction("profiler_set_area_enabled")]
		public static extern void SetAreaEnabled(ProfilerArea area, bool enabled);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("profiler_is_area_enabled")]
		[NativeConditional("ENABLE_PROFILER")]
		public static extern bool GetAreaEnabled(ProfilerArea area);

		[Conditional("UNITY_EDITOR")]
		public static void AddFramesFromFile(string file)
		{
			if (string.IsNullOrEmpty(file))
			{
				Debug.LogError("AddFramesFromFile: Invalid or empty path");
			}
			else
			{
				AddFramesFromFile_Internal(file, keepExistingFrames: true);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("profiling::GetProfilerSessionPtr()", StaticAccessorType.Arrow)]
		[NativeHeader("Modules/ProfilerEditor/Public/ProfilerSession.h")]
		[NativeConditional("ENABLE_PROFILER && UNITY_EDITOR")]
		[NativeMethod(Name = "LoadFromFile")]
		private static extern void AddFramesFromFile_Internal(string file, bool keepExistingFrames);

		[Conditional("ENABLE_PROFILER")]
		public static void BeginThreadProfiling(string threadGroupName, string threadName)
		{
			if (string.IsNullOrEmpty(threadGroupName))
			{
				throw new ArgumentException("Argument should be a valid string", "threadGroupName");
			}
			if (string.IsNullOrEmpty(threadName))
			{
				throw new ArgumentException("Argument should be a valid string", "threadName");
			}
			BeginThreadProfilingInternal(threadGroupName, threadName);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "ProfilerBindings::BeginThreadProfiling", IsFreeFunction = true, IsThreadSafe = true)]
		[NativeConditional("ENABLE_PROFILER")]
		private static extern void BeginThreadProfilingInternal(string threadGroupName, string threadName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "ProfilerBindings::EndThreadProfiling", IsFreeFunction = true, IsThreadSafe = true)]
		[NativeConditional("ENABLE_PROFILER")]
		public static extern void EndThreadProfiling();

		[Conditional("ENABLE_PROFILER")]
		public static void BeginSample(string name)
		{
			ValidateArguments(name);
			BeginSampleImpl(name, null);
		}

		[Conditional("ENABLE_PROFILER")]
		public static void BeginSample(string name, Object targetObject)
		{
			ValidateArguments(name);
			BeginSampleImpl(name, targetObject);
		}

		private static void ValidateArguments(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Argument should be a valid string.", "name");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "ProfilerBindings::BeginSample", IsFreeFunction = true, IsThreadSafe = true)]
		private static extern void BeginSampleImpl(string name, Object targetObject);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "ProfilerBindings::EndSample", IsFreeFunction = true, IsThreadSafe = true)]
		[Conditional("ENABLE_PROFILER")]
		public static extern void EndSample();

		[Obsolete("GetRuntimeMemorySize has been deprecated since it is limited to 2GB. Please use GetRuntimeMemorySizeLong() instead.")]
		public static int GetRuntimeMemorySize(Object o)
		{
			return (int)GetRuntimeMemorySizeLong(o);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "ProfilerBindings::GetRuntimeMemorySizeLong", IsFreeFunction = true)]
		[NativeConditional("ENABLE_PROFILER")]
		public static extern long GetRuntimeMemorySizeLong(Object o);

		[Obsolete("GetMonoHeapSize has been deprecated since it is limited to 4GB. Please use GetMonoHeapSizeLong() instead.")]
		public static uint GetMonoHeapSize()
		{
			return (uint)GetMonoHeapSizeLong();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "scripting_gc_get_heap_size", IsFreeFunction = true)]
		public static extern long GetMonoHeapSizeLong();

		[Obsolete("GetMonoUsedSize has been deprecated since it is limited to 4GB. Please use GetMonoUsedSizeLong() instead.")]
		public static uint GetMonoUsedSize()
		{
			return (uint)GetMonoUsedSizeLong();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "scripting_gc_get_used_size", IsFreeFunction = true)]
		public static extern long GetMonoUsedSizeLong();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		public static extern bool SetTempAllocatorRequestedSize(uint size);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		public static extern uint GetTempAllocatorSize();

		[Obsolete("GetTotalAllocatedMemory has been deprecated since it is limited to 4GB. Please use GetTotalAllocatedMemoryLong() instead.")]
		public static uint GetTotalAllocatedMemory()
		{
			return (uint)GetTotalAllocatedMemoryLong();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[NativeMethod(Name = "GetTotalAllocatedMemory")]
		public static extern long GetTotalAllocatedMemoryLong();

		[Obsolete("GetTotalUnusedReservedMemory has been deprecated since it is limited to 4GB. Please use GetTotalUnusedReservedMemoryLong() instead.")]
		public static uint GetTotalUnusedReservedMemory()
		{
			return (uint)GetTotalUnusedReservedMemoryLong();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "GetTotalUnusedReservedMemory")]
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		public static extern long GetTotalUnusedReservedMemoryLong();

		[Obsolete("GetTotalReservedMemory has been deprecated since it is limited to 4GB. Please use GetTotalReservedMemoryLong() instead.")]
		public static uint GetTotalReservedMemory()
		{
			return (uint)GetTotalReservedMemoryLong();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "GetTotalReservedMemory")]
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		public static extern long GetTotalReservedMemoryLong();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_PROFILER")]
		[NativeMethod(Name = "GetRegisteredGFXDriverMemory")]
		public static extern long GetAllocatedMemoryForGraphicsDriver();
	}
}
