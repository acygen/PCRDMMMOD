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
using HarmonyLib;
using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;

namespace PCRCalculator.UI
{
    public partial class BattleResultUI : Form
    {
        public BattleResultUI()
        {
            InitializeComponent();
        }
        string[] logs = new string[] { };
        int currentpage = 1;
        int pageMax = 1;
        List<LogData> logDatas = new List<LogData>();
        List<LogData> ShowLogDatas = new List<LogData>();
        bool isIniting = false;
        Dictionary<int, List<UnitStateInputData>> allUnitStateInputDic;
        Dictionary<int, List<UnitAbnormalStateChangeData>> allUnitAbnormalStateInputDic;
        static Dictionary<int, string> LevNameDic = new Dictionary<int, string>
        {
            {-2,"降低至0%" },
            {-1,"降低" },
            {0,"不变" },
            {1,"增加" },
            {2,"增加至100%" },
        };
        bool needUpdateUnitProp;
        public void Init(List<LogData> logDatas, Dictionary<int, List<UnitStateInputData>> allUnitStateInputDic, Dictionary<int, List<UnitAbnormalStateChangeData>> allUnitAbnormalStateInputDic)
        {
            isIniting = true;
            this.allUnitStateInputDic = allUnitStateInputDic;
            this.allUnitAbnormalStateInputDic = allUnitAbnormalStateInputDic;
            /*logs = json.Split('\n');
            foreach (string ss in logs)
            {
                logDatas.Add(PCRBattle.Instance.CreateLogData(ss));
            }*/
            //this.logDatas = logDatas;
            ReflashLog();
            damage.Text = PCRBattle.Instance.totalDamage.ToString();
            damageYellow.Text = PCRBattle.Instance.totalDamageCriEX.ToString();
            damageBlue.Text = PCRBattle.Instance.totalDamageExcept.ToString();
            damageBlack.Text = "" + (PCRBattle.Instance.totalDamage - PCRBattle.Instance.totalDamageCriEX);
            SeedLabel.Text = "" + PCRBattle.Instance.RandomSeed;
            PCRSettings.Instance.SaveDataToFile("STATE_INPUT", allUnitStateInputDic);
            tabControl1.SelectTab(1);
            isIniting = false;
        }
        bool useAutoUB = false;
        public void InitOnBattleStart(bool useAutoUB)
        {
            isIniting = true;
            this.useAutoUB = useAutoUB;
            UpdateUBTimeDataUI();

            this.logDatas = PCRBattle.Instance.ReceivedLogList;
            PCRBattle.Instance.OnUBTimeDataChanged += UpdateUBTimeDataUI;
            PCRBattle.Instance.OnReceivedLog += ReflashLog2;
            PCRBattle.Instance.OnUnitPropChange += UpdateUnitProp;
            tabControl1.Selected += (a,b)=> ReflashLog();

            this.FormClosed += (a, b) => OnClose();
            isIniting = false;
            //tabControl1.Selected += OnTabSelected;
        }
        private void OnClose()
        {
            PCRBattle.Instance.OnUBTimeDataChanged -= UpdateUBTimeDataUI;
            PCRBattle.Instance.OnReceivedLog -= ReflashLog2;
            PCRBattle.Instance.OnUnitPropChange -= UpdateUnitProp;
        }
        private void UpdateUBTimeDataUI()
        {
            string str = "";

            if (useAutoUB)
            {
                if (PCRBattle.Instance.UseUBTimeDatas)
                {
                    AutoUBLabel.Text = "自动UB已开启";
                    AutoUBButton.Text = "停止";
                    AutoUBButton.Enabled = true;

                }
                else
                {
                    AutoUBLabel.Text = "自动UB已停止";
                    AutoUBButton.Text = "恢复";
                    AutoUBButton.Enabled = true;
                    str += "自动UB已停止\n";
                }

                for (int i = 0; i < PCRBattle.Instance.UBTimeDatas.Count; i++)
                {
                    //if (i < PCRBattle.Instance.UBTimeDatas.Count)
                    //{
                        var data = PCRBattle.Instance.UBTimeDatas[i];
                        str += $"[{data.ubTime}{(data.waitBOSSUB?"*":"")}]-{PCRBattle.Instance.GetName(data.unitid)}({data.unitid})\n";
                    //}
                }
                if (string.IsNullOrEmpty(str))
                {
                    str = "已放完所有UB";
                }
            }
            else
            {
                AutoUBButton.Text = "未启用";
                AutoUBButton.Enabled = false;
                str = "未启用自动UB";
            }


            
            richTextBox1.Text = str;
        }
        private static Color[] Colors = new Color[14]
        {
            Color.Black,
            Color.FromArgb(255,105,105,105),
            Color.FromArgb(255,255,153,0),
            Color.FromArgb(255,182,71,255),
            Color.FromArgb(255,63,185,0),
            Color.FromArgb(255,189,142,0),
            Color.Black,
            Color.YellowGreen,
            Color.FromArgb(255,0,92,242),
            Color.Black,
            Color.Black,
            Color.Black,
            Color.Black,
            Color.Black,
        };
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                BattleStatePage battleStatePage = new BattleStatePage();
                battleStatePage.Show();
                LogData logData = logDatas.Find(a => a.logType == 12);
                if (logData == null)
                    logData = new LogData("", 12, 0, 5400, 6000);
                battleStatePage.Init(logData.logicalFrame,logData.realFrame,allUnitStateInputDic, allUnitAbnormalStateInputDic);
            }
            catch(System.Exception ex)
            {
                MessageBox.Show("error:" + ex.Message + ex.StackTrace);
            }

        }
        private void ReflashLog2()
        {
            if(tabControl1.SelectedIndex == 2)
            {
                ReflashLog();
            }
        }
        private void UpdateUnitProp()
        {
            if (tabControl1.SelectedIndex == 3)
            {
                ReflashUnitProp();
                //needUpdateUnitProp = true;
            }
        }
        public void ReflashLog()
        {
            //string result = "战斗日志：\n";
            ShowLogDatas.Clear();
            foreach(LogData logData in logDatas)
            {
                bool flag = true;
                if (!PCRBattle.Instance.saveData.GetLogEnable(logData.logType))
                    flag = false;
                if (logData.sourceUnitID > 0)
                {
                    if (PCRBattle.Instance.unitShowLogDic.TryGetValue(logData.sourceUnitID, out bool value))
                    {
                        if (!value)
                            flag = false;
                    }
                    else
                    {
                        PCRBattle.Instance.unitShowLogDic.Add(logData.sourceUnitID, true);
                    }
                }
                if (flag)
                {
                    ShowLogDatas.Add(logData);
                }
                //logBox.AppendTextColorful(describe + "\n", Colors[colorType], false);

            }
            pageMax =(int) Math.Ceiling(ShowLogDatas.Count / 25.0f);
            ReflashLogPage();
        }
        public void ReflashLogPage()
        {
            logbox.Clear();
            logbox.AppendTextColorful("战斗日志：\n", Color.Black, true);
            if (currentpage < 0 || currentpage > pageMax)
            {
                currentpage = 1;
            }
            textBox1.Text = currentpage.ToString();
            Pagelabel.Text =  "/" + pageMax;
            for (int i = (currentpage - 1) * 25; i < currentpage * 25; i++)
            {
                if (i < ShowLogDatas.Count)
                {
                    //string show = PCRBattle.Instance.saveData.logFrameLogic ? ShowLogDatas[i].Des_logic : ShowLogDatas[i].Des_real;
                    logbox.AppendTextColorful(ShowLogDatas[i].Des, Colors[ShowLogDatas[i].logType], true);
                }
            }
        }
        public void ReflashUnitProp()
        {
            var list = PCRSettings.staticBattleBanager?.GetMyUnitList();
            if (list != null)
            {
                UpdateSingleUnit(list[0], RB_1);
                UpdateSingleUnit(list[1], RB_2);
                UpdateSingleUnit(list[2], RB_3);
                UpdateSingleUnit(list[3], RB_4);
                UpdateSingleUnit(list[4], RB_5);
            }
            UpdateSingleUnit(PCRSettings.staticBattleBanager?.BossUnit, RB_BOSS);
        }
        private void UpdateSingleUnit(Elements.UnitCtrl unitCtrl,RichTextBox richTextBox)
        {
            if (unitCtrl == null)
                return;
            richTextBox.Clear();
            richTextBox.AppendTextColorful(PCRBattle.Instance.GetName(unitCtrl.UnitId), Colors[0]);
            richTextBox.AppendTextColorful($"HP:{unitCtrl.Hp}", Color.Red);
            richTextBox.AppendTextColorful($"MAXHP:{unitCtrl.MaxHp}", Color.Red);
            richTextBox.AppendTextColorful($"TP:{unitCtrl.Energy}", Color.Blue);
            richTextBox.AppendTextColorful($"TP上:{unitCtrl.EnergyRecoveryRate}", Color.Blue);
            richTextBox.AppendTextColorful($"攻击:{unitCtrl.Atk}", Colors[1]);
            richTextBox.AppendTextColorful($"魔攻:{unitCtrl.MagicStr}", Colors[1]);
            richTextBox.AppendTextColorful($"防御:{unitCtrl.Def}", Colors[4]);
            richTextBox.AppendTextColorful($"魔防:{unitCtrl.MagicDef}", Colors[4]);
            richTextBox.AppendTextColorful($"物暴:{unitCtrl.PhysicalCritical}", Colors[0]);
            richTextBox.AppendTextColorful($"魔暴:{unitCtrl.MagicCritical}", Colors[0]);
            richTextBox.AppendTextColorful($"物暴伤:{unitCtrl.PhysicalCriticalDamageRate}", Colors[0]);
            richTextBox.AppendTextColorful($"魔暴伤:{unitCtrl.MagicCriticalDamageRate}", Colors[0]);

            richTextBox.AppendTextColorful($"闪避:{unitCtrl.Dodge}", Colors[0]);
            richTextBox.AppendTextColorful($"吸血:{unitCtrl.LifeSteal}", Colors[0]);
            richTextBox.AppendTextColorful($"命中:{unitCtrl.Accuracy}", Colors[0]);
            float castTime = Traverse.Create(unitCtrl).Property("m_fCastTimer").GetValue<ObscuredFloat>();
            if(unitCtrl.CurrentState == Elements.UnitCtrl.ActionState.IDLE)
            {
                float startRemainTime = Traverse.Create(PCRSettings.staticBattleBanager).Field("startRemainTime").GetValue<float>();
                int exectTime = UnityEngine.Mathf.RoundToInt((startRemainTime - PCRSettings.staticBattleBanager.HIJKBOIEPFC) * 60);
                int skillFrame = exectTime + UnityEngine.Mathf.RoundToInt(castTime / 60);
                richTextBox.AppendTextColorful($"下一技能时间:{skillFrame}", Colors[0]);
            }
        }
        private void selectButton_Click(object sender, EventArgs e)
        {
            LogTypeChooseUI chooseUI = new LogTypeChooseUI();
            chooseUI.Show();
            chooseUI.Set(() => ReflashLog());
        }

        private void LastPageButton_Click(object sender, EventArgs e)
        {
            if (currentpage > 1)
            {
                currentpage--;
                ReflashLogPage();
            }
        }

        private void NextPageButton_Click(object sender, EventArgs e)
        {
            if (currentpage < pageMax)
            {
                currentpage++;
                ReflashLogPage();
            }
        }

        

       

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = PCRBattle.Instance.SaveChartSaveData();
                System.Diagnostics.Process exep = new System.Diagnostics.Process();
                exep.StartInfo.FileName = UnityEngine.Application.streamingAssetsPath + "/Chart/PCRChart.exe";
                exep.StartInfo.Arguments = $"\"{filePath}\"";
                exep.StartInfo.CreateNoWindow = false;
                exep.StartInfo.UseShellExecute = false;
                exep.Start();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void UBSaveButton_Click(object sender, EventArgs e)
        {
            NameUbTimeUI nameUbTimeUI = new NameUbTimeUI();
            nameUbTimeUI.Show();
        }

        private void ExportButton(object sender, EventArgs e)
        {
            try
            {
                PCRBattle.Instance.CallExcelHelper();
                MessageBox.Show("成功！");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void AutoUBButton_Click(object sender, EventArgs e)
        {
            PCRBattle.Instance.SwitchUBTimeEnable();
            UpdateUBTimeDataUI();
        }

        private void UBEditButton_Click(object sender, EventArgs e)
        {
            string text_bf = richTextBox1.Text;
            try
            {
                PCRBattle.Instance.RebuildUBTimeData(richTextBox1.Text);
                MessageBox.Show("成功！");
            }
            catch(Exception exc)
            {
                MessageBox.Show($"ERROR:{exc.Message}");
                richTextBox1.Text = text_bf;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            PCRBattle.Instance.UseFakeCritical = checkBox1.Checked;
        }

        private void CriTrackBar_player_Scroll(object sender, EventArgs e)
        {
            PCRBattle.Instance.FakeCritLev_player = CriTrackBar_player.Value;
            CriLabel_player.Text = $"己方受到暴击的几率{LevNameDic[PCRBattle.Instance.FakeCritLev_player]}";
        }

        private void CriTrackBar_enemy_Scroll(object sender, EventArgs e)
        {
            PCRBattle.Instance.FakeCritLev_enemy = CriTrackBar_enemy.Value;
            CriLabel_enemy.Text = $"敌方受到暴击的几率{LevNameDic[PCRBattle.Instance.FakeCritLev_enemy]}";

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                currentpage = int.Parse(textBox1.Text);
                ReflashLogPage();
            }
            finally
            {

            }
        }

        private void LastPageButton2_Click(object sender, EventArgs e)
        {
            if (currentpage > 5)
            {
                currentpage-=5;
            }
            else
            {
                currentpage = 1;
            }
            ReflashLogPage();

        }

        private void NextPageButton2_Click(object sender, EventArgs e)
        {
            if (currentpage < pageMax-5)
            {
                currentpage+=5;
            }
            else
            {
                currentpage=pageMax;
            }
            ReflashLogPage();

        }
        
        private void UpdateUnit_2()
        {
            try
            {
                if (tabControl1.SelectedIndex == 3)
                {
                    if (needUpdateUnitProp)
                    {
                        ReflashUnitProp();
                        needUpdateUnitProp = false;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
    }
    public static class ExtensionClass
    {
        public static void AppendTextColorful(this RichTextBox rtBox, string text, Color color, bool addNewLine = true)
        {
            if (addNewLine)
            {
                text += Environment.NewLine;
            }
            rtBox.SelectionStart = rtBox.TextLength;
            rtBox.SelectionLength = 0;
            rtBox.SelectionColor = color;
            rtBox.AppendText(text);
            rtBox.SelectionColor = rtBox.ForeColor;
        }
    }
}
