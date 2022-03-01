using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[NativeHeader("Runtime/Export/ComputeShader.bindings.h")]
	[UsedByNativeCode]
	public sealed class ComputeBuffer : IDisposable
	{
		internal IntPtr m_Ptr;

		public extern int count
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int stride
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public ComputeBuffer(int count, int stride)
			: this(count, stride, ComputeBufferType.Default, 3)
		{
		}

		public ComputeBuffer(int count, int stride, ComputeBufferType type)
			: this(count, stride, type, 3)
		{
		}

		internal ComputeBuffer(int count, int stride, ComputeBufferType type, int stackDepth)
		{
			if (count <= 0)
			{
				throw new ArgumentException("Attempting to create a zero length compute buffer", "count");
			}
			if (stride <= 0)
			{
				throw new ArgumentException("Attempting to create a compute buffer with a negative or null stride", "stride");
			}
			m_Ptr = InitBuffer(count, stride, type);
		}

		~ComputeBuffer()
		{
			Dispose(disposing: false);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				DestroyBuffer(this);
			}
			else if (m_Ptr != IntPtr.Zero)
			{
				Debug.LogWarning("GarbageCollector disposing of ComputeBuffer. Please use ComputeBuffer.Release() or .Dispose() to manually release the buffer.");
			}
			m_Ptr = IntPtr.Zero;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ComputeShader_Bindings::InitBuffer")]
		private static extern IntPtr InitBuffer(int count, int stride, ComputeBufferType type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ComputeShader_Bindings::DestroyBuffer")]
		private static extern void DestroyBuffer(ComputeBuffer buf);

		public void Release()
		{
			Dispose();
		}

		public bool IsValid()
		{
			return m_Ptr != IntPtr.Zero;
		}

		[SecuritySafeCritical]
		public void SetData(Array data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (!UnsafeUtility.IsArrayBlittable(data))
			{
				throw new ArgumentException($"Array passed to ComputeBuffer.SetData(array) must be blittable.\n{UnsafeUtility.GetReasonForArrayNonBlittable(data)}");
			}
			InternalSetData(data, 0, 0, data.Length, UnsafeUtility.SizeOf(data.GetType().GetElementType()));
		}

		[SecuritySafeCritical]
		public void SetData<T>(List<T> data) where T : struct
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (!UnsafeUtility.IsGenericListBlittable<T>())
			{
				throw new ArgumentException($"List<{typeof(T)}> passed to ComputeBuffer.SetData(List<>) must be blittable.\n{UnsafeUtility.GetReasonForGenericListNonBlittable<T>()}");
			}
			InternalSetData(NoAllocHelpers.ExtractArrayFromList(data), 0, 0, NoAllocHelpers.SafeLength(data), Marshal.SizeOf(typeof(T)));
		}

		[SecuritySafeCritical]
		public unsafe void SetData<T>(NativeArray<T> data) where T : struct
		{
			InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr(), 0, 0, data.Length, UnsafeUtility.SizeOf<T>());
		}

		[SecuritySafeCritical]
		public void SetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (!UnsafeUtility.IsArrayBlittable(data))
			{
				throw new ArgumentException($"Array passed to ComputeBuffer.SetData(array) must be blittable.\n{UnsafeUtility.GetReasonForArrayNonBlittable(data)}");
			}
			if (managedBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length)
			{
				throw new ArgumentOutOfRangeException($"Bad indices/count arguments (managedBufferStartIndex:{managedBufferStartIndex} computeBufferStartIndex:{computeBufferStartIndex} count:{count})");
			}
			InternalSetData(data, managedBufferStartIndex, computeBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		[SecuritySafeCritical]
		public void SetData<T>(List<T> data, int managedBufferStartIndex, int computeBufferStartIndex, int count) where T : struct
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (!UnsafeUtility.IsGenericListBlittable<T>())
			{
				throw new ArgumentException($"List<{typeof(T)}> passed to ComputeBuffer.SetData(List<>) must be blittable.\n{UnsafeUtility.GetReasonForGenericListNonBlittable<T>()}");
			}
			if (managedBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Count)
			{
				throw new ArgumentOutOfRangeException($"Bad indices/count arguments (managedBufferStartIndex:{managedBufferStartIndex} computeBufferStartIndex:{computeBufferStartIndex} count:{count})");
			}
			InternalSetData(NoAllocHelpers.ExtractArrayFromList(data), managedBufferStartIndex, computeBufferStartIndex, count, Marshal.SizeOf(typeof(T)));
		}

		[SecuritySafeCritical]
		public unsafe void SetData<T>(NativeArray<T> data, int nativeBufferStartIndex, int computeBufferStartIndex, int count) where T : struct
		{
			if (nativeBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || nativeBufferStartIndex + count > data.Length)
			{
				throw new ArgumentOutOfRangeException($"Bad indices/count arguments (nativeBufferStartIndex:{nativeBufferStartIndex} computeBufferStartIndex:{computeBufferStartIndex} count:{count})");
			}
			InternalSetNativeData((IntPtr)data.GetUnsafeReadOnlyPtr(), nativeBufferStartIndex, computeBufferStartIndex, count, UnsafeUtility.SizeOf<T>());
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SecurityCritical]
		[FreeFunction(Name = "ComputeShader_Bindings::InternalSetNativeData", HasExplicitThis = true, ThrowsException = true)]
		private extern void InternalSetNativeData(IntPtr data, int nativeBufferStartIndex, int computeBufferStartIndex, int count, int elemSize);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SecurityCritical]
		[FreeFunction(Name = "ComputeShader_Bindings::InternalSetData", HasExplicitThis = true, ThrowsException = true)]
		private extern void InternalSetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count, int elemSize);

		[SecurityCritical]
		public void GetData(Array data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (!UnsafeUtility.IsArrayBlittable(data))
			{
				throw new ArgumentException($"Array passed to ComputeBuffer.GetData(array) must be blittable.\n{UnsafeUtility.GetReasonForArrayNonBlittable(data)}");
			}
			InternalGetData(data, 0, 0, data.Length, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		[SecurityCritical]
		public void GetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (!UnsafeUtility.IsArrayBlittable(data))
			{
				throw new ArgumentException($"Array passed to ComputeBuffer.GetData(array) must be blittable.\n{UnsafeUtility.GetReasonForArrayNonBlittable(data)}");
			}
			if (managedBufferStartIndex < 0 || computeBufferStartIndex < 0 || count < 0 || managedBufferStartIndex + count > data.Length)
			{
				throw new ArgumentOutOfRangeException($"Bad indices/count argument (managedBufferStartIndex:{managedBufferStartIndex} computeBufferStartIndex:{computeBufferStartIndex} count:{count})");
			}
			InternalGetData(data, managedBufferStartIndex, computeBufferStartIndex, count, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SecurityCritical]
		[FreeFunction(Name = "ComputeShader_Bindings::InternalGetData", HasExplicitThis = true, ThrowsException = true)]
		private extern void InternalGetData(Array data, int managedBufferStartIndex, int computeBufferStartIndex, int count, int elemSize);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetCounterValue(uint counterValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CopyCount(ComputeBuffer src, ComputeBuffer dst, int dstOffsetBytes);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IntPtr GetNativeBufferPtr();
	}
}
