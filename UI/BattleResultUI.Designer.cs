
namespace PCRCalculator.UI
{
    partial class BattleResultUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.CriTrackBar_enemy = new System.Windows.Forms.TrackBar();
            this.CriLabel_enemy = new System.Windows.Forms.Label();
            this.CriLabel_player = new System.Windows.Forms.Label();
            this.CriTrackBar_player = new System.Windows.Forms.TrackBar();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.UBEditButton = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.AutoUBButton = new System.Windows.Forms.Button();
            this.AutoUBLabel = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.exportButton = new System.Windows.Forms.Button();
            this.UBSaveButton = new System.Windows.Forms.Button();
            this.SeedLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.damageBlue = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.damageYellow = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.damageBlack = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.damage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.NextPageButton2 = new System.Windows.Forms.Button();
            this.LastPageButton2 = new System.Windows.Forms.Button();
            this.Pagelabel = new System.Windows.Forms.Label();
            this.NextPageButton = new System.Windows.Forms.Button();
            this.LastPageButton = new System.Windows.Forms.Button();
            this.selectButton = new System.Windows.Forms.Button();
            this.logbox = new System.Windows.Forms.RichTextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.RB_BOSS = new System.Windows.Forms.RichTextBox();
            this.RB_1 = new System.Windows.Forms.RichTextBox();
            this.RB_2 = new System.Windows.Forms.RichTextBox();
            this.RB_3 = new System.Windows.Forms.RichTextBox();
            this.RB_4 = new System.Windows.Forms.RichTextBox();
            this.RB_5 = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CriTrackBar_enemy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CriTrackBar_player)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(709, 399);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.CriTrackBar_enemy);
            this.tabPage3.Controls.Add(this.CriLabel_enemy);
            this.tabPage3.Controls.Add(this.CriLabel_player);
            this.tabPage3.Controls.Add(this.CriTrackBar_player);
            this.tabPage3.Controls.Add(this.checkBox1);
            this.tabPage3.Controls.Add(this.UBEditButton);
            this.tabPage3.Controls.Add(this.richTextBox1);
            this.tabPage3.Controls.Add(this.AutoUBButton);
            this.tabPage3.Controls.Add(this.AutoUBLabel);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(701, 370);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "战斗设置";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(8, 313);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(308, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "UB时间可直接修改，修改后点击下方按钮应用";
            // 
            // CriTrackBar_enemy
            // 
            this.CriTrackBar_enemy.Location = new System.Drawing.Point(406, 86);
            this.CriTrackBar_enemy.Maximum = 2;
            this.CriTrackBar_enemy.Minimum = -2;
            this.CriTrackBar_enemy.Name = "CriTrackBar_enemy";
            this.CriTrackBar_enemy.Size = new System.Drawing.Size(207, 56);
            this.CriTrackBar_enemy.TabIndex = 8;
            this.CriTrackBar_enemy.Scroll += new System.EventHandler(this.CriTrackBar_enemy_Scroll);
            // 
            // CriLabel_enemy
            // 
            this.CriLabel_enemy.AutoSize = true;
            this.CriLabel_enemy.Location = new System.Drawing.Point(416, 59);
            this.CriLabel_enemy.Name = "CriLabel_enemy";
            this.CriLabel_enemy.Size = new System.Drawing.Size(187, 15);
            this.CriLabel_enemy.TabIndex = 7;
            this.CriLabel_enemy.Text = "敌方受到暴击的几率：正常";
            // 
            // CriLabel_player
            // 
            this.CriLabel_player.AutoSize = true;
            this.CriLabel_player.Location = new System.Drawing.Point(416, 161);
            this.CriLabel_player.Name = "CriLabel_player";
            this.CriLabel_player.Size = new System.Drawing.Size(187, 15);
            this.CriLabel_player.TabIndex = 6;
            this.CriLabel_player.Text = "己方受到暴击的几率：正常";
            // 
            // CriTrackBar_player
            // 
            this.CriTrackBar_player.Location = new System.Drawing.Point(406, 197);
            this.CriTrackBar_player.Maximum = 2;
            this.CriTrackBar_player.Minimum = -2;
            this.CriTrackBar_player.Name = "CriTrackBar_player";
            this.CriTrackBar_player.Size = new System.Drawing.Size(207, 56);
            this.CriTrackBar_player.TabIndex = 5;
            this.CriTrackBar_player.Scroll += new System.EventHandler(this.CriTrackBar_player_Scroll);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("宋体", 12F);
            this.checkBox1.Location = new System.Drawing.Point(400, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(151, 24);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "开启暴击修改";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // UBEditButton
            // 
            this.UBEditButton.Location = new System.Drawing.Point(29, 331);
            this.UBEditButton.Name = "UBEditButton";
            this.UBEditButton.Size = new System.Drawing.Size(272, 29);
            this.UBEditButton.TabIndex = 3;
            this.UBEditButton.Text = "修改UB时间";
            this.UBEditButton.UseVisualStyleBackColor = true;
            this.UBEditButton.Click += new System.EventHandler(this.UBEditButton_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(29, 59);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(274, 251);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // AutoUBButton
            // 
            this.AutoUBButton.Location = new System.Drawing.Point(181, 6);
            this.AutoUBButton.Name = "AutoUBButton";
            this.AutoUBButton.Size = new System.Drawing.Size(122, 39);
            this.AutoUBButton.TabIndex = 1;
            this.AutoUBButton.Text = "关闭";
            this.AutoUBButton.UseVisualStyleBackColor = true;
            this.AutoUBButton.Click += new System.EventHandler(this.AutoUBButton_Click);
            // 
            // AutoUBLabel
            // 
            this.AutoUBLabel.AutoSize = true;
            this.AutoUBLabel.Font = new System.Drawing.Font("宋体", 12F);
            this.AutoUBLabel.Location = new System.Drawing.Point(26, 17);
            this.AutoUBLabel.Name = "AutoUBLabel";
            this.AutoUBLabel.Size = new System.Drawing.Size(149, 20);
            this.AutoUBLabel.TabIndex = 0;
            this.AutoUBLabel.Text = "自动UB已经开启";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.exportButton);
            this.tabPage1.Controls.Add(this.UBSaveButton);
            this.tabPage1.Controls.Add(this.SeedLabel);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.damageBlue);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.damageYellow);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.damageBlack);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.damage);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(701, 370);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "战斗结果";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // exportButton
            // 
            this.exportButton.Font = new System.Drawing.Font("宋体", 12F);
            this.exportButton.Location = new System.Drawing.Point(369, 274);
            this.exportButton.Margin = new System.Windows.Forms.Padding(4);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(219, 66);
            this.exportButton.TabIndex = 14;
            this.exportButton.Text = "导出EXCEL";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.ExportButton);
            // 
            // UBSaveButton
            // 
            this.UBSaveButton.Font = new System.Drawing.Font("宋体", 12F);
            this.UBSaveButton.Location = new System.Drawing.Point(52, 274);
            this.UBSaveButton.Margin = new System.Windows.Forms.Padding(4);
            this.UBSaveButton.Name = "UBSaveButton";
            this.UBSaveButton.Size = new System.Drawing.Size(205, 66);
            this.UBSaveButton.TabIndex = 13;
            this.UBSaveButton.Text = "保存UB时间";
            this.UBSaveButton.UseVisualStyleBackColor = true;
            this.UBSaveButton.Click += new System.EventHandler(this.UBSaveButton_Click);
            // 
            // SeedLabel
            // 
            this.SeedLabel.AutoSize = true;
            this.SeedLabel.Font = new System.Drawing.Font("宋体", 15F);
            this.SeedLabel.Location = new System.Drawing.Point(508, 30);
            this.SeedLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SeedLabel.Name = "SeedLabel";
            this.SeedLabel.Size = new System.Drawing.Size(142, 25);
            this.SeedLabel.TabIndex = 12;
            this.SeedLabel.Text = "1234567890";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F);
            this.label2.Location = new System.Drawing.Point(381, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "随机数：";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 12F);
            this.button2.Location = new System.Drawing.Point(52, 176);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(205, 66);
            this.button2.TabIndex = 10;
            this.button2.Text = "数据分析";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // damageBlue
            // 
            this.damageBlue.AutoSize = true;
            this.damageBlue.Font = new System.Drawing.Font("宋体", 15F);
            this.damageBlue.ForeColor = System.Drawing.Color.Blue;
            this.damageBlue.Location = new System.Drawing.Point(167, 105);
            this.damageBlue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.damageBlue.Name = "damageBlue";
            this.damageBlue.Size = new System.Drawing.Size(142, 25);
            this.damageBlue.TabIndex = 9;
            this.damageBlue.Text = "1234567890";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 15F);
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(33, 105);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(137, 25);
            this.label8.TabIndex = 8;
            this.label8.Text = "期望伤害：";
            // 
            // damageYellow
            // 
            this.damageYellow.AutoSize = true;
            this.damageYellow.Font = new System.Drawing.Font("宋体", 15F);
            this.damageYellow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.damageYellow.Location = new System.Drawing.Point(167, 80);
            this.damageYellow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.damageYellow.Name = "damageYellow";
            this.damageYellow.Size = new System.Drawing.Size(142, 25);
            this.damageYellow.TabIndex = 7;
            this.damageYellow.Text = "1234567890";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 15F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(33, 80);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 25);
            this.label6.TabIndex = 6;
            this.label6.Text = "暴击：";
            // 
            // damageBlack
            // 
            this.damageBlack.AutoSize = true;
            this.damageBlack.Font = new System.Drawing.Font("宋体", 15F);
            this.damageBlack.Location = new System.Drawing.Point(167, 55);
            this.damageBlack.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.damageBlack.Name = "damageBlack";
            this.damageBlack.Size = new System.Drawing.Size(142, 25);
            this.damageBlack.TabIndex = 5;
            this.damageBlack.Text = "1234567890";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F);
            this.label4.Location = new System.Drawing.Point(33, 55);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 25);
            this.label4.TabIndex = 4;
            this.label4.Text = "底伤：";
            // 
            // damage
            // 
            this.damage.AutoSize = true;
            this.damage.Font = new System.Drawing.Font("宋体", 15F);
            this.damage.Location = new System.Drawing.Point(167, 30);
            this.damage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.damage.Name = "damage";
            this.damage.Size = new System.Drawing.Size(142, 25);
            this.damage.TabIndex = 3;
            this.damage.Text = "1234567890";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F);
            this.label1.Location = new System.Drawing.Point(33, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "造成伤害：";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 12F);
            this.button1.Location = new System.Drawing.Point(369, 176);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(219, 66);
            this.button1.TabIndex = 0;
            this.button1.Text = "查看角色行动轴";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.NextPageButton2);
            this.tabPage2.Controls.Add(this.LastPageButton2);
            this.tabPage2.Controls.Add(this.Pagelabel);
            this.tabPage2.Controls.Add(this.NextPageButton);
            this.tabPage2.Controls.Add(this.LastPageButton);
            this.tabPage2.Controls.Add(this.selectButton);
            this.tabPage2.Controls.Add(this.logbox);
            this.tabPage2.Font = new System.Drawing.Font("宋体", 9F);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(701, 370);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "战斗信息";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.textBox1.AllowDrop = true;
            this.textBox1.Font = new System.Drawing.Font("宋体", 15F);
            this.textBox1.Location = new System.Drawing.Point(290, 335);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(70, 36);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "99";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // NextPageButton2
            // 
            this.NextPageButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NextPageButton2.Location = new System.Drawing.Point(531, 332);
            this.NextPageButton2.Margin = new System.Windows.Forms.Padding(4);
            this.NextPageButton2.Name = "NextPageButton2";
            this.NextPageButton2.Size = new System.Drawing.Size(81, 34);
            this.NextPageButton2.TabIndex = 7;
            this.NextPageButton2.Text = "下五页";
            this.NextPageButton2.UseVisualStyleBackColor = true;
            this.NextPageButton2.Click += new System.EventHandler(this.NextPageButton2_Click);
            // 
            // LastPageButton2
            // 
            this.LastPageButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LastPageButton2.Location = new System.Drawing.Point(101, 332);
            this.LastPageButton2.Margin = new System.Windows.Forms.Padding(4);
            this.LastPageButton2.Name = "LastPageButton2";
            this.LastPageButton2.Size = new System.Drawing.Size(80, 34);
            this.LastPageButton2.TabIndex = 6;
            this.LastPageButton2.Text = "上五页";
            this.LastPageButton2.UseVisualStyleBackColor = true;
            this.LastPageButton2.Click += new System.EventHandler(this.LastPageButton2_Click);
            // 
            // Pagelabel
            // 
            this.Pagelabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Pagelabel.AutoSize = true;
            this.Pagelabel.Font = new System.Drawing.Font("宋体", 15F);
            this.Pagelabel.Location = new System.Drawing.Point(355, 338);
            this.Pagelabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Pagelabel.Name = "Pagelabel";
            this.Pagelabel.Size = new System.Drawing.Size(51, 25);
            this.Pagelabel.TabIndex = 5;
            this.Pagelabel.Text = "/99";
            // 
            // NextPageButton
            // 
            this.NextPageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NextPageButton.Location = new System.Drawing.Point(442, 332);
            this.NextPageButton.Margin = new System.Windows.Forms.Padding(4);
            this.NextPageButton.Name = "NextPageButton";
            this.NextPageButton.Size = new System.Drawing.Size(81, 34);
            this.NextPageButton.TabIndex = 4;
            this.NextPageButton.Text = "下一页";
            this.NextPageButton.UseVisualStyleBackColor = true;
            this.NextPageButton.Click += new System.EventHandler(this.NextPageButton_Click);
            // 
            // LastPageButton
            // 
            this.LastPageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LastPageButton.Location = new System.Drawing.Point(189, 332);
            this.LastPageButton.Margin = new System.Windows.Forms.Padding(4);
            this.LastPageButton.Name = "LastPageButton";
            this.LastPageButton.Size = new System.Drawing.Size(80, 34);
            this.LastPageButton.TabIndex = 3;
            this.LastPageButton.Text = "上一页";
            this.LastPageButton.UseVisualStyleBackColor = true;
            this.LastPageButton.Click += new System.EventHandler(this.LastPageButton_Click);
            // 
            // selectButton
            // 
            this.selectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectButton.Location = new System.Drawing.Point(4, 332);
            this.selectButton.Margin = new System.Windows.Forms.Padding(4);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(73, 34);
            this.selectButton.TabIndex = 2;
            this.selectButton.Text = " 筛选";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // logbox
            // 
            this.logbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logbox.Font = new System.Drawing.Font("宋体", 10F);
            this.logbox.Location = new System.Drawing.Point(0, 4);
            this.logbox.Margin = new System.Windows.Forms.Padding(4);
            this.logbox.Name = "logbox";
            this.logbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.logbox.Size = new System.Drawing.Size(695, 327);
            this.logbox.TabIndex = 1;
            this.logbox.Text = "";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.tableLayoutPanel1);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(701, 370);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "角色属性";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(8, 352);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(307, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "为防止卡顿，切换到此页面之前请调到一倍速";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.RB_BOSS, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.RB_1, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.RB_2, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.RB_3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.RB_4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.RB_5, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(697, 336);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // RB_BOSS
            // 
            this.RB_BOSS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_BOSS.Location = new System.Drawing.Point(583, 3);
            this.RB_BOSS.Name = "RB_BOSS";
            this.RB_BOSS.Size = new System.Drawing.Size(111, 330);
            this.RB_BOSS.TabIndex = 5;
            this.RB_BOSS.Text = "角色1\nHP:999\n";
            // 
            // RB_1
            // 
            this.RB_1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_1.Location = new System.Drawing.Point(467, 3);
            this.RB_1.Name = "RB_1";
            this.RB_1.Size = new System.Drawing.Size(110, 330);
            this.RB_1.TabIndex = 4;
            this.RB_1.Text = "角色1\nHP:999\n";
            // 
            // RB_2
            // 
            this.RB_2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_2.Location = new System.Drawing.Point(351, 3);
            this.RB_2.Name = "RB_2";
            this.RB_2.Size = new System.Drawing.Size(110, 330);
            this.RB_2.TabIndex = 3;
            this.RB_2.Text = "角色1\nHP:999\n";
            // 
            // RB_3
            // 
            this.RB_3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_3.Location = new System.Drawing.Point(235, 3);
            this.RB_3.Name = "RB_3";
            this.RB_3.Size = new System.Drawing.Size(110, 330);
            this.RB_3.TabIndex = 2;
            this.RB_3.Text = "角色1\nHP:999\n";
            // 
            // RB_4
            // 
            this.RB_4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_4.Location = new System.Drawing.Point(119, 3);
            this.RB_4.Name = "RB_4";
            this.RB_4.Size = new System.Drawing.Size(110, 330);
            this.RB_4.TabIndex = 1;
            this.RB_4.Text = "角色1\nHP:999\n";
            // 
            // RB_5
            // 
            this.RB_5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RB_5.Location = new System.Drawing.Point(3, 3);
            this.RB_5.Name = "RB_5";
            this.RB_5.Size = new System.Drawing.Size(110, 330);
            this.RB_5.TabIndex = 0;
            this.RB_5.Text = "角色1\nHP:999\n";
            // 
            // BattleResultUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 394);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BattleResultUI";
            this.Text = "战斗面板";
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CriTrackBar_enemy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CriTrackBar_player)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox logbox;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Label Pagelabel;
        private System.Windows.Forms.Button NextPageButton;
        private System.Windows.Forms.Button LastPageButton;
        private System.Windows.Forms.Label damageBlue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label damageYellow;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label damageBlack;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label damage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label SeedLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button UBSaveButton;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TrackBar CriTrackBar_enemy;
        private System.Windows.Forms.Label CriLabel_enemy;
        private System.Windows.Forms.Label CriLabel_player;
        private System.Windows.Forms.TrackBar CriTrackBar_player;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button UBEditButton;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button AutoUBButton;
        private System.Windows.Forms.Label AutoUBLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button NextPageButton2;
        private System.Windows.Forms.Button LastPageButton2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox RB_BOSS;
        private System.Windows.Forms.RichTextBox RB_1;
        private System.Windows.Forms.RichTextBox RB_2;
        private System.Windows.Forms.RichTextBox RB_3;
        private System.Windows.Forms.RichTextBox RB_4;
        private System.Windows.Forms.RichTextBox RB_5;
        private System.Windows.Forms.Label label5;
    }
}