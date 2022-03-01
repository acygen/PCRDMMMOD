using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCRCalculator.Tool
{
    public class ClanData
    {
        public DataHead data_headers = new DataHead(1);
        public ClanDataBody data = new ClanDataBody();
    }
    public class ClanChatListData
    {
        public DataHead data_headers = new DataHead(1);
        public List<object> data = new List<object>();
    }
    public class ClanDataBody
    {
        public Clan clan = new Clan();
        public int clan_status = 2;
        public int current_period_ranking = 1;
        public int last_total_ranking = 1;
        public int grade_rank = 1;
        public int clan_point = 900;
        public int remaining_count = 3;
        public int unread_liked_count = 0;
        public List<object> user_equip = new List<object>();
    }
    public class Clan
    {
        public Detail detail = new Detail();
        public List<object> members = new List<object>();
    }
    public class Detail
    {
        public int clan_id = 340721;
        public string leader_name = "此账号已被冻结";
        public int leader_viewer_id = 0;
        public Unit00 leader_favorite_unit = new Unit00();
        public string clan_name = "该行会已解散";
        public string description = "不要乱点以防止卡死！！！";
        public int join_condition = 2;
        public int activity = 1;
        public int member_num = 2;
        public Emblem0 emblem = new Emblem0();
    }
    public class Unit00
    {
        public int id = 107801;
        public int unit_rarity = 5;
        public int unit_level = 117;
        public int promotion_level = 12;
        public SkinData00 skin_data = new SkinData00();
    }
    public class SkinData00
    {
        public int icon_skin_id = 0;
    }
    public class Emblem0
    {
        public int emblem_id = 10000001;
        public int ex_value = 0;
    }
}
