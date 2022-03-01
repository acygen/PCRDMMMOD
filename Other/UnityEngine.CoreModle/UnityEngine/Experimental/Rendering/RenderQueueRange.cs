namespace UnityEngine.Experimental.Rendering
{
	public struct RenderQueueRange
	{
		public int min;

		public int max;

		public static RenderQueueRange all
		{
			get
			{
				RenderQueueRange result = default(RenderQueueRange);
				result.min = 0;
				result.max = 5000;
				return result;
			}
		}

		public static RenderQueueRange opaque
		{
			get
			{
				RenderQueueRange result = default(RenderQueueRange);
				result.min = 0;
				result.max = 2500;
				return result;
			}
		}

		public static RenderQueueRange transparent
		{
			get
			{
				RenderQueueRange result = default(RenderQueueRange);
				result.min = 2501;
				result.max = 5000;
				return result;
			}
		}
	}
}
