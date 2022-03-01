using System;

namespace UnityEngine
{
	[Flags]
	public enum CameraType
	{
		Game = 0x1,
		SceneView = 0x2,
		Preview = 0x4,
		VR = 0x8,
		Reflection = 0x10
	}
}
