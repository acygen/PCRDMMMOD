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
    public partial class StateDetailPage : Form
    {
        public StateDetailPage()
        {
            InitializeComponent();
        }
        public void Init(string error)
        {
            Append(error,Color.Red);
        }
        public void Init(UnitStateInputData data)
        {
            try
            {
                Append("对象id:" + data.skillData.unitid);
                Append("对象名字:" + data.skillData.UnitName,Color.Blue);
                Append("技能id:" + data.skillID);
                Append("技能名字:" + data.skillData.skillName,Color.Blue);
                Append("开始时间:" + data.from);
                Append("开始时间2:" + data.fromReal);
                Append("结束时间:" + data.to);
                Append("结束时间2:" + data.toReal);
                Append("技能释放情况:" + data.skillData.skillState.GetDescription());
                foreach (var dd in data.skillData.actionExecDatas)
                {
                    Append("_______________________");
                    if (!dd.additional)
                    {
                        Append("actionid:" + dd.actionID);
                        Append("类型:" + dd.actionType);
                        Append("执行时间:" + dd.execTime);
                        Append("执行时间2:" + dd.execTimeReal);
                        Append(dd.GetDescribe());
                    }
                    foreach (var data0 in dd.playerDamages)
                        AppendDamageData(data0,dd.additional);
                }
                Append("_________END____________");

            }
            catch(System.Exception ex)
            {
                Append("ERROR:" + ex.Message + ex.StackTrace);
            }
        }
        private void AppendDamageData(PlayerDamageData damageData,bool showFrame=false)
        {
            string des = PCRSettings.Instance.GetUnitNameByID(damageData.source) + "对" + PCRSettings.Instance.GetUnitNameByID(damageData.target) + "造成";
            Append((showFrame?damageData.GetFrame():"") + des, false);
            Append(damageData.damage.ToString(), damageData.isCri ? Color.Yellow : Color.Red,false);
            Append(damageData.isCri ? "点暴击伤害" : "点伤害");
            Append("暴击率：", false);
            Append(damageData.criValue.ToString(),Color.Red, false);
            Append(",暴伤倍率：", Color.Black,false);
            Append(damageData.criDamageValue.ToString(),Color.Red,true);
        }
        private void Append(string text,Color color, bool newLine = true)
        {
            richTextBox1.AppendTextColorful(text, color, newLine);
            //richTextBox1.Text += text + "\r\n";
        }
        private void Append(string text,bool newLine = true)
        {
            richTextBox1.AppendTextColorful(text, Color.Black, newLine);
            //richTextBox1.Text += text + "\r\n";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
