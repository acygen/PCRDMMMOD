using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/Ping.bindings.h")]
	public sealed class Ping
	{
		internal IntPtr m_Ptr;

		public bool isDone
		{
			get
			{
				if (m_Ptr == IntPtr.Zero)
				{
					return false;
				}
				return Internal_IsDone();
			}
		}

		public extern int time
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern string ip
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetIP")]
			get;
		}

		public Ping(string address)
		{
			m_Ptr = Internal_Create(address);
		}

		~Ping()
		{
			DestroyPing();
		}

		[ThreadAndSerializationSafe]
		public void DestroyPing()
		{
			if (!(m_Ptr == IntPtr.Zero))
			{
				Internal_Destroy(m_Ptr);
				m_Ptr = IntPtr.Zero;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("DestroyPing", IsThreadSafe = true)]
		private static extern void Internal_Destroy(IntPtr ptr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CreatePing")]
		private static extern IntPtr Internal_Create(string address);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetIsDone")]
		private extern bool Internal_IsDone();
	}
}
