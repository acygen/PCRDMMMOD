namespace UnityEngine
{
	public struct RangeInt
	{
		public int start;

		public int length;

		public int end => start + length;

		public RangeInt(int start, int length)
		{
			this.start = start;
			this.length = length;
		}
	}
}
