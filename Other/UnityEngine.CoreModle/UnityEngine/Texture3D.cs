using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/Texture3D.h")]
	[ExcludeFromPreset]
	public sealed class Texture3D : Texture
	{
		public extern int depth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetTextureLayerCount")]
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
		public Texture3D(int width, int height, int depth, GraphicsFormat format, TextureCreationFlags flags)
		{
			if (ValidateFormat(format, FormatUsage.Sample))
			{
				Internal_Create(this, width, height, depth, format, flags);
			}
		}

		public Texture3D(int width, int height, int depth, TextureFormat textureFormat, bool mipChain)
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
				Internal_Create(this, width, height, depth, graphicsFormat, textureCreationFlags);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Texture3DScripting::Create")]
		private static extern bool Internal_CreateImpl([Writable] Texture3D mono, int w, int h, int d, GraphicsFormat format, TextureCreationFlags flags);

		private static void Internal_Create([Writable] Texture3D mono, int w, int h, int d, GraphicsFormat format, TextureCreationFlags flags)
		{
			if (!Internal_CreateImpl(mono, w, h, d, format, flags))
			{
				throw new UnityException("Failed to create texture because of invalid parameters.");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Texture3DScripting::Apply", HasExplicitThis = true)]
		private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Texture3DScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		public extern Color[] GetPixels(int miplevel);

		public Color[] GetPixels()
		{
			return GetPixels(0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Texture3DScripting::GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		public extern Color32[] GetPixels32(int miplevel);

		public Color32[] GetPixels32()
		{
			return GetPixels32(0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Texture3DScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		public extern void SetPixels(Color[] colors, int miplevel);

		public void SetPixels(Color[] colors)
		{
			SetPixels(colors, 0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Texture3DScripting::SetPixels32", HasExplicitThis = true, ThrowsException = true)]
		public extern void SetPixels32(Color32[] colors, int miplevel);

		public void SetPixels32(Color32[] colors)
		{
			SetPixels32(colors, 0);
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
