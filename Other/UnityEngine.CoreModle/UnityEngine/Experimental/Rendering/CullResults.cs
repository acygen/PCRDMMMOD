using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	[UsedByNativeCode]
	public struct CullResults
	{
		public List<VisibleLight> visibleLights;

		public List<VisibleLight> visibleOffscreenVertexLights;

		public List<VisibleReflectionProbe> visibleReflectionProbes;

		public FilterResults visibleRenderers;

		internal IntPtr cullResults;

		private void Init()
		{
			visibleLights = new List<VisibleLight>();
			visibleOffscreenVertexLights = new List<VisibleLight>();
			visibleReflectionProbes = new List<VisibleReflectionProbe>();
			visibleRenderers = default(FilterResults);
			cullResults = IntPtr.Zero;
		}

		public unsafe static bool GetCullingParameters(Camera camera, out ScriptableCullingParameters cullingParameters)
		{
			return GetCullingParameters_Internal(camera, stereoAware: false, out cullingParameters, sizeof(ScriptableCullingParameters));
		}

		public unsafe static bool GetCullingParameters(Camera camera, bool stereoAware, out ScriptableCullingParameters cullingParameters)
		{
			return GetCullingParameters_Internal(camera, stereoAware, out cullingParameters, sizeof(ScriptableCullingParameters));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptableRenderLoop_Bindings::GetCullingParameters_Internal")]
		private static extern bool GetCullingParameters_Internal(Camera camera, bool stereoAware, out ScriptableCullingParameters cullingParameters, int managedCullingParametersSize);

		[FreeFunction("ScriptableRenderLoop_Bindings::Internal_Cull")]
		internal static void Internal_Cull(ref ScriptableCullingParameters parameters, ScriptableRenderContext renderLoop, ref CullResults results)
		{
			Internal_Cull_Injected(ref parameters, ref renderLoop, ref results);
		}

		public static CullResults Cull(ref ScriptableCullingParameters parameters, ScriptableRenderContext renderLoop)
		{
			CullResults results = default(CullResults);
			Cull(ref parameters, renderLoop, ref results);
			return results;
		}

		public static void Cull(ref ScriptableCullingParameters parameters, ScriptableRenderContext renderLoop, ref CullResults results)
		{
			if (results.visibleLights == null || results.visibleOffscreenVertexLights == null || results.visibleReflectionProbes == null)
			{
				results.Init();
			}
			Internal_Cull(ref parameters, renderLoop, ref results);
		}

		public static bool Cull(Camera camera, ScriptableRenderContext renderLoop, out CullResults results)
		{
			results.cullResults = IntPtr.Zero;
			results.visibleLights = null;
			results.visibleOffscreenVertexLights = null;
			results.visibleReflectionProbes = null;
			results.visibleRenderers = default(FilterResults);
			if (!GetCullingParameters(camera, out var cullingParameters))
			{
				return false;
			}
			results = Cull(ref cullingParameters, renderLoop);
			return true;
		}

		public bool GetShadowCasterBounds(int lightIndex, out Bounds outBounds)
		{
			return GetShadowCasterBounds(cullResults, lightIndex, out outBounds);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptableRenderLoop_Bindings::GetShadowCasterBounds")]
		private static extern bool GetShadowCasterBounds(IntPtr cullResults, int lightIndex, out Bounds bounds);

		public int GetLightIndicesCount()
		{
			return GetLightIndicesCount(cullResults);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptableRenderLoop_Bindings::GetLightIndicesCount")]
		private static extern int GetLightIndicesCount(IntPtr cullResults);

		public void FillLightIndices(ComputeBuffer computeBuffer)
		{
			FillLightIndices(cullResults, computeBuffer);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptableRenderLoop_Bindings::FillLightIndices")]
		private static extern void FillLightIndices(IntPtr cullResults, ComputeBuffer computeBuffer);

		public int[] GetLightIndexMap()
		{
			return GetLightIndexMap(cullResults);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptableRenderLoop_Bindings::GetLightIndexMap")]
		private static extern int[] GetLightIndexMap(IntPtr cullResults);

		public void SetLightIndexMap(int[] mapping)
		{
			SetLightIndexMap(cullResults, mapping);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptableRenderLoop_Bindings::SetLightIndexMap")]
		private static extern void SetLightIndexMap(IntPtr cullResults, int[] mapping);

		public bool ComputeSpotShadowMatricesAndCullingPrimitives(int activeLightIndex, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			return ComputeSpotShadowMatricesAndCullingPrimitives(cullResults, activeLightIndex, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptableRenderLoop_Bindings::ComputeSpotShadowMatricesAndCullingPrimitives")]
		private static extern bool ComputeSpotShadowMatricesAndCullingPrimitives(IntPtr cullResults, int activeLightIndex, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData);

		public bool ComputePointShadowMatricesAndCullingPrimitives(int activeLightIndex, CubemapFace cubemapFace, float fovBias, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			return ComputePointShadowMatricesAndCullingPrimitives(cullResults, activeLightIndex, cubemapFace, fovBias, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ScriptableRenderLoop_Bindings::ComputePointShadowMatricesAndCullingPrimitives")]
		private static extern bool ComputePointShadowMatricesAndCullingPrimitives(IntPtr cullResults, int activeLightIndex, CubemapFace cubemapFace, float fovBias, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData);

		public bool ComputeDirectionalShadowMatricesAndCullingPrimitives(int activeLightIndex, int splitIndex, int splitCount, Vector3 splitRatio, int shadowResolution, float shadowNearPlaneOffset, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			return ComputeDirectionalShadowMatricesAndCullingPrimitives(cullResults, activeLightIndex, splitIndex, splitCount, splitRatio, shadowResolution, shadowNearPlaneOffset, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		[FreeFunction("ScriptableRenderLoop_Bindings::ComputeDirectionalShadowMatricesAndCullingPrimitives")]
		private static bool ComputeDirectionalShadowMatricesAndCullingPrimitives(IntPtr cullResults, int activeLightIndex, int splitIndex, int splitCount, Vector3 splitRatio, int shadowResolution, float shadowNearPlaneOffset, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData)
		{
			return ComputeDirectionalShadowMatricesAndCullingPrimitives_Injected(cullResults, activeLightIndex, splitIndex, splitCount, ref splitRatio, shadowResolution, shadowNearPlaneOffset, out viewMatrix, out projMatrix, out shadowSplitData);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Cull_Injected(ref ScriptableCullingParameters parameters, ref ScriptableRenderContext renderLoop, ref CullResults results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ComputeDirectionalShadowMatricesAndCullingPrimitives_Injected(IntPtr cullResults, int activeLightIndex, int splitIndex, int splitCount, ref Vector3 splitRatio, int shadowResolution, float shadowNearPlaneOffset, out Matrix4x4 viewMatrix, out Matrix4x4 projMatrix, out ShadowSplitData shadowSplitData);
	}
}
