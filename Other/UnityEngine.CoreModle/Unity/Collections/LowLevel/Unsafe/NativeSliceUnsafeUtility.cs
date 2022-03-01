using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	public static class NativeSliceUnsafeUtility
	{
		public unsafe static NativeSlice<T> ConvertExistingDataToNativeSlice<T>(void* dataPointer, int stride, int length) where T : struct
		{
			if (length < 0)
			{
				throw new ArgumentException($"Invalid length of '{length}'. It must be greater than 0.", "length");
			}
			if (stride < 0)
			{
				throw new ArgumentException($"Invalid stride '{stride}'. It must be greater than 0.", "stride");
			}
			NativeSlice<T> result = default(NativeSlice<T>);
			result.m_Stride = stride;
			result.m_Buffer = (byte*)dataPointer;
			result.m_Length = length;
			return result;
		}

		public unsafe static void* GetUnsafePtr<T>(this NativeSlice<T> nativeSlice) where T : struct
		{
			return nativeSlice.m_Buffer;
		}

		public unsafe static void* GetUnsafeReadOnlyPtr<T>(this NativeSlice<T> nativeSlice) where T : struct
		{
			return nativeSlice.m_Buffer;
		}
	}
}
