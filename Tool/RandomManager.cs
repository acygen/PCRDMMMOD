using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using PCRCalculator.Tool;

namespace PCRCalculator
{
    public class RandomManager
    {
        
    }
    public class GuildRandomData
    {
        public string DataName = "默认世界线";
        public bool UseFixedRandomSeed = true;
        public int RandomSeed = 666;
        public bool ForceNoCritical_player;
        public bool ForceNoCritical_enemy;
        public bool ForceIgnoreDodge_player;
        public bool ForceIgnoreDodge_enemy;
        public bool ForceCritical_player;
        public List<GuildRandomSpecialData> randomSpecialDatas = new List<GuildRandomSpecialData>();

        public GuildRandomData() { }
        public GuildRandomData(string dataName)
        {
            DataName = dataName;
        }
        public string GetDescribe()
        {
            string des = UseFixedRandomSeed ? "随机固定" : "正常随机";
            if (ForceNoCritical_player || ForceNoCritical_enemy)
            {
                if (ForceNoCritical_player)
                    des += "-" + (ForceNoCritical_enemy ? "双" : "己") + "方不暴击";
                else
                    des += "-敌方不暴击";
            }
            if (ForceIgnoreDodge_enemy || ForceIgnoreDodge_player)
            {
                if (ForceIgnoreDodge_player)
                    des += "-" + (ForceIgnoreDodge_enemy ? "双" : "己") + "方伤害必中";
                else
                    des += "-敌方伤害必中";
            }
            return des;
        }
        public bool TryJudgeRandomSpecialSetting(Elements.UnitCtrl source, Elements.UnitCtrl target, Elements.Skill skill, Elements.eActionType actionType, int currrentFrame, out float randomResult)
        {
            randomResult = 0;
            bool result = false;
            if (randomSpecialDatas == null || randomSpecialDatas.Count <= 0)
            {
                return result;
            }
            foreach (var a in randomSpecialDatas)
            {
                if (a.TryJudgeRandomSpecialSetting(source, target, skill, actionType, currrentFrame, out float randomOut))
                {
                    randomResult = randomOut;
                    result = true;
                }
            }
            return result;
        }
    }
    public class GuildRandomSpecialData
    {
        public bool fixTimeExec;
        public int startFream;
        public int endFream;
        public bool fixCountExec;
        public int countEcexNum;
        public enum UnitType
        {
            [Description("BOSS")]
            BOSS = 0,
            [Description("己方一号位")]
            PLAYER1 = 1,
            [Description("己方二号位")]
            PLAYER2 = 2,
            [Description("己方三号位")]
            PLAYER3 = 3,
            [Description("己方四号位")]
            PLAYER4 = 4,
            [Description("己方五号位")]
            PLAYER5 = 5,
            [Description("己方全体")]
            ALLPLAYER = 6
        }
        public UnitType sourceNum;
        public enum skillNameType
        {
            [Description("UB")]
            UB = 0,
            [Description("技能1")]
            SKILL1 = 1,
            [Description("技能2")]
            SKILL2 = 2,
            [Description("普攻")]
            ATK = 3,
            [Description("技能3")]
            SKILL3 = 13,
            [Description("技能4")]
            SKILL4 = 14,
            [Description("技能5")]
            SKILL5 = 15,
            [Description("技能6")]
            SKILL6 = 16,
            [Description("技能7")]
            SKILL7 = 17,
            [Description("所有")]
            ALL = 100,


        }
        public skillNameType sourceSkillNum;
        public enum skillType
        {
            [Description("攻击类")]
            ATK = 0,
            [Description("减速类")]
            ChangeSpeed = 1,
            [Description("咕咕咕")]
            KNOCK = 2,
            [Description("DOT伤害")]
            SLIP_DAMAGE = 3,
            [Description("致盲黑暗类")]
            BLIND = 4

        }
        public skillType sourceSkillType;
        public UnitType targetNum;
        public enum ResultType
        {
            [Description("正常随机")]
            RANDOM = 0,
            [Description("必中")]
            FORCE_ACC = 1,
            [Description("必爆")]
            FORCE_CRI = 2,
            [Description("必MISS")]
            FORCE_MISS = 3,
            [Description("必不爆")]
            FORCE_NORMAL = 4
        }
        public ResultType resuleType;

        public GuildRandomSpecialData()
        {

        }

        public GuildRandomSpecialData(bool fixTimeExec, int startFream, int endFream, bool fixCountExec, int countEcexNum, UnitType sourceNum, skillNameType sourceSkillNum, skillType sourceSkillType, UnitType targetNum, ResultType resuleType)
        {
            this.fixTimeExec = fixTimeExec;
            this.startFream = startFream;
            this.endFream = endFream;
            this.fixCountExec = fixCountExec;
            this.countEcexNum = countEcexNum;
            this.sourceNum = sourceNum;
            this.sourceSkillNum = sourceSkillNum;
            this.sourceSkillType = sourceSkillType;
            this.targetNum = targetNum;
            this.resuleType = resuleType;
        }
        public string GetDescribe()
        {
            string des = "";
            if (fixTimeExec)
            {
                des += startFream + "-" + endFream + "帧" + "-";
            }
            if (fixCountExec)
            {
                des += "第" + countEcexNum + "次";
            }
            des += sourceNum.GetDescription() + "的" + sourceSkillNum.GetDescription();
            des += "对" + targetNum.GetDescription() + "的" + sourceSkillType.GetDescription() + "效果";
            des += resuleType.GetDescription();
            return des;
        }
        public bool TryJudgeRandomSpecialSetting(Elements.UnitCtrl source, Elements.UnitCtrl target, Elements.Skill skill, Elements.eActionType actionType, int currrentFrame, out float randomResult)
        {
            randomResult = 0;
            return false;
            /*randomResult = 0;
            if (fixTimeExec)
                if (currrentFrame < startFream || currrentFrame > endFream)
                {
                    return false;
                }
            if (fixCountExec)
            {
                if (source.MySkillExecDic.TryGetValue(skill.SkillId, out int count))
                {
                    if (count != countEcexNum)
                        return false;
                }
                else
                    return false;
            }
            switch (sourceNum)
            {
                case UnitType.BOSS:
                    if (!source.IsBoss)
                    {
                        return false;
                    }
                    break;
                case UnitType.PLAYER1:
                case UnitType.PLAYER2:
                case UnitType.PLAYER3:
                case UnitType.PLAYER4:
                case UnitType.PLAYER5:
                    if (source.IsBoss)
                    {
                        return false;
                    }
                    if ((int)sourceNum - 1 != source.posIdx)
                    {
                        return false;
                    }
                    break;
                case UnitType.ALLPLAYER:
                    if (source.IsBoss)
                    {
                        return false;
                    }
                    break;
            }
            if (skill == null)
            {
                return false;
            }
            if (!source.JudgeIsTargetSkill(skill.SkillId, (int)sourceSkillNum))
            {
                return false;
            }
            switch (sourceSkillType)
            {
                case skillType.ATK:
                    if (actionType != Elements.eActionType.ATTACK)
                    {
                        return false;
                    }
                    break;
                case skillType.ChangeSpeed:
                    if (actionType != Elements.eActionType.CHANGE_SPEED)
                    {
                        return false;
                    }
                    break;
                case skillType.KNOCK:
                    if (actionType != Elements.eActionType.KNOCK)
                    {
                        return false;
                    }
                    break;
                case skillType.SLIP_DAMAGE:
                    if (actionType != Elements.eActionType.SLIP_DAMAGE)
                    {
                        return false;
                    }
                    break;
                case skillType.BLIND:
                    if (actionType != Elements.eActionType.BLIND)
                    {
                        return false;
                    }
                    break;
            }
            switch (targetNum)
            {
                case UnitType.BOSS:
                    if (!target.IsBoss)
                    {
                        return false;
                    }
                    break;
                case UnitType.PLAYER1:
                case UnitType.PLAYER2:
                case UnitType.PLAYER3:
                case UnitType.PLAYER4:
                case UnitType.PLAYER5:
                    if (target.IsBoss)
                    {
                        return false;
                    }
                    if ((int)targetNum - 1 != target.posIdx)
                    {
                        return false;
                    }
                    break;
                case UnitType.ALLPLAYER:
                    if (target.IsBoss)
                    {
                        return false;
                    }
                    break;
            }
            switch (resuleType)
            {
                case ResultType.RANDOM:
                    return false;
                case ResultType.FORCE_ACC:
                case ResultType.FORCE_NORMAL:
                    randomResult = 1;
                    break;
                case ResultType.FORCE_MISS:
                case ResultType.FORCE_CRI:
                    randomResult = 0;
                    break;


            }

            return true;*/
        }

    }

}