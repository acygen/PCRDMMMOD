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
    public partial class BattleSettingWindow : Form
    {
        public BattleSettingWindow()
        {
            InitializeComponent();
            Reflash();
            ReflashKey();
        }
        public List<GuildTimelineData> list=>saveData.timelineDatas;

        public BattleSaveData saveData => PCRBattle.Instance.saveData;
        bool isRefing = false;
        public void Reflash()
        {
            isRefing = true;
            try
            {


                MainList.Items.Clear();
                cb_enable.Checked = saveData.useUBSet;
                foreach (var obj in list)
                {
                    if (string.IsNullOrEmpty(obj.timeLineName))
                        obj.timeLineName = "未命名";
                    
                    MainList.Items.Add(obj.timeLineName);
                }
                if (saveData.selectIndex >= 0 && saveData.selectIndex < list.Count)
                {
                    MainList.SelectedIndex = saveData.selectIndex;
                    ReflashRight(saveData.selectIndex);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR:" + ex.Message + ex.StackTrace);
            }
            checkBox2.Checked = saveData.nnkBUG;
            isRefing = false;
        }
        public void ReflashRight(int idx)
        {
            if (idx >= 0 && idx < list.Count)
            {
                GuildTimelineData data = list[idx];
                radioButton1.Checked = data.timeType == 0;
                radioButton2.Checked = data.timeType == 1;
                textBox1.Text = data.timeLineName;
                data.playerGroupData.Fix();
                ReflashUBList(data.playerGroupData.UBExecTimeData,0, UBList1);
                ReflashUBList(data.playerGroupData.UBExecTimeData,1, UBList2);
                ReflashUBList(data.playerGroupData.UBExecTimeData,2, UBList3);
                ReflashUBList(data.playerGroupData.UBExecTimeData,3, UBList4);
                ReflashUBList(data.playerGroupData.UBExecTimeData,4, UBList5);

            }
        }
        public void ReflashUBList(List<List<float>> values,int idx,ListBox listBox)
        {
            listBox.Items.Clear();
            List<float> value = new List<float>();
            if (idx >= 0 && idx < values.Count)
                value = values[idx];
            foreach(float v in value)
            {
                float ub = (v >= 90 ? v : (int)(60 * (90 - v)));
                listBox.Items.Add(ub);
            }
        }
        public List<float> GetUBValue(ListBox listBox)
        {
            List<float> result = new List<float>();
            foreach(var item in listBox.Items)
            {
                if(item.GetType() == typeof(float))
                {
                    result.Add((float)item);
                }
            }
            return result;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            saveData.useUBSet = cb_enable.Checked;
            saveData.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            list.Add(new GuildTimelineData(5));
            saveData.selectIndex = list.Count - 1;
            saveData.Save();
            Reflash();
        }

        private void MainList_SelectedIndexChanged(object sender, EventArgs e)
        {
            saveData.selectIndex = MainList.SelectedIndex;
            saveData.Save();
            ReflashRight(MainList.SelectedIndex);
        }
        
        private void UBEditButton(int type,int idx,ListBox listBox)
        {
            if (MainList.SelectedIndex < 0 || MainList.SelectedIndex >= list.Count)
            {
                MessageBox.Show("请先在右侧选择方案！");
                return;
            }
            string log = "";

            try
            {
                switch (type)
                {
                    case 1://修改
                        if (listBox.SelectedIndex < 0)
                        {
                            MessageBox.Show("请先选择对象！");
                            return;
                        }
                        InputForm inputForm = new InputForm();
                        inputForm.Show();
                        inputForm.Init("输入UB时间", (float)listBox.SelectedItem, a =>
                          {
                              //listBox.Items[listBox.SelectedIndex] = int.Parse(a);
                              var data = list[MainList.SelectedIndex];
                              data.playerGroupData.UBExecTimeData[idx][listBox.SelectedIndex] = float.Parse(a);
                              data.playerGroupData.SortUbTimes();
                              ReflashUBList(data.playerGroupData.UBExecTimeData,idx, listBox);
                          });
                        break;
                    case 2://添加
                        InputForm inputForm2 = new InputForm();
                        inputForm2.Show();
                        inputForm2.Init("输入UB时间", "", a =>
                        {
                            //listBox.Items.Add(int.Parse(a));
                            var data2 = list[MainList.SelectedIndex];
                            data2.playerGroupData.UBExecTimeData[idx].Add(float.Parse(a));
                            data2.playerGroupData.SortUbTimes();
                            ReflashUBList(data2.playerGroupData.UBExecTimeData,idx, listBox);
                        });
                        break;
                    case 3://删除
                        if (listBox.SelectedIndex < 0)
                        {
                            MessageBox.Show("[删除]请先选择对象！" + idx + "-"+  listBox.SelectedIndex + "-" + listBox.Items.Count);
                            return;
                        }
                        int select = listBox.SelectedIndex;
                        log += idx  +"-" + listBox.Name+ "-" + listBox.SelectedIndex + "-" + listBox.Items.Count + "\n";
                        listBox.Items.RemoveAt(listBox.SelectedIndex);
                        var data3 = list[MainList.SelectedIndex];
                        //log += data3.playerGroupData.UBExecTimeData[idx].Count + "->" + listBox.SelectedIndex;
                        data3.playerGroupData.UBExecTimeData[idx].RemoveAt(select);

                        ReflashUBList(data3.playerGroupData.UBExecTimeData,idx, listBox);
                        break;
                    case 4://清空
                        if (MessageBox.Show("是否清空UB时间？", "二次确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            listBox.Items.Clear();
                        }
                        break;
                    default:
                        throw new NotImplementedException("鸽了");
                }
            }
            catch(System.Exception ex)
            {
                StateDetailPage stateDetail = new StateDetailPage();
                stateDetail.Init("ERROR:" + ex.Message + ex.StackTrace + "\n" + log);
                stateDetail.Show();
                //MessageBox.Show("ERROR:" + ex.Message + ex.StackTrace);
            }
        }
        private void BT_del_Click(object sender, EventArgs e)
        {
            if (MainList.SelectedIndex < 0 || MainList.SelectedIndex >= list.Count)
            {
                MessageBox.Show("请先在右侧选择方案！");
                return;
            }
            else
            {
                if(MessageBox.Show("是否删除该方案？","二次确认",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Delete_0(MainList.SelectedIndex);
                }
            }
        }
        private void Delete_0(int idx)
        {
            list.RemoveAt(idx);
            saveData.Save();
            Reflash();
        }

        private void BT_save_Click(object sender, EventArgs e)
        {
            if (MainList.SelectedIndex < 0 || MainList.SelectedIndex >= list.Count)
            {
                MessageBox.Show("请先在右侧选择方案！");
                return;
            }
            var dd = list[MainList.SelectedIndex];
            dd.timeLineName = textBox1.Text;
            dd.timeType = radioButton1.Checked ? 0 : 1;
            List<List<float>> UBlist = new List<List<float>>();
            UBlist.Add(GetUBValue(UBList1));
            UBlist.Add(GetUBValue(UBList2));
            UBlist.Add(GetUBValue(UBList3));
            UBlist.Add(GetUBValue(UBList4));
            UBlist.Add(GetUBValue(UBList5));
            dd.playerGroupData.UBExecTimeData = UBlist;            
            saveData.Save();
            MessageBox.Show("保存成功！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var data = EXCELHelper.ReadSingleLineData(out var vs);
                string errmsg = "";
                foreach (string ee in vs)
                {
                    errmsg += "\n" + ee;
                }
                if (data == null)
                {
                    MessageBox.Show("错误：" + errmsg);
                    return;
                }
                data.CreatePro();
                if (MainList.SelectedIndex < 0 || MainList.SelectedIndex >= list.Count)
                {
                    list.Add(data);
                    saveData.selectIndex = list.Count - 1;
                }
                else
                {
                    list[MainList.SelectedIndex] = data;
                }
                saveData.Save();
                Reflash();
                MessageBox.Show("成功：" + errmsg);
            }
            catch(System.Exception ee)
            {
                MessageBox.Show("错误：" + ee.Message + ee.StackTrace);
            }

        }

        private void BT_e5_Click(object sender, EventArgs e)
        {
            UBEditButton(1, 4, UBList5);
        }

        private void BT_a5_Click(object sender, EventArgs e)
        {
            UBEditButton(2, 4, UBList5);
        }

        private void BT_d5_Click(object sender, EventArgs e)
        {
            UBEditButton(3, 4, UBList5);
        }

        private void BT_c5_Click(object sender, EventArgs e)
        {
            UBEditButton(4, 4, UBList5);
        }

        private void BT_e4_Click(object sender, EventArgs e)
        {
            UBEditButton(1, 3, UBList4);
        }

        private void BT_a4_Click(object sender, EventArgs e)
        {
            UBEditButton(2, 3, UBList4);
        }

        private void BT_d4_Click(object sender, EventArgs e)
        {
            UBEditButton(3, 2, UBList3);
        }

        private void BT_c4_Click(object sender, EventArgs e)
        {
            UBEditButton(4, 3, UBList4);
        }

        private void BT_e3_Click(object sender, EventArgs e)
        {
            UBEditButton(1, 2, UBList3);
        }

        private void BT_a3_Click(object sender, EventArgs e)
        {
            UBEditButton(2, 2, UBList3);
        }

        private void BT_d3_Click(object sender, EventArgs e)
        {
            UBEditButton(3, 3, UBList4);
        }

        private void BT_c3_Click(object sender, EventArgs e)
        {
            UBEditButton(4, 2, UBList3);
        }

        private void BT_e2_Click(object sender, EventArgs e)
        {
            UBEditButton(1, 1, UBList2);
        }

        private void BT_a2_Click(object sender, EventArgs e)
        {
            UBEditButton(2, 1, UBList2);
        }

        private void BT_d2_Click(object sender, EventArgs e)
        {
            UBEditButton(3, 0, UBList1);
        }

        private void BT_c2_Click(object sender, EventArgs e)
        {
            UBEditButton(4, 1, UBList2);
        }

        private void BT_e1_Click(object sender, EventArgs e)
        {
            UBEditButton(1, 0, UBList1);
        }

        private void BT_a1_Click(object sender, EventArgs e)
        {
            UBEditButton(2, 0, UBList1);
        }

        private void BT_d1_Click(object sender, EventArgs e)
        {
            UBEditButton(3, 1, UBList2);
        }

        private void BT_c1_Click(object sender, EventArgs e)
        {
            UBEditButton(4, 0, UBList1);
        }

        private void BT_timeLine_Click(object sender, EventArgs e)
        {

        }
        public void ReflashKey()
        {
            fastKeyCheck.Checked = PCRBattle.Instance.saveData.useFastKey;
            int[] keys = PCRBattle.Instance.saveData.fastKeys;
            key1.Text = ((UnityEngine.KeyCode)keys[0]).GetDescription();
            key2.Text = ((UnityEngine.KeyCode)keys[1]).GetDescription();
            key3.Text = ((UnityEngine.KeyCode)keys[2]).GetDescription();
            key4.Text = ((UnityEngine.KeyCode)keys[3]).GetDescription();
            key5.Text = ((UnityEngine.KeyCode)keys[4]).GetDescription();
            key6.Text = ((UnityEngine.KeyCode)keys[5]).GetDescription();
            key7.Text = ((UnityEngine.KeyCode)keys[6]).GetDescription();
            key8.Text = ((UnityEngine.KeyCode)keys[7]).GetDescription();
        }
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            PCRBattle.Instance.saveData.useFastKey = fastKeyCheck.Checked;
            PCRBattle.Instance.saveData.Save();
            ReflashKey();
        }
        public void SetKey(int key,int order)
        {
            PCRBattle.Instance.saveData.fastKeys[order] = key;
            PCRBattle.Instance.saveData.Save();
            ReflashKey();
        }
        public void SetKey0(int order)
        {
            try
            {
                SetKeyUI setKeyUI = new SetKeyUI();
                setKeyUI.Init(PCRBattle.Instance.saveData.fastKeys[order], a => SetKey(a, order));
                setKeyUI.Show();
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            SetKey0(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SetKey0(1);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SetKey0(2);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            SetKey0(3);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            SetKey0(4);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            SetKey0(5);

        }

        private void button9_Click(object sender, EventArgs e)
        {
            SetKey0(6);

        }

        private void button10_Click(object sender, EventArgs e)
        {
            SetKey0(7);

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (isRefing)
                return;
            PCRBattle.Instance.saveData.nnkBUG = checkBox2.Checked;
            PCRBattle.Instance.saveData.Save();
        }
    }
}
