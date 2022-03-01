using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Scripting
{
	[VisibleToOtherModules]
	[NativeHeader("Runtime/Scripting/GarbageCollector.h")]
	public static class GarbageCollector
	{
		public enum Mode
		{
			Disabled,
			Enabled
		}

		public static Mode GCMode
		{
			get
			{
				return GetMode();
			}
			set
			{
				if (value != GetMode())
				{
					SetMode(value);
					if (GarbageCollector.GCModeChanged != null)
					{
						GarbageCollector.GCModeChanged(value);
					}
				}
			}
		}

		public static event Action<Mode> GCModeChanged;

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetMode(Mode mode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern Mode GetMode();
	}
}
