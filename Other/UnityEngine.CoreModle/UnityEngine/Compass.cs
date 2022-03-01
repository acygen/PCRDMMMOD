namespace UnityEngine
{
	public class Compass
	{
		public float magneticHeading => LocationService.GetLastHeading().magneticHeading;

		public float trueHeading => LocationService.GetLastHeading().trueHeading;

		public float headingAccuracy => LocationService.GetLastHeading().headingAccuracy;

		public Vector3 rawVector => LocationService.GetLastHeading().raw;

		public double timestamp => LocationService.GetLastHeading().timestamp;

		public bool enabled
		{
			get
			{
				return LocationService.IsHeadingUpdatesEnabled();
			}
			set
			{
				LocationService.SetHeadingUpdatesEnabled(value);
			}
		}
	}
}
