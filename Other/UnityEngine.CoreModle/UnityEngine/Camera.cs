using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Camera/RenderManager.h")]
	[NativeHeader("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
	[NativeHeader("Runtime/Misc/GameObjectUtility.h")]
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Runtime/Shaders/Shader.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/RenderTexture.h")]
	[NativeHeader("Runtime/Camera/Camera.h")]
	public sealed class Camera : Behaviour
	{
		internal enum ProjectionMatrixMode
		{
			Explicit,
			Implicit,
			PhysicalPropertiesBased
		}

		public enum GateFitMode
		{
			Vertical = 1,
			Horizontal = 2,
			Fill = 3,
			Overscan = 4,
			None = 0
		}

		public struct GateFitParameters
		{
			public GateFitMode mode { get; set; }

			public float aspect { get; set; }

			public GateFitParameters(GateFitMode mode, float aspect)
			{
				this.mode = mode;
				this.aspect = aspect;
			}
		}

		public enum StereoscopicEye
		{
			Left,
			Right
		}

		public enum MonoOrStereoscopicEye
		{
			Left,
			Right,
			Mono
		}

		public delegate void CameraCallback(Camera cam);

		public static CameraCallback onPreCull;

		public static CameraCallback onPreRender;

		public static CameraCallback onPostRender;

		[NativeProperty("Near")]
		public extern float nearClipPlane
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("Far")]
		public extern float farClipPlane
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("Fov")]
		public extern float fieldOfView
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern RenderingPath renderingPath
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern RenderingPath actualRenderingPath
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("CalculateRenderingPath")]
			get;
		}

		public extern bool allowHDR
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool allowMSAA
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool allowDynamicResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("ForceIntoRT")]
		public extern bool forceIntoRenderTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float orthographicSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool orthographic
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern OpaqueSortMode opaqueSortMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern TransparencySortMode transparencySortMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 transparencySortAxis
		{
			get
			{
				get_transparencySortAxis_Injected(out var ret);
				return ret;
			}
			set
			{
				set_transparencySortAxis_Injected(ref value);
			}
		}

		public extern float depth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float aspect
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 velocity
		{
			get
			{
				get_velocity_Injected(out var ret);
				return ret;
			}
		}

		public extern int cullingMask
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int eventMask
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool layerCullSpherical
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern CameraType cameraType
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public float[] layerCullDistances
		{
			get
			{
				return GetLayerCullDistances();
			}
			set
			{
				if (value.Length != 32)
				{
					throw new UnityException("Array needs to contain exactly 32 floats for layerCullDistances.");
				}
				SetLayerCullDistances(value);
			}
		}

		internal static extern int PreviewCullingLayer
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("CameraScripting::GetPreviewCullingLayer")]
			get;
		}

		public extern bool useOcclusionCulling
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Matrix4x4 cullingMatrix
		{
			get
			{
				get_cullingMatrix_Injected(out var ret);
				return ret;
			}
			set
			{
				set_cullingMatrix_Injected(ref value);
			}
		}

		public Color backgroundColor
		{
			get
			{
				get_backgroundColor_Injected(out var ret);
				return ret;
			}
			set
			{
				set_backgroundColor_Injected(ref value);
			}
		}

		public extern CameraClearFlags clearFlags
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern DepthTextureMode depthTextureMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool clearStencilAfterLightingPass
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal extern ProjectionMatrixMode projectionMatrixMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool usePhysicalProperties
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector2 sensorSize
		{
			get
			{
				get_sensorSize_Injected(out var ret);
				return ret;
			}
			set
			{
				set_sensorSize_Injected(ref value);
			}
		}

		public Vector2 lensShift
		{
			get
			{
				get_lensShift_Injected(out var ret);
				return ret;
			}
			set
			{
				set_lensShift_Injected(ref value);
			}
		}

		public extern float focalLength
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern GateFitMode gateFit
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("NormalizedViewportRect")]
		public Rect rect
		{
			get
			{
				get_rect_Injected(out var ret);
				return ret;
			}
			set
			{
				set_rect_Injected(ref value);
			}
		}

		[NativeProperty("ScreenViewportRect")]
		public Rect pixelRect
		{
			get
			{
				get_pixelRect_Injected(out var ret);
				return ret;
			}
			set
			{
				set_pixelRect_Injected(ref value);
			}
		}

		public extern int pixelWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("CameraScripting::GetPixelWidth", HasExplicitThis = true)]
			get;
		}

		public extern int pixelHeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("CameraScripting::GetPixelHeight", HasExplicitThis = true)]
			get;
		}

		public extern int scaledPixelWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("CameraScripting::GetScaledPixelWidth", HasExplicitThis = true)]
			get;
		}

		public extern int scaledPixelHeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("CameraScripting::GetScaledPixelHeight", HasExplicitThis = true)]
			get;
		}

		public extern RenderTexture targetTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern RenderTexture activeTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetCurrentTargetTexture")]
			get;
		}

		public extern int targetDisplay
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Matrix4x4 cameraToWorldMatrix
		{
			get
			{
				get_cameraToWorldMatrix_Injected(out var ret);
				return ret;
			}
		}

		public Matrix4x4 worldToCameraMatrix
		{
			get
			{
				get_worldToCameraMatrix_Injected(out var ret);
				return ret;
			}
			set
			{
				set_worldToCameraMatrix_Injected(ref value);
			}
		}

		public Matrix4x4 projectionMatrix
		{
			get
			{
				get_projectionMatrix_Injected(out var ret);
				return ret;
			}
			set
			{
				set_projectionMatrix_Injected(ref value);
			}
		}

		public Matrix4x4 nonJitteredProjectionMatrix
		{
			get
			{
				get_nonJitteredProjectionMatrix_Injected(out var ret);
				return ret;
			}
			set
			{
				set_nonJitteredProjectionMatrix_Injected(ref value);
			}
		}

		[NativeProperty("UseJitteredProjectionMatrixForTransparent")]
		public extern bool useJitteredProjectionMatrixForTransparentRendering
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Matrix4x4 previousViewProjectionMatrix
		{
			get
			{
				get_previousViewProjectionMatrix_Injected(out var ret);
				return ret;
			}
		}

		public static extern Camera main
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("FindMainCamera")]
			get;
		}

		public static extern Camera current
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetCurrentCameraPtr")]
			get;
		}

		public Scene scene
		{
			[FreeFunction("CameraScripting::GetScene", HasExplicitThis = true)]
			get
			{
				get_scene_Injected(out var ret);
				return ret;
			}
			[FreeFunction("CameraScripting::SetScene", HasExplicitThis = true)]
			set
			{
				set_scene_Injected(ref value);
			}
		}

		public extern bool stereoEnabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float stereoSeparation
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float stereoConvergence
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool areVRStereoViewMatricesWithinSingleCullTolerance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("AreVRStereoViewMatricesWithinSingleCullTolerance")]
			get;
		}

		public extern StereoTargetEyeMask stereoTargetEye
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern MonoOrStereoscopicEye stereoActiveEye
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("CameraScripting::GetStereoActiveEye", HasExplicitThis = true)]
			get;
		}

		public static int allCamerasCount => GetAllCamerasCount();

		public static Camera[] allCameras
		{
			get
			{
				Camera[] array = new Camera[allCamerasCount];
				GetAllCamerasImpl(array);
				return array;
			}
		}

		public extern int commandBufferCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Reset();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetTransparencySortSettings();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetAspect();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CameraScripting::GetLayerCullDistances", HasExplicitThis = true)]
		private extern float[] GetLayerCullDistances();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CameraScripting::SetLayerCullDistances", HasExplicitThis = true)]
		private extern void SetLayerCullDistances([NotNull] float[] d);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetCullingMatrix();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetReplacementShader(Shader shader, string replacementTag);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetReplacementShader();

		internal Vector3 GetLocalSpaceAim()
		{
			GetLocalSpaceAim_Injected(out var ret);
			return ret;
		}

		[FreeFunction("CameraScripting::SetTargetBuffers", HasExplicitThis = true)]
		private void SetTargetBuffersImpl(RenderBuffer color, RenderBuffer depth)
		{
			SetTargetBuffersImpl_Injected(ref color, ref depth);
		}

		public void SetTargetBuffers(RenderBuffer colorBuffer, RenderBuffer depthBuffer)
		{
			SetTargetBuffersImpl(colorBuffer, depthBuffer);
		}

		[FreeFunction("CameraScripting::SetTargetBuffers", HasExplicitThis = true)]
		private void SetTargetBuffersMRTImpl(RenderBuffer[] color, RenderBuffer depth)
		{
			SetTargetBuffersMRTImpl_Injected(color, ref depth);
		}

		public void SetTargetBuffers(RenderBuffer[] colorBuffer, RenderBuffer depthBuffer)
		{
			SetTargetBuffersMRTImpl(colorBuffer, depthBuffer);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string[] GetCameraBufferWarnings();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetWorldToCameraMatrix();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetProjectionMatrix();

		[FreeFunction("CameraScripting::CalculateObliqueMatrix", HasExplicitThis = true)]
		public Matrix4x4 CalculateObliqueMatrix(Vector4 clipPlane)
		{
			CalculateObliqueMatrix_Injected(ref clipPlane, out var ret);
			return ret;
		}

		public Vector3 WorldToScreenPoint(Vector3 position, MonoOrStereoscopicEye eye)
		{
			WorldToScreenPoint_Injected(ref position, eye, out var ret);
			return ret;
		}

		public Vector3 WorldToViewportPoint(Vector3 position, MonoOrStereoscopicEye eye)
		{
			WorldToViewportPoint_Injected(ref position, eye, out var ret);
			return ret;
		}

		public Vector3 ViewportToWorldPoint(Vector3 position, MonoOrStereoscopicEye eye)
		{
			ViewportToWorldPoint_Injected(ref position, eye, out var ret);
			return ret;
		}

		public Vector3 ScreenToWorldPoint(Vector3 position, MonoOrStereoscopicEye eye)
		{
			ScreenToWorldPoint_Injected(ref position, eye, out var ret);
			return ret;
		}

		public Vector3 WorldToScreenPoint(Vector3 position)
		{
			return WorldToScreenPoint(position, MonoOrStereoscopicEye.Mono);
		}

		public Vector3 WorldToViewportPoint(Vector3 position)
		{
			return WorldToViewportPoint(position, MonoOrStereoscopicEye.Mono);
		}

		public Vector3 ViewportToWorldPoint(Vector3 position)
		{
			return ViewportToWorldPoint(position, MonoOrStereoscopicEye.Mono);
		}

		public Vector3 ScreenToWorldPoint(Vector3 position)
		{
			return ScreenToWorldPoint(position, MonoOrStereoscopicEye.Mono);
		}

		public Vector3 ScreenToViewportPoint(Vector3 position)
		{
			ScreenToViewportPoint_Injected(ref position, out var ret);
			return ret;
		}

		public Vector3 ViewportToScreenPoint(Vector3 position)
		{
			ViewportToScreenPoint_Injected(ref position, out var ret);
			return ret;
		}

		internal Vector2 GetFrustumPlaneSizeAt(float distance)
		{
			GetFrustumPlaneSizeAt_Injected(distance, out var ret);
			return ret;
		}

		private Ray ViewportPointToRay(Vector2 pos, MonoOrStereoscopicEye eye)
		{
			ViewportPointToRay_Injected(ref pos, eye, out var ret);
			return ret;
		}

		public Ray ViewportPointToRay(Vector3 pos, MonoOrStereoscopicEye eye)
		{
			return ViewportPointToRay((Vector2)pos, eye);
		}

		public Ray ViewportPointToRay(Vector3 pos)
		{
			return ViewportPointToRay(pos, MonoOrStereoscopicEye.Mono);
		}

		private Ray ScreenPointToRay(Vector2 pos, MonoOrStereoscopicEye eye)
		{
			ScreenPointToRay_Injected(ref pos, eye, out var ret);
			return ret;
		}

		public Ray ScreenPointToRay(Vector3 pos, MonoOrStereoscopicEye eye)
		{
			return ScreenPointToRay((Vector2)pos, eye);
		}

		public Ray ScreenPointToRay(Vector3 pos)
		{
			return ScreenPointToRay(pos, MonoOrStereoscopicEye.Mono);
		}

		[FreeFunction("CameraScripting::RaycastTry", HasExplicitThis = true)]
		internal GameObject RaycastTry(Ray ray, float distance, int layerMask)
		{
			return RaycastTry_Injected(ref ray, distance, layerMask);
		}

		[FreeFunction("CameraScripting::RaycastTry2D", HasExplicitThis = true)]
		internal GameObject RaycastTry2D(Ray ray, float distance, int layerMask)
		{
			return RaycastTry2D_Injected(ref ray, distance, layerMask);
		}

		[FreeFunction("CameraScripting::CalculateViewportRayVectors", HasExplicitThis = true)]
		private void CalculateFrustumCornersInternal(Rect viewport, float z, MonoOrStereoscopicEye eye, [Out] Vector3[] outCorners)
		{
			CalculateFrustumCornersInternal_Injected(ref viewport, z, eye, outCorners);
		}

		public void CalculateFrustumCorners(Rect viewport, float z, MonoOrStereoscopicEye eye, Vector3[] outCorners)
		{
			if (outCorners == null)
			{
				throw new ArgumentNullException("outCorners");
			}
			if (outCorners.Length < 4)
			{
				throw new ArgumentException("outCorners minimum size is 4", "outCorners");
			}
			CalculateFrustumCornersInternal(viewport, z, eye, outCorners);
		}

		[NativeName("CalculateProjectionMatrixFromPhysicalProperties")]
		private static void CalculateProjectionMatrixFromPhysicalPropertiesInternal(out Matrix4x4 output, float focalLength, Vector2 sensorSize, Vector2 lensShift, float nearClip, float farClip, float gateAspect, GateFitMode gateFitMode)
		{
			CalculateProjectionMatrixFromPhysicalPropertiesInternal_Injected(out output, focalLength, ref sensorSize, ref lensShift, nearClip, farClip, gateAspect, gateFitMode);
		}

		public static void CalculateProjectionMatrixFromPhysicalProperties(out Matrix4x4 output, float focalLength, Vector2 sensorSize, Vector2 lensShift, float nearClip, float farClip, GateFitParameters gateFitParameters = default(GateFitParameters))
		{
			CalculateProjectionMatrixFromPhysicalPropertiesInternal(out output, focalLength, sensorSize, lensShift, nearClip, farClip, gateFitParameters.aspect, gateFitParameters.mode);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float FocalLengthToFOV(float focalLength, float sensorSize);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float FOVToFocalLength(float fov, float sensorSize);

		public Matrix4x4 GetStereoNonJitteredProjectionMatrix(StereoscopicEye eye)
		{
			GetStereoNonJitteredProjectionMatrix_Injected(eye, out var ret);
			return ret;
		}

		public Matrix4x4 GetStereoViewMatrix(StereoscopicEye eye)
		{
			GetStereoViewMatrix_Injected(eye, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CopyStereoDeviceProjectionMatrixToNonJittered(StereoscopicEye eye);

		public Matrix4x4 GetStereoProjectionMatrix(StereoscopicEye eye)
		{
			GetStereoProjectionMatrix_Injected(eye, out var ret);
			return ret;
		}

		public void SetStereoProjectionMatrix(StereoscopicEye eye, Matrix4x4 matrix)
		{
			SetStereoProjectionMatrix_Injected(eye, ref matrix);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetStereoProjectionMatrices();

		public void SetStereoViewMatrix(StereoscopicEye eye, Matrix4x4 matrix)
		{
			SetStereoViewMatrix_Injected(eye, ref matrix);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetStereoViewMatrices();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CameraScripting::GetAllCamerasCount")]
		private static extern int GetAllCamerasCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CameraScripting::GetAllCameras")]
		private static extern int GetAllCamerasImpl([Out][NotNull] Camera[] cam);

		public static int GetAllCameras(Camera[] cameras)
		{
			if (cameras == null)
			{
				throw new NullReferenceException();
			}
			if (cameras.Length < allCamerasCount)
			{
				throw new ArgumentException("Passed in array to fill with cameras is to small to hold the number of cameras. Use Camera.allCamerasCount to get the needed size.");
			}
			return GetAllCamerasImpl(cameras);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CameraScripting::RenderToCubemap", HasExplicitThis = true)]
		private extern bool RenderToCubemapImpl(Texture tex, [DefaultValue("63")] int faceMask);

		public bool RenderToCubemap(Cubemap cubemap, int faceMask)
		{
			return RenderToCubemapImpl(cubemap, faceMask);
		}

		public bool RenderToCubemap(Cubemap cubemap)
		{
			return RenderToCubemapImpl(cubemap, 63);
		}

		public bool RenderToCubemap(RenderTexture cubemap, int faceMask)
		{
			return RenderToCubemapImpl(cubemap, faceMask);
		}

		public bool RenderToCubemap(RenderTexture cubemap)
		{
			return RenderToCubemapImpl(cubemap, 63);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("RenderToCubemap")]
		private extern bool RenderToCubemapEyeImpl(RenderTexture cubemap, int faceMask, MonoOrStereoscopicEye stereoEye);

		public bool RenderToCubemap(RenderTexture cubemap, int faceMask, MonoOrStereoscopicEye stereoEye)
		{
			return RenderToCubemapEyeImpl(cubemap, faceMask, stereoEye);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CameraScripting::Render", HasExplicitThis = true)]
		public extern void Render();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CameraScripting::RenderWithShader", HasExplicitThis = true)]
		public extern void RenderWithShader(Shader shader, string replacementTag);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CameraScripting::RenderDontRestore", HasExplicitThis = true)]
		public extern void RenderDontRestore();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CameraScripting::SetupCurrent")]
		public static extern void SetupCurrent(Camera cur);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CameraScripting::CopyFrom", HasExplicitThis = true)]
		public extern void CopyFrom(Camera other);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveCommandBuffers(CameraEvent evt);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveAllCommandBuffers();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("AddCommandBuffer")]
		private extern void AddCommandBufferImpl(CameraEvent evt, [NotNull] CommandBuffer buffer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("AddCommandBufferAsync")]
		private extern void AddCommandBufferAsyncImpl(CameraEvent evt, [NotNull] CommandBuffer buffer, ComputeQueueType queueType);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("RemoveCommandBuffer")]
		private extern void RemoveCommandBufferImpl(CameraEvent evt, [NotNull] CommandBuffer buffer);

		public void AddCommandBuffer(CameraEvent evt, CommandBuffer buffer)
		{
			if (!CameraEventUtils.IsValid(evt))
			{
				throw new ArgumentException($"Invalid CameraEvent value \"{(int)evt}\".", "evt");
			}
			if (buffer == null)
			{
				throw new NullReferenceException("buffer is null");
			}
			AddCommandBufferImpl(evt, buffer);
		}

		public void AddCommandBufferAsync(CameraEvent evt, CommandBuffer buffer, ComputeQueueType queueType)
		{
			if (!CameraEventUtils.IsValid(evt))
			{
				throw new ArgumentException($"Invalid CameraEvent value \"{(int)evt}\".", "evt");
			}
			if (buffer == null)
			{
				throw new NullReferenceException("buffer is null");
			}
			AddCommandBufferAsyncImpl(evt, buffer, queueType);
		}

		public void RemoveCommandBuffer(CameraEvent evt, CommandBuffer buffer)
		{
			if (!CameraEventUtils.IsValid(evt))
			{
				throw new ArgumentException($"Invalid CameraEvent value \"{(int)evt}\".", "evt");
			}
			if (buffer == null)
			{
				throw new NullReferenceException("buffer is null");
			}
			RemoveCommandBufferImpl(evt, buffer);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CameraScripting::GetCommandBuffers", HasExplicitThis = true)]
		public extern CommandBuffer[] GetCommandBuffers(CameraEvent evt);

		[RequiredByNativeCode]
		private static void FireOnPreCull(Camera cam)
		{
			if (onPreCull != null)
			{
				onPreCull(cam);
			}
		}

		[RequiredByNativeCode]
		private static void FireOnPreRender(Camera cam)
		{
			if (onPreRender != null)
			{
				onPreRender(cam);
			}
		}

		[RequiredByNativeCode]
		private static void FireOnPostRender(Camera cam)
		{
			if (onPostRender != null)
			{
				onPostRender(cam);
			}
		}

		internal void OnlyUsedForTesting1()
		{
		}

		internal void OnlyUsedForTesting2()
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_transparencySortAxis_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_transparencySortAxis_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_velocity_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_cullingMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_cullingMatrix_Injected(ref Matrix4x4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_backgroundColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_backgroundColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_sensorSize_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_sensorSize_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_lensShift_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_lensShift_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetLocalSpaceAim_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_rect_Injected(out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_rect_Injected(ref Rect value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_pixelRect_Injected(out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_pixelRect_Injected(ref Rect value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTargetBuffersImpl_Injected(ref RenderBuffer color, ref RenderBuffer depth);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTargetBuffersMRTImpl_Injected(RenderBuffer[] color, ref RenderBuffer depth);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_cameraToWorldMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_worldToCameraMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_worldToCameraMatrix_Injected(ref Matrix4x4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_projectionMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_projectionMatrix_Injected(ref Matrix4x4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_nonJitteredProjectionMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_nonJitteredProjectionMatrix_Injected(ref Matrix4x4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_previousViewProjectionMatrix_Injected(out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CalculateObliqueMatrix_Injected(ref Vector4 clipPlane, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void WorldToScreenPoint_Injected(ref Vector3 position, MonoOrStereoscopicEye eye, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void WorldToViewportPoint_Injected(ref Vector3 position, MonoOrStereoscopicEye eye, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ViewportToWorldPoint_Injected(ref Vector3 position, MonoOrStereoscopicEye eye, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ScreenToWorldPoint_Injected(ref Vector3 position, MonoOrStereoscopicEye eye, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ScreenToViewportPoint_Injected(ref Vector3 position, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ViewportToScreenPoint_Injected(ref Vector3 position, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetFrustumPlaneSizeAt_Injected(float distance, out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ViewportPointToRay_Injected(ref Vector2 pos, MonoOrStereoscopicEye eye, out Ray ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ScreenPointToRay_Injected(ref Vector2 pos, MonoOrStereoscopicEye eye, out Ray ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern GameObject RaycastTry_Injected(ref Ray ray, float distance, int layerMask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern GameObject RaycastTry2D_Injected(ref Ray ray, float distance, int layerMask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CalculateFrustumCornersInternal_Injected(ref Rect viewport, float z, MonoOrStereoscopicEye eye, [Out] Vector3[] outCorners);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateProjectionMatrixFromPhysicalPropertiesInternal_Injected(out Matrix4x4 output, float focalLength, ref Vector2 sensorSize, ref Vector2 lensShift, float nearClip, float farClip, float gateAspect, GateFitMode gateFitMode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_scene_Injected(out Scene ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_scene_Injected(ref Scene value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetStereoNonJitteredProjectionMatrix_Injected(StereoscopicEye eye, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetStereoViewMatrix_Injected(StereoscopicEye eye, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetStereoProjectionMatrix_Injected(StereoscopicEye eye, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetStereoProjectionMatrix_Injected(StereoscopicEye eye, ref Matrix4x4 matrix);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetStereoViewMatrix_Injected(StereoscopicEye eye, ref Matrix4x4 matrix);
	}
}
