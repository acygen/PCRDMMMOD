using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.LowLevel
{
	public class PlayerLoop
	{
		public static PlayerLoopSystem GetDefaultPlayerLoop()
		{
			PlayerLoopSystemInternal[] defaultPlayerLoopInternal = GetDefaultPlayerLoopInternal();
			int offset = 0;
			return InternalToPlayerLoopSystem(defaultPlayerLoopInternal, ref offset);
		}

		public static void SetPlayerLoop(PlayerLoopSystem loop)
		{
			List<PlayerLoopSystemInternal> internalSys = new List<PlayerLoopSystemInternal>();
			PlayerLoopSystemToInternal(loop, ref internalSys);
			SetPlayerLoopInternal(internalSys.ToArray());
		}

		private static int PlayerLoopSystemToInternal(PlayerLoopSystem sys, ref List<PlayerLoopSystemInternal> internalSys)
		{
			int count = internalSys.Count;
			PlayerLoopSystemInternal playerLoopSystemInternal = default(PlayerLoopSystemInternal);
			playerLoopSystemInternal.type = sys.type;
			playerLoopSystemInternal.updateDelegate = sys.updateDelegate;
			playerLoopSystemInternal.updateFunction = sys.updateFunction;
			playerLoopSystemInternal.loopConditionFunction = sys.loopConditionFunction;
			playerLoopSystemInternal.numSubSystems = 0;
			PlayerLoopSystemInternal playerLoopSystemInternal2 = playerLoopSystemInternal;
			internalSys.Add(playerLoopSystemInternal2);
			if (sys.subSystemList != null)
			{
				for (int i = 0; i < sys.subSystemList.Length; i++)
				{
					playerLoopSystemInternal2.numSubSystems += PlayerLoopSystemToInternal(sys.subSystemList[i], ref internalSys);
				}
			}
			internalSys[count] = playerLoopSystemInternal2;
			return playerLoopSystemInternal2.numSubSystems + 1;
		}

		private static PlayerLoopSystem InternalToPlayerLoopSystem(PlayerLoopSystemInternal[] internalSys, ref int offset)
		{
			PlayerLoopSystem playerLoopSystem = default(PlayerLoopSystem);
			playerLoopSystem.type = internalSys[offset].type;
			playerLoopSystem.updateDelegate = internalSys[offset].updateDelegate;
			playerLoopSystem.updateFunction = internalSys[offset].updateFunction;
			playerLoopSystem.loopConditionFunction = internalSys[offset].loopConditionFunction;
			playerLoopSystem.subSystemList = null;
			PlayerLoopSystem result = playerLoopSystem;
			int num = offset++;
			if (internalSys[num].numSubSystems > 0)
			{
				List<PlayerLoopSystem> list = new List<PlayerLoopSystem>();
				while (offset <= num + internalSys[num].numSubSystems)
				{
					list.Add(InternalToPlayerLoopSystem(internalSys, ref offset));
				}
				result.subSystemList = list.ToArray();
			}
			return result;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsFreeFunction = true)]
		private static extern PlayerLoopSystemInternal[] GetDefaultPlayerLoopInternal();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsFreeFunction = true)]
		private static extern void SetPlayerLoopInternal(PlayerLoopSystemInternal[] loop);
	}
}
