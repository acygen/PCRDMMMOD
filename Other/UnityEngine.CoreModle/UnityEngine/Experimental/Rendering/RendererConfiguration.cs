using System;

namespace UnityEngine.Experimental.Rendering
{
	[Flags]
	public enum RendererConfiguration
	{
		None = 0x0,
		PerObjectLightProbe = 0x1,
		PerObjectReflectionProbes = 0x2,
		PerObjectLightProbeProxyVolume = 0x4,
		PerObjectLightmaps = 0x8,
		ProvideLightIndices = 0x10,
		PerObjectMotionVectors = 0x20,
		PerObjectLightIndices8 = 0x40,
		ProvideReflectionProbeIndices = 0x80,
		PerObjectOcclusionProbe = 0x100,
		PerObjectOcclusionProbeProxyVolume = 0x200,
		PerObjectShadowMask = 0x400
	}
}
