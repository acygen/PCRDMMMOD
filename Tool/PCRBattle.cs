using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cute;
using Newtonsoft0.Json;
using System.Drawing;
using Elements;

namespace PCRCalculator.Tool
{
    public class PCRBattle
    {
        private static PCRBattle instance;

        public static PCRBattle Instance
        {
            get
            {
                if (instance == null)
                    CreateInstance();
                return instance;
            }
        }
        public static void CreateInstance()
        {
            if (instance == null)
            {
                instance = new PCRBattle();
                instance.Init();
            }
        }
        public BattleSaveData saveData;
        public UI.BattleSettingWindow settingWindow;
        UI.BattleResultUI resultPage;
        public ClanBattleFinishReceiveData clanFinishData;
        public Dictionary<int, bool> unitShowLogDic = new Dictionary<int, bool>();
        public List<LogData> ReceivedLogList = new List<LogData>();
        public float battleTotalTime = 90;
        public static string[] stateNames = new string[] { "等待", "普攻", "UB", "技能", "走路", "无法行动", "召唤", "死亡", "开局", "失败", "???" };

        public static Color[] StateColors = new Color[14]
{
            Color.FromArgb(145,178,178,178),
            Color.FromArgb(157,255,134,134),
            Color.FromArgb(255,255,173,95),
            Color.FromArgb(134,151,168,255),
            Color.FromArgb(148,190,190,190),
            Color.FromArgb(163,185,120,100),
            Color.FromArgb(161,184,167,255),
            Color.FromArgb(139,135,135,135),
            Color.FromArgb(161,172,255,167),
            Color.FromArgb(161,255,243,167),
            Color.FromArgb(161,255,243,167),
            Color.FromArgb(161,255,243,167),
            Color.FromArgb(161,255,243,167),
            Color.FromArgb(161,255,243,167),
};
        public static Color[] BuffColors = new Color[14]
{
            Color.FromArgb(179,255,114,28),
            Color.FromArgb(183,255,180,28),
            Color.FromArgb(183,255,144,69),
            Color.FromArgb(186,235,66,255),
            Color.FromArgb(181,235,83,255),
            Color.FromArgb(255,156,156,156),
            Color.FromArgb(184,255,45,45),
            Color.FromArgb(181,255,45,182),
            Color.FromArgb(190,45,255,245),
            Color.FromArgb(188,0,255,87),
            Color.FromArgb(190,143,255,37),
            Color.FromArgb(177,255,52,0),
            Color.FromArgb(186,255,0,140),
            Color.FromArgb(86,51,0,255),
};
        public static Color[] AbnormalColors = new Color[14]
{
            Color.FromArgb(179,28,114,255),
            Color.FromArgb(183,28,180,255),
            Color.FromArgb(183,69,144,255),
            Color.FromArgb(186,255,66,235),
            Color.FromArgb(181,255,83,135),
            Color.FromArgb(255,156,156,156),
            Color.FromArgb(184,45,45,255),
            Color.FromArgb(181,182,45,255),
            Color.FromArgb(190,245,255,45),
            Color.FromArgb(188,87,255,0),
            Color.FromArgb(190,37,255,143),
            Color.FromArgb(177,0,52,255),
            Color.FromArgb(186,140,0,255),
            Color.FromArgb(86,255,0,51),
};

        public Dictionary<int, List<UnitStateChangeData>> allUnitStateChangeDic = new Dictionary<int, List<UnitStateChangeData>>();
        public Dictionary<int, List<UnitAbnormalStateChangeData>> allUnitAbnormalStateDic = new Dictionary<int, List<UnitAbnormalStateChangeData>>();
        private Dictionary<int, UnitStateChangeData> allUnitLastStateDic = new Dictionary<int, UnitStateChangeData>();
        public Dictionary<int, List<UnitStateInputData>> allUnitStateInputDic = new Dictionary<int, List<UnitStateInputData>>();
        public Dictionary<int, List<UnitSkillExecData>> allUnitSkillExecDic = new Dictionary<int, List<UnitSkillExecData>>();
        public Dictionary<int, List<UnitAbnormalStateChangeData>> allUnitAbnormalStateInputDic = new Dictionary<int, List<UnitAbnormalStateChangeData>>();
        public Dictionary<int, List<ValueChangeData>> allUnitHPDic = new Dictionary<int, List<ValueChangeData>>();
        public Dictionary<int, List<ValueChangeData>> allUnitTPDic = new Dictionary<int, List<ValueChangeData>>();
        public Dictionary<int, List<PlayerDamageData>> playerUnitDamageDic = new Dictionary<int, List<PlayerDamageData>>();
        public List<ValueChangeData> bossDefChangeDic = new List<ValueChangeData>();
        public List<ValueChangeData> bossMgcDefChangeDic = new List<ValueChangeData>();
        public List<DamageGetData> clanBattleDamageList = new List<DamageGetData>();
        public List<List<float>> ubTimes = new List<List<float>>();
        public List<List<float>> ubTimesReal = new List<List<float>>();
        public Dictionary<int, Dictionary<int, int>> allUnitSkillExecTimeDic = new Dictionary<int, Dictionary<int, int>>();
        public long totalDamage = 0;
        public long totalDamageCriEX = 0;
        public long totalDamageExcept = 0;
        public int RandomSeed;
        public List<int> PlayerIds = new List<int>();
        public int enemyUnitid;
        public string enemyDes = "??";
        public List<Hook.UnitCtrlAdd.UBTimeData> uBTimeDatas = new List<Hook.UnitCtrlAdd.UBTimeData>();
        public bool ubTimesInited = false;
        public bool ubTimesIsLogic = true;

        public bool isJJCBattle = false;
        public void Init()
        {
            saveData = PCRSettings.Instance.GetDataFromFile<BattleSaveData>("battleSaveData", true);
            if(saveData == null)
            {
                saveData = new BattleSaveData();
                Save();
            }
            PCRSettings.Instance.OtherToolButton = () => OpenSetWindow();
        }
        public void Save()
        {
            PCRSettings.Instance.SaveDataToFile("battleSaveData", saveData);
        }
        public void OpenSetWindow()
        {
            settingWindow = new UI.BattleSettingWindow();

            settingWindow.Show();

        }
        public void OnQiutGame()
        {
            settingWindow?.Close();
            resultPage?.Close();
        }
        public (List<float>,bool) GetUbTimes(int idx)
        {
            if (saveData.selectIndex < 0 || saveData.selectIndex >= saveData.timelineDatas.Count)
                return (new List<float>(),true);
            return (saveData.timelineDatas[saveData.selectIndex].playerGroupData.UBExecTimeData[idx],
                saveData.timelineDatas[saveData.selectIndex].timeType == 0);
        }
        public void SetUbTimes(string name,bool isLogic)
        {
            GuildTimelineData timelineData = new GuildTimelineData(1);
            timelineData.playerGroupData.UBExecTimeData = isLogic? ubTimes:ubTimesReal;
            timelineData.playerGroupData.SortUbTimes();
            timelineData.timeType = isLogic ? 0 : 1;
            timelineData.timeLineName = name;
            saveData.timelineDatas.Add(timelineData);
            Save();
        }
        public void OnBattleStart(int seed,float totaltime = 90, bool isJJC = false,string bossDes="??")
        {
            ReceivedLogList.Clear();
            unitShowLogDic.Clear();
            this.battleTotalTime = totaltime;
            allUnitStateChangeDic.Clear();
            allUnitAbnormalStateDic.Clear();
            allUnitLastStateDic.Clear();
            allUnitStateInputDic.Clear();
            allUnitSkillExecDic.Clear();
            allUnitAbnormalStateInputDic.Clear();
            allUnitHPDic.Clear();
            allUnitTPDic.Clear();
            playerUnitDamageDic.Clear();
            bossDefChangeDic.Clear();
            bossMgcDefChangeDic.Clear();
            clanBattleDamageList.Clear();
            totalDamage = 0;
            totalDamageCriEX = 0;
            totalDamageExcept = 0;
            isJJCBattle = isJJC;
            RandomSeed = seed;
            ubTimes.Clear();
            ubTimesReal.Clear();
            allUnitSkillExecTimeDic.Clear();
            PlayerIds.Clear();
            enemyUnitid = 0;
            this.enemyDes = bossDes;
            uBTimeDatas.Clear();
            ubTimesInited = false;
            //ShowPCRUI.LoadUI();
        }
        public void OnBattleFinish(int frameLogic,int frameReal)
        {
            try
            {
                foreach (int unitid in allUnitStateChangeDic.Keys)
                {
                    BattleLogData logData = new BattleLogData();
                    logData.SetBattleLogType(6);
                    logData.SetTargetUnitId(unitid);
                    logData.SetType(8);
                    logData.SetFrame(frameReal);
                    AppendLog(logData, frameLogic);
                    if (allUnitAbnormalStateDic.ContainsKey(unitid))
                    {
                        foreach (var changeData in allUnitAbnormalStateDic[unitid])
                        {
                            changeData.endFrameCountLogic = frameLogic;
                            changeData.endFrameCountReal = frameReal;
                            changeData.isFinish = true;
                            //skillGroupPrefabDic[unitid].AddAbnormalStateButtons(changeData);
                            allUnitAbnormalStateInputDic[unitid].Add(changeData);
                        }
                    }
                    if(allUnitSkillExecDic.TryGetValue(unitid,out var list1))
                    {
                        foreach(var data in list1)
                        {
                            var dd = allUnitStateInputDic[unitid].FindLastIndex(a => a.fromReal <= data.startTimeReal && a.skillID == data.skillID);
                            if (dd >=0&& allUnitStateInputDic[unitid].Count>dd+1)
                                allUnitStateInputDic[unitid][dd+1].skillData = data;
                        }
                    }
                    if(playerUnitDamageDic.TryGetValue(unitid,out var list2))
                    {
                        foreach(var data in list2)
                        {
                            var dd2 = allUnitStateInputDic[unitid].FindLastIndex(a => a.fromReal <= data.frameReal && a.skillID >0);
                            if (dd2 >= 0 && allUnitStateInputDic[unitid].Count > dd2 + 1)
                                allUnitStateInputDic[unitid][dd2 + 1]?.skillData?.TryAddDamageData(data);

                        }
                    }
                    if (unitid < 200000)
                        PlayerIds.Add(unitid);
                }
                enemyUnitid = PCRSettings.staticBattleBanager.GetEnemyCtrl(0).UnitId;
                foreach (UnitCtrl unit in PCRSettings.staticBattleBanager.OFMPGBKBOPM.UnitCtrls)
                {
                    OnUnitHPChange(unit.UnitId, unit.IsOther, (int)unit.Hp);
                    OnUnitTPChange(unit);
                    List<LogData> unitUB = ReceivedLogList.FindAll(a => a.logType == 7 && a.sourceUnitID == unit.UnitId);
                    List<float> times = new List<float>();
                    List<float> timesReal = new List<float>();
                    foreach (var data in unitUB)
                    {
                        times.Add(data.logicalFrame);
                        timesReal.Add(data.realFrame);
                    }
                    ubTimes.Add(times);
                    ubTimesReal.Add(timesReal);
                    int unitid = unit.UnitId;
                    if (allUnitSkillExecDic.TryGetValue(unitid, out var list1))
                    {
                        foreach (var data in list1)
                        {
                            var dd = allUnitStateInputDic[unitid].FindLastIndex(a => a.fromReal <= data.startTimeReal && a.skillID == data.skillID);
                            if (dd >= 0 && allUnitStateInputDic[unitid].Count > dd + 1)
                                allUnitStateInputDic[unitid][dd + 1].skillData = data;
                        }
                    }
                    if (playerUnitDamageDic.TryGetValue(unitid, out var list2))
                    {
                        foreach (var data in list2)
                        {
                            var dd2 = allUnitStateInputDic[unitid].FindLastIndex(a => a.fromReal <= data.frameReal && a.skillID > 0);
                            if (dd2 >= 0 && allUnitStateInputDic[unitid].Count > dd2 + 1)
                                allUnitStateInputDic[unitid][dd2 + 1]?.skillData?.TryAddDamageData(data);

                        }
                    }

                }
                for (int i = ubTimes.Count; i < 5; i++)
                {
                    ubTimes.Add(new List<float>());
                    ubTimesReal.Add(new List<float>());
                }
                //action(MyGameCtrl.Instance.playerUnitCtrl);
                //action(MyGameCtrl.Instance.enemyUnitCtrl);
                /*if (boss.IsPartsBoss)
                {
                    foreach (var parts in boss.BossPartsListForBattle)
                    {
                        AppendChangeBaseValue(bossId, parts.Index - 1, 1, parts.Def, 5400, "时间耗尽");
                        AppendChangeBaseValue(bossId, parts.Index - 1, 2, parts.MagicDef, 5400, "时间耗尽");
                    }

                }
                else
                {
                    AppendChangeBaseValue(bossId, 0, 1, boss.Def, 5400, "时间耗尽");
                    AppendChangeBaseValue(bossId, 0, 2, boss.MagicDef, 5400, "时间耗尽");
                }*/
                foreach (UnitCtrl enemy in PCRSettings.staticBattleBanager.KKJHHMAAMEI)
                {
                    if (enemy.IsBoss)
                    {
                        if (enemy.IsPartsBoss)
                        {
                            foreach (var parts in enemy.BossPartsListForBattle)
                            {
                                OnBossDEFChange(enemy, parts.Index, true);
                            }
                        }
                        else
                            OnBossDEFChange(enemy, 0, true);
                        //OnUnitHPChange(enemy.UnitId, enemy.IsOther, (int)enemy.Hp);
                    }
                }
            }
            catch(Exception ex)
            {
                Cute.ClientLog.AccumulateClientLog("结束战斗时出错！" + ex.Message);
            }
        }
        public void OnUnitHPChange(int unitid,bool isOther,int hp)
        {
            if (isJJCBattle)
                return;
            int exectTime = UnityEngine.Mathf.RoundToInt((battleTotalTime - PCRSettings.staticBattleBanager.HIJKBOIEPFC) * 60);
            ValueChangeData changeData = new ValueChangeData(exectTime, hp);
            if (allUnitHPDic.TryGetValue(unitid, out var list))
            {
                list.Add(changeData);
            }
            else
                allUnitHPDic.Add(unitid, new List<ValueChangeData> { changeData });
        }
        public void OnUnitTPChange(UnitCtrl unit)
        {
            if (isJJCBattle)
                return;
            int exectTime = UnityEngine.Mathf.RoundToInt((battleTotalTime - PCRSettings.staticBattleBanager.HIJKBOIEPFC) * 60);
            ValueChangeData changeData = new ValueChangeData(exectTime, unit.Energy);
            if (allUnitTPDic.TryGetValue(unit.UnitId, out var list))
            {
                list.Add(changeData);
            }
            else
                allUnitTPDic.Add(unit.UnitId, new List<ValueChangeData> { changeData });
        }
        public void OnBossDEFChange(UnitCtrl unit,int partsIndex = 0,bool forceAdd = false)
        {
            if (isJJCBattle||!unit.IsBoss)
                return;
            int exectTime = UnityEngine.Mathf.RoundToInt((battleTotalTime - PCRSettings.staticBattleBanager.HIJKBOIEPFC) * 60);
            
            Action<List<ValueChangeData>, bool> action = (a, t) =>
             {
                 int b = t ? unit.Def : unit.MagicDef;
                 if (unit.IsPartsBoss)
                 {
                     var parts = unit.BossPartsListForBattle.Find(a0 => a0.Index == partsIndex);
                     if (parts == null)
                         parts = unit.BossPartsListForBattle[0];
                     b = t ? parts.Def : parts.MagicDef;
                 }
                 ValueChangeData changeData2 = a.FindLast(a1 => a1.Index == partsIndex);

                 if (changeData2==null||forceAdd)
                 {
                     ValueChangeData changeData = new ValueChangeData(partsIndex, exectTime, b, 0, "");
                     a.Add(changeData);
                 }
                 else
                 {
                     if (b != changeData2.yValue)
                     {
                         ValueChangeData changeData3 = new ValueChangeData(partsIndex, exectTime, b, 0, "");
                         a.Add(changeData3);
                     }
                 }
            };
            action(bossDefChangeDic, true);
            action(bossMgcDefChangeDic, false);
        }
        public void AppendStartSkill(UnitSkillExecData data)
        {
            if(allUnitSkillExecTimeDic.TryGetValue(data.unitid,out var dic2))
            {
                if (dic2.ContainsKey(data.skillID))
                    dic2[data.skillID]++;
                else
                    dic2.Add(data.skillID, 1);
            }
            else
            {
                allUnitSkillExecTimeDic.Add(data.unitid, new Dictionary<int, int> { { data.skillID, 1 } });
            }
            if (allUnitSkillExecDic.TryGetValue(data.unitid, out var list))
            {
                list.Add(data);
            }
            else
                allUnitSkillExecDic.Add(data.unitid, new List<UnitSkillExecData> { data });
        }
        public void AppendExecAction(int unitid, int skillid, UnitActionExecData actionExecData)
        {
            if (!allUnitSkillExecDic.ContainsKey(unitid)) { return; }
            UnitSkillExecData skillData = allUnitSkillExecDic[unitid].FindLast(a =>
                a.startTime <= actionExecData.execTime && a.skillID == skillid);
            if (skillData != null)
            {
                UnitActionExecData unitAction = skillData.actionExecDatas.Find(a => a.actionID == actionExecData.actionID);
                if (unitAction != null && unitAction.CanCombine(actionExecData))
                {
                    if (!unitAction.targetNames.Contains(actionExecData.targetNames[0]))
                        unitAction.targetNames.Add(actionExecData.targetNames[0]);
                    else
                    {
                        unitAction.describe += "/" + actionExecData.describe;
                    }
                }
                else
                {
                    skillData.actionExecDatas.Add(actionExecData);
                }
            }
        }

        public void OnReceiveLog(Elements.BattleLogData logData,int logicFrame)
        {
            if (isJJCBattle || logData.battle_log_type == 5 && logData.value3 < 100)
                return;
            ReceivedLogList.Add(CreateLogData(logData, logicFrame));
            AppendLog(logData, logicFrame);
        }
        public void AppendLog(Elements.BattleLogData logData,int frame)
        {
            /*if (PCRSettings.staticBattleBanager.JILIICMHLCH != eBattleCategory.CLAN_BATTLE)
            {
                return;
            }*/
            int unitid = logData.target_unit_id;

            switch ((eBattleLogType)logData.battle_log_type)
            {
                case eBattleLogType.SET_STATE:
                    try
                    {
                        UnitCtrl.ActionState actionState = (Elements.UnitCtrl.ActionState)logData.type;
                        //string stateFrom = ((Elements.UnitCtrl.ActionState)current_value).GetDescription();

                        //if (unitid >= 500000 && allUnitStateChangeDic.ContainsKey(unitid)) { return; }
                        if (!allUnitStateChangeDic.ContainsKey(unitid))
                        {
                            allUnitStateChangeDic.Add(unitid, new List<UnitStateChangeData>());
                            allUnitLastStateDic.Add(unitid, new UnitStateChangeData(0, UnitCtrl.ActionState.GAME_START, UnitCtrl.ActionState.GAME_START));
                            allUnitStateInputDic.Add(unitid, new List<UnitStateInputData>());
                        }
                        if (allUnitLastStateDic[unitid].changStateTo != actionState)
                        {
                            allUnitStateChangeDic[unitid].Add(
                                new UnitStateChangeData
                                {
                                    changStateFrom = allUnitLastStateDic[unitid].changStateTo,
                                    changStateTo = actionState,
                                    currentFrameCount = frame,
                                    currentFrameReal = logData.frame
                                }) ;
                            System.Action action = null;
                            int startFrame = allUnitLastStateDic[unitid].currentFrameCount;
                            int startReal = allUnitLastStateDic[unitid].currentFrameReal;
                            int oldState = (int)allUnitLastStateDic[unitid].changStateTo;
                            if (oldState >= 1 && oldState <= 3)
                            {
                                /*UnitSkillExecData skillExecData = allUnitSkillExecDic[unitid].FindLast(a => a.startTime == startFrame);
                                if (skillExecData != null)
                                {
                                    skillExecData.endTime = frameCount;
                                    action = () => { OpenSkillDetailPannel(skillExecData); };
                                }*/
                            }
                            allUnitStateInputDic[unitid].Add(new UnitStateInputData(startFrame, startReal, frame, logData.frame, oldState, action,(int)logData.value1));
                            allUnitLastStateDic[unitid] = new UnitStateChangeData(frame, allUnitLastStateDic[unitid].changStateTo, actionState, "",logData.frame);
                        }
                    }
                    catch (System.Exception e)
                    {
                        Cute.ClientLog.AccumulateClientLog("添加角色按钮时出错！" + e.Message);
                    }
                    break;
                case eBattleLogType.SET_DAMAGE:
                    try
                    {
                        OnUnitHPChange(logData.target_unit_id, false,(int)logData.current_value);
                    }
                    catch (System.Exception e)
                    {
                        Cute.ClientLog.AccumulateClientLog("添加角色伤害时出错！" + e.Message);
                    }
                    break;
                case eBattleLogType.MISS:
                    try
                    {
                        int source = logData.source_unit_id;
                        if (source > 0)
                        {
                            if (allUnitSkillExecDic.ContainsKey(source))
                            {
                                var skillExec = allUnitSkillExecDic[source].FindLast(a => a.startTimeReal < logData.frame);
                                if (skillExec != null)
                                {
                                    var list = skillExec.actionExecDatas.FindAll(a => a.execTimeReal == logData.frame);
                                    foreach(var data in list)
                                    {
                                        data.describe += ((eMissLogType)logData.type).GetDescription();
                                    }
                                }
                            }
                        }
                    }
                    catch (System.Exception e)
                    {
                        Cute.ClientLog.AccumulateClientLog("添加MISS时出错！" + e.Message);
                    }
                    break; 



            }
        }
        public void OnReceiveDamage(int target,int source,PlayerDamageData damageData)
        {
            if (isJJCBattle)
                return;
            try
            {
                bool canAdd = false;
                if (UnitUtility.JudgeIsBoss(target))
                {
                    if (source > 0 && source < 200000)
                    {
                        canAdd = true;
                    }
                }
                damageData.canAddToTotal = canAdd;
                damageData.target = target;
                damageData.source = source;
                if (playerUnitDamageDic.TryGetValue(source, out var list))
                {
                    list.Add(damageData);
                }
                else
                    playerUnitDamageDic.Add(source, new List<PlayerDamageData> { damageData });                
                if (canAdd)
                {
                    totalDamage += (long)(damageData.damage);
                    long criEX = (long)(damageData.damage * (damageData.criDamageValue - 1));
                    long basedamage = (long)(damageData.damage);
                    if (damageData.isCri)
                    {
                        //totalDamageCriEX += criEX;
                        basedamage = (long)(damageData.damage / damageData.criDamageValue);
                        criEX =(long)(criEX / damageData.criDamageValue);
                        totalDamageCriEX += criEX;

                    }
                    //totalDamageExcept += (long)(basedamage + criEX * damageData.criValue);
                    totalDamageExcept += (long)(damageData.damage / (damageData.isCri ? damageData.criDamageValue : 1) * (1 + damageData.criValue * (damageData.criDamageValue - 1)));
                    clanBattleDamageList.Add(new DamageGetData(damageData.frame, totalDamage, totalDamageExcept));
                }

            }
            catch (System.Exception e)
            {
                Cute.ClientLog.AccumulateClientLog("添加角色伤害时出错！" + e.Message);
            }
        }
        public void OnAbnormalStateChange(int unitid, UnitAbnormalStateChangeData abnormalData, int frame,int framereal)
        {
            if (!allUnitAbnormalStateDic.ContainsKey(unitid))
            {
                allUnitAbnormalStateDic.Add(unitid, new List<UnitAbnormalStateChangeData>());
                allUnitAbnormalStateInputDic.Add(unitid, new List<UnitAbnormalStateChangeData>());
            }
            UnitAbnormalStateChangeData changeData;
            if (abnormalData.isBuff)
            {
                if (abnormalData.BUFF_Type == 14)
                    return;
                changeData = allUnitAbnormalStateDic[unitid].Find(a =>
                {
                    return a.BUFF_Type == abnormalData.BUFF_Type &&
                    a.MainValue == abnormalData.MainValue;
                });
            }
            else
            {
                if (abnormalData.CurrentAbnormalState == UnitCtrl.eAbnormalState.NONE)
                    return;
                changeData = allUnitAbnormalStateDic[unitid].FindLast(
                    a => a.CurrentAbnormalState == abnormalData.CurrentAbnormalState);/*||
                    a.CurrentAbnormalState== UnitCtrl.eAbnormalState.SLOW &&
                    abnormalData.CurrentAbnormalState == UnitCtrl.eAbnormalState.HASTE||
                    a.CurrentAbnormalState == UnitCtrl.eAbnormalState.HASTE &&
                    abnormalData.CurrentAbnormalState == UnitCtrl.eAbnormalState.SLOW);*/
            }
            if (abnormalData.enable)
            {
                abnormalData.startFrameCountLogic = frame;
                abnormalData.startFrameCountReal = framereal;
                abnormalData.isFinish = false;
                allUnitAbnormalStateDic[unitid].Add(abnormalData);
            }
            else
            {
                if (changeData == null)
                {
                    //Debug.LogError("(" + frameCount + "）角色：" + unitid + "的状态" + abnormalData.SkillName + "-" + abnormalData.ActionId + "发生错误！");
                    return;
                }
                changeData.endFrameCountLogic = frame;
                changeData.endFrameCountReal = framereal;
                changeData.isFinish = true;
                //skillGroupPrefabDic[unitid].AddAbnormalStateButtons(changeData);
                allUnitAbnormalStateInputDic[unitid].Add(changeData);
                allUnitAbnormalStateDic[unitid].Remove(changeData);
            }
        }
        public void ReceiveBattleLog(string json)
        {
            if (isJJCBattle)
                return;
            //BattleLogReceiveData data = JsonConvert.DeserializeObject<BattleLogReceiveData>(json);
            resultPage?.Close();
            resultPage = new UI.BattleResultUI();
            resultPage.Show();
            resultPage.Init(ReceivedLogList,allUnitStateInputDic,allUnitAbnormalStateInputDic);
        }
        public void UpdateClanBattleFinish(ClanBattleFinishReceiveData data)
        {
            clanFinishData = data;
        }
        public LogData CreateLogData(Elements.BattleLogData logData,int logicFrame)
        {
            string describe = "";
            int colorType = 0;
            int source_unit_id = 0;
            int target_unit_id = 0;
            int frame = 0;
            try
            {
                eBattleLogType battle_log_type = (eBattleLogType)logData.battle_log_type;
                int type = logData.type;
                target_unit_id = logData.target_unit_id;
                int target_is_own_unit = logData.target_is_own_unit;
                source_unit_id = logData.source_unit_id;
                int source_is_own_unit = logData.source_is_own_unit;
                int action_id = logData.action_id;
                frame = logData.frame;
                long value1 = logData.value1;
                int value2 = logData.value2;
                int value3 = logData.value3;
                int duration = logData.duration;
                long current_value = logData.current_value;
                int wave_count = logData.wave_count;
                colorType = (int)battle_log_type;                

                describe = " |" + battle_log_type.GetDescription() + "| ";

                //logBox.AppendTextColorful(describe, Colors[colorType],false);
                //describe = "";
                string actionname = "";
                var data = Elements.ManagerSingleton<Elements.MasterDataManager>.Instance;
                if (action_id > 0)
                {
                    if (action_id == 1)
                        actionname = "普攻";
                    else
                    {
                        int skill_id = action_id / 100;
                        actionname = data.masterSkillData.Get(skill_id)?.name;

                    }
                }
                string targetName = GetName(target_unit_id);
                string sourceName = GetName(source_unit_id);


                switch (battle_log_type)
                {
                    case eBattleLogType.MISS:
                        describe += sourceName + ((eMissLogType)type).GetDescription();
                        if (!string.IsNullOrEmpty(targetName))
                        {
                            describe += ",来源：" + targetName;
                        }
                        if (!string.IsNullOrEmpty(actionname))
                        {
                            describe += "的" + actionname;
                        }
                        if (value1 > 0)
                            describe += "值" + value1;
                        break;
                    case eBattleLogType.SET_DAMAGE:
                        if (frame == 0)
                        {
                            describe += targetName + "初始化HP：" + current_value;
                            break;
                        }
                        string damageTypedes = "魔法伤害";
                        if (type / 10 == 1)
                            damageTypedes = "物理伤害";
                        if (type < 20)
                            damageTypedes += "(暴击)";
                        describe += targetName + "受到";
                        if (!string.IsNullOrEmpty(sourceName))
                        {
                            describe += sourceName;
                        }
                        if (!string.IsNullOrEmpty(actionname))
                        {
                            describe += "的" + actionname;
                        }
                        describe += value1 + "点(" + value2 + "/" + value3 + ")" + damageTypedes + ",剩余HP：" + current_value;
                        break;
                    case eBattleLogType.SET_ABNORMAL:
                        string abdes = ((Elements.UnitCtrl.eAbnormalState)type).GetDescription();
                        describe += targetName + "受到";
                        if (!string.IsNullOrEmpty(sourceName))
                        {
                            describe += sourceName;
                        }
                        if (!string.IsNullOrEmpty(actionname))
                        {
                            describe += "的" + actionname;
                        }
                        describe += "异常状态" + abdes + "预计持续" + current_value + "帧,数值：" + value1;
                        break;
                    case eBattleLogType.SET_RECOVERY:
                        describe += targetName + "受到";
                        if (!string.IsNullOrEmpty(sourceName))
                        {
                            describe += sourceName;
                        }
                        describe += "治疗,治疗量:" + value1 + "当前HP:" + current_value;
                        break;
                    case eBattleLogType.SET_BUFF_DEBUFF:
                        string buffdes = "";
                        if (value3 != 100)
                        {
                            buffdes = GetBuffDes(type);
                        }
                        else
                        {
                            buffdes = GetBuffDes2(type, value2);
                        }
                        describe += targetName;
                        if (value3 == 200)
                        {
                            describe += "(部位" + current_value + ")";
                        }
                        describe += "受到";
                        if (!string.IsNullOrEmpty(sourceName))
                        {
                            describe += sourceName;
                        }
                        describe += "BUFF,类型:" + buffdes + "值：" + value1;
                        break;
                    case eBattleLogType.SET_STATE:
                        string stateTo = ((Elements.UnitCtrl.ActionState)type).GetDescription();
                        string stateFrom = ((Elements.UnitCtrl.ActionState)current_value).GetDescription();
                        describe += targetName + "从" + stateFrom + "状态切换到" + stateTo + "状态";
                        if (value1 > 1)
                        {
                            string ss0 = data.masterSkillData.Get((int)value1)?.Name;

                            describe += ",准备技能" + ss0;
                        }
                        break;
                    case eBattleLogType.BUTTON_TAP:
                        string stateFrom0 = ((Elements.UnitCtrl.ActionState)current_value).GetDescription();
                        string ubName = data.masterSkillData.Get((int)value1)?.Name;
                        describe += targetName + "在" + stateFrom0 + "状态释放UB" + ubName;
                        break;
                    case eBattleLogType.SET_ENERGY:
                        describe += targetName + "通过" + ((eSetEnergyType)type).GetDescription() + "将TP变为" + current_value;
                        break;
                    case eBattleLogType.DAMAGE_CHARGE:
                        describe += "未知log";
                        break;
                    case eBattleLogType.GIVE_VALUE_ADDITIONAL:
                        describe += targetName + "发动加法赋值效果，目标";
                        if (!string.IsNullOrEmpty(sourceName))
                        {
                            describe += sourceName;
                        }
                        if (!string.IsNullOrEmpty(actionname))
                        {
                            describe += "技能：" + actionname;
                        }
                        break;
                    case eBattleLogType.GIVE_VALUE_MULTIPLY:
                        describe += targetName + "发动乘法赋值效果，目标";
                        if (!string.IsNullOrEmpty(sourceName))
                        {
                            describe += sourceName;
                        }
                        if (!string.IsNullOrEmpty(actionname))
                        {
                            describe += "技能：" + actionname;
                        }
                        break;
                    case eBattleLogType.WAVE_END_HP:
                        describe += targetName + "战斗结束HP为：" + value1;
                        break;
                    case eBattleLogType.WAVE_END_DAMAGE_AMOUNT:
                        describe += targetName + "造成伤害为：" + value1;
                        break;

                    case eBattleLogType.BREAK:
                        describe += targetName + "进入Break状态";
                        break;


                }

            }
            catch (System.Exception ex)
            {
                colorType = 0;
                describe += "ERROR!" + ex.Message;
            }
            //logBox.AppendTextColorful(describe + "\n", Colors[colorType], false);
            return new LogData(describe, colorType, target_unit_id,logicFrame,frame);
        }
        public LogData CreateLogData(string log)
        {
            string[] values = log.Split(',');
            string describe = "";
            int colorType = 0;
            int source_unit_id = 0;
            int target_unit_id = 0;
            try
            {
                eBattleLogType battle_log_type = (eBattleLogType)(int.Parse(values[0]));
                int type = int.Parse(values[1]);
                target_unit_id = int.Parse(values[2]);
                int target_is_own_unit = int.Parse(values[3]);
                source_unit_id = int.Parse(values[4]);
                int source_is_own_unit = int.Parse(values[5]);
                int action_id = int.Parse(values[6]);
                int frame = int.Parse(values[7]);
                long value1 = int.Parse(values[8]);
                int value2 = int.Parse(values[9]);
                int value3 = int.Parse(values[10]);
                int duration = int.Parse(values[11]);
                long current_value = int.Parse(values[12]);
                int wave_count = int.Parse(values[13]);
                colorType = (int)battle_log_type;

                /*if (!PCRBattle.Instance.saveData.GetLogEnable(colorType))
                    return;
                if (source_unit_id > 0)
                {
                    if (PCRBattle.Instance.unitShowLogDic.TryGetValue(source_unit_id, out bool value))
                    {
                        if (!value)
                            return;
                    }
                    else
                    {
                        PCRBattle.Instance.unitShowLogDic.Add(source_unit_id, true);
                    }
                }*/

                describe = "(" + frame + ") |" + battle_log_type.GetDescription() + "| ";

                //logBox.AppendTextColorful(describe, Colors[colorType],false);
                //describe = "";
                string actionname = "";
                var data = Elements.ManagerSingleton<Elements.MasterDataManager>.Instance;
                if (action_id > 0)
                {
                    if (action_id == 1)
                        actionname = "普攻";
                    else
                    {
                        int skill_id = action_id / 100;
                        actionname = data.masterSkillData.Get(skill_id)?.name;

                    }
                }
                string targetName = GetName(target_unit_id);
                string sourceName = GetName(source_unit_id);


                switch (battle_log_type)
                {
                    case eBattleLogType.MISS:
                        describe += sourceName + ((eMissLogType)type).GetDescription();
                        if (!string.IsNullOrEmpty(targetName))
                        {
                            describe += ",来源：" + targetName;
                        }
                        if (!string.IsNullOrEmpty(actionname))
                        {
                            describe += "的" + actionname;
                        }
                        if (value1 > 0)
                            describe += "值" + value1;
                        break;
                    case eBattleLogType.SET_DAMAGE:
                        if (frame == 0)
                        {
                            describe += targetName + "初始化HP：" + current_value;
                            break;
                        }
                        string damageTypedes = "魔法伤害";
                        if (type / 10 == 1)
                            damageTypedes = "物理伤害";
                        if (type < 20)
                            damageTypedes += "(暴击)";
                        describe += targetName + "受到";
                        if (!string.IsNullOrEmpty(sourceName))
                        {
                            describe += sourceName;
                        }
                        if (!string.IsNullOrEmpty(actionname))
                        {
                            describe += "的" + actionname;
                        }
                        describe += value1 + "点(" + value2 + "/" + value3 + ")" + damageTypedes + ",剩余HP：" + current_value;
                        break;
                    case eBattleLogType.SET_ABNORMAL:
                        string abdes = ((Elements.UnitCtrl.eAbnormalState)type).GetDescription();
                        describe += targetName + "受到";
                        if (!string.IsNullOrEmpty(sourceName))
                        {
                            describe += sourceName;
                        }
                        if (!string.IsNullOrEmpty(actionname))
                        {
                            describe += "的" + actionname;
                        }
                        describe += "异常状态" + abdes + "预计持续" + current_value + "帧,数值：" + value1;
                        break;
                    case eBattleLogType.SET_RECOVERY:
                        describe += targetName + "受到";
                        if (!string.IsNullOrEmpty(sourceName))
                        {
                            describe += sourceName;
                        }
                        describe += "治疗,治疗量:" + value1 + "当前HP:" + current_value;
                        break;
                    case eBattleLogType.SET_BUFF_DEBUFF:
                        string buffdes = GetBuffDes(type);
                        describe += targetName + "受到";
                        if (!string.IsNullOrEmpty(sourceName))
                        {
                            describe += sourceName;
                        }
                        describe += "BUFF,类型:" + buffdes + "值：" + value1;
                        break;
                    case eBattleLogType.SET_STATE:
                        string stateTo = ((Elements.UnitCtrl.ActionState)type).GetDescription();
                        string stateFrom = ((Elements.UnitCtrl.ActionState)current_value).GetDescription();
                        describe += targetName + "从" + stateFrom + "状态切换到" + stateTo + "状态";
                        if (value1 > 1)
                        {
                            string ss0 = data.masterSkillData.Get((int)value1)?.Name;

                            describe += ",准备技能" + ss0;
                        }
                        break;
                    case eBattleLogType.BUTTON_TAP:
                        string stateFrom0 = ((Elements.UnitCtrl.ActionState)current_value).GetDescription();
                        string ubName = data.masterSkillData.Get((int)value1)?.Name;
                        describe += targetName + "在" + stateFrom0 + "状态释放UB" + ubName;
                        break;
                    case eBattleLogType.SET_ENERGY:
                        describe += targetName + "通过" + ((eSetEnergyType)type).GetDescription() + "将TP变为" + current_value;
                        break;
                    case eBattleLogType.DAMAGE_CHARGE:
                        describe += "未知log";
                        break;
                    case eBattleLogType.GIVE_VALUE_ADDITIONAL:
                        describe += targetName + "发动加法赋值效果，目标";
                        if (!string.IsNullOrEmpty(sourceName))
                        {
                            describe += sourceName;
                        }
                        if (!string.IsNullOrEmpty(actionname))
                        {
                            describe += "技能：" + actionname;
                        }
                        break;
                    case eBattleLogType.GIVE_VALUE_MULTIPLY:
                        describe += targetName + "发动乘法赋值效果，目标";
                        if (!string.IsNullOrEmpty(sourceName))
                        {
                            describe += sourceName;
                        }
                        if (!string.IsNullOrEmpty(actionname))
                        {
                            describe += "技能：" + actionname;
                        }
                        break;
                    case eBattleLogType.WAVE_END_HP:
                        describe += targetName + "战斗结束HP为：" + value1;
                        break;
                    case eBattleLogType.WAVE_END_DAMAGE_AMOUNT:
                        describe += targetName + "造成伤害为：" + value1;
                        break;

                    case eBattleLogType.BREAK:
                        describe += targetName + "进入Break状态";
                        break;


                }

            }
            catch (System.Exception ex)
            {
                colorType = 0;
                describe += "ERROR!" + ex.Message;
            }
            //logBox.AppendTextColorful(describe + "\n", Colors[colorType], false);
            return new LogData(describe, colorType, target_unit_id);
        }
        public string SaveChartSaveData()
        {
            Dictionary<int, string> NameDic = new Dictionary<int, string>();
            foreach(int unitid in allUnitHPDic.Keys)
            {
                NameDic.Add(unitid, PCRSettings.Instance.GetUnitNameByID(unitid));
            }
            ChartSaveData chartSaveData = new ChartSaveData(bossDefChangeDic, bossMgcDefChangeDic,playerUnitDamageDic,allUnitHPDic,allUnitTPDic,NameDic,clanBattleDamageList);
            PCRSettings.Instance.SaveDataToFile("Chart/ChartSaveData",chartSaveData);
            return UnityEngine.Application.streamingAssetsPath + "/Chart/ChartSaveData";
        }

        public string GetName(int unitid)
        {
            string result = "";
            if (unitid <= 0)
                return result;
            var data = Elements.ManagerSingleton<Elements.MasterDataManager>.Instance;

            if (unitid <= 199999)
            {
                if (PCRSettings.unitNameDic.TryGetValue(unitid, out var value))
                    return value;
                result = data.masterUnitData.Get(unitid)?.UnitName;
                return result;
            }
            else
            {
                return data.masterEnemyParameter.GetFromAllKind(unitid)?.name;
            }
        }
        public string GetBuffDes(int type)
        {
            int enable = type - 10;
            int desable = type - 20;
            if (desable < 0)
            {
                return ((Elements.UnitCtrl.BuffParamKind)enable).GetDescription() + "开始";
            }
            if (enable > 14 && enable < 100)
            {
                return ((Elements.UnitCtrl.BuffParamKind)desable).GetDescription() + "结束";
            }
            if (enable == 100 && enable == 101)
            {
                return ((Elements.UnitCtrl.BuffParamKind)enable).GetDescription() + "开始";
            }
            if (desable == 100 && desable == 101)
            {
                return ((Elements.UnitCtrl.BuffParamKind)desable).GetDescription() + "结束";
            }
            return ((Elements.UnitCtrl.BuffParamKind)enable).GetDescription() + "开始" + "或" + ((Elements.UnitCtrl.BuffParamKind)desable).GetDescription() + "结束";

        }
        public string GetBuffDes2(int type,int enable)
        {
            if (enable > 0)
            {
                return ((Elements.UnitCtrl.BuffParamKind)type).GetDescription() + "(开始)";
            }
            else
            {
                return ((Elements.UnitCtrl.BuffParamKind)type).GetDescription() + "(结束)";
            }
        }
        private string CreateExcelName()
        {
            string fileName = enemyDes + "-";
            for (int i = 0; i < 5; i++)
            {
                if (PlayerIds.Count >= i)
                {
                    int player = PlayerIds[i];
                    fileName += PCRSettings.Instance.GetUnitNameByID(player);
                }
            }
            fileName += "-" + UnityEngine.Mathf.RoundToInt(totalDamage / 10000) + "(" + (totalDamageCriEX / 10000) + ")w";
            return fileName;
        }
        public List<UBDetail> CreateUBDetailList()
        {
            List<UBDetail> uBDetails = new List<UBDetail>();
            //List<List<float>> UBTimes = CreateUBExecTimeData();
            for (int i = 0; i < 5; i++)
            {
                if (i < PlayerIds.Count)
                {
                    foreach (int tm in ubTimes[i])
                    {
                        //var unitData = players[i].unitData;
                        UBDetail detail = new UBDetail();
                        //detail.unitData = unitData;
                        detail.unitid = PlayerIds[i];
                        detail.UBTime = (int)tm;
                        /*ValueChangeData changeData = allUnitHPDic[bossId].Find(
                            a => Mathf.RoundToInt(a.xValue * 5400) == tm);*/
                        PlayerDamageData damageData = playerUnitDamageDic[PlayerIds[i]].Find(a => a.frame == tm && a.source == PlayerIds[i]);
                        if (damageData != null)
                        {
                            detail.Damage = (int)damageData.damage;
                            detail.Critical = damageData.isCri;
                        }
                        else
                            detail.Damage = 0;
                        uBDetails.Add(detail);
                    }
                }
            }
            foreach (var data in allUnitStateChangeDic[enemyUnitid])
            {
                if (data.changStateTo == UnitCtrl.ActionState.SKILL_1)
                {
                    UBDetail detail = new UBDetail();
                    detail.isBossUB = true;
                    detail.UBTime = data.currentFrameCount;
                    uBDetails.Add(detail);
                }
            }
            return uBDetails;
        }

        public void CallExcelHelper()
        {
            string fileName = CreateExcelName();

            GuildPlayerGroupData groupData = new GuildPlayerGroupData(PCRSettings.Instance.CreateAddPlayerData(PlayerIds), ubTimes);
            GuildTimelineData timelineData = new GuildTimelineData(groupData, RandomSeed, allUnitStateChangeDic,
                allUnitAbnormalStateDic, allUnitHPDic, allUnitTPDic, allUnitSkillExecDic, playerUnitDamageDic,
                bossDefChangeDic, bossMgcDefChangeDic, new List<RandomData>(), clanBattleDamageList, new List<int[]>());
            timelineData.UBExecTime = ubTimes;
            timelineData.exceptDamage = UnityEngine.Mathf.RoundToInt(totalDamageExcept / 10000);
            timelineData.backDamage = UnityEngine.Mathf.RoundToInt((totalDamage - totalDamageCriEX) / 10000);
            //timelineData.charImages = PCRCaculator.Battle.BattleUIManager.Instance.GetCharactersImage();
            //string fileName = CreateExcelName();
            timelineData.timeLineName = CreateExcelName();
            timelineData.uBDetails = CreateUBDetailList();
            EXCELHelper.OutputGuildTimeLine(timelineData, fileName);
            //MainManager.Instance.WindowConfigMessage("成功！", null, null);

        }
        public void AddUBTimes(List<Hook.UnitCtrlAdd.UBTimeData> timeDatas,bool islogic)
        {
            uBTimeDatas.AddRange(timeDatas);
            uBTimeDatas.Sort((a, b) =>
            {
                if (a.ubTime != b.ubTime)
                    return a.ubTime - b.ubTime;
                else
                    return a.prioruty - b.prioruty;
            });
            if (!ubTimesInited && timeDatas.Count>0)
            {
                Elements.Battle.BattleManager battleManager = HarmonyLib.Traverse.Create(timeDatas[0].unit).Field("staticBattleManager").GetValue<Elements.Battle.BattleManager>();
                battleManager.AppendCoroutine(Hook.UnitCtrlAdd.UpdateUBExecTime_new(uBTimeDatas, !islogic, saveData.ubTryCount),ePauseType.SYSTEM);
                ubTimesInited = true;            
            }
        }
    }
    public class LogData
    {
        public string describe;
        public int logType;
        public int sourceUnitID;
        public int logicalFrame;
        public int realFrame;
        public string Des_real => "(" + realFrame + ")" + describe;
        public string Des_logic => "[" + logicalFrame + "]" + describe;
        public string Des => "[" + logicalFrame + "/" + realFrame + "]" + describe;


        public LogData(string describe, int logType, int sourceUnitID, int logicalFrame = 0, int realFrame = 0)
        {
            this.describe = describe;
            this.logType = logType;
            this.sourceUnitID = sourceUnitID;
            this.logicalFrame = logicalFrame;
            this.realFrame = realFrame;
        }
    }
    
    public class BattleSaveData
    {
        public List<GuildTimelineData> timelineDatas = new List<GuildTimelineData>();
        public bool useUBSet = false;
        public int selectIndex = -1;
        public int ubTryCount = 20;
        public int[] logTypes = new int[14] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public bool logFrameLogic = true;
        public bool useFastKey = false;
        public int[] fastKeys = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        public bool nnkBUG = false;
        public BattleSaveData() { }
        public void Save()
        {
            PCRBattle.Instance.Save();
        }
        public bool GetLogEnable(int type)
        {
            if (type > 0 && type < logTypes.Length)
                return logTypes[type] == 1;
            return true;
        }
        public void SetLogEnable(int type,bool enable)
        {
            if (type > 0 && type < logTypes.Length)
            {
                logTypes[type] = enable ? 1 : 0;
            }
        }
    }
    /*public class GuildTimelineData
    {
        //public GuildSettingData currentSettingData;
        public int currentRandomSeed;
        public GuildPlayerGroupData playerGroupData;// = new GuildPlayerGroupData();
        public string timeLineName = "新建方案";
        public int exceptDamage;
        public int backDamage;
        public int timeType = 0;//0-逻辑，1-渲染
        public GuildTimelineData()
        {

        }
        public GuildTimelineData(int t)
        {
            playerGroupData = new GuildPlayerGroupData(t);
        }
    }*/
    public class GuildPlayerGroupData
    {
        public AddedPlayerData playerData;// = new AddedPlayerData();
                
        public List<List<float>> UBExecTimeData;
        public bool useAutoMode;
        //public GuildRandomData timeLineData;
        public int currentGuildMonth = 9;
        public int currentGuildEnemyNum = 1;
        public int currentTurn = 1;
        public int selectedEnemyID = 0;
        public bool isViolent;
        public bool usePlayerSettingHP;
        public int playerSetingHP;
        public bool useLogBarrier;

        public bool isSpecialBoss;
        public int specialBossID;
        public int specialInputValue;
        public GuildPlayerGroupData() { }
        public GuildPlayerGroupData(int t)
        {
            UBExecTimeData = new List<List<float>>();
            for(int i = 0; i < 5; i++)
            {
                UBExecTimeData.Add(new List<float>());
            }
        }
        public GuildPlayerGroupData(AddedPlayerData playerData, List<List<float>> UBExecTimeData)
        {
            this.playerData = playerData;
            this.UBExecTimeData = UBExecTimeData;
        }
        public void SortUbTimes()
        {
            foreach(var ubs in UBExecTimeData)
            {
                ubs.Sort();
                //ubs.Reverse();
            }
        }
        public void Fix()
        {
            if (UBExecTimeData == null)
            {
                UBExecTimeData = new List<List<float>>();
                for (int i = 0; i < 5; i++)
                {
                    UBExecTimeData.Add(new List<float>());
                }
            }
            if (UBExecTimeData.Count < 5)
            {
                while (UBExecTimeData.Count < 5)
                    UBExecTimeData.Add(new List<float>());
            }
        }
    }
    public class AddedPlayerData
    {
        public string playerName = "未命名";
        public int playerLevel = 100;
        public int totalpoint = 0;//队伍总战力
        public List<UnitData> playrCharacters = new List<UnitData>();//至少为1，最多为5
    }
    [System.Serializable]
    public class UnitData
    {
        public int unitId = 0;
        public int level = 1;
        public int rarity = 1;
        public int love = 0;//好感度
        public int[] rankEX = new int[2] { 1, 0 };
        public int rank = 1;

        public int[] equipLevel = new int[6] { -1, -1, -1, -1, -1, -1 };//装备等级，-1-未装备，0~5表示强化等级
        public int[] skillLevel = new int[4] { 1, 1, 1, 1 };//技能等级，0123对应UB/技能1/技能2/EX技能
        public int uniqueEqLv = 0;
        public Dictionary<int, int> playLoveDic;
        public UnitData() { }
        public UnitData(int id, int rarity, int[] rank)
        {
            unitId = id;
            this.rarity = rarity;
            this.rankEX = rank;
        }
        public UnitData(UnitDataS dataS,int love)
        {
            unitId = dataS.id;
            level = dataS.unit_level;
            rarity = dataS.unit_rarity;
            this.love = love;
            rank = dataS.promotion_level;
            equipLevel = dataS.GetEquipList();
            try
            {
                skillLevel[0] = dataS.union_burst[0].skill_level;
                skillLevel[1] = dataS.main_skill[0].skill_level;
                skillLevel[2] = dataS.main_skill[1].skill_level;
                skillLevel[3] = dataS.ex_skill[0].skill_level;
                uniqueEqLv = dataS.unique_equip_slot[0].GetLv();
            }
            catch(Exception ex)
            {
                Cute.ClientLog.AccumulateClientLog("ERROR WHEN INIT UNITDATA" + ex.Message + ex.StackTrace);
            }
        }
        public bool Equal(UnitData unit)
        {
            if (unit.rankEX[0] != rankEX[0]) { return false; }
            if (unit.rankEX[1] != rankEX[1]) { return false; }
            for (int i = 0; i < 4; i++)
            {
                if (unit.skillLevel[i] != skillLevel[i]) { return false; }
            }
            return unit.unitId == unitId && unit.level == level && unit.love == love && unit.uniqueEqLv == uniqueEqLv;
        }
        public void SetRankEX()
        {
            rankEX[0] = rank;
            int a = 0;
            foreach (var eq in equipLevel)
            {
                if (eq > 0)
                    a++;
            }
            rankEX[1] = a;
        }
        public string GetNicName()
        {
            return PCRSettings.Instance.GetUnitNameByID(unitId);
        }
        public string GetUnitName()
        {
            return PCRSettings.Instance.GetUnitNameByID(unitId);
        }

        public string GetRankDescribe()
        {
            string result = "R" + rank + "-";
            int count = 0;
            bool special = false;
            for (int i = 0; i < equipLevel.Length; i++)
            {
                if (equipLevel[i] == 5)
                    count++;
                else if (equipLevel[i] > 0)
                    special = true;
            }
            result += count;
            if (special)
                result += "*";
            if (uniqueEqLv > 0)
                result += "(" + uniqueEqLv + ")";
            return result;
        }
        public string GetLevelDescribe()
        {
            string result = "" + level;
            bool special = false;
            foreach (int lv in skillLevel)
                if (lv != level)
                    special = true;
            if (special)
                result += "*";
            return result;
        }

    }
    public class ChartSaveData
    {
        public List<ValueChangeData> bossDefChangeDic = new List<ValueChangeData>();
        public List<ValueChangeData> bossMgcDefChangeDic = new List<ValueChangeData>();
        public Dictionary<int, List<PlayerDamageData>> playerUnitDamageDic = new Dictionary<int, List<PlayerDamageData>>();
        public Dictionary<int, List<ValueChangeData>> allUnitHPDic = new Dictionary<int, List<ValueChangeData>>();
        public Dictionary<int, List<ValueChangeData>> allUnitTPDic = new Dictionary<int, List<ValueChangeData>>();
        public Dictionary<int, string> nameDic = new Dictionary<int, string>();
        public List<DamageGetData> clanBattleDamageList = new List<DamageGetData>();

        public ChartSaveData() { }

        public ChartSaveData(List<ValueChangeData> bossDefChangeDic, List<ValueChangeData> bossMgcDefChangeDic, Dictionary<int, List<PlayerDamageData>> playerUnitDamageDic, Dictionary<int, List<ValueChangeData>> allUnitHPDic, Dictionary<int, List<ValueChangeData>> allUnitTPDic, Dictionary<int, string> nameDic, List<DamageGetData> clanBattleDamageList)
        {
            this.bossDefChangeDic = bossDefChangeDic;
            this.bossMgcDefChangeDic = bossMgcDefChangeDic;
            this.playerUnitDamageDic = playerUnitDamageDic;
            this.allUnitHPDic = allUnitHPDic;
            this.allUnitTPDic = allUnitTPDic;
            this.nameDic = nameDic;
            this.clanBattleDamageList = clanBattleDamageList;
        }
    }
    public class PlayerDamageData
    {
        public int frame;
        public int frameReal;
        public long damage;
        public bool isCri;
        public float criValue;
        public float criDamageValue;
        public int skillid;
        public bool canAddToTotal;
        public int target;
        public int source;

        public PlayerDamageData() { }

        public PlayerDamageData(int frame, int frameReal, long damage, bool isCri, float criValue, float criDamageValue, int actionid)
        {
            this.frame = frame;
            this.frameReal = frameReal;
            this.damage = damage;
            this.isCri = isCri;
            this.criValue = criValue;
            this.criDamageValue = criDamageValue;
            this.skillid = actionid;
            Check();
        }
        public void Check()
        {
            if (criValue < 0)
                criValue = 0;
            if (criValue > 1)
                criValue = 1;

        }
        public string GetFrame()
        {
            return "[" + frame + "/" + frameReal + "]";
        }
    }
}
