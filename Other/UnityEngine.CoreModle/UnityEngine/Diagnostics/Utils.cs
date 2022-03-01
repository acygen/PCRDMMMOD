using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Diagnostics
{
	[NativeHeader("Runtime/Export/Diagnostics/DiagnosticsUtils.bindings.h")]
	public static class Utils
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("DiagnosticsUtils_Bindings::ForceCrash", ThrowsException = true)]
		public static extern void ForceCrash(ForcedCrashCategory crashCategory);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("DiagnosticsUtils_Bindings::NativeAssert")]
		public static extern void NativeAssert(string message);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("DiagnosticsUtils_Bindings::NativeError")]
		public static extern void NativeError(string message);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("DiagnosticsUtils_Bindings::NativeWarning")]
		public static extern void NativeWarning(string message);
	}
}
