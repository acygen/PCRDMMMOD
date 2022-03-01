using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/Texture2DArray.h")]
	public sealed class Texture2DArray : Texture
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
		public Texture2DArray(int width, int height, int depth, GraphicsFormat format, TextureCreationFlags flags)
		{
			if (ValidateFormat(format, FormatUsage.Sample))
			{
				Internal_Create(this, width, height, depth, format, flags);
			}
		}

		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, bool mipChain, [DefaultValue("false")] bool linear)
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
				Internal_Create(this, width, height, depth, graphicsFormat, textureCreationFlags);
			}
		}

		public Texture2DArray(int width, int height, int depth, TextureFormat textureFormat, bool mipChain)
			: this(width, height, depth, textureFormat, mipChain, linear: false)
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Texture2DArrayScripting::Create")]
		private static extern bool Internal_CreateImpl([Writable] Texture2DArray mono, int w, int h, int d, GraphicsFormat format, TextureCreationFlags flags);

		private static void Internal_Create([Writable] Texture2DArray mono, int w, int h, int d, GraphicsFormat format, TextureCreationFlags flags)
		{
			if (!Internal_CreateImpl(mono, w, h, d, format, flags))
			{
				throw new UnityException("Failed to create 2D array texture because of invalid parameters.");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Texture2DArrayScripting::Apply", HasExplicitThis = true)]
		private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Texture2DArrayScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		public extern Color[] GetPixels(int arrayElement, int miplevel);

		public Color[] GetPixels(int arrayElement)
		{
			return GetPixels(arrayElement, 0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Texture2DArrayScripting::GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		public extern Color32[] GetPixels32(int arrayElement, int miplevel);

		public Color32[] GetPixels32(int arrayElement)
		{
			return GetPixels32(arrayElement, 0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Texture2DArrayScripting::SetPixels", HasExplicitThis = true, ThrowsException = true)]
		public extern void SetPixels(Color[] colors, int arrayElement, int miplevel);

		public void SetPixels(Color[] colors, int arrayElement)
		{
			SetPixels(colors, arrayElement, 0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Texture2DArrayScripting::SetPixels32", HasExplicitThis = true, ThrowsException = true)]
		public extern void SetPixels32(Color32[] colors, int arrayElement, int miplevel);

		public void SetPixels32(Color32[] colors, int arrayElement)
		{
			SetPixels32(colors, arrayElement, 0);
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
