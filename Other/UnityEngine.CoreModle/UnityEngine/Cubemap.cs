using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[ExcludeFromPreset]
	[NativeHeader("Runtime/Graphics/CubemapTexture.h")]
	public sealed class Cubemap : Texture
	{
		public extern int mipmapCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("CountDataMipmaps")]
			get;
		}

		public extern TextureFormat format
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetTextureFormat")]
			get;
		}

		public override extern bool isReadable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[RequiredByNativeCode]
		public Cubemap(int width, GraphicsFormat format, TextureCreationFlags flags)
		{
			if (ValidateFormat(format, FormatUsage.Sample))
			{
				Internal_Create(this, width, format, flags, IntPtr.Zero);
			}
		}

		internal Cubemap(int width, TextureFormat textureFormat, bool mipChain, IntPtr nativeTex)
		{
			if (ValidateFormat(textureFormat))
			{
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(textureFormat, isSRGB: false);
				TextureCreationFlags textureCreationFlags = TextureCreationFlags.None;
				if (mipChain)
				{
					textureCreationFlags |= TextureCreationFlags.MipChain;
				}
				if (GraphicsFormatUtility.IsCrunchFormat(textureFormat))
				{
					textureCreationFlags |= TextureCreationFlags.Crunch;
				}
				Internal_Create(this, width, graphicsFormat, textureCreationFlags, nativeTex);
			}
		}

		public Cubemap(int width, TextureFormat textureFormat, bool mipChain)
			: this(width, textureFormat, mipChain, IntPtr.Zero)
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CubemapScripting::Create")]
		private static extern bool Internal_CreateImpl([Writable] Cubemap mono, int ext, GraphicsFormat format, TextureCreationFlags flags, IntPtr nativeTex);

		private static void Internal_Create([Writable] Cubemap mono, int ext, GraphicsFormat format, TextureCreationFlags flags, IntPtr nativeTex)
		{
			if (!Internal_CreateImpl(mono, ext, format, flags, nativeTex))
			{
				throw new UnityException("Failed to create texture because of invalid parameters.");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "CubemapScripting::Apply", HasExplicitThis = true)]
		private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

		[NativeName("SetPixel")]
		private void SetPixelImpl(int image, int x, int y, Color color)
		{
			SetPixelImpl_Injected(image, x, y, ref color);
		}

		[NativeName("GetPixel")]
		private Color GetPixelImpl(int image, int x, int y)
		{
			GetPixelImpl_Injected(image, x, y, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("FixupEdges")]
		public extern void SmoothEdges([DefaultValue("1")] int smoothRegionWidthInPixels);

		public void SmoothEdges()
		{
			SmoothEdges(1);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "CubemapScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		public extern Color[] GetPixels(CubemapFace face, int miplevel);

		public Color[] GetPixels(CubemapFace face)
		{
			return GetPixels(face, 0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "CubemapScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		public extern void SetPixels(Color[] colors, CubemapFace face, int miplevel);

		public void SetPixels(Color[] colors, CubemapFace face)
		{
			SetPixels(colors, face, 0);
		}

		public static Cubemap CreateExternalTexture(int width, TextureFormat format, bool mipmap, IntPtr nativeTex)
		{
			if (nativeTex == IntPtr.Zero)
			{
				throw new ArgumentException("nativeTex can not be null");
			}
			return new Cubemap(width, format, mipmap, nativeTex);
		}

		public void SetPixel(CubemapFace face, int x, int y, Color color)
		{
			if (!isReadable)
			{
				throw CreateNonReadableException(this);
			}
			SetPixelImpl((int)face, x, y, color);
		}

		public Color GetPixel(CubemapFace face, int x, int y)
		{
			if (!isReadable)
			{
				throw CreateNonReadableException(this);
			}
			return GetPixelImpl((int)face, x, y);
		}

		public void Apply([DefaultValue("true")] bool updateMipmaps, [DefaultValue("false")] bool makeNoLongerReadable)
		{
			if (!isReadable)
			{
				throw CreateNonReadableException(this);
			}
			ApplyImpl(updateMipmaps, makeNoLongerReadable);
		}

		public void Apply(bool updateMipmaps)
		{
			Apply(updateMipmaps, makeNoLongerReadable: false);
		}

		public void Apply()
		{
			Apply(updateMipmaps: true, makeNoLongerReadable: false);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPixelImpl_Injected(int image, int x, int y, ref Color color);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPixelImpl_Injected(int image, int x, int y, out Color ret);
	}
}
