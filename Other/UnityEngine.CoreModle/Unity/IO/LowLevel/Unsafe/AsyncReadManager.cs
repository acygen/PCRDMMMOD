using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	[NativeHeader("Runtime/File/AsyncReadManagerManagedApi.h")]
	public static class AsyncReadManager
	{
		[FreeFunction("AsyncReadManagerManaged::Read", IsThreadSafe = true)]
		[ThreadAndSerializationSafe]
		private unsafe static ReadHandle ReadInternal(string filename, void* cmds, uint cmdCount)
		{
			ReadInternal_Injected(filename, cmds, cmdCount, out var ret);
			return ret;
		}

		public unsafe static ReadHandle Read(string filename, ReadCommand* readCmds, uint readCmdCount)
		{
			return ReadInternal(filename, readCmds, readCmdCount);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ReadInternal_Injected(string filename, void* cmds, uint cmdCount, out ReadHandle ret);
	}
}
