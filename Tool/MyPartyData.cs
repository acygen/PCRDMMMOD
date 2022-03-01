using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCRCalculator.Tool
{
    public class MyPartyReceiveData
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
        public List<int> change_rarity_unit_list;
        

    }
    public class MyPartyData
    {
        public DataHead data_headers = new DataHead(1);
        public List<object> data = new List<object>();
    }
}
