using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Elements;
using Elements.Battle;
using PCRCalculator.Tool;
using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;

namespace PCRCalculator.Hook
{
	/*[HarmonyPatch(typeof(PLFCLLHLDOO), "Update")]
	class NNKBUGFix
	{
		public static bool Prefix(PLFCLLHLDOO __instance, ref IEnumerator __result)
		{
			if (!PCRBattle.Instance.saveData.nnkBUG)
			{
				return true;
			}
			__result = UpdateFix(__instance);
			return false;
		}
		private static IEnumerator UpdateFix(PLFCLLHLDOO __instance)
		{
			float time = 0f;
			float intervalCount = 0f;
			AccessTools.FieldRef<PLFCLLHLDOO, bool> FDKMGLDCAJD =
AccessTools.FieldRefAccess<PLFCLLHLDOO, bool>("FDKMGLDCAJD");

			FDKMGLDCAJD(__instance) = true;

			BattleManager BGAGEJBMAMH = PCRSettings.staticBattleBanager;
			SkillEffectCtrl skillEffect = Traverse.Create(__instance).Field("skillEffect").GetValue<SkillEffectCtrl>();
			int MOOGAKLKDJI = Traverse.Create(__instance).Property("MOOGAKLKDJI").GetValue<int>();
			bool AHABEPKMKJJ = Traverse.Create(__instance).Property("AHABEPKMKJJ").GetValue<bool>();
			while (true)
			{
				if (BGAGEJBMAMH.GetBlackOutUnitLength() == 0 && (Object)(object)skillEffect != (Object)null)
				{
					skillEffect.SetSortOrderBack();
				}
				int i = 0;
				for (int count = __instance.ALFDJACNNCL.Count; i < count; i++)
				{
					Action<BasePartsData> action = delegate (BasePartsData FNHGFDNICFG)
					{
						//IL_0071: Unknown result type (might be due to invalid IL or missing references)
						//IL_008b: Unknown result type (might be due to invalid IL or missing references)
						//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
						//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
						var pp = Traverse.Create(__instance).Method("getClearedIndex", new object[] { FNHGFDNICFG.Owner }).GetValue<int>();


						if (FNHGFDNICFG.Owner.IsStealth)
						{
							if (__instance.BPDLLDEBJHE.Contains(FNHGFDNICFG))
							{
								__instance.OnExit(FNHGFDNICFG);
							}
						}
						else if (pp >= MOOGAKLKDJI)
						{
							if (__instance.BPDLLDEBJHE.Contains(FNHGFDNICFG))
							{
								__instance.OnExit(FNHGFDNICFG);
							}
						}
						else if (FNHGFDNICFG.Owner.IsDead)
						{
							if (__instance.BPDLLDEBJHE.Contains(FNHGFDNICFG))
							{
								__instance.OnExit(FNHGFDNICFG);
							}
						}
						else
						{
							bool flag = (FNHGFDNICFG.GetLocalPosition().x <= __instance.FNFIJFAIPNE + __instance.HIKKPKEKLDA 
							&& FNHGFDNICFG.GetLocalPosition().x >= __instance.FNFIJFAIPNE - __instance.HIKKPKEKLDA) 
							|| BattleUtil.Approximately(FNHGFDNICFG.GetLocalPosition().x, __instance.FNFIJFAIPNE + __instance.HIKKPKEKLDA) 
							|| BattleUtil.Approximately(FNHGFDNICFG.GetLocalPosition().x, __instance.FNFIJFAIPNE - __instance.HIKKPKEKLDA);
							if (FNHGFDNICFG.Owner.IsSummonOrPhantom && FNHGFDNICFG.Owner.IdleOnly)
							{
								flag = false;
							}
							if (!FNHGFDNICFG.GetTargetable())
							{
								flag = false;
							}
							if (flag && !__instance.BPDLLDEBJHE.Contains(FNHGFDNICFG))
							{
								__instance.OnEnter(FNHGFDNICFG);
							}
							else if (!flag && __instance.BPDLLDEBJHE.Contains(FNHGFDNICFG))
							{
								__instance.OnExit(FNHGFDNICFG);
							}
						}
					};
					UnitCtrl unitCtrl = __instance.ALFDJACNNCL[i];
					if (!unitCtrl.IsPartsBoss)
					{
						action(unitCtrl.GetFirstParts());
						continue;
					}
					for (int j = 0; j < unitCtrl.BossPartsListForBattle.Count; j++)
					{
						action(unitCtrl.BossPartsListForBattle[j]);
					}
				}
				if (__instance.HKDBJHAIOMB == eFieldExecType.REPEAT)
				{
					if (BGAGEJBMAMH.GetBlackOutUnitLength() == 0)
					{
						intervalCount += BGAGEJBMAMH.FNHFJLAENPF;
					}
					if (intervalCount > 1f)
					{
						intervalCount = 0f;
						__instance.OnRepeat();
					}
				}
				//if (BGAGEJBMAMH.GetBlackOutUnitLength() == 0)
				//{
					time += BGAGEJBMAMH.FNHFJLAENPF;
				//}
				eBattleGameState gameState = BGAGEJBMAMH.MMBMBJNNACG;
				if (time > __instance.MCICHHNFAKA || gameState == eBattleGameState.NEXT_WAVE_PROCESS 
					|| gameState == eBattleGameState.WAIT_WAVE_END || AHABEPKMKJJ)
				{
					break;
				}
				yield return null;
			}
			if (__instance.PMHDBOJMEAD != null && __instance.PMHDBOJMEAD.BonusId != 0)
			{
				BGAGEJBMAMH.DeleteBonusIcon(__instance.PMHDBOJMEAD.BonusId);
			}
			for (int num = __instance.BPDLLDEBJHE.Count - 1; num >= 0; num--)
			{
				if (num < __instance.BPDLLDEBJHE.Count)
				{
					__instance.OnExit(__instance.BPDLLDEBJHE[num]);
				}
			}
			FDKMGLDCAJD(__instance) = false;
			if (!((Object)(object)skillEffect != (Object)null))
			{
				yield break;
			}
			FieldEffectController fieldEffect = skillEffect as FieldEffectController;
			fieldEffect.StopEmitter();
			if (fieldEffect.EndDuration == 0f)
			{
				skillEffect.SetTimeToDie(true);
			}
			else
			{
				float endDuration = fieldEffect.EndDuration;
				while (endDuration > 0f)
				{
					if (BGAGEJBMAMH.GetBlackOutUnitLength() == 0)
					{
						endDuration -= BGAGEJBMAMH.FNHFJLAENPF;
					}
					yield return null;
				}
				skillEffect.SetTimeToDie(true);
			}
			var pp0 = Traverse.Create(__instance).Method("createPrefabWithTime", new object[]{ fieldEffect.EndEffectPrefab,
				((Component)skillEffect).transform.position}).GetValue<IEnumerator>();

			
BGAGEJBMAMH.AppendCoroutine(pp0, ePauseType.SYSTEM, __instance.EGEPDDJBILL);
		
		}

	}*/
}
    

