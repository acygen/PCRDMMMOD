using System;

namespace UnityEngine.Experimental.Rendering
{
	public abstract class RenderPipeline : IRenderPipeline, IDisposable
	{
		public bool disposed { get; private set; }

		public static event Action<Camera[]> beginFrameRendering;

		public static event Action<Camera> beginCameraRendering;

		public virtual void Render(ScriptableRenderContext renderContext, Camera[] cameras)
		{
			if (disposed)
			{
				throw new ObjectDisposedException($"{this} has been disposed. Do not call Render on disposed RenderLoops.");
			}
		}

		public virtual void Dispose()
		{
			disposed = true;
		}

		public static void BeginFrameRendering(Camera[] cameras)
		{
			if (RenderPipeline.beginFrameRendering != null)
			{
				RenderPipeline.beginFrameRendering(cameras);
			}
		}

		public static void BeginCameraRendering(Camera camera)
		{
			if (RenderPipeline.beginCameraRendering != null)
			{
				RenderPipeline.beginCameraRendering(camera);
			}
		}
	}
}
