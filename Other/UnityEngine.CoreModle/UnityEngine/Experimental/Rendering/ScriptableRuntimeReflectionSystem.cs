using System;

namespace UnityEngine.Experimental.Rendering
{
	public abstract class ScriptableRuntimeReflectionSystem : IScriptableRuntimeReflectionSystem, IDisposable
	{
		public virtual bool TickRealtimeProbes()
		{
			return false;
		}

		protected virtual void Dispose(bool disposing)
		{
		}

		void IDisposable.Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
