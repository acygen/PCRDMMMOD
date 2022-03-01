using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Math/Vector3.h")]
	[ThreadAndSerializationSafe]
	[NativeHeader("Runtime/Math/MathScripting.h")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeType(Header = "Runtime/Math/Vector3.h")]
	[NativeClass("Vector3f")]
	public struct Vector3 : IEquatable<Vector3>
	{
		public const float kEpsilon = 1E-05f;

		public const float kEpsilonNormalSqrt = 1E-15f;

		public float x;

		public float y;

		public float z;

		private static readonly Vector3 zeroVector = new Vector3(0f, 0f, 0f);

		private static readonly Vector3 oneVector = new Vector3(1f, 1f, 1f);

		private static readonly Vector3 upVector = new Vector3(0f, 1f, 0f);

		private static readonly Vector3 downVector = new Vector3(0f, -1f, 0f);

		private static readonly Vector3 leftVector = new Vector3(-1f, 0f, 0f);

		private static readonly Vector3 rightVector = new Vector3(1f, 0f, 0f);

		private static readonly Vector3 forwardVector = new Vector3(0f, 0f, 1f);

		private static readonly Vector3 backVector = new Vector3(0f, 0f, -1f);

		private static readonly Vector3 positiveInfinityVector = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

		private static readonly Vector3 negativeInfinityVector = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

		public float this[int index]
		{
			get
			{
				return index switch
				{
					0 => x, 
					1 => y, 
					2 => z, 
					_ => throw new IndexOutOfRangeException("Invalid Vector3 index!"), 
				};
			}
			set
			{
				switch (index)
				{
				case 0:
					x = value;
					break;
				case 1:
					y = value;
					break;
				case 2:
					z = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
		}

		public Vector3 normalized => Normalize(this);

		public float magnitude => Mathf.Sqrt(x * x + y * y + z * z);

		public float sqrMagnitude => x * x + y * y + z * z;

		public static Vector3 zero => zeroVector;

		public static Vector3 one => oneVector;

		public static Vector3 forward => forwardVector;

		public static Vector3 back => backVector;

		public static Vector3 up => upVector;

		public static Vector3 down => downVector;

		public static Vector3 left => leftVector;

		public static Vector3 right => rightVector;

		public static Vector3 positiveInfinity => positiveInfinityVector;

		public static Vector3 negativeInfinity => negativeInfinityVector;

		[Obsolete("Use Vector3.forward instead.")]
		public static Vector3 fwd => new Vector3(0f, 0f, 1f);

		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vector3(float x, float y)
		{
			this.x = x;
			this.y = y;
			z = 0f;
		}

		[FreeFunction("VectorScripting::Slerp", IsThreadSafe = true)]
		public static Vector3 Slerp(Vector3 a, Vector3 b, float t)
		{
			Slerp_Injected(ref a, ref b, t, out var ret);
			return ret;
		}

		[FreeFunction("VectorScripting::SlerpUnclamped", IsThreadSafe = true)]
		public static Vector3 SlerpUnclamped(Vector3 a, Vector3 b, float t)
		{
			SlerpUnclamped_Injected(ref a, ref b, t, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("VectorScripting::OrthoNormalize", IsThreadSafe = true)]
		private static extern void OrthoNormalize2(ref Vector3 a, ref Vector3 b);

		public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent)
		{
			OrthoNormalize2(ref normal, ref tangent);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("VectorScripting::OrthoNormalize", IsThreadSafe = true)]
		private static extern void OrthoNormalize3(ref Vector3 a, ref Vector3 b, ref Vector3 c);

		public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent, ref Vector3 binormal)
		{
			OrthoNormalize3(ref normal, ref tangent, ref binormal);
		}

		[FreeFunction(IsThreadSafe = true)]
		public static Vector3 RotateTowards(Vector3 current, Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta)
		{
			RotateTowards_Injected(ref current, ref target, maxRadiansDelta, maxMagnitudeDelta, out var ret);
			return ret;
		}

		public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
		{
			return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
		{
			Vector3 vector = target - current;
			float num = vector.magnitude;
			if (num <= maxDistanceDelta || num < float.Epsilon)
			{
				return target;
			}
			return current + vector / num * maxDistanceDelta;
		}

		[ExcludeFromDocs]
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		[ExcludeFromDocs]
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float maxSpeed = float.PositiveInfinity;
			return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			Vector3 vector = current - target;
			Vector3 vector2 = target;
			float maxLength = maxSpeed * smoothTime;
			vector = ClampMagnitude(vector, maxLength);
			target = current - vector;
			Vector3 vector3 = (currentVelocity + num * vector) * deltaTime;
			currentVelocity = (currentVelocity - num * vector3) * num3;
			Vector3 vector4 = target + (vector + vector3) * num3;
			if (Dot(vector2 - current, vector4 - vector2) > 0f)
			{
				vector4 = vector2;
				currentVelocity = (vector4 - vector2) / deltaTime;
			}
			return vector4;
		}

		public void Set(float newX, float newY, float newZ)
		{
			x = newX;
			y = newY;
			z = newZ;
		}

		public static Vector3 Scale(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		public void Scale(Vector3 scale)
		{
			x *= scale.x;
			y *= scale.y;
			z *= scale.z;
		}

		public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
		}

		public override bool Equals(object other)
		{
			if (!(other is Vector3))
			{
				return false;
			}
			return Equals((Vector3)other);
		}

		public bool Equals(Vector3 other)
		{
			return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z);
		}

		public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
		{
			return -2f * Dot(inNormal, inDirection) * inNormal + inDirection;
		}

		public static Vector3 Normalize(Vector3 value)
		{
			float num = Magnitude(value);
			if (num > 1E-05f)
			{
				return value / num;
			}
			return zero;
		}

		public void Normalize()
		{
			float num = Magnitude(this);
			if (num > 1E-05f)
			{
				this /= num;
			}
			else
			{
				this = zero;
			}
		}

		public static float Dot(Vector3 lhs, Vector3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		public static Vector3 Project(Vector3 vector, Vector3 onNormal)
		{
			float num = Dot(onNormal, onNormal);
			if (num < Mathf.Epsilon)
			{
				return zero;
			}
			return onNormal * Dot(vector, onNormal) / num;
		}

		public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
		{
			return vector - Project(vector, planeNormal);
		}

		public static float Angle(Vector3 from, Vector3 to)
		{
			float num = Mathf.Sqrt(from.sqrMagnitude * to.sqrMagnitude);
			if (num < 1E-15f)
			{
				return 0f;
			}
			float f = Mathf.Clamp(Dot(from, to) / num, -1f, 1f);
			return Mathf.Acos(f) * 57.29578f;
		}

		public static float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
		{
			float num = Angle(from, to);
			float num2 = Mathf.Sign(Dot(axis, Cross(from, to)));
			return num * num2;
		}

		public static float Distance(Vector3 a, Vector3 b)
		{
			Vector3 vector = new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
			return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
		}

		public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
		{
			if (vector.sqrMagnitude > maxLength * maxLength)
			{
				return vector.normalized * maxLength;
			}
			return vector;
		}

		public static float Magnitude(Vector3 vector)
		{
			return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
		}

		public static float SqrMagnitude(Vector3 vector)
		{
			return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
		}

		public static Vector3 Min(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		}

		public static Vector3 Max(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		}

		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(0f - a.x, 0f - a.y, 0f - a.z);
		}

		public static Vector3 operator *(Vector3 a, float d)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		public static Vector3 operator *(float d, Vector3 a)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		public static Vector3 operator /(Vector3 a, float d)
		{
			return new Vector3(a.x / d, a.y / d, a.z / d);
		}

		public static bool operator ==(Vector3 lhs, Vector3 rhs)
		{
			return SqrMagnitude(lhs - rhs) < 9.99999944E-11f;
		}

		public static bool operator !=(Vector3 lhs, Vector3 rhs)
		{
			return !(lhs == rhs);
		}

		public override string ToString()
		{
			return UnityString.Format("({0:F1}, {1:F1}, {2:F1})", x, y, z);
		}

		public string ToString(string format)
		{
			return UnityString.Format("({0}, {1}, {2})", x.ToString(format), y.ToString(format), z.ToString(format));
		}

		[Obsolete("Use Vector3.Angle instead. AngleBetween uses radians instead of degrees and was deprecated for this reason")]
		public static float AngleBetween(Vector3 from, Vector3 to)
		{
			return Mathf.Acos(Mathf.Clamp(Dot(from.normalized, to.normalized), -1f, 1f));
		}

		[Obsolete("Use Vector3.ProjectOnPlane instead.")]
		public static Vector3 Exclude(Vector3 excludeThis, Vector3 fromThat)
		{
			return ProjectOnPlane(fromThat, excludeThis);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Slerp_Injected(ref Vector3 a, ref Vector3 b, float t, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SlerpUnclamped_Injected(ref Vector3 a, ref Vector3 b, float t, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RotateTowards_Injected(ref Vector3 current, ref Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta, out Vector3 ret);
	}
}
