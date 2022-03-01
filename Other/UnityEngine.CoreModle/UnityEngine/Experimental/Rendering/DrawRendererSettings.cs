using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Rendering
{
	[NativeHeader("Runtime/Graphics/ScriptableRenderLoop/ScriptableDrawRenderersUtility.h")]
	public struct DrawRendererSettings
	{
		private const int kMaxShaderPasses = 16;

		public static readonly int maxShaderPasses = 16;

		public DrawRendererSortSettings sorting;

		internal unsafe fixed int shaderPassNames[16];

		public RendererConfiguration rendererConfiguration;

		public DrawRendererFlags flags;

		private int m_OverrideMaterialInstanceId;

		private int m_OverrideMaterialPassIdx;

		private int useSRPBatcher;

		public unsafe DrawRendererSettings(Camera camera, ShaderPassName shaderPassName)
		{
			rendererConfiguration = RendererConfiguration.None;
			flags = DrawRendererFlags.EnableInstancing;
			m_OverrideMaterialInstanceId = 0;
			m_OverrideMaterialPassIdx = 0;
			fixed (int* ptr = shaderPassNames)
			{
				for (int i = 0; i < maxShaderPasses; i++)
				{
					System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, i) = -1;
				}
			}
			fixed (int* ptr2 = shaderPassNames)
			{
				*ptr2 = shaderPassName.nameIndex;
			}
			rendererConfiguration = RendererConfiguration.None;
			flags = DrawRendererFlags.EnableInstancing;
			InitializeSortSettings(camera, out sorting);
			useSRPBatcher = 0;
		}

		public void SetOverrideMaterial(Material mat, int passIndex)
		{
			if (mat == null)
			{
				m_OverrideMaterialInstanceId = 0;
			}
			else
			{
				m_OverrideMaterialInstanceId = mat.GetInstanceID();
			}
			m_OverrideMaterialPassIdx = passIndex;
		}

		public unsafe void SetShaderPassName(int index, ShaderPassName shaderPassName)
		{
			if (index >= maxShaderPasses || index < 0)
			{
				throw new ArgumentOutOfRangeException("index", $"Index should range from 0 - DrawRendererSettings.maxShaderPasses ({maxShaderPasses}), was {index}");
			}
			fixed (int* ptr = shaderPassNames)
			{
				System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index) = shaderPassName.nameIndex;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("InitializeSortSettings")]
		private static extern void InitializeSortSettings(Camera camera, out DrawRendererSortSettings sortSettings);
	}
}
