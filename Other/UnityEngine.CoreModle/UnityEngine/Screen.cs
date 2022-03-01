using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	[StaticAccessor("GetScreenManager()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/ScreenManager.h")]
	public sealed class Screen
	{
		public static extern int width
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "GetWidth", IsThreadSafe = true)]
			get;
		}

		public static extern int height
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "GetHeight", IsThreadSafe = true)]
			get;
		}

		public static extern float dpi
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetDPI")]
			get;
		}

		public static ScreenOrientation orientation
		{
			get
			{
				return GetScreenOrientation();
			}
			set
			{
				if (value == ScreenOrientation.Unknown)
				{
					Debug.Log("ScreenOrientation.Unknown is deprecated. Please use ScreenOrientation.AutoRotation");
					value = ScreenOrientation.AutoRotation;
				}
				RequestOrientation(value);
			}
		}

		[NativeProperty("ScreenTimeout")]
		public static extern int sleepTimeout
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static bool autorotateToPortrait
		{
			get
			{
				return IsOrientationEnabled(EnabledOrientation.kAutorotateToPortrait);
			}
			set
			{
				SetOrientationEnabled(EnabledOrientation.kAutorotateToPortrait, value);
			}
		}

		public static bool autorotateToPortraitUpsideDown
		{
			get
			{
				return IsOrientationEnabled(EnabledOrientation.kAutorotateToPortraitUpsideDown);
			}
			set
			{
				SetOrientationEnabled(EnabledOrientation.kAutorotateToPortraitUpsideDown, value);
			}
		}

		public static bool autorotateToLandscapeLeft
		{
			get
			{
				return IsOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeLeft);
			}
			set
			{
				SetOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeLeft, value);
			}
		}

		public static bool autorotateToLandscapeRight
		{
			get
			{
				return IsOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeRight);
			}
			set
			{
				SetOrientationEnabled(EnabledOrientation.kAutorotateToLandscapeRight, value);
			}
		}

		public static Resolution currentResolution
		{
			get
			{
				get_currentResolution_Injected(out var ret);
				return ret;
			}
		}

		public static extern bool fullScreen
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("IsFullscreen")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("RequestSetFullscreenFromScript")]
			set;
		}

		public static extern FullScreenMode fullScreenMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetFullscreenMode")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("RequestSetFullscreenModeFromScript")]
			set;
		}

		public static Rect safeArea
		{
			get
			{
				get_safeArea_Injected(out var ret);
				return ret;
			}
		}

		public static extern Resolution[] resolutions
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("ScreenScripting::GetResolutions")]
			get;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use Cursor.lockState and Cursor.visible instead.", false)]
		public static bool lockCursor
		{
			get
			{
				return CursorLockMode.Locked == Cursor.lockState;
			}
			set
			{
				if (value)
				{
					Cursor.visible = false;
					Cursor.lockState = CursorLockMode.Locked;
				}
				else
				{
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;
				}
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RequestOrientation(ScreenOrientation orient);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ScreenOrientation GetScreenOrientation();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetIsOrientationEnabled")]
		private static extern bool IsOrientationEnabled(EnabledOrientation orient);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("SetIsOrientationEnabled")]
		private static extern void SetOrientationEnabled(EnabledOrientation orient, bool enabled);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("RequestResolution")]
		public static extern void SetResolution(int width, int height, FullScreenMode fullscreenMode, [UnityEngine.Internal.DefaultValue("0")] int preferredRefreshRate);

		public static void SetResolution(int width, int height, FullScreenMode fullscreenMode)
		{
			SetResolution(width, height, fullscreenMode, 0);
		}

		public static void SetResolution(int width, int height, bool fullscreen, [UnityEngine.Internal.DefaultValue("0")] int preferredRefreshRate)
		{
			SetResolution(width, height, fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed, preferredRefreshRate);
		}

		public static void SetResolution(int width, int height, bool fullscreen)
		{
			SetResolution(width, height, fullscreen, 0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void get_currentResolution_Injected(out Resolution ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void get_safeArea_Injected(out Rect ret);
	}
}
