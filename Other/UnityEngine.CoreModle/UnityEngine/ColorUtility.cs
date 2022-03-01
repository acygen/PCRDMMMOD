using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/ColorUtility.bindings.h")]
	public class ColorUtility
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		internal static extern bool DoTryParseHtmlColor(string htmlString, out Color32 color);

		public static bool TryParseHtmlString(string htmlString, out Color color)
		{
			Color32 color2;
			bool result = DoTryParseHtmlColor(htmlString, out color2);
			color = color2;
			return result;
		}

		public static string ToHtmlStringRGB(Color color)
		{
			Color32 color2 = new Color32((byte)Mathf.Clamp(Mathf.RoundToInt(color.r * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.g * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.b * 255f), 0, 255), 1);
			return $"{color2.r:X2}{color2.g:X2}{color2.b:X2}";
		}

		public static string ToHtmlStringRGBA(Color color)
		{
			Color32 color2 = new Color32((byte)Mathf.Clamp(Mathf.RoundToInt(color.r * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.g * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.b * 255f), 0, 255), (byte)Mathf.Clamp(Mathf.RoundToInt(color.a * 255f), 0, 255));
			return $"{color2.r:X2}{color2.g:X2}{color2.b:X2}{color2.a:X2}";
		}
	}
}
