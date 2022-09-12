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

namespace PCRCalculator.Hook
{
    [HarmonyPatch(typeof(BattleHeaderController), "SetRestTime")]
    class BattleTime
    {
        static void Postfix(BattleHeaderController __instance, float _restTime)
        {
            if (PCRSettings.showExectTime)
            {
                BattleManager battleManager = Traverse.Create(__instance).Field("battleManager").GetValue<FKOEBGHGCNP>() as BattleManager;

                float startRemainTime = Traverse.Create(battleManager).Field("startRemainTime").GetValue<float>();
                int num = (int)(_restTime / 60f);
                int num2 = (int)(_restTime - (float)(num * 60));
                string exectTime = "[" + UnityEngine.Mathf.RoundToInt((startRemainTime - _restTime) * 60) + "]";
                string exectTimeReal = "(ERROR!)";
                if (battleManager != null)
                {
                    exectTimeReal = "(" + battleManager.JJCJONPDGIM + ")";
                }
                string str = string.Format(eTextId.PROGRESS_TIME.Name(), num, num2);

                str = (PCRSettings.showExectTimeReal ? exectTimeReal : exectTime) + str;
                CustomUILabel timeLimit = Traverse.Create(__instance).Field("timeLimit").GetValue<CustomUILabel>();

                timeLimit.SetText(str);
            }
        }

    }
    [HarmonyPatch(typeof(BasePartsData), "SetBuffDebuff")]

    public class Buffdef0
    {
        static void Postfix(BasePartsData __instance,bool _enable, int _value, UnitCtrl.BuffParamKind _kind, UnitCtrl _source, MLEGMHAOCON _battleLog, bool _additional)
        {
            if (PCRSettings.showBossDEF)
            {
                if (__instance.Owner.IsBoss)
                {
                    BattleHeaderController instance = SingletonMonoBehaviour<BattleHeaderController>.Instance;
                    if (instance != null)
                    {
                        /*if (__instance.Owner.IsPartsBoss)
                        {
                            UpdateBossDEF(instance.BossGauge, __instance.Owner);
                        }
                        else*/
                            UpdateBossDEF( __instance.Owner);
                    }
                }
            }
            Battlelog.MyLog(null, Elements.eBattleLogType.SET_BUFF_DEBUFF, (int)_kind, _value, 0L, 0, 0,( _enable?1:0), 100, _source, __instance.Owner);
            if (__instance.Owner.IsBoss)
            {
                string des = $"来自{PCRBattle.Instance.GetName(_source?.UnitId ?? 0)}，值{_value}";
                PCRBattle.Instance.OnBossDEFChange(__instance.Owner, des);
            }
        }
        public static void UpdateBossDEF(UnitCtrl a)
        {
            int pos = 0;
            try
            {
                PartsBossGauge instance = SingletonMonoBehaviour<BattleHeaderController>.Instance.BossGauge;
                if (instance == null || a == null)
                    return;
                pos = 1000;
                string inputstr = "DEF:" + a.Def + "    MDEF:" + a.MagicDef + "    TP:" + a.Energy.ToString("F2");
                pos = 2000;
                if (a.IsPartsBoss)
                {
                    bool showMDEF = PCRSettings.mutiTargetShowMDEF;
                    inputstr = showMDEF ? "MDEF:" : "DEF:";
                    pos += 10;
                    foreach (PartsData part in a.BossPartsListForBattle)
                    {
                        inputstr += showMDEF ? $"{part.GetMagicDefZero()}  " : $"{part.GetDefZero()}  ";
                        pos += 100;
                    }
                    inputstr += $"TP:{a.Energy:F2}";
                }
                pos = 3000;
                CustomUILabel nameAndLevel = Traverse.Create(instance).Field("nameAndLevel").GetValue<CustomUILabel>();
                pos = 4000;
                nameAndLevel.SetText(inputstr);
            }
            catch(Exception eex)
            {
                Cute.ClientLog.AccumulateClientLog($"ERROR AT:{pos}" + eex.Message);
            }
        }
        public static void UpdateBossTP(UnitCtrl a)
        {
           //BattleHeaderController instance = SingletonMonoBehaviour<BattleHeaderController>.Instance;
            UpdateBossDEF(a);
        }

    }

    [HarmonyPatch(typeof(PartsData), "SetBuffDebuff")]

    public class Buffdef1
    {
        static void Postfix(PartsData __instance, bool _enable, int _value, UnitCtrl.BuffParamKind _kind, UnitCtrl _source, MLEGMHAOCON _battleLog, bool _additional)
        {            
            Battlelog.MyLog(null, Elements.eBattleLogType.SET_BUFF_DEBUFF, (int)_kind, _value, __instance.Index, 0, 0, (_enable ? 1 : 0), 200, _source, __instance.Owner);
            string des = $"来自{PCRBattle.Instance.GetName(_source?.UnitId ?? 0)}，值{_value}";
            PCRBattle.Instance.OnBossDEFChange(__instance.Owner,des,__instance.Index);
            Buffdef0.UpdateBossDEF( __instance.Owner);

        }
    }

    [HarmonyPatch(typeof(BattleManager), "OnDestroy")]
    class Battle00
    {
        static void Postfix(BattleManager __instance)
        {
            PCRSettings.staticBattleBanager = null;
        }

    }
    
    [HarmonyPatch(typeof(BattleManager), "Awake")]
    class Battle09
    {
        static void Postfix(BattleManager __instance)
        {
            PCRSettings.staticBattleBanager = __instance;
        }

    }
    [HarmonyPatch(typeof(BattleManager), "Update")]
    class Battle111
    {
        static bool Prefix(BattleManager __instance)
        {
            try
            {
                if (PCRBattle.Instance.saveData.useFastKey)
                {
                    int[] keys = PCRBattle.Instance.saveData.fastKeys;

                    for (int i = 0; i < 5; i++)
                    {
                        if (Input.GetKey((KeyCode)keys[i]))
                        {
                            if (__instance.OFMPGBKBOPM.UnitCtrls.Length > i)
                                __instance.OFMPGBKBOPM.UnitCtrls[i].IsUbExecTrying = true;
                        }
                    }
                    if (Input.GetKeyDown((KeyCode)keys[5]))
                    {
                        SingletonMonoBehaviour<BattleHeaderController>.Instance.OnClickPauseButton();
                    }
                    else if (Input.GetKeyDown((KeyCode)keys[6]))
                    {
                        Hook.CallVoidMethod(typeof(UnitUiCtrl), __instance.OFMPGBKBOPM, "onClickSkillModeBtn", new object[] { });
                        //__instance.OFMPGBKBOPM.SetAutoMode(!__instance.OFMPGBKBOPM.IsAutoMode);

                    }
                    else if (Input.GetKeyDown((KeyCode)keys[7]))
                    {
                        Hook.CallVoidMethod(typeof(UnitUiCtrl), __instance.OFMPGBKBOPM, "onClickSpeedBtn", new object[] { });
                    }


                }

                return !PCRSettings.DVGGWVBMH;
            }catch(System.Exception ex)
            {
                Cute.ClientLog.AccumulateClientLog("ERROR WHEN UPDATE AT BATTLEMANAGER:" + ex.Message);
                return true;
            }
        }

    }
    [HarmonyPatch(typeof(GJNIHENNINA), "AppendBattleLog")]
    public class Battlelog
    {
        static void Postfix(GJNIHENNINA __instance, Elements.eBattleLogType LIFIGNMLOLF, int HLIKLPNIOKJ, long KGNFLOPBOMB, long KDCBJHCMAOH, int FNGPHAODBAM, int OJHBHHCOAGK, int PFLDDMLAICG = 1, int PNJFIOPGCIC = 1, UnitCtrl JELADBAMFKH = null, UnitCtrl LIMEKPEENOB = null)
        {
            MyLog(__instance, LIFIGNMLOLF, HLIKLPNIOKJ, KGNFLOPBOMB, KDCBJHCMAOH, FNGPHAODBAM, OJHBHHCOAGK, PFLDDMLAICG, PNJFIOPGCIC, JELADBAMFKH, LIMEKPEENOB);
        }
        public static void MyLog(GJNIHENNINA __instance, Elements.eBattleLogType logtype, int type, long value1, long currentValue, int duration, int actionid, int vlue2 = 1, int value3 = 1, UnitCtrl source = null, UnitCtrl target = null)
        {
            BattleManager battleManager = PCRSettings.staticBattleBanager;
            //int waveCount = battleManager.GetCurrentWaveOffset() + battleManager.POKEAEBGPIB + 1;
            BattleLogData battleLogData = new BattleLogData();
            battleLogData.SetBattleLogType((int)logtype);
            battleLogData.SetSourceUnitId((!(source == null)) ? source.UnitId : 0);
            battleLogData.SetTargetUnitId((!(target == null)) ? target.UnitId : 0);
            battleLogData.SetTargetIsOwnUnit((!(target == null)) ? ((target.IsOther ? 200 : 100) + target.UnitInstanceId) : 0);
            battleLogData.SetType(type);
            battleLogData.SetCurrentValue(currentValue);
            battleLogData.SetValue1(value1);
            battleLogData.SetDuration(duration);
            battleLogData.SetFrame(battleManager.JJCJONPDGIM);
            battleLogData.SetSourceIsOwnUnit((!(source == null)) ? ((source.IsOther ? 200 : 100) + source.UnitInstanceId) : 0);
            battleLogData.SetActionId(actionid);
            battleLogData.SetWaveCount(1);
            battleLogData.SetValue2(vlue2);
            battleLogData.SetValue3(value3);
            float startRemainTime = Traverse.Create(battleManager).Field("startRemainTime").GetValue<float>();

            int exectTime = UnityEngine.Mathf.RoundToInt((startRemainTime - battleManager.HIJKBOIEPFC) * 60);
            PCRBattle.Instance.OnReceiveLog(battleLogData, exectTime);
        }
    }
    [HarmonyPatch(typeof(BattleManager), "appendWaveEndLog")]
    public class Battlelog00
    {
        static void Postfix(BattleManager __instance)
        {
            float startRemainTime = Traverse.Create(__instance).Field("startRemainTime").GetValue<float>();
            int exectTime = UnityEngine.Mathf.RoundToInt((startRemainTime - __instance.HIJKBOIEPFC) * 60);
            PCRBattle.Instance.OnBattleFinish(exectTime, __instance.JJCJONPDGIM);
        }
    }

}
