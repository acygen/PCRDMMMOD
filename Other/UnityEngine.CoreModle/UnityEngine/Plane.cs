using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	public struct Plane
	{
		private Vector3 m_Normal;

		private float m_Distance;

		public Vector3 normal
		{
			get
			{
				return m_Normal;
			}
			set
			{
				m_Normal = value;
			}
		}

		public float distance
		{
			get
			{
				return m_Distance;
			}
			set
			{
				m_Distance = value;
			}
		}

		public Plane flipped => new Plane(-m_Normal, 0f - m_Distance);

		public Plane(Vector3 inNormal, Vector3 inPoint)
		{
			m_Normal = Vector3.Normalize(inNormal);
			m_Distance = 0f - Vector3.Dot(m_Normal, inPoint);
		}

		public Plane(Vector3 inNormal, float d)
		{
			m_Normal = Vector3.Normalize(inNormal);
			m_Distance = d;
		}

		public Plane(Vector3 a, Vector3 b, Vector3 c)
		{
			m_Normal = Vector3.Normalize(Vector3.Cross(b - a, c - a));
			m_Distance = 0f - Vector3.Dot(m_Normal, a);
		}

		public void SetNormalAndPosition(Vector3 inNormal, Vector3 inPoint)
		{
			m_Normal = Vector3.Normalize(inNormal);
			m_Distance = 0f - Vector3.Dot(inNormal, inPoint);
		}

		public void Set3Points(Vector3 a, Vector3 b, Vector3 c)
		{
			m_Normal = Vector3.Normalize(Vector3.Cross(b - a, c - a));
			m_Distance = 0f - Vector3.Dot(m_Normal, a);
		}

		public void Flip()
		{
			m_Normal = -m_Normal;
			m_Distance = 0f - m_Distance;
		}

		public void Translate(Vector3 translation)
		{
			m_Distance += Vector3.Dot(m_Normal, translation);
		}

		public static Plane Translate(Plane plane, Vector3 translation)
		{
			return new Plane(plane.m_Normal, plane.m_Distance += Vector3.Dot(plane.m_Normal, translation));
		}

		public Vector3 ClosestPointOnPlane(Vector3 point)
		{
			float num = Vector3.Dot(m_Normal, point) + m_Distance;
			return point - m_Normal * num;
		}

		public float GetDistanceToPoint(Vector3 point)
		{
			return Vector3.Dot(m_Normal, point) + m_Distance;
		}

		public bool GetSide(Vector3 point)
		{
			return Vector3.Dot(m_Normal, point) + m_Distance > 0f;
		}

		public bool SameSide(Vector3 inPt0, Vector3 inPt1)
		{
			float distanceToPoint = GetDistanceToPoint(inPt0);
			float distanceToPoint2 = GetDistanceToPoint(inPt1);
			return (distanceToPoint > 0f && distanceToPoint2 > 0f) || (distanceToPoint <= 0f && distanceToPoint2 <= 0f);
		}

		public bool Raycast(Ray ray, out float enter)
		{
			float num = Vector3.Dot(ray.direction, m_Normal);
			float num2 = 0f - Vector3.Dot(ray.origin, m_Normal) - m_Distance;
			if (Mathf.Approximately(num, 0f))
			{
				enter = 0f;
				return false;
			}
			enter = num2 / num;
			return enter > 0f;
		}

		public override string ToString()
		{
			return UnityString.Format("(normal:({0:F1}, {1:F1}, {2:F1}), distance:{3:F1})", m_Normal.x, m_Normal.y, m_Normal.z, m_Distance);
		}

		public string ToString(string format)
		{
			return UnityString.Format("(normal:({0}, {1}, {2}), distance:{3})", m_Normal.x.ToString(format), m_Normal.y.ToString(format), m_Normal.z.ToString(format), m_Distance.ToString(format));
		}
	}
}
