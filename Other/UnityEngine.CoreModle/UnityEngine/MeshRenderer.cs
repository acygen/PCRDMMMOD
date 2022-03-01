using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/Mesh/MeshRenderer.h")]
	public class MeshRenderer : Renderer
	{
		public extern Mesh additionalVertexStreams
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int subMeshStartIndex
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetSubMeshStartIndex")]
			get;
		}

		[RequiredByNativeCode]
		private void DontStripMeshRenderer()
		{
		}
	}
}
