namespace Unity.IO.LowLevel.Unsafe
{
	public struct ReadCommand
	{
		public unsafe void* Buffer;

		public long Offset;

		public long Size;
	}
}
