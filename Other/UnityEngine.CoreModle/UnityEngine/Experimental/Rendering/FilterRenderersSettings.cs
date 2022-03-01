namespace UnityEngine.Experimental.Rendering
{
	public struct FilterRenderersSettings
	{
		private RenderQueueRange m_RenderQueueRange;

		private int m_LayerMask;

		private uint m_RenderingLayerMask;

		private int m_ExcludeMotionVectorObjects;

		public RenderQueueRange renderQueueRange
		{
			get
			{
				return m_RenderQueueRange;
			}
			set
			{
				m_RenderQueueRange = value;
			}
		}

		public int layerMask
		{
			get
			{
				return m_LayerMask;
			}
			set
			{
				m_LayerMask = value;
			}
		}

		public uint renderingLayerMask
		{
			get
			{
				return m_RenderingLayerMask;
			}
			set
			{
				m_RenderingLayerMask = value;
			}
		}

		public bool excludeMotionVectorObjects
		{
			get
			{
				return m_ExcludeMotionVectorObjects != 0;
			}
			set
			{
				m_ExcludeMotionVectorObjects = (value ? 1 : 0);
			}
		}

		public FilterRenderersSettings(bool initializeValues = false)
		{
			this = default(FilterRenderersSettings);
			if (initializeValues)
			{
				m_RenderQueueRange = RenderQueueRange.all;
				m_LayerMask = -1;
				m_RenderingLayerMask = uint.MaxValue;
				m_ExcludeMotionVectorObjects = 0;
			}
		}
	}
}
