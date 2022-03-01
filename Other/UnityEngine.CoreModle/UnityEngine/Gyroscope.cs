using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Input/GetInput.h")]
	public class Gyroscope
	{
		private int m_GyroIndex;

		public Vector3 rotationRate => rotationRate_Internal(m_GyroIndex);

		public Vector3 rotationRateUnbiased => rotationRateUnbiased_Internal(m_GyroIndex);

		public Vector3 gravity => gravity_Internal(m_GyroIndex);

		public Vector3 userAcceleration => userAcceleration_Internal(m_GyroIndex);

		public Quaternion attitude => attitude_Internal(m_GyroIndex);

		public bool enabled
		{
			get
			{
				return getEnabled_Internal(m_GyroIndex);
			}
			set
			{
				setEnabled_Internal(m_GyroIndex, value);
			}
		}

		public float updateInterval
		{
			get
			{
				return getUpdateInterval_Internal(m_GyroIndex);
			}
			set
			{
				setUpdateInterval_Internal(m_GyroIndex, value);
			}
		}

		internal Gyroscope(int index)
		{
			m_GyroIndex = index;
		}

		[FreeFunction("GetGyroRotationRate")]
		private static Vector3 rotationRate_Internal(int idx)
		{
			rotationRate_Internal_Injected(idx, out var ret);
			return ret;
		}

		[FreeFunction("GetGyroRotationRateUnbiased")]
		private static Vector3 rotationRateUnbiased_Internal(int idx)
		{
			rotationRateUnbiased_Internal_Injected(idx, out var ret);
			return ret;
		}

		[FreeFunction("GetGravity")]
		private static Vector3 gravity_Internal(int idx)
		{
			gravity_Internal_Injected(idx, out var ret);
			return ret;
		}

		[FreeFunction("GetUserAcceleration")]
		private static Vector3 userAcceleration_Internal(int idx)
		{
			userAcceleration_Internal_Injected(idx, out var ret);
			return ret;
		}

		[FreeFunction("GetAttitude")]
		private static Quaternion attitude_Internal(int idx)
		{
			attitude_Internal_Injected(idx, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("IsGyroEnabled")]
		private static extern bool getEnabled_Internal(int idx);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("SetGyroEnabled")]
		private static extern void setEnabled_Internal(int idx, bool enabled);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetGyroUpdateInterval")]
		private static extern float getUpdateInterval_Internal(int idx);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("SetGyroUpdateInterval")]
		private static extern void setUpdateInterval_Internal(int idx, float interval);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void rotationRate_Internal_Injected(int idx, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void rotationRateUnbiased_Internal_Injected(int idx, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void gravity_Internal_Injected(int idx, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void userAcceleration_Internal_Injected(int idx, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void attitude_Internal_Injected(int idx, out Quaternion ret);
	}
}
