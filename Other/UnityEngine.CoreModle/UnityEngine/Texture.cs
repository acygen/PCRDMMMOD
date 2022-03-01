using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/Texture.h")]
	[NativeHeader("Runtime/Streaming/TextureStreamingManager.h")]
	[UsedByNativeCode]
	public class Texture : Object
	{
		public static extern int masterTextureLimit
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("AnisoLimit")]
		public static extern AnisotropicFiltering anisotropicFiltering
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public virtual int width
		{
			get
			{
				return GetDataWidth();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public virtual int height
		{
			get
			{
				return GetDataHeight();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public virtual TextureDimension dimension
		{
			get
			{
				return GetDimension();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public virtual extern bool isReadable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern TextureWrapMode wrapMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetWrapModeU")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern TextureWrapMode wrapModeU
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern TextureWrapMode wrapModeV
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern TextureWrapMode wrapModeW
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern FilterMode filterMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int anisoLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float mipMapBias
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector2 texelSize
		{
			[NativeName("GetNpotTexelSize")]
			get
			{
				get_texelSize_Injected(out var ret);
				return ret;
			}
		}

		public extern uint updateCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ulong totalTextureMemory
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetTextureStreamingManager().GetTotalTextureMemory")]
			get;
		}

		public static extern ulong desiredTextureMemory
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetTextureStreamingManager().GetDesiredTextureMemory")]
			get;
		}

		public static extern ulong targetTextureMemory
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetTextureStreamingManager().GetTargetTextureMemory")]
			get;
		}

		public static extern ulong currentTextureMemory
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetTextureStreamingManager().GetCurrentTextureMemory")]
			get;
		}

		public static extern ulong nonStreamingTextureMemory
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetTextureStreamingManager().GetNonStreamingTextureMemory")]
			get;
		}

		public static extern ulong streamingMipmapUploadCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetTextureStreamingManager().GetStreamingMipmapUploadCount")]
			get;
		}

		public static extern ulong streamingRendererCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetTextureStreamingManager().GetStreamingRendererCount")]
			get;
		}

		public static extern ulong streamingTextureCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetTextureStreamingManager().GetStreamingTextureCount")]
			get;
		}

		public static extern ulong nonStreamingTextureCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetTextureStreamingManager().GetNonStreamingTextureCount")]
			get;
		}

		public static extern ulong streamingTexturePendingLoadCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetTextureStreamingManager().GetStreamingTexturePendingLoadCount")]
			get;
		}

		public static extern ulong streamingTextureLoadingCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetTextureStreamingManager().GetStreamingTextureLoadingCount")]
			get;
		}

		public static extern bool streamingTextureForceLoadAll
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction(Name = "GetTextureStreamingManager().GetForceLoadAll")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction(Name = "GetTextureStreamingManager().SetForceLoadAll")]
			set;
		}

		public static extern bool streamingTextureDiscardUnusedMips
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction(Name = "GetTextureStreamingManager().GetDiscardUnusedMips")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction(Name = "GetTextureStreamingManager().SetDiscardUnusedMips")]
			set;
		}

		protected Texture()
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("SetGlobalAnisoLimits")]
		public static extern void SetGlobalAnisotropicFilteringLimits(int forcedMin, int globalMax);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetDataWidth();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetDataHeight();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern TextureDimension GetDimension();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IntPtr GetNativeTexturePtr();

		[Obsolete("Use GetNativeTexturePtr instead.", false)]
		public int GetNativeTextureID()
		{
			return (int)GetNativeTexturePtr();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void IncrementUpdateCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetTextureStreamingManager().SetStreamingTextureMaterialDebugProperties")]
		public static extern void SetStreamingTextureMaterialDebugProperties();

		internal bool ValidateFormat(RenderTextureFormat format)
		{
			if (SystemInfo.SupportsRenderTextureFormat(format))
			{
				return true;
			}
			Debug.LogError($"RenderTexture creation failed. '{format.ToString()}' is not supported on this platform. Use 'SystemInfo.SupportsRenderTextureFormat' C# API to check format support.", this);
			return false;
		}

		internal bool ValidateFormat(TextureFormat format)
		{
			if (SystemInfo.SupportsTextureFormat(format))
			{
				return true;
			}
			if (GraphicsFormatUtility.IsCompressedTextureFormat(format))
			{
				Debug.LogWarning($"'{format.ToString()}' is not supported on this platform. Decompressing texture. Use 'SystemInfo.SupportsTextureFormat' C# API to check format support.", this);
				return true;
			}
			Debug.LogError($"Texture creation failed. '{format.ToString()}' is not supported on this platform. Use 'SystemInfo.SupportsTextureFormat' C# API to check format support.", this);
			return false;
		}

		internal bool ValidateFormat(GraphicsFormat format, FormatUsage usage)
		{
			if (SystemInfo.IsFormatSupported(format, usage))
			{
				return true;
			}
			Debug.LogError($"Texture creation failed. '{format.ToString()}' is not supported for {usage.ToString()} usage on this platform. Use 'SystemInfo.IsFormatSupported' C# API to check format support.", this);
			return false;
		}

		internal UnityException CreateNonReadableException(Texture t)
		{
			return new UnityException($"Texture '{t.name}' is not readable, the texture memory can not be accessed from scripts. You can make the texture readable in the Texture Import Settings.");
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_texelSize_Injected(out Vector2 ret);
	}
}
