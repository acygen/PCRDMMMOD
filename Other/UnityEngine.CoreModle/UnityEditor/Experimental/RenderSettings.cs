using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEditor.Experimental
{
	[NativeHeader("Runtime/Camera/RenderSettings.h")]
	[StaticAccessor("GetRenderSettings()", StaticAccessorType.Dot)]
	public sealed class RenderSettings
	{
		public static extern bool useRadianceAmbientProbe
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
