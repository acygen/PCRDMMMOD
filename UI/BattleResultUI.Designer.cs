
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
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
            this.Pagelabel = new System.Windows.Forms.Label();
            this.NextPageButton = new System.Windows.Forms.Button();
            this.LastPageButton = new System.Windows.Forms.Button();
            this.selectButton = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.exportButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(533, 319);
            this.tabControl1.TabIndex = 0;
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
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(525, 293);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "综合";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // UBSaveButton
            // 
            this.UBSaveButton.Font = new System.Drawing.Font("宋体", 12F);
            this.UBSaveButton.Location = new System.Drawing.Point(39, 219);
            this.UBSaveButton.Name = "UBSaveButton";
            this.UBSaveButton.Size = new System.Drawing.Size(154, 53);
            this.UBSaveButton.TabIndex = 13;
            this.UBSaveButton.Text = "保存UB时间";
            this.UBSaveButton.UseVisualStyleBackColor = true;
            this.UBSaveButton.Click += new System.EventHandler(this.UBSaveButton_Click);
            // 
            // SeedLabel
            // 
            this.SeedLabel.AutoSize = true;
            this.SeedLabel.Font = new System.Drawing.Font("宋体", 15F);
            this.SeedLabel.Location = new System.Drawing.Point(381, 24);
            this.SeedLabel.Name = "SeedLabel";
            this.SeedLabel.Size = new System.Drawing.Size(109, 20);
            this.SeedLabel.TabIndex = 12;
            this.SeedLabel.Text = "1234567890";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F);
            this.label2.Location = new System.Drawing.Point(286, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "随机数：";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 12F);
            this.button2.Location = new System.Drawing.Point(39, 141);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 53);
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
            this.damageBlue.Location = new System.Drawing.Point(125, 84);
            this.damageBlue.Name = "damageBlue";
            this.damageBlue.Size = new System.Drawing.Size(109, 20);
            this.damageBlue.TabIndex = 9;
            this.damageBlue.Text = "1234567890";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 15F);
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(25, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 20);
            this.label8.TabIndex = 8;
            this.label8.Text = "期望伤害：";
            // 
            // damageYellow
            // 
            this.damageYellow.AutoSize = true;
            this.damageYellow.Font = new System.Drawing.Font("宋体", 15F);
            this.damageYellow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.damageYellow.Location = new System.Drawing.Point(125, 64);
            this.damageYellow.Name = "damageYellow";
            this.damageYellow.Size = new System.Drawing.Size(109, 20);
            this.damageYellow.TabIndex = 7;
            this.damageYellow.Text = "1234567890";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 15F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(25, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "暴击：";
            // 
            // damageBlack
            // 
            this.damageBlack.AutoSize = true;
            this.damageBlack.Font = new System.Drawing.Font("宋体", 15F);
            this.damageBlack.Location = new System.Drawing.Point(125, 44);
            this.damageBlack.Name = "damageBlack";
            this.damageBlack.Size = new System.Drawing.Size(109, 20);
            this.damageBlack.TabIndex = 5;
            this.damageBlack.Text = "1234567890";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15F);
            this.label4.Location = new System.Drawing.Point(25, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "底伤：";
            // 
            // damage
            // 
            this.damage.AutoSize = true;
            this.damage.Font = new System.Drawing.Font("宋体", 15F);
            this.damage.Location = new System.Drawing.Point(125, 24);
            this.damage.Name = "damage";
            this.damage.Size = new System.Drawing.Size(109, 20);
            this.damage.TabIndex = 3;
            this.damage.Text = "1234567890";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F);
            this.label1.Location = new System.Drawing.Point(25, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "造成伤害：";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 12F);
            this.button1.Location = new System.Drawing.Point(277, 141);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 53);
            this.button1.TabIndex = 0;
            this.button1.Text = "查看角色行动轴";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Pagelabel);
            this.tabPage2.Controls.Add(this.NextPageButton);
            this.tabPage2.Controls.Add(this.LastPageButton);
            this.tabPage2.Controls.Add(this.selectButton);
            this.tabPage2.Controls.Add(this.logBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(525, 293);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Pagelabel
            // 
            this.Pagelabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Pagelabel.AutoSize = true;
            this.Pagelabel.Font = new System.Drawing.Font("宋体", 15F);
            this.Pagelabel.Location = new System.Drawing.Point(277, 268);
            this.Pagelabel.Name = "Pagelabel";
            this.Pagelabel.Size = new System.Drawing.Size(59, 20);
            this.Pagelabel.TabIndex = 5;
            this.Pagelabel.Text = "99/99";
            // 
            // NextPageButton
            // 
            this.NextPageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NextPageButton.Location = new System.Drawing.Point(357, 265);
            this.NextPageButton.Name = "NextPageButton";
            this.NextPageButton.Size = new System.Drawing.Size(116, 27);
            this.NextPageButton.TabIndex = 4;
            this.NextPageButton.Text = "下一页";
            this.NextPageButton.UseVisualStyleBackColor = true;
            this.NextPageButton.Click += new System.EventHandler(this.NextPageButton_Click);
            // 
            // LastPageButton
            // 
            this.LastPageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LastPageButton.Location = new System.Drawing.Point(140, 265);
            this.LastPageButton.Name = "LastPageButton";
            this.LastPageButton.Size = new System.Drawing.Size(116, 27);
            this.LastPageButton.TabIndex = 3;
            this.LastPageButton.Text = "上一页";
            this.LastPageButton.UseVisualStyleBackColor = true;
            this.LastPageButton.Click += new System.EventHandler(this.LastPageButton_Click);
            // 
            // selectButton
            // 
            this.selectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectButton.Location = new System.Drawing.Point(6, 265);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(116, 27);
            this.selectButton.TabIndex = 2;
            this.selectButton.Text = " 筛选";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // logBox
            // 
            this.logBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logBox.Font = new System.Drawing.Font("宋体", 10F);
            this.logBox.Location = new System.Drawing.Point(0, 3);
            this.logBox.Name = "logBox";
            this.logBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.logBox.Size = new System.Drawing.Size(522, 258);
            this.logBox.TabIndex = 1;
            this.logBox.Text = "";
            // 
            // exportButton
            // 
            this.exportButton.Font = new System.Drawing.Font("宋体", 12F);
            this.exportButton.Location = new System.Drawing.Point(277, 219);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(164, 53);
            this.exportButton.TabIndex = 14;
            this.exportButton.Text = "导出EXCEL";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.ExportButton);
            // 
            // BattleResultUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 315);
            this.Controls.Add(this.tabControl1);
            this.Name = "BattleResultUI";
            this.Text = "战斗结果";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox logBox;
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
    }
}