using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Internal;

namespace Unity.Collections
{
	[NativeContainer]
	[NativeContainerSupportsMinMaxWriteRestriction]
	[DebuggerDisplay("Length = {Length}")]
	[DebuggerTypeProxy(typeof(NativeSliceDebugView<>))]
	public struct NativeSlice<T> : IEnumerable<T>, IEquatable<NativeSlice<T>>, IEnumerable where T : struct
	{
		[ExcludeFromDocs]
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			private NativeSlice<T> m_Array;

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

			public Enumerator(ref NativeSlice<T> array)
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
		internal unsafe byte* m_Buffer;

		internal int m_Stride;

		internal int m_Length;

		public unsafe T this[int index]
		{
			get
			{
				return UnsafeUtility.ReadArrayElementWithStride<T>(m_Buffer, index, m_Stride);
			}
			[WriteAccessRequired]
			set
			{
				UnsafeUtility.WriteArrayElementWithStride(m_Buffer, index, m_Stride, value);
			}
		}

		public int Stride
		{
			[CompilerGenerated]
			get
			{
				return m_Stride;
			}
		}

		public int Length
		{
			[CompilerGenerated]
			get
			{
				return m_Length;
			}
		}

		public NativeSlice(NativeSlice<T> slice, int start)
			: this(slice, start, slice.Length - start)
		{
		}

		public unsafe NativeSlice(NativeSlice<T> slice, int start, int length)
		{
			m_Stride = slice.m_Stride;
			m_Buffer = slice.m_Buffer + m_Stride * start;
			m_Length = length;
		}

		public NativeSlice(NativeArray<T> array)
			: this(array, 0, array.Length)
		{
		}

		public NativeSlice(NativeArray<T> array, int start)
			: this(array, start, array.Length - start)
		{
		}

		public unsafe NativeSlice(NativeArray<T> array, int start, int length)
		{
			m_Stride = UnsafeUtility.SizeOf<T>();
			byte* ptr = (m_Buffer = (byte*)array.m_Buffer + m_Stride * start);
			m_Length = length;
		}

		public static implicit operator NativeSlice<T>(NativeArray<T> array)
		{
			return new NativeSlice<T>(array);
		}

		public unsafe NativeSlice<U> SliceConvert<U>() where U : struct
		{
			int num = UnsafeUtility.SizeOf<U>();
			NativeSlice<U> result = default(NativeSlice<U>);
			result.m_Buffer = m_Buffer;
			result.m_Stride = num;
			result.m_Length = m_Length * m_Stride / num;
			return result;
		}

		public unsafe NativeSlice<U> SliceWithStride<U>(int offset) where U : struct
		{
			NativeSlice<U> result = default(NativeSlice<U>);
			result.m_Buffer = m_Buffer + offset;
			result.m_Stride = m_Stride;
			result.m_Length = m_Length;
			return result;
		}

		public NativeSlice<U> SliceWithStride<U>() where U : struct
		{
			return SliceWithStride<U>(0);
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckReadIndex(int index)
		{
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckWriteIndex(int index)
		{
		}

		[WriteAccessRequired]
		public unsafe void CopyFrom(NativeSlice<T> slice)
		{
			UnsafeUtility.MemCpyStride(this.GetUnsafePtr(), Stride, slice.GetUnsafeReadOnlyPtr(), slice.Stride, UnsafeUtility.SizeOf<T>(), m_Length);
		}

		[WriteAccessRequired]
		public void CopyFrom(T[] array)
		{
			for (int i = 0; i != m_Length; i++)
			{
				this[i] = array[i];
			}
		}

		public unsafe void CopyTo(NativeArray<T> array)
		{
			int num = UnsafeUtility.SizeOf<T>();
			UnsafeUtility.MemCpyStride(array.GetUnsafePtr(), num, this.GetUnsafeReadOnlyPtr(), Stride, num, m_Length);
		}

		public void CopyTo(T[] array)
		{
			for (int i = 0; i != m_Length; i++)
			{
				array[i] = this[i];
			}
		}

		public T[] ToArray()
		{
			T[] array = new T[Length];
			CopyTo(array);
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

		public unsafe bool Equals(NativeSlice<T> other)
		{
			return m_Buffer == other.m_Buffer && m_Stride == other.m_Stride && m_Length == other.m_Length;
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(null, obj))
			{
				return false;
			}
			return obj is NativeSlice<T> && Equals((NativeSlice<T>)obj);
		}

		public unsafe override int GetHashCode()
		{
			int num = (int)m_Buffer;
			num = (num * 397) ^ m_Stride;
			return (num * 397) ^ m_Length;
		}

		public static bool operator ==(NativeSlice<T> left, NativeSlice<T> right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(NativeSlice<T> left, NativeSlice<T> right)
		{
			return !left.Equals(right);
		}
	}
}
