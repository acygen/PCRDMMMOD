using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCRCalculator.Tool
{
    public class LoadData
    {
        public DataHead data_headers;
        public LoadDataBody data;
    }
    public class HomeData
    {
        public DataHead data_headers;
        //public HomeDataBody data;
        public object data;
        public HomeData() { }
        public HomeData(long time)
        {
            data_headers = new DataHead(1);
            data = new HomeDataBody();
        }
    }
    public class HomeDataBody
    {
        public UnreasMsg[] unread_message_list = new UnreasMsg[0];
        public int have_clan_battle_reward = 0;
        public LastFriendTime last_friend_time = new LastFriendTime();
        public List<object> missions = new List<object>();
        public List<object> season_pack = new List<object>();
        public long daily_reset_time = 0;
        public UserClan user_clan = new UserClan();
        public int have_join_request = 0;
        public List<Quest> quest_list = new List<Quest>();//new List<Quest> { new Quest(11003001), new Quest(11004006) };
        public Training training_quest_count = new Training();
        public Training training_quest_max_count = new Training();
        public int training_quest_pack_end_time = 0;
        public Dungeon dungeon_info = new Dungeon();
        public int paid_jewel = 4500000;
        public int free_jewel = 4500000;
        public int clan_battle_remaining_count = 3;


    }
    public class UnreasMsg
    {
        public List<object> equip_requests = new List<object>();
        public List<object> cooperation_data = new List<object>();
    }
    public class LastFriendTime
    {
        public int accept = 0;
        public int pending = 0;
    }
    public class UserClan
    {
        public int clan_id = 1;
        public int leave_time = 0;
        public int clan_member_count = 2;
        public int latest_request_time = 0;
        public int donation_num = 0;
    }
    public class Quest
    {
        public int quest_id;
        public int clear_flg = 3;
        public int result_type = 2;
        public int daily_clear_count = 0;
        public int daily_recovery_count = 0;
        public Quest() { }

        public Quest(int quest_id)
        {
            this.quest_id = quest_id;
        }

        public Quest(int quest_id, int clear_flg, int result_type, int daily_clear_count, int daily_recovery_count)
        {
            this.quest_id = quest_id;
            this.clear_flg = clear_flg;
            this.result_type = result_type;
            this.daily_clear_count = daily_clear_count;
            this.daily_recovery_count = daily_recovery_count;
        }
    }
    public class Training
    {
        public int gold_quest = 2;
        public int exp_quest = 2;
    }
    public class Dungeon
    {
        public List<object> dungeon_area = new List<object>();
        public int enter_area_id = 0;
        public List<object> rest_challenge_count = new List<object>();
        public List<object> dungeon_cleared_area_id_list = new List<object>();
    }
    public class GameStartData
    {
        public DataHead_gameStart data_headers;
        public GameStartDataBody data;

        public GameStartData() { }
        public GameStartData(int version)
        {
            data_headers = new DataHead_gameStart(version);
            data = new GameStartDataBody();
        }
    }
    public class GameStartDataBody
    {
        public int rewrite_viewer_id=114514;
    }
    public class ClanBattleTopData
    {
        public DataHead data_headers;
        public ClanBattleTopBody data;
    }
    public class ClanBattleBossInfo
    {
        public DataHead data_headers;
        public BossInfoBody data;
        public ClanBattleBossInfo() { }
        public void Set(long hp)
        {
            data_headers.result_code = 1;
            data = new BossInfoBody();
            data.damage_history = new List<object>();
            data.current_hp = hp;
        }
    }
    public class ClanBattleBossInfoReceiveData
    {
        public int clan_id;
        public int clan_battle_id;
        public int lap_num;
        public int order_num;
        public int viewer_id;
        public long dmm_viewer_id;
        public string dmm_onetime_token;

    }
    public class ClanBattleBossReload
    {
        public DataHead data_headers;
        public ReloadData data;

        public class ReloadData
        {
            public int fighter_num = 0;
            public int current_hp = 0;
        }
        public ClanBattleBossReload() { }
        public void Set(int hp)
        {
            data_headers.result_code = 1;
            data = new ReloadData();
            data.current_hp = hp;
        }
    }
    public class AreaBattleReplay
    {
        public DataHead data_headers;
        public AreaData data;
    }
    public class DataHead
    {
        public string request_id = "1234564894";
        public int short_udid = 0;
        public long viewer_id = 0;
        public string sid = "000";
        public long servertime = 1615604550;
        public int result_code;
        public DataHead() { }
        public DataHead(int resultcode,long time = 0)
        {
            servertime = time;
            result_code = resultcode;
        }
    }
    public class DataHead_gameStart
    {
        public string request_id = "1234564894";
        public int short_udid = 0;
        public long viewer_id = 0;
        public string sid = "000";
        public long servertime = 1615604550;
        public int result_code = 1;
        public int required_res_ver = 10038100;
        public DataHead_gameStart() { }
        public DataHead_gameStart(int version)
        {
            required_res_ver = version;
        }
    }
    public class DataHead2
    {
        public string request_id = "1234564894";
        public int short_udid = 0;
        public string udid = "12346598";
        public long viewer_id = 0;
        public string sid = "000";
        public long servertime = 1615604550;
        public int result_code;
        public DataHead2() { }
        public DataHead2(int resultcode, long time = 0)
        {
            servertime = time;
            result_code = resultcode;
        }
    }
    public class ToolSign
    {
        public DataHead2 data_headers = new DataHead2(1);
        public List<object> data = new List<object>();
    }
    public class LoadDataBody
    {
        public UserInfo user_info;
        public object user_jewel;
        public object user_gold;
        public List<UnitDataS> unit_list;
        public List<object> growth_unit_list = new List<object>();
        public List<object> user_redeem_unit = new List<object>();
        public List<UserChara> user_chara_info; 
        public List<Deck> deck_list;
        public object item_list;
        public object user_equip;
        public List<UserExEquipData> user_ex_equip = new List<UserExEquipData>();
        public List<UserExEquipData> user_clan_battle_ex_equip_restriction = new List<UserExEquipData>();
        public object shop;
        public object ini_setting;
        public int max_storage_num;
        public List<int> campaign_list;
        public int can_free_gacha;
        public int can_campaign_gacha;
        public object gacha_point_info;
        public List<int> read_story_ids;
        public List<int> limit_still_ids = new List<int>();
        public List<string> unlock_story_ids;
        public object event_statuses;
        public object drj;
        public List<UserMyParty> user_my_party;
        public List<UserMyPartyTab> user_my_party_tab;
        //public object user_my_quest;
        public long daily_reset_time;
        public object login_bonus_list;
        public int present_count;
        public int clan_like_count;
        public object dispatch_units;
        public object clan_battle;
        public object voice;
        public object csc;

        public List<object> part_maintenance_status = new List<object>();
        public int today_start_level;
        public int bank_bought;

        public int ddn;
        public int cbm;
        public int csm;
        public int cr;
        public int een_n;
        public int een_r;
        public int chr;
        public int cbs;
        public int cbtl;
        public int cbsa;
        public int legion_term;
        public int cbel;
        public int errm;
        public string recheck_dmm_jewel;
        public int tt;
        public int cgl;
        public int ebm;
        public int dbm;
        public int lsm;
        public int evmb;
        public int nls;
        public int bnk;
        public int sma;
        public int cbr;
        public int rsc;
        public int evfm;
        public int ubr;
        public int trb;
        public int tcb;
        public int giu;
        public int shmb;
        public int mss;
        public int sar;
        public int exeq;
        public int tvq;
        public int rug;
        public int ags;
        public int wac_start_time;
        public int wac_end_time;

        public object taq;


        public int my_page_exists;
        public List<MyPage> my_page = new List<MyPage>();
        public void ReplaceUnitList(List<UnitDataS> unit_list_change,int type)
        {
            switch (type)
            {
                case 0://replace
                    unit_list = unit_list_change;
                    break;
                case 1://add
                    foreach(UnitDataS unit in unit_list_change)
                    {
                        int idx = unit_list.FindIndex(a => a.id == unit.id);
                        if (idx < 0)
                            unit_list.Add(unit);
                    }
                    break;
                case 2://add+replace
                    foreach (UnitDataS unit in unit_list_change)
                    {
                        int idx = unit_list.FindIndex(a => a.id == unit.id);
                        if (idx < 0)
                            unit_list.Add(unit);
                        else
                            unit_list[idx] = unit;
                    }
                    break;
            }
        }
        public void ReplaceUnitLove(List<UserChara> user_chara_info_change,int type)
        {
            switch (type)
            {
                case 0:
                    user_chara_info = user_chara_info_change;
                    break;
                case 1:
                    foreach(var charater in user_chara_info_change)
                    {
                        int idx = user_chara_info.FindIndex(a => a.chara_id == charater.chara_id);
                        if (idx < 0)
                            user_chara_info.Add(charater);
                    }
                    break;
                case 2:
                    foreach (var charater in user_chara_info_change)
                    {
                        int idx = user_chara_info.FindIndex(a => a.chara_id == charater.chara_id);
                        if (idx < 0)
                            user_chara_info.Add(charater);
                        else
                            user_chara_info[idx] = charater;
                    }
                    break;                    
            }
        }
        public void ReplaceReadStories(List<int> read_stories,int type)
        {
            switch (type)
            {
                case 0:
                    read_story_ids = read_stories;
                    break;
                case 1:
                case 2:
                    foreach(int stid in read_stories)
                    {
                        if (!read_story_ids.Contains(stid))
                            read_story_ids.Add(stid);
                    }
                    break;
                case 3:
                    foreach(int st in read_story_ids)
                    {
                        if (read_story_ids.Contains(st))
                            read_story_ids.Remove(st);
                    }
                    break;
            }
            read_story_ids.Sort();
        }
        public void UpdataStoryId(List<int> add,List<int> remove)
        {
            foreach(int i in remove)
            {
                if (read_story_ids.Contains(i))
                    read_story_ids.Remove(i);
            }
            foreach(int j in add)
            {
                if (!read_story_ids.Contains(j))
                    read_story_ids.Add(j);
            }
            read_story_ids.Sort();
        }
        public void Remove1701()
        {
            UnitDataS huannai = unit_list.Find(a => a.id == 170101);
            UnitDataS huannai2 = unit_list.Find(a => a.id == 170201);
            if (huannai != null)
                unit_list.Remove(huannai);
            if (huannai2 != null)
                unit_list.Remove(huannai2);
        }
    }
    public class Deck
    {
        public int deck_number = -1;
        public int unit_id_1;
        public int unit_id_2;
        public int unit_id_3;
        public int unit_id_4;
        public int unit_id_5;
        public object general_property_1;
        public object general_property_2;
        public object general_property_3;
        public object general_property_4;
        public object general_property_5;
    }
    public class UserInfo
    {
        public int viewer_id;
        public string user_name;
        public string user_comment;
        public int team_level;
        public int user_stamina;
        public int max_stamina;
        public int team_exp;
        public int favorite_unit_id;
        public int tutorial_flag;
        public int invite_accept_flag;
        public int user_birth;
        public int platform_id;
        public int channel_id;
        public string last_ac;
        public int last_ac_time;
        public int server_id;
        public int reg_time;
        public int stamina_full_recovery_time;
        public object emblem;
    }
    public class UserMyParty
    {
        public int tab_number;
        public int party_number;
        public int party_label_type;
        public string party_name;
        public int unit_id_1;
        public int unit_id_2;
        public int unit_id_3;
        public int unit_id_4;
        public int unit_id_5;
        public UserMyParty() { }
        public UserMyParty(int tab_number, int party_number, int party_label_type, string party_name, int unit_id_1, int unit_id_2, int unit_id_3, int unit_id_4, int unit_id_5)
        {
            this.tab_number = tab_number;
            this.party_number = party_number;
            this.party_label_type = party_label_type;
            this.party_name = party_name;
            this.unit_id_1 = unit_id_1;
            this.unit_id_2 = unit_id_2;
            this.unit_id_3 = unit_id_3;
            this.unit_id_4 = unit_id_4;
            this.unit_id_5 = unit_id_5;
        }
        public bool AllZero()
        {
            return unit_id_1 +unit_id_2 + unit_id_3 + unit_id_4 + unit_id_5 == 0;
        }
        public List<int> GetUnits()
        {
            List<int> result = new List<int>();
            if (unit_id_1 > 0)
                result.Add(unit_id_1);
            if (unit_id_2 > 0)
                result.Add(unit_id_2);
            if (unit_id_3 > 0)
                result.Add(unit_id_3);
            if (unit_id_4 > 0)
                result.Add(unit_id_4);
            if (unit_id_5 > 0)
                result.Add(unit_id_5);
            return result;
        }
    }
    public class UserMyPartyTab
    {
        public int tab_number;
        public string tab_name;
        public UserMyPartyTab() { }

        public UserMyPartyTab(int tab_number, string tab_name)
        {
            this.tab_number = tab_number;
            this.tab_name = tab_name;
        }
    }
    public class MyPage
    {
        public int type;
        public int id;
        public int music_id;
        public int still_skin_id;
        public int frame_id;

        public MyPage() { }
        public MyPage(int type, int id, int music_id, int still_skin_id)
        {
            this.type = type;
            this.id = id;
            this.music_id = music_id;
            this.still_skin_id = still_skin_id;
        }
    }
    public class ClanBattleTopBody
    {
        public int clan_battle_id;
        public int period;
        public int lap_num;
        public List<BossInfo> boss_info;
        public List<object> damage_history;
        public int period_rank = 1;
        public int own_rank = 1;
        public int point;
        public int remaining_count;
        public int carry_over_time;
        public int change_season;
        public int change_period;
        public List<object> used_unit;
        public int clan_battle_mode = 0;
        public int next_clan_battle_mode = 0;
        public Clan0 user_clan = new Clan0();

        public ClanBattleTopBody() 
        {
        }
    }
    public class Clan0
    {
        public string clan_name = "该行会已解散";
        public int clan_role = 1;
    }
    public class BossInfo
    {
        public int order_num;
        public int lap_num;
        public int enemy_id;
        public int max_hp;
        public int current_hp;
        public BossInfo() { }

        public BossInfo(int order_num, int enemy_id, int max_hp, int current_hp, int lap_num)
        {
            this.order_num = order_num;
            this.enemy_id = enemy_id;
            this.max_hp = max_hp;
            this.current_hp = current_hp;
            this.lap_num = lap_num;
        }
    }
    public class BossInfoBody
    {
        public List<object> damage_history;
        public long current_hp;
    }
    public class ClanBattleStartData
    {
        public DataHead data_headers;
        public ClanBattleStartBody data;
        public ClanBattleStartData() { }
        public ClanBattleStartData(UnitDataC enemydata,int seed,int limitTime = 90,int currentHP = -1)
        {
            data_headers = new DataHead(1);
            data = new ClanBattleStartBody(limitTime, enemydata, 1, (int)currentHP, seed);
            
        }
    }
    public class ClanBattleStartBody
    {
        public int limit_time;
        public UnitDataC enemy_data;
        public int battle_log_id;
        public int current_hp;
        public int seed;
        public UserGood user_gold;
        public ClanBattleStartBody() { }

        public ClanBattleStartBody(int limit_time, UnitDataC enemy_data, int battle_log_id, int current_hp, int seed)
        {
            this.limit_time = limit_time;
            this.enemy_data = enemy_data;
            this.battle_log_id = battle_log_id;
            this.current_hp = current_hp;
            this.seed = seed;
            user_gold = new UserGood();
        }
    }
    public class UserGood
    {
        public int gold_id_pay = 0;
        public int gold_id_free = 9999999;
    }
    public class ClanBattleStartSendData
    {
        public int clan_id;
        public int clan_battle_id;
        public int period;
        public int lap_num;
        public int order_num;
        public int owner_viewer_id;
        public int support_unit_id;
        public int support_battle_rarity;
        public int remaining_count;

        //static string[] lapDes = new string[] { "A", "B", "C", "D", "E" };
        //public int viewer_id;
        //public int dmm_viewer_id;
        //public string dmm_onetime_token;
        public string GetBossDes()
        {
            string des = "A";
            if (lap_num >= 4 && lap_num <= 10)
                des = "B";
            else if (lap_num <= 34)
                des = "C";
            else if (lap_num <= 45)
                des = "D";
            else
                des = "E";
            return des + order_num;
        }
    }
    public class ClanBattleSupportListData
    {
        public DataHead data_headers = new DataHead(1);
        public ClanBattleSupportListDataBody data = new ClanBattleSupportListDataBody();
    }
    public class ClanBattleSupportListDataBody
    {
        public List<ClanBattleSupportUnitLight> support_unit_list = new List<ClanBattleSupportUnitLight>();
    }
    public class ClanBattleSupportUnitLight
    {
        public UnitDataLi unit_data;
        public int current_support_unit;
        public int owner_viewer_id;
        public string owner_name;
        public int remaining_count;
    }
    public class EmptyReturnBodyData
    {
        public DataHead data_headers;
        public string data;
    }
    public class ClanBattleFinishData
    {
        public DataHead data_headers;
        public ClanBattleFinishDataBody data;
        public ClanBattleFinishData() { }
        public ClanBattleFinishData(int damage_result = 0, int dead = 0, int carry_over_time = 0)
        {
            data_headers = new DataHead(1);
            data = new ClanBattleFinishDataBody(damage_result, dead, carry_over_time);
        }
    }
    public class ClanBattleFinishDataBody
    {
        public int acquired_gold = 0;
        public UserGood user_gold = new UserGood();
        public int damage_result;
        public int dead = 0;
        public int carry_over_time = 0;
        public int is_over_kill = 0;
        public int attack_count = 3;
        public ClanBattleFinishDataBody() { }
        public ClanBattleFinishDataBody(int damage_result = 0, int dead = 0, int carry_over_time = 0)
        {
            this.damage_result = damage_result;
            //this.dead = dead;
            this.is_over_kill = dead;
            this.carry_over_time = carry_over_time;
        }
    }
    public class ClanBattleFinishReceiveData
    {
        public int clan_id;
        public int clan_battle_id;
        public int lap_num;
        public int order_num;
        public UserUnit user_unit;
        public int boss_hp;
        public int boss_damage;
        public int remain_time;
        public int total_damage;
        public int battle_log_id;
        public int is_auto;
        public List<TimeLine> timeline;
        public int battle_time;
        public int start_remain_time;
        //public int viewer_id;
        //public long dmm_viewer_id;
        //public string dmm_onetime_token;
    }
    public class UserUnit
    {
        public List<DamageData> unit_damage_list;
        public List<HPData> unit_hp_list;
    }
    public class DamageData
    {
        public int viewer_id;
        public int unit_id;
        public int damage;
        public int rarity;
        //public SkinData skin_data;
    }
    public class HPData
    {
        public int viewer_id;
        public int unit_id;
        public int hp;
    }
    public class TimeLine
    {
        public int unit_id;
        public int remain_time;
        public int is_battle_finish;
    }
    public class UserExEquipData
    {
        public int serial_id;
        public int ex_equipment_id;
        public int enhancement_pt;
        public int rank;
        public int protection_flag;

        public int unit_id;
    }
}
