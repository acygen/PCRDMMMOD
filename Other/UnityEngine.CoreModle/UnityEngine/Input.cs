using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Input/InputBindings.h")]
	public class Input
	{
		private static LocationService locationServiceInstance;

		private static Compass compassInstance;

		private static Gyroscope s_MainGyro;

		public static extern bool simulateMouseWithTouches
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool anyKey
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool anyKeyDown
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string inputString
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static Vector3 mousePosition
		{
			get
			{
				get_mousePosition_Injected(out var ret);
				return ret;
			}
		}

		public static Vector2 mouseScrollDelta
		{
			get
			{
				get_mouseScrollDelta_Injected(out var ret);
				return ret;
			}
		}

		public static extern IMECompositionMode imeCompositionMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern string compositionString
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool imeIsSelected
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static Vector2 compositionCursorPos
		{
			get
			{
				get_compositionCursorPos_Injected(out var ret);
				return ret;
			}
			set
			{
				set_compositionCursorPos_Injected(ref value);
			}
		}

		[Obsolete("eatKeyPressOnTextFieldFocus property is deprecated, and only provided to support legacy behavior.")]
		public static extern bool eatKeyPressOnTextFieldFocus
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool mousePresent
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetMousePresent")]
			get;
		}

		public static extern int touchCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetTouchCount")]
			get;
		}

		public static extern bool touchPressureSupported
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("IsTouchPressureSupported")]
			get;
		}

		public static extern bool stylusTouchSupported
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("IsStylusTouchSupported")]
			get;
		}

		public static extern bool touchSupported
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("IsTouchSupported")]
			get;
		}

		public static extern bool multiTouchEnabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("IsMultiTouchEnabled")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("SetMultiTouchEnabled")]
			set;
		}

		[Obsolete("isGyroAvailable property is deprecated. Please use SystemInfo.supportsGyroscope instead.")]
		public static extern bool isGyroAvailable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("IsGyroAvailable")]
			get;
		}

		public static extern DeviceOrientation deviceOrientation
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetOrientation")]
			get;
		}

		public static Vector3 acceleration
		{
			[FreeFunction("GetAcceleration")]
			get
			{
				get_acceleration_Injected(out var ret);
				return ret;
			}
		}

		public static extern bool compensateSensors
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("IsCompensatingSensors")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("SetCompensatingSensors")]
			set;
		}

		public static extern int accelerationEventCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetAccelerationCount")]
			get;
		}

		public static extern bool backButtonLeavesApp
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetBackButtonLeavesApp")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("SetBackButtonLeavesApp")]
			set;
		}

		public static LocationService location
		{
			get
			{
				if (locationServiceInstance == null)
				{
					locationServiceInstance = new LocationService();
				}
				return locationServiceInstance;
			}
		}

		public static Compass compass
		{
			get
			{
				if (compassInstance == null)
				{
					compassInstance = new Compass();
				}
				return compassInstance;
			}
		}

		public static Gyroscope gyro
		{
			get
			{
				if (s_MainGyro == null)
				{
					s_MainGyro = new Gyroscope(GetGyroInternal());
				}
				return s_MainGyro;
			}
		}

		public static Touch[] touches
		{
			get
			{
				int num = touchCount;
				Touch[] array = new Touch[num];
				for (int i = 0; i < num; i++)
				{
					ref Touch reference = ref array[i];
					reference = GetTouch(i);
				}
				return array;
			}
		}

		public static AccelerationEvent[] accelerationEvents
		{
			get
			{
				int num = accelerationEventCount;
				AccelerationEvent[] array = new AccelerationEvent[num];
				for (int i = 0; i < num; i++)
				{
					ref AccelerationEvent reference = ref array[i];
					reference = GetAccelerationEvent(i);
				}
				return array;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetKeyInt(KeyCode key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetKeyString(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetKeyUpInt(KeyCode key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetKeyUpString(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetKeyDownInt(KeyCode key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetKeyDownString(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		public static extern float GetAxis(string axisName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		public static extern float GetAxisRaw(string axisName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		public static extern bool GetButton(string buttonName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		public static extern bool GetButtonDown(string buttonName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		public static extern bool GetButtonUp(string buttonName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		public static extern bool GetMouseButton(int button);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		public static extern bool GetMouseButtonDown(int button);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		public static extern bool GetMouseButtonUp(int button);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ResetInput")]
		public static extern void ResetInputAxes();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetJoystickNames();

		[NativeThrows]
		public static Touch GetTouch(int index)
		{
			GetTouch_Injected(index, out var ret);
			return ret;
		}

		[NativeThrows]
		public static AccelerationEvent GetAccelerationEvent(int index)
		{
			GetAccelerationEvent_Injected(index, out var ret);
			return ret;
		}

		public static bool GetKey(KeyCode key)
		{
			return GetKeyInt(key);
		}

		public static bool GetKey(string name)
		{
			return GetKeyString(name);
		}

		public static bool GetKeyUp(KeyCode key)
		{
			return GetKeyUpInt(key);
		}

		public static bool GetKeyUp(string name)
		{
			return GetKeyUpString(name);
		}

		public static bool GetKeyDown(KeyCode key)
		{
			return GetKeyDownInt(key);
		}

		public static bool GetKeyDown(string name)
		{
			return GetKeyDownString(name);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetGyro")]
		private static extern int GetGyroInternal();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTouch_Injected(int index, out Touch ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAccelerationEvent_Injected(int index, out AccelerationEvent ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void get_mousePosition_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void get_mouseScrollDelta_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void get_compositionCursorPos_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void set_compositionCursorPos_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void get_acceleration_Injected(out Vector3 ret);
	}
}
