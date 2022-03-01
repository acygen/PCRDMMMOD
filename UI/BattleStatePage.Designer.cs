
namespace PCRCalculator.UI
{
    partial class BattleStatePage
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
            this.timeType3 = new System.Windows.Forms.RadioButton();
            this.timeType2 = new System.Windows.Forms.RadioButton();
            this.timeType1 = new System.Windows.Forms.RadioButton();
            this.showBuffCheck = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SimpleBuff = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timeType3
            // 
            this.timeType3.AutoSize = true;
            this.timeType3.Location = new System.Drawing.Point(405, 12);
            this.timeType3.Name = "timeType3";
            this.timeType3.Size = new System.Drawing.Size(59, 16);
            this.timeType3.TabIndex = 9;
            this.timeType3.Text = "显示秒";
            this.timeType3.UseVisualStyleBackColor = true;
            this.timeType3.CheckedChanged += new System.EventHandler(this.timeType3_CheckedChanged);
            // 
            // timeType2
            // 
            this.timeType2.AutoSize = true;
            this.timeType2.Location = new System.Drawing.Point(316, 12);
            this.timeType2.Name = "timeType2";
            this.timeType2.Size = new System.Drawing.Size(83, 16);
            this.timeType2.TabIndex = 8;
            this.timeType2.Text = "显示渲染帧";
            this.timeType2.UseVisualStyleBackColor = true;
            this.timeType2.CheckedChanged += new System.EventHandler(this.timeType2_CheckedChanged);
            // 
            // timeType1
            // 
            this.timeType1.AutoSize = true;
            this.timeType1.Checked = true;
            this.timeType1.Location = new System.Drawing.Point(227, 12);
            this.timeType1.Name = "timeType1";
            this.timeType1.Size = new System.Drawing.Size(83, 16);
            this.timeType1.TabIndex = 7;
            this.timeType1.TabStop = true;
            this.timeType1.Text = "显示逻辑帧";
            this.timeType1.UseVisualStyleBackColor = true;
            this.timeType1.CheckedChanged += new System.EventHandler(this.timeType1_CheckedChanged);
            // 
            // showBuffCheck
            // 
            this.showBuffCheck.AutoSize = true;
            this.showBuffCheck.Location = new System.Drawing.Point(8, 13);
            this.showBuffCheck.Name = "showBuffCheck";
            this.showBuffCheck.Size = new System.Drawing.Size(84, 16);
            this.showBuffCheck.TabIndex = 6;
            this.showBuffCheck.Text = "显示BUFF条";
            this.showBuffCheck.UseVisualStyleBackColor = true;
            this.showBuffCheck.CheckedChanged += new System.EventHandler(this.showBuffCheck_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMinSize = new System.Drawing.Size(4000, 999);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(8, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(881, 442);
            this.panel1.TabIndex = 10;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(4, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(874, 439);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // SimpleBuff
            // 
            this.SimpleBuff.AutoSize = true;
            this.SimpleBuff.Checked = true;
            this.SimpleBuff.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SimpleBuff.Location = new System.Drawing.Point(98, 13);
            this.SimpleBuff.Name = "SimpleBuff";
            this.SimpleBuff.Size = new System.Drawing.Size(96, 16);
            this.SimpleBuff.TabIndex = 11;
            this.SimpleBuff.Text = "折叠相同BUFF";
            this.SimpleBuff.UseVisualStyleBackColor = true;
            this.SimpleBuff.CheckedChanged += new System.EventHandler(this.SimpleBuff_CheckedChanged);
            // 
            // BattleStatePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 514);
            this.Controls.Add(this.SimpleBuff);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.timeType3);
            this.Controls.Add(this.timeType2);
            this.Controls.Add(this.timeType1);
            this.Controls.Add(this.showBuffCheck);
            this.Name = "BattleStatePage";
            this.Text = "BattleStatePage";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton timeType3;
        private System.Windows.Forms.RadioButton timeType2;
        private System.Windows.Forms.RadioButton timeType1;
        private System.Windows.Forms.CheckBox showBuffCheck;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox SimpleBuff;
    }
}