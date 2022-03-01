using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.PlayerLoop
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	[RequiredByNativeCode]
	public struct Initialization
	{
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct PlayerUpdateTime
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct AsyncUploadTimeSlicedUpdate
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct SynchronizeState
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct SynchronizeInputs
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct XREarlyUpdate
		{
		}
	}
}
