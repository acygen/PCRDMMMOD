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
    public partial class UnitEditForm : Form
    {
        public UnitEditForm(UnitDataS unit, int love, System.Action action)
        {
            InitializeComponent();
            Init(unit, love, action);
        }
        
        public int unitid;
        private void Init(UnitDataS unit,int love,System.Action action)
        {
            try
            {
                //this.FormClosed += (a, b) => { action(); };
                unitid = unit.id;
                string unitName = PCRBattle.Instance.GetName(unitid);
                NameLabel.Text = $"({unitid}){unitName}";
                M_rarity.Value = unit.unit_rarity;
                M_love.Value = love;
                M_lv.Value = unit.unit_level;
                M_sk0.Value = unit.union_burst[0].skill_level;
                M_sk1.Value = unit.main_skill[0].skill_level;
                M_sk2.Value = unit.main_skill[1].skill_level;
                M_skex.Value = unit.ex_skill[0].skill_level;
                M_rank.Value = unit.promotion_level;
                int[] eq = unit.GetEquipList();
                M_eq1.Value = eq[0];
                M_eq2.Value = eq[1];
                M_eq3.Value = eq[2];
                M_eq4.Value = eq[3];
                M_eq5.Value = eq[4];
                M_eq6.Value = eq[5];
                if (unit.unique_equip_slot != null && unit.unique_equip_slot.Count > 0)
                {
                    M_uek.Value = unit.unique_equip_slot[0].GetLv();
                    M_uek.Enabled = true;
                }
                else
                {
                    M_uek.Value = -1;
                    M_uek.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex.Message}/n{ex.StackTrace}", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int lv = (int)M_lv.Value;
            M_sk0.Value = lv;
            M_sk1.Value = lv;
            M_sk2.Value = lv;
            M_skex.Value = lv;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Tool.PCRSettings.Instance.ChangeCharacterBox(unitid, (int)M_rarity.Value,
                (int)M_love.Value, (int)M_lv.Value, (int)M_sk0.Value, (int)M_sk1.Value, 
                (int)M_sk2.Value, (int)M_skex.Value, (int)M_rank.Value, (int)M_eq1.Value, 
                (int)M_eq2.Value, (int)M_eq3.Value, (int)M_eq4.Value, (int)M_eq5.Value,
                (int)M_eq6.Value, (int)M_uek.Value);
            Tool.PCRSettings.Instance.Save(1);
            Close();
        }
    }
}
