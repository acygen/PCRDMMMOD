using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Runtime/Graphics/Mesh/MeshFilter.h")]
	public sealed class MeshFilter : Component
	{
		public extern Mesh sharedMesh
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Mesh mesh
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetInstantiatedMeshFromScript")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("SetInstantiatedMesh")]
			set;
		}

		[RequiredByNativeCode]
		private void DontStripMeshFilter()
		{
		}
	}
}
