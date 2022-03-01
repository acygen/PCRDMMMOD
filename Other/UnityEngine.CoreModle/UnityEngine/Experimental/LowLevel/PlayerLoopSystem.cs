using System;

namespace UnityEngine.Experimental.LowLevel
{
	public struct PlayerLoopSystem
	{
		public delegate void UpdateFunction();

		public Type type;

		public PlayerLoopSystem[] subSystemList;

		public UpdateFunction updateDelegate;

		public IntPtr updateFunction;

		public IntPtr loopConditionFunction;
	}
}
