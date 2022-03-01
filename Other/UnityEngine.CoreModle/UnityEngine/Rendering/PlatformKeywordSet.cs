using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[UsedByNativeCode]
	public struct PlatformKeywordSet
	{
		private const int k_SizeInBits = 32;

		internal uint m_Bits;

		private uint ComputeKeywordMask(BuiltinShaderDefine define)
		{
			return (uint)(1 << (int)define % 32);
		}

		public bool IsEnabled(BuiltinShaderDefine define)
		{
			return (m_Bits & ComputeKeywordMask(define)) != 0;
		}

		public void Enable(BuiltinShaderDefine define)
		{
			m_Bits |= ComputeKeywordMask(define);
		}

		public void Disable(BuiltinShaderDefine define)
		{
			m_Bits &= ~ComputeKeywordMask(define);
		}
	}
}
