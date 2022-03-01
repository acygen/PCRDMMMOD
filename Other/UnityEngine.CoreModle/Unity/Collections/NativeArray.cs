using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Internal;

namespace Unity.Collections
{
	[NativeContainerSupportsDeallocateOnJobCompletion]
	[DebuggerTypeProxy(typeof(NativeArrayDebugView<>))]
	[NativeContainerSupportsDeferredConvertListToArray]
	[DebuggerDisplay("Length = {Length}")]
	[NativeContainerSupportsMinMaxWriteRestriction]
	[NativeContainer]
	public struct NativeArray<T> : IDisposable, IEnumerable<T>, IEquatable<NativeArray<T>>, IEnumerable where T : struct
	{
		[ExcludeFromDocs]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			private NativeArray<T> m_Array;

			private int m_Index;

			object IEnumerator.Current
			{
				[CompilerGenerated]
				get
				{
					return Current;
				}
			}

			public T Current
			{
				[CompilerGenerated]
				get
				{
					return m_Array[m_Index];
				}
			}

			public Enumerator(ref NativeArray<T> array)
			{
				m_Array = array;
				m_Index = -1;
			}

			public void Dispose()
			{
			}

			public bool MoveNext()
			{
				m_Index++;
				return m_Index < m_Array.Length;
			}

			public void Reset()
			{
				m_Index = -1;
			}
		}

		[NativeDisableUnsafePtrRestriction]
		internal unsafe void* m_Buffer;

		internal int m_Length;

		internal Allocator m_AllocatorLabel;

		public int Length
		{
			[CompilerGenerated]
			get
			{
				return m_Length;
			}
		}

		public unsafe T this[int index]
		{
			get
			{
				return UnsafeUtility.ReadArrayElement<T>(m_Buffer, index);
			}
			[WriteAccessRequired]
			set
			{
				UnsafeUtility.WriteArrayElement(m_Buffer, index, value);
			}
		}

		public unsafe bool IsCreated
		{
			[CompilerGenerated]
			get
			{
				return m_Buffer != null;
			}
		}

		public unsafe NativeArray(int length, Allocator allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory)
		{
			Allocate(length, allocator, out this);
			if ((options & NativeArrayOptions.ClearMemory) == NativeArrayOptions.ClearMemory)
			{
				UnsafeUtility.MemClear(m_Buffer, (long)Length * (long)UnsafeUtility.SizeOf<T>());
			}
		}

		public NativeArray(T[] array, Allocator allocator)
		{
			Allocate(array.Length, allocator, out this);
			Copy(array, this);
		}

		public NativeArray(NativeArray<T> array, Allocator allocator)
		{
			Allocate(array.Length, allocator, out this);
			Copy(array, this);
		}

		private unsafe static void Allocate(int length, Allocator allocator, out NativeArray<T> array)
		{
			long size = (long)UnsafeUtility.SizeOf<T>() * (long)length;
			array.m_Buffer = UnsafeUtility.Malloc(size, UnsafeUtility.AlignOf<T>(), allocator);
			array.m_Length = length;
			array.m_AllocatorLabel = allocator;
		}

		[BurstDiscard]
		internal static void IsBlittableAndThrow()
		{
			if (!UnsafeUtility.IsBlittable<T>())
			{
				throw new InvalidOperationException($"{typeof(T)} used in NativeArray<{typeof(T)}> must be blittable.\n{UnsafeUtility.GetReasonForValueTypeNonBlittable<T>()}");
			}
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckElementReadAccess(int index)
		{
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckElementWriteAccess(int index)
		{
		}

		[WriteAccessRequired]
		public unsafe void Dispose()
		{
			UnsafeUtility.Free(m_Buffer, m_AllocatorLabel);
			m_Buffer = null;
			m_Length = 0;
		}

		[WriteAccessRequired]
		public void CopyFrom(T[] array)
		{
			Copy(array, this);
		}

		[WriteAccessRequired]
		public void CopyFrom(NativeArray<T> array)
		{
			Copy(array, this);
		}

		public void CopyTo(T[] array)
		{
			Copy(this, array);
		}

		public void CopyTo(NativeArray<T> array)
		{
			Copy(this, array);
		}

		public T[] ToArray()
		{
			T[] array = new T[Length];
			Copy(this, array, Length);
			return array;
		}

		public Enumerator GetEnumerator()
		{
			return new Enumerator(ref this);
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new Enumerator(ref this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public unsafe bool Equals(NativeArray<T> other)
		{
			return m_Buffer == other.m_Buffer && m_Length == other.m_Length;
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(null, obj))
			{
				return false;
			}
			return obj is NativeArray<T> && Equals((NativeArray<T>)obj);
		}

		public unsafe override int GetHashCode()
		{
			return ((int)m_Buffer * 397) ^ m_Length;
		}

		public static bool operator ==(NativeArray<T> left, NativeArray<T> right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(NativeArray<T> left, NativeArray<T> right)
		{
			return !left.Equals(right);
		}

		public static void Copy(NativeArray<T> src, NativeArray<T> dst)
		{
			Copy(src, 0, dst, 0, src.Length);
		}

		public static void Copy(T[] src, NativeArray<T> dst)
		{
			Copy(src, 0, dst, 0, src.Length);
		}

		public static void Copy(NativeArray<T> src, T[] dst)
		{
			Copy(src, 0, dst, 0, src.Length);
		}

		public static void Copy(NativeArray<T> src, NativeArray<T> dst, int length)
		{
			Copy(src, 0, dst, 0, length);
		}

		public static void Copy(T[] src, NativeArray<T> dst, int length)
		{
			Copy(src, 0, dst, 0, length);
		}

		public static void Copy(NativeArray<T> src, T[] dst, int length)
		{
			Copy(src, 0, dst, 0, length);
		}

		public unsafe static void Copy(NativeArray<T> src, int srcIndex, NativeArray<T> dst, int dstIndex, int length)
		{
			UnsafeUtility.MemCpy((byte*)dst.m_Buffer + dstIndex * UnsafeUtility.SizeOf<T>(), (byte*)src.m_Buffer + srcIndex * UnsafeUtility.SizeOf<T>(), length * UnsafeUtility.SizeOf<T>());
		}

		public unsafe static void Copy(T[] src, int srcIndex, NativeArray<T> dst, int dstIndex, int length)
		{
			GCHandle gCHandle = GCHandle.Alloc(src, GCHandleType.Pinned);
			IntPtr intPtr = gCHandle.AddrOfPinnedObject();
			UnsafeUtility.MemCpy((byte*)dst.m_Buffer + dstIndex * UnsafeUtility.SizeOf<T>(), (byte*)(void*)intPtr + srcIndex * UnsafeUtility.SizeOf<T>(), length * UnsafeUtility.SizeOf<T>());
			gCHandle.Free();
		}

		public unsafe static void Copy(NativeArray<T> src, int srcIndex, T[] dst, int dstIndex, int length)
		{
			GCHandle gCHandle = GCHandle.Alloc(dst, GCHandleType.Pinned);
			IntPtr intPtr = gCHandle.AddrOfPinnedObject();
			UnsafeUtility.MemCpy((byte*)(void*)intPtr + dstIndex * UnsafeUtility.SizeOf<T>(), (byte*)src.m_Buffer + srcIndex * UnsafeUtility.SizeOf<T>(), length * UnsafeUtility.SizeOf<T>());
			gCHandle.Free();
		}
	}
}
