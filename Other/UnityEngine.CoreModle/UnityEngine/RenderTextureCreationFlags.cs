using System;

namespace UnityEngine
{
	[Flags]
	public enum RenderTextureCreationFlags
	{
		MipMap = 0x1,
		AutoGenerateMips = 0x2,
		SRGB = 0x4,
		EyeTexture = 0x8,
		EnableRandomWrite = 0x10,
		CreatedFromScript = 0x20,
		AllowVerticalFlip = 0x80,
		NoResolvedColorSurface = 0x100,
		DynamicallyScalable = 0x400,
		BindMS = 0x800
	}
}
