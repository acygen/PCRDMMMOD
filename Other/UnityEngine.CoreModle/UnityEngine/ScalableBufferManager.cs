using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/GfxDevice/ScalableBufferManager.h")]
	[StaticAccessor("ScalableBufferManager::GetInstance()", StaticAccessorType.Dot)]
	public static class ScalableBufferManager
	{
		public static extern float widthScaleFactor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern float heightScaleFactor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResizeBuffers(float widthScale, float heightScale);
	}
}
