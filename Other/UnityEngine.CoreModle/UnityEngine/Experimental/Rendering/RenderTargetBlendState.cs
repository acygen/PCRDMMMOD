using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	public struct RenderTargetBlendState
	{
		private byte m_WriteMask;

		private byte m_SourceColorBlendMode;

		private byte m_DestinationColorBlendMode;

		private byte m_SourceAlphaBlendMode;

		private byte m_DestinationAlphaBlendMode;

		private byte m_ColorBlendOperation;

		private byte m_AlphaBlendOperation;

		private byte m_Padding;

		public static RenderTargetBlendState Default => new RenderTargetBlendState(ColorWriteMask.All, BlendMode.One, BlendMode.Zero, BlendMode.One, BlendMode.Zero, BlendOp.Add, BlendOp.Add);

		public ColorWriteMask writeMask
		{
			get
			{
				return (ColorWriteMask)m_WriteMask;
			}
			set
			{
				m_WriteMask = (byte)value;
			}
		}

		public BlendMode sourceColorBlendMode
		{
			get
			{
				return (BlendMode)m_SourceColorBlendMode;
			}
			set
			{
				m_SourceColorBlendMode = (byte)value;
			}
		}

		public BlendMode destinationColorBlendMode
		{
			get
			{
				return (BlendMode)m_DestinationColorBlendMode;
			}
			set
			{
				m_DestinationColorBlendMode = (byte)value;
			}
		}

		public BlendMode sourceAlphaBlendMode
		{
			get
			{
				return (BlendMode)m_SourceAlphaBlendMode;
			}
			set
			{
				m_SourceAlphaBlendMode = (byte)value;
			}
		}

		public BlendMode destinationAlphaBlendMode
		{
			get
			{
				return (BlendMode)m_DestinationAlphaBlendMode;
			}
			set
			{
				m_DestinationAlphaBlendMode = (byte)value;
			}
		}

		public BlendOp colorBlendOperation
		{
			get
			{
				return (BlendOp)m_ColorBlendOperation;
			}
			set
			{
				m_ColorBlendOperation = (byte)value;
			}
		}

		public BlendOp alphaBlendOperation
		{
			get
			{
				return (BlendOp)m_AlphaBlendOperation;
			}
			set
			{
				m_AlphaBlendOperation = (byte)value;
			}
		}

		public RenderTargetBlendState(ColorWriteMask writeMask = ColorWriteMask.All, BlendMode sourceColorBlendMode = BlendMode.One, BlendMode destinationColorBlendMode = BlendMode.Zero, BlendMode sourceAlphaBlendMode = BlendMode.One, BlendMode destinationAlphaBlendMode = BlendMode.Zero, BlendOp colorBlendOperation = BlendOp.Add, BlendOp alphaBlendOperation = BlendOp.Add)
		{
			m_WriteMask = (byte)writeMask;
			m_SourceColorBlendMode = (byte)sourceColorBlendMode;
			m_DestinationColorBlendMode = (byte)destinationColorBlendMode;
			m_SourceAlphaBlendMode = (byte)sourceAlphaBlendMode;
			m_DestinationAlphaBlendMode = (byte)destinationAlphaBlendMode;
			m_ColorBlendOperation = (byte)colorBlendOperation;
			m_AlphaBlendOperation = (byte)alphaBlendOperation;
			m_Padding = 0;
		}
	}
}
