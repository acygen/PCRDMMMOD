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
    [HarmonyPatch(typeof(UnitCtrl), "Initialize")]
    public class UnitCtrlAdd
    {
        public static void Postfix(UnitCtrl __instance, UnitParameter _data, bool _isOther, bool _isFirstWave, bool _isGaugeAlwaysVisible = false)
        {
            if (PCRBattle.Instance.saveData.useUBSet)
            {
                Elements.Battle.BattleManager battleManager = Traverse.Create(__instance).Field("staticBattleManager").GetValue<BattleManager>();
                if(battleManager.JILIICMHLCH != eBattleCategory.CLAN_BATTLE)
                {
                    return;
                }
                int idx = battleManager.DJJKGCFKJNJ.FindIndex(a => a == __instance);
                if(idx>=0&&idx<=battleManager.DJJKGCFKJNJ.Count)
                {
                    var ub = PCRBattle.Instance.GetUbTimes(idx);
                    SetUBExecTime_new(__instance, ub.Item1, PCRBattle.Instance.saveData.ubTryCount, ub.Item2);
                    //SetUBExecTime(__instance, ub.Item1, PCRBattle.Instance.saveData.ubTryCount,ub.Item2);
                }
                else
                {
                    Cute.ClientLog.AccumulateClientLog("错误！未找到对应角色" + _data.MasterData.UnitName);
                }
            }
            if(__instance.IsBoss && PCRSettings.showBossDEF)
            {

                __instance.EnergyChange += (a) => {
                    Buffdef0.UpdateBossTP(a);        
                };
            }
            PCRBattle.Instance.OnUnitTPChange(__instance);
            __instance.EnergyChange += (a) => {
                PCRBattle.Instance.OnUnitTPChange(a);
            };
            PCRBattle.Instance.OnUnitHPChange(__instance.UnitId, __instance.IsOther,(int)__instance.Hp);
            if (__instance.IsPartsBoss)
            {
                foreach(var parts in __instance.BossPartsListForBattle)
                {
                    PCRBattle.Instance.OnBossDEFChange(__instance, parts.Index);
                }
            }
            else
            {
                PCRBattle.Instance.OnBossDEFChange(__instance, 0);
            }
        }
        public static void SetUBExecTime(UnitCtrl __instance,List<float> times, int tryCount,bool islogic = true)
        {
            if (__instance.IsBoss || __instance.IsOther)
                return;
            List<float> times2 = new List<float>();
            for (int i = 0; i < times.Count; i++)
            {
                if (times[i] > 91)
                {
                    times2.Add(90 - times[i] / 60.0f);
                }
                else
                {
                    times2.Add(times[i]);
                }
            }
            if (islogic)
                __instance.AppendCoroutine(UpdateUBExecTime(__instance, times2, tryCount), ePauseType.SYSTEM);
            else
                __instance.AppendCoroutine(UpdateUBExecTimeRealFrame(__instance, times, tryCount), ePauseType.SYSTEM);
        }
        public static void SetUBExecTime_new(UnitCtrl __instance, List<float> times, int tryCount, bool islogic = true)
        {
            if (__instance.IsBoss || __instance.IsOther)
                return;
            List<UBTimeData> times2 = new List<UBTimeData>();
            for (int i = 0; i < times.Count; i++)
            {
                int ubTime = (int)times[i];
                /*if (times[i] > 91)
                {
                    ubTime = (int)(90 - times[i] / 60.0f);
                }
                else
                {
                    ubTime = (int)times[i];
                }*/
                int priority = (int)(10.0f * times[i]) - 10 * ubTime;
                UBTimeData timeData = new UBTimeData(ubTime, priority, __instance);
                times2.Add(timeData);
            }
            PCRBattle.Instance.AddUBTimes(times2,islogic);
        }
        public static IEnumerator UpdateUBExecTime(UnitCtrl __instance, List<float> times, int tryCount)
        {
            int idx = 0;
            int count = tryCount;
            if (times.Count <= 0)
            {
                yield break;
            }
            yield return null;
            yield return null;
            Elements.Battle.BattleManager battleManager = Traverse.Create(__instance).Field("staticBattleManager").GetValue<BattleManager>();
            while (true)
            {
                if (battleManager.HIJKBOIEPFC > times[idx])
                {
                    yield return null;
                    continue;
                }
                __instance.IsUbExecTrying = true;
                count--;
                if (count <= 0)
                {
                    idx++;
                    count = tryCount;
                    if (idx >= times.Count)
                    {
                        yield break;
                    }
                }
                yield return null;
            }
        }
        public static IEnumerator UpdateUBExecTimeRealFrame(UnitCtrl __instance, List<float> times, int tryCount)
        {
            int idx = 0;
            int count = tryCount;
            if (times.Count <= 0)
            {
                yield break;
            }
            yield return null;
            yield return null;
            Elements.Battle.BattleManager battleManager = Traverse.Create(__instance).Field("staticBattleManager").GetValue<BattleManager>();
            while (true)
            {
                if (battleManager.JJCJONPDGIM < times[idx])
                {
                    yield return null;
                    continue;
                }
                __instance.IsUbExecTrying = true;
                count--;
                if (count <= 0)
                {
                    idx++;
                    count = tryCount;
                    if (idx >= times.Count)
                    {
                        yield break;
                    }
                }
                yield return null;
            }
        }
        public static IEnumerator UpdateUBExecTime_new(List<UBTimeData> times,bool isreal, int tryCount)
        {
            //int realTimeCount = 0;
            yield return null;
            if (times.Count <= 0)
            {
                yield break;
            }
            yield return null;
            yield return null;
            //int lastFrame = 0;
            //int priority = 0;
            Elements.Battle.BattleManager battleManager = Traverse.Create(times[0].unit).Field("staticBattleManager").GetValue<BattleManager>();

            float startRemainTime = Traverse.Create(battleManager).Field("startRemainTime").GetValue<float>();
            while (true)
            {
                int currentLogicFrame = (int)((startRemainTime - battleManager.HIJKBOIEPFC)*60.0f);
                int currentRealFrame = battleManager.JJCJONPDGIM;
                int currentFrame = isreal ? currentRealFrame : currentLogicFrame;
                Label0:
                if (times.Count <= 0)
                    yield break;
                UBTimeData date = times[0];
                if (date.ubTime <= currentFrame)
                {
                    if (date.execCount <= tryCount)
                    {
                        //if (isreal || date.prioruty <= priority)
                        //{
                        date.unit.IsUbExecTrying = true;
                        Cute.ClientLog.AccumulateClientLog($"[{currentLogicFrame}/{currentRealFrame}]角色{date.unit.UnitId}尝试UB ({date.execCount}/{tryCount})");

                        if (battleManager.GetBlackoutUnitTargetLength() <= 0)
                        {
                            date.execCount++;
                        }
                        else
                        {
                            if (battleManager.GetBlackoutUnitTarget(0).UnitId == date.unit.UnitId)
                            {
                                Cute.ClientLog.AccumulateClientLog($"[{currentLogicFrame}/{currentRealFrame}]检测到角色{date.unit.UnitId}正在UB，跳转下一个目标");
                                times.RemoveAt(0);
                                goto Label0;
                            }
                            //if (PCRBattle.Instance.IsUBExecd(date.unit.UnitId, currentFrame))
                            if (date.unit.CurrentState == UnitCtrl.ActionState.SKILL_1)
                            {
                                //priority++;
                                Cute.ClientLog.AccumulateClientLog($"[{currentLogicFrame}/{currentRealFrame}]检测到角色{date.unit.UnitId}处于UB状态，跳转下一个目标");
                                date.execCount += 9999;
                                times.RemoveAt(0);
                                goto Label0;

                            }
                        }
                        //}

                    }
                    else
                    {
                        Cute.ClientLog.AccumulateClientLog($"[{currentLogicFrame}/{currentRealFrame}]角色{date.unit.UnitId}UB尝试次数已满，跳转下一个目标");
                        times.RemoveAt(0);
                        goto Label0;

                    }
                }
                
                /*if (currentFrame != lastFrame)
                {
                    //realTimeCount = 0;
                    lastFrame = currentFrame;
                    priority = 0;
                }
                foreach (var date in times)
                {
                    if(date.ubTime<=currentFrame && date.execCount <= tryCount)
                    {
                        if (isreal || date.prioruty <= priority)
                        {
                            date.unit.IsUbExecTrying = true;
                            if (battleManager.GetBlackoutUnitTargetLength() <= 0)
                                date.execCount++;
                            if (isreal || PCRBattle.Instance.IsUBExecd(date.unit.UnitId, currentFrame))
                            {
                                priority++;
                                date.execCount += 9999;
                            }
                        }
                    }
                }
                //realTimeCount++;*/
                yield return null;
            }
        }

        public class UBTimeData
        {
            public int ubTime;
            public int prioruty;
            public UnitCtrl unit;
            public int execCount;

            public UBTimeData(int ubTime, int prioruty, UnitCtrl unit)
            {
                this.ubTime = ubTime;
                this.prioruty = prioruty;
                this.unit = unit;
            }

        }
    }
    [HarmonyPatch(typeof(UnitCtrl), "switchAbnormalState")]
    public class UnitCtrlAdd00
    {
        static bool Prefix(UnitCtrl __instance, UnitCtrl.eAbnormalState abnormalState,
          AbnormalStateEffectPrefabData _specialEffectData)
        {
            UnitCtrlAdd0.Switch_On = true;
            return true;
        }

    }

    [HarmonyPatch(typeof(UnitCtrl), "EnableAbnormalState")]
    public class UnitCtrlAdd0
    {
        public static bool Switch_On = false;
        static void Postfix(UnitCtrl __instance, UnitCtrl.eAbnormalState _abnormalState,
          bool _enable,
          bool _reduceEnergy = false,
          bool _switch = false)
        {
            var abnormalStateCategory = UnitCtrl.GetAbnormalStateCategory(_abnormalState);
            Dictionary<UnitCtrl.eAbnormalStateCategory, AbnormalStateCategoryData> abnormalStateCategoryDataDictionary =
                Traverse.Create(__instance).Field("abnormalStateCategoryDataDictionary").GetValue<Dictionary<UnitCtrl.eAbnormalStateCategory, AbnormalStateCategoryData>>();
            bool _switch_On = _enable && Switch_On;
            CallBackAbnormalStateChanged(__instance,abnormalStateCategoryDataDictionary[abnormalStateCategory], _switch_On);
            Switch_On = false;
        }
        static void CallBackAbnormalStateChanged(UnitCtrl __instance,AbnormalStateCategoryData data, bool waitForFrame = false)
        {
            Action action = () =>
            {
                int pos = 0;
                try
                {
                    UnitAbnormalStateChangeData stateChangeData = new UnitAbnormalStateChangeData();
                    if (data != null)
                    {
                        pos = 10;
                        stateChangeData.AbsorberValue = data.AbsorberValue;
                        stateChangeData.ActionId = data.ActionId;
                        stateChangeData.CurrentAbnormalState = data.CurrentAbnormalState;
                        stateChangeData.Duration = data.Duration;
                        stateChangeData.enable = data.enable;
                        stateChangeData.EnergyReduceRate = data.EnergyReduceRate;
                        stateChangeData.IsDamageRelease = data.IsDamageRelease;
                        stateChangeData.IsEnergyReduceMode = data.IsEnergyReduceMode;
                        stateChangeData.IsReleasedByDamage = data.IsReleasedByDamage;
                        stateChangeData.MainValue = data.MainValue;
                        stateChangeData.SubValue = data.SubValue;
                        string skillName = data.Skill!=null ? data.Skill.SkillName : "NULL";
                        stateChangeData.SkillName = skillName;
                        pos = 20;
                        stateChangeData.SourceName = data.Source != null ? PCRSettings.Instance.GetUnitNameByID(data.Source.UnitId) : "System";
                    }
                    BattleManager battleManager = PCRSettings.staticBattleBanager;
                    float startRemainTime = Traverse.Create(battleManager).Field("startRemainTime").GetValue<float>();
                    int exectTime = UnityEngine.Mathf.RoundToInt((startRemainTime - battleManager.HIJKBOIEPFC) * 60);
                    int currentFrame = exectTime;

                    if (waitForFrame)
                        currentFrame--;
                    pos = 30;
                    PCRBattle.Instance.OnAbnormalStateChange(__instance.UnitId, stateChangeData, currentFrame, battleManager.JJCJONPDGIM);
                }
                catch(System.Exception exp)
                {
                    Cute.ClientLog.AccumulateClientLog($"异常状态hook函数出错({pos})：" + exp.Message);
                }
                };
            if (waitForFrame)
                __instance.AppendCoroutine(WaitForFrame(action), ePauseType.SYSTEM, __instance);
            else
                action();
        }
        static IEnumerator WaitForFrame(System.Action action)
        {
            yield return null;
            action?.Invoke();
        }

    }
    [HarmonyPatch(typeof(UnitCtrl), "EnableBuffParam")]
    public class UnitCtrlAdd1
    {
        static void Postfix(UnitCtrl __instance, UnitCtrl.BuffParamKind _kind,
          Dictionary<BasePartsData, int> _value,
          bool _enable,
          UnitCtrl _source,
          bool _isBuff,
          bool _additional)
        {
            string des = "";
            int MainValue = 0;
            foreach (int value in _value.Values)
            {
                des += value;
                MainValue = value;
            }
            UnitAbnormalStateChangeData stateChangeData = new UnitAbnormalStateChangeData();
            //stateChangeData.AbsorberValue = data.AbsorberValue;
            //stateChangeData.ActionId = data.ActionId;
            //stateChangeData.CurrentAbnormalState = data.CurrentAbnormalState;
            //stateChangeData.Duration = data.Duration;
            stateChangeData.enable = _enable;
            //stateChangeData.EnergyReduceRate = data.EnergyReduceRate;
            //stateChangeData.IsDamageRelease = data.IsDamageRelease;
            //stateChangeData.IsEnergyReduceMode = data.IsEnergyReduceMode;
            //stateChangeData.IsReleasedByDamage = data.IsReleasedByDamage;
            stateChangeData.MainValue = MainValue;
            //stateChangeData.SubValue = data.SubValue;
            //stateChangeData.SkillName = data.Skill.SkillName;
            //stateChangeData.SourceName = data.Source.UnitName;
            stateChangeData.isBuff = true;
            stateChangeData.BUFF_Type = (int)_kind;
            BattleManager battleManager = PCRSettings.staticBattleBanager;
            float startRemainTime = Traverse.Create(battleManager).Field("startRemainTime").GetValue<float>();
            int exectTime = UnityEngine.Mathf.RoundToInt((startRemainTime - battleManager.HIJKBOIEPFC) * 60);

            PCRBattle.Instance.OnAbnormalStateChange(__instance.UnitId, stateChangeData, exectTime,battleManager.JJCJONPDGIM);
        }
        

    }
    [HarmonyPatch(typeof(UnitCtrl), "SetDamageImpl")]
    public class UnitCtrlAdd05
    {
        static void Postfix(UnitCtrl __instance, ref long __result, Elements.DamageData _damageData, bool _byAttack, ActionParameter.OnDamageHitDelegate _onDamageHit, bool _hasEffect, Skill _skill, bool _energyAdd, bool _critical, Action _onDefeat, bool _noMotion, Func<int, float, int> _upperLimitFunc, float _energyChargeMultiple)
        {
            BattleManager battleManager = PCRSettings.staticBattleBanager;
            float startRemainTime = Traverse.Create(battleManager).Field("startRemainTime").GetValue<float>();
            int exectTime = UnityEngine.Mathf.RoundToInt((startRemainTime - battleManager.HIJKBOIEPFC) * 60);
            int skillid = _skill == null ? 0 : _skill.SkillId;
            PlayerDamageData damageData = new PlayerDamageData(exectTime, battleManager.JJCJONPDGIM, __result, _critical, _damageData.CriticalRate, 2f * _damageData.CriticalDamageRate, skillid);
            int sourceid = _damageData.Source == null ? 0 : _damageData.Source.UnitId;
            /*if (__instance.IsAbnormalState(Elements.UnitCtrl.eAbnormalState.LOG_ALL_BARRIR))
            {

            }*/
                PCRBattle.Instance.OnReceiveDamage(__instance.UnitId, sourceid , damageData);

        }

    }
}
