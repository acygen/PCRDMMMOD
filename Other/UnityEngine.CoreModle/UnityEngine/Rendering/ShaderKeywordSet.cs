using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[UsedByNativeCode]
	public struct ShaderKeywordSet
	{
		private const int k_SizeInBits = 32;

		internal unsafe fixed uint m_Bits[8];

		private void ComputeSliceAndMask(ShaderKeyword keyword, out uint slice, out uint mask)
		{
			int keywordIndex = keyword.GetKeywordIndex();
			slice = (uint)(keywordIndex / 32);
			mask = (uint)(1 << keywordIndex % 32);
		}

		public unsafe bool IsEnabled(ShaderKeyword keyword)
		{
			if (!keyword.IsValid())
			{
				return false;
			}
			ComputeSliceAndMask(keyword, out var slice, out var mask);
			fixed (uint* ptr = m_Bits)
			{
				return (System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, slice) & mask) != 0;
			}
		}

		public unsafe void Enable(ShaderKeyword keyword)
		{
			if (keyword.IsValid())
			{
				ComputeSliceAndMask(keyword, out var slice, out var mask);
				fixed (uint* ptr = m_Bits)
				{
					System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, slice) |= mask;
				}
			}
		}

		public unsafe void Disable(ShaderKeyword keyword)
		{
			if (keyword.IsValid())
			{
				ComputeSliceAndMask(keyword, out var slice, out var mask);
				fixed (uint* ptr = m_Bits)
				{
					System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, slice) &= ~mask;
				}
			}
		}

		public ShaderKeyword[] GetShaderKeywords()
		{
			ShaderKeyword[] array = new ShaderKeyword[256];
			int num = 0;
			for (int i = 0; i < 256; i++)
			{
				ShaderKeyword shaderKeyword = new ShaderKeyword(i);
				if (IsEnabled(shaderKeyword))
				{
					array[num] = shaderKeyword;
					num++;
				}
			}
			Array.Resize(ref array, num);
			return array;
		}
	}
}
