namespace UnityEngine.Experimental.Rendering
{
	public interface IRenderPipelineAsset
	{
		void DestroyCreatedInstances();

		IRenderPipeline CreatePipeline();
	}
}
