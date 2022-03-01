using System;

namespace UnityEngine.Experimental.Rendering
{
	[Flags]
	public enum CullFlag
	{
		None = 0x0,
		ForceEvenIfCameraIsNotActive = 0x1,
		OcclusionCull = 0x2,
		NeedsLighting = 0x4,
		NeedsReflectionProbes = 0x8,
		Stereo = 0x10,
		DisablePerObjectCulling = 0x20,
		ShadowCasters = 0x40
	}
}
