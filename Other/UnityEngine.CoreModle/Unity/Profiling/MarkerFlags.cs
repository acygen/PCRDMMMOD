using System;
using UnityEngine.Bindings;

namespace Unity.Profiling
{
	[NativeHeader("Runtime/Profiler/ScriptBindings/ProfilerMarker.bindings.h")]
	[Flags]
	internal enum MarkerFlags
	{
		Default = 0x0,
		AvailabilityEditor = 0x4,
		AvailabilityNonDevelopment = 0x8,
		Warning = 0x10,
		VerbosityDebug = 0x400,
		VerbosityInternal = 0x800,
		VerbosityAdvanced = 0x1000
	}
}
