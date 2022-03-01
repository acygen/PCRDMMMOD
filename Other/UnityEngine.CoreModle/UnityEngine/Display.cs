using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/DisplayManager.h")]
	public class Display
	{
		public delegate void DisplaysUpdatedDelegate();

		internal IntPtr nativeDisplay;

		public static Display[] displays = new Display[1]
		{
			new Display()
		};

		private static Display _mainDisplay = displays[0];

		public int renderingWidth
		{
			get
			{
				int w = 0;
				int h = 0;
				GetRenderingExtImpl(nativeDisplay, out w, out h);
				return w;
			}
		}

		public int renderingHeight
		{
			get
			{
				int w = 0;
				int h = 0;
				GetRenderingExtImpl(nativeDisplay, out w, out h);
				return h;
			}
		}

		public int systemWidth
		{
			get
			{
				int w = 0;
				int h = 0;
				GetSystemExtImpl(nativeDisplay, out w, out h);
				return w;
			}
		}

		public int systemHeight
		{
			get
			{
				int w = 0;
				int h = 0;
				GetSystemExtImpl(nativeDisplay, out w, out h);
				return h;
			}
		}

		public RenderBuffer colorBuffer
		{
			get
			{
				GetRenderingBuffersImpl(nativeDisplay, out var color, out var _);
				return color;
			}
		}

		public RenderBuffer depthBuffer
		{
			get
			{
				GetRenderingBuffersImpl(nativeDisplay, out var _, out var depth);
				return depth;
			}
		}

		public bool active => GetActiveImp(nativeDisplay);

		public static Display main => _mainDisplay;

		public static event DisplaysUpdatedDelegate onDisplaysUpdated;

		internal Display()
		{
			nativeDisplay = new IntPtr(0);
		}

		internal Display(IntPtr nativeDisplay)
		{
			this.nativeDisplay = nativeDisplay;
		}

		public void Activate()
		{
			ActivateDisplayImpl(nativeDisplay, 0, 0, 60);
		}

		public void Activate(int width, int height, int refreshRate)
		{
			ActivateDisplayImpl(nativeDisplay, width, height, refreshRate);
		}

		public void SetParams(int width, int height, int x, int y)
		{
			SetParamsImpl(nativeDisplay, width, height, x, y);
		}

		public void SetRenderingResolution(int w, int h)
		{
			SetRenderingResolutionImpl(nativeDisplay, w, h);
		}

		[Obsolete("MultiDisplayLicense has been deprecated.", false)]
		public static bool MultiDisplayLicense()
		{
			return true;
		}

		public static Vector3 RelativeMouseAt(Vector3 inputMouseCoordinates)
		{
			int rx = 0;
			int ry = 0;
			int x = (int)inputMouseCoordinates.x;
			int y = (int)inputMouseCoordinates.y;
			Vector3 result = default(Vector3);
			result.z = RelativeMouseAtImpl(x, y, out rx, out ry);
			result.x = rx;
			result.y = ry;
			return result;
		}

		[RequiredByNativeCode]
		private static void RecreateDisplayList(IntPtr[] nativeDisplay)
		{
			if (nativeDisplay.Length != 0)
			{
				displays = new Display[nativeDisplay.Length];
				for (int i = 0; i < nativeDisplay.Length; i++)
				{
					displays[i] = new Display(nativeDisplay[i]);
				}
				_mainDisplay = displays[0];
			}
		}

		[RequiredByNativeCode]
		private static void FireDisplaysUpdated()
		{
			if (Display.onDisplaysUpdated != null)
			{
				Display.onDisplaysUpdated();
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("UnityDisplayManager_DisplaySystemResolution")]
		private static extern void GetSystemExtImpl(IntPtr nativeDisplay, out int w, out int h);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("UnityDisplayManager_DisplayRenderingResolution")]
		private static extern void GetRenderingExtImpl(IntPtr nativeDisplay, out int w, out int h);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("UnityDisplayManager_GetRenderingBuffersWrapper")]
		private static extern void GetRenderingBuffersImpl(IntPtr nativeDisplay, out RenderBuffer color, out RenderBuffer depth);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("UnityDisplayManager_SetRenderingResolution")]
		private static extern void SetRenderingResolutionImpl(IntPtr nativeDisplay, int w, int h);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("UnityDisplayManager_ActivateDisplay")]
		private static extern void ActivateDisplayImpl(IntPtr nativeDisplay, int width, int height, int refreshRate);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("UnityDisplayManager_SetDisplayParam")]
		private static extern void SetParamsImpl(IntPtr nativeDisplay, int width, int height, int x, int y);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("UnityDisplayManager_RelativeMouseAt")]
		private static extern int RelativeMouseAtImpl(int x, int y, out int rx, out int ry);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("UnityDisplayManager_DisplayActive")]
		private static extern bool GetActiveImp(IntPtr nativeDisplay);

		static Display()
		{
			Display.onDisplaysUpdated = null;
		}
	}
}
