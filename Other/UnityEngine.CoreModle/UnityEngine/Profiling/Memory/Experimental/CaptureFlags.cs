using System;

namespace UnityEngine.Profiling.Memory.Experimental
{
	[Flags]
	public enum CaptureFlags : uint
	{
		ManagedObjects = 0x1u,
		NativeObjects = 0x2u,
		NativeAllocations = 0x4u,
		NativeAllocationSites = 0x8u,
		NativeStackTraces = 0x10u
	}
}
