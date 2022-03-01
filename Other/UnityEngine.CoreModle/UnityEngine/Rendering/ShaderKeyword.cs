using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Runtime/Shaders/ShaderKeywords.h")]
	[UsedByNativeCode]
	public class ShaderKeyword
	{
		internal const int k_MaxShaderKeywords = 256;

		private const int k_InvalidKeyword = -1;

		internal int m_KeywordIndex;

		internal ShaderKeyword(int keywordIndex)
		{
			m_KeywordIndex = keywordIndex;
		}

		public ShaderKeyword(string keywordName)
		{
			m_KeywordIndex = GetShaderKeywordIndex(keywordName);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("keywords::Find", true)]
		internal static extern int GetShaderKeywordIndex(string keywordName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("keywords::GetKeywordName", true)]
		internal static extern string GetShaderKeywordName(int keywordIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("keywords::GetKeywordType", true)]
		internal static extern ShaderKeywordType GetShaderKeywordType(int keywordIndex);

		public bool IsValid()
		{
			return m_KeywordIndex >= 0 && m_KeywordIndex < 256 && m_KeywordIndex != -1;
		}

		public ShaderKeywordType GetKeywordType()
		{
			return GetShaderKeywordType(m_KeywordIndex);
		}

		public string GetKeywordName()
		{
			return GetShaderKeywordName(m_KeywordIndex);
		}

		internal int GetKeywordIndex()
		{
			return m_KeywordIndex;
		}

		[Obsolete("GetName() has been deprecated. Use GetKeywordName() instead (UnityUpgradable) -> GetKeywordName()")]
		public string GetName()
		{
			return GetKeywordName();
		}
	}
}
