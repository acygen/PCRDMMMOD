using System;

namespace UnityEngine
{
	[Flags]
	public enum MaterialGlobalIlluminationFlags
	{
		None = 0x0,
		RealtimeEmissive = 0x1,
		BakedEmissive = 0x2,
		EmissiveIsBlack = 0x4,
		AnyEmissive = 0x3
	}
}
