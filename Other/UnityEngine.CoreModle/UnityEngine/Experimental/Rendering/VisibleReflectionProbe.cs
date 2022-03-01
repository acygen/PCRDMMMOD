using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	[UsedByNativeCode]
	public struct VisibleReflectionProbe
	{
		public Bounds bounds;

		public Matrix4x4 localToWorld;

		public Vector4 hdr;

		public Vector3 center;

		public float blendDistance;

		public int importance;

		public int boxProjection;

		private int instanceId;

		private int textureId;

		public Texture texture => GetTextureObject(textureId);

		public ReflectionProbe probe => GetReflectionProbeObject(instanceId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("(Texture*)Object::IDToPointer")]
		private static extern Texture GetTextureObject(int textureId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("(ReflectionProbe*)Object::IDToPointer")]
		private static extern ReflectionProbe GetReflectionProbeObject(int instanceId);
	}
}
