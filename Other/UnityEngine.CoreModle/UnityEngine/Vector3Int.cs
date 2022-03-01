using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	public struct Vector3Int : IEquatable<Vector3Int>
	{
		private int m_X;

		private int m_Y;

		private int m_Z;

		private static readonly Vector3Int s_Zero = new Vector3Int(0, 0, 0);

		private static readonly Vector3Int s_One = new Vector3Int(1, 1, 1);

		private static readonly Vector3Int s_Up = new Vector3Int(0, 1, 0);

		private static readonly Vector3Int s_Down = new Vector3Int(0, -1, 0);

		private static readonly Vector3Int s_Left = new Vector3Int(-1, 0, 0);

		private static readonly Vector3Int s_Right = new Vector3Int(1, 0, 0);

		public int x
		{
			get
			{
				return m_X;
			}
			set
			{
				m_X = value;
			}
		}

		public int y
		{
			get
			{
				return m_Y;
			}
			set
			{
				m_Y = value;
			}
		}

		public int z
		{
			get
			{
				return m_Z;
			}
			set
			{
				m_Z = value;
			}
		}

		public int this[int index]
		{
			get
			{
				return index switch
				{
					0 => x, 
					1 => y, 
					2 => z, 
					_ => throw new IndexOutOfRangeException(UnityString.Format("Invalid Vector3Int index addressed: {0}!", index)), 
				};
			}
			set
			{
				switch (index)
				{
				case 0:
					x = value;
					break;
				case 1:
					y = value;
					break;
				case 2:
					z = value;
					break;
				default:
					throw new IndexOutOfRangeException(UnityString.Format("Invalid Vector3Int index addressed: {0}!", index));
				}
			}
		}

		public float magnitude => Mathf.Sqrt(x * x + y * y + z * z);

		public int sqrMagnitude => x * x + y * y + z * z;

		public static Vector3Int zero => s_Zero;

		public static Vector3Int one => s_One;

		public static Vector3Int up => s_Up;

		public static Vector3Int down => s_Down;

		public static Vector3Int left => s_Left;

		public static Vector3Int right => s_Right;

		public Vector3Int(int x, int y, int z)
		{
			m_X = x;
			m_Y = y;
			m_Z = z;
		}

		public void Set(int x, int y, int z)
		{
			m_X = x;
			m_Y = y;
			m_Z = z;
		}

		public static float Distance(Vector3Int a, Vector3Int b)
		{
			return (a - b).magnitude;
		}

		public static Vector3Int Min(Vector3Int lhs, Vector3Int rhs)
		{
			return new Vector3Int(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		}

		public static Vector3Int Max(Vector3Int lhs, Vector3Int rhs)
		{
			return new Vector3Int(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		}

		public static Vector3Int Scale(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		public void Scale(Vector3Int scale)
		{
			x *= scale.x;
			y *= scale.y;
			z *= scale.z;
		}

		public void Clamp(Vector3Int min, Vector3Int max)
		{
			x = Math.Max(min.x, x);
			x = Math.Min(max.x, x);
			y = Math.Max(min.y, y);
			y = Math.Min(max.y, y);
			z = Math.Max(min.z, z);
			z = Math.Min(max.z, z);
		}

		public static implicit operator Vector3(Vector3Int v)
		{
			return new Vector3(v.x, v.y, v.z);
		}

		public static explicit operator Vector2Int(Vector3Int v)
		{
			return new Vector2Int(v.x, v.y);
		}

		public static Vector3Int FloorToInt(Vector3 v)
		{
			return new Vector3Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y), Mathf.FloorToInt(v.z));
		}

		public static Vector3Int CeilToInt(Vector3 v)
		{
			return new Vector3Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y), Mathf.CeilToInt(v.z));
		}

		public static Vector3Int RoundToInt(Vector3 v)
		{
			return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
		}

		public static Vector3Int operator +(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static Vector3Int operator -(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static Vector3Int operator *(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		public static Vector3Int operator *(Vector3Int a, int b)
		{
			return new Vector3Int(a.x * b, a.y * b, a.z * b);
		}

		public static bool operator ==(Vector3Int lhs, Vector3Int rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
		}

		public static bool operator !=(Vector3Int lhs, Vector3Int rhs)
		{
			return !(lhs == rhs);
		}

		public override bool Equals(object other)
		{
			if (!(other is Vector3Int))
			{
				return false;
			}
			return Equals((Vector3Int)other);
		}

		public bool Equals(Vector3Int other)
		{
			return this == other;
		}

		public override int GetHashCode()
		{
			int hashCode = y.GetHashCode();
			int hashCode2 = z.GetHashCode();
			return x.GetHashCode() ^ (hashCode << 4) ^ (hashCode >> 28) ^ (hashCode2 >> 4) ^ (hashCode2 << 28);
		}

		public override string ToString()
		{
			return UnityString.Format("({0}, {1}, {2})", x, y, z);
		}

		public string ToString(string format)
		{
			return UnityString.Format("({0}, {1}, {2})", x.ToString(format), y.ToString(format), z.ToString(format));
		}
	}
}
