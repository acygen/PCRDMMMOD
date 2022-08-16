using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCRCalculator.Tool
{
    public class DeckData
    {
        public DataHead data_headers = new DataHead(1);
        public string data;
    }
    public class DeckReceiveData
    {
        public List<DeckRe> deck_list = new List<DeckRe>();
    }
    public class DeckRe
    {
        public int deck_number;
        public List<int> unit_list = new List<int>();

        public int TryGetId(int idx)
        {
            if (unit_list.Count <= idx)
                return 0;
            return unit_list[idx];
        }
    }
    public class PaymentData
    {
        public DataHead data_headers = new DataHead(1);
        public List<object> data = new List<object>();
    }
    public class MusicTop
    {
        public DataHead data_headers = new DataHead(1);
        public List<object> data = new List<object>();
    }
    public class MyPageSet
    {
        public DataHead data_headers = new DataHead(1);
        public List<object> data = new List<object>();
    }
    public class MyPageReceive
    {
        public List<MyPage> my_page_info = new List<MyPage>();
    }
    public class BattleLodData
    {
        public DataHead data_headers = new DataHead(1);
        public List<object> data = new List<object>();
    }
    public class BattleLogReceiveData
    {
        public int battle_log_id;
        public int frame_rate;
        public string battle_log;
        public int system_id;
        //public int viewer_id;
        //public long dmm_viewer_id;
        //public string dmm_onetime_token;

    }
}