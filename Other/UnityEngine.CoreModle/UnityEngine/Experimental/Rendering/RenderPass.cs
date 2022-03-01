using System;

namespace UnityEngine.Experimental.Rendering
{
	public class RenderPass : IDisposable
	{
		public class SubPass : IDisposable
		{
			public SubPass(RenderPass renderPass, RenderPassAttachment[] colors, RenderPassAttachment[] inputs, bool readOnlyDepth = false)
			{
				ScriptableRenderContext.BeginSubPassInternal(renderPass.context.Internal_GetPtr(), (colors == null) ? new RenderPassAttachment[0] : colors, (inputs == null) ? new RenderPassAttachment[0] : inputs, readOnlyDepth);
			}

			public void Dispose()
			{
			}
		}

		public RenderPassAttachment[] colorAttachments { get; private set; }

		public RenderPassAttachment depthAttachment { get; private set; }

		public int width { get; private set; }

		public int height { get; private set; }

		public int sampleCount { get; private set; }

		public ScriptableRenderContext context { get; private set; }

		public RenderPass(ScriptableRenderContext ctx, int w, int h, int samples, RenderPassAttachment[] colors, RenderPassAttachment depth = null)
		{
			width = w;
			height = h;
			sampleCount = samples;
			colorAttachments = colors;
			depthAttachment = depth;
			context = ctx;
			ScriptableRenderContext.BeginRenderPassInternal(ctx.Internal_GetPtr(), w, h, samples, colors, depth);
		}

		public void Dispose()
		{
			ScriptableRenderContext.EndRenderPassInternal(context.Internal_GetPtr());
		}
	}
}
