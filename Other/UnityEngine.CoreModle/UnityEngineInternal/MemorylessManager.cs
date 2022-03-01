using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngineInternal
{
	[NativeHeader("Runtime/Misc/PlayerSettings.h")]
	public class MemorylessManager
	{
		public static MemorylessMode depthMemorylessMode
		{
			get
			{
				return GetFramebufferDepthMemorylessMode();
			}
			set
			{
				SetFramebufferDepthMemorylessMode(value);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "GetFramebufferDepthMemorylessMode")]
		[StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
		internal static extern MemorylessMode GetFramebufferDepthMemorylessMode();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetPlayerSettings()", StaticAccessorType.Dot)]
		[NativeMethod(Name = "SetFramebufferDepthMemorylessMode")]
		internal static extern void SetFramebufferDepthMemorylessMode(MemorylessMode mode);
	}
}
