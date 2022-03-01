using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine
{
	internal static class SpookyHash
	{
		[StructLayout(LayoutKind.Explicit)]
		private struct U
		{
			[FieldOffset(0)]
			public unsafe ushort* p8;

			[FieldOffset(0)]
			public unsafe uint* p32;

			[FieldOffset(0)]
			public unsafe ulong* p64;

			[FieldOffset(0)]
			public ulong i;

			public unsafe U(ushort* p8)
			{
				p32 = null;
				p64 = null;
				i = 0uL;
				this.p8 = p8;
			}
		}

		private static readonly bool AllowUnalignedRead = AttemptDetectAllowUnalignedRead();

		private const int k_NumVars = 12;

		private const int k_BlockSize = 96;

		private const int k_BufferSize = 192;

		private const ulong k_DeadBeefConst = 16045690984833335023uL;

		private static bool AttemptDetectAllowUnalignedRead()
		{
			switch (SystemInfo.processorType)
			{
			case "x86":
			case "AMD64":
				return true;
			default:
				return false;
			}
		}

		public unsafe static void Hash(void* message, ulong length, ulong* hash1, ulong* hash2)
		{
			if (length < 192)
			{
				Short(message, length, hash1, hash2);
				return;
			}
			ulong* ptr = stackalloc ulong[12];
			ulong s3;
			ulong s2;
			ulong s;
			ulong s4 = (s3 = (s2 = (s = *hash1)));
			ulong s7;
			ulong s6;
			ulong s5;
			ulong s8 = (s7 = (s6 = (s5 = *hash2)));
			ulong s11;
			ulong s10;
			ulong s9;
			ulong s12 = (s11 = (s10 = (s9 = 16045690984833335023uL)));
			U u = new U((ushort*)message);
			ulong* ptr2 = u.p64 + length / 96uL * 12;
			if (AllowUnalignedRead || (u.i & 7) == 0)
			{
				while (u.p64 < ptr2)
				{
					Mix(u.p64, ref s4, ref s8, ref s12, ref s3, ref s7, ref s11, ref s2, ref s6, ref s10, ref s, ref s5, ref s9);
					u.p64 += 12;
				}
			}
			else
			{
				while (u.p64 < ptr2)
				{
					UnsafeUtility.MemCpy(ptr, u.p64, 96L);
					Mix(ptr, ref s4, ref s8, ref s12, ref s3, ref s7, ref s11, ref s2, ref s6, ref s10, ref s, ref s5, ref s9);
					u.p64 += 12;
				}
			}
			ulong num = length - (ulong)((nint)((byte*)ptr2 - (nuint)message) / (nint)2);
			UnsafeUtility.MemCpy(ptr, ptr2, (long)num);
			memset((byte*)ptr + num * 2, 0, 96 - num);
			*(ushort*)((byte*)ptr + 190) = (ushort)num;
			End(ptr, ref s4, ref s8, ref s12, ref s3, ref s7, ref s11, ref s2, ref s6, ref s10, ref s, ref s5, ref s9);
			*hash1 = s4;
			*hash2 = s8;
		}

		private unsafe static void End(ulong* data, ref ulong h0, ref ulong h1, ref ulong h2, ref ulong h3, ref ulong h4, ref ulong h5, ref ulong h6, ref ulong h7, ref ulong h8, ref ulong h9, ref ulong h10, ref ulong h11)
		{
			h0 += *data;
			h1 += data[1];
			h2 += data[2];
			h3 += data[3];
			h4 += data[4];
			h5 += data[5];
			h6 += data[6];
			h7 += data[7];
			h8 += data[8];
			h9 += data[9];
			h10 += data[10];
			h11 += data[11];
			EndPartial(ref h0, ref h1, ref h2, ref h3, ref h4, ref h5, ref h6, ref h7, ref h8, ref h9, ref h10, ref h11);
			EndPartial(ref h0, ref h1, ref h2, ref h3, ref h4, ref h5, ref h6, ref h7, ref h8, ref h9, ref h10, ref h11);
			EndPartial(ref h0, ref h1, ref h2, ref h3, ref h4, ref h5, ref h6, ref h7, ref h8, ref h9, ref h10, ref h11);
		}

		private static void EndPartial(ref ulong h0, ref ulong h1, ref ulong h2, ref ulong h3, ref ulong h4, ref ulong h5, ref ulong h6, ref ulong h7, ref ulong h8, ref ulong h9, ref ulong h10, ref ulong h11)
		{
			h11 += h1;
			h2 ^= h11;
			Rot64(ref h1, 44);
			h0 += h2;
			h3 ^= h0;
			Rot64(ref h2, 15);
			h1 += h3;
			h4 ^= h1;
			Rot64(ref h3, 34);
			h2 += h4;
			h5 ^= h2;
			Rot64(ref h4, 21);
			h3 += h5;
			h6 ^= h3;
			Rot64(ref h5, 38);
			h4 += h6;
			h7 ^= h4;
			Rot64(ref h6, 33);
			h5 += h7;
			h8 ^= h5;
			Rot64(ref h7, 10);
			h6 += h8;
			h9 ^= h6;
			Rot64(ref h8, 13);
			h7 += h9;
			h10 ^= h7;
			Rot64(ref h9, 38);
			h8 += h10;
			h11 ^= h8;
			Rot64(ref h10, 53);
			h9 += h11;
			h0 ^= h9;
			Rot64(ref h11, 42);
			h10 += h0;
			h1 ^= h10;
			Rot64(ref h0, 54);
		}

		private static void Rot64(ref ulong x, int k)
		{
			x = (x << k) | (x >> 64 - k);
		}

		private unsafe static void Short(void* message, ulong length, ulong* hash1, ulong* hash2)
		{
			ulong* ptr = stackalloc ulong[24];
			U u = new U((ushort*)message);
			if (!AllowUnalignedRead && (u.i & 7) != 0)
			{
				UnsafeUtility.MemCpy(ptr, message, (long)length);
				u.p64 = ptr;
			}
			ulong num = length % 32uL;
			ulong h = *hash1;
			ulong h2 = *hash2;
			ulong h3 = 16045690984833335023uL;
			ulong h4 = 16045690984833335023uL;
			if (length > 15)
			{
				ulong* ptr2 = u.p64 + length / 32uL * 4;
				while (u.p64 < ptr2)
				{
					h3 += *u.p64;
					h4 += u.p64[1];
					ShortMix(ref h, ref h2, ref h3, ref h4);
					h += u.p64[2];
					h2 += u.p64[3];
					u.p64 += 4;
				}
				if (num >= 16)
				{
					h3 += *u.p64;
					h4 += u.p64[1];
					ShortMix(ref h, ref h2, ref h3, ref h4);
					u.p64 += 2;
					num -= 16;
				}
			}
			h4 += length << 56;
			if ((long)num >= 0L && (long)num <= 15L)
			{
				switch (num)
				{
				case 15uL:
					h4 += (ulong)u.p8[14] << 48;
					goto case 14uL;
				case 14uL:
					h4 += (ulong)u.p8[13] << 40;
					goto case 13uL;
				case 13uL:
					h4 += (ulong)u.p8[12] << 32;
					goto case 12uL;
				case 12uL:
					h4 += u.p32[2];
					h3 += *u.p64;
					break;
				case 11uL:
					h4 += (ulong)u.p8[10] << 16;
					goto case 10uL;
				case 10uL:
					h4 += (ulong)u.p8[9] << 8;
					goto case 9uL;
				case 9uL:
					h4 += u.p8[8];
					goto case 8uL;
				case 8uL:
					h3 += *u.p64;
					break;
				case 7uL:
					h3 += (ulong)u.p8[6] << 48;
					goto case 9uL;
				case 6uL:
					h3 += (ulong)u.p8[5] << 40;
					goto case 5uL;
				case 5uL:
					h3 += (ulong)u.p8[4] << 32;
					goto case 4uL;
				case 4uL:
					h3 += *u.p32;
					break;
				case 3uL:
					h3 += (ulong)u.p8[2] << 16;
					goto case 2uL;
				case 2uL:
					h3 += (ulong)u.p8[1] << 8;
					goto case 1uL;
				case 1uL:
					h3 += *u.p8;
					break;
				case 0uL:
					h3 += 16045690984833335023uL;
					h4 += 16045690984833335023uL;
					break;
				}
			}
			ShortEnd(ref h, ref h2, ref h3, ref h4);
			*hash1 = h;
			*hash2 = h2;
		}

		private static void ShortMix(ref ulong h0, ref ulong h1, ref ulong h2, ref ulong h3)
		{
			Rot64(ref h2, 50);
			h2 += h3;
			h0 ^= h2;
			Rot64(ref h3, 52);
			h3 += h0;
			h1 ^= h3;
			Rot64(ref h0, 30);
			h0 += h1;
			h2 ^= h0;
			Rot64(ref h1, 41);
			h1 += h2;
			h3 ^= h1;
			Rot64(ref h2, 54);
			h2 += h3;
			h0 ^= h2;
			Rot64(ref h3, 48);
			h3 += h0;
			h1 ^= h3;
			Rot64(ref h0, 38);
			h0 += h1;
			h2 ^= h0;
			Rot64(ref h1, 37);
			h1 += h2;
			h3 ^= h1;
			Rot64(ref h2, 62);
			h2 += h3;
			h0 ^= h2;
			Rot64(ref h3, 34);
			h3 += h0;
			h1 ^= h3;
			Rot64(ref h0, 5);
			h0 += h1;
			h2 ^= h0;
			Rot64(ref h1, 36);
			h1 += h2;
			h3 ^= h1;
		}

		private static void ShortEnd(ref ulong h0, ref ulong h1, ref ulong h2, ref ulong h3)
		{
			h3 ^= h2;
			Rot64(ref h2, 15);
			h3 += h2;
			h0 ^= h3;
			Rot64(ref h3, 52);
			h0 += h3;
			h1 ^= h0;
			Rot64(ref h0, 26);
			h1 += h0;
			h2 ^= h1;
			Rot64(ref h1, 51);
			h2 += h1;
			h3 ^= h2;
			Rot64(ref h2, 28);
			h3 += h2;
			h0 ^= h3;
			Rot64(ref h3, 9);
			h0 += h3;
			h1 ^= h0;
			Rot64(ref h0, 47);
			h1 += h0;
			h2 ^= h1;
			Rot64(ref h1, 54);
			h2 += h1;
			h3 ^= h2;
			Rot64(ref h2, 32);
			h3 += h2;
			h0 ^= h3;
			Rot64(ref h3, 25);
			h0 += h3;
			h1 ^= h0;
			Rot64(ref h0, 63);
			h1 += h0;
		}

		private unsafe static void Mix(ulong* data, ref ulong s0, ref ulong s1, ref ulong s2, ref ulong s3, ref ulong s4, ref ulong s5, ref ulong s6, ref ulong s7, ref ulong s8, ref ulong s9, ref ulong s10, ref ulong s11)
		{
			s0 += *data;
			s2 ^= s10;
			s11 ^= s0;
			Rot64(ref s0, 11);
			s11 += s1;
			s1 += data[1];
			s3 ^= s11;
			s0 ^= s1;
			Rot64(ref s1, 32);
			s0 += s2;
			s2 += data[2];
			s4 ^= s0;
			s1 ^= s2;
			Rot64(ref s2, 43);
			s1 += s3;
			s3 += data[3];
			s5 ^= s1;
			s2 ^= s3;
			Rot64(ref s3, 31);
			s2 += s4;
			s4 += data[4];
			s6 ^= s2;
			s3 ^= s4;
			Rot64(ref s4, 17);
			s3 += s5;
			s5 += data[5];
			s7 ^= s3;
			s4 ^= s5;
			Rot64(ref s5, 28);
			s4 += s6;
			s6 += data[6];
			s8 ^= s4;
			s5 ^= s6;
			Rot64(ref s6, 39);
			s5 += s7;
			s7 += data[7];
			s9 ^= s5;
			s6 ^= s7;
			Rot64(ref s7, 57);
			s6 += s8;
			s8 += data[8];
			s10 ^= s6;
			s7 ^= s8;
			Rot64(ref s8, 55);
			s7 += s9;
			s9 += data[9];
			s11 ^= s7;
			s8 ^= s9;
			Rot64(ref s9, 54);
			s8 += s10;
			s10 += data[10];
			s0 ^= s8;
			s9 ^= s10;
			Rot64(ref s10, 22);
			s9 += s11;
			s11 += data[11];
			s1 ^= s9;
			s10 ^= s11;
			Rot64(ref s11, 46);
			s10 += s0;
		}

		private unsafe static void memset(void* dst, int value, ulong numberOfBytes)
		{
			ulong num = (uint)(value | (value << 0));
			ulong* ptr = (ulong*)dst;
			ulong num2 = numberOfBytes >> 3;
			for (ulong num3 = 0uL; num3 < num2; num3++)
			{
				ptr[num3] = num;
			}
			dst = ptr;
			numberOfBytes -= num2;
			byte* ptr2 = stackalloc byte[4];
			*ptr2 = (byte)((uint)value & 0xFu);
			ptr2[1] = (byte)(((uint)value >> 4) & 0xFu);
			ptr2[2] = (byte)(((uint)value >> 8) & 0xFu);
			ptr2[3] = (byte)(((uint)value >> 12) & 0xFu);
			byte* ptr3 = (byte*)dst;
			ulong num4 = numberOfBytes;
			for (ulong num5 = 0uL; num5 < num4; num5++)
			{
				ptr3[num5] = ptr2[num5 % 4uL];
			}
		}
	}
}
