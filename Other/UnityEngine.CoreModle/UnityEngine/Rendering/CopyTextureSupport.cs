using System;

namespace UnityEngine.Rendering
{
	[Flags]
	public enum CopyTextureSupport
	{
		None = 0x0,
		Basic = 0x1,
		Copy3D = 0x2,
		DifferentTypes = 0x4,
		TextureToRT = 0x8,
		RTToTexture = 0x10
	}
}
