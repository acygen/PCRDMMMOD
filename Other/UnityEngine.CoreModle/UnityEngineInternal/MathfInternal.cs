using System.Runtime.InteropServices;

namespace UnityEngineInternal
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct MathfInternal
	{
		public static volatile float FloatMinNormal = 1.17549435E-38f;

		public static volatile float FloatMinDenormal = float.Epsilon;

		public static bool IsFlushToZeroEnabled = FloatMinDenormal == 0f;
	}
}
