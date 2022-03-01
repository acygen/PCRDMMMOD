using UnityEngine.Playables;

namespace UnityEngine.Experimental.Playables
{
	public static class TexturePlayableBinding
	{
		public static PlayableBinding Create(string name, Object key)
		{
			return PlayableBinding.CreateInternal(name, key, typeof(RenderTexture), CreateTextureOutput);
		}

		private static PlayableOutput CreateTextureOutput(PlayableGraph graph, string name)
		{
			return TexturePlayableOutput.Create(graph, name, null);
		}
	}
}
