using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[StaticAccessor("CacheWrapper", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Misc/Cache.h")]
	public struct Cache : IEquatable<Cache>
	{
		private int m_Handle;

		internal int handle => m_Handle;

		public bool valid => Cache_IsValid(m_Handle);

		public bool ready => Cache_IsReady(m_Handle);

		public bool readOnly => Cache_IsReadonly(m_Handle);

		public string path => Cache_GetPath(m_Handle);

		public int index => Cache_GetIndex(m_Handle);

		public long spaceFree => Cache_GetSpaceFree(m_Handle);

		public long maximumAvailableStorageSpace
		{
			get
			{
				return Cache_GetMaximumDiskSpaceAvailable(m_Handle);
			}
			set
			{
				Cache_SetMaximumDiskSpaceAvailable(m_Handle, value);
			}
		}

		public long spaceOccupied => Cache_GetCachingDiskSpaceUsed(m_Handle);

		public int expirationDelay
		{
			get
			{
				return Cache_GetExpirationDelay(m_Handle);
			}
			set
			{
				Cache_SetExpirationDelay(m_Handle, value);
			}
		}

		public static bool operator ==(Cache lhs, Cache rhs)
		{
			return lhs.handle == rhs.handle;
		}

		public static bool operator !=(Cache lhs, Cache rhs)
		{
			return lhs.handle != rhs.handle;
		}

		public override int GetHashCode()
		{
			return m_Handle;
		}

		public override bool Equals(object other)
		{
			return other is Cache && Equals((Cache)other);
		}

		public bool Equals(Cache other)
		{
			return handle == other.handle;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Cache_IsValid(int handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern bool Cache_IsReady(int handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern bool Cache_IsReadonly(int handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern string Cache_GetPath(int handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Cache_GetIndex(int handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern long Cache_GetSpaceFree(int handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern long Cache_GetMaximumDiskSpaceAvailable(int handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern void Cache_SetMaximumDiskSpaceAvailable(int handle, long value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern long Cache_GetCachingDiskSpaceUsed(int handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern int Cache_GetExpirationDelay(int handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern void Cache_SetExpirationDelay(int handle, int value);

		public bool ClearCache()
		{
			return Cache_ClearCache(m_Handle);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern bool Cache_ClearCache(int handle);

		public bool ClearCache(int expiration)
		{
			return Cache_ClearCache_Expiration(m_Handle, expiration);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern bool Cache_ClearCache_Expiration(int handle, int expiration);
	}
}
