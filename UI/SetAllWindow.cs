using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SetBox
{
    public class SetAllWindow : Form
    {
        public SetAllWindow()
        {
            InitializeComponent();
        }

        private void SetAllWindow_Load(object sender, EventArgs e)
        {

        }
        public Main mainWindow;

        private CheckBox checkBox1;
        private TextBox textBox2;
        private Label label2;
        private CheckBox checkBox2;
        private TextBox textBox3;
        private Label label3;
        private TextBox textBox4;
        private Label label4;
        private CheckBox fitCheck;
        private CheckBox setLoveOne;
        private TextBox textBox5;
        private CheckBox setUnique;
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


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.setUnique = new System.Windows.Forms.CheckBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.fitCheck = new System.Windows.Forms.CheckBox();
            this.setLoveOne = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.configButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.trackBar6 = new System.Windows.Forms.TrackBar();
            this.trackBar5 = new System.Windows.Forms.TrackBar();
            this.trackBar4 = new System.Windows.Forms.TrackBar();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label14 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.textBox5);
            this.panel1.Controls.Add(this.setUnique);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.fitCheck);
            this.panel1.Controls.Add(this.setLoveOne);
            this.panel1.Controls.Add(this.checkBox2);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.configButton);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.trackBar6);
            this.panel1.Controls.Add(this.trackBar5);
            this.panel1.Controls.Add(this.trackBar4);
            this.panel1.Controls.Add(this.trackBar3);
            this.panel1.Controls.Add(this.trackBar2);
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Location = new System.Drawing.Point(0, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(546, 371);
            this.panel1.TabIndex = 14;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(227, 279);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(63, 21);
            this.textBox5.TabIndex = 28;
            this.textBox5.Text = "140";
            // 
            // setUnique
            // 
            this.setUnique.AutoSize = true;
            this.setUnique.Location = new System.Drawing.Point(25, 284);
            this.setUnique.Name = "setUnique";
            this.setUnique.Size = new System.Drawing.Size(192, 16);
            this.setUnique.TabIndex = 27;
            this.setUnique.Text = "设置专武等级：（如果有的话）";
            this.setUnique.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(260, 60);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(63, 21);
            this.textBox3.TabIndex = 26;
            this.textBox3.Text = "109901";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "到：";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(149, 60);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(63, 21);
            this.textBox4.TabIndex = 24;
            this.textBox4.Text = "100101";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(109, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "从：";
            // 
            // fitCheck
            // 
            this.fitCheck.AutoSize = true;
            this.fitCheck.Location = new System.Drawing.Point(25, 62);
            this.fitCheck.Name = "fitCheck";
            this.fitCheck.Size = new System.Drawing.Size(72, 16);
            this.fitCheck.TabIndex = 22;
            this.fitCheck.Text = "指定范围";
            this.fitCheck.UseVisualStyleBackColor = true;
            // 
            // setLoveOne
            // 
            this.setLoveOne.AutoSize = true;
            this.setLoveOne.Location = new System.Drawing.Point(447, 46);
            this.setLoveOne.Name = "setLoveOne";
            this.setLoveOne.Size = new System.Drawing.Size(78, 16);
            this.setLoveOne.TabIndex = 21;
            this.setLoveOne.Text = "全部1好感";
            this.setLoveOne.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(447, 24);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(96, 16);
            this.checkBox2.TabIndex = 20;
            this.checkBox2.Text = "全部8/12好感";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(340, 23);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(102, 16);
            this.checkBox1.TabIndex = 19;
            this.checkBox1.Text = "全部设置5/6星";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(229, 21);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(63, 21);
            this.textBox2.TabIndex = 18;
            this.textBox2.Text = "13";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "RANK:";
            // 
            // configButton
            // 
            this.configButton.Location = new System.Drawing.Point(200, 318);
            this.configButton.Name = "configButton";
            this.configButton.Size = new System.Drawing.Size(144, 41);
            this.configButton.TabIndex = 16;
            this.configButton.Text = "确定";
            this.configButton.UseVisualStyleBackColor = true;
            this.configButton.Click += new System.EventHandler(this.configButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(80, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(63, 21);
            this.textBox1.TabIndex = 14;
            this.textBox1.Text = "133";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(275, 218);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(29, 12);
            this.label19.TabIndex = 13;
            this.label19.Text = "右下";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(275, 161);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(29, 12);
            this.label18.TabIndex = 12;
            this.label18.Text = "右中";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(275, 113);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 12);
            this.label17.TabIndex = 11;
            this.label17.Text = "右上";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(23, 218);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 12);
            this.label16.TabIndex = 10;
            this.label16.Text = "左下";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(23, 161);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 9;
            this.label15.Text = "左中";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(23, 24);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 7;
            this.label13.Text = "等级：";
            // 
            // trackBar6
            // 
            this.trackBar6.Location = new System.Drawing.Point(71, 97);
            this.trackBar6.Maximum = 5;
            this.trackBar6.Minimum = -1;
            this.trackBar6.Name = "trackBar6";
            this.trackBar6.Size = new System.Drawing.Size(183, 45);
            this.trackBar6.TabIndex = 6;
            this.trackBar6.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar6.Value = -1;
            // 
            // trackBar5
            // 
            this.trackBar5.Location = new System.Drawing.Point(71, 151);
            this.trackBar5.Maximum = 5;
            this.trackBar5.Minimum = -1;
            this.trackBar5.Name = "trackBar5";
            this.trackBar5.Size = new System.Drawing.Size(183, 45);
            this.trackBar5.TabIndex = 5;
            this.trackBar5.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar5.Value = -1;
            // 
            // trackBar4
            // 
            this.trackBar4.Location = new System.Drawing.Point(71, 205);
            this.trackBar4.Maximum = 5;
            this.trackBar4.Minimum = -1;
            this.trackBar4.Name = "trackBar4";
            this.trackBar4.Size = new System.Drawing.Size(183, 45);
            this.trackBar4.TabIndex = 4;
            this.trackBar4.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar4.Value = 5;
            // 
            // trackBar3
            // 
            this.trackBar3.Location = new System.Drawing.Point(323, 205);
            this.trackBar3.Maximum = 5;
            this.trackBar3.Minimum = -1;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(183, 45);
            this.trackBar3.TabIndex = 3;
            this.trackBar3.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar3.Value = 5;
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(323, 151);
            this.trackBar2.Maximum = 5;
            this.trackBar2.Minimum = -1;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(183, 45);
            this.trackBar2.TabIndex = 2;
            this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar2.Value = 5;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(323, 97);
            this.trackBar1.Maximum = 5;
            this.trackBar1.Minimum = -1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(183, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar1.Value = 5;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(23, 113);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 12);
            this.label14.TabIndex = 8;
            this.label14.Text = "左上";
            // 
            // SetAllWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 376);
            this.Controls.Add(this.panel1);
            this.Name = "SetAllWindow";
            this.Text = "批量设置";
            this.Load += new System.EventHandler(this.SetAllWindow_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);

        }


        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TrackBar trackBar6;
        private System.Windows.Forms.TrackBar trackBar5;
        private System.Windows.Forms.TrackBar trackBar4;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button configButton;
        private System.Windows.Forms.TextBox textBox1;

        private void configButton_Click(object sender, EventArgs e)
        {
            if (mainWindow != null)
            {
                try
                {
                    int lev = int.Parse(textBox1.Text);
                    int rank = int.Parse(textBox2.Text);
                    int fitstart = 0;
                    int fitend = 0;
                    if (fitCheck.Checked)
                    {
                        fitstart = int.Parse(textBox4.Text);
                        fitend = int.Parse(textBox3.Text);
                    }
                    int eqlevel = int.Parse(textBox5.Text);
                    mainWindow.SetAllMax(fitCheck.Checked,fitstart,fitend,lev, rank, checkBox1.Checked, checkBox2.Checked,setLoveOne.Checked,
                        trackBar6.Value, trackBar1.Value, trackBar5.Value, trackBar2.Value, trackBar4.Value, trackBar3.Value,
                        setUnique.Checked,eqlevel);
                    this.Close();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("错误：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("失败!BOX窗口已关闭!");
            }

        }

        
    }
}
