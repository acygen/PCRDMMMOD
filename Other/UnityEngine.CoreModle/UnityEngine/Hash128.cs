using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[Serializable]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Utilities/Hash128.h")]
	public struct Hash128 : IComparable, IComparable<Hash128>, IEquatable<Hash128>
	{
		private uint m_u32_0;

		private uint m_u32_1;

		private uint m_u32_2;

		private uint m_u32_3;

		internal unsafe ulong u64_0
		{
			get
			{
				fixed (uint* ptr = &m_u32_0)
				{
					return *(ulong*)ptr;
				}
			}
		}

		internal unsafe ulong u64_1
		{
			get
			{
				fixed (uint* ptr = &m_u32_1)
				{
					return *(ulong*)ptr;
				}
			}
		}

		public bool isValid => m_u32_0 != 0 || m_u32_1 != 0 || m_u32_2 != 0 || m_u32_3 != 0;

		public Hash128(uint u32_0, uint u32_1, uint u32_2, uint u32_3)
		{
			m_u32_0 = u32_0;
			m_u32_1 = u32_1;
			m_u32_2 = u32_2;
			m_u32_3 = u32_3;
		}

		public unsafe Hash128(ulong u64_0, ulong u64_1)
		{
			uint* ptr = (uint*)(&u64_0);
			uint* ptr2 = (uint*)(&u64_1);
			m_u32_0 = *ptr;
			m_u32_1 = ptr[1];
			m_u32_2 = *ptr2;
			m_u32_3 = ptr2[1];
		}

		public int CompareTo(Hash128 rhs)
		{
			if (this < rhs)
			{
				return -1;
			}
			if (this > rhs)
			{
				return 1;
			}
			return 0;
		}

		public override string ToString()
		{
			return Internal_Hash128ToString(this);
		}

		[FreeFunction("StringToHash128")]
		public static Hash128 Parse(string hashString)
		{
			Parse_Injected(hashString, out var ret);
			return ret;
		}

		[FreeFunction("Hash128ToString")]
		internal static string Internal_Hash128ToString(Hash128 hash128)
		{
			return Internal_Hash128ToString_Injected(ref hash128);
		}

		[FreeFunction("ComputeHash128FromString")]
		public static Hash128 Compute(string hashString)
		{
			Compute_Injected(hashString, out var ret);
			return ret;
		}

		public override bool Equals(object obj)
		{
			return obj is Hash128 && this == (Hash128)obj;
		}

		public bool Equals(Hash128 obj)
		{
			return this == obj;
		}

		public override int GetHashCode()
		{
			return m_u32_0.GetHashCode() ^ m_u32_1.GetHashCode() ^ m_u32_2.GetHashCode() ^ m_u32_3.GetHashCode();
		}

		public int CompareTo(object obj)
		{
			if (obj == null || !(obj is Hash128))
			{
				return 1;
			}
			Hash128 rhs = (Hash128)obj;
			return CompareTo(rhs);
		}

		public static bool operator ==(Hash128 hash1, Hash128 hash2)
		{
			return hash1.m_u32_0 == hash2.m_u32_0 && hash1.m_u32_1 == hash2.m_u32_1 && hash1.m_u32_2 == hash2.m_u32_2 && hash1.m_u32_3 == hash2.m_u32_3;
		}

		public static bool operator !=(Hash128 hash1, Hash128 hash2)
		{
			return !(hash1 == hash2);
		}

		public static bool operator <(Hash128 x, Hash128 y)
		{
			if (x.m_u32_0 != y.m_u32_0)
			{
				return x.m_u32_0 < y.m_u32_0;
			}
			if (x.m_u32_1 != y.m_u32_1)
			{
				return x.m_u32_1 < y.m_u32_1;
			}
			if (x.m_u32_2 != y.m_u32_2)
			{
				return x.m_u32_2 < y.m_u32_2;
			}
			return x.m_u32_3 < y.m_u32_3;
		}

		public static bool operator >(Hash128 x, Hash128 y)
		{
			if (x < y)
			{
				return false;
			}
			if (x == y)
			{
				return false;
			}
			return true;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Parse_Injected(string hashString, out Hash128 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_Hash128ToString_Injected(ref Hash128 hash128);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Compute_Injected(string hashString, out Hash128 ret);
	}
}
