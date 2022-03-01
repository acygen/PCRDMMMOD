using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine
{
	public static class HashUtilities
	{
		public unsafe static void AppendHash(ref Hash128 inHash, ref Hash128 outHash)
		{
			fixed (Hash128* hash = &outHash)
			{
				fixed (Hash128* data = &inHash)
				{
					HashUnsafeUtilities.ComputeHash128(data, (ulong)sizeof(Hash128), hash);
				}
			}
		}

		public unsafe static void QuantisedMatrixHash(ref Matrix4x4 value, ref Hash128 hash)
		{
			fixed (Hash128* hash2 = &hash)
			{
				int* ptr = stackalloc int[16];
				for (int i = 0; i < 16; i++)
				{
					ptr[i] = (int)(value[i] * 1000f + 0.5f);
				}
				HashUnsafeUtilities.ComputeHash128(ptr, 64uL, hash2);
			}
		}

		public unsafe static void QuantisedVectorHash(ref Vector3 value, ref Hash128 hash)
		{
			fixed (Hash128* hash2 = &hash)
			{
				int* ptr = stackalloc int[3];
				for (int i = 0; i < 3; i++)
				{
					ptr[i] = (int)(value[i] * 1000f + 0.5f);
				}
				HashUnsafeUtilities.ComputeHash128(ptr, 12uL, hash2);
			}
		}

		public unsafe static void ComputeHash128<T>(ref T value, ref Hash128 hash) where T : struct
		{
			void* data = UnsafeUtility.AddressOf(ref value);
			ulong dataSize = (ulong)UnsafeUtility.SizeOf<T>();
			Hash128* hash2 = (Hash128*)UnsafeUtility.AddressOf(ref hash);
			HashUnsafeUtilities.ComputeHash128(data, dataSize, hash2);
		}
	}
}
