using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[UsedByNativeCode]
	[NativeHeader("Runtime/Shaders/ShaderKeywordSet.h")]
	public enum ShaderKeywordType
	{
		None = 0,
		BuiltinDefault = 2,
		BuiltinExtra = 6,
		BuiltinAutoStripped = 10,
		UserDefined = 0x10
	}
}
