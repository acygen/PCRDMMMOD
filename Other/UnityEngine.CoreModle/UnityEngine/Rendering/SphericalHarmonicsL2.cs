using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[NativeHeader("Runtime/Export/SphericalHarmonicsL2.bindings.h")]
	[UsedByNativeCode]
	public struct SphericalHarmonicsL2 : IEquatable<SphericalHarmonicsL2>
	{
		private float shr0;

		private float shr1;

		private float shr2;

		private float shr3;

		private float shr4;

		private float shr5;

		private float shr6;

		private float shr7;

		private float shr8;

		private float shg0;

		private float shg1;

		private float shg2;

		private float shg3;

		private float shg4;

		private float shg5;

		private float shg6;

		private float shg7;

		private float shg8;

		private float shb0;

		private float shb1;

		private float shb2;

		private float shb3;

		private float shb4;

		private float shb5;

		private float shb6;

		private float shb7;

		private float shb8;

		public float this[int rgb, int coefficient]
		{
			get
			{
				return (rgb * 9 + coefficient) switch
				{
					0 => shr0, 
					1 => shr1, 
					2 => shr2, 
					3 => shr3, 
					4 => shr4, 
					5 => shr5, 
					6 => shr6, 
					7 => shr7, 
					8 => shr8, 
					9 => shg0, 
					10 => shg1, 
					11 => shg2, 
					12 => shg3, 
					13 => shg4, 
					14 => shg5, 
					15 => shg6, 
					16 => shg7, 
					17 => shg8, 
					18 => shb0, 
					19 => shb1, 
					20 => shb2, 
					21 => shb3, 
					22 => shb4, 
					23 => shb5, 
					24 => shb6, 
					25 => shb7, 
					26 => shb8, 
					_ => throw new IndexOutOfRangeException("Invalid index!"), 
				};
			}
			set
			{
				switch (rgb * 9 + coefficient)
				{
				case 0:
					shr0 = value;
					break;
				case 1:
					shr1 = value;
					break;
				case 2:
					shr2 = value;
					break;
				case 3:
					shr3 = value;
					break;
				case 4:
					shr4 = value;
					break;
				case 5:
					shr5 = value;
					break;
				case 6:
					shr6 = value;
					break;
				case 7:
					shr7 = value;
					break;
				case 8:
					shr8 = value;
					break;
				case 9:
					shg0 = value;
					break;
				case 10:
					shg1 = value;
					break;
				case 11:
					shg2 = value;
					break;
				case 12:
					shg3 = value;
					break;
				case 13:
					shg4 = value;
					break;
				case 14:
					shg5 = value;
					break;
				case 15:
					shg6 = value;
					break;
				case 16:
					shg7 = value;
					break;
				case 17:
					shg8 = value;
					break;
				case 18:
					shb0 = value;
					break;
				case 19:
					shb1 = value;
					break;
				case 20:
					shb2 = value;
					break;
				case 21:
					shb3 = value;
					break;
				case 22:
					shb4 = value;
					break;
				case 23:
					shb5 = value;
					break;
				case 24:
					shb6 = value;
					break;
				case 25:
					shb7 = value;
					break;
				case 26:
					shb8 = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid index!");
				}
			}
		}

		public void Clear()
		{
			SetZero();
		}

		private void SetZero()
		{
			SetZero_Injected(ref this);
		}

		public void AddAmbientLight(Color color)
		{
			AddAmbientLight_Injected(ref this, ref color);
		}

		public void AddDirectionalLight(Vector3 direction, Color color, float intensity)
		{
			Color color2 = color * (2f * intensity);
			AddDirectionalLightInternal(ref this, direction, color2);
		}

		[FreeFunction]
		private static void AddDirectionalLightInternal(ref SphericalHarmonicsL2 sh, Vector3 direction, Color color)
		{
			AddDirectionalLightInternal_Injected(ref sh, ref direction, ref color);
		}

		public void Evaluate(Vector3[] directions, Color[] results)
		{
			if (directions == null)
			{
				throw new ArgumentNullException("directions");
			}
			if (results == null)
			{
				throw new ArgumentNullException("results");
			}
			if (directions.Length != 0)
			{
				if (directions.Length != results.Length)
				{
					throw new ArgumentException("Length of the directions array and the results array must match.");
				}
				EvaluateInternal(ref this, directions, results);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		private static extern void EvaluateInternal(ref SphericalHarmonicsL2 sh, Vector3[] directions, [Out] Color[] results);

		public override int GetHashCode()
		{
			int num = 17;
			num = num * 23 + shr0.GetHashCode();
			num = num * 23 + shr1.GetHashCode();
			num = num * 23 + shr2.GetHashCode();
			num = num * 23 + shr3.GetHashCode();
			num = num * 23 + shr4.GetHashCode();
			num = num * 23 + shr5.GetHashCode();
			num = num * 23 + shr6.GetHashCode();
			num = num * 23 + shr7.GetHashCode();
			num = num * 23 + shr8.GetHashCode();
			num = num * 23 + shg0.GetHashCode();
			num = num * 23 + shg1.GetHashCode();
			num = num * 23 + shg2.GetHashCode();
			num = num * 23 + shg3.GetHashCode();
			num = num * 23 + shg4.GetHashCode();
			num = num * 23 + shg5.GetHashCode();
			num = num * 23 + shg6.GetHashCode();
			num = num * 23 + shg7.GetHashCode();
			num = num * 23 + shg8.GetHashCode();
			num = num * 23 + shb0.GetHashCode();
			num = num * 23 + shb1.GetHashCode();
			num = num * 23 + shb2.GetHashCode();
			num = num * 23 + shb3.GetHashCode();
			num = num * 23 + shb4.GetHashCode();
			num = num * 23 + shb5.GetHashCode();
			num = num * 23 + shb6.GetHashCode();
			num = num * 23 + shb7.GetHashCode();
			return num * 23 + shb8.GetHashCode();
		}

		public override bool Equals(object other)
		{
			return other is SphericalHarmonicsL2 && Equals((SphericalHarmonicsL2)other);
		}

		public bool Equals(SphericalHarmonicsL2 other)
		{
			return this == other;
		}

		public static SphericalHarmonicsL2 operator *(SphericalHarmonicsL2 lhs, float rhs)
		{
			SphericalHarmonicsL2 result = default(SphericalHarmonicsL2);
			result.shr0 = lhs.shr0 * rhs;
			result.shr1 = lhs.shr1 * rhs;
			result.shr2 = lhs.shr2 * rhs;
			result.shr3 = lhs.shr3 * rhs;
			result.shr4 = lhs.shr4 * rhs;
			result.shr5 = lhs.shr5 * rhs;
			result.shr6 = lhs.shr6 * rhs;
			result.shr7 = lhs.shr7 * rhs;
			result.shr8 = lhs.shr8 * rhs;
			result.shg0 = lhs.shg0 * rhs;
			result.shg1 = lhs.shg1 * rhs;
			result.shg2 = lhs.shg2 * rhs;
			result.shg3 = lhs.shg3 * rhs;
			result.shg4 = lhs.shg4 * rhs;
			result.shg5 = lhs.shg5 * rhs;
			result.shg6 = lhs.shg6 * rhs;
			result.shg7 = lhs.shg7 * rhs;
			result.shg8 = lhs.shg8 * rhs;
			result.shb0 = lhs.shb0 * rhs;
			result.shb1 = lhs.shb1 * rhs;
			result.shb2 = lhs.shb2 * rhs;
			result.shb3 = lhs.shb3 * rhs;
			result.shb4 = lhs.shb4 * rhs;
			result.shb5 = lhs.shb5 * rhs;
			result.shb6 = lhs.shb6 * rhs;
			result.shb7 = lhs.shb7 * rhs;
			result.shb8 = lhs.shb8 * rhs;
			return result;
		}

		public static SphericalHarmonicsL2 operator *(float lhs, SphericalHarmonicsL2 rhs)
		{
			SphericalHarmonicsL2 result = default(SphericalHarmonicsL2);
			result.shr0 = rhs.shr0 * lhs;
			result.shr1 = rhs.shr1 * lhs;
			result.shr2 = rhs.shr2 * lhs;
			result.shr3 = rhs.shr3 * lhs;
			result.shr4 = rhs.shr4 * lhs;
			result.shr5 = rhs.shr5 * lhs;
			result.shr6 = rhs.shr6 * lhs;
			result.shr7 = rhs.shr7 * lhs;
			result.shr8 = rhs.shr8 * lhs;
			result.shg0 = rhs.shg0 * lhs;
			result.shg1 = rhs.shg1 * lhs;
			result.shg2 = rhs.shg2 * lhs;
			result.shg3 = rhs.shg3 * lhs;
			result.shg4 = rhs.shg4 * lhs;
			result.shg5 = rhs.shg5 * lhs;
			result.shg6 = rhs.shg6 * lhs;
			result.shg7 = rhs.shg7 * lhs;
			result.shg8 = rhs.shg8 * lhs;
			result.shb0 = rhs.shb0 * lhs;
			result.shb1 = rhs.shb1 * lhs;
			result.shb2 = rhs.shb2 * lhs;
			result.shb3 = rhs.shb3 * lhs;
			result.shb4 = rhs.shb4 * lhs;
			result.shb5 = rhs.shb5 * lhs;
			result.shb6 = rhs.shb6 * lhs;
			result.shb7 = rhs.shb7 * lhs;
			result.shb8 = rhs.shb8 * lhs;
			return result;
		}

		public static SphericalHarmonicsL2 operator +(SphericalHarmonicsL2 lhs, SphericalHarmonicsL2 rhs)
		{
			SphericalHarmonicsL2 result = default(SphericalHarmonicsL2);
			result.shr0 = lhs.shr0 + rhs.shr0;
			result.shr1 = lhs.shr1 + rhs.shr1;
			result.shr2 = lhs.shr2 + rhs.shr2;
			result.shr3 = lhs.shr3 + rhs.shr3;
			result.shr4 = lhs.shr4 + rhs.shr4;
			result.shr5 = lhs.shr5 + rhs.shr5;
			result.shr6 = lhs.shr6 + rhs.shr6;
			result.shr7 = lhs.shr7 + rhs.shr7;
			result.shr8 = lhs.shr8 + rhs.shr8;
			result.shg0 = lhs.shg0 + rhs.shg0;
			result.shg1 = lhs.shg1 + rhs.shg1;
			result.shg2 = lhs.shg2 + rhs.shg2;
			result.shg3 = lhs.shg3 + rhs.shg3;
			result.shg4 = lhs.shg4 + rhs.shg4;
			result.shg5 = lhs.shg5 + rhs.shg5;
			result.shg6 = lhs.shg6 + rhs.shg6;
			result.shg7 = lhs.shg7 + rhs.shg7;
			result.shg8 = lhs.shg8 + rhs.shg8;
			result.shb0 = lhs.shb0 + rhs.shb0;
			result.shb1 = lhs.shb1 + rhs.shb1;
			result.shb2 = lhs.shb2 + rhs.shb2;
			result.shb3 = lhs.shb3 + rhs.shb3;
			result.shb4 = lhs.shb4 + rhs.shb4;
			result.shb5 = lhs.shb5 + rhs.shb5;
			result.shb6 = lhs.shb6 + rhs.shb6;
			result.shb7 = lhs.shb7 + rhs.shb7;
			result.shb8 = lhs.shb8 + rhs.shb8;
			return result;
		}

		public static bool operator ==(SphericalHarmonicsL2 lhs, SphericalHarmonicsL2 rhs)
		{
			return lhs.shr0 == rhs.shr0 && lhs.shr1 == rhs.shr1 && lhs.shr2 == rhs.shr2 && lhs.shr3 == rhs.shr3 && lhs.shr4 == rhs.shr4 && lhs.shr5 == rhs.shr5 && lhs.shr6 == rhs.shr6 && lhs.shr7 == rhs.shr7 && lhs.shr8 == rhs.shr8 && lhs.shg0 == rhs.shg0 && lhs.shg1 == rhs.shg1 && lhs.shg2 == rhs.shg2 && lhs.shg3 == rhs.shg3 && lhs.shg4 == rhs.shg4 && lhs.shg5 == rhs.shg5 && lhs.shg6 == rhs.shg6 && lhs.shg7 == rhs.shg7 && lhs.shg8 == rhs.shg8 && lhs.shb0 == rhs.shb0 && lhs.shb1 == rhs.shb1 && lhs.shb2 == rhs.shb2 && lhs.shb3 == rhs.shb3 && lhs.shb4 == rhs.shb4 && lhs.shb5 == rhs.shb5 && lhs.shb6 == rhs.shb6 && lhs.shb7 == rhs.shb7 && lhs.shb8 == rhs.shb8;
		}

		public static bool operator !=(SphericalHarmonicsL2 lhs, SphericalHarmonicsL2 rhs)
		{
			return !(lhs == rhs);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetZero_Injected(ref SphericalHarmonicsL2 _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddAmbientLight_Injected(ref SphericalHarmonicsL2 _unity_self, ref Color color);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddDirectionalLightInternal_Injected(ref SphericalHarmonicsL2 sh, ref Vector3 direction, ref Color color);
	}
}
