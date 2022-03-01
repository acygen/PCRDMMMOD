using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCRCalculator.UI
{
    public partial class RankBounsSetting : Form
    {
        public RankBounsSetting()
        {
            InitializeComponent();
            numericUpDown1.Value = Tool.PCRSettings.Instance.globalSetting.rankbounsRank;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tool.PCRSettings.Instance.SetRankBounsRank((int)numericUpDown1.Value);
            Close();
        }
    }
}
