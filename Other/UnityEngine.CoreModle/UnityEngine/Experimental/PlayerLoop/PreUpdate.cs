using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.PlayerLoop
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	[RequiredByNativeCode]
	public struct PreUpdate
	{
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct PhysicsUpdate
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct Physics2DUpdate
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct CheckTexFieldInput
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct IMGUISendQueuedEvents
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct SendMouseEvents
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct AIUpdate
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct WindUpdate
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct UpdateVideo
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct NewInputUpdate
		{
		}
	}
}
