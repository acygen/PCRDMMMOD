namespace UnityEngine.Experimental.Rendering
{
	public struct RenderStateMapping
	{
		private int m_RenderTypeID;

		private RenderStateBlock m_StateBlock;

		public string renderType
		{
			get
			{
				return Shader.IDToTag(m_RenderTypeID);
			}
			set
			{
				m_RenderTypeID = Shader.TagToID(value);
			}
		}

		public RenderStateBlock stateBlock
		{
			get
			{
				return m_StateBlock;
			}
			set
			{
				m_StateBlock = value;
			}
		}

		public RenderStateMapping(string renderType, RenderStateBlock stateBlock)
		{
			m_RenderTypeID = Shader.TagToID(renderType);
			m_StateBlock = stateBlock;
		}

		public RenderStateMapping(RenderStateBlock stateBlock)
			: this(null, stateBlock)
		{
		}
	}
}
