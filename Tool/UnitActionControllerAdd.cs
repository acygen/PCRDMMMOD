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
    [HarmonyPatch(typeof(UnitActionController), "StartAction")]
    class UnitActionControllerAdd
    {
        static void Postfix(UnitActionController __instance,ref bool __result, int skillId)
        {
            int pos = 0;
            try
            {
                UnitSkillExecData skillExecData = new UnitSkillExecData();
                Dictionary<int, Skill> skillDictionary = Traverse.Create(__instance).Property("skillDictionary").GetValue<Dictionary<int, Skill>>();
                pos = 10;
                Skill skill = skillDictionary[skillId];
                skillExecData.skillID = skillId;
                skillExecData.skillName = skill.SkillName;
                pos = 30;
                skillExecData.skillState = __result ? UnitSkillExecData.SkillState.NORMAL : UnitSkillExecData.SkillState.CANCEL;
                BattleManager battleManager = PCRSettings.staticBattleBanager;
                float startRemainTime = Traverse.Create(battleManager).Field("startRemainTime").GetValue<float>();
                int exectTime = UnityEngine.Mathf.RoundToInt((startRemainTime - battleManager.HIJKBOIEPFC) * 60);
                pos = 50;
                skillExecData.startTime = exectTime;
                skillExecData.startTimeReal = battleManager.JJCJONPDGIM;
                skillExecData.unitid = __instance.Owner.UnitId;
                pos = 60;
                skillExecData.UnitName = PCRSettings.Instance.GetUnitNameByID(skillExecData.unitid);
                pos = 70;
                PCRBattle.Instance.AppendStartSkill(skillExecData);
            }
            catch(System.Exception ex)
            {
                Cute.ClientLog.AccumulateClientLog("ERROR AT STARTACTION:[" + pos + "]" + ex.Message + ex.StackTrace);
            }

        }
        

    }
    [HarmonyPatch(typeof(UnitActionController), "ExecAction")]
    class UnitActionControllerAdd0
    {
        static void Postfix(UnitActionController __instance, ActionParameter action,
          Skill skill,
          BasePartsData target,
          int num,
          float starttime)
        {
            int pos = 0;
            try
            {               
                BattleManager battleManager = PCRSettings.staticBattleBanager;
                float startRemainTime = Traverse.Create(battleManager).Field("startRemainTime").GetValue<float>();
                int exectTime = UnityEngine.Mathf.RoundToInt((startRemainTime - battleManager.HIJKBOIEPFC) * 60);
                pos = 10;
                UnitActionExecData actionExecData = new UnitActionExecData();
                actionExecData.execTime = exectTime;
                actionExecData.execTimeReal = battleManager.JJCJONPDGIM;
                actionExecData.describe = "咕咕咕";
                pos = 20;
                actionExecData.actionID = action.ActionId;
                actionExecData.unitid = __instance.Owner.UnitId;
                pos = 30;
                actionExecData.actionType = action.ActionType.GetDescription();
                pos = 40;
                actionExecData.targetNames = new List<string> { PCRSettings.Instance.GetUnitNameByID(target.Owner.UnitId) };
                pos = 50;
                actionExecData.result = UnitActionExecData.ActionState.NORMAL;
                pos = 60;
                PCRBattle.Instance.AppendExecAction(__instance.Owner.UnitId, skill.SkillId, actionExecData);

            }
            catch (System.Exception ex)
            {
                Cute.ClientLog.AccumulateClientLog("ERROR AT STARTACTION:" + "[" + pos + "]" + ex.Message + ex.StackTrace);
            }

        }


    }

}
