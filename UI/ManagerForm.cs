using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Windows.Forms;
using PCRCalculator.Tool;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

namespace SetBox
{
	public class ManagerForm : Form
	{
        #region
        private IContainer components;

		private StatusStrip statusStrip1;

		private ToolStripStatusLabel toolStripStatusLabel1;
        private TabPage clanSetting;
        private Label label11;
        private Label label10;
        private Label label5;
        private NumericUpDown cb_seed;
        private NumericUpDown boss_hp;
        private NumericUpDown boss_num;
        private NumericUpDown battle_time;
        private NumericUpDown cb_lap;
        private NumericUpDown cb_id;
        private Label label4;
        private Label label7;
        private Label label6;
        private Button Apply_CB_Config;
        private Label label3;
        private Label label2;
        private Label label1;
        private TabPage MainSetting;
        private Button saveBaseSettingButton;
        private CheckBox logWebJson;
        private CheckBox useOwnDB;
        private CheckBox cryptfile;
        private CheckBox ignore2;
        private CheckBox ignoreManifest;
        private CheckBox useUserPath_check;
        private TextBox assestPath_txt;
        private TextBox unitlevel_txt;
        private TextBox username_txt;
        private Label label9;
        private Button button1;
        private Label label23;
        private Label label22;
        private TabControl battleTool;
        private TabPage tabPage2;
        private TrackBar speedbar;
        private Label label14;
        private Label label15;
        private CheckBox checkBox6;
        private CheckBox showTimeCkeck;
        private Label label12;
        private ToolStripProgressBar toolStripProgressBar1;
        private CheckBox checkBox1;
        private Label label8;
        private TabPage tabPage1;
        private TextBox logBox;
        private CheckBox uselog;
        private Label speedlabel;
        private CheckBox checkBox2;
        private TabPage tabPage3;
        private Label label16;
        private TextBox textBox1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private CheckBox timeCheck2;
        private Button OtherTool;
        private Button UpdateClanButton;
        private Label label17;
        private Label label18;
        private Button button2;
        private Label label13;
        private Label label19;
        private Button button3;
        private CheckBox showUICheck;
        private CheckBox replaceURLCheck;
        private Label label20;
        private TextBox VersionText;
        private Button ConfigVersion;
        private CheckBox checkBoxMutiTarget;
        private PCRSettings Settings;
        #endregion
        public ManagerForm()
		{
			InitializeComponent();
		}

		private void Form2_Load(object sender, EventArgs e)
		{
            Settings = PCRSettings.Instance;
		}

        public void Reflash()
        {
            ReflashMainSetting();
            ReflashClanSetting();
            ReflashBattleSetting();
            ReflashOtherSetting();
        }
		public void ReflashMainSetting()
        {
            var loadIndex = Settings.loadData;
            username_txt.Text = loadIndex.data.user_info.user_name;
            unitlevel_txt.Text = loadIndex.data.user_info.team_level.ToString();
            assestPath_txt.Text = Settings.globalSetting.userAssestPath;
            useUserPath_check.Checked = Settings.globalSetting.useUserAssestPath;
            ignoreManifest.Checked = Settings.globalSetting.ignoreManifestCheck;
            ignore2.Checked = Settings.globalSetting.useMyPlayerPrefab;
            cryptfile.Checked = Settings.globalSetting.fileNameEnrcypted;
            useOwnDB.Checked = Settings.globalSetting.useDBinStreamingAssestPath;
            logWebJson.Checked = Settings.globalSetting.forceLogBattleStart;
            uselog.Checked = Settings.globalSetting.useLog;
            //checkBox1701.Checked = Settings.globalSetting.use1701;
            replaceURLCheck.Checked = Settings.globalSetting.replaceManifestURL;
        }
        public void SaveMainSetting()
        {
            try
            {
                var loadIndex = Settings.loadData;
                loadIndex.data.user_info.user_name = username_txt.Text;
                loadIndex.data.user_info.team_level = int.Parse(unitlevel_txt.Text);

                Settings.globalSetting.userAssestPath = assestPath_txt.Text;
                Settings.globalSetting.useUserAssestPath = useUserPath_check.Checked;
                Settings.globalSetting.ignoreManifestCheck = ignoreManifest.Checked;
                Settings.globalSetting.replaceManifestURL = replaceURLCheck.Checked;
                Settings.globalSetting.useMyPlayerPrefab = ignore2.Checked;
                Settings.globalSetting.fileNameEnrcypted = cryptfile.Checked;
                Settings.globalSetting.useDBinStreamingAssestPath = useOwnDB.Checked;
                Settings.globalSetting.forceLogBattleStart = logWebJson.Checked;
                Settings.globalSetting.useLog = uselog.Checked;
                //Settings.globalSetting.use1701 = checkBox1701.Checked;
                Settings.Save(1);
                Settings.Save(4);
                MessageBox.Show("设置更改成功！重启生效");

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("错误：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ReflashClanSetting()
        {
            if (!Settings.FirstReset)
            {
                return;
            }
            cb_id.Value = Settings.clanSetting.clanbattleid;
            cb_lap.Value = Settings.clanSetting.lap;
            battle_time.Value = Settings.clanSetting.battleTime;
            boss_num.Value = Settings.clanSetting.order;
            boss_hp.Value = Settings.clanSetting.boss_info[Settings.clanSetting.order-1].current_hp;
            cb_seed.Value = Settings.clanSetting.battleSeed;
            checkBox1.Checked = Settings.clanSetting.fixSeed;
        }
        public void SaveClanSetting()
        {
            if (!Settings.FirstReset)
            {
                MessageBox.Show("请先进入会战界面!");
                return;
            }

            Settings.clanSetting.clanbattleid =(int)cb_id.Value;
            Settings.clanSetting.lap = (int)cb_lap.Value;
            Settings.clanSetting.battleTime = (int)battle_time.Value;
            Settings.clanSetting.order = (int)boss_num.Value;
            //Settings.clanSetting.boss_info[Settings.clanSetting.order-1].current_hp = (int)boss_hp.Value;
            for(int i = 0; i < 5; i++)
            {
                if (i < Settings.clanSetting.order - 1)
                    Settings.clanSetting.boss_info[i].current_hp = 0;
                else if (i == Settings.clanSetting.order - 1)
                    Settings.clanSetting.boss_info[i].current_hp = (int)boss_hp.Value;
                else
                    Settings.clanSetting.boss_info[i].current_hp = Settings.clanSetting.boss_info[i].max_hp;
            }
            Settings.clanSetting.battleSeed = (int)cb_seed.Value;
            Settings.clanSetting.fixSeed = checkBox1.Checked;
            Settings.Save(3);
            Settings.ReflashClanSetting();
            MessageBox.Show("设置更改成功！重进会战界面生效");

        }
        public void ReflashOtherSetting()
        {
            var vv = Settings.globalSetting.GetTimeStr();
            radioButton1.Checked = vv.Item1;
            radioButton2.Checked = !vv.Item1;
            textBox1.Text = vv.Item2;
            VersionText.Text = PCRSettings.Version.ToString();
        }
        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
		{
		}

		private void toolStripStatusLabel1_TextChanged(object sender, EventArgs e)
		{
			
		}
        public void ReflashBattleSetting()
        {
            showTimeCkeck.Checked = Settings.battleSetting.showExectTime;
            checkBox6.Checked = Settings.battleSetting.showBossDEF;
            checkBox2.Checked = Settings.battleSetting.useLogBarrier;
            timeCheck2.Checked = Settings.battleSetting.showRealFrame;
            showUICheck.Checked = Settings.battleSetting.showUI;
            checkBoxMutiTarget.Checked = Settings.battleSetting.mutiTargetShowMDEF;
        }
		

		

		private void username_txt_TextChanged(object sender, EventArgs e)
		{
		}

		private void clanname_txt_TextChanged(object sender, EventArgs e)
		{
		}

		
		private void tabPage1_Click(object sender, EventArgs e)
		{
		}
        public void OnLog(string str)
        {
            if (logBox.Text.Length <= 2500)
                logBox.Text += "\r\n" + str;
            else
                logBox.Text = str;
        }




        protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
        #region
        private void InitializeComponent()
		{
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.clanSetting = new System.Windows.Forms.TabPage();
            this.UpdateClanButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_seed = new System.Windows.Forms.NumericUpDown();
            this.boss_hp = new System.Windows.Forms.NumericUpDown();
            this.boss_num = new System.Windows.Forms.NumericUpDown();
            this.battle_time = new System.Windows.Forms.NumericUpDown();
            this.cb_lap = new System.Windows.Forms.NumericUpDown();
            this.cb_id = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Apply_CB_Config = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.MainSetting = new System.Windows.Forms.TabPage();
            this.replaceURLCheck = new System.Windows.Forms.CheckBox();
            this.uselog = new System.Windows.Forms.CheckBox();
            this.saveBaseSettingButton = new System.Windows.Forms.Button();
            this.logWebJson = new System.Windows.Forms.CheckBox();
            this.useOwnDB = new System.Windows.Forms.CheckBox();
            this.cryptfile = new System.Windows.Forms.CheckBox();
            this.ignore2 = new System.Windows.Forms.CheckBox();
            this.ignoreManifest = new System.Windows.Forms.CheckBox();
            this.useUserPath_check = new System.Windows.Forms.CheckBox();
            this.assestPath_txt = new System.Windows.Forms.TextBox();
            this.unitlevel_txt = new System.Windows.Forms.TextBox();
            this.username_txt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.battleTool = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.showUICheck = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.OtherTool = new System.Windows.Forms.Button();
            this.timeCheck2 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.speedlabel = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.showTimeCkeck = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.speedbar = new System.Windows.Forms.TrackBar();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.logBox = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ConfigVersion = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.VersionText = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBoxMutiTarget = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.clanSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cb_seed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boss_hp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boss_num)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.battle_time)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_lap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_id)).BeginInit();
            this.MainSetting.SuspendLayout();
            this.battleTool.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedbar)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 354);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(551, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "Ready.";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(167, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            this.toolStripStatusLabel1.TextChanged += new System.EventHandler(this.toolStripStatusLabel1_TextChanged);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 18);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // clanSetting
            // 
            this.clanSetting.Controls.Add(this.UpdateClanButton);
            this.clanSetting.Controls.Add(this.label8);
            this.clanSetting.Controls.Add(this.checkBox1);
            this.clanSetting.Controls.Add(this.label11);
            this.clanSetting.Controls.Add(this.label10);
            this.clanSetting.Controls.Add(this.label5);
            this.clanSetting.Controls.Add(this.cb_seed);
            this.clanSetting.Controls.Add(this.boss_hp);
            this.clanSetting.Controls.Add(this.boss_num);
            this.clanSetting.Controls.Add(this.battle_time);
            this.clanSetting.Controls.Add(this.cb_lap);
            this.clanSetting.Controls.Add(this.cb_id);
            this.clanSetting.Controls.Add(this.label4);
            this.clanSetting.Controls.Add(this.label7);
            this.clanSetting.Controls.Add(this.label6);
            this.clanSetting.Controls.Add(this.Apply_CB_Config);
            this.clanSetting.Controls.Add(this.label3);
            this.clanSetting.Controls.Add(this.label2);
            this.clanSetting.Controls.Add(this.label1);
            this.clanSetting.Location = new System.Drawing.Point(4, 36);
            this.clanSetting.Name = "clanSetting";
            this.clanSetting.Padding = new System.Windows.Forms.Padding(3);
            this.clanSetting.Size = new System.Drawing.Size(531, 301);
            this.clanSetting.TabIndex = 0;
            this.clanSetting.Text = "会战设置";
            this.clanSetting.UseVisualStyleBackColor = true;
            this.clanSetting.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // UpdateClanButton
            // 
            this.UpdateClanButton.Location = new System.Drawing.Point(415, 18);
            this.UpdateClanButton.Name = "UpdateClanButton";
            this.UpdateClanButton.Size = new System.Drawing.Size(90, 34);
            this.UpdateClanButton.TabIndex = 32;
            this.UpdateClanButton.Text = "重载";
            this.UpdateClanButton.UseVisualStyleBackColor = true;
            this.UpdateClanButton.Click += new System.EventHandler(this.UpdateClanButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(347, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 27);
            this.label8.TabIndex = 31;
            this.label8.Text = "HP";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(364, 161);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(174, 31);
            this.checkBox1.TabIndex = 30;
            this.checkBox1.Text = "使用固定的种子";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(208, 95);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(225, 23);
            this.label11.TabIndex = 29;
            this.label11.Text = "默认90s，补偿刀等自己调整";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(208, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(325, 23);
            this.label10.TabIndex = 28;
            this.label10.Text = "1-3A，4-10B，11-34C，35-45D，45+E";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(208, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(237, 23);
            this.label5.TabIndex = 27;
            this.label5.Text = "1016为巨蟹座，以后每次加一";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // cb_seed
            // 
            this.cb_seed.Location = new System.Drawing.Point(102, 157);
            this.cb_seed.Maximum = new decimal(new int[] {
            268435455,
            1042612833,
            542101086,
            0});
            this.cb_seed.Minimum = new decimal(new int[] {
            268435455,
            1042612833,
            542101086,
            -2147483648});
            this.cb_seed.Name = "cb_seed";
            this.cb_seed.Size = new System.Drawing.Size(239, 34);
            this.cb_seed.TabIndex = 26;
            this.cb_seed.Value = new decimal(new int[] {
            666,
            0,
            0,
            0});
            // 
            // boss_hp
            // 
            this.boss_hp.Location = new System.Drawing.Point(240, 124);
            this.boss_hp.Maximum = new decimal(new int[] {
            276447231,
            23283,
            0,
            0});
            this.boss_hp.Name = "boss_hp";
            this.boss_hp.Size = new System.Drawing.Size(101, 34);
            this.boss_hp.TabIndex = 25;
            this.boss_hp.Value = new decimal(new int[] {
            6000000,
            0,
            0,
            0});
            // 
            // boss_num
            // 
            this.boss_num.Location = new System.Drawing.Point(102, 124);
            this.boss_num.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.boss_num.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.boss_num.Name = "boss_num";
            this.boss_num.Size = new System.Drawing.Size(101, 34);
            this.boss_num.TabIndex = 24;
            this.boss_num.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // battle_time
            // 
            this.battle_time.Location = new System.Drawing.Point(102, 91);
            this.battle_time.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.battle_time.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.battle_time.Name = "battle_time";
            this.battle_time.Size = new System.Drawing.Size(100, 34);
            this.battle_time.TabIndex = 23;
            this.battle_time.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // cb_lap
            // 
            this.cb_lap.Location = new System.Drawing.Point(102, 58);
            this.cb_lap.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.cb_lap.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cb_lap.Name = "cb_lap";
            this.cb_lap.Size = new System.Drawing.Size(100, 34);
            this.cb_lap.TabIndex = 22;
            this.cb_lap.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cb_id
            // 
            this.cb_id.Location = new System.Drawing.Point(101, 25);
            this.cb_id.Maximum = new decimal(new int[] {
            1099,
            0,
            0,
            0});
            this.cb_id.Minimum = new decimal(new int[] {
            1001,
            0,
            0,
            0});
            this.cb_id.Name = "cb_id";
            this.cb_id.Size = new System.Drawing.Size(100, 34);
            this.cb_id.TabIndex = 21;
            this.cb_id.Value = new decimal(new int[] {
            1016,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 27);
            this.label4.TabIndex = 19;
            this.label4.Text = "当前种子：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(208, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 27);
            this.label7.TabIndex = 14;
            this.label7.Text = "王";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 27);
            this.label6.TabIndex = 7;
            this.label6.Text = "当前挑战：";
            // 
            // Apply_CB_Config
            // 
            this.Apply_CB_Config.Location = new System.Drawing.Point(27, 203);
            this.Apply_CB_Config.Name = "Apply_CB_Config";
            this.Apply_CB_Config.Size = new System.Drawing.Size(483, 45);
            this.Apply_CB_Config.TabIndex = 6;
            this.Apply_CB_Config.Text = "保存并应用";
            this.Apply_CB_Config.UseVisualStyleBackColor = true;
            this.Apply_CB_Config.Click += new System.EventHandler(this.Apply_CB_Config_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 27);
            this.label3.TabIndex = 4;
            this.label3.Text = "战斗时间：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "周目数量：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "会战序号：";
            // 
            // MainSetting
            // 
            this.MainSetting.Controls.Add(this.replaceURLCheck);
            this.MainSetting.Controls.Add(this.uselog);
            this.MainSetting.Controls.Add(this.saveBaseSettingButton);
            this.MainSetting.Controls.Add(this.logWebJson);
            this.MainSetting.Controls.Add(this.useOwnDB);
            this.MainSetting.Controls.Add(this.cryptfile);
            this.MainSetting.Controls.Add(this.ignore2);
            this.MainSetting.Controls.Add(this.ignoreManifest);
            this.MainSetting.Controls.Add(this.useUserPath_check);
            this.MainSetting.Controls.Add(this.assestPath_txt);
            this.MainSetting.Controls.Add(this.unitlevel_txt);
            this.MainSetting.Controls.Add(this.username_txt);
            this.MainSetting.Controls.Add(this.label9);
            this.MainSetting.Controls.Add(this.button1);
            this.MainSetting.Controls.Add(this.label23);
            this.MainSetting.Controls.Add(this.label22);
            this.MainSetting.Location = new System.Drawing.Point(4, 36);
            this.MainSetting.Name = "MainSetting";
            this.MainSetting.Size = new System.Drawing.Size(531, 301);
            this.MainSetting.TabIndex = 3;
            this.MainSetting.Text = "基础设置";
            this.MainSetting.UseVisualStyleBackColor = true;
            // 
            // replaceURLCheck
            // 
            this.replaceURLCheck.AutoSize = true;
            this.replaceURLCheck.Location = new System.Drawing.Point(215, 270);
            this.replaceURLCheck.Name = "replaceURLCheck";
            this.replaceURLCheck.Size = new System.Drawing.Size(236, 31);
            this.replaceURLCheck.TabIndex = 16;
            this.replaceURLCheck.Text = "将Manifest定向到本地";
            this.replaceURLCheck.UseVisualStyleBackColor = true;
            this.replaceURLCheck.CheckedChanged += new System.EventHandler(this.replaceURLCheck_CheckedChanged);
            // 
            // uselog
            // 
            this.uselog.AutoSize = true;
            this.uselog.Location = new System.Drawing.Point(215, 239);
            this.uselog.Name = "uselog";
            this.uselog.Size = new System.Drawing.Size(145, 31);
            this.uselog.TabIndex = 14;
            this.uselog.Text = "输出log信息";
            this.uselog.UseVisualStyleBackColor = true;
            // 
            // saveBaseSettingButton
            // 
            this.saveBaseSettingButton.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.saveBaseSettingButton.Location = new System.Drawing.Point(395, 153);
            this.saveBaseSettingButton.Name = "saveBaseSettingButton";
            this.saveBaseSettingButton.Size = new System.Drawing.Size(108, 98);
            this.saveBaseSettingButton.TabIndex = 13;
            this.saveBaseSettingButton.Text = "保存设置";
            this.saveBaseSettingButton.UseVisualStyleBackColor = true;
            this.saveBaseSettingButton.Click += new System.EventHandler(this.saveBaseSettingButton_click);
            // 
            // logWebJson
            // 
            this.logWebJson.AutoSize = true;
            this.logWebJson.Location = new System.Drawing.Point(215, 208);
            this.logWebJson.Name = "logWebJson";
            this.logWebJson.Size = new System.Drawing.Size(154, 31);
            this.logWebJson.TabIndex = 12;
            this.logWebJson.Text = "输出通信内容";
            this.logWebJson.UseVisualStyleBackColor = true;
            // 
            // useOwnDB
            // 
            this.useOwnDB.AutoSize = true;
            this.useOwnDB.Location = new System.Drawing.Point(23, 208);
            this.useOwnDB.Name = "useOwnDB";
            this.useOwnDB.Size = new System.Drawing.Size(160, 31);
            this.useOwnDB.TabIndex = 11;
            this.useOwnDB.Text = "使用自定义db";
            this.useOwnDB.UseVisualStyleBackColor = true;
            // 
            // cryptfile
            // 
            this.cryptfile.AutoSize = true;
            this.cryptfile.Location = new System.Drawing.Point(215, 177);
            this.cryptfile.Name = "cryptfile";
            this.cryptfile.Size = new System.Drawing.Size(174, 31);
            this.cryptfile.TabIndex = 10;
            this.cryptfile.Text = "资源文件名加密";
            this.cryptfile.UseVisualStyleBackColor = true;
            // 
            // ignore2
            // 
            this.ignore2.AutoSize = true;
            this.ignore2.Location = new System.Drawing.Point(23, 177);
            this.ignore2.Name = "ignore2";
            this.ignore2.Size = new System.Drawing.Size(174, 31);
            this.ignore2.TabIndex = 9;
            this.ignore2.Text = "防止修改注册表";
            this.ignore2.UseVisualStyleBackColor = true;
            this.ignore2.CheckedChanged += new System.EventHandler(this.ignore2_CheckedChanged);
            // 
            // ignoreManifest
            // 
            this.ignoreManifest.AutoSize = true;
            this.ignoreManifest.Location = new System.Drawing.Point(215, 146);
            this.ignoreManifest.Name = "ignoreManifest";
            this.ignoreManifest.Size = new System.Drawing.Size(194, 31);
            this.ignoreManifest.TabIndex = 8;
            this.ignoreManifest.Text = "忽略资源联网检查";
            this.ignoreManifest.UseVisualStyleBackColor = true;
            this.ignoreManifest.CheckedChanged += new System.EventHandler(this.ignoreManifest_CheckedChanged);
            // 
            // useUserPath_check
            // 
            this.useUserPath_check.AutoSize = true;
            this.useUserPath_check.Location = new System.Drawing.Point(23, 146);
            this.useUserPath_check.Name = "useUserPath_check";
            this.useUserPath_check.Size = new System.Drawing.Size(214, 31);
            this.useUserPath_check.TabIndex = 7;
            this.useUserPath_check.Text = "使用自定义资源路径";
            this.useUserPath_check.UseVisualStyleBackColor = true;
            this.useUserPath_check.CheckedChanged += new System.EventHandler(this.useUserPath_check_CheckedChanged);
            // 
            // assestPath_txt
            // 
            this.assestPath_txt.Location = new System.Drawing.Point(96, 105);
            this.assestPath_txt.MaxLength = 10000;
            this.assestPath_txt.Name = "assestPath_txt";
            this.assestPath_txt.Size = new System.Drawing.Size(408, 34);
            this.assestPath_txt.TabIndex = 6;
            // 
            // unitlevel_txt
            // 
            this.unitlevel_txt.Location = new System.Drawing.Point(96, 60);
            this.unitlevel_txt.MaxLength = 10;
            this.unitlevel_txt.Name = "unitlevel_txt";
            this.unitlevel_txt.Size = new System.Drawing.Size(151, 34);
            this.unitlevel_txt.TabIndex = 3;
            this.unitlevel_txt.TextChanged += new System.EventHandler(this.clanname_txt_TextChanged);
            // 
            // username_txt
            // 
            this.username_txt.Location = new System.Drawing.Point(96, 17);
            this.username_txt.MaxLength = 10;
            this.username_txt.Name = "username_txt";
            this.username_txt.Size = new System.Drawing.Size(151, 34);
            this.username_txt.TabIndex = 1;
            this.username_txt.TextChanged += new System.EventHandler(this.username_txt_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 27);
            this.label9.TabIndex = 5;
            this.label9.Text = "资源路径：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(286, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(218, 58);
            this.button1.TabIndex = 4;
            this.button1.Text = "设置角色数据";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(16, 63);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(112, 27);
            this.label23.TabIndex = 2;
            this.label23.Text = "玩家等级：";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(16, 20);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(92, 27);
            this.label22.TabIndex = 0;
            this.label22.Text = "用户名：";
            // 
            // battleTool
            // 
            this.battleTool.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.battleTool.Controls.Add(this.MainSetting);
            this.battleTool.Controls.Add(this.clanSetting);
            this.battleTool.Controls.Add(this.tabPage2);
            this.battleTool.Controls.Add(this.tabPage1);
            this.battleTool.Controls.Add(this.tabPage3);
            this.battleTool.Location = new System.Drawing.Point(6, 12);
            this.battleTool.Name = "battleTool";
            this.battleTool.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.battleTool.SelectedIndex = 0;
            this.battleTool.Size = new System.Drawing.Size(539, 341);
            this.battleTool.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBoxMutiTarget);
            this.tabPage2.Controls.Add(this.showUICheck);
            this.tabPage2.Controls.Add(this.label19);
            this.tabPage2.Controls.Add(this.label18);
            this.tabPage2.Controls.Add(this.OtherTool);
            this.tabPage2.Controls.Add(this.timeCheck2);
            this.tabPage2.Controls.Add(this.checkBox2);
            this.tabPage2.Controls.Add(this.speedlabel);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.checkBox6);
            this.tabPage2.Controls.Add(this.showTimeCkeck);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.speedbar);
            this.tabPage2.Location = new System.Drawing.Point(4, 36);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(531, 301);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "战斗设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // showUICheck
            // 
            this.showUICheck.AutoSize = true;
            this.showUICheck.Location = new System.Drawing.Point(23, 209);
            this.showUICheck.Name = "showUICheck";
            this.showUICheck.Size = new System.Drawing.Size(174, 31);
            this.showUICheck.TabIndex = 27;
            this.showUICheck.Text = "不显示治疗数字";
            this.showUICheck.UseVisualStyleBackColor = true;
            this.showUICheck.CheckedChanged += new System.EventHandler(this.showUICheck_CheckedChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(269, 111);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(214, 23);
            this.label19.TabIndex = 26;
            this.label19.Text = "开启后秒数显示可能不准！";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label18.ForeColor = System.Drawing.Color.Blue;
            this.label18.Location = new System.Drawing.Point(111, 54);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(265, 23);
            this.label18.TabIndex = 25;
            this.label18.Text = "鼠标放在滑动条上然后用滚轮调节";
            // 
            // OtherTool
            // 
            this.OtherTool.Location = new System.Drawing.Point(279, 173);
            this.OtherTool.Name = "OtherTool";
            this.OtherTool.Size = new System.Drawing.Size(207, 53);
            this.OtherTool.TabIndex = 24;
            this.OtherTool.Text = "设置UB时间和快捷键";
            this.OtherTool.UseVisualStyleBackColor = true;
            this.OtherTool.Click += new System.EventHandler(this.OtherTool_Click_1);
            // 
            // timeCheck2
            // 
            this.timeCheck2.AutoSize = true;
            this.timeCheck2.Location = new System.Drawing.Point(186, 111);
            this.timeCheck2.Name = "timeCheck2";
            this.timeCheck2.Size = new System.Drawing.Size(94, 31);
            this.timeCheck2.TabIndex = 23;
            this.timeCheck2.Text = "渲染帧";
            this.timeCheck2.UseVisualStyleBackColor = true;
            this.timeCheck2.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(23, 173);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(134, 31);
            this.checkBox2.TabIndex = 22;
            this.checkBox2.Text = "启用对数盾";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // speedlabel
            // 
            this.speedlabel.AutoSize = true;
            this.speedlabel.Location = new System.Drawing.Point(467, 15);
            this.speedlabel.Name = "speedlabel";
            this.speedlabel.Size = new System.Drawing.Size(24, 27);
            this.speedlabel.TabIndex = 21;
            this.speedlabel.Text = "1";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(81, 74);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(382, 23);
            this.label15.TabIndex = 14;
            this.label15.Text = "警告：速度过快或过慢可能会导致意想不到的bug";
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(23, 142);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(198, 31);
            this.checkBox6.TabIndex = 13;
            this.checkBox6.Text = "实时显示boss双防";
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.CheckedChanged += new System.EventHandler(this.checkBox6_CheckedChanged);
            // 
            // showTimeCkeck
            // 
            this.showTimeCkeck.AutoSize = true;
            this.showTimeCkeck.Location = new System.Drawing.Point(23, 111);
            this.showTimeCkeck.Name = "showTimeCkeck";
            this.showTimeCkeck.Size = new System.Drawing.Size(194, 31);
            this.showTimeCkeck.TabIndex = 12;
            this.showTimeCkeck.Text = "时间显示精确到帧";
            this.showTimeCkeck.UseVisualStyleBackColor = true;
            this.showTimeCkeck.CheckedChanged += new System.EventHandler(this.showTimeCkeck_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(371, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(112, 27);
            this.label12.TabIndex = 10;
            this.label12.Text = "当前速度：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(19, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(112, 27);
            this.label14.TabIndex = 9;
            this.label14.Text = "全局变速：";
            // 
            // speedbar
            // 
            this.speedbar.Location = new System.Drawing.Point(115, 6);
            this.speedbar.Maximum = 5;
            this.speedbar.Minimum = -6;
            this.speedbar.Name = "speedbar";
            this.speedbar.Size = new System.Drawing.Size(234, 56);
            this.speedbar.TabIndex = 7;
            this.speedbar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.speedbar.Scroll += new System.EventHandler(this.speedbar_Scroll);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.logBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 36);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(531, 301);
            this.tabPage1.TabIndex = 5;
            this.tabPage1.Text = "log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // logBox
            // 
            this.logBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logBox.Location = new System.Drawing.Point(2, 3);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(506, 268);
            this.logBox.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ConfigVersion);
            this.tabPage3.Controls.Add(this.label20);
            this.tabPage3.Controls.Add(this.VersionText);
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.label17);
            this.tabPage3.Controls.Add(this.radioButton2);
            this.tabPage3.Controls.Add(this.radioButton1);
            this.tabPage3.Controls.Add(this.label16);
            this.tabPage3.Controls.Add(this.textBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 36);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(531, 301);
            this.tabPage3.TabIndex = 6;
            this.tabPage3.Text = "备用";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ConfigVersion
            // 
            this.ConfigVersion.Location = new System.Drawing.Point(286, 256);
            this.ConfigVersion.Name = "ConfigVersion";
            this.ConfigVersion.Size = new System.Drawing.Size(227, 43);
            this.ConfigVersion.TabIndex = 11;
            this.ConfigVersion.Text = "更新版本号";
            this.ConfigVersion.UseVisualStyleBackColor = true;
            this.ConfigVersion.Click += new System.EventHandler(this.ConfigVersion_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(17, 263);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(98, 27);
            this.label20.TabIndex = 10;
            this.label20.Text = "版本号： ";
            // 
            // VersionText
            // 
            this.VersionText.Location = new System.Drawing.Point(101, 261);
            this.VersionText.Name = "VersionText";
            this.VersionText.Size = new System.Drawing.Size(179, 34);
            this.VersionText.TabIndex = 9;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(86, 95);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(149, 43);
            this.button3.TabIndex = 8;
            this.button3.Text = "设置rank bonus";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(104, 220);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(192, 27);
            this.label13.TabIndex = 7;
            this.label13.Text = "请在进入游戏后使用";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(86, 174);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(200, 43);
            this.button2.TabIndex = 6;
            this.button2.Text = "使用SQL文件更新数据库";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(232, 47);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(332, 27);
            this.label17.TabIndex = 5;
            this.label17.Text = "时间戳请复制到别处修改后黏贴即可";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(86, 49);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(153, 31);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "使用系统时间";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(86, 18);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(173, 31);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.Text = "使用自定义时间";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(17, 18);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(78, 27);
            this.label16.TabIndex = 2;
            this.label16.Text = "时间： ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(236, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(210, 34);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "2021/06/25 10:00:00";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // checkBoxMutiTarget
            // 
            this.checkBoxMutiTarget.AutoSize = true;
            this.checkBoxMutiTarget.Location = new System.Drawing.Point(23, 246);
            this.checkBoxMutiTarget.Name = "checkBoxMutiTarget";
            this.checkBoxMutiTarget.Size = new System.Drawing.Size(174, 31);
            this.checkBoxMutiTarget.TabIndex = 28;
            this.checkBoxMutiTarget.Text = "多目标显示魔防";
            this.checkBoxMutiTarget.UseVisualStyleBackColor = true;
            this.checkBoxMutiTarget.CheckedChanged += new System.EventHandler(this.checkBoxMutiTarget_CheckedChanged);
            // 
            // ManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 380);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.battleTool);
            this.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManagerForm";
            this.Text = "DMM辅助工具";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.clanSetting.ResumeLayout(false);
            this.clanSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cb_seed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boss_hp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boss_num)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.battle_time)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_lap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_id)).EndInit();
            this.MainSetting.ResumeLayout(false);
            this.MainSetting.PerformLayout();
            this.battleTool.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.speedbar)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
        #endregion
        private void saveBaseSettingButton_click(object sender, EventArgs e)
        {
            SaveMainSetting();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Settings.CanSetCharData())
            {
                try
                {
                    var Main = new Main();
                    Main.Show();
                    Main.Reflash();
                }
                catch(System.Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message + "\n" + ex.StackTrace);

                }
            }
            else
            {
                MessageBox.Show("请等待游戏启动完毕!");
            }
        }

        private void Apply_CB_Config_Click(object sender, EventArgs e)
        {
            SaveClanSetting();
        }

        private void showTimeCkeck_CheckedChanged(object sender, EventArgs e)
        {
            Settings.battleSetting.showExectTime = showTimeCkeck.Checked;
            Settings.Save(5);
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            Settings.battleSetting.showBossDEF = checkBox6.Checked;
            Settings.Save(5);
        }

        private void speedbar_Scroll(object sender, EventArgs e)
        {
            float sc = (float)Math.Pow(2, speedbar.Value);
            bool rs = Settings.SetBattleTimeScale(sc);
            speedlabel.Text = "x" + (rs?sc.ToString():"ERROR!");
        }

        private void ignore2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Settings.battleSetting.useLogBarrier = checkBox2.Checked;
            Settings.Save(5);
        }

        private void useUserPath_check_CheckedChanged(object sender, EventArgs e)
        {

        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SetTime();
        }
        private void SetTime()
        {
            if (PCRSettings.Instance.globalSetting.SetFixTime(radioButton1.Checked, textBox1.Text))
            {
                Settings.Save(4);
                
            }
            else
            {
                MessageBox.Show("设置失败！");
            }
            ReflashOtherSetting();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Settings.battleSetting.showRealFrame = timeCheck2.Checked;
            Settings.Save(5);
        }

        private void OtherTool_Click_1(object sender, EventArgs e)
        {
            PCRSettings.Instance.OtherToolButton?.Invoke();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                SetTime();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                SetTime();
        }

        private void UpdateClanButton_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("是否重载会战信息？\n（只有切换会战id才需要重载，变更周目不需要）","提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                PCRSettings.Instance.SetTopData();
                Reflash();
                MessageBox.Show("完成，请重进会战界面！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = EXCELHelper.OpenSQLFile();
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("文件无效！");
                return;
            }
            bool flag = PCRCalculator.Hook.db.ExecDB(path,true);
            if (flag)
            {
                MessageBox.Show("成功，重启游戏后生效！");
            }
            else
            {
                MessageBox.Show("失败！");
            }
        }

        private void checkBox1701_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var window = new PCRCalculator.UI.RankBounsSetting();
            window.Show();
        }

        private void showUICheck_CheckedChanged(object sender, EventArgs e)
        {
            Settings.battleSetting.showUI = showUICheck.Checked;
            Settings.Save(5);
        }

        private void replaceURLCheck_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ignoreManifest_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ConfigVersion_Click(object sender, EventArgs e)
        {
            try
            {
                PCRSettings.Version = int.Parse(VersionText.Text);
                MessageBox.Show($"成功，当前版本{PCRSettings.Version}，重启生效！");
            }
            catch (System.FormatException)
            {
                MessageBox.Show("输入错误！");
            }
        }

        private void checkBoxMutiTarget_CheckedChanged(object sender, EventArgs e)
        {
            Settings.battleSetting.mutiTargetShowMDEF = checkBoxMutiTarget.Checked;
            Settings.Save(5);
        }
    }
}
