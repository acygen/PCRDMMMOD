using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Input/LocationService.h")]
	[NativeHeader("Runtime/Input/InputBindings.h")]
	public class LocationService
	{
		internal struct HeadingInfo
		{
			public float magneticHeading;

			public float trueHeading;

			public float headingAccuracy;

			public Vector3 raw;

			public double timestamp;
		}

		public bool isEnabledByUser => IsServiceEnabledByUser();

		public LocationServiceStatus status => GetLocationStatus();

		public LocationInfo lastData
		{
			get
			{
				if (status != LocationServiceStatus.Running)
				{
					Debug.Log("Location service updates are not enabled. Check LocationService.status before querying last location.");
				}
				return GetLastLocation();
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LocationService::IsServiceEnabledByUser")]
		internal static extern bool IsServiceEnabledByUser();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LocationService::GetLocationStatus")]
		internal static extern LocationServiceStatus GetLocationStatus();

		[FreeFunction("LocationService::GetLastLocation")]
		internal static LocationInfo GetLastLocation()
		{
			GetLastLocation_Injected(out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LocationService::SetDesiredAccuracy")]
		internal static extern void SetDesiredAccuracy(float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LocationService::SetDistanceFilter")]
		internal static extern void SetDistanceFilter(float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LocationService::StartUpdatingLocation")]
		internal static extern void StartUpdatingLocation();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LocationService::StopUpdatingLocation")]
		internal static extern void StopUpdatingLocation();

		[FreeFunction("LocationService::GetLastHeading")]
		internal static HeadingInfo GetLastHeading()
		{
			GetLastHeading_Injected(out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LocationService::IsHeadingUpdatesEnabled")]
		internal static extern bool IsHeadingUpdatesEnabled();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LocationService::SetHeadingUpdatesEnabled")]
		internal static extern void SetHeadingUpdatesEnabled(bool value);

		public void Start(float desiredAccuracyInMeters, float updateDistanceInMeters)
		{
			SetDesiredAccuracy(desiredAccuracyInMeters);
			SetDistanceFilter(updateDistanceInMeters);
			StartUpdatingLocation();
		}

		public void Start(float desiredAccuracyInMeters)
		{
			Start(desiredAccuracyInMeters, 10f);
		}

		public void Start()
		{
			Start(10f, 10f);
		}

		public void Stop()
		{
			StopUpdatingLocation();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLastLocation_Injected(out LocationInfo ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLastHeading_Injected(out HeadingInfo ret);
	}
}
