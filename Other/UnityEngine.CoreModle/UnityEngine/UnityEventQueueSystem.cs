using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Export/UnityEventQueueSystem.bindings.h")]
	public class UnityEventQueueSystem
	{
		public static string GenerateEventIdForPayload(string eventPayloadName)
		{
			byte[] array = Guid.NewGuid().ToByteArray();
			return $"REGISTER_EVENT_ID(0x{array[0]:X2}{array[1]:X2}{array[2]:X2}{array[3]:X2}{array[4]:X2}{array[5]:X2}{array[6]:X2}{array[7]:X2}ULL,0x{array[8]:X2}{array[9]:X2}{array[10]:X2}{array[11]:X2}{array[12]:X2}{array[13]:X2}{array[14]:X2}{array[15]:X2}ULL,{eventPayloadName})";
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern IntPtr GetGlobalEventQueue();
	}
}
