
namespace PCRChart
{
    partial class MainEnterUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BossDEFbutton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonHP = new System.Windows.Forms.Button();
            this.buttonTP = new System.Windows.Forms.Button();
            this.buttonDamage = new System.Windows.Forms.Button();
            this.buttontotal = new System.Windows.Forms.Button();
            this.buttonEXCEPT = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BossDEFbutton
            // 
            this.BossDEFbutton.Font = new System.Drawing.Font("宋体", 12F);
            this.BossDEFbutton.Location = new System.Drawing.Point(44, 52);
            this.BossDEFbutton.Name = "BossDEFbutton";
            this.BossDEFbutton.Size = new System.Drawing.Size(162, 55);
            this.BossDEFbutton.TabIndex = 0;
            this.BossDEFbutton.Text = "查看BOSS双防变化";
            this.BossDEFbutton.UseVisualStyleBackColor = true;
            this.BossDEFbutton.Click += new System.EventHandler(this.BossDEFbutton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // buttonHP
            // 
            this.buttonHP.Font = new System.Drawing.Font("宋体", 12F);
            this.buttonHP.Location = new System.Drawing.Point(250, 52);
            this.buttonHP.Name = "buttonHP";
            this.buttonHP.Size = new System.Drawing.Size(162, 55);
            this.buttonHP.TabIndex = 2;
            this.buttonHP.Text = "查看角色HP变化";
            this.buttonHP.UseVisualStyleBackColor = true;
            this.buttonHP.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonTP
            // 
            this.buttonTP.Font = new System.Drawing.Font("宋体", 12F);
            this.buttonTP.Location = new System.Drawing.Point(250, 128);
            this.buttonTP.Name = "buttonTP";
            this.buttonTP.Size = new System.Drawing.Size(162, 55);
            this.buttonTP.TabIndex = 3;
            this.buttonTP.Text = "查看角色TP变化";
            this.buttonTP.UseVisualStyleBackColor = true;
            this.buttonTP.Click += new System.EventHandler(this.buttonTP_Click);
            // 
            // buttonDamage
            // 
            this.buttonDamage.Font = new System.Drawing.Font("宋体", 12F);
            this.buttonDamage.Location = new System.Drawing.Point(44, 128);
            this.buttonDamage.Name = "buttonDamage";
            this.buttonDamage.Size = new System.Drawing.Size(162, 55);
            this.buttonDamage.TabIndex = 4;
            this.buttonDamage.Text = "查看角色伤害详情";
            this.buttonDamage.UseVisualStyleBackColor = true;
            this.buttonDamage.Click += new System.EventHandler(this.buttonDamage_Click);
            // 
            // buttontotal
            // 
            this.buttontotal.Font = new System.Drawing.Font("宋体", 12F);
            this.buttontotal.Location = new System.Drawing.Point(44, 211);
            this.buttontotal.Name = "buttontotal";
            this.buttontotal.Size = new System.Drawing.Size(162, 55);
            this.buttontotal.TabIndex = 5;
            this.buttontotal.Text = "查看累计伤害详情";
            this.buttontotal.UseVisualStyleBackColor = true;
            this.buttontotal.Click += new System.EventHandler(this.buttontotal_Click);
            // 
            // buttonEXCEPT
            // 
            this.buttonEXCEPT.Font = new System.Drawing.Font("宋体", 12F);
            this.buttonEXCEPT.Location = new System.Drawing.Point(250, 211);
            this.buttonEXCEPT.Name = "buttonEXCEPT";
            this.buttonEXCEPT.Size = new System.Drawing.Size(162, 55);
            this.buttonEXCEPT.TabIndex = 6;
            this.buttonEXCEPT.Text = "查看伤害分布";
            this.buttonEXCEPT.UseVisualStyleBackColor = true;
            this.buttonEXCEPT.Click += new System.EventHandler(this.buttonEXCEPT_Click);
            // 
            // MainEnterUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 303);
            this.Controls.Add(this.buttonEXCEPT);
            this.Controls.Add(this.buttontotal);
            this.Controls.Add(this.buttonDamage);
            this.Controls.Add(this.buttonTP);
            this.Controls.Add(this.buttonHP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BossDEFbutton);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainEnterUI";
            this.Text = "战斗图表";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BossDEFbutton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonHP;
        private System.Windows.Forms.Button buttonTP;
        private System.Windows.Forms.Button buttonDamage;
        private System.Windows.Forms.Button buttontotal;
        private System.Windows.Forms.Button buttonEXCEPT;
    }
}

