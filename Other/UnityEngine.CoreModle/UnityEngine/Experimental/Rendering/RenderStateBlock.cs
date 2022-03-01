namespace UnityEngine.Experimental.Rendering
{
	public struct RenderStateBlock
	{
		private BlendState m_BlendState;

		private RasterState m_RasterState;

		private DepthState m_DepthState;

		private StencilState m_StencilState;

		private int m_StencilReference;

		private RenderStateMask m_Mask;

		public BlendState blendState
		{
			get
			{
				return m_BlendState;
			}
			set
			{
				m_BlendState = value;
			}
		}

		public RasterState rasterState
		{
			get
			{
				return m_RasterState;
			}
			set
			{
				m_RasterState = value;
			}
		}

		public DepthState depthState
		{
			get
			{
				return m_DepthState;
			}
			set
			{
				m_DepthState = value;
			}
		}

		public StencilState stencilState
		{
			get
			{
				return m_StencilState;
			}
			set
			{
				m_StencilState = value;
			}
		}

		public int stencilReference
		{
			get
			{
				return m_StencilReference;
			}
			set
			{
				m_StencilReference = value;
			}
		}

		public RenderStateMask mask
		{
			get
			{
				return m_Mask;
			}
			set
			{
				m_Mask = value;
			}
		}

		public RenderStateBlock(RenderStateMask mask)
		{
			m_BlendState = BlendState.Default;
			m_RasterState = RasterState.Default;
			m_DepthState = DepthState.Default;
			m_StencilState = StencilState.Default;
			m_StencilReference = 0;
			m_Mask = mask;
		}
	}
}
