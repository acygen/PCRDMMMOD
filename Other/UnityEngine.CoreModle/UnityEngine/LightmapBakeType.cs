using System;

namespace UnityEngine
{
	[Flags]
	public enum LightmapBakeType
	{
		Realtime = 0x4,
		Baked = 0x2,
		Mixed = 0x1
	}
}
