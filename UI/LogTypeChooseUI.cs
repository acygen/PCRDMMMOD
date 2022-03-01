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

namespace PCRCalculator.UI
{
    public partial class LogTypeChooseUI : Form
    {
        public LogTypeChooseUI()
        {
            InitializeComponent();
        }
        Dictionary<string, int> enumDic = new Dictionary<string, int>();
        Action action;
        public void Set(Action callBack )
        {
            action = callBack;
            enumDic.Clear();
            checkedListBox1.Items.Clear();
            foreach (eBattleLogType suit in Enum.GetValues(typeof(eBattleLogType)))
            {
                int type = (int)suit;
                string namestr = "[" + type + "]" + suit.GetDescription();
                enumDic.Add(namestr, type);
                checkedListBox1.Items.Add(namestr, PCRBattle.Instance.saveData.GetLogEnable(type));
            }
            checkedListBox2.Items.Clear();
            if (PCRBattle.Instance.clanFinishData != null)
            {
                foreach (var obj in PCRBattle.Instance.unitShowLogDic)
                {
                    string name = PCRSettings.Instance.GetUnitNameByID(obj.Key);
                    enumDic.Add(name, obj.Key);
                    if (PCRBattle.Instance.unitShowLogDic.TryGetValue(obj.Key, out bool value0))
                    {
                        checkedListBox2.Items.Add(name, value0);
                    }
                    else
                    {
                        PCRBattle.Instance.unitShowLogDic.Add(obj.Key, true);
                        checkedListBox2.Items.Add(name, true);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for(int i=0;i<checkedListBox1.Items.Count;i++)
            {
                string key = checkedListBox1.Items[i].ToString();
                int value = enumDic[key];
                PCRBattle.Instance.saveData.SetLogEnable(value, checkedListBox1.GetItemChecked(i));
            }
            PCRBattle.Instance.saveData.Save();
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                string key = checkedListBox2.Items[i].ToString();
                int value = enumDic[key];
                PCRBattle.Instance.unitShowLogDic[value] = checkedListBox2.GetItemChecked(i);
            }
            action?.Invoke();
            this.Close();
        }
    }
}
