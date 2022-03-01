namespace UnityEngine.Rendering
{
	public struct RenderTargetBinding
	{
		private RenderTargetIdentifier[] m_ColorRenderTargets;

		private RenderTargetIdentifier m_DepthRenderTarget;

		private RenderBufferLoadAction[] m_ColorLoadActions;

		private RenderBufferStoreAction[] m_ColorStoreActions;

		private RenderBufferLoadAction m_DepthLoadAction;

		private RenderBufferStoreAction m_DepthStoreAction;

		public RenderTargetIdentifier[] colorRenderTargets
		{
			get
			{
				return m_ColorRenderTargets;
			}
			set
			{
				m_ColorRenderTargets = value;
			}
		}

		public RenderTargetIdentifier depthRenderTarget
		{
			get
			{
				return m_DepthRenderTarget;
			}
			set
			{
				m_DepthRenderTarget = value;
			}
		}

		public RenderBufferLoadAction[] colorLoadActions
		{
			get
			{
				return m_ColorLoadActions;
			}
			set
			{
				m_ColorLoadActions = value;
			}
		}

		public RenderBufferStoreAction[] colorStoreActions
		{
			get
			{
				return m_ColorStoreActions;
			}
			set
			{
				m_ColorStoreActions = value;
			}
		}

		public RenderBufferLoadAction depthLoadAction
		{
			get
			{
				return m_DepthLoadAction;
			}
			set
			{
				m_DepthLoadAction = value;
			}
		}

		public RenderBufferStoreAction depthStoreAction
		{
			get
			{
				return m_DepthStoreAction;
			}
			set
			{
				m_DepthStoreAction = value;
			}
		}

		public RenderTargetBinding(RenderTargetIdentifier[] colorRenderTargets, RenderBufferLoadAction[] colorLoadActions, RenderBufferStoreAction[] colorStoreActions, RenderTargetIdentifier depthRenderTarget, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
		{
			m_ColorRenderTargets = colorRenderTargets;
			m_DepthRenderTarget = depthRenderTarget;
			m_ColorLoadActions = colorLoadActions;
			m_ColorStoreActions = colorStoreActions;
			m_DepthLoadAction = depthLoadAction;
			m_DepthStoreAction = depthStoreAction;
		}

		public RenderTargetBinding(RenderTargetIdentifier colorRenderTarget, RenderBufferLoadAction colorLoadAction, RenderBufferStoreAction colorStoreAction, RenderTargetIdentifier depthRenderTarget, RenderBufferLoadAction depthLoadAction, RenderBufferStoreAction depthStoreAction)
			: this(new RenderTargetIdentifier[1] { colorRenderTarget }, new RenderBufferLoadAction[1] { colorLoadAction }, new RenderBufferStoreAction[1] { colorStoreAction }, depthRenderTarget, depthLoadAction, depthStoreAction)
		{
		}

		public RenderTargetBinding(RenderTargetSetup setup)
		{
			m_ColorRenderTargets = new RenderTargetIdentifier[setup.color.Length];
			for (int i = 0; i < m_ColorRenderTargets.Length; i++)
			{
				ref RenderTargetIdentifier reference = ref m_ColorRenderTargets[i];
				reference = new RenderTargetIdentifier(setup.color[i], setup.mipLevel, setup.cubemapFace, setup.depthSlice);
			}
			m_DepthRenderTarget = setup.depth;
			m_ColorLoadActions = (RenderBufferLoadAction[])setup.colorLoad.Clone();
			m_ColorStoreActions = (RenderBufferStoreAction[])setup.colorStore.Clone();
			m_DepthLoadAction = setup.depthLoad;
			m_DepthStoreAction = setup.depthStore;
		}
	}
}
