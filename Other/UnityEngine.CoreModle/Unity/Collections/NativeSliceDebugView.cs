namespace Unity.Collections
{
	internal sealed class NativeSliceDebugView<T> where T : struct
	{
		private NativeSlice<T> m_Array;

		public T[] Items => m_Array.ToArray();

		public NativeSliceDebugView(NativeSlice<T> array)
		{
			m_Array = array;
		}
	}
}
