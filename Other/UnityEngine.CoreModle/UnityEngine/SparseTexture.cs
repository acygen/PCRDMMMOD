using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/SparseTexture.h")]
	public sealed class SparseTexture : Texture
	{
		public extern int tileWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int tileHeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isCreated
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("IsInitialized")]
			get;
		}

		public SparseTexture(int width, int height, GraphicsFormat format, int mipCount)
		{
			if (ValidateFormat(format, FormatUsage.Sample))
			{
				Internal_Create(this, width, height, GraphicsFormatUtility.GetTextureFormat(format), GraphicsFormatUtility.IsSRGBFormat(format), mipCount);
			}
		}

		public SparseTexture(int width, int height, TextureFormat format, int mipCount)
		{
			if (ValidateFormat(format))
			{
				Internal_Create(this, width, height, format, linear: false, mipCount);
			}
		}

		public SparseTexture(int width, int height, TextureFormat format, int mipCount, bool linear)
		{
			if (ValidateFormat(format))
			{
				Internal_Create(this, width, height, format, linear, mipCount);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "SparseTextureScripting::Create", ThrowsException = true)]
		private static extern void Internal_Create([Writable] SparseTexture mono, int width, int height, TextureFormat format, bool linear, int mipCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "SparseTextureScripting::UpdateTile", HasExplicitThis = true)]
		public extern void UpdateTile(int tileX, int tileY, int miplevel, Color32[] data);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "SparseTextureScripting::UpdateTileRaw", HasExplicitThis = true)]
		public extern void UpdateTileRaw(int tileX, int tileY, int miplevel, byte[] data);

		public void UnloadTile(int tileX, int tileY, int miplevel)
		{
			UpdateTileRaw(tileX, tileY, miplevel, null);
		}
	}
}
