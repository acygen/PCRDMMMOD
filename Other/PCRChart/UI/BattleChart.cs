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
using System.Windows.Forms.DataVisualization.Charting;

namespace PCRCalculator.UI
{
    public partial class BattleChart : Form
    {
        public BattleChart()
        {
            InitializeComponent();
        }
        public Dictionary<string, bool> showUnitDic = new Dictionary<string, bool>();
        public List<Series> allSeriesList = new List<Series>();
        bool isDEF = false;
        public void Init(string title,List<ValueChangeData> def, List<ValueChangeData> mdef)
        {
            Dictionary<int, List<ValueChangeData>> defDic = new Dictionary<int, List<ValueChangeData>>();
            Dictionary<int, List<ValueChangeData>> MdefDic = new Dictionary<int, List<ValueChangeData>>();
            this.chart1.Titles[0].Text = title;
            foreach (var value in def)
            {
                if (defDic.TryGetValue(value.Index, out var list))
                {
                    list.Add(value);
                }
                else
                    defDic.Add(value.Index, new List<ValueChangeData> { value });
            }
            foreach (var value in mdef)
            {
                if (MdefDic.TryGetValue(value.Index, out var list))
                {
                    list.Add(value);
                }
                else
                    MdefDic.Add(value.Index, new List<ValueChangeData> { value });
            }
            InitChart(defDic, MdefDic);
        }
        public void Init(string title, Dictionary<int, List<ValueChangeData>> valueDic, Dictionary<int, string> nameDic,bool ignoreBoss = false)
        {
            this.chart1.Titles[0].Text = title;
            InitChart(valueDic, nameDic,ignoreBoss);
        }
        public void Init(string title, List<ValueChangeData> valueDic)
        {
            this.chart1.Titles[0].Text = title;
            InitChart(valueDic);
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
        private void Reflash()
        {
            foreach(Series ss in allSeriesList)
            {
                bool flag = true;
                if(showUnitDic.TryGetValue(ss.Name,out var value))
                {
                    flag = value;
                }
                if (flag&&!chart1.Series.Contains(ss))
                {
                    chart1.Series.Add(ss);
                }
                else if(!flag && chart1.Series.Contains(ss))
                {
                    chart1.Series.Remove(ss);
                }
            }
        }
        private void InitChart(Dictionary<int, List<ValueChangeData>> defList, Dictionary<int, List<ValueChangeData>> MdefList)
        {
            isDEF = true;
            ((ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            this.chart1.ChartAreas.Clear();
            this.chart1.Series.Clear();
            foreach(int i in defList.Keys)
            {
                ChartArea chartArea1 = new ChartArea();
                chartArea1.BackColor = Color.Gray;
                chartArea1.Name = "ChartArea" + (i+1);
                var def = Createserirs(chartArea1.Name,i, defList[i], true);
                var mdef = Createserirs(chartArea1.Name,i, MdefList[i], false);

                this.chart1.ChartAreas.Add(chartArea1);
                this.chart1.Series.Add(def);
                this.chart1.Series.Add(mdef);
                showUnitDic.Add(def.Name, true);
                showUnitDic.Add(mdef.Name, true);
                allSeriesList.Add(def);
                allSeriesList.Add(mdef);
            }
            ((ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.chart1.MouseMove += chart1_MouseMove;
        }
        private void InitChart(Dictionary<int, List<ValueChangeData>> mainDic, Dictionary<int, string> NameDic, bool ignoreBoss)
        {
            ((ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            this.chart1.ChartAreas.Clear();
            this.chart1.Series.Clear();
            ChartArea chartArea1 = new ChartArea();
            chartArea1.BackColor = Color.Gray;
            chartArea1.Name = "ChartArea00";
            foreach (var pair in mainDic)
            {
                if (!ignoreBoss || pair.Key <= 200000)
                {
                    var def = Createserirs(chartArea1.Name, pair.Value, NameDic.TryGetValue(pair.Key, out var nn) ? nn : "" + pair.Key);

                    //this.chart1.ChartAreas.Add(chartArea1);
                    this.chart1.Series.Add(def);
                    showUnitDic.Add(def.Name, true);
                    allSeriesList.Add(def);
                }
            }
            this.chart1.ChartAreas.Add(chartArea1);


            ((ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.chart1.MouseMove += chart1_MouseMove;
        }
        private void InitChart(List<ValueChangeData> mainDic)
        {
            ((ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            this.chart1.ChartAreas.Clear();
            this.chart1.Series.Clear();
            ChartArea chartArea1 = new ChartArea();
            chartArea1.BackColor = Color.Gray;
            chartArea1.Name = "ChartArea00";
            var def = Createserirs(chartArea1.Name, mainDic);

            //this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Series.Add(def);
            showUnitDic.Add(def.Name, true);
            allSeriesList.Add(def);
            this.chart1.ChartAreas.Add(chartArea1);


            ((ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.chart1.MouseMove += chart1_MouseMove;
        }
        private Series Createserirs(string areaName,int idx,List<ValueChangeData> list,bool isdef)
        {
            Series series1 = new Series();
            series1.ChartArea = areaName;
            series1.ChartType = SeriesChartType.StepLine;
            series1.Legend = "Legend1";
            string nameStr = (isdef ? "防御" : "魔防");
            if (idx > 0)
                nameStr += "(部位" + idx + ")";
            series1.Name = nameStr;
            foreach (var data in list)
            {
                series1.Points.AddXY(data.xValue,data.yValue);
            }
            return series1;
        }
        private Series Createserirs(string areaName, List<ValueChangeData> list, string labelName)
        {
            Series series1 = new Series();
            series1.ChartArea = areaName;
            series1.ChartType = SeriesChartType.StepLine;
            series1.Legend = "Legend1";
            series1.Name = labelName;
            foreach (var data in list)
            {
                series1.Points.AddXY(data.xValue, data.yValue);
            }
            return series1;
        }
        private Series Createserirs(string areaName, List<ValueChangeData> list)
        {
            Series series1 = new Series();
            series1.ChartArea = areaName;
            series1.ChartType = SeriesChartType.Area;
            series1.Legend = "Legend1";
            series1.Name = "轴伤分布";
            foreach (var data in list)
            {
                series1.Points.AddXY(data.xValue, data.yValue);
            }
            return series1;

        }

        /// <summary>
        /// 鼠标经过时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                HitTestResult Result = new HitTestResult();
                Result = chart1.HitTest(e.X, e.Y);
                if (Result.Series != null && Result.Object != null)
                {
                    // 获取当前焦点x轴的值
                    string xValue = ObjectUtil.GetPropertyValue(Result.Object, "XValue").ToString();
                    // 获取当前焦点所属区域名称
                    string areaName = ObjectUtil.GetPropertyValue(Result.Object, "LegendText").ToString();
                    // 获取当前焦点y轴的值
                    double yValue = Result.Series.Points[Result.PointIndex].YValues[0];

                    // 鼠标经过时label显示
                    MessageLabel.Text = "当前帧：" + xValue + "   " + areaName + "值：" + yValue;
                }
                else
                {
                    // 鼠标离开时label隐藏
                    MessageLabel.Text = "将鼠标放至节点处获取信息";
                }
            }
            catch (Exception se)
            {
                // 鼠标离开时label隐藏
                MessageLabel.Text = "ERROE:"+se.Message;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectUI uI = new SelectUI();
            uI.Set(showUnitDic, Reflash);
            uI.Show();
        }
    }
    public class ObjectUtil
    {
        /// <summary>
        /// 获取某个对象中的属性值
        /// </summary>
        /// <param name="info"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static object GetPropertyValue(object info, string field)
        {
            if (info == null) return null;
            Type t = info.GetType();
            IEnumerable<System.Reflection.PropertyInfo> property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
            return property.First().GetValue(info, null);
        }
    }

}
