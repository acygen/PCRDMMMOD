using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/GPUFence.h")]
	public struct GPUFence
	{
		internal IntPtr m_Ptr;

		internal int m_Version;

		public bool passed
		{
			get
			{
				Validate();
				if (!SystemInfo.supportsGPUFence)
				{
					throw new NotSupportedException("Cannot determine if this GPUFence has passed as this platform has not implemented GPUFences.");
				}
				if (!IsFencePending())
				{
					return true;
				}
				return HasFencePassed_Internal(m_Ptr);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GPUFenceInternals::HasFencePassed_Internal")]
		private static extern bool HasFencePassed_Internal(IntPtr fencePtr);

		internal void InitPostAllocation()
		{
			if (m_Ptr == IntPtr.Zero)
			{
				if (SystemInfo.supportsGPUFence)
				{
					throw new NullReferenceException("The internal fence ptr is null, this should not be possible for fences that have been correctly constructed using Graphics.CreateGPUFence() or CommandBuffer.CreateGPUFence()");
				}
				m_Version = GetPlatformNotSupportedVersion();
			}
			else
			{
				m_Version = GetVersionNumber(m_Ptr);
			}
		}

		internal bool IsFencePending()
		{
			if (m_Ptr == IntPtr.Zero)
			{
				return false;
			}
			return m_Version == GetVersionNumber(m_Ptr);
		}

		internal void Validate()
		{
			if (m_Version == 0 || (SystemInfo.supportsGPUFence && m_Version == GetPlatformNotSupportedVersion()))
			{
				throw new InvalidOperationException("This GPUFence object has not been correctly constructed see Graphics.CreateGPUFence() or CommandBuffer.CreateGPUFence()");
			}
		}

		private int GetPlatformNotSupportedVersion()
		{
			return -1;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GPUFenceInternals::GetVersionNumber")]
		[NativeThrows]
		private static extern int GetVersionNumber(IntPtr fencePtr);
	}
}
