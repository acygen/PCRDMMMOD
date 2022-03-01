using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	[NativeHeader("Runtime/Camera/ReflectionProbes.h")]
	internal class BuiltinRuntimeReflectionSystem : IScriptableRuntimeReflectionSystem, IDisposable
	{
		public bool TickRealtimeProbes()
		{
			return BuiltinUpdate();
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		private void Dispose(bool disposing)
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetReflectionProbes()", Type = StaticAccessorType.Dot)]
		private static extern bool BuiltinUpdate();

		[RequiredByNativeCode]
		private static BuiltinRuntimeReflectionSystem Internal_BuiltinRuntimeReflectionSystem_New()
		{
			return new BuiltinRuntimeReflectionSystem();
		}
	}
}
