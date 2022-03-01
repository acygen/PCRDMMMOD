namespace Unity.Collections
{
	public static class NativeLeakDetection
	{
		private static int s_NativeLeakDetectionMode;

		public static NativeLeakDetectionMode Mode
		{
			get
			{
				return (NativeLeakDetectionMode)s_NativeLeakDetectionMode;
			}
			set
			{
				s_NativeLeakDetectionMode = (int)value;
			}
		}
	}
}
