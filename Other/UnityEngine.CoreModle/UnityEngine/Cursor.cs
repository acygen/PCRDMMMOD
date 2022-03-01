using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/Cursor.bindings.h")]
	public class Cursor
	{
		public static extern bool visible
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern CursorLockMode lockState
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		private static void SetCursor(Texture2D texture, CursorMode cursorMode)
		{
			SetCursor(texture, Vector2.zero, cursorMode);
		}

		public static void SetCursor(Texture2D texture, Vector2 hotspot, CursorMode cursorMode)
		{
			SetCursor_Injected(texture, ref hotspot, cursorMode);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCursor_Injected(Texture2D texture, ref Vector2 hotspot, CursorMode cursorMode);
	}
}
