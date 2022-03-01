using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/CubemapArrayTexture.h")]
	public sealed class CubemapArray : Texture
	{
		public extern int cubemapCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
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
		public CubemapArray(int width, int cubemapCount, GraphicsFormat format, TextureCreationFlags flags)
		{
			if (ValidateFormat(format, FormatUsage.Sample))
			{
				Internal_Create(this, width, cubemapCount, format, flags);
			}
		}

		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, bool mipChain, [DefaultValue("false")] bool linear)
		{
			if (ValidateFormat(textureFormat))
			{
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(textureFormat, !linear);
				TextureCreationFlags textureCreationFlags = TextureCreationFlags.None;
				if (mipChain)
				{
					textureCreationFlags |= TextureCreationFlags.MipChain;
				}
				if (GraphicsFormatUtility.IsCrunchFormat(textureFormat))
				{
					textureCreationFlags |= TextureCreationFlags.Crunch;
				}
				Internal_Create(this, width, cubemapCount, graphicsFormat, textureCreationFlags);
			}
		}

		public CubemapArray(int width, int cubemapCount, TextureFormat textureFormat, bool mipChain)
			: this(width, cubemapCount, textureFormat, mipChain, linear: false)
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CubemapArrayScripting::Create")]
		private static extern bool Internal_CreateImpl([Writable] CubemapArray mono, int ext, int count, GraphicsFormat format, TextureCreationFlags flags);

		private static void Internal_Create([Writable] CubemapArray mono, int ext, int count, GraphicsFormat format, TextureCreationFlags flags)
		{
			if (!Internal_CreateImpl(mono, ext, count, format, flags))
			{
				throw new UnityException("Failed to create cubemap array texture because of invalid parameters.");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "CubemapArrayScripting::Apply", HasExplicitThis = true)]
		private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "CubemapArrayScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		public extern Color[] GetPixels(CubemapFace face, int arrayElement, int miplevel);

		public Color[] GetPixels(CubemapFace face, int arrayElement)
		{
			return GetPixels(face, arrayElement, 0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "CubemapArrayScripting::GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		public extern Color32[] GetPixels32(CubemapFace face, int arrayElement, int miplevel);

		public Color32[] GetPixels32(CubemapFace face, int arrayElement)
		{
			return GetPixels32(face, arrayElement, 0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "CubemapArrayScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		public extern void SetPixels(Color[] colors, CubemapFace face, int arrayElement, int miplevel);

		public void SetPixels(Color[] colors, CubemapFace face, int arrayElement)
		{
			SetPixels(colors, face, arrayElement, 0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "CubemapArrayScripting::SetPixels32", HasExplicitThis = true, ThrowsException = true)]
		public extern void SetPixels32(Color32[] colors, CubemapFace face, int arrayElement, int miplevel);

		public void SetPixels32(Color32[] colors, CubemapFace face, int arrayElement)
		{
			SetPixels32(colors, face, arrayElement, 0);
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
	}
}
