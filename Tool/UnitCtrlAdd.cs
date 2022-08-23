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
                if (!(__instance.IsOther || __instance.IsBoss))
                {
                    int idx = battleManager.DJJKGCFKJNJ.FindIndex(a => a == __instance);
                    if (idx >= 0 && idx <= battleManager.DJJKGCFKJNJ.Count)
                    {
                        var ub = PCRBattle.Instance.GetUbTimes(idx);
                        SetUBExecTime_new(__instance, ub.Item1, PCRBattle.Instance.saveData.ubTryCount, ub.Item2);
                        //SetUBExecTime(__instance, ub.Item1, PCRBattle.Instance.saveData.ubTryCount,ub.Item2);
                    }
                    else
                    {
                        Cute.ClientLog.AccumulateClientLog("错误！未找到对应角色" + _data.MasterData.UnitName + "idx" + idx);
                    }
                }
            }
            if(__instance.IsBoss && PCRSettings.showBossDEF)
            {

                __instance.EnergyChange += (a) => {
                    Buffdef0.UpdateBossTP(a);        
                };
            }
            //PCRBattle.Instance.OnUnitTPChange(__instance.UnitId,__instance.Energy,"初始化");
            /*__instance.EnergyChange += (a) => {
                PCRBattle.Instance.OnUnitTPChange(a);
            };*/
            //PCRBattle.Instance.OnUnitHPChange(__instance.UnitId, __instance.IsOther,(int)__instance.Hp,"初始化");
            if (__instance.IsPartsBoss)
            {
                foreach(var parts in __instance.BossPartsListForBattle)
                {
                    PCRBattle.Instance.OnBossDEFChange(__instance, "初始化", parts.Index);
                }
            }
            else if(__instance.IsBoss)
            {
                PCRBattle.Instance.OnBossDEFChange(__instance, "初始化", 0);
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
                int priority = Mathf.RoundToInt(10.0f * times[i]) - 10 * ubTime;
                UBTimeData timeData = new UBTimeData(ubTime, priority, __instance);
                timeData.waitBOSSUB = times[i] * 10 - Mathf.RoundToInt(times[i] * 10) > 0.01f;
                if (timeData.waitBOSSUB)
                {
                    UnityEngine.Debug.Log($"WAIT BOSS UB:{__instance.UnitId}-{times[i]}-{times[i] * 10}-{(int)(times[i] * 10)}");

                }
                //Cute.ClientLog.AccumulateClientLog($"ADD UB TIME:{__instance.UnitId}-{times[i]}-{ubTime}-{priority}");

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
        public static IEnumerator UpdateUBExecTime_new(bool isreal, int tryCount,Action action)
        {
            //int realTimeCount = 0;
            yield return null;

            if (PCRBattle.Instance.UBTimeDatas.Count <= 0)
            {
                yield break;
            }
            yield return null;
            yield return null;
            //int lastFrame = 0;
            //int priority = 0;
            string logFull = Newtonsoft0.Json.JsonConvert.SerializeObject(PCRBattle.Instance.UBTimeDatas);
            //Cute.ClientLog.AccumulateClientLog($"UBTimeDataList:\n{logFull}\n\n");
            Elements.Battle.BattleManager battleManager = Traverse.Create(PCRBattle.Instance.UBTimeDatas[0].unit).Field("staticBattleManager").GetValue<BattleManager>();

            float startRemainTime = Traverse.Create(battleManager).Field("startRemainTime").GetValue<float>();
            bool waitFlag = true;
            while (true)
            {
                int currentLogicFrame = (int)((startRemainTime - battleManager.HIJKBOIEPFC)*60.0f);
                int currentRealFrame = battleManager.JJCJONPDGIM;
                int currentFrame = isreal ? currentRealFrame : currentLogicFrame;
            Label0:
                if (PCRBattle.Instance.UBTimeDatas.Count <= 0)
                {
                    action?.Invoke();
                    yield break;
                }
                if (!PCRBattle.Instance.UseUBTimeDatas)
                {
                    yield return null;
                    continue;
                }
                UBTimeData date = PCRBattle.Instance.UBTimeDatas[0];
                if (date.ubTime <= currentFrame)
                {
                    if (date.execCount <= tryCount)
                    {
                        //if (isreal || date.prioruty <= priority)
                        //{
                        if (!isreal && date.waitBOSSUB)
                        {
                            if (!PCRBattle.Instance.IsBossUBExecd(date.ubTime))
                            {
                                //UnityEngine.Debug.Log($"[{currentLogicFrame}/{currentRealFrame}]角色{date.unit.UnitId}等待BOSSUB ({date.execCount}/{tryCount})");
                                yield return null;
                                continue;
                            }
                            //Cute.ClientLog.AccumulateClientLog($"[{currentLogicFrame}/{currentRealFrame}]角色{date.unit.UnitId}等待BOSSUB ({date.execCount}/{tryCount})");
                            date.waitBOSSUB = false;
                        }



                        date.unit.IsUbExecTrying = true;
                        //UnityEngine.Debug.Log($"[{currentLogicFrame}/{currentRealFrame}]角色{date.unit.UnitId}尝试UB ({date.execCount}/{tryCount})");

                        if (battleManager.GetBlackoutUnitTargetLength() <= 0)
                        {
                            date.execCount++;
                        }
                        else
                        {
                            /*if(waitFlag)
                            {
                                waitFlag = false;
                                yield return null;
                                continue;
                            }
                            else
                            {
                                waitFlag = true;
                            }*/
                            /*if (battleManager.GetBlackoutUnitTarget(0).UnitId == date.unit.UnitId)
                            {
                                Cute.ClientLog.AccumulateClientLog($"[{currentLogicFrame}/{currentRealFrame}]检测到角色{date.unit.UnitId}正在UB，跳转下一个目标");
                                PCRBattle.Instance.UBTimeDatas.RemoveAt(0);
                                action?.Invoke();
                                goto Label0;
                            }*/
                            //if (PCRBattle.Instance.IsUBExecd(date.unit.UnitId, currentFrame))
                            if (date.unit.CurrentState == UnitCtrl.ActionState.SKILL_1)
                            {
                                //priority++;
                                Cute.ClientLog.AccumulateClientLog($"[{currentLogicFrame}/{currentRealFrame}]检测到角色{date.unit.UnitId}处于UB状态，跳转下一个目标");
                                date.execCount += 9999;
                                PCRBattle.Instance.UBTimeDatas.RemoveAt(0);
                                action?.Invoke();
                                goto Label0;

                            }
                        }
                        //}

                    }
                    else
                    {
                        Cute.ClientLog.AccumulateClientLog($"[{currentLogicFrame}/{currentRealFrame}]角色{date.unit.UnitId}UB尝试次数已满，跳转下一个目标");
                        PCRBattle.Instance.UBTimeDatas.RemoveAt(0);
                        action?.Invoke();
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
            public int unitid;
            [Newtonsoft0.Json.JsonIgnore]
            public UnitCtrl unit;
            public int execCount;
            public bool waitBOSSUB;
            public UBTimeData(int ubTime, int prioruty, UnitCtrl unit)
            {
                this.ubTime = ubTime;
                this.prioruty = prioruty;
                this.unit = unit;
                this.unitid = unit.UnitId;
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
    [HarmonyPatch(typeof(UnitCtrl), "createHealNumEffect")]
    public class UnitCtrlAdd0099
    {
        static bool Prefix(UnitCtrl __instance, int _value, BasePartsData _targetParts)
        {            
            return !PCRSettings.Instance.battleSetting.showUI;
        }

    }
    [HarmonyPatch(typeof(UnitCtrl), "setStateSkill1")]
    public class UnitCtrlAdd07
    {
        static bool Prefix(UnitCtrl __instance)
        {
            if (__instance.IsBoss)
            {
                BattleManager battleManager = PCRSettings.staticBattleBanager;
                float startRemainTime = Traverse.Create(battleManager).Field("startRemainTime").GetValue<float>();
                int exectTime = UnityEngine.Mathf.RoundToInt((startRemainTime - battleManager.HIJKBOIEPFC) * 60);
                PCRBattle.Instance.EnemyLastUBFrame = exectTime;
            }
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
    public class UnitCtrlAdd050
    {
        public static int actionid = 0;
        public static void Postfix(UnitCtrl __instance,ref long __result, Elements.DamageData _damageData,bool _critical, Skill _skill)
        {
            //UnityEngine.Debug.Log($"aaa");
            int pos = 0;
            try
            {
                BattleManager battleManager = PCRSettings.staticBattleBanager;
                float startRemainTime = Traverse.Create(battleManager).Field("startRemainTime").GetValue<float>();
                int exectTime = UnityEngine.Mathf.RoundToInt((startRemainTime - battleManager.HIJKBOIEPFC) * 60);
                int skillid = _skill == null ? 0 : _skill.SkillId;
                pos = 100;
                PlayerDamageData damageData = new PlayerDamageData(exectTime, battleManager.JJCJONPDGIM, __result, _critical, _damageData.CriticalRate, 2f * _damageData.CriticalDamageRate, skillid);
                int sourceid = 0;
                if(_damageData.Source != null)
                {
                    if (_damageData.Source.IsSummonOrPhantom || _damageData.Source.IsDivision)
                    {
                        sourceid = _damageData.Source.SummonSource?.UnitId ?? 0;
                    }
                    else
                    {
                        sourceid = _damageData.Source.UnitId;
                    }
                }
                if (_skill != null)
                {
                    //Cute.ClientLog.AccumulateClientLog($"{_skill.SkillName}");
                    ActionParameter action = _skill.ActionParameters.Find(a => a.ActionId == actionid);
                    if (action != null)
                    {
                        if(action.HitOnceDic.TryGetValue(_damageData.Target,out var value) && !value)
                        {
                            var criticalList = action.CriticalDataDictionary[_damageData.Target];
                            float num2 = 0;
                            //string debugSTR = "";
                            foreach(var c in criticalList)
                            {
                                float criRateFx = Mathf.Min(1, Math.Max(0, _damageData.CriticalRate));
                                float criDamRateFx = Mathf.Max(1, _damageData.CriticalDamageRate*2f);
                                float num3 = c.ExpectedDamageNotCritical * (1 + criRateFx * (criDamRateFx - 1));
                                num2 += Mathf.Min (num3, 999999);
                                //debugSTR += $"-{c.ExpectedDamage}/{c.ExpectedDamageNotCritical}/{criRateFx}/{criDamRateFx}\n";
                            }
                            UnitCtrl tar = _damageData.Target.Owner;
                            if (tar.IsAbnormalState(UnitCtrl.eAbnormalState.LOG_ALL_BARRIR))
                            {
                                float abnormalStateSubValue3 = tar.GetAbnormalStateSubValue(UnitCtrl.eAbnormalStateCategory.LOG_ALL_BARRIR);
                                if (num2 > abnormalStateSubValue3)
                                {
                                    float abnormalStateMainValue3 = tar.GetAbnormalStateMainValue(UnitCtrl.eAbnormalStateCategory.LOG_ALL_BARRIR);
                                    float num9 = (Mathf.Log((num2 - abnormalStateSubValue3) / abnormalStateMainValue3 + 1f) * abnormalStateMainValue3 + abnormalStateSubValue3) / num2;
                                    num2 *= num9;
                                }
                            }
                            //Cute.ClientLog.AccumulateClientLog($"期望伤害{num2}-{criticalList.Count}\n{debugSTR}");
                            damageData.exceptDamageForLogBarrier = (int)num2;
                        }
                    }
                }
                pos = 200;
                PCRBattle.Instance.OnReceiveDamage(__instance.UnitId, sourceid, damageData);
                pos = 300;
            }
            catch (Exception ex)
            {

                Cute.ClientLog.AccumulateClientLog($"[{pos}]{ex.ToString()}");
            }
            
        }

    }
    [HarmonyPatch(typeof(UnitCtrl), "SetDamage")]
    public class UnitCtrlAdd06
    {
        static bool Prefix(UnitCtrl __instance, Elements.DamageData _damageData, bool _byAttack, int _actionId, ActionParameter.OnDamageHitDelegate _onDamageHit = null, bool _hasEffect = true, Skill _skill = null, bool _energyAdd = true, Action _onDefeat = null, bool _noMotion = false, float _damageWeight = 1f, float _damageWeightSum = 1f, Func<int, float, int> _upperLimitFunc = null, float _energyChargeMultiple = 1f)
        {
            UnitCtrlAdd050.actionid = _actionId;
            return true;
        }

    }
}
