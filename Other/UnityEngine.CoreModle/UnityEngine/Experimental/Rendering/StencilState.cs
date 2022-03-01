using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	public struct StencilState
	{
		private byte m_Enabled;

		private byte m_ReadMask;

		private byte m_WriteMask;

		private byte m_Padding;

		private byte m_CompareFunctionFront;

		private byte m_PassOperationFront;

		private byte m_FailOperationFront;

		private byte m_ZFailOperationFront;

		private byte m_CompareFunctionBack;

		private byte m_PassOperationBack;

		private byte m_FailOperationBack;

		private byte m_ZFailOperationBack;

		public static StencilState Default => new StencilState(enabled: false, byte.MaxValue, byte.MaxValue, CompareFunction.Always, StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);

		public bool enabled
		{
			get
			{
				return Convert.ToBoolean(m_Enabled);
			}
			set
			{
				m_Enabled = Convert.ToByte(value);
			}
		}

		public byte readMask
		{
			get
			{
				return m_ReadMask;
			}
			set
			{
				m_ReadMask = value;
			}
		}

		public byte writeMask
		{
			get
			{
				return m_WriteMask;
			}
			set
			{
				m_WriteMask = value;
			}
		}

		public CompareFunction compareFunction
		{
			set
			{
				compareFunctionFront = value;
				compareFunctionBack = value;
			}
		}

		public StencilOp passOperation
		{
			set
			{
				passOperationFront = value;
				passOperationBack = value;
			}
		}

		public StencilOp failOperation
		{
			set
			{
				failOperationFront = value;
				failOperationBack = value;
			}
		}

		public StencilOp zFailOperation
		{
			set
			{
				zFailOperationFront = value;
				zFailOperationBack = value;
			}
		}

		public CompareFunction compareFunctionFront
		{
			get
			{
				return (CompareFunction)m_CompareFunctionFront;
			}
			set
			{
				m_CompareFunctionFront = (byte)value;
			}
		}

		public StencilOp passOperationFront
		{
			get
			{
				return (StencilOp)m_PassOperationFront;
			}
			set
			{
				m_PassOperationFront = (byte)value;
			}
		}

		public StencilOp failOperationFront
		{
			get
			{
				return (StencilOp)m_FailOperationFront;
			}
			set
			{
				m_FailOperationFront = (byte)value;
			}
		}

		public StencilOp zFailOperationFront
		{
			get
			{
				return (StencilOp)m_ZFailOperationFront;
			}
			set
			{
				m_ZFailOperationFront = (byte)value;
			}
		}

		public CompareFunction compareFunctionBack
		{
			get
			{
				return (CompareFunction)m_CompareFunctionBack;
			}
			set
			{
				m_CompareFunctionBack = (byte)value;
			}
		}

		public StencilOp passOperationBack
		{
			get
			{
				return (StencilOp)m_PassOperationBack;
			}
			set
			{
				m_PassOperationBack = (byte)value;
			}
		}

		public StencilOp failOperationBack
		{
			get
			{
				return (StencilOp)m_FailOperationBack;
			}
			set
			{
				m_FailOperationBack = (byte)value;
			}
		}

		public StencilOp zFailOperationBack
		{
			get
			{
				return (StencilOp)m_ZFailOperationBack;
			}
			set
			{
				m_ZFailOperationBack = (byte)value;
			}
		}

		public StencilState(bool enabled = false, byte readMask = byte.MaxValue, byte writeMask = byte.MaxValue, CompareFunction compareFunction = CompareFunction.Always, StencilOp passOperation = StencilOp.Keep, StencilOp failOperation = StencilOp.Keep, StencilOp zFailOperation = StencilOp.Keep)
			: this(enabled, readMask, writeMask, compareFunction, passOperation, failOperation, zFailOperation, compareFunction, passOperation, failOperation, zFailOperation)
		{
		}

		public StencilState(bool enabled, byte readMask, byte writeMask, CompareFunction compareFunctionFront, StencilOp passOperationFront, StencilOp failOperationFront, StencilOp zFailOperationFront, CompareFunction compareFunctionBack, StencilOp passOperationBack, StencilOp failOperationBack, StencilOp zFailOperationBack)
		{
			m_Enabled = Convert.ToByte(enabled);
			m_ReadMask = readMask;
			m_WriteMask = writeMask;
			m_Padding = 0;
			m_CompareFunctionFront = (byte)compareFunctionFront;
			m_PassOperationFront = (byte)passOperationFront;
			m_FailOperationFront = (byte)failOperationFront;
			m_ZFailOperationFront = (byte)zFailOperationFront;
			m_CompareFunctionBack = (byte)compareFunctionBack;
			m_PassOperationBack = (byte)passOperationBack;
			m_FailOperationBack = (byte)failOperationBack;
			m_ZFailOperationBack = (byte)zFailOperationBack;
		}
	}
}
