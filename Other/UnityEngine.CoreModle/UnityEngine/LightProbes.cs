using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	[NativeAsStruct]
	[NativeHeader("Runtime/Export/Graphics.bindings.h")]
	public sealed class LightProbes : Object
	{
		public extern Vector3[] positions
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern SphericalHarmonicsL2[] bakedProbes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetBakedCoefficients")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("SetBakedCoefficients")]
			set;
		}

		public extern int count
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int cellCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetTetrahedraSize")]
			get;
		}

		[Obsolete("Use bakedProbes instead.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float[] coefficients
		{
			get
			{
				return new float[0];
			}
			set
			{
			}
		}

		private LightProbes()
		{
		}

		[FreeFunction]
		public static void GetInterpolatedProbe(Vector3 position, Renderer renderer, out SphericalHarmonicsL2 probe)
		{
			GetInterpolatedProbe_Injected(ref position, renderer, out probe);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		internal static extern bool AreLightProbesAllowed(Renderer renderer);

		public static void CalculateInterpolatedLightAndOcclusionProbes(Vector3[] positions, SphericalHarmonicsL2[] lightProbes, Vector4[] occlusionProbes)
		{
			if (positions == null)
			{
				throw new ArgumentNullException("positions");
			}
			if (lightProbes == null && occlusionProbes == null)
			{
				throw new ArgumentException("Argument lightProbes and occlusionProbes cannot both be null.");
			}
			if (lightProbes != null && lightProbes.Length < positions.Length)
			{
				throw new ArgumentException("lightProbes", "Argument lightProbes has less elements than positions");
			}
			if (occlusionProbes != null && occlusionProbes.Length < positions.Length)
			{
				throw new ArgumentException("occlusionProbes", "Argument occlusionProbes has less elements than positions");
			}
			CalculateInterpolatedLightAndOcclusionProbes_Internal(positions, positions.Length, lightProbes, occlusionProbes);
		}

		public static void CalculateInterpolatedLightAndOcclusionProbes(List<Vector3> positions, List<SphericalHarmonicsL2> lightProbes, List<Vector4> occlusionProbes)
		{
			if (positions == null)
			{
				throw new ArgumentNullException("positions");
			}
			if (lightProbes == null && occlusionProbes == null)
			{
				throw new ArgumentException("Argument lightProbes and occlusionProbes cannot both be null.");
			}
			if (lightProbes != null)
			{
				if (lightProbes.Capacity < positions.Count)
				{
					lightProbes.Capacity = positions.Count;
				}
				if (lightProbes.Count < positions.Count)
				{
					NoAllocHelpers.ResizeList(lightProbes, positions.Count);
				}
			}
			if (occlusionProbes != null)
			{
				if (occlusionProbes.Capacity < positions.Count)
				{
					occlusionProbes.Capacity = positions.Count;
				}
				if (occlusionProbes.Count < positions.Count)
				{
					NoAllocHelpers.ResizeList(occlusionProbes, positions.Count);
				}
			}
			CalculateInterpolatedLightAndOcclusionProbes_Internal(NoAllocHelpers.ExtractArrayFromListT(positions), positions.Count, NoAllocHelpers.ExtractArrayFromListT(lightProbes), NoAllocHelpers.ExtractArrayFromListT(occlusionProbes));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		[NativeName("CalculateInterpolatedLightAndOcclusionProbes")]
		internal static extern void CalculateInterpolatedLightAndOcclusionProbes_Internal(Vector3[] positions, int positionsCount, SphericalHarmonicsL2[] lightProbes, Vector4[] occlusionProbes);

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use GetInterpolatedProbe instead.", true)]
		public void GetInterpolatedLightProbe(Vector3 position, Renderer renderer, float[] coefficients)
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetInterpolatedProbe_Injected(ref Vector3 position, Renderer renderer, out SphericalHarmonicsL2 probe);
	}
}
