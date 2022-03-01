using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	public struct RectInt : IEquatable<RectInt>
	{
		public struct PositionEnumerator : IEnumerator<Vector2Int>, IEnumerator, IDisposable
		{
			private readonly Vector2Int _min;

			private readonly Vector2Int _max;

			private Vector2Int _current;

			object IEnumerator.Current => Current;

			public Vector2Int Current => _current;

			public PositionEnumerator(Vector2Int min, Vector2Int max)
			{
				_min = (_current = min);
				_max = max;
				Reset();
			}

			public PositionEnumerator GetEnumerator()
			{
				return this;
			}

			public bool MoveNext()
			{
				if (_current.y >= _max.y)
				{
					return false;
				}
				_current.x++;
				if (_current.x >= _max.x)
				{
					_current.x = _min.x;
					_current.y++;
					if (_current.y >= _max.y)
					{
						return false;
					}
				}
				return true;
			}

			public void Reset()
			{
				_current = _min;
				_current.x--;
			}

			void IDisposable.Dispose()
			{
			}
		}

		private int m_XMin;

		private int m_YMin;

		private int m_Width;

		private int m_Height;

		public int x
		{
			get
			{
				return m_XMin;
			}
			set
			{
				m_XMin = value;
			}
		}

		public int y
		{
			get
			{
				return m_YMin;
			}
			set
			{
				m_YMin = value;
			}
		}

		public Vector2 center => new Vector2((float)x + (float)m_Width / 2f, (float)y + (float)m_Height / 2f);

		public Vector2Int min
		{
			get
			{
				return new Vector2Int(xMin, yMin);
			}
			set
			{
				xMin = value.x;
				yMin = value.y;
			}
		}

		public Vector2Int max
		{
			get
			{
				return new Vector2Int(xMax, yMax);
			}
			set
			{
				xMax = value.x;
				yMax = value.y;
			}
		}

		public int width
		{
			get
			{
				return m_Width;
			}
			set
			{
				m_Width = value;
			}
		}

		public int height
		{
			get
			{
				return m_Height;
			}
			set
			{
				m_Height = value;
			}
		}

		public int xMin
		{
			get
			{
				return Math.Min(m_XMin, m_XMin + m_Width);
			}
			set
			{
				int num = xMax;
				m_XMin = value;
				m_Width = num - m_XMin;
			}
		}

		public int yMin
		{
			get
			{
				return Math.Min(m_YMin, m_YMin + m_Height);
			}
			set
			{
				int num = yMax;
				m_YMin = value;
				m_Height = num - m_YMin;
			}
		}

		public int xMax
		{
			get
			{
				return Math.Max(m_XMin, m_XMin + m_Width);
			}
			set
			{
				m_Width = value - m_XMin;
			}
		}

		public int yMax
		{
			get
			{
				return Math.Max(m_YMin, m_YMin + m_Height);
			}
			set
			{
				m_Height = value - m_YMin;
			}
		}

		public Vector2Int position
		{
			get
			{
				return new Vector2Int(m_XMin, m_YMin);
			}
			set
			{
				m_XMin = value.x;
				m_YMin = value.y;
			}
		}

		public Vector2Int size
		{
			get
			{
				return new Vector2Int(m_Width, m_Height);
			}
			set
			{
				m_Width = value.x;
				m_Height = value.y;
			}
		}

		public PositionEnumerator allPositionsWithin => new PositionEnumerator(min, max);

		public RectInt(int xMin, int yMin, int width, int height)
		{
			m_XMin = xMin;
			m_YMin = yMin;
			m_Width = width;
			m_Height = height;
		}

		public RectInt(Vector2Int position, Vector2Int size)
		{
			m_XMin = position.x;
			m_YMin = position.y;
			m_Width = size.x;
			m_Height = size.y;
		}

		public void SetMinMax(Vector2Int minPosition, Vector2Int maxPosition)
		{
			min = minPosition;
			max = maxPosition;
		}

		public void ClampToBounds(RectInt bounds)
		{
			position = new Vector2Int(Math.Max(Math.Min(bounds.xMax, position.x), bounds.xMin), Math.Max(Math.Min(bounds.yMax, position.y), bounds.yMin));
			size = new Vector2Int(Math.Min(bounds.xMax - position.x, size.x), Math.Min(bounds.yMax - position.y, size.y));
		}

		public bool Contains(Vector2Int position)
		{
			return position.x >= xMin && position.y >= yMin && position.x < xMax && position.y < yMax;
		}

		public override string ToString()
		{
			return UnityString.Format("(x:{0}, y:{1}, width:{2}, height:{3})", x, y, width, height);
		}

		public bool Equals(RectInt other)
		{
			return m_XMin == other.m_XMin && m_YMin == other.m_YMin && m_Width == other.m_Width && m_Height == other.m_Height;
		}
	}
}
