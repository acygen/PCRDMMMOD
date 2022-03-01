using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[Serializable]
	[UsedByNativeCode]
	public struct CustomRenderTextureUpdateZone
	{
		public Vector3 updateZoneCenter;

		public Vector3 updateZoneSize;

		public float rotation;

		public int passIndex;

		public bool needSwap;
	}
}
