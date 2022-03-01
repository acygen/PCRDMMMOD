using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/QualitySettingsTypes.h")]
	[NativeHeader("Runtime/Camera/RenderSettings.h")]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[StaticAccessor("GetRenderSettings()", StaticAccessorType.Dot)]
	public sealed class RenderSettings : Object
	{
		[Obsolete("Use RenderSettings.ambientIntensity instead (UnityUpgradable) -> ambientIntensity", false)]
		public static float ambientSkyboxAmount
		{
			get
			{
				return ambientIntensity;
			}
			set
			{
				ambientIntensity = value;
			}
		}

		[NativeProperty("UseFog")]
		public static extern bool fog
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("LinearFogStart")]
		public static extern float fogStartDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("LinearFogEnd")]
		public static extern float fogEndDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern FogMode fogMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static Color fogColor
		{
			get
			{
				get_fogColor_Injected(out var ret);
				return ret;
			}
			set
			{
				set_fogColor_Injected(ref value);
			}
		}

		public static extern float fogDensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern AmbientMode ambientMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static Color ambientSkyColor
		{
			get
			{
				get_ambientSkyColor_Injected(out var ret);
				return ret;
			}
			set
			{
				set_ambientSkyColor_Injected(ref value);
			}
		}

		public static Color ambientEquatorColor
		{
			get
			{
				get_ambientEquatorColor_Injected(out var ret);
				return ret;
			}
			set
			{
				set_ambientEquatorColor_Injected(ref value);
			}
		}

		public static Color ambientGroundColor
		{
			get
			{
				get_ambientGroundColor_Injected(out var ret);
				return ret;
			}
			set
			{
				set_ambientGroundColor_Injected(ref value);
			}
		}

		public static extern float ambientIntensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("AmbientSkyColor")]
		public static Color ambientLight
		{
			get
			{
				get_ambientLight_Injected(out var ret);
				return ret;
			}
			set
			{
				set_ambientLight_Injected(ref value);
			}
		}

		public static Color subtractiveShadowColor
		{
			get
			{
				get_subtractiveShadowColor_Injected(out var ret);
				return ret;
			}
			set
			{
				set_subtractiveShadowColor_Injected(ref value);
			}
		}

		[NativeProperty("SkyboxMaterial")]
		public static extern Material skybox
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern Light sun
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static SphericalHarmonicsL2 ambientProbe
		{
			get
			{
				get_ambientProbe_Injected(out var ret);
				return ret;
			}
			set
			{
				set_ambientProbe_Injected(ref value);
			}
		}

		public static extern Cubemap customReflection
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float reflectionIntensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int reflectionBounces
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern DefaultReflectionMode defaultReflectionMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int defaultReflectionResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float haloStrength
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float flareStrength
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float flareFadeSpeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		private RenderSettings()
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetRenderSettings")]
		internal static extern Object GetRenderSettings();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("RenderSettingsScripting", StaticAccessorType.DoubleColon)]
		internal static extern void Reset();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void get_fogColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void set_fogColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void get_ambientSkyColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void set_ambientSkyColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void get_ambientEquatorColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void set_ambientEquatorColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void get_ambientGroundColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void set_ambientGroundColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void get_ambientLight_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void set_ambientLight_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void get_subtractiveShadowColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void set_subtractiveShadowColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void get_ambientProbe_Injected(out SphericalHarmonicsL2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void set_ambientProbe_Injected(ref SphericalHarmonicsL2 value);
	}
}
