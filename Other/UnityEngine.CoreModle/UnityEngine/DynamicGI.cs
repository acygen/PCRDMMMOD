using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/GI/DynamicGI.h")]
	public sealed class DynamicGI
	{
		public static extern float indirectScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float updateThreshold
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern int materialUpdateTimeSlice
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool synchronousMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool isConverged
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static extern int scheduledMaterialUpdatesCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static void SetEmissive(Renderer renderer, Color color)
		{
			SetEmissive_Injected(renderer, ref color);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		public static extern void SetEnvironmentData([NotNull] float[] input);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UpdateEnvironment();

		[Obsolete("DynamicGI.UpdateMaterials(Renderer) is deprecated; instead, use extension method from RendererExtensions: 'renderer.UpdateGIMaterials()' (UnityUpgradable).", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void UpdateMaterials(Renderer renderer)
		{
		}

		[Obsolete("DynamicGI.UpdateMaterials(Terrain) is deprecated; instead, use extension method from TerrainExtensions: 'terrain.UpdateGIMaterials()' (UnityUpgradable).", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void UpdateMaterials(Object renderer)
		{
		}

		[Obsolete("DynamicGI.UpdateMaterials(Terrain, int, int, int, int) is deprecated; instead, use extension method from TerrainExtensions: 'terrain.UpdateGIMaterials(x, y, width, height)' (UnityUpgradable).", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void UpdateMaterials(Object renderer, int x, int y, int width, int height)
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetEmissive_Injected(Renderer renderer, ref Color color);
	}
}
