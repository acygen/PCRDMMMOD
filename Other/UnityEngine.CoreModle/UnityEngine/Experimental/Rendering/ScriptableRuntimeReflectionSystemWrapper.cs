using System;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	[RequiredByNativeCode]
	internal class ScriptableRuntimeReflectionSystemWrapper
	{
		internal IScriptableRuntimeReflectionSystem implementation { get; set; }

		[RequiredByNativeCode]
		private unsafe void Internal_ScriptableRuntimeReflectionSystemWrapper_TickRealtimeProbes(IntPtr result)
		{
			*(bool*)(void*)result = implementation != null && implementation.TickRealtimeProbes();
		}
	}
}
