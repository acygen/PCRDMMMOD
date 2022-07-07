using System.Collections;
using System.Collections.Generic;
using Elements.Battle;
using Elements;
using System.ComponentModel;
using Newtonsoft0.Json;
using System.Drawing;
using UnityEngine;
using System;

namespace PCRCalculator.Tool
{
    public class GuildCalculator
    {
        public static GuildCalculator Instance;
        public const float deltaXforChat = 1 / 5400.0f;
        public Dictionary<int, List<UnitStateChangeData>> allUnitStateChangeDic = new Dictionary<int, List<UnitStateChangeData>>();
        public Dictionary<int, List<UnitAbnormalStateChangeData>> allUnitAbnormalStateDic = new Dictionary<int, List<UnitAbnormalStateChangeData>>();
        public Dictionary<int, List<ValueChangeData>> allUnitHPDic = new Dictionary<int, List<ValueChangeData>>();
        public Dictionary<int, List<ValueChangeData>> allUnitTPDic = new Dictionary<int, List<ValueChangeData>>();
        public Dictionary<int, List<UnitSkillExecData>> allUnitSkillExecDic = new Dictionary<int, List<UnitSkillExecData>>();
        public Dictionary<int, List<ValueChangeData>> playerUnitDamageDic = new Dictionary<int, List<ValueChangeData>>();
        public List<DamageGetData> clanBattleDamageList = new List<DamageGetData>();
        public List<ValueChangeData> bossDefChangeDic = new List<ValueChangeData>();
        public List<ValueChangeData> bossMgcDefChangeDic = new List<ValueChangeData>();
        public List<RandomData> AllRandomDataList = new List<RandomData>();
        public List<int[]> AllUnitUBList = new List<int[]>();


        public List<System.Drawing.Color> skillGroupColors;

        private Dictionary<int, UnitStateChangeData> allUnitLastStateDic = new Dictionary<int, UnitStateChangeData>();
        private List<int> playerIds = new List<int>();
        private int bossId;
        private List<UnitCtrl> players;
        private UnitCtrl boss;
        //private Dictionary<int, GuildSkillGroupPrefab> skillGroupPrefabDic = new Dictionary<int, GuildSkillGroupPrefab>();
        private long totalDamage = 0;
        private long totalDamageCriEX = 0;
        private long totalDamageExcept = 0;
        private bool isFinishCalc;
        private int backTime = 0;

        public List<int> PlayerIds { get => playerIds; }
        public int BossId { get => bossId; }

        private void Awake()
        {
            Instance = this;
        }
        private void OnDestroy()
        {
            Instance = null;
        }
        public void Initialize(List<UnitCtrl> players, UnitCtrl boss)
        {
            this.players = players;
            this.boss = boss;
            
            ReflashTotalDamage(false, 0, false, 0, 0, 0);
            //CurrentBossText.text = MainManager.Instance.GuildBattleData.SettingData.GetCurrentBossDes();
            //currentSeedText.text = "" + MyGameCtrl.Instance.CurrentSeedForSave;

            int idx = 0;
            bossId = boss.UnitId;
            AddSkillGroupPrefab(boss.UnitId, boss, 0);
            //boss.MyOnDamage += AppendGetDamage;
            //boss.MyOnBaseValueChanged += AppendChangeBaseValue;
            foreach (UnitCtrl unitCtrl in players)
            {
                idx++;
                AddSkillGroupPrefab(unitCtrl.UnitId, unitCtrl, idx);
                PlayerIds.Add(unitCtrl.UnitId);
            }
            playerUnitDamageDic.Add(0, new List<ValueChangeData> { new ValueChangeData(0, 0) });
            if (boss.IsPartsBoss)
            {
                foreach (var parts in boss.BossPartsListForBattle)
                {
                    AppendChangeBaseValue(bossId, parts.Index - 1, 1, parts.Def, 0, "初始化");
                    AppendChangeBaseValue(bossId, parts.Index - 1, 2, parts.MagicDef, 0, "初始化");
                }
            }
            else
            {
                AppendChangeBaseValue(bossId, 0, 1, boss.Def, 0, "初始化");
                AppendChangeBaseValue(bossId, 0, 2, boss.MagicDef, 0, "初始化");
            }
            OnToggleSwitched(0);
            //Elements.Battle.BattleManager.OnCallRandom = AppendCallRandom;
        }
        private void AddSkillGroupPrefab(int a, UnitCtrl b, int c, bool create = true)
        {
            bool isPlayer = b.UnitId <= 200000;
            if (create)
            {
                allUnitStateChangeDic.Add(a, new List<UnitStateChangeData>());
                allUnitAbnormalStateDic.Add(a, new List<UnitAbnormalStateChangeData>());
                allUnitHPDic.Add(a, new List<ValueChangeData> { new ValueChangeData(0, 1, (int)b.MaxHp, "初始化") });
                allUnitTPDic.Add(a, new List<ValueChangeData> { new ValueChangeData(0, 0) });
                allUnitLastStateDic.Add(a, new UnitStateChangeData(0, UnitCtrl.ActionState.GAME_START, UnitCtrl.ActionState.GAME_START));
                allUnitSkillExecDic.Add(a, new List<UnitSkillExecData>());
            }
            /*b.MyOnChangeState += AppendChangeState;
            if (b.UnitId >= 300000 && b.UnitId <= 400000)
            {
                b.MyOnDamage2 += ReflashTotalDamage;
            }
            else if (isPlayer)
            {
                playerUnitDamageDic.Add(b.UnitId, new List<ValueChangeData> { new ValueChangeData(0, 0) });
            }
            b.MyOnAbnormalStateChange += AppendChangeAbnormalState;
            b.MyOnLifeChanged += AppendChangeHP;
            b.MyOnTPChanged += AppendChangeTP;
            b.MyOnStartAction += AppendStartSkill;
            b.MyOnExecAction += AppendExecAction;
            if (create)
                AddSkillGroups(a, c, c, b.UnitName);*/

            //AddSkillGroups(a, c, c,isPlayer?b.unitData.GetNicName():b.UnitName);

        }
        public void AppendChangeState(int unitid, UnitCtrl.ActionState actionState, int frameCount, string describe)
        {
            if (actionState == UnitCtrl.ActionState.SKILL_1)
            {
                //AllUnitUBList.Add(new int[3] { Elements.BattleHeaderController.CurrentFrameCount, Elements.Battle.BattleManager.Instance.FrameCount, unitid });
            }
            try
            {
                if (unitid >= 500000 && allUnitStateChangeDic.ContainsKey(unitid)) { return; }
                if (allUnitLastStateDic[unitid].changStateTo != actionState)
                {
                    allUnitStateChangeDic[unitid].Add(
                        new UnitStateChangeData
                        {
                            changStateFrom = allUnitLastStateDic[unitid].changStateTo,
                            changStateTo = actionState,
                            currentFrameCount = frameCount
                        });
                    //skillGroupPrefabDic[unitid].AddButtons(allUnitLastStateDic[unitid].currentFrameCount, frameCount, (int)actionState);
                    System.Action action = null;
                    int startFrame = allUnitLastStateDic[unitid].currentFrameCount;
                    int oldState = (int)allUnitLastStateDic[unitid].changStateTo;
                    if (oldState >= 1 && oldState <= 3)
                    {
                        UnitSkillExecData skillExecData = allUnitSkillExecDic[unitid].FindLast(a => a.startTime == startFrame);
                        if (skillExecData != null)
                        {
                            skillExecData.endTime = frameCount;
                            action = () => { OpenSkillDetailPannel(skillExecData); };
                        }
                    }
                    //skillGroupPrefabDic[unitid].AddButtons(startFrame, frameCount, oldState, action);

                    //skillScrollRect.horizontalNormalizedPosition = frameCount * deltaXforChat;
                    allUnitLastStateDic[unitid] = new UnitStateChangeData(frameCount, allUnitLastStateDic[unitid].changStateTo, actionState, describe);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("添加角色按钮时出错！" + e.Message);
            }
        }
        public void AppendChangeAbnormalState(int unitid, UnitAbnormalStateChangeData abnormalData, int frameCountLogic,int frameCountReal)
        {

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
                abnormalData.startFrameCountLogic = frameCountLogic;
                abnormalData.startFrameCountReal = frameCountReal;
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
                changeData.endFrameCountLogic = frameCountLogic;
                abnormalData.endFrameCountReal = frameCountReal;
                changeData.isFinish = true;
                //skillGroupPrefabDic[unitid].AddAbnormalStateButtons(changeData);
                allUnitAbnormalStateDic[unitid].Remove(changeData);
            }
        }
        public void AppendChangeHP(int unitid, float currentHP, int hp, int damage, int frame, string describe)
        {
            if (unitid >= 400000 && !allUnitHPDic.ContainsKey(unitid)) { return; }
            var valueData = new ValueChangeData(frame * deltaXforChat, currentHP, hp, describe);
            valueData.damage = damage;
            allUnitHPDic[unitid].Add(valueData);
            //skillGroupPrefabDic[unitid].ReflashHPChat(allUnitHPDic[unitid]);
        }
        public void AppendChangeTP(int unitid, float currentTP, int frame, string describe)
        {
            if (unitid >= 400000 && !allUnitTPDic.ContainsKey(unitid)) { return; }
            allUnitTPDic[unitid].Add(new ValueChangeData(frame * deltaXforChat, currentTP, 0, describe));
           // skillGroupPrefabDic[unitid].ReflashTPChat(allUnitTPDic[unitid]);
        }
        public void AppendStartSkill(int unitid, UnitSkillExecData unitSkillExecData)
        {
            if (unitid >= 400000 && !allUnitSkillExecDic.ContainsKey(unitid)) { return; }
            allUnitSkillExecDic[unitid].Add(unitSkillExecData);
        }
        public void AppendExecAction(int unitid, int skillid, UnitActionExecData actionExecData)
        {
            if (unitid >= 400000 && !allUnitSkillExecDic.ContainsKey(unitid)) { return; }
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
        public void AppendGetDamage(int unitid, int sourceUnitid, float damage, int currentFrame)
        {
            if (sourceUnitid >= 400000) { return; }
            if (unitid == bossId)
            {
                playerUnitDamageDic[sourceUnitid].Add(new ValueChangeData(currentFrame / 5400.0f, damage));
                playerUnitDamageDic[0].Add(new ValueChangeData(currentFrame / 5400.0f, damage));
            }
        }
        public void AppendChangeBaseValue(int unitid, int index, int valueType, float value, int currentFrame, string describe)
        {
            if (unitid == bossId)
            {
                if (value < 0)
                {
                    value = 0;
                }
                switch (valueType)
                {
                    case 1:
                        bossDefChangeDic.Add(new ValueChangeData(index, currentFrame * deltaXforChat, value, 0, describe));
                        break;
                    case 2:
                        bossMgcDefChangeDic.Add(new ValueChangeData(index, currentFrame * deltaXforChat, value, 0, describe));
                        break;
                }
            }
        }
        public void AppendCallRandom(RandomData data)
        {
            data.id = AllRandomDataList.Count;
            //data.frame = BattleHeaderController.CurrentFrameCount;
            //data.currentSeed = Random.seed;
            AllRandomDataList.Add(data);
        }
        public void OnToggleSwitched(int toggleId)
        {
            /*if (ModeToggles[toggleId].isOn)
            {
                foreach (var sk in skillGroupPrefabDic.Values)
                {
                    sk.SwitchPage(toggleId);
                }
                ResizePrefabs(toggleId == 1);
            }*/
        }
        private void AddSkillGroups(int unitid, int idx, int colorIdx, string Name)
        {
            /*GameObject a = Instantiate(skillGroupPrefab);
            a.transform.SetParent(skillGroupParent, false);
            a.transform.localPosition = skillGroupBasePos + idx * skillGroupAddPos;
            GuildSkillGroupPrefab guildSkill = a.GetComponent<GuildSkillGroupPrefab>();
            if (colorIdx >= skillGroupColors.Count)
                colorIdx = skillGroupColors.Count - 1;
            guildSkill.Initialize(Name, skillGroupColors[colorIdx]);
            skillGroupPrefabDic.Add(unitid, guildSkill);*/
        }
        public void ReflashTotalDamage(bool byAttack, float value, bool critical, float criticalEXdamage, float criticalValue, float criticalDamageMulti)
        {
            /*totalDamage += (long)value;
            criticalValue = Mathf.Min(1, Mathf.Max(0, criticalValue));
            int baseDamage = critical ? (int)(value - criticalEXdamage) : (int)value;
            totalDamageExcept += (long)(baseDamage + baseDamage * criticalValue * (criticalDamageMulti - 1));
            if (critical)
                totalDamageCriEX += (long)criticalEXdamage;
            string damageStr = "" + totalDamage +
                "(" + (totalDamage - totalDamageCriEX) + "+<color=#FFEC00>" + totalDamageCriEX + "</color>)" +
                "[<color=#56A0FF>" + totalDamageExcept + "</color>]";
            if (backTime > 0)
                damageStr += "返" + backTime + "s";
            totalDamageText.text = damageStr;
            clanBattleDamageList.Add(new DamageGetData(Elements.BattleHeaderController.CurrentFrameCount, totalDamage, totalDamageExcept));
        */
        }
        public void AddSummonUnit(UnitCtrl unitCtrl)
        {
            if (playerIds.Contains(unitCtrl.UnitId))
            {
                AddSkillGroupPrefab(unitCtrl.UnitId, unitCtrl, playerIds.Count, false);
                return;
            }
            int idx = playerIds.Count + 1;
            AddSkillGroupPrefab(unitCtrl.UnitId, unitCtrl, idx);
            PlayerIds.Add(unitCtrl.UnitId);
        }
        public void OnBattleFinished(int result, int currentFrame)
        {
            System.Action<List<UnitCtrl>> action = new System.Action<List<UnitCtrl>>(a =>
            {
                foreach (UnitCtrl unitCtrl in a)
                {
                    int unitid = unitCtrl.UnitId;
                    AppendChangeState(unitid, UnitCtrl.ActionState.GAME_START, 5400, "时间耗尽");
                    AppendChangeHP(unitid, (float)unitCtrl.Hp / unitCtrl.MaxHp, 0, (int)unitCtrl.Hp, 5400, "时间耗尽");
                    AppendChangeTP(unitid, (float)unitCtrl.Energy / BattleDefine.SKILL_ENERGY_MAX, 5400, "时间耗尽");
                    foreach (var changeData in allUnitAbnormalStateDic[unitid])
                    {
                        changeData.endFrameCountLogic = 5400;
                        changeData.isFinish = true;
                        //skillGroupPrefabDic[unitid].AddAbnormalStateButtons(changeData);
                    }
                }
            });
            //action(MyGameCtrl.Instance.playerUnitCtrl);
            //action(MyGameCtrl.Instance.enemyUnitCtrl);
            if (boss.IsPartsBoss)
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
            }
            /*skillScrollRect.horizontalNormalizedPosition = 1;
            guildPageUI.SetActive(true);
            isFinishCalc = true;
            chatPannel.Init();
            OnToggleSwitched(0);
            if (currentFrame < 5400)
            {
                backTime = Mathf.CeilToInt((5400 - currentFrame) / 60.0f) + 20;
                ReflashTotalDamage(true, 0, false, 0, 0, 0);
            }*/
        }
        private void ResizePrefabs(bool change)
        {
            int idx = 0;
            /*float localPosX = skillGroupBasePos.x;
            float localPosY = skillGroupAddPos.y;
            foreach (var prefab in skillGroupPrefabDic.Values)
            {
                //a.transform.localPosition = skillGroupBasePos + idx * skillGroupAddPos;
                float localPosY_change = prefab.Resize(change) + 3;
                prefab.gameObject.transform.localPosition = new Vector3(localPosX, localPosY, 0);
                localPosY -= localPosY_change;
                idx++;
            }
            float totalHight = Mathf.Abs(localPosY - 3);
            skillGroupParent.sizeDelta = new Vector2(skillGroupParent.sizeDelta.x, totalHight);*/
        }
        public void OpenSkillDetailPannel(UnitSkillExecData data)
        {
            /*GameObject a = Instantiate(skillDetailPrefab);
            a.transform.SetParent(BaseBackManager.Instance.latestUIback.transform, false);
            a.GetComponent<GuildSkillDetailPannel>().Setdetails(data);*/
        }
        
        public void SaveFileToExcel()
        {
            /*if (Application.platform == RuntimePlatform.Android)
            {
                MainManager.Instance.WindowConfigMessage("手机端导出可能失败，建议使用电脑端导出！\n如要尝试请按确定继续", CallExcelHelper, null);
            }
            else
            {
                CallExcelHelper();
            }*/
        }
        private void CallExcelHelper()
        {
            //string fileName = CreateExcelName();

            /*try
            {

                GuildTimelineData timelineData = new GuildTimelineData(MyGameCtrl.Instance.tempData.SettingData.GetCurrentPlayerGroup(), MyGameCtrl.Instance.CurrentSeedForSave, allUnitStateChangeDic,
                    allUnitAbnormalStateDic, allUnitHPDic, allUnitTPDic, allUnitSkillExecDic, playerUnitDamageDic,
                    bossDefChangeDic, bossMgcDefChangeDic, AllRandomDataList, clanBattleDamageList, AllUnitUBList);
                timelineData.UBExecTime = CreateUBExecTimeData();
                timelineData.exceptDamage = Mathf.RoundToInt(totalDamageExcept / 10000);
                timelineData.backDamage = Mathf.RoundToInt((totalDamage - totalDamageCriEX) / 10000);
                timelineData.charImages = PCRCaculator.Battle.BattleUIManager.Instance.GetCharactersImage();
                //string fileName = CreateExcelName();
                timelineData.timeLineName = CreateExcelName();
                timelineData.uBDetails = CreateUBDetailList();
                ExcelHelper.ExcelHelper.OutputGuildTimeLine(timelineData, fileName);
                MainManager.Instance.WindowConfigMessage("成功！", null, null);
            }
#if UNITY_EDITOR
            catch (System.DuplicateWaitObjectException a)
#else
            catch (System.Exception a)
#endif
            {
                MainManager.Instance.WindowConfigMessage("发生错误：" + a.Message, null, null);
            }*/

        }
        public void JudgeIfShareInAndroid()
        {
            /*if (Application.platform == RuntimePlatform.Android)
            {
                FantomLib.AndroidPlugin.StartAction("android.intent.action.SEND",
    new string[] { "android.intent.extra.TEXT", "android.intent.extra.STREAM" },
    new string[] { "Shard by PCRCalculator", },
    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            }*/
        }
        public List<List<float>> CreateUBExecTimeData()
        {
            List<List<float>> UBTimes = new List<List<float>>();
            for (int i = 0; i < 5; i++)
            {
                List<float> ubline = new List<float>();
                if (i < PlayerIds.Count)
                {
                    int playerid = PlayerIds[i];
                    foreach (var data in allUnitStateChangeDic[playerid])
                    {
                        if (data.changStateTo == UnitCtrl.ActionState.SKILL_1)
                        {
                            ubline.Add(data.currentFrameCount);
                        }
                    }
                }
                UBTimes.Add(ubline);
            }
            return UBTimes;
        }
        public List<UBDetail> CreateUBDetailList()
        {
            List<UBDetail> uBDetails = new List<UBDetail>();
            List<List<float>> UBTimes = CreateUBExecTimeData();
            /*for (int i = 0; i < 5; i++)
            {
                if (i < PlayerIds.Count)
                {
                    foreach (int tm in UBTimes[i])
                    {
                        var unitData = players[i].unitData;
                        UBDetail detail = new UBDetail();
                        detail.unitData = unitData;
                        detail.UBTime = (int)tm;
                        ValueChangeData changeData = allUnitHPDic[bossId].Find(
                            a => Mathf.RoundToInt(a.xValue * 5400) == tm);
                        if (changeData != null)
                        {
                            detail.Damage = changeData.damage;
                            detail.Critical = changeData.describe.Contains("暴击");
                        }
                        else
                            detail.Damage = 0;
                        uBDetails.Add(detail);
                    }
                }
            }
            foreach (var data in allUnitStateChangeDic[bossId])
            {
                if (data.changStateTo == UnitCtrl.ActionState.SKILL_1)
                {
                    UBDetail detail = new UBDetail();
                    detail.isBossUB = true;
                    detail.UBTime = data.currentFrameCount;
                    uBDetails.Add(detail);
                }
            }*/
            return uBDetails;
        }
        /*private string CreateExcelName()
        {
            string fileName = CurrentBossText.text + "-";
            for (int i = 0; i < 5; i++)
            {
                if (playerIds.Count >= i)
                {
                    int player = playerIds[i];
                    fileName += MainManager.Instance.GetUnitNickName(player);
                }
            }
            fileName += "-" + Mathf.RoundToInt(totalDamage / 10000) + "(" + (totalDamageCriEX / 10000) + ")w";
            return fileName;
        }*/
       /* public void ReplaceUBTime()
        {
            //MainManager.Instance.WindowConfigMessage("是否将预设阵容的UB时间改为当前的UB时间？", ReplaceUBTime_0, null);
            GameObject a = Instantiate(UBTimeEditorPrefab);
            a.transform.SetParent(BaseBackManager.Instance.latestUIback.transform, false);
            a.GetComponent<UBTimeEditor>().Init(MyGameCtrl.Instance.tempData.SettingData.GetCurrentPlayerGroup());
        }*/
        /*private void ReplaceUBTime_0()
        {
            MyGameCtrl.Instance.tempData.SettingData.ReplaceUBTimeData(CreateUBExecTimeData());
            GuildManager.SaveSettingData(MyGameCtrl.Instance.tempData.SettingData);
            MainManager.Instance.WindowConfigMessage("成功！", null, null);
        }*/
        /*public PCRCaculator.OnceResultData GetOnceResultData(int id)
        {
            List<string> errorList = new List<string>();
            List<List<float>> ubExecTime = CreateUBExecTimeData();
            for (int i = 0; i < 5; i++)
            {
                if (players.Count > i)
                {
                    if (MyGameCtrl.Instance.tempData.UBExecTimeList[i].Count != ubExecTime[i].Count)
                    {
                        errorList.Add(players[i].UnitName + "的某个UB释放失败！");
                    }
                }
            }
            return new OnceResultData
            {
                id = id,
                exceptDamage = totalDamageExcept,
                criticalEX = totalDamageCriEX,
                currentDamage = totalDamage,
                randomSeed = MyGameCtrl.Instance.CurrentSeedForSave,
                warnings = errorList
            };
        }*/
        public static List<ValueChangeData> CreateLineChatData(List<ValueChangeData> data, float deltaXforChat = 1 / 5400.0f)
        {
            List<ValueChangeData> newData = new List<ValueChangeData>();
            if (data.Count >= 1)
            {
                newData.Add(data[0]);
            }
            int delta = 1;
            if (data.Count >= 200)
            {
                delta = Mathf.Max(1, Mathf.RoundToInt(data.Count / (float)200));
            }
            for (int i = 1; i < data.Count; i += delta)
            {
                if (data[i].xValue > data[i - 1].xValue + deltaXforChat)
                {
                    newData.Add(new ValueChangeData(data[i].xValue - deltaXforChat, data[i - 1].yValue));
                }
                newData.Add(data[i]);
            }
            return newData;
        }
        public static List<ValueChangeData> CreateTotalChatData(List<ValueChangeData> data)
        {
            List<ValueChangeData> data0 = new List<ValueChangeData>();
            float add = 0;
            foreach (ValueChangeData value in data)
            {
                add += value.yValue;
                data0.Add(new ValueChangeData(value.xValue, add));
            }
            return CreateLineChatData(data0);
        }
        public static List<ValueChangeData> CreateLineChatData2(List<ValueChangeData> data, float deltaX = 1 / 1080.0f, float deltaXforChat = 1 / 5400.0f)
        {
            List<ValueChangeData> newData = new List<ValueChangeData>();
            if (data.Count >= 1)
            {
                newData.Add(data[0]);
            }
            for (int i = 1; i < data.Count; i++)
            {
                if (data[i].xValue > data[i - 1].xValue + deltaXforChat)
                {
                    newData.Add(new ValueChangeData(data[i].xValue - deltaXforChat, data[i - 1].yValue));
                }

                if (i == data.Count - 1 || (data[i].xValue < data[i + 1].xValue + deltaX + deltaXforChat))
                {
                    newData.Add(new ValueChangeData(data[i].xValue + deltaX, data[i].yValue));
                    newData.Add(new ValueChangeData(data[i].xValue + deltaX + deltaXforChat, 0));
                }
                newData.Add(data[i]);
            }
            return newData;
        }
        public static List<ValueChangeData> NormalizeLineChatData(List<ValueChangeData> data, float Max)
        {
            List<ValueChangeData> data0 = new List<ValueChangeData>();
            foreach (ValueChangeData value in data)
            {
                data0.Add(new ValueChangeData(value.xValue, Mathf.Max(0, value.yValue / Max)));
            }
            return data0;
        }

    }
    public struct UnitStateChangeData
    {
        public int currentFrameCount;
        public int currentFrameReal;
        public Elements.UnitCtrl.ActionState changStateFrom;
        public Elements.UnitCtrl.ActionState changStateTo;
        public string describe;
        public static string[] stateNames = new string[] { "等待", "普攻", "UB", "技能", "走路", "无法行动", "召唤", "死亡", "开局", "失败", "???" };

        public UnitStateChangeData(int currentFrameCount, UnitCtrl.ActionState changStateFrom, UnitCtrl.ActionState changStateTo, string describe = "", int currentFrameReal = 0)
        {
            this.currentFrameCount = currentFrameCount;
            this.changStateFrom = changStateFrom;
            this.changStateTo = changStateTo;
            this.describe = describe;
            this.currentFrameReal = currentFrameReal;
        }
        public string GetMainDescribe()
        {
            return "切换到" + stateNames[(int)changStateTo%stateNames.Length] + "状态";
        }
    }
    public class UnitAbnormalStateChangeData
    {
        public bool isFinish;
        public bool enable;
        public int startFrameCountLogic;
        public int startFrameCountReal;
        public int endFrameCountLogic;
        public int endFrameCountReal;
        public UnitCtrl.eAbnormalState CurrentAbnormalState;
        public float MainValue;
        public float Time;
        public float Duration;
        public int ActionId;
        public float SubValue;

        public float EnergyReduceRate;
        public bool IsEnergyReduceMode;

        public bool IsDamageRelease;
        public bool IsReleasedByDamage;
        public int AbsorberValue;

        public string SkillName;
        public string SourceName;
        //public Skill Skill;
        //public UnitCtrl Source;
        public bool isBuff;
        public int BUFF_Type;
        public string GetDescription()
        {
            if (isBuff)
            {
                return ((Elements.UnitCtrl.BuffParamKind)BUFF_Type).GetDescription() + " " + MainValue;
            }
            else
            {
                return CurrentAbnormalState.GetDescription();
            }
        }
        public void ShowDetail()
        {
            float total = (endFrameCountLogic - startFrameCountLogic) / 60.0f;
            string detail = "开始时间：" + startFrameCountLogic + "\n结束时间：" + endFrameCountLogic + "\n持续时间：" + total + "\n效果：" + GetDescription();
            //MainManager.Instance.WindowConfigMessage(detail, null);
            System.Windows.Forms.MessageBox.Show(detail);
        }
    }

    public class ValueChangeData
    {
        public float xValue { get; set; }
        public float yValue { get; set; }
        public int Index = 0;
        public int hp;
        public int damage;
        public string describe;
        public ValueChangeData() { }
        public ValueChangeData(float x, float y)
        {
            this.xValue = x;
            this.yValue = y;
        }

        public ValueChangeData(float x, float y, int hp, string describe)
        {
            this.xValue = x;
            this.yValue = y;
            this.hp = hp;
            this.describe = describe;
        }
        public ValueChangeData(int index, float x, float y, int hp, string describe)
        {
            Index = index;
            this.xValue = x;
            this.yValue = y;
            this.hp = hp;
            this.describe = describe;
        }

    }
    public class DamageGetData
    {
        public int frame;
        public long totalDamage;
        public long totalDamageExcept;
        public DamageGetData() { }

        public DamageGetData(int frame, long totalDamage, long totalDamageExcept)
        {
            this.frame = frame;
            this.totalDamage = totalDamage;
            this.totalDamageExcept = totalDamageExcept;
        }
    }
    public class UnitSkillExecData
    {
        public enum SkillState
        {
            [Description("正常释放")]
            NORMAL = 0,
            [Description("被取消")]
            CANCEL = 1
        }
        public int unitid;
        public string UnitName;
        public int skillID;
        public SkillState skillState;//0-正常，1-被取消
        public string skillName;
        public int startTime;
        public int startTimeReal;
        public int endTime;
        public int endTimeReal;
        public List<UnitActionExecData> actionExecDatas = new List<UnitActionExecData>();

        public string GetDescribeA()
        {
            return "释放技能" + skillName;
        }
        public void TryAddDamageData(PlayerDamageData damageData)
        {
            var ac = actionExecDatas.Find(a => a.execTimeReal == damageData.frameReal&&a.targetNames.Contains(PCRSettings.Instance.GetUnitNameByID(damageData.target)));
            if (ac == null)
            {
                UnitActionExecData execData = new UnitActionExecData();
                execData.additional = true;
                execData.AddDamageDes(damageData);
                actionExecDatas.Add(execData);
            }
            else
            {
                ac.AddDamageDes(damageData);
            }

        }
    }
    public class UnitActionExecData
    {
        public enum ActionState
        {
            [Description("正常释放")]
            NORMAL = 0,
            [Description("未命中")]
            MISS = 1,
            [Description("被取消")]
            CANCEL_BY_SKILL = 2,
            [Description("被条件分支取消")]
            CANCEL_BY_IFFORALL = 3,
            [Description("被系统取消")]
            CANCEL_BY_COVENT = 4
        }
        public int unitid;
        public int actionID;
        public string actionType;
        public List<string> targetNames = new List<string>();
        public int execTime;
        public int execTimeReal;
        public ActionState result;//0-正常触发，1-MISS，2-被取消，3-被排他条件分支取消
        public string describe;
        public List<int> damageList = new List<int>();
        public List<PlayerDamageData> playerDamages = new List<PlayerDamageData>();
        public bool additional = false;

        public bool CanCombine(UnitActionExecData data)
        {
            /*if (unitid == data.unitid && actionID == data.actionID && actionType == data.actionType && execTime == data.execTime && describe == data.describe)
            {
                return true;
            }*/
            return false;
        }
        public string GetDescribe()
        {
            string names = "";
            for (int i = 0; i < targetNames.Count; i++)
            {
                names += targetNames[i];
                if (i < targetNames.Count - 1)
                {
                    names += "、";
                }
            }            
            return "目标：" + names + "\r\n" + describe;
        }
        public void AddDamageDes(PlayerDamageData damageData)
        {
            playerDamages.Add(damageData);
        }
    }
    /// <summary>
    /// 存档类，保存到excel用
    /// </summary>
    public class GuildTimelineData
    {
        //public GuildSettingData currentSettingData;
        public int currentRandomSeed;
        public GuildPlayerGroupData playerGroupData;
        [JsonIgnore]
        public Dictionary<int, List<UnitStateChangeData>> allUnitStateChangeDic = new Dictionary<int, List<UnitStateChangeData>>();
        [JsonIgnore]
        public Dictionary<int, List<UnitAbnormalStateChangeData>> allUnitAbnormalStateDic = new Dictionary<int, List<UnitAbnormalStateChangeData>>();
        [JsonIgnore]
        public Dictionary<int, List<ValueChangeData>> allUnitHPDic = new Dictionary<int, List<ValueChangeData>>();
        [JsonIgnore]
        public Dictionary<int, List<ValueChangeData>> allUnitTPDic = new Dictionary<int, List<ValueChangeData>>();
        [JsonIgnore]
        public Dictionary<int, List<UnitSkillExecData>> allUnitSkillExecDic = new Dictionary<int, List<UnitSkillExecData>>();
        [JsonIgnore]
        public Dictionary<int, List<PlayerDamageData>> playerUnitDamageDic = new Dictionary<int, List<PlayerDamageData>>();
        [JsonIgnore]
        public List<ValueChangeData> bossDefChangeDic = new List<ValueChangeData>();
        [JsonIgnore]
        public List<ValueChangeData> bossMgcDefChangeDic = new List<ValueChangeData>();
        [JsonIgnore]
        public List<DamageGetData> clanTotalDamageList = new List<DamageGetData>();
        //
        public List<List<float>> UBExecTime = new List<List<float>>();
        
        public List<int[]> AllUnitUBList = new List<int[]>();
        [JsonIgnore]
        public List<RandomData> AllRandomList = new List<RandomData>();
        public int exceptDamage;
        public int backDamage;
        [JsonIgnore]
        public List<byte[]> charImages;
        public string timeLineName = "未命名";
        [JsonIgnore]
        public List<UBDetail> uBDetails;
        public int timeType = 0;//0-逻辑，1-渲染

        public GuildTimelineData()
        {

        }
        public GuildTimelineData(int t)
        {
            playerGroupData = new GuildPlayerGroupData(t);
        }

        public GuildTimelineData(GuildPlayerGroupData playerGroupData, int seed, Dictionary<int, List<UnitStateChangeData>> allUnitStateChangeDic, Dictionary<int, List<UnitAbnormalStateChangeData>> allUnitAbnormalStateDic, Dictionary<int, List<ValueChangeData>> allUnitHPDic, Dictionary<int, List<ValueChangeData>> allUnitTPDic, Dictionary<int, List<UnitSkillExecData>> allUnitSkillExecDic, Dictionary<int, List<PlayerDamageData>> playerUnitDamageDic, List<ValueChangeData> bossDefChangeDic, List<ValueChangeData> bossMgcDefChangeDic, List<RandomData> allRandomList, List<DamageGetData> clanTotalDamageList, List<int[]> allUnitUBList)
        {
            //this.currentSettingData = currentSettingData;
            this.currentRandomSeed = seed;
            this.playerGroupData = playerGroupData;
            this.allUnitStateChangeDic = allUnitStateChangeDic;
            this.allUnitAbnormalStateDic = allUnitAbnormalStateDic;
            this.allUnitHPDic = allUnitHPDic;
            this.allUnitTPDic = allUnitTPDic;
            this.allUnitSkillExecDic = allUnitSkillExecDic;
            this.playerUnitDamageDic = playerUnitDamageDic;
            this.bossDefChangeDic = bossDefChangeDic;
            this.bossMgcDefChangeDic = bossMgcDefChangeDic;
            AllRandomList = allRandomList;
            this.clanTotalDamageList = clanTotalDamageList;
            AllUnitUBList = allUnitUBList;
        }
        public void CreatePro()
        {
            if (timeType==0)
            {
                for (int i = 0; i < AllUnitUBList.Count; i++)
                {
                    int prop = AllUnitUBList[i][2];
                    if (prop > 0)
                    {
                        int idx = playerGroupData.playerData.playrCharacters.FindIndex(x => x.unitId == AllUnitUBList[i][0]);
                        if (idx != -1)
                        {
                            int idx2 = playerGroupData.UBExecTimeData[idx].FindIndex(x => x == AllUnitUBList[i][1]);
                            if (idx2 != -1)
                            {
                                playerGroupData.UBExecTimeData[idx][idx2] += prop / 10.0f;
                            }
                        }
                    }
                }
            }
        }
    }
    public class UBDetail
    {
        public bool isBossUB;
        public UnitData unitData;
        public int UBTime;
        public int Damage;
        public bool Critical;
        public int unitid;
    }
    public class RandomData
    {
        public int id;//第几次调用
        public int frame;
        public int ownerID;
        public int targetID;
        public int actionID;
        //20-恐惧判定
        //19-次数黑暗判定
        //18-清BUFF判定??
        //17-触发器判定,大于则不触发
        //16-不能放UB
        //15-DOT
        //14-沉默
        //13-禁疗，小于则中
        //12-条件分支判定
        //11-减疗，小于则中
        //10-技能随机选择目标判定 (此时target为上限)
        //9-抗性判定，大于等于无效，小于则抵抗，actionid为actiontype
        //8-随机特效判定，actionid为skillid
        //7-魅惑判定,小于则中
        //6-减速加速判定,小于等于则命中,加速必中无视判定
        //5-致盲判定，小于等于则命中
        //4-恐惧判定，小于等于则命中
        //3-黑暗判定，小于则中黑暗
        //2-MISS判定，小于则闪避
        //1-对数盾暴击判定，小于等于则判定暴击（未触发对数盾则无效！）
        //0-暴击判定，小于等于则判定暴击
        public int type;
        public int currentSeed;
        public int randomResult;
        public float targetResult;
        public float criticalDamageRate;
        public RandomData() { }
        public RandomData(UnitCtrl source, UnitCtrl target, int actionid, int type, float targetResult, float criDamage = 0)
        {
            ownerID = source == null ? 0 : source.UnitId;
            targetID = target == null ? 0 : target.UnitId;
            actionID = actionid;
            this.type = type;
            this.targetResult = targetResult;
            criticalDamageRate = criDamage;
        }
        public string GetDescribe()
        {
            string sourceName = PCRSettings.Instance.GetUnitNameByID(ownerID);
            string targetName = PCRSettings.Instance.GetUnitNameByID(targetID);

            switch (type)
            {
                case 0:
                    return sourceName + "对" + targetName + "的暴击判定" + ((float)randomResult <= targetResult * 1000 ? "暴击" : "没暴击");
                case 1:
                    return sourceName + "对" + targetName + "的对数盾暴击判定" + ((float)randomResult <= targetResult * 1000 ? "暴击" : "没暴击");
                case 2:
                    return sourceName + "对" + targetName + "的闪避判定" + ((float)randomResult < targetResult * 1000 ? "MISS" : "命中");
                case 9:
                    return sourceName + "对" + targetName + "的抗性判定" + ((float)randomResult < targetResult * 1000 ? "抵抗" : "不抵抗");
                case 10:
                    return sourceName + "的技能随机选择判定，目标" + randomResult;
                case 15:
                    return sourceName + "对" + targetName + "的DOT判定" + ((float)randomResult < targetResult * 1000 ? "MISS" : "命中");
                default:
                    return sourceName + "对" + targetName + "的其他判定";
            }
        }
        public bool JudgeColored()
        {
            if (actionID >= 100000000)
            {
                int skill = (int)((actionID % 1000) / 100);
                if (skill == 1 && type == 0)
                    return true;
            }
            return false;
        }
    }
    public class UnitStateInputData
    {
        public int from;
        public int fromReal;
        public int to;
        public int toReal;
        public int type;
        public int skillID;
        public UnitSkillExecData skillData;
        [JsonIgnore]
        public System.Action Onclick;

        public UnitStateInputData()
        {

        }
        public UnitStateInputData(int from, int fromReal, int to, int toReal, int type, Action onclick, int skillID = 0)
        {
            this.from = from;
            this.fromReal = fromReal;
            this.to = to;
            this.toReal = toReal;
            this.type = type;
            Onclick = onclick;
            this.skillID = skillID;
        }

    }
}
