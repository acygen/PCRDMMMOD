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
    public partial class SetKeyUI : Form
    {
        public SetKeyUI()
        {
            InitializeComponent();
        }
        Action<int> action;
        Dictionary<string, int> KeyDic = new Dictionary<string, int>();
        public void Init(int current,Action<int> finish)
        {
            listBox1.Items.Clear();
            foreach (int myCode in Enum.GetValues(typeof(KeyCode)))
            {
                string strName = Enum.GetName(typeof(KeyCode), myCode);//获取名称
                if (!KeyDic.ContainsKey(strName))
                    KeyDic.Add(strName, myCode);
                listBox1.Items.Add(strName);
                if (current == myCode)
                {
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                }
            }
            action = finish;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("请选择快捷键！");
                return;
            }
            action?.Invoke(KeyDic[listBox1.SelectedItem.ToString()]);
            Close();
        }
    }
}
