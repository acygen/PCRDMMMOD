using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PCRCalculator.Tool
{
    public enum eBattleLogType
    {
        [Description("闪避")]
        MISS = 1,
        [Description("伤害")]
        SET_DAMAGE = 2,
        [Description("异常状态")]
        SET_ABNORMAL = 3,
        [Description("回复")]
        SET_RECOVERY = 4,
        [Description("BUFF")]
        SET_BUFF_DEBUFF = 5,
        [Description("行动状态")]
        SET_STATE = 6,
        [Description("按钮")]
        BUTTON_TAP = 7,
        [Description("TP")]
        SET_ENERGY = 8,
        [Description("伤害变更")]
        DAMAGE_CHARGE = 9,
        [Description("加法赋值")]
        GIVE_VALUE_ADDITIONAL = 10,
        [Description("乘法赋值")]
        GIVE_VALUE_MULTIPLY = 11,
        [Description("战后HP")]
        WAVE_END_HP = 12,
        [Description("伤害统计")]
        WAVE_END_DAMAGE_AMOUNT = 13,
        [Description("BREAK")]
        BREAK = 14,
        [Description("未知")]
        INVALID_VALUE = -1
    }
    public enum eMissLogType
    {
        [Description("闪避攻击")]
        DODGE_ATTACK = 0,
        [Description("闪避攻击（对方被致盲）")]
        DODGE_ATTACK_DARK = 1,
        [Description("闪避减速")]
        DODGE_CHANGE_SPEED = 2,
        [Description("闪避魅惑")]
        DODGE_CHARM = 3,
        [Description("闪避黑暗")]
        DODGE_DARK = 4,
        [Description("闪避DOT")]
        DODGE_SLIP_DAMAGE = 5,
        [Description("闪避沉默")]
        DODGE_SILENCE = 6,
        [Description("处于无敌")]
        DODGE_BY_NO_DAMAGE_MOTION = 7,
        [Description("忽略治疗（己方已经死亡）")]
        MISS_HP0_RECOVER = 8,
        [Description("闪避击退")]
        DODGE_KNOCK = 9,
        [Description("闪避其他")]
        DODGE_DAMAGE_BY_BEHAVIOUR = 10,
        [Description("未知")]
        INVALID_VALUE = -1
    }
    public enum eSetEnergyType
    {
        [Description("剧情")]
        BY_STORY_TIME_LINE = 0,
        [Description("攻击")]
        BY_ATK = 1,
        [Description("充能")]
        BY_CHARGE_SKILL = 2,
        [Description("改变行动模式")]
        BY_MODE_CHANGE = 3,
        [Description("初始化")]
        INITIALIZE = 4,
        [Description("受伤")]
        BY_SET_DAMAGE = 5,
        [Description("使用UB")]
        BY_USE_SKILL = 6,
        [Description("战后回复")]
        BATTLE_RECOVERY = 7,
        [Description("击杀单位")]
        KILL_BONUS = 8,
        [Description("调用函数")]
        BY_CHANGE_ENERGY = 9,
        [Description("未知")]
        INVALID_VALUE = -1
    }

}
