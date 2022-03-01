using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/GraphicsFormatUtility.bindings.h")]
	[NativeHeader("Runtime/Input/GetInput.h")]
	[NativeHeader("Runtime/Camera/RenderLoops/MotionVectorRenderLoop.h")]
	[NativeHeader("Runtime/Shaders/GraphicsCapsScriptBindings.h")]
	[NativeHeader("Runtime/Misc/SystemInfo.h")]
	public sealed class SystemInfo
	{
		public const string unsupportedIdentifier = "n/a";

		[NativeProperty]
		public static float batteryLevel => GetBatteryLevel();

		public static BatteryStatus batteryStatus => GetBatteryStatus();

		public static string operatingSystem => GetOperatingSystem();

		public static OperatingSystemFamily operatingSystemFamily => GetOperatingSystemFamily();

		public static string processorType => GetProcessorType();

		public static int processorFrequency => GetProcessorFrequencyMHz();

		public static int processorCount => GetProcessorCount();

		public static int systemMemorySize => GetPhysicalMemoryMB();

		public static string deviceUniqueIdentifier => GetDeviceUniqueIdentifier();

		public static string deviceName => GetDeviceName();

		public static string deviceModel => GetDeviceModel();

		public static bool supportsAccelerometer => SupportsAccelerometer();

		public static bool supportsGyroscope => IsGyroAvailable();

		public static bool supportsLocationService => SupportsLocationService();

		public static bool supportsVibration => SupportsVibration();

		public static bool supportsAudio => SupportsAudio();

		public static DeviceType deviceType => GetDeviceType();

		public static int graphicsMemorySize => GetGraphicsMemorySize();

		public static string graphicsDeviceName => GetGraphicsDeviceName();

		public static string graphicsDeviceVendor => GetGraphicsDeviceVendor();

		public static int graphicsDeviceID => GetGraphicsDeviceID();

		public static int graphicsDeviceVendorID => GetGraphicsDeviceVendorID();

		public static GraphicsDeviceType graphicsDeviceType => GetGraphicsDeviceType();

		public static bool graphicsUVStartsAtTop => GetGraphicsUVStartsAtTop();

		public static string graphicsDeviceVersion => GetGraphicsDeviceVersion();

		public static int graphicsShaderLevel => GetGraphicsShaderLevel();

		public static bool graphicsMultiThreaded => GetGraphicsMultiThreaded();

		public static bool hasHiddenSurfaceRemovalOnGPU => HasHiddenSurfaceRemovalOnGPU();

		public static bool hasDynamicUniformArrayIndexingInFragmentShaders => HasDynamicUniformArrayIndexingInFragmentShaders();

		public static bool supportsShadows => SupportsShadows();

		public static bool supportsRawShadowDepthSampling => SupportsRawShadowDepthSampling();

		[Obsolete("supportsRenderTextures always returns true, no need to call it")]
		public static bool supportsRenderTextures => true;

		public static bool supportsMotionVectors => SupportsMotionVectors();

		public static bool supportsRenderToCubemap => SupportsRenderToCubemap();

		public static bool supportsImageEffects => SupportsImageEffects();

		public static bool supports3DTextures => Supports3DTextures();

		public static bool supports2DArrayTextures => Supports2DArrayTextures();

		public static bool supports3DRenderTextures => Supports3DRenderTextures();

		public static bool supportsCubemapArrayTextures => SupportsCubemapArrayTextures();

		public static CopyTextureSupport copyTextureSupport => GetCopyTextureSupport();

		public static bool supportsComputeShaders => SupportsComputeShaders();

		public static bool supportsInstancing => SupportsInstancing();

		public static bool supportsHardwareQuadTopology => SupportsHardwareQuadTopology();

		public static bool supports32bitsIndexBuffer => Supports32bitsIndexBuffer();

		public static bool supportsSparseTextures => SupportsSparseTextures();

		public static int supportedRenderTargetCount => SupportedRenderTargetCount();

		public static bool supportsSeparatedRenderTargetsBlend => SupportsSeparatedRenderTargetsBlend();

		internal static int supportedRandomWriteTargetCount => SupportedRandomWriteTargetCount();

		public static int supportsMultisampledTextures => SupportsMultisampledTextures();

		public static bool supportsMultisampleAutoResolve => SupportsMultisampleAutoResolve();

		public static int supportsTextureWrapMirrorOnce => SupportsTextureWrapMirrorOnce();

		public static bool usesReversedZBuffer => UsesReversedZBuffer();

		[Obsolete("supportsStencil always returns true, no need to call it")]
		public static int supportsStencil => 1;

		public static NPOTSupport npotSupport => GetNPOTSupport();

		public static int maxTextureSize => GetMaxTextureSize();

		public static int maxCubemapSize => GetMaxCubemapSize();

		internal static int maxRenderTextureSize => GetMaxRenderTextureSize();

		public static bool supportsAsyncCompute => SupportsAsyncCompute();

		public static bool supportsGPUFence => SupportsGPUFence();

		public static bool supportsAsyncGPUReadback => SupportsAsyncGPUReadback();

		public static bool supportsMipStreaming => SupportsMipStreaming();

		[Obsolete("graphicsPixelFillrate is no longer supported in Unity 5.0+.")]
		public static int graphicsPixelFillrate => -1;

		[Obsolete("Vertex program support is required in Unity 5.0+")]
		public static bool supportsVertexPrograms => true;

		private static bool IsValidEnumValue(Enum value)
		{
			if (!Enum.IsDefined(value.GetType(), value))
			{
				return false;
			}
			return true;
		}

		public static bool SupportsRenderTextureFormat(RenderTextureFormat format)
		{
			if (!IsValidEnumValue(format))
			{
				throw new ArgumentException("Failed SupportsRenderTextureFormat; format is not a valid RenderTextureFormat");
			}
			return HasRenderTextureNative(format);
		}

		public static bool SupportsBlendingOnRenderTextureFormat(RenderTextureFormat format)
		{
			if (!IsValidEnumValue(format))
			{
				throw new ArgumentException("Failed SupportsBlendingOnRenderTextureFormat; format is not a valid RenderTextureFormat");
			}
			return SupportsBlendingOnRenderTextureFormatNative(format);
		}

		public static bool SupportsTextureFormat(TextureFormat format)
		{
			if (!IsValidEnumValue(format))
			{
				throw new ArgumentException("Failed SupportsTextureFormat; format is not a valid TextureFormat");
			}
			return SupportsTextureFormatNative(format);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("systeminfo::GetBatteryLevel")]
		private static extern float GetBatteryLevel();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("systeminfo::GetBatteryStatus")]
		private static extern BatteryStatus GetBatteryStatus();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("systeminfo::GetOperatingSystem")]
		private static extern string GetOperatingSystem();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("systeminfo::GetOperatingSystemFamily")]
		private static extern OperatingSystemFamily GetOperatingSystemFamily();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("systeminfo::GetProcessorType")]
		private static extern string GetProcessorType();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("systeminfo::GetProcessorFrequencyMHz")]
		private static extern int GetProcessorFrequencyMHz();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("systeminfo::GetProcessorCount")]
		private static extern int GetProcessorCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("systeminfo::GetPhysicalMemoryMB")]
		private static extern int GetPhysicalMemoryMB();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("systeminfo::GetDeviceUniqueIdentifier")]
		private static extern string GetDeviceUniqueIdentifier();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("systeminfo::GetDeviceName")]
		private static extern string GetDeviceName();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("systeminfo::GetDeviceModel")]
		private static extern string GetDeviceModel();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("systeminfo::SupportsAccelerometer")]
		private static extern bool SupportsAccelerometer();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		private static extern bool IsGyroAvailable();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("systeminfo::SupportsLocationService")]
		private static extern bool SupportsLocationService();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("systeminfo::SupportsVibration")]
		private static extern bool SupportsVibration();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("systeminfo::SupportsAudio")]
		private static extern bool SupportsAudio();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("systeminfo::GetDeviceType")]
		private static extern DeviceType GetDeviceType();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsMemorySize")]
		private static extern int GetGraphicsMemorySize();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceName")]
		private static extern string GetGraphicsDeviceName();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceVendor")]
		private static extern string GetGraphicsDeviceVendor();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceID")]
		private static extern int GetGraphicsDeviceID();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceVendorID")]
		private static extern int GetGraphicsDeviceVendorID();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceType")]
		private static extern GraphicsDeviceType GetGraphicsDeviceType();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsUVStartsAtTop")]
		private static extern bool GetGraphicsUVStartsAtTop();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsDeviceVersion")]
		private static extern string GetGraphicsDeviceVersion();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsShaderLevel")]
		private static extern int GetGraphicsShaderLevel();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::GetGraphicsMultiThreaded")]
		private static extern bool GetGraphicsMultiThreaded();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::HasHiddenSurfaceRemovalOnGPU")]
		private static extern bool HasHiddenSurfaceRemovalOnGPU();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::HasDynamicUniformArrayIndexingInFragmentShaders")]
		private static extern bool HasDynamicUniformArrayIndexingInFragmentShaders();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportsShadows")]
		private static extern bool SupportsShadows();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportsRawShadowDepthSampling")]
		private static extern bool SupportsRawShadowDepthSampling();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("SupportsMotionVectors")]
		private static extern bool SupportsMotionVectors();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportsRenderToCubemap")]
		private static extern bool SupportsRenderToCubemap();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportsImageEffects")]
		private static extern bool SupportsImageEffects();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::Supports3DTextures")]
		private static extern bool Supports3DTextures();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::Supports2DArrayTextures")]
		private static extern bool Supports2DArrayTextures();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::Supports3DRenderTextures")]
		private static extern bool Supports3DRenderTextures();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportsCubemapArrayTextures")]
		private static extern bool SupportsCubemapArrayTextures();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::GetCopyTextureSupport")]
		private static extern CopyTextureSupport GetCopyTextureSupport();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportsComputeShaders")]
		private static extern bool SupportsComputeShaders();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportsInstancing")]
		private static extern bool SupportsInstancing();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportsHardwareQuadTopology")]
		private static extern bool SupportsHardwareQuadTopology();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::Supports32bitsIndexBuffer")]
		private static extern bool Supports32bitsIndexBuffer();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportsSparseTextures")]
		private static extern bool SupportsSparseTextures();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportedRenderTargetCount")]
		private static extern int SupportedRenderTargetCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportsSeparatedRenderTargetsBlend")]
		private static extern bool SupportsSeparatedRenderTargetsBlend();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportedRandomWriteTargetCount")]
		private static extern int SupportedRandomWriteTargetCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampledTextures")]
		private static extern int SupportsMultisampledTextures();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportsMultisampleAutoResolve")]
		private static extern bool SupportsMultisampleAutoResolve();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportsTextureWrapMirrorOnce")]
		private static extern int SupportsTextureWrapMirrorOnce();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::UsesReversedZBuffer")]
		private static extern bool UsesReversedZBuffer();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::HasRenderTexture")]
		private static extern bool HasRenderTextureNative(RenderTextureFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportsBlendingOnRenderTextureFormat")]
		private static extern bool SupportsBlendingOnRenderTextureFormatNative(RenderTextureFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportsTextureFormat")]
		private static extern bool SupportsTextureFormatNative(TextureFormat format);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::GetNPOTSupport")]
		private static extern NPOTSupport GetNPOTSupport();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::GetMaxTextureSize")]
		private static extern int GetMaxTextureSize();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::GetMaxCubemapSize")]
		private static extern int GetMaxCubemapSize();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::GetMaxRenderTextureSize")]
		private static extern int GetMaxRenderTextureSize();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportsAsyncCompute")]
		private static extern bool SupportsAsyncCompute();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportsGPUFence")]
		private static extern bool SupportsGPUFence();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportsAsyncGPUReadback")]
		private static extern bool SupportsAsyncGPUReadback();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::SupportsMipStreaming")]
		private static extern bool SupportsMipStreaming();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptingGraphicsCaps::IsFormatSupported")]
		public static extern bool IsFormatSupported(GraphicsFormat format, FormatUsage usage);
	}
}
