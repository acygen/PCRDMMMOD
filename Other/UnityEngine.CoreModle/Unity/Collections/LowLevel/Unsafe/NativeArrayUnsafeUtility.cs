namespace Unity.Collections.LowLevel.Unsafe
{
	public static class NativeArrayUnsafeUtility
	{
		public unsafe static NativeArray<T> ConvertExistingDataToNativeArray<T>(void* dataPointer, int length, Allocator allocator) where T : struct
		{
			NativeArray<T> result = default(NativeArray<T>);
			result.m_Buffer = dataPointer;
			result.m_Length = length;
			result.m_AllocatorLabel = allocator;
			return result;
		}

		public unsafe static void* GetUnsafePtr<T>(this NativeArray<T> nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}

		public unsafe static void* GetUnsafeReadOnlyPtr<T>(this NativeArray<T> nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}

		public unsafe static void* GetUnsafeBufferPointerWithoutChecks<T>(NativeArray<T> nativeArray) where T : struct
		{
			return nativeArray.m_Buffer;
		}
	}
}
