using System;

namespace UnityEngine
{
	[Flags]
	public enum RenderTextureMemoryless
	{
		None = 0x0,
		Color = 0x1,
		Depth = 0x2,
		MSAA = 0x4
	}
}
