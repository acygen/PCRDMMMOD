using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
//using System.Linq;
using System.Text;
using System.Web;
//using System.Web.Script.Serialization;
using System.Windows.Forms;
using PCRCalculator.Tool;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using Elements;

namespace SetBox
{
	public class Main : Form
	{
		private DataTable dt = new DataTable();

		private string jsonstr;

		private string uid;

		private string token;

		private string weburl;

		private IContainer components;

		private DataGridView dataGridView1;

		private Button RollBack;

		private Button Update;

		private Label label3;

		private TextBox textBox1;

		private Button button3;

		private Button button4;

		private Button setAll;
        private Button NameChangeButton;
        private static readonly string[] DataNames = new string[17]
		{
			"id","角色名字","星级","好感度","等级",
			"UB等级","技能1","技能2","EX技能","RANK",
			"左上","右上","左中","右中","左下","右下","专武"
		};
		public Main()
		{
			SetStyle(ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
			UpdateStyles();
			InitializeComponent();
			//dataGridView1.DoubleBufferedDataGirdView(flag: true);
		}

		

		private void Main_Load(object sender, EventArgs e)
		{
			/*dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			File.ReadAllText(Application.StartupPath + "\\config.json");
			using (StreamReader reader = File.OpenText(Application.StartupPath + "\\config.json"))
			{
				using (JsonTextReader reader2 = new JsonTextReader(reader))
				{
					JObject jObject = (JObject)JToken.ReadFrom(reader2);
					uid = jObject["uid"].ToString();
					token = jObject["token"].ToString();
					weburl = jObject["url"].ToString();
				}
			}
			RollBack.Enabled = false;
			Update.Enabled = false;
			string path = "api/getbox?uid=" + uid + "&token=" + token;
			getdata(path);*/
		}
		public void Reflash()
        {
			//设置表格控件的DataSource属性
			dataGridView1.DataSource = PCRSettings.Instance.CreateDateTable();
			//设置数据表格上显示的列标题
			//for (int i = 0; i < DataNames.Length; i++)
			//	dataGridView1.Columns[i].HeaderText = DataNames[i];
			dataGridView1.ReadOnly = false;
			//不允许添加行
			dataGridView1.AllowUserToAddRows = false;
			//背景为白色
			dataGridView1.BackgroundColor = Color.White;
			dataGridView1.MultiSelect = true;
			dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
		}

		public DataTable CreateDateTable0(DataGridView dgv)
		{
			DataTable dataTable = new DataTable();
			for (int i = 0; i < dgv.Columns.Count; i++)
			{
				new DataColumn(dgv.Columns[i].Name.ToString());
				if (dgv.Columns[i].Name.ToString() == "name" || dgv.Columns[i].Name.ToString() == "skin_data" || dgv.Columns[i].Name.ToString() == "名称")
				{
					dataTable.Columns.Add(dgv.Columns[i].Name.ToString(), typeof(object));
				}
				else
				{
					dataTable.Columns.Add(dgv.Columns[i].Name.ToString(), typeof(int));
				}
			}
			for (int j = 0; j < dgv.Rows.Count; j++)
			{
				DataRow dataRow = dataTable.NewRow();
				for (int k = 0; k < dgv.Columns.Count; k++)
				{
					try
					{
						dataRow[k] = Convert.ToInt32(dgv.Rows[j].Cells[k].Value);
					}
					catch
					{
						dataRow[k] = Convert.ToString(dgv.Rows[j].Cells[k].Value);
					}
				}
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
		}

		private void RollBack_click(object sender, EventArgs e)
		{
			DialogResult result = MessageBox.Show("是否放弃更改?", "二次确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if (result == DialogResult.OK)
			{
				Reflash();
			}
		}




		private void Update_click(object sender, EventArgs e)
		{
			for (int i = 0; i < dataGridView1.Rows.Count; i++)
			{
				try
				{
					var cells = dataGridView1.Rows[i].Cells;
					PCRSettings.Instance.ChangeCharacterBox(
						Convert.ToInt32(cells[0].Value), Convert.ToInt32(cells[2].Value), Convert.ToInt32(cells[3].Value),
						Convert.ToInt32(cells[4].Value), Convert.ToInt32(cells[5].Value), Convert.ToInt32(cells[6].Value),
						Convert.ToInt32(cells[7].Value), Convert.ToInt32(cells[8].Value), Convert.ToInt32(cells[9].Value),
						Convert.ToInt32(cells[10].Value), Convert.ToInt32(cells[11].Value), Convert.ToInt32(cells[12].Value),
						Convert.ToInt32(cells[13].Value), Convert.ToInt32(cells[14].Value), Convert.ToInt32(cells[15].Value),
						Convert.ToInt32(cells[16].Value));
				}
				catch (System.Exception ex)
				{
					Cute.ClientLog.AccumulateClientLog("错误：" + ex.Message + "-"+ex.StackTrace);
				}
			}
			PCRSettings.Instance.Save(1);
			MessageBox.Show("更新成功!重新进入游戏生效");
			Reflash();
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			if (textBox1.Text == "")
			{
				return;
			}
			for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
			{
				if (dataGridView1.Rows[i].Cells[1].Value.ToString().Contains(textBox1.Text))
				{
					dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
					dataGridView1.ClearSelection();
					dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[1];
					break;
				}
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				if (textBox1.Text == "")
				{
					return;
				}
				for (int num = dataGridView1.CurrentRow.Index - 1; num > -1; num--)
				{
					if (dataGridView1.Rows[num].Cells[1].Value.ToString().Contains(textBox1.Text))
					{
						dataGridView1.ClearSelection();
						dataGridView1.CurrentCell = dataGridView1.Rows[num].Cells[1];
						break;
					}
				}
			}
			catch
			{
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			try
			{
				if (textBox1.Text == "")
				{
					return;
				}
				for (int i = dataGridView1.CurrentRow.Index + 1; i < dataGridView1.Rows.Count - 1; i++)
				{
					if (dataGridView1.Rows[i].Cells[1].Value.ToString().Contains(textBox1.Text))
					{
						dataGridView1.ClearSelection();
						dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[1];
						break;
					}
				}
			}
			catch (System.Exception ex)
			{
				MessageBox.Show("错误：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void Main_Resize(object sender, EventArgs e)
		{
			dataGridView1.Width = base.Width - 44;
			dataGridView1.Height = base.Height - 97;
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		}

		private void button5_Click(object sender, EventArgs e)
		{
			//new ManagerForm().Show();
			var a = new SetAllWindow();
			a.Show();
			a.mainWindow = this;
		}	
		public void SetAllMax(bool fit,int fitstart,int fitend,int level,int rank,bool rarity5,bool love8,bool love1,int l1,int l2,int l3,int l4,int l5,int l6,bool setEQ,int eqLevel)
        {
			for (int i = 0; i < dataGridView1.Rows.Count; i++)
			{
				var cells = dataGridView1.Rows[i].Cells;
				int unitid = Convert.ToInt32(cells[0].Value);
                if (fit)
                {
					if (unitid < fitstart || unitid > fitend)
						continue;
                }
				bool rarity6Flag = Elements.UnitUtility.GetMaxRarity(unitid) == 6;
				if (rarity5)
					cells[2].Value = rarity6Flag?6:5;
				if (love8)
					cells[3].Value = rarity6Flag?12:8;
				else if(love1)
					cells[3].Value = 1;
				cells[4].Value = level;
				cells[5].Value = level;
				cells[6].Value = level;
				cells[7].Value = level;
				cells[8].Value = level;
				cells[9].Value = rank;
				cells[10].Value = l1;
				cells[11].Value = l2;
				cells[12].Value = l3;
				cells[13].Value = l4;
				cells[14].Value = l5;
				cells[15].Value = l6;
                if (setEQ)
                {
					cells[16].Value = eqLevel;
                }
			}
			MessageBox.Show("已全部拉满,记得保存!");
		}


		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.RollBack = new System.Windows.Forms.Button();
            this.Update = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.setAll = new System.Windows.Forms.Button();
            this.NameChangeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(14, 44);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1249, 478);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // RollBack
            // 
            this.RollBack.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.RollBack.Location = new System.Drawing.Point(14, 9);
            this.RollBack.Name = "RollBack";
            this.RollBack.Size = new System.Drawing.Size(82, 27);
            this.RollBack.TabIndex = 3;
            this.RollBack.Text = "回滚";
            this.RollBack.UseVisualStyleBackColor = true;
            this.RollBack.Click += new System.EventHandler(this.RollBack_click);
            // 
            // Update
            // 
            this.Update.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Update.Location = new System.Drawing.Point(102, 9);
            this.Update.Name = "Update";
            this.Update.Size = new System.Drawing.Size(82, 27);
            this.Update.TabIndex = 4;
            this.Update.Text = "保存";
            this.Update.UseVisualStyleBackColor = true;
            this.Update.Click += new System.EventHandler(this.Update_click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(278, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "模糊搜索：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(355, 9);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(478, 27);
            this.textBox1.TabIndex = 8;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(840, 9);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 27);
            this.button3.TabIndex = 9;
            this.button3.Text = "上一个结果";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(921, 9);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 27);
            this.button4.TabIndex = 10;
            this.button4.Text = "下一个结果";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // setAll
            // 
            this.setAll.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.setAll.Location = new System.Drawing.Point(190, 9);
            this.setAll.Name = "setAll";
            this.setAll.Size = new System.Drawing.Size(82, 27);
            this.setAll.TabIndex = 11;
            this.setAll.Text = "批量设置";
            this.setAll.UseVisualStyleBackColor = true;
            this.setAll.Click += new System.EventHandler(this.button5_Click);
            // 
            // NameChangeButton
            // 
            this.NameChangeButton.Location = new System.Drawing.Point(1002, 9);
            this.NameChangeButton.Name = "NameChangeButton";
            this.NameChangeButton.Size = new System.Drawing.Size(139, 27);
            this.NameChangeButton.TabIndex = 12;
            this.NameChangeButton.Text = "角色改名";
            this.NameChangeButton.UseVisualStyleBackColor = true;
            this.NameChangeButton.Click += new System.EventHandler(this.NameChangeButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1277, 536);
            this.Controls.Add(this.NameChangeButton);
            this.Controls.Add(this.setAll);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Update);
            this.Controls.Add(this.RollBack);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Main";
            this.Text = "角色设置";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            this.Resize += new System.EventHandler(this.Main_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private void NameChangeButton_Click(object sender, EventArgs e)
        {
			for (int i = 0; i < dataGridView1.Rows.Count; i++)
			{
				var cells = dataGridView1.Rows[i].Cells;
				int unitid = Convert.ToInt32(cells[0].Value);
				string name = Convert.ToString(cells[1].Value);
				PCRSettings.Instance.UpdateUnitName(unitid, name);
			}
			PCRSettings.Instance.Save(7);
			MessageBox.Show("角色重命名已生效！");
		}

        private void SetAllMaxButton(object sender, EventArgs e)
        {
			
		}
		
	}
}
