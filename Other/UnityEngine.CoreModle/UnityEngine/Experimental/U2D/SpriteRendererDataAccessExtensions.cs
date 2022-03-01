using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.U2D
{
	[NativeHeader("Runtime/Graphics/Mesh/SpriteRenderer.h")]
	[NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	public static class SpriteRendererDataAccessExtensions
	{
		public unsafe static NativeArray<Vector3> GetDeformableVertices(this SpriteRenderer spriteRenderer)
		{
			SpriteChannelInfo deformableChannelInfo = spriteRenderer.GetDeformableChannelInfo(VertexAttribute.Position);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3>(deformableChannelInfo.buffer, deformableChannelInfo.count, Allocator.Invalid);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeactivateDeformableBuffer(this SpriteRenderer renderer);

		public static void UpdateDeformableBuffer(this SpriteRenderer spriteRenderer, JobHandle fence)
		{
			UpdateDeformableBuffer_Injected(spriteRenderer, ref fence);
		}

		private static SpriteChannelInfo GetDeformableChannelInfo(this SpriteRenderer sprite, VertexAttribute channel)
		{
			GetDeformableChannelInfo_Injected(sprite, channel, out var ret);
			return ret;
		}

		internal static void SetLocalAABB(this SpriteRenderer renderer, Bounds aabb)
		{
			SetLocalAABB_Injected(renderer, ref aabb);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateDeformableBuffer_Injected(SpriteRenderer spriteRenderer, ref JobHandle fence);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDeformableChannelInfo_Injected(SpriteRenderer sprite, VertexAttribute channel, out SpriteChannelInfo ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalAABB_Injected(SpriteRenderer renderer, ref Bounds aabb);
	}
}
