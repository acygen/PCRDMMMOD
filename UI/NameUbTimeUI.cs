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
    public partial class NameUbTimeUI : Form
    {
        public NameUbTimeUI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                PCRBattle.Instance.SetUbTimes(textBox1.Text, radioButton1.Checked);
                MessageBox.Show("保存成功!");
            }
            catch(Exception exp)
            {
                MessageBox.Show($"ERROR:{exp.Message}");
            }
            Close();
        }
    }
}
