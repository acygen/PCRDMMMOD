using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCRCalculator.Tool;
using PCRCalculator.UI;
using Newtonsoft.Json;
using System.IO;

namespace PCRChart
{
    public partial class MainEnterUI : Form
    {
        public MainEnterUI(string[] args)
        {
            InitializeComponent();
            Init(args);
        }
        ChartSaveData saveData;
        Dictionary<int, List<ValueChangeData>> allUnitTotalDamageDic = new Dictionary<int, List<ValueChangeData>>();
        Dictionary<int, List<ValueChangeData>> totalDamageDic = new Dictionary<int, List<ValueChangeData>>();
        List<ValueChangeData> calcresult;
        bool calcFinish = false;
        public void Init(string[] args)
        {
            if (args == null || args.Length == 0)
            {                
                args = new string[] { "ChartSaveData" };
            }
            label1.Text = args[0];
            saveData = GetDataFromFile<ChartSaveData>(args[0] + ".json");
            Create();
        }
        private void Create()
        {
            if (saveData == null)
                return;
            foreach(var pair in saveData.playerUnitDamageDic)
            {
                if (pair.Key <= 200000)
                {
                    List<ValueChangeData> list = new List<ValueChangeData> { new ValueChangeData(0, 0) };

                    foreach (var data in pair.Value)
                    {
                        if (data.canAddToTotal)
                        {
                            list.Add(new ValueChangeData(data.frame, (int)data.damage + list[list.Count - 1].yValue));
                        }
                    }
                    allUnitTotalDamageDic.Add(pair.Key, list);
                }
            }
            List<ValueChangeData> list1 = new List<ValueChangeData> { new ValueChangeData(0, 0) };
            List<ValueChangeData> list2 = new List<ValueChangeData> { new ValueChangeData(0, 0) };

            foreach (var data in saveData.clanBattleDamageList)
            {
                list1.Add(new ValueChangeData(data.frame, data.totalDamage));
                list2.Add(new ValueChangeData(data.frame, data.totalDamageExcept));
                
            }
            totalDamageDic.Add(-999, list1);
            totalDamageDic.Add(-9999, list2);
            saveData.nameDic.Add(-999, "累计伤害");
            saveData.nameDic.Add(-9999, "累计期望伤害");
            Dictionary<string, int> nameCheck = new Dictionary<string, int>();
            foreach(var pair in saveData.nameDic)
            {
                if (!nameCheck.ContainsKey(pair.Value))
                {
                    nameCheck.Add(pair.Value, pair.Key);
                }
                else
                {
                    int i = 0;
                    string name0 = pair.Value + i;
                    while (nameCheck.ContainsKey(name0))
                    {
                        i++;
                        name0 = pair.Value + i;
                    }
                    nameCheck.Add(name0, pair.Key);
                }
            }
            foreach(var pair in nameCheck)
            {
                saveData.nameDic[pair.Value] = pair.Key;
            }
        }
        public T GetDataFromFile<T>(string path)
        {
            T resultT = default;
            if (File.Exists(path))
            {
                string result = File.ReadAllText(path);
                resultT = JsonConvert.DeserializeObject<T>(result);
            }
            return resultT;
        }
        private void BossDEFbutton_Click(object sender, EventArgs e)
        {
            if(saveData==null)
            {
                MessageBox.Show("没有数据！");
                return;
            }
            BattleChart battleChart = new BattleChart();
            battleChart.Init("BOSS双防变化图", saveData.bossDefChangeDic, saveData.bossMgcDefChangeDic);
            battleChart.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveData == null)
            {
                MessageBox.Show("没有数据！");
                return;
            }
            BattleChart battleChart = new BattleChart();
            battleChart.Init("角色HP变化图", saveData.allUnitHPDic, saveData.nameDic,true);
            battleChart.Show();

        }

        private void buttonTP_Click(object sender, EventArgs e)
        {
            if (saveData == null)
            {
                MessageBox.Show("没有数据！");
                return;
            }
            BattleChart battleChart = new BattleChart();
            battleChart.Init("角色TP变化图", saveData.allUnitTPDic, saveData.nameDic);
            battleChart.Show();

        }

        private void buttonDamage_Click(object sender, EventArgs e)
        {
            if (saveData == null)
            {
                MessageBox.Show("没有数据！");
                return;
            }
            BattleChart battleChart = new BattleChart();
            battleChart.Init("角色累计伤害图", allUnitTotalDamageDic, saveData.nameDic);
            battleChart.Show();

        }

        private void buttontotal_Click(object sender, EventArgs e)
        {
            if (saveData == null)
            {
                MessageBox.Show("没有数据！");
                return;
            }
            BattleChart battleChart = new BattleChart();
            battleChart.Init("累计伤害图", totalDamageDic, saveData.nameDic);
            battleChart.Show();

        }

        private void buttonEXCEPT_Click(object sender, EventArgs e)
        {
            if(calcFinish)
            {
                OpenChart(calcresult);
                return;
            }
            var list = AsyncMethod(100, 10000);
            //calcresult = list;
        }
        private void OpenChart(List<ValueChangeData> list)
        {
            if(list == null)
            {
                MessageBox.Show("没有数据！");
                return;
            }
            BattleChart battleChart = new BattleChart();
            battleChart.Init("轴伤分布图(蒙特卡洛10000次结果)", list);
            battleChart.Show();
        }
        private async Task AsyncMethod(int step, int tryTime)
        {
            var ResultFromTimeConsumingMethod = TimeConsumingMethod(step,tryTime);
            //string Result = await ResultFromTimeConsumingMethod + " + AsyncMethod. My Thread ID is :" + Thread.CurrentThread.ManagedThreadId;
            //Console.WriteLine(Result);
            //返回值是Task的函数可以不用return
            calcresult = await ResultFromTimeConsumingMethod;
            OpenChart(calcresult);
            calcFinish = true;
        }

        //这个函数就是一个耗时函数，可能是IO操作，也可能是cpu密集型工作。
        private Task<List<ValueChangeData>> TimeConsumingMethod(int step, int tryTime)
        {
            var task = Task.Run(() => {
                return CreateExcept(step, tryTime);
            });
            return task;
        }
        private List<ValueChangeData> CreateExcept(int step,int tryTime)
        {
            List<ValueChangeData> list = new List<ValueChangeData>();

            int minDamage = 0;
            int maxDamage = 0;
            Dictionary<int, int> count = new Dictionary<int, int>();
            for(int i = 0; i <= step; i++)
            {
                count.Add(i, 0);
            }
            foreach (var dd in saveData.playerUnitDamageDic)
            {
                if (dd.Key <= 200000)
                {
                    foreach (var data in dd.Value)
                    {
                        if (data.canAddToTotal)
                        {
                            int baseDamage = data.isCri ?(int)( data.damage / data.criDamageValue) : (int)data.damage;
                            int criDamage = data.criValue > 0 ? (int)(baseDamage * data.criDamageValue) : baseDamage;
                            minDamage += baseDamage;
                            maxDamage += criDamage;
                        }
                    }
                }
            }
            int delta = (maxDamage - minDamage) / step;
            System.Random random = new Random((int)(System.DateTime.Now.Ticks % int.MaxValue-1));
            for(int i = 0; i < tryTime; i++)
            {
                int totalDamage = 0;
                foreach (var dd in saveData.playerUnitDamageDic)
                {
                    if (dd.Key <= 200000)
                    {
                        foreach (var data in dd.Value)
                        {
                            if (data.canAddToTotal)
                            {
                                int baseDamage = data.isCri ? (int)(data.damage / data.criDamageValue) : (int)data.damage;
                                int criDamage = data.criValue > 0 ? (int)(baseDamage * data.criDamageValue) : baseDamage;
                                bool criflag = random.Next(0, 100) <= data.criValue * 100 && data.criValue > 0;
                                totalDamage += criflag ? criDamage : baseDamage;
                            }
                        }
                    }
                }
                int idx = (int)(((totalDamage - minDamage) / (float)(maxDamage - minDamage)) * step);
                if (count.ContainsKey(idx))
                {
                    count[idx]++;
                }
                else
                    count.Add(idx, 1);
            }
            foreach(var pair in count)
            {
                if (pair.Value >= step * 0.02f)
                    list.Add(new ValueChangeData(delta * pair.Key + minDamage, pair.Value / (float)tryTime));
            }
            return list;
        }
    }
}
