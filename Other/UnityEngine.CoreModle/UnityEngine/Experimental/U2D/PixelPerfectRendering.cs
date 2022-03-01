using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.U2D
{
	[NativeHeader("Runtime/2D/Common/PixelSnapping.h")]
	public static class PixelPerfectRendering
	{
		public static extern float pixelSnapSpacing
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetPixelSnapSpacing")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("SetPixelSnapSpacing")]
			set;
		}
	}
}
