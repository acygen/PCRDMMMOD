using System.Collections.Generic;

namespace UnityEngine.Experimental.Rendering
{
	public abstract class RenderPipelineAsset : ScriptableObject, IRenderPipelineAsset
	{
		private readonly List<IRenderPipeline> m_CreatedPipelines = new List<IRenderPipeline>();

		public void DestroyCreatedInstances()
		{
			foreach (IRenderPipeline createdPipeline in m_CreatedPipelines)
			{
				createdPipeline.Dispose();
			}
			m_CreatedPipelines.Clear();
		}

		public IRenderPipeline CreatePipeline()
		{
			IRenderPipeline renderPipeline = InternalCreatePipeline();
			if (renderPipeline != null)
			{
				m_CreatedPipelines.Add(renderPipeline);
			}
			return renderPipeline;
		}

		public virtual string[] GetRenderingLayerMaskNames()
		{
			return null;
		}

		public virtual Material GetDefaultMaterial()
		{
			return null;
		}

		public virtual Shader GetAutodeskInteractiveShader()
		{
			return null;
		}

		public virtual Shader GetAutodeskInteractiveTransparentShader()
		{
			return null;
		}

		public virtual Shader GetAutodeskInteractiveMaskedShader()
		{
			return null;
		}

		public virtual Material GetDefaultParticleMaterial()
		{
			return null;
		}

		public virtual Material GetDefaultLineMaterial()
		{
			return null;
		}

		public virtual Material GetDefaultTerrainMaterial()
		{
			return null;
		}

		public virtual Material GetDefaultUIMaterial()
		{
			return null;
		}

		public virtual Material GetDefaultUIOverdrawMaterial()
		{
			return null;
		}

		public virtual Material GetDefaultUIETC1SupportedMaterial()
		{
			return null;
		}

		public virtual Material GetDefault2DMaterial()
		{
			return null;
		}

		public virtual Shader GetDefaultShader()
		{
			return null;
		}

		protected abstract IRenderPipeline InternalCreatePipeline();

		protected IEnumerable<IRenderPipeline> CreatedInstances()
		{
			return m_CreatedPipelines;
		}

		protected virtual void OnValidate()
		{
			DestroyCreatedInstances();
		}

		protected virtual void OnDisable()
		{
			DestroyCreatedInstances();
		}
	}
}
