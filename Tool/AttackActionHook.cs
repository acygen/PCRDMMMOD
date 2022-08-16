using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elements;
using Elements.Battle;
using UnityEngine;
using HarmonyLib;
using PCRCalculator.Tool;


namespace PCRCalculator.Hook
{
    //[HarmonyPatch(typeof(AttackAction), "ExecAction")]
    public class AttackActionHook
    {
        public static bool Prefix(AttackAction __instance,UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, Dictionary<eValueNumber, float> _valueDictionary)
        {
            if (PCRBattle.Instance.UseFakeCritical)
            {
                if (_target.Owner.IsOther)
                {
                    BattleManagerHook.RandomState = PCRBattle.Instance.FakeCritLev_enemy;
                }
                else
                {
                    BattleManagerHook.RandomState = PCRBattle.Instance.FakeCritLev_player;
                }
            }
            return true;
        }

    }
    [HarmonyPatch(typeof(AttackActionBase), "createDamageData")]
    public class AttackActionBaseHook
    {
        public static void Postfix(AttackActionBase __instance,Elements.DamageData __result, UnitCtrl _source, BasePartsData _target, int _num, Dictionary<eValueNumber, float> _valueDictionary, Elements.AttackActionBase.eAttackType _actionDetail1, bool _isCritical, Skill _skill, eActionType _actionTypeForSource, bool _isPhysicalForTarget)
        {
            if (PCRBattle.Instance.UseFakeCritical)
            {
                int lev = 0;
                if (_target.Owner.IsOther)
                {
                    lev = PCRBattle.Instance.FakeCritLev_enemy;
                }
                else
                {
                    lev = PCRBattle.Instance.FakeCritLev_player;
                }
                __result.CriticalRate = ReCalValue(__result.CriticalRate, lev);
                __result.CriticalRateForLogBarrier = ReCalValue(__result.CriticalRateForLogBarrier, lev);
            }
        }
        private static float ReCalValue(float target,int lv)
        {
            float result = 0;
            switch (lv)
            {
                case 0:
                    return target;
                case 1:
                    result = Mathf.Pow(target, 2f);
                    break;
                case 2:
                    result = target <= 0 ? 0 : 1;
                    break;
                case -1:
                    result = Mathf.Pow(target, 0.5f);
                    break;
                case -2:
                    result = target >= 1 ? 1 : 0;
                    break;
            }
            return result >= 0 ? (result <= 1 ? result : 1) : 0;
        }

    }
}
