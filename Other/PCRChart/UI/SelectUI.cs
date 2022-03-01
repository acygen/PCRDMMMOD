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
    public partial class SelectUI : Form
    {
        public SelectUI()
        {
            InitializeComponent();
        }
        Action action;
        Dictionary<string, bool> dic;
        public void Set(Dictionary<string,bool> dic,Action callBack)
        {
            this.dic = dic;
            action = callBack;
            checkedListBox1.Items.Clear();
            foreach (var pair  in dic)
            {
                checkedListBox1.Items.Add(pair.Key,pair.Value);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                string key = checkedListBox1.Items[i].ToString();
                
                dic[key] =checkedListBox1.GetItemChecked(i);
            }            
            action?.Invoke();
            this.Close();
        }

    }
}
