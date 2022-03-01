using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.U2D
{
	[NativeType(Header = "Runtime/2D/SpriteAtlas/SpriteAtlas.h")]
	[NativeHeader("Runtime/Graphics/SpriteFrame.h")]
	public class SpriteAtlas : Object
	{
		public extern bool isVariant
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("IsVariant")]
			get;
		}

		public extern string tag
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int spriteCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool CanBindTo(Sprite sprite);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Sprite GetSprite(string name);

		public int GetSprites(Sprite[] sprites)
		{
			return GetSpritesScripting(sprites);
		}

		public int GetSprites(Sprite[] sprites, string name)
		{
			return GetSpritesWithNameScripting(sprites, name);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetSpritesScripting(Sprite[] sprites);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetSpritesWithNameScripting(Sprite[] sprites, string name);
	}
}
