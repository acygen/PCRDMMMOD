using System;

namespace UnityEngine.Experimental.Rendering
{
	[Flags]
	public enum TextureCreationFlags
	{
		None = 0x0,
		MipChain = 0x1,
		Crunch = 0x40
	}
}
