using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.U2D
{
	[NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	[NativeHeader("Runtime/Graphics/SpriteFrame.h")]
	public static class SpriteDataAccessExtensions
	{
		private static void CheckAttributeTypeMatchesAndThrow<T>(VertexAttribute channel)
		{
			bool flag = false;
			switch (channel)
			{
			case VertexAttribute.Position:
			case VertexAttribute.Normal:
				flag = (object)typeof(T) == typeof(Vector3);
				break;
			case VertexAttribute.Tangent:
				flag = (object)typeof(T) == typeof(Vector4);
				break;
			case VertexAttribute.Color:
				flag = (object)typeof(T) == typeof(Color32);
				break;
			case VertexAttribute.TexCoord0:
			case VertexAttribute.TexCoord1:
			case VertexAttribute.TexCoord2:
			case VertexAttribute.TexCoord3:
			case VertexAttribute.TexCoord4:
			case VertexAttribute.TexCoord5:
			case VertexAttribute.TexCoord6:
				flag = (object)typeof(T) == typeof(Vector2);
				break;
			default:
				throw new InvalidOperationException($"The requested channel '{channel}' is unknown.");
			}
			if (!flag)
			{
				throw new InvalidOperationException($"The requested channel '{channel}' does not match the return type {typeof(T).Name}.");
			}
		}

		public unsafe static NativeSlice<T> GetVertexAttribute<T>(this Sprite sprite, VertexAttribute channel) where T : struct
		{
			CheckAttributeTypeMatchesAndThrow<T>(channel);
			SpriteChannelInfo channelInfo = GetChannelInfo(sprite, channel);
			byte* dataPointer = (byte*)channelInfo.buffer + channelInfo.offset;
			return NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<T>(dataPointer, channelInfo.stride, channelInfo.count);
		}

		public unsafe static void SetVertexAttribute<T>(this Sprite sprite, VertexAttribute channel, NativeArray<T> src) where T : struct
		{
			CheckAttributeTypeMatchesAndThrow<T>(channel);
			SetChannelData(sprite, channel, src.GetUnsafeReadOnlyPtr());
		}

		public unsafe static NativeArray<Matrix4x4> GetBindPoses(this Sprite sprite)
		{
			SpriteChannelInfo bindPoseInfo = GetBindPoseInfo(sprite);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>(bindPoseInfo.buffer, bindPoseInfo.count, Allocator.Invalid);
		}

		public unsafe static void SetBindPoses(this Sprite sprite, NativeArray<Matrix4x4> src)
		{
			SetBindPoseData(sprite, src.GetUnsafeReadOnlyPtr(), src.Length);
		}

		public unsafe static NativeArray<ushort> GetIndices(this Sprite sprite)
		{
			SpriteChannelInfo indicesInfo = GetIndicesInfo(sprite);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<ushort>(indicesInfo.buffer, indicesInfo.count, Allocator.Invalid);
		}

		public unsafe static void SetIndices(this Sprite sprite, NativeArray<ushort> src)
		{
			SetIndicesData(sprite, src.GetUnsafeReadOnlyPtr(), src.Length);
		}

		public unsafe static NativeArray<BoneWeight> GetBoneWeights(this Sprite sprite)
		{
			SpriteChannelInfo boneWeightsInfo = GetBoneWeightsInfo(sprite);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<BoneWeight>(boneWeightsInfo.buffer, boneWeightsInfo.count, Allocator.Invalid);
		}

		public unsafe static void SetBoneWeights(this Sprite sprite, NativeArray<BoneWeight> src)
		{
			SetBoneWeightsData(sprite, src.GetUnsafeReadOnlyPtr(), src.Length);
		}

		public static SpriteBone[] GetBones(this Sprite sprite)
		{
			return GetBoneInfo(sprite);
		}

		public static void SetBones(this Sprite sprite, SpriteBone[] src)
		{
			SetBoneData(sprite, src);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("HasChannel")]
		public static extern bool HasVertexAttribute(this Sprite sprite, VertexAttribute channel);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetVertexCount(this Sprite sprite, int count);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetVertexCount(this Sprite sprite);

		private static SpriteChannelInfo GetBindPoseInfo(Sprite sprite)
		{
			GetBindPoseInfo_Injected(sprite, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetBindPoseData(Sprite sprite, void* src, int count);

		private static SpriteChannelInfo GetIndicesInfo(Sprite sprite)
		{
			GetIndicesInfo_Injected(sprite, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetIndicesData(Sprite sprite, void* src, int count);

		private static SpriteChannelInfo GetChannelInfo(Sprite sprite, VertexAttribute channel)
		{
			GetChannelInfo_Injected(sprite, channel, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetChannelData(Sprite sprite, VertexAttribute channel, void* src);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SpriteBone[] GetBoneInfo(Sprite sprite);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoneData(Sprite sprite, SpriteBone[] src);

		private static SpriteChannelInfo GetBoneWeightsInfo(Sprite sprite)
		{
			GetBoneWeightsInfo_Injected(sprite, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetBoneWeightsData(Sprite sprite, void* src, int count);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBindPoseInfo_Injected(Sprite sprite, out SpriteChannelInfo ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetIndicesInfo_Injected(Sprite sprite, out SpriteChannelInfo ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetChannelInfo_Injected(Sprite sprite, VertexAttribute channel, out SpriteChannelInfo ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBoneWeightsInfo_Injected(Sprite sprite, out SpriteChannelInfo ret);
	}
}
