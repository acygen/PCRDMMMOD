using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	public struct Vector2Int : IEquatable<Vector2Int>
	{
		private int m_X;

		private int m_Y;

		private static readonly Vector2Int s_Zero = new Vector2Int(0, 0);

		private static readonly Vector2Int s_One = new Vector2Int(1, 1);

		private static readonly Vector2Int s_Up = new Vector2Int(0, 1);

		private static readonly Vector2Int s_Down = new Vector2Int(0, -1);

		private static readonly Vector2Int s_Left = new Vector2Int(-1, 0);

		private static readonly Vector2Int s_Right = new Vector2Int(1, 0);

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

		public int this[int index]
		{
			get
			{
				return index switch
				{
					0 => x, 
					1 => y, 
					_ => throw new IndexOutOfRangeException($"Invalid Vector2Int index addressed: {index}!"), 
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
				default:
					throw new IndexOutOfRangeException($"Invalid Vector2Int index addressed: {index}!");
				}
			}
		}

		public float magnitude => Mathf.Sqrt(x * x + y * y);

		public int sqrMagnitude => x * x + y * y;

		public static Vector2Int zero => s_Zero;

		public static Vector2Int one => s_One;

		public static Vector2Int up => s_Up;

		public static Vector2Int down => s_Down;

		public static Vector2Int left => s_Left;

		public static Vector2Int right => s_Right;

		public Vector2Int(int x, int y)
		{
			m_X = x;
			m_Y = y;
		}

		public void Set(int x, int y)
		{
			m_X = x;
			m_Y = y;
		}

		public static float Distance(Vector2Int a, Vector2Int b)
		{
			return (a - b).magnitude;
		}

		public static Vector2Int Min(Vector2Int lhs, Vector2Int rhs)
		{
			return new Vector2Int(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
		}

		public static Vector2Int Max(Vector2Int lhs, Vector2Int rhs)
		{
			return new Vector2Int(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
		}

		public static Vector2Int Scale(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x * b.x, a.y * b.y);
		}

		public void Scale(Vector2Int scale)
		{
			x *= scale.x;
			y *= scale.y;
		}

		public void Clamp(Vector2Int min, Vector2Int max)
		{
			x = Math.Max(min.x, x);
			x = Math.Min(max.x, x);
			y = Math.Max(min.y, y);
			y = Math.Min(max.y, y);
		}

		public static implicit operator Vector2(Vector2Int v)
		{
			return new Vector2(v.x, v.y);
		}

		public static explicit operator Vector3Int(Vector2Int v)
		{
			return new Vector3Int(v.x, v.y, 0);
		}

		public static Vector2Int FloorToInt(Vector2 v)
		{
			return new Vector2Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
		}

		public static Vector2Int CeilToInt(Vector2 v)
		{
			return new Vector2Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y));
		}

		public static Vector2Int RoundToInt(Vector2 v)
		{
			return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
		}

		public static Vector2Int operator +(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x + b.x, a.y + b.y);
		}

		public static Vector2Int operator -(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x - b.x, a.y - b.y);
		}

		public static Vector2Int operator *(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x * b.x, a.y * b.y);
		}

		public static Vector2Int operator *(Vector2Int a, int b)
		{
			return new Vector2Int(a.x * b, a.y * b);
		}

		public static bool operator ==(Vector2Int lhs, Vector2Int rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y;
		}

		public static bool operator !=(Vector2Int lhs, Vector2Int rhs)
		{
			return !(lhs == rhs);
		}

		public override bool Equals(object other)
		{
			if (!(other is Vector2Int))
			{
				return false;
			}
			return Equals((Vector2Int)other);
		}

		public bool Equals(Vector2Int other)
		{
			return x.Equals(other.x) && y.Equals(other.y);
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ (y.GetHashCode() << 2);
		}

		public override string ToString()
		{
			return UnityString.Format("({0}, {1})", x, y);
		}
	}
}
