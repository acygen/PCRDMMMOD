using System;

namespace UnityEngine.Experimental.Rendering
{
	[Flags]
	public enum VisibleLightFlags
	{
		None = 0x0,
		IntersectsNearPlane = 0x1,
		IntersectsFarPlane = 0x2
	}
}
