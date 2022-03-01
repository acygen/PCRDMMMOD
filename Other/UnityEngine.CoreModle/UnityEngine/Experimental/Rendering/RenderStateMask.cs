using System;

namespace UnityEngine.Experimental.Rendering
{
	[Flags]
	public enum RenderStateMask
	{
		Nothing = 0x0,
		Blend = 0x1,
		Raster = 0x2,
		Depth = 0x4,
		Stencil = 0x8,
		Everything = 0xF
	}
}
