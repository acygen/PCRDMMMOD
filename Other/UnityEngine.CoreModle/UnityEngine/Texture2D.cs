using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/GeneratedTextures.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/Texture2D.h")]
	public sealed class Texture2D : Texture
	{
		[Flags]
		public enum EXRFlags
		{
			None = 0x0,
			OutputAsFloat = 0x1,
			CompressZIP = 0x2,
			CompressRLE = 0x4,
			CompressPIZ = 0x8
		}

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

		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D whiteTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[StaticAccessor("builtintex", StaticAccessorType.DoubleColon)]
		public static extern Texture2D blackTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public override extern bool isReadable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool streamingMipmaps
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int streamingMipmapsPriority
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int requestedMipmapLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction(Name = "GetTextureStreamingManager().GetRequestedMipmapLevel", HasExplicitThis = true)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction(Name = "GetTextureStreamingManager().SetRequestedMipmapLevel", HasExplicitThis = true)]
			set;
		}

		public extern int desiredMipmapLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction(Name = "GetTextureStreamingManager().GetDesiredMipmapLevel", HasExplicitThis = true)]
			get;
		}

		public extern int loadingMipmapLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadingMipmapLevel", HasExplicitThis = true)]
			get;
		}

		public extern int loadedMipmapLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction(Name = "GetTextureStreamingManager().GetLoadedMipmapLevel", HasExplicitThis = true)]
			get;
		}

		internal Texture2D(int width, int height, GraphicsFormat format, TextureCreationFlags flags, IntPtr nativeTex)
		{
			if (ValidateFormat(format, FormatUsage.Sample))
			{
				Internal_Create(this, width, height, format, flags, nativeTex);
			}
		}

		public Texture2D(int width, int height, GraphicsFormat format, TextureCreationFlags flags)
			: this(width, height, format, flags, IntPtr.Zero)
		{
		}

		internal Texture2D(int width, int height, TextureFormat textureFormat, bool mipChain, bool linear, IntPtr nativeTex)
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
				Internal_Create(this, width, height, graphicsFormat, textureCreationFlags, nativeTex);
			}
		}

		public Texture2D(int width, int height, [DefaultValue("TextureFormat.RGBA32")] TextureFormat textureFormat, [DefaultValue("true")] bool mipChain, [DefaultValue("false")] bool linear)
			: this(width, height, textureFormat, mipChain, linear, IntPtr.Zero)
		{
		}

		public Texture2D(int width, int height, TextureFormat textureFormat, bool mipChain)
			: this(width, height, textureFormat, mipChain, linear: false, IntPtr.Zero)
		{
		}

		public Texture2D(int width, int height)
			: this(width, height, TextureFormat.RGBA32, mipChain: true, linear: false, IntPtr.Zero)
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Compress(bool highQuality);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Texture2DScripting::Create")]
		private static extern bool Internal_CreateImpl([Writable] Texture2D mono, int w, int h, GraphicsFormat format, TextureCreationFlags flags, IntPtr nativeTex);

		private static void Internal_Create([Writable] Texture2D mono, int w, int h, GraphicsFormat format, TextureCreationFlags flags, IntPtr nativeTex)
		{
			if (!Internal_CreateImpl(mono, w, h, format, flags, nativeTex))
			{
				throw new UnityException("Failed to create texture because of invalid parameters.");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("Apply")]
		private extern void ApplyImpl(bool updateMipmaps, bool makeNoLongerReadable);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("Resize")]
		private extern bool ResizeImpl(int width, int height);

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

		[NativeName("GetPixelBilinear")]
		private Color GetPixelBilinearImpl(int image, float x, float y)
		{
			GetPixelBilinearImpl_Injected(image, x, y, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Texture2DScripting::ResizeWithFormat", HasExplicitThis = true)]
		private extern bool ResizeWithFormatImpl(int width, int height, TextureFormat format, bool hasMipMap);

		[FreeFunction(Name = "Texture2DScripting::ReadPixels", HasExplicitThis = true)]
		private void ReadPixelsImpl(Rect source, int destX, int destY, bool recalculateMipMaps)
		{
			ReadPixelsImpl_Injected(ref source, destX, destY, recalculateMipMaps);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Texture2DScripting::SetPixels", HasExplicitThis = true)]
		private extern void SetPixelsImpl(int x, int y, int w, int h, Color[] pixel, int miplevel, int frame);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Texture2DScripting::LoadRawData", HasExplicitThis = true)]
		private extern bool LoadRawTextureDataImpl(IntPtr data, int size);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "Texture2DScripting::LoadRawData", HasExplicitThis = true)]
		private extern bool LoadRawTextureDataImplArray(byte[] data);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetWritableImageData(int frame);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern long GetRawImageDataSize();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Texture2DScripting::GenerateAtlas")]
		private static extern void GenerateAtlasImpl(Vector2[] sizes, int padding, int atlasSize, [Out] Rect[] rect);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "GetTextureStreamingManager().ClearRequestedMipmapLevel", HasExplicitThis = true)]
		public extern void ClearRequestedMipmapLevel();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "GetTextureStreamingManager().IsRequestedMipmapLevelLoaded", HasExplicitThis = true)]
		public extern bool IsRequestedMipmapLevelLoaded();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Texture2DScripting::UpdateExternalTexture", HasExplicitThis = true)]
		public extern void UpdateExternalTexture(IntPtr nativeTex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Texture2DScripting::SetAllPixels32", HasExplicitThis = true, ThrowsException = true)]
		private extern void SetAllPixels32(Color32[] colors, int miplevel);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Texture2DScripting::SetBlockOfPixels32", HasExplicitThis = true, ThrowsException = true)]
		private extern void SetBlockOfPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors, int miplevel);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Texture2DScripting::GetRawTextureData", HasExplicitThis = true)]
		public extern byte[] GetRawTextureData();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Texture2DScripting::GetPixels", HasExplicitThis = true, ThrowsException = true)]
		public extern Color[] GetPixels(int x, int y, int blockWidth, int blockHeight, int miplevel);

		public Color[] GetPixels(int x, int y, int blockWidth, int blockHeight)
		{
			return GetPixels(x, y, blockWidth, blockHeight, 0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Texture2DScripting::GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		public extern Color32[] GetPixels32(int miplevel);

		public Color32[] GetPixels32()
		{
			return GetPixels32(0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Texture2DScripting::PackTextures", HasExplicitThis = true)]
		public extern Rect[] PackTextures(Texture2D[] textures, int padding, int maximumAtlasSize, bool makeNoLongerReadable);

		public Rect[] PackTextures(Texture2D[] textures, int padding, int maximumAtlasSize)
		{
			return PackTextures(textures, padding, maximumAtlasSize, makeNoLongerReadable: false);
		}

		public Rect[] PackTextures(Texture2D[] textures, int padding)
		{
			return PackTextures(textures, padding, 2048);
		}

		public static Texture2D CreateExternalTexture(int width, int height, TextureFormat format, bool mipChain, bool linear, IntPtr nativeTex)
		{
			if (nativeTex == IntPtr.Zero)
			{
				throw new ArgumentException("nativeTex can not be null");
			}
			return new Texture2D(width, height, format, mipChain, linear, nativeTex);
		}

		public void SetPixel(int x, int y, Color color)
		{
			if (!isReadable)
			{
				throw CreateNonReadableException(this);
			}
			SetPixelImpl(0, x, y, color);
		}

		public void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors, [DefaultValue("0")] int miplevel)
		{
			if (!isReadable)
			{
				throw CreateNonReadableException(this);
			}
			SetPixelsImpl(x, y, blockWidth, blockHeight, colors, miplevel, 0);
		}

		public void SetPixels(int x, int y, int blockWidth, int blockHeight, Color[] colors)
		{
			SetPixels(x, y, blockWidth, blockHeight, colors, 0);
		}

		public void SetPixels(Color[] colors, [DefaultValue("0")] int miplevel)
		{
			int num = width >> miplevel;
			if (num < 1)
			{
				num = 1;
			}
			int num2 = height >> miplevel;
			if (num2 < 1)
			{
				num2 = 1;
			}
			SetPixels(0, 0, num, num2, colors, miplevel);
		}

		public void SetPixels(Color[] colors)
		{
			SetPixels(0, 0, width, height, colors, 0);
		}

		public Color GetPixel(int x, int y)
		{
			if (!isReadable)
			{
				throw CreateNonReadableException(this);
			}
			return GetPixelImpl(0, x, y);
		}

		public Color GetPixelBilinear(float x, float y)
		{
			if (!isReadable)
			{
				throw CreateNonReadableException(this);
			}
			return GetPixelBilinearImpl(0, x, y);
		}

		public void LoadRawTextureData(IntPtr data, int size)
		{
			if (!isReadable)
			{
				throw CreateNonReadableException(this);
			}
			if (data == IntPtr.Zero || size == 0)
			{
				Debug.LogError("No texture data provided to LoadRawTextureData", this);
			}
			else if (!LoadRawTextureDataImpl(data, size))
			{
				throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
			}
		}

		public void LoadRawTextureData(byte[] data)
		{
			if (!isReadable)
			{
				throw CreateNonReadableException(this);
			}
			if (data == null || data.Length == 0)
			{
				Debug.LogError("No texture data provided to LoadRawTextureData", this);
			}
			else if (!LoadRawTextureDataImplArray(data))
			{
				throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
			}
		}

		public unsafe void LoadRawTextureData<T>(NativeArray<T> data) where T : struct
		{
			if (!isReadable)
			{
				throw CreateNonReadableException(this);
			}
			if (!data.IsCreated || data.Length == 0)
			{
				throw new UnityException("No texture data provided to LoadRawTextureData");
			}
			if (!LoadRawTextureDataImpl((IntPtr)data.GetUnsafeReadOnlyPtr(), data.Length * UnsafeUtility.SizeOf<T>()))
			{
				throw new UnityException("LoadRawTextureData: not enough data provided (will result in overread).");
			}
		}

		public unsafe NativeArray<T> GetRawTextureData<T>() where T : struct
		{
			if (!isReadable)
			{
				throw CreateNonReadableException(this);
			}
			int num = UnsafeUtility.SizeOf<T>();
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)GetWritableImageData(0), (int)(GetRawImageDataSize() / num), Allocator.None);
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

		public bool Resize(int width, int height)
		{
			if (!isReadable)
			{
				throw CreateNonReadableException(this);
			}
			return ResizeImpl(width, height);
		}

		public bool Resize(int width, int height, TextureFormat format, bool hasMipMap)
		{
			if (!isReadable)
			{
				throw CreateNonReadableException(this);
			}
			return ResizeWithFormatImpl(width, height, format, hasMipMap);
		}

		public void ReadPixels(Rect source, int destX, int destY, [DefaultValue("true")] bool recalculateMipMaps)
		{
			if (!isReadable)
			{
				throw CreateNonReadableException(this);
			}
			ReadPixelsImpl(source, destX, destY, recalculateMipMaps);
		}

		[ExcludeFromDocs]
		public void ReadPixels(Rect source, int destX, int destY)
		{
			ReadPixels(source, destX, destY, recalculateMipMaps: true);
		}

		public static bool GenerateAtlas(Vector2[] sizes, int padding, int atlasSize, List<Rect> results)
		{
			if (sizes == null)
			{
				throw new ArgumentException("sizes array can not be null");
			}
			if (results == null)
			{
				throw new ArgumentException("results list cannot be null");
			}
			if (padding < 0)
			{
				throw new ArgumentException("padding can not be negative");
			}
			if (atlasSize <= 0)
			{
				throw new ArgumentException("atlas size must be positive");
			}
			results.Clear();
			if (sizes.Length == 0)
			{
				return true;
			}
			NoAllocHelpers.EnsureListElemCount(results, sizes.Length);
			GenerateAtlasImpl(sizes, padding, atlasSize, NoAllocHelpers.ExtractArrayFromListT(results));
			return results.Count != 0;
		}

		public void SetPixels32(Color32[] colors, int miplevel)
		{
			SetAllPixels32(colors, miplevel);
		}

		public void SetPixels32(Color32[] colors)
		{
			SetPixels32(colors, 0);
		}

		public void SetPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors, int miplevel)
		{
			SetBlockOfPixels32(x, y, blockWidth, blockHeight, colors, miplevel);
		}

		public void SetPixels32(int x, int y, int blockWidth, int blockHeight, Color32[] colors)
		{
			SetPixels32(x, y, blockWidth, blockHeight, colors, 0);
		}

		public Color[] GetPixels(int miplevel)
		{
			int num = width >> miplevel;
			if (num < 1)
			{
				num = 1;
			}
			int num2 = height >> miplevel;
			if (num2 < 1)
			{
				num2 = 1;
			}
			return GetPixels(0, 0, num, num2, miplevel);
		}

		public Color[] GetPixels()
		{
			return GetPixels(0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPixelImpl_Injected(int image, int x, int y, ref Color color);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPixelImpl_Injected(int image, int x, int y, out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPixelBilinearImpl_Injected(int image, float x, float y, out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ReadPixelsImpl_Injected(ref Rect source, int destX, int destY, bool recalculateMipMaps);
	}
}
