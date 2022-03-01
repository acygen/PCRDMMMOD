using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Windows
{
	public static class CrashReporting
	{
		public static extern string crashReportFolder
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[ThreadSafe]
			[NativeHeader("PlatformDependent/WinPlayer/Bindings/CrashReportingBindings.h")]
			get;
		}
	}
}
