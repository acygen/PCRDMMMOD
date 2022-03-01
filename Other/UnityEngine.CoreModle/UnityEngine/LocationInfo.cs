namespace UnityEngine
{
	public struct LocationInfo
	{
		internal double m_Timestamp;

		internal float m_Latitude;

		internal float m_Longitude;

		internal float m_Altitude;

		internal float m_HorizontalAccuracy;

		internal float m_VerticalAccuracy;

		public float latitude => m_Latitude;

		public float longitude => m_Longitude;

		public float altitude => m_Altitude;

		public float horizontalAccuracy => m_HorizontalAccuracy;

		public float verticalAccuracy => m_VerticalAccuracy;

		public double timestamp => m_Timestamp;
	}
}
