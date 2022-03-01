using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCRCalculator.Tool
{
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

        public ChartSaveData(List<ValueChangeData> bossDefChangeDic, List<ValueChangeData> bossMgcDefChangeDic)
        {
            this.bossDefChangeDic = bossDefChangeDic;
            this.bossMgcDefChangeDic = bossMgcDefChangeDic;
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

}
