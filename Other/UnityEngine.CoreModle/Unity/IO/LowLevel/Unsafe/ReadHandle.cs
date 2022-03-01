using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	public struct ReadHandle : IDisposable
	{
		[NativeDisableUnsafePtrRestriction]
		internal IntPtr ptr;

		internal int version;

		public JobHandle JobHandle
		{
			get
			{
				if (!IsReadHandleValid(this))
				{
					throw new InvalidOperationException("ReadHandle.JobHandle cannot be called after the ReadHandle has been disposed");
				}
				return GetJobHandle(this);
			}
		}

		public ReadStatus Status
		{
			get
			{
				if (!IsReadHandleValid(this))
				{
					throw new InvalidOperationException("ReadHandle.Status cannot be called after the ReadHandle has been disposed");
				}
				return GetReadStatus(this);
			}
		}

		public bool IsValid()
		{
			return IsReadHandleValid(this);
		}

		public void Dispose()
		{
			if (!IsReadHandleValid(this))
			{
				throw new InvalidOperationException("ReadHandle.Dispose cannot be called twice on the same ReadHandle");
			}
			if (Status == ReadStatus.InProgress)
			{
				throw new InvalidOperationException("ReadHandle.Dispose cannot be called until the read operation completes");
			}
			ReleaseReadHandle(this);
		}

		[FreeFunction("AsyncReadManagerManaged::GetReadStatus", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private static ReadStatus GetReadStatus(ReadHandle handle)
		{
			return GetReadStatus_Injected(ref handle);
		}

		[FreeFunction("AsyncReadManagerManaged::ReleaseReadHandle", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private static void ReleaseReadHandle(ReadHandle handle)
		{
			ReleaseReadHandle_Injected(ref handle);
		}

		[ThreadAndSerializationSafe]
		[FreeFunction("AsyncReadManagerManaged::IsReadHandleValid", IsThreadSafe = true)]
		private static bool IsReadHandleValid(ReadHandle handle)
		{
			return IsReadHandleValid_Injected(ref handle);
		}

		[FreeFunction("AsyncReadManagerManaged::GetJobHandle", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private static JobHandle GetJobHandle(ReadHandle handle)
		{
			GetJobHandle_Injected(ref handle, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ReadStatus GetReadStatus_Injected(ref ReadHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseReadHandle_Injected(ref ReadHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsReadHandleValid_Injected(ref ReadHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetJobHandle_Injected(ref ReadHandle handle, out JobHandle ret);
	}
}
