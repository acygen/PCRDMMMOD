using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	[NativeHeader("Runtime/Graphics/DrawSplashScreenAndWatermarks.h")]
	public class SplashScreen
	{
		public static extern bool isFinished
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("IsSplashScreenFinished")]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("BeginSplashScreen_Binding")]
		public static extern void Begin();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("DrawSplashScreen_Binding")]
		public static extern void Draw();
	}
}
