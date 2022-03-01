using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Rendering
{
	[NativeHeader("Runtime/Export/ScriptableRenderLoop/ScriptableRenderLoop.bindings.h")]
	public struct ShaderPassName
	{
		private int m_NameIndex;

		internal int nameIndex => m_NameIndex;

		public ShaderPassName(string name)
		{
			m_NameIndex = Init(name);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptableRenderLoop_Bindings::InitShaderPassName")]
		private static extern int Init(string name);
	}
}
