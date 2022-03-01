using System;

namespace UnityEngine
{
	[Flags]
	public enum DepthTextureMode
	{
		None = 0x0,
		Depth = 0x1,
		DepthNormals = 0x2,
		MotionVectors = 0x4
	}
}
