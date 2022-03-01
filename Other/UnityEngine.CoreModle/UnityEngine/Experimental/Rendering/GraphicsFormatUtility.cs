using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.Rendering
{
	[NativeHeader("Runtime/Graphics/TextureFormat.h")]
	[NativeHeader("Runtime/Graphics/GraphicsFormatUtility.bindings.h")]
	public class GraphicsFormatUtility
	{
		public static GraphicsFormat GetGraphicsFormat(TextureFormat format, bool isSRGB)
		{
			return GetGraphicsFormat_Native_TextureFormat(format, isSRGB);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		private static extern GraphicsFormat GetGraphicsFormat_Native_TextureFormat(TextureFormat format, bool isSRGB);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern TextureFormat GetTextureFormat(GraphicsFormat format);

		public static GraphicsFormat GetGraphicsFormat(RenderTextureFormat format, bool isSRGB)
		{
			return GetGraphicsFormat_Native_RenderTextureFormat(format, isSRGB);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		private static extern GraphicsFormat GetGraphicsFormat_Native_RenderTextureFormat(RenderTextureFormat format, bool isSRGB);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsSRGBFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern RenderTextureFormat GetRenderTextureFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern uint GetColorComponentCount(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern uint GetAlphaComponentCount(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern uint GetComponentCount(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsCompressedFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("IsAnyCompressedTextureFormat")]
		internal static extern bool IsCompressedTextureFormat(TextureFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsPackedFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool Is16BitPackedFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern GraphicsFormat ConvertToAlphaFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsAlphaOnlyFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool HasAlphaChannel(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsDepthFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsStencilFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsIEEE754Format(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsFloatFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsHalfFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsUnsignedFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsSignedFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsNormFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsUNormFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsSNormFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsIntegerFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsUIntFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsSIntFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsDXTCFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsRGTCFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsBPTCFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsBCFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsPVRTCFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsETCFormat(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern bool IsASTCFormat(GraphicsFormat format);

		public static bool IsCrunchFormat(TextureFormat format)
		{
			return format == TextureFormat.DXT1Crunched || format == TextureFormat.DXT5Crunched || format == TextureFormat.ETC_RGB4Crunched || format == TextureFormat.ETC2_RGBA8Crunched;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern uint GetBlockSize(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern uint GetBlockWidth(GraphicsFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern uint GetBlockHeight(GraphicsFormat format);

		public static uint ComputeMipmapSize(int width, int height, GraphicsFormat format)
		{
			return ComputeMipmapSize_Native_2D(width, height, format);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		private static extern uint ComputeMipmapSize_Native_2D(int width, int height, GraphicsFormat format);

		public static uint ComputeMipmapSize(int width, int height, int depth, GraphicsFormat format)
		{
			return ComputeMipmapSize_Native_3D(width, height, depth, format);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		private static extern uint ComputeMipmapSize_Native_3D(int width, int height, int depth, GraphicsFormat format);
	}
}
