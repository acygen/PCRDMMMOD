using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	public struct DepthState
	{
		private byte m_WriteEnabled;

		private sbyte m_CompareFunction;

		public static DepthState Default => new DepthState(writeEnabled: true, CompareFunction.Less);

		public bool writeEnabled
		{
			get
			{
				return Convert.ToBoolean(m_WriteEnabled);
			}
			set
			{
				m_WriteEnabled = Convert.ToByte(value);
			}
		}

		public CompareFunction compareFunction
		{
			get
			{
				return (CompareFunction)m_CompareFunction;
			}
			set
			{
				m_CompareFunction = (sbyte)value;
			}
		}

		public DepthState(bool writeEnabled = true, CompareFunction compareFunction = CompareFunction.Less)
		{
			m_WriteEnabled = Convert.ToByte(writeEnabled);
			m_CompareFunction = (sbyte)compareFunction;
		}
	}
}
