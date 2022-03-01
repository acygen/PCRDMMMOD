using System;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	public class SupportedRenderingFeatures
	{
		[Flags]
		public enum ReflectionProbeSupportFlags
		{
			None = 0x0,
			Rotation = 0x1
		}

		[Flags]
		public enum LightmapMixedBakeMode
		{
			None = 0x0,
			IndirectOnly = 0x1,
			Subtractive = 0x2,
			Shadowmask = 0x4
		}

		private static SupportedRenderingFeatures s_Active = new SupportedRenderingFeatures();

		public static SupportedRenderingFeatures active
		{
			get
			{
				if (s_Active == null)
				{
					s_Active = new SupportedRenderingFeatures();
				}
				return s_Active;
			}
			set
			{
				s_Active = value;
			}
		}

		public ReflectionProbeSupportFlags reflectionProbeSupportFlags { get; set; } = ReflectionProbeSupportFlags.None;


		public LightmapMixedBakeMode defaultMixedLightingMode { get; set; } = LightmapMixedBakeMode.None;


		public LightmapMixedBakeMode supportedMixedLightingModes { get; set; } = LightmapMixedBakeMode.IndirectOnly | LightmapMixedBakeMode.Subtractive | LightmapMixedBakeMode.Shadowmask;


		public LightmapBakeType supportedLightmapBakeTypes { get; set; } = LightmapBakeType.Realtime | LightmapBakeType.Baked | LightmapBakeType.Mixed;


		public LightmapsMode supportedLightmapsModes { get; set; } = LightmapsMode.CombinedDirectional;


		public bool rendererSupportsLightProbeProxyVolumes { get; set; } = true;


		public bool rendererSupportsMotionVectors { get; set; } = true;


		public bool rendererSupportsReceiveShadows { get; set; } = true;


		public bool rendererSupportsReflectionProbes { get; set; } = true;


		public bool rendererSupportsRendererPriority { get; set; } = false;


		public bool rendererOverridesEnvironmentLighting { get; set; } = false;


		public bool rendererOverridesFog { get; set; } = false;


		public bool rendererOverridesOtherLightingSettings { get; set; } = false;


		internal unsafe static MixedLightingMode FallbackMixedLightingMode()
		{
			MixedLightingMode result = default(MixedLightingMode);
			FallbackMixedLightingModeByRef(new IntPtr(&result));
			return result;
		}

		[RequiredByNativeCode]
		internal unsafe static void FallbackMixedLightingModeByRef(IntPtr fallbackModePtr)
		{
			MixedLightingMode* ptr = (MixedLightingMode*)(void*)fallbackModePtr;
			if (active.defaultMixedLightingMode != 0 && (active.supportedMixedLightingModes & active.defaultMixedLightingMode) == active.defaultMixedLightingMode)
			{
				switch (active.defaultMixedLightingMode)
				{
				case LightmapMixedBakeMode.Shadowmask:
					*ptr = MixedLightingMode.Shadowmask;
					break;
				case LightmapMixedBakeMode.Subtractive:
					*ptr = MixedLightingMode.Subtractive;
					break;
				default:
					*ptr = MixedLightingMode.IndirectOnly;
					break;
				}
			}
			else if (IsMixedLightingModeSupported(MixedLightingMode.Shadowmask))
			{
				*ptr = MixedLightingMode.Shadowmask;
			}
			else if (IsMixedLightingModeSupported(MixedLightingMode.Subtractive))
			{
				*ptr = MixedLightingMode.Subtractive;
			}
			else
			{
				*ptr = MixedLightingMode.IndirectOnly;
			}
		}

		internal unsafe static bool IsMixedLightingModeSupported(MixedLightingMode mixedMode)
		{
			bool result = default(bool);
			IsMixedLightingModeSupportedByRef(mixedMode, new IntPtr(&result));
			return result;
		}

		[RequiredByNativeCode]
		internal unsafe static void IsMixedLightingModeSupportedByRef(MixedLightingMode mixedMode, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			if (!IsLightmapBakeTypeSupported(LightmapBakeType.Mixed))
			{
				*ptr = false;
			}
			else
			{
				*ptr = (mixedMode == MixedLightingMode.IndirectOnly && (active.supportedMixedLightingModes & LightmapMixedBakeMode.IndirectOnly) == LightmapMixedBakeMode.IndirectOnly) || (mixedMode == MixedLightingMode.Subtractive && (active.supportedMixedLightingModes & LightmapMixedBakeMode.Subtractive) == LightmapMixedBakeMode.Subtractive) || (mixedMode == MixedLightingMode.Shadowmask && (active.supportedMixedLightingModes & LightmapMixedBakeMode.Shadowmask) == LightmapMixedBakeMode.Shadowmask);
			}
		}

		internal unsafe static bool IsLightmapBakeTypeSupported(LightmapBakeType bakeType)
		{
			bool result = default(bool);
			IsLightmapBakeTypeSupportedByRef(bakeType, new IntPtr(&result));
			return result;
		}

		[RequiredByNativeCode]
		internal unsafe static void IsLightmapBakeTypeSupportedByRef(LightmapBakeType bakeType, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			if (bakeType == LightmapBakeType.Mixed && (!IsLightmapBakeTypeSupported(LightmapBakeType.Baked) || active.supportedMixedLightingModes == LightmapMixedBakeMode.None))
			{
				*ptr = false;
			}
			else
			{
				*ptr = (active.supportedLightmapBakeTypes & bakeType) == bakeType;
			}
		}

		internal unsafe static bool IsLightmapsModeSupported(LightmapsMode mode)
		{
			bool result = default(bool);
			IsLightmapsModeSupportedByRef(mode, new IntPtr(&result));
			return result;
		}

		[RequiredByNativeCode]
		internal unsafe static void IsLightmapsModeSupportedByRef(LightmapsMode mode, IntPtr isSupportedPtr)
		{
			bool* ptr = (bool*)(void*)isSupportedPtr;
			*ptr = (active.supportedLightmapsModes & mode) == mode;
		}
	}
}
