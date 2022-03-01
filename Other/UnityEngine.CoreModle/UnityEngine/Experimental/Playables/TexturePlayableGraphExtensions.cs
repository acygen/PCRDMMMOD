using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;

namespace UnityEngine.Experimental.Playables
{
	[NativeHeader("Runtime/Export/Director/TexturePlayableGraphExtensions.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[StaticAccessor("TexturePlayableGraphExtensionsBindings", StaticAccessorType.DoubleColon)]
	internal static class TexturePlayableGraphExtensions
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern bool InternalCreateTextureOutput(ref PlayableGraph graph, string name, out PlayableOutputHandle handle);
	}
}
