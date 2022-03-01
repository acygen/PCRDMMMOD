namespace UnityEngine.Experimental.GlobalIllumination
{
	public struct DirectionalLight
	{
		public int instanceID;

		public bool shadow;

		public LightMode mode;

		public Vector3 direction;

		public LinearColor color;

		public LinearColor indirectColor;

		public float penumbraWidthRadian;
	}
}
