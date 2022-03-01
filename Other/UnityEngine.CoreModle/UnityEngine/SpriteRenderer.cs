using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeType("Runtime/Graphics/Mesh/SpriteRenderer.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class SpriteRenderer : Renderer
	{
		internal extern bool shouldSupportTiling
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("ShouldSupportTiling")]
			get;
		}

		public extern Sprite sprite
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern SpriteDrawMode drawMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector2 size
		{
			get
			{
				get_size_Injected(out var ret);
				return ret;
			}
			set
			{
				set_size_Injected(ref value);
			}
		}

		public extern float adaptiveModeThreshold
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern SpriteTileMode tileMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Color color
		{
			get
			{
				get_color_Injected(out var ret);
				return ret;
			}
			set
			{
				set_color_Injected(ref value);
			}
		}

		public extern SpriteMaskInteraction maskInteraction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool flipX
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool flipY
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern SpriteSortPoint spriteSortPoint
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeMethod(Name = "GetSpriteBounds")]
		internal Bounds Internal_GetSpriteBounds(SpriteDrawMode mode)
		{
			Internal_GetSpriteBounds_Injected(mode, out var ret);
			return ret;
		}

		internal Bounds GetSpriteBounds()
		{
			return Internal_GetSpriteBounds(drawMode);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_size_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_size_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_color_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_color_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetSpriteBounds_Injected(SpriteDrawMode mode, out Bounds ret);
	}
}
