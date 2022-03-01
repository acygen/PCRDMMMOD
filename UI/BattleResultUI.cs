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
            this.logDatas = logDatas;
            ReflashLog();
            damage.Text = PCRBattle.Instance.totalDamage.ToString();
            damageYellow.Text = PCRBattle.Instance.totalDamageCriEX.ToString();
            damageBlue.Text = PCRBattle.Instance.totalDamageExcept.ToString();
            damageBlack.Text = "" + (PCRBattle.Instance.totalDamage - PCRBattle.Instance.totalDamageCriEX);
            SeedLabel.Text = "" + PCRBattle.Instance.RandomSeed;
            PCRSettings.Instance.SaveDataToFile("STATE_INPUT", allUnitStateInputDic);
            isIniting = false;
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
            logBox.Clear();
            logBox.AppendTextColorful("战斗日志：\n", Color.Black, true);
            if (currentpage < 0 || currentpage > pageMax)
            {
                currentpage = 1;
            }
            Pagelabel.Text = currentpage + "/" + pageMax;
            for (int i = (currentpage - 1) * 25; i < currentpage * 25; i++)
            {
                if (i < ShowLogDatas.Count)
                {
                    //string show = PCRBattle.Instance.saveData.logFrameLogic ? ShowLogDatas[i].Des_logic : ShowLogDatas[i].Des_real;
                    logBox.AppendTextColorful(ShowLogDatas[i].Des, Colors[ShowLogDatas[i].logType], true);
                }
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

        

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = PCRBattle.Instance.SaveChartSaveData();
                System.Diagnostics.Process exep = new System.Diagnostics.Process();
                exep.StartInfo.FileName = UnityEngine.Application.streamingAssetsPath + "/Chart/PCRChart.exe";
                exep.StartInfo.Arguments = filePath;
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
