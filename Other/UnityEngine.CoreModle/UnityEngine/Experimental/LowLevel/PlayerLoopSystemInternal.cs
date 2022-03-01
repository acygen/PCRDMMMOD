using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.LowLevel
{
	[RequiredByNativeCode]
	[NativeType(Header = "Runtime/Misc/PlayerLoop.h")]
	internal struct PlayerLoopSystemInternal
	{
		public Type type;

		public PlayerLoopSystem.UpdateFunction updateDelegate;

		public IntPtr updateFunction;

		public IntPtr loopConditionFunction;

		public int numSubSystems;
	}
}
