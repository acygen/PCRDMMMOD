namespace UnityEngine
{
	public struct CullingGroupEvent
	{
		private int m_Index;

		private byte m_PrevState;

		private byte m_ThisState;

		private const byte kIsVisibleMask = 128;

		private const byte kDistanceMask = 127;

		public int index => m_Index;

		public bool isVisible => (m_ThisState & 0x80) != 0;

		public bool wasVisible => (m_PrevState & 0x80) != 0;

		public bool hasBecomeVisible => isVisible && !wasVisible;

		public bool hasBecomeInvisible => !isVisible && wasVisible;

		public int currentDistance => m_ThisState & 0x7F;

		public int previousDistance => m_PrevState & 0x7F;
	}
}
