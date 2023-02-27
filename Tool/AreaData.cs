using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elements;
using System.Reflection;
using HarmonyLib;

namespace PCRCalculator.Tool
{
    public class AreaApply
    {
        public DataHead data_headers = new DataHead(1);
        public List<object> data = new List<object>();
    }
    public class AreaInfo
    {
        public DataHead data_headers = new DataHead(1);
        public AreaInfoData data = new AreaInfoData();
    }
    public class AreaInfoData
    {
        public Info arena_info = new Info();
        public Reward reward_info = new Reward();
        public bool is_time_reward_max = false;
        public int reward_hour_num = 8;
        public List<OppoentData> search_opponent = new List<OppoentData>();
    }
    public class Info
    {
        public int max_battle_number = 5;
        public int battle_number = 5;
        public long interval_end_time = 1609572121;
        public int highest_rank = 1;
        public int season_highest_rank = 1;
        public int yesterday_defend_number = 0;
        public int group = 1;
        public long group_moving_release_time = 604800;
        public int rank = 11;
    }
    public class Reward
    {
        public int id = 900003;
        public int type = 2;
        public int count = 0;
    }
    public class OppoentData
    {
        public int rank = 1;
        public int viewer_id = 123456;
        public string user_name = "佑树（在挖）";
        public int team_level = 999;
        public Favour favorite_unit = new Favour();
        public Emblem emblem = new Emblem();
        public List<UnitDataLi> arena_deck = new List<UnitDataLi>();
        public OppoentData() { }
        public void SetUnit(int rank,string name,List<int> unitids)
        {
            this.rank = rank;
            user_name = name;
            viewer_id = 100000 + rank;
            arena_deck.Clear();
            foreach (int id in unitids)
            {
                arena_deck.Add(new UnitDataLi(id));
            }
        }
    }
    public class AreaData
    {
        public List<UnitDataLi> user_unit_list = new List<UnitDataLi>();
        public List<UnitDataLi> opponent_unit_list = new List<UnitDataLi>();
        public int seed;
        public int is_challenge = 1;
        public AreaData()
        {

        }
        public AreaData(int seed)
        {
            this.seed = seed;
            user_unit_list = new List<UnitDataLi>();
            opponent_unit_list = new List<UnitDataLi>();
        }
        /*public int EditUnitData(UnitData unitData,bool isUser,bool add)
        {
            List<UnitDataLi> list = isUser ? user_unit_list : opponent_unit_list;
            int idx = list.FindIndex(a => a.id == unitData.UnitId);
            if (add)
            {
                if (idx<0)
                {
                    if (list.Count <= 4)
                    {
                        list.Add(new UnitDataLi(unitData));
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    list[idx] = new UnitDataLi(unitData);
                    return 1;
                }
            }
            else
            {
                if (idx < 0)
                    return 1;
                else
                {
                    list.RemoveAt(idx);
                    return 1;
                }
            }
        }*/
        /*public AreaData(List<UnitData> userList,List<UnitData> opponentList,int seed)
        {
            user_unit_list = new List<UnitDataLi>();
            foreach (var unit in userList)
                user_unit_list.Add(new UnitDataLi(unit));
            opponent_unit_list = new List<UnitDataLi>();
            foreach (var unit in opponentList)
                opponent_unit_list.Add(new UnitDataLi(unit));
            this.seed = seed;
        }*/
    }
    public class Favour
    {
        public int id = 100101;
        public int unit_rarity = 5;
        public int unit_level = 100;
        public int promotion_level = 21;
        public SkinData00 skin_data = new SkinData00();
    }
    public class Emblem
    {
        public int emblem_id = 10000001;
        public int ex_value = 0;
    }
    public class UnitDataS
    {
        public int id;

        public int get_time;

        public int unit_rarity;
        //public int battle_rarity = 3;
        public int unit_level;

        public int unit_exp;
        public int promotion_level;

        public List<SkillLevelInfo> union_burst;

        public List<SkillLevelInfo> main_skill;

        public List<SkillLevelInfo> ex_skill;

        public List<SkillLevelInfo> free_skill;
        public List<EquipSlot> equip_slot;

        public List<EquipSlot> unique_equip_slot;
        public List<ExSolt> ex_equip_slot;
        public List<ExSolt> cb_ex_equip_slot;
        public int power = -1;
        public SkinData skin_data;
        public Unlock6Item unlock_rarity_6_item = new Unlock6Item(0);
        public int favorite_flag = 0;
        public UnitDataS()
        {
            ex_equip_slot = new List<ExSolt>();
            cb_ex_equip_slot = new List<ExSolt>();
        }
        public void InitExSolt()
        {
            ex_equip_slot = new List<ExSolt>();
            cb_ex_equip_slot = new List<ExSolt>();
            for (int i = 1; i <= 3; i++)
            {
                ExSolt exSolt = new ExSolt(i);
                ex_equip_slot.Add(exSolt);
                cb_ex_equip_slot.Add(exSolt);
            }
        }
        public UnitDataS(int id, int rarity):this()
        {
            this.id = id;
            unit_rarity = rarity;
            //battle_rarity = rarity;
            unit_level = 1;
            promotion_level = 1;
            CreateSkillLevels();
            CreateEquipment();
            //CreateLoveAdd(unitData);
            get_time = 1593394543;
            skin_data = new SkinData();
        }
        public void ChangeAll(int rarity, int lv, int ub, int main1, int main2, int ex, int rank,
            int l1 = -1, int l2 = -1, int l3 = -1, int l4 = -1, int l5 = -1, int l6 = -1, int eq = 0)
        {
            //Cute.ClientLog.AccumulateClientLog(id + "->ub" + ub + "lv" + lv + "ex" + ex);
            unit_rarity = rarity;
            //battle_rarity = rarity;
            /*if (battle_rarity == 6 || battle_rarity <= 2)
            {
                unit_rarity = battle_rarity;
            }
            else
                unit_rarity = 5;*/
            unit_level = lv;
            promotion_level = rank;
            CreateEquipment(l1, l2, l3, l4, l5, l6, eq);
            CreateSkillLevels();
            ChangeSkillLvs(ub, main1, main2, ex);
            //Cute.ClientLog.AccumulateClientLog(union_burst[0].skill_level + "-lv-" + main_skill[0].skill_level + "-ex-" + ex_skill[0].skill_level);
        }
        public void ChangeSkillLvs(int ub, int main1, int main2, int ex)
        {
            union_burst[0].skill_level = ub;
            main_skill[0].skill_level = main1;
            main_skill[1].skill_level = main2;
            ex_skill[0].skill_level = ex;
        }
        public void CheckAndCreat()
        {
            if (union_burst.Count == 0 || main_skill.Count == 0 || ex_skill.Count == 0)
            {
                CreateSkillLevels();
            }
        }
        public void CreateSkillLevels()
        {
            Elements.MasterUnitSkillData.UnitSkillData unitSkillData = Elements.ManagerSingleton<Elements.MasterDataManager>.Instance.masterUnitSkillData.Get(id);
            
            bool mainPlus = IsUniqueOn();
            MethodInfo method = typeof(UnitUtility).GetMethod("createSkillLevel", BindingFlags.Static | BindingFlags.NonPublic);
            List<Elements.SkillLevelInfo> mainSkill =(List<Elements.SkillLevelInfo>)method.Invoke( null,new object[] { unitSkillData.MainSkillIds, promotion_level, unit_level, true, mainPlus, unitSkillData.MainSkillEvolutionIds });
            main_skill = ChangeData(mainSkill);
            bool UBplus = unit_rarity == 6;
            var ubid = UBplus ? unitSkillData.UnionBurstEvolutionIds : unitSkillData.UnionBurstIds;
            union_burst = ChangeData((List<Elements.SkillLevelInfo>)method.Invoke(null, new object[] { ubid, promotion_level, unit_level,false,false,null })); 
            //ChangeData(UnitUtility.createSkillLevel(ubid, promotion_level, unit_level));
            bool isEXplus = unit_rarity >= 5;
            var exid = isEXplus ? unitSkillData.ExSkillEvolutionIds : unitSkillData.ExSkillIds;
            ex_skill = ChangeData((List<Elements.SkillLevelInfo>)method.Invoke(null, new object[] { exid, promotion_level, unit_level ,false,false,null}));
                //UnitUtility.createSkillLevel(exid, promotion_level, unit_level));
            free_skill = ChangeData((List<Elements.SkillLevelInfo>)method.Invoke(null, new object[] { unitSkillData.SpSkillIds, promotion_level, union_burst[0].skill_level, true, mainPlus, unitSkillData.SpSkillEvolutionIds }));
            //UnitUtility.createSkillLevel(unitSkillData.SpSkillIds, promotion_level, union_burst[0].skill_level,true, mainPlus, unitSkillData.SpSkillEvolutionIds));

        }
        public List<SkillLevelInfo> ChangeData(List<Elements.SkillLevelInfo> skills)
        {
            List<SkillLevelInfo> result = new List<SkillLevelInfo>();
            foreach(var sk in skills)
            {
                SkillLevelInfo info = new SkillLevelInfo(sk.SkillLevel, sk.SkillId);
                result.Add(info);
            }
            return result;
        }
        public void CreateEquipment(int l1=-1,int l2=-1,int l3=-1,int l4=-1,int l5=-1,int l6=-1,int eq=0)
        {
            var List = ManagerSingleton<MasterDataManager>.Instance.masterUnitPromotion.GetAllUnitPromotion(id);
            var prom = List.Find(a => a.promotion_level == promotion_level);
            equip_slot = new List<EquipSlot>();

            equip_slot.Add(new EquipSlot(prom.equip_slot_1, l1));
            equip_slot.Add(new EquipSlot(prom.equip_slot_2, l2));
            equip_slot.Add(new EquipSlot(prom.equip_slot_3, l3));
            equip_slot.Add(new EquipSlot(prom.equip_slot_4, l4));
            equip_slot.Add(new EquipSlot(prom.equip_slot_5, l5));
            equip_slot.Add(new EquipSlot(prom.equip_slot_6, l6));

            MasterUnitUniqueEquip.UnitUniqueEquip unitUniqueEquip = ManagerSingleton<MasterDataManager>.Instance.masterUnitUniqueEquip.Get(id);

            unique_equip_slot = new List<EquipSlot>();
            if (eq > 0&&unitUniqueEquip!=null)
            {
                unique_equip_slot.Add(new EquipSlot(unitUniqueEquip.equip_id, eq));
            }
        }
        public void CreateExp()
        {
            //unit_exp = 0;
            var expUnit = ManagerSingleton<MasterDataManager>.Instance.masterExperienceUnit;
            UnitExpTable unitExpTable = expUnit[unit_level];
            unit_exp = unitExpTable.total_exp + (int)(unitExpTable.next_exp * 0.9f);
        }
        public int[] GetEquipList()
        {
            var  Result = new int[6] { -1, -1, -1, -1, -1, -1 };
            for(int i = 0; i < 6; i++)
            {
                if (equip_slot.Count > i)
                {
                    Result[i] = equip_slot[i].GetLv();
                }
            }
            return Result;
        }
        public bool IsUniqueOn()
        {
            if (unique_equip_slot == null || unique_equip_slot.Count == 0)
                return false;
            return unique_equip_slot[0].is_slot > 0;
        }

    }
    public class Unlock6Item
    {
        public int quest_clear = 0;
        public int slot_1=0;
        public int slot_2=0;
        public int slot_3=0;
        public Unlock6Item() { }
        public Unlock6Item(int a)
        {

        }
    }
    public class AreaFinish
    {
        public DataHead data_headers = new DataHead(1);
        public AreaFinishData data = new AreaFinishData();
    }
    public class AreaFinishData
    {
        public Info arena_info = new Info();
        public int old_record = 11;
        public int new_record = 11;
    }
    public class AreaSearch
    {
        public DataHead data_headers = new DataHead(1);
        public AreaSearchData data = new AreaSearchData();
    }
    public class AreaSearchData
    {
        public List<OppoentData> search_opponent = new List<OppoentData>();
    }
    public class UnitDataLi
    {
        public int id;

        public int get_time;

        public int unit_rarity;

        public int unit_level;

        public int unit_exp;
        public int promotion_level;

        public List<SkillLevelInfo> union_burst;

        public List<SkillLevelInfo> main_skill;

        public List<SkillLevelInfo> ex_skill;

        public List<SkillLevelInfo> free_skill;

        public List<EquipSlot> equip_slot;

        public List<EquipSlot> unique_equip_slot;
        //public int power = -1;
        //public UnitParam UnitParam;

        public StatusParamShort bonus_param;
        public SkinData skin_data = new SkinData();

        public UnitDataLi()
        {

        }
        public UnitDataLi(Elements.MasterEnemyParameter.EnemyParameter enemydata)
        {
            id = enemydata.unit_id;
            unit_rarity = enemydata.rarity;
            unit_level = enemydata.level;
            promotion_level = enemydata.promotion_level;
            get_time = 0;
            CreateSkillLevels(enemydata);
        }
        /*public UnitDataLi(UnitData unitData)
        {
            id = unitData.unitId;
            unit_rarity = unitData.rarity;
            unit_level = unitData.level;
            promotion_level = unitData.rank;
            CreateSkillLevels(unitData);
            CreateEquipment(unitData);
            CreateLoveAdd(unitData);
            get_time = 1593394543 + id;
            skin_data = new SkinData();
        }*/
        /*public void CreateSkillLevels(UnitData unitData)
        {
            var raData = MainManager.Instance.UnitRarityDic[id];
            UnitSkillData skillData = raData.skillData;
            power = (int)Math.Round(raData.GetPowerValue(unitData));
            int[] skillIDs = skillData.GetSkillList(unitData);
            union_burst = new List<SkillLevelInfo> { new SkillLevelInfo(unitData.skillLevel[0],skillIDs[0]) };
            main_skill = new List<SkillLevelInfo>
            {
                new SkillLevelInfo(unitData.skillLevel[1],skillIDs[1]),
                new SkillLevelInfo(unitData.skillLevel[2],skillIDs[2])
            };
            ex_skill = new List<SkillLevelInfo> { new SkillLevelInfo(unitData.skillLevel[3], skillIDs[3]) };
            free_skill = new List<SkillLevelInfo>
            {
                new SkillLevelInfo(unitData.skillLevel[0],skillIDs[4]),
                new SkillLevelInfo(unitData.skillLevel[0],skillIDs[5])
            };
        }*/
        public void CreateSkillLevels(Elements.MasterEnemyParameter.EnemyParameter enemydata)
        {
            var SkillData = Elements.ManagerSingleton<Elements.MasterDataManager>.Instance.masterUnitSkillData[enemydata.unit_id];
            union_burst = new List<SkillLevelInfo> { new SkillLevelInfo(enemydata.union_burst_level, SkillData.UnionBurstIds[0]) };
            main_skill = new List<SkillLevelInfo>
            {
                new SkillLevelInfo(enemydata.main_skill_lv_1,SkillData.main_skill_1),
                new SkillLevelInfo(enemydata.main_skill_lv_2,SkillData.main_skill_2),
                new SkillLevelInfo(enemydata.main_skill_lv_3,SkillData.main_skill_3),
                new SkillLevelInfo(enemydata.main_skill_lv_4,SkillData.main_skill_4),
                new SkillLevelInfo(enemydata.main_skill_lv_5,SkillData.main_skill_5),
                new SkillLevelInfo(enemydata.main_skill_lv_6,SkillData.main_skill_6),
                new SkillLevelInfo(enemydata.main_skill_lv_7,SkillData.main_skill_7),
                new SkillLevelInfo(enemydata.main_skill_lv_8,SkillData.main_skill_8),
                new SkillLevelInfo(enemydata.main_skill_lv_9,SkillData.main_skill_9),
                new SkillLevelInfo(enemydata.main_skill_lv_10,SkillData.main_skill_10)
            };
            ex_skill = new List<SkillLevelInfo> 
            {
                new SkillLevelInfo(enemydata.ex_skill_lv_1,SkillData.ex_skill_1),
                new SkillLevelInfo(enemydata.ex_skill_lv_2,SkillData.ex_skill_2),
                new SkillLevelInfo(enemydata.ex_skill_lv_3,SkillData.ex_skill_3),
                new SkillLevelInfo(enemydata.ex_skill_lv_4,SkillData.ex_skill_4),
                new SkillLevelInfo(enemydata.ex_skill_lv_5,SkillData.ex_skill_5),
            };
            free_skill = new List<SkillLevelInfo>();

        }
               
        public UnitDataLi(int unitId)
        {
            UserData instance = Singleton<UserData>.Instance;
            var unitParameter = instance.UnitParameterDictionary[unitId];
            var unit = unitParameter.UniqueData;// CreateRarityUpUnitDataForView
            //var unit2 = new UnitData()
            //UnitUtility.CalcParamAndSkill(unit, null, _isPowerLocal: true);
            id = unit.Id;
            get_time = 0;
            unit_rarity = unit.UnitRarity;
            unit_level = unit.UnitLevel;
            unit_exp = 0;
            promotion_level = (int)unit.PromotionLevel;
            union_burst = ChangeData(unit.UnionBurst);
            main_skill = ChangeData(unit.MainSkill);
            ex_skill = ChangeData(unit.ExSkill);
            free_skill = ChangeData(unit.FreeSkill);
            equip_slot = ChangeData(unit.EquipSlot,false,promotion_level);
            unique_equip_slot = ChangeData(unit.UniqueEquipSlot,true,1);
            bonus_param = new StatusParamShort(unit.BonusParam);

        }
        public List<SkillLevelInfo> ChangeData(List<Elements.SkillLevelInfo> skills)
        {
            List<SkillLevelInfo> result = new List<SkillLevelInfo>();
            foreach (var sk in skills)
            {
                SkillLevelInfo info = new SkillLevelInfo(sk.SkillLevel, sk.SkillId);
                result.Add(info);
            }
            return result;
        }
        public List<EquipSlot> ChangeData(List<Elements.EquipSlot> equips,bool isunique,int rank)
        {
            List<EquipSlot> result = new List<EquipSlot>();
            foreach (var sk in equips)
            {
                EquipSlot info = new EquipSlot(sk.Id,sk.IsSlot,sk.EnhancementLevel,true,rank);
                result.Add(info);
            }
            return result;
        }

    }

    public class UnitDataC
    {
        public int id;

        //public int get_time;

        public int unit_rarity;

        public int unit_level;

        public int unit_exp;
        public int promotion_level;

        public List<SkillLevelInfo> union_burst;

        public List<SkillLevelInfo> main_skill;

        public List<SkillLevelInfo> ex_skill;

        public List<SkillLevelInfo> free_skill;

        public List<EquipSlot> equip_slot;
        public int resist_status_id;

        //public List<EquipSlot> unique_equip_slot;
        //public int power = -1;
        public UnitParam UnitParam;

        //public StatusParamShort BonusParam;
        public SkinData skin_data;

        public UnitDataC()
        {

        }
        public UnitDataC(Elements.MasterEnemyParameter.EnemyParameter enemydata)
        {
            id = enemydata.enemy_id;
            unit_rarity = enemydata.rarity;
            unit_level = enemydata.level;
            promotion_level = enemydata.promotion_level;
            //get_time = 0;
            CreateSkillLevels(enemydata);
            skin_data = new SkinData();
            UnitParam = new UnitParam(new  StatusParamShort(enemydata));
            equip_slot = new List<EquipSlot>();
            resist_status_id = enemydata.resist_status_id;
        }       
        public void CreateSkillLevels(Elements.MasterEnemyParameter.EnemyParameter enemydata)
        {
            var SkillData = Elements.ManagerSingleton<Elements.MasterDataManager>.Instance.masterUnitSkillData[enemydata.unit_id];
            union_burst = new List<SkillLevelInfo> { new SkillLevelInfo(enemydata.union_burst_level, SkillData.UnionBurstIds[0]) };
            main_skill = new List<SkillLevelInfo>
            {
                new SkillLevelInfo(enemydata.main_skill_lv_1,SkillData.main_skill_1),
                new SkillLevelInfo(enemydata.main_skill_lv_2,SkillData.main_skill_2),
                new SkillLevelInfo(enemydata.main_skill_lv_3,SkillData.main_skill_3),
                new SkillLevelInfo(enemydata.main_skill_lv_4,SkillData.main_skill_4),
                new SkillLevelInfo(enemydata.main_skill_lv_5,SkillData.main_skill_5),
                new SkillLevelInfo(enemydata.main_skill_lv_6,SkillData.main_skill_6),
                new SkillLevelInfo(enemydata.main_skill_lv_7,SkillData.main_skill_7),
                new SkillLevelInfo(enemydata.main_skill_lv_8,SkillData.main_skill_8),
                new SkillLevelInfo(enemydata.main_skill_lv_9,SkillData.main_skill_9),
                new SkillLevelInfo(enemydata.main_skill_lv_10,SkillData.main_skill_10)
            };
            ex_skill = new List<SkillLevelInfo>
            {
                new SkillLevelInfo(enemydata.ex_skill_lv_1,SkillData.ex_skill_1),
                new SkillLevelInfo(enemydata.ex_skill_lv_2,SkillData.ex_skill_2),
                new SkillLevelInfo(enemydata.ex_skill_lv_3,SkillData.ex_skill_3),
                new SkillLevelInfo(enemydata.ex_skill_lv_4,SkillData.ex_skill_4),
                new SkillLevelInfo(enemydata.ex_skill_lv_5,SkillData.ex_skill_5),
            };
            free_skill = new List<SkillLevelInfo>();

        }
        
        
    }

    public class SkillLevelInfo
    {
        public int skill_level;
        public int skill_id;
        public SkillLevelInfo()
        {

        }
        public SkillLevelInfo(int skill_level, int skill_id)
        {
            this.skill_level = skill_level;
            this.skill_id = skill_id;
        }
    }
    public class EquipSlot
    {
        public int id;
        public int is_slot;
        public int enhancement_level;
        public int enhancement_pt;
        //public int rank;
        public EquipSlot()
        {

        }
        public EquipSlot(int id,int level)
        {
            this.id = level >= 0 ? id : 999999;
            is_slot = level >= 0 ? 1 : 0;
            enhancement_level = level >= 0 ? level : 0;
        }
        public EquipSlot(int id,bool is_slot,int level,bool isUnique,int promotionLevel)
        {
            this.id = id;
            this.is_slot = is_slot ? 1 : 0;
            this.enhancement_level = is_slot?level:1;
            if (enhancement_level > 0 && is_slot)
            {
                MasterDataManager instance = ManagerSingleton<MasterDataManager>.Instance;
                if (isUnique)
                {
                    //MasterUniqueEquipmentData masterUniqueEquipmentData = instance.masterUniqueEquipmentData;
                    //MasterUniqueEquipmentData.UniqueEquipmentData uniqueEquipmentData = masterUniqueEquipmentData.Get(num3);
                    MasterUniqueEquipmentEnhanceData masterUniqueEquipmentEnhanceData = instance.masterUniqueEquipmentEnhanceData;
                    var data = masterUniqueEquipmentEnhanceData.Get(promotionLevel, enhancement_level);
                    if (data != null)
                        enhancement_pt = data.TotalPoint;
                }
                else
                {
                    MasterEquipmentEnhanceData masterEquipmentEnhanceData = instance.masterEquipmentEnhanceData;
                    var data2 = masterEquipmentEnhanceData.Get(promotionLevel, enhancement_level);
                    if (data2 != null)
                        enhancement_pt = data2.TotalPoint;
                }
            }
        }
        public int GetLv()
        {
            if (is_slot > 0)
                return enhancement_level;
            return -1;

        }
    }
    public class ExSolt
    {
        public int ex_equipment_id = 0;
        public int serial_id = 0;
        public int enhancement_pt = 0;
        public int slot;
        public ExSolt()
        {
        }
        public ExSolt(int i)
        {
            slot = i;
        }
    }

    public class StatusParamShort
    {
        public long hp;
        public int atk;
        public int def;
        public int matk;
        public int mdef;
        public int crt;
        public int mcrt;
        public int hrec;
        public int erec;
        public int hrec_rate;
        public int pnt;
        public int mpnt;
        public int life_steal;
        public int dodge;
        public int erec_rate;
        public int ered_rate;
        public int accuracy;

        public StatusParamShort()
        {

        }
        public StatusParamShort(Elements.MasterEnemyParameter.EnemyParameter enemydata)
        {
            hp = enemydata.hp;
            atk = enemydata.atk;
            def = enemydata.def;
            matk = enemydata.magic_str;
            mdef = enemydata.magic_def;
            crt = enemydata.physical_critical;
            mcrt = enemydata.magic_critical;
            hrec = enemydata.wave_hp_recovery;
            erec = enemydata.wave_energy_recovery;
            hrec_rate = enemydata.energy_recovery_rate;
            pnt = enemydata.physical_penetrate;
            mpnt = enemydata.magic_penetrate;
            life_steal = enemydata.life_steal;
            dodge = enemydata.dodge;
            erec_rate = enemydata.energy_recovery_rate;
            ered_rate = enemydata.energy_reduce_rate;
            accuracy = enemydata.accuracy;
        }
        public StatusParamShort(Elements.StatusParamShort enemydata)
        {
            if (enemydata != null)
            {
                hp = enemydata.Hp;
                atk = enemydata.Atk;
                def = enemydata.Def;
                matk = enemydata.Matk;
                mdef = enemydata.Mdef;
                crt = enemydata.Crt;
                mcrt = enemydata.Mcrt;
                hrec = enemydata.Hrec;
                erec = enemydata.Erec;
                hrec_rate = enemydata.HrecRate;
                pnt = enemydata.Pnt;
                mpnt = enemydata.Mpnt;
                life_steal = enemydata.LifeSteal;
                dodge = enemydata.Dodge;
                erec_rate = enemydata.ErecRate;
                ered_rate = enemydata.EredRate;
                accuracy = enemydata.Accuracy;
            }
        }
        /*public StatusParamShort(BaseData baseData)
        {
            hp = (int)baseData.Hp;
            atk = (int)baseData.Atk;
            def = (int)baseData.Def;
            matk = (int)baseData.Magic_str;
            mdef = (int)baseData.Magic_def;
            crt = (int)baseData.Physical_critical;
            mcrt = (int)baseData.Magic_critical;
            hrec = (int)baseData.Wave_hp_recovery;
            erec = (int)baseData.Wave_energy_recovery;
            hrec_rate = (int)baseData.Hp_recovery_rate;
            pnt = (int)baseData.Physical_penetrate;
            mpnt = (int)baseData.Magic_penetrate;
            life_steal = (int)baseData.Life_steal;
            dodge = (int)baseData.Dodge;
            erec_rate = (int)baseData.Energy_recovery_rate;
            ered_rate = (int)baseData.Enerey_reduce_rate;
            accuracy = (int)baseData.Accuracy;

        }*/
    }
    public class UnitParam
    {
        public StatusParamShort base_param;
        public StatusParamShort equip_param;
        public UnitParam() { }
        public UnitParam(StatusParamShort base_p)
        {
            base_param = base_p;
            equip_param = new StatusParamShort();
        }

        public UnitParam(StatusParamShort base_param, StatusParamShort equip_param) : this(base_param)
        {
            this.equip_param = equip_param;
        }
    }
    public class SkinData
    {
        public int icon_skin_id = 0;
        public int sd_skin_id = 0;
        public int still_skin_id = 0;
        public int motion_id = 0;
    }
    public class UserChara
    {
        public int chara_id;
        public int chara_love;//0-175(175)-245(420)-280(700)-700(1400)-700(2100)-700(2800)-1400(4200)
        public int love_level;
        public static int[] LOVE_VALUE = new int[20] 
        { 
            0, 0, 175, 420, 700, 1400, 2100, 2800, 4200,4255,
            4500,4550,4580,4750,4987,8521,9000,9002,9500,9999
        };
        public UserChara()
        {

        }
        public UserChara(int unitid,int loveLevel)
        {
            if (loveLevel <= 0)
                loveLevel = 1;
            chara_id= (int)Math.Round(unitid / 100.0f);
            //chara_id = unitid;
            chara_love = LOVE_VALUE[loveLevel];
            love_level = loveLevel;
        }
        public void Update(int loveLevel)
        {
            if (loveLevel <= 0)
                loveLevel = 1;
            chara_love = LOVE_VALUE[loveLevel];
            love_level = loveLevel;

        }
    }
    public class AreaStart
    {
        public DataHead data_headers = new DataHead(1);
        public AreaStartData data = new AreaStartData();
        public void Set(List<int> usetid, List<int> oppid, int seed)
        {
            data.Set(usetid, oppid, seed);
        }
    }
    public class AreaStartData
    {
        public long battle_viewer_id = 12340000;
        public int battle_id = 815;
        public List<WaveData> wave_info_list = new List<WaveData>();
        public void Set(List<int> usetid, List<int> oppid, int seed)
        {
            wave_info_list = new List<WaveData>();
            var wavedata = new WaveData();
            wavedata.Set(usetid, oppid, seed);
            wave_info_list.Add(wavedata);
        }
    }
    public class WaveData
    {
        public int battle_log_id = 826;
        public int seed = 377414144;
        public List<UnitDataLi> user_arena_deck = new List<UnitDataLi>();
        public List<UnitDataLi> vs_user_arena_deck = new List<UnitDataLi>();
        public int wave_num = 1;

        public void Set(List<int> usetid,List<int> oppid,int seed)
        {
            user_arena_deck.Clear();
            vs_user_arena_deck.Clear();
            this.seed = seed;
            foreach(int id in usetid)
            {
                UnitDataLi unit = new UnitDataLi(id);
                user_arena_deck.Add(unit);
            }
            foreach (int id in oppid)
            {
                UnitDataLi unit = new UnitDataLi(id);
                vs_user_arena_deck.Add(unit);
            }
        }
    }
    public class AreaSelectData
    {
        public long battle_viewer_id;
        public int opponent_rank;
        public string viewer_id;
    }
    public class AreaCancel
    {
        public DataHead data_headers = new DataHead(1);
        public AreacancelData data = new AreacancelData();
    }
    public class AreacancelData
    {
        public List<OppoentData> search_opponent = new List<OppoentData>();
    }
}
