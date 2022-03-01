namespace UnityEngine
{
	public struct AccelerationEvent
	{
		internal float x;

		internal float y;

		internal float z;

		internal float m_TimeDelta;

		public Vector3 acceleration => new Vector3(x, y, z);

		public float deltaTime => m_TimeDelta;
	}
}
