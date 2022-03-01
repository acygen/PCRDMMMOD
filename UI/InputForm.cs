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
    public partial class InputForm : Form
    {
        public InputForm()
        {
            InitializeComponent();
        }
        Action<string> callBack;
        Action cancel;
        public void Init(string help,string ss,Action<string> callBack,Action cancel=null)
        {
            label1.Text = help;
            textBox1.Text = ss;
            this.callBack = callBack;
            this.cancel = cancel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float p = 0;
            try
            {
                p = int.Parse(textBox1.Text) + (float)((int)numericUpDown1.Value) / 10.0f;
            }
            catch
            {
                MessageBox.Show("输入错误！");
                return;
            }
            callBack?.Invoke(p.ToString());
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cancel?.Invoke();
            Close();
        }
    }
}
