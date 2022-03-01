using System;

namespace UnityEngine.Rendering
{
	[Flags]
	public enum ColorWriteMask
	{
		Alpha = 0x1,
		Blue = 0x2,
		Green = 0x4,
		Red = 0x8,
		All = 0xF
	}
}
