using System;

namespace UnityEngine.Experimental.Rendering
{
	[Flags]
	public enum DrawRendererFlags
	{
		None = 0x0,
		EnableDynamicBatching = 0x1,
		EnableInstancing = 0x2
	}
}
