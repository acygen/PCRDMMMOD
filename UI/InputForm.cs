using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnityEngine;

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
        public void Init(string help,float value, Action<string> callBack, Action cancel = null)
        {
            label1.Text = help;
            int ubTime = (int)value;
            int priority = Mathf.RoundToInt(10.0f * value) - 10 * ubTime;
            bool waitBOSSUB = value * 10 - (int)(value * 10) > 0;
            checkBox1.Checked = waitBOSSUB;
            textBox1.Text = ubTime.ToString();
            numericUpDown1.Value = priority;
            this.callBack = callBack;
            this.cancel = cancel;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            float p = 0;
            int propoty = (int)numericUpDown1.Value;
            if (propoty < 0 || propoty > 9)
            {
                MessageBox.Show("优先级错误！");
                return;

            }
            try
            {
                p = int.Parse(textBox1.Text) + (float)((int)numericUpDown1.Value) / 10.0f;
                if (checkBox1.Checked)
                {
                    p += 0.01f;
                }
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
