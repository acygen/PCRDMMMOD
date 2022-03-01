using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	[NativeHeader("Runtime/Camera/Light.h")]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Runtime/Export/Light.bindings.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class Light : Behaviour
	{
		private int m_BakedIndex;

		[NativeProperty("LightType")]
		public extern LightType type
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float spotAngle
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

		public extern float colorTemperature
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float intensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float bounceIntensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int shadowCustomResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float shadowBias
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float shadowNormalBias
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float shadowNearPlane
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float range
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Flare flare
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public LightBakingOutput bakingOutput
		{
			get
			{
				get_bakingOutput_Injected(out var ret);
				return ret;
			}
			set
			{
				set_bakingOutput_Injected(ref value);
			}
		}

		public extern int cullingMask
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern LightShadowCasterMode lightShadowCasterMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern LightShadows shadows
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetShadowType")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("Light_Bindings::SetShadowType", HasExplicitThis = true, ThrowsException = true)]
			set;
		}

		public extern float shadowStrength
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("Light_Bindings::SetShadowStrength", HasExplicitThis = true)]
			set;
		}

		public extern LightShadowResolution shadowResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("Light_Bindings::SetShadowResolution", HasExplicitThis = true, ThrowsException = true)]
			set;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Shadow softness is removed in Unity 5.0+", true)]
		public float shadowSoftness
		{
			get
			{
				return 4f;
			}
			set
			{
			}
		}

		[Obsolete("Shadow softness is removed in Unity 5.0+", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float shadowSoftnessFade
		{
			get
			{
				return 1f;
			}
			set
			{
			}
		}

		public extern float[] layerShadowCullDistances
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("Light_Bindings::GetLayerShadowCullDistances", HasExplicitThis = true, ThrowsException = false)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("Light_Bindings::SetLayerShadowCullDistances", HasExplicitThis = true, ThrowsException = true)]
			set;
		}

		public extern float cookieSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Texture cookie
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern LightRenderMode renderMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("Light_Bindings::SetRenderMode", HasExplicitThis = true, ThrowsException = true)]
			set;
		}

		[Obsolete("warning bakedIndex has been removed please use bakingOutput.isBaked instead.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int bakedIndex
		{
			get
			{
				return m_BakedIndex;
			}
			set
			{
				m_BakedIndex = value;
			}
		}

		public extern int commandBufferCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[Obsolete("Use QualitySettings.pixelLightCount instead.")]
		public static int pixelLightCount
		{
			get
			{
				return QualitySettings.pixelLightCount;
			}
			set
			{
				QualitySettings.pixelLightCount = value;
			}
		}

		[Obsolete("light.shadowConstantBias was removed, use light.shadowBias", true)]
		public float shadowConstantBias
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[Obsolete("light.shadowObjectSizeBias was removed, use light.shadowBias", true)]
		public float shadowObjectSizeBias
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[Obsolete("light.attenuate was removed; all lights always attenuate now", true)]
		public bool attenuate
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Reset();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Light_Bindings::SetFalloffTable", HasExplicitThis = true, ThrowsException = true)]
		private extern void SetFalloffTable([NotNull] float[] input);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Light_Bindings::SetAllLightsFalloffToInverseSquared")]
		private static extern void SetAllLightsFalloffToInverseSquared();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Light_Bindings::SetAllLightsFalloffToUnityLegacy")]
		private static extern void SetAllLightsFalloffToUnityLegacy();

		public void AddCommandBuffer(LightEvent evt, CommandBuffer buffer)
		{
			AddCommandBuffer(evt, buffer, ShadowMapPass.All);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Light_Bindings::AddCommandBuffer", HasExplicitThis = true)]
		public extern void AddCommandBuffer(LightEvent evt, CommandBuffer buffer, ShadowMapPass shadowPassMask);

		public void AddCommandBufferAsync(LightEvent evt, CommandBuffer buffer, ComputeQueueType queueType)
		{
			AddCommandBufferAsync(evt, buffer, ShadowMapPass.All, queueType);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Light_Bindings::AddCommandBufferAsync", HasExplicitThis = true)]
		public extern void AddCommandBufferAsync(LightEvent evt, CommandBuffer buffer, ShadowMapPass shadowPassMask, ComputeQueueType queueType);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveCommandBuffer(LightEvent evt, CommandBuffer buffer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveCommandBuffers(LightEvent evt);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveAllCommandBuffers();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Light_Bindings::GetCommandBuffers", HasExplicitThis = true)]
		public extern CommandBuffer[] GetCommandBuffers(LightEvent evt);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("Light_Bindings::GetLights")]
		public static extern Light[] GetLights(LightType type, int layer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_color_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_color_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_bakingOutput_Injected(out LightBakingOutput ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_bakingOutput_Injected(ref LightBakingOutput value);
	}
}
