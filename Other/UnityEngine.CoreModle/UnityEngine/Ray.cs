namespace UnityEngine
{
	public struct Ray
	{
		private Vector3 m_Origin;

		private Vector3 m_Direction;

		public Vector3 origin
		{
			get
			{
				return m_Origin;
			}
			set
			{
				m_Origin = value;
			}
		}

		public Vector3 direction
		{
			get
			{
				return m_Direction;
			}
			set
			{
				m_Direction = value.normalized;
			}
		}

		public Ray(Vector3 origin, Vector3 direction)
		{
			m_Origin = origin;
			m_Direction = direction.normalized;
		}

		public Vector3 GetPoint(float distance)
		{
			return m_Origin + m_Direction * distance;
		}

		public override string ToString()
		{
			return UnityString.Format("Origin: {0}, Dir: {1}", m_Origin, m_Direction);
		}

		public string ToString(string format)
		{
			return UnityString.Format("Origin: {0}, Dir: {1}", m_Origin.ToString(format), m_Direction.ToString(format));
		}
	}
}
