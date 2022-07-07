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
    public partial class BattleStatePage : Form
    {
        public BattleStatePage()
        {
            InitializeComponent();
        }

        
        private Action<int> OnChangeLabelType;
        // List<UnitTimeLineGroup> timeLineGroups = new List<UnitTimeLineGroup>();
        private List<UnitTimeLineGroupImage> unitTimeLineGroupImages = new List<UnitTimeLineGroupImage>();
        private TimeLabelType timeType = TimeLabelType.LOGIC;
        public static int endframereal = 0;
        public static int endframelogic = 0;
        Dictionary<int, List<UnitStateInputData>> allUnitStateInputDic;
        Dictionary<int, List<UnitAbnormalStateChangeData>> allUnitAbnormalStateInputDic;
        List<ClickEvevtData> clickEventList = new List<ClickEvevtData>();
        Dictionary<string, int> comInfoDic = new Dictionary<string, int>();
        string buff_all = "所有";
        private void showBuffCheck_CheckedChanged(object sender, EventArgs e)
        {
            Reflash();
        }
        public void Init(int endframeLogic,int endFrameReal,Dictionary<int, List<UnitStateInputData>> allUnitStateInputDic, Dictionary<int, List<UnitAbnormalStateChangeData>> allUnitAbnormalStateInputDic)
        {
            endframereal = endFrameReal;
            endframelogic = endframeLogic;
            this.allUnitAbnormalStateInputDic = allUnitAbnormalStateInputDic;
            this.allUnitStateInputDic = allUnitStateInputDic;
            comboBox1.Items.Add(buff_all);
            comInfoDic.Add(buff_all, 0);
            foreach (object obj in Enum.GetValues(typeof(Elements.UnitCtrl.BuffParamKind)))
            {
                string objName = PCRBattle.Instance.GetBuffName((int)obj);
                if (!comInfoDic.ContainsKey(objName))
                {
                    comboBox1.Items.Add(objName);
                    comInfoDic.Add(objName, (int)obj);
                }
            
            }
            /*int basePos = 0;
            foreach(int unitid in allUnitStateInputDic.Keys)
            {
                string name = PCRSettings.Instance.GetUnitNameByID(unitid);
                UnitTimeLineGroup unitTimeLineGroup = new UnitTimeLineGroup();                
                unitTimeLineGroup.Init(name, panel1, timeType, basePos, showBuffCheck.Checked,allUnitStateInputDic[unitid], allUnitAbnormalStateInputDic[unitid]);
                unitTimeLineGroup.UpdateHeight(basePos);
                basePos += unitTimeLineGroup.Height;
            }*/
            Reflash();
        }
        public void Reflash()
        {
            unitTimeLineGroupImages.Clear();
            clickEventList.Clear();
            int basePos = 0;
            foreach (int unitid in allUnitStateInputDic.Keys)
            {
                string name = PCRSettings.Instance.GetUnitNameByID(unitid);
                UnitTimeLineGroupImage unitTimeLineGroup = new UnitTimeLineGroupImage();
                List<UnitAbnormalStateChangeData> abnormals = new List<UnitAbnormalStateChangeData>();
                if (allUnitAbnormalStateInputDic.TryGetValue(unitid, out var value))
                {
                    abnormals = value;
                }
                unitTimeLineGroup.Init(name, timeType, basePos, showBuffCheck.Checked,GetCombSelectBuff(), allUnitStateInputDic[unitid], abnormals,clickEventList,SimpleBuff.Checked);
                unitTimeLineGroup.UpdateHeight(basePos);
                basePos += unitTimeLineGroup.Height;
                unitTimeLineGroupImages.Add(unitTimeLineGroup);
            }
            basePos += 50;
            int maxLength = (int)((timeType == TimeLabelType.REAL ? endframereal : endframelogic) * UnitTimeLineGroupImage.scaleValue) + 400;
            Image image = ImageTool.ImageTool.DrawStateImage(maxLength, basePos, unitTimeLineGroupImages);
            pictureBox1.Image = image;
            //PCRSettings.Instance.SaveDataToFile("BUFF_RESULT", unitTimeLineGroupImages);
        }
        private int GetCombSelectBuff()
        {
            return comInfoDic[(comboBox1.SelectedItem ?? buff_all).ToString()];
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void timeType1_CheckedChanged(object sender, EventArgs e)
        {
            if (timeType1.Checked)
            {
                timeType = TimeLabelType.LOGIC;
                Reflash();
            }
        }

        private void timeType2_CheckedChanged(object sender, EventArgs e)
        {
            if (timeType2.Checked)
            {
                timeType = TimeLabelType.REAL;
                Reflash();
            }

        }

        private void timeType3_CheckedChanged(object sender, EventArgs e)
        {
            if (timeType3.Checked)
            {
                timeType = TimeLabelType.LOGICINT;
                Reflash();
            }
        }

        private void SimpleBuff_CheckedChanged(object sender, EventArgs e)
        {
            Reflash();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs e0 = e as MouseEventArgs;
            if (e0 != null)
            {
                Point p = e0.Location;
                OnClickPicture(p);
            }
            else
            {
                MessageBox.Show("ERROR!");
            }
        }
        private void OnClickPicture(Point p)
        {
            foreach(var item in clickEventList)
            {
                if (item.rectangle1.Contains(p))
                {
                    item.action1?.Invoke();
                }
            }
        }
        public static void OpenStateDetailPage(UnitStateInputData data)
        {
            if (data.skillData == null)
                return;
            StateDetailPage stateDetailPage = new StateDetailPage();
            stateDetailPage.Init(data);
            stateDetailPage.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Reflash();
        }
    }
    public enum TimeLabelType { LOGIC = 0, REAL = 1, LOGICINT = 2 }

    /*public class UnitTimeLineGroup
    {
        public string unitName;
        public List<ButtonEX> stateButtons = new List<ButtonEX>();
        public List<ButtonEX> buffButtons = new List<ButtonEX>();
        public Label nameLabel;
        public Panel parent;
        public TimeLabelType timeType;
        public int basePosY;
        public int Height = 110;
        public int StartHeight = 110;
        public int BuffStartHight => Height - ButtonEX.sizey[0];
        public float scaleValue = 0.4f;
        public int startPos = 100;
        public int abnormalPrefabHight = 20;
        public int minLength = 10;


        public static string[] stateNames = new string[8] { "等待", "普攻", "UB", "技能", "走路", "无法行动", "死亡", "开局" };

        public Color[] stateColors => PCRBattle.StateColors;
        public Color[] buffColors => PCRBattle.BuffColors;
        public Color[] abnormalStateColors => PCRBattle.AbnormalColors;

        private List<ToEndAbnormalState> toEndAbnormalData = new List<ToEndAbnormalState>();
        private List<int> currentAbnormalButtonEndPosList = new List<int>();

        public void Init(string name,Panel panel, TimeLabelType  timeType,int basePosY,bool showFuff,List<UnitStateInputData> unitStates, List<UnitAbnormalStateChangeData> unitAbnormals)
        {
            unitName = name;
            parent = panel;
            this.timeType = timeType;
            this.basePosY = basePosY;
            nameLabel = new Label();
            nameLabel.AutoSize = true;
            nameLabel.Location = new System.Drawing.Point(20, basePosY + 50);
            nameLabel.Size = new System.Drawing.Size(29, 12);
            nameLabel.Font = new System.Drawing.Font("宋体", 12F);
            parent.Controls.Add(nameLabel);
            foreach(var data in unitStates)
            {
                AddStateButtons(data.from, data.fromReal, data.to, data.toReal, data.type, data.Onclick);
            }
            if(showFuff)
            foreach(var data in unitAbnormals)
            {
                AddAbnormalStateButtons(data,()=> { data.ShowDetail(); });
            }
        }
        public void ClearAll()
        {
            foreach (var bt in stateButtons)
            {
                bt.Clear();
            }
            foreach (var bt2 in buffButtons)
            {
                bt2.Clear();
            }
            stateButtons.Clear();
            buffButtons.Clear();
            Height = StartHeight;
            toEndAbnormalData.Clear();
            currentAbnormalButtonEndPosList.Clear();
        }
        public void UpdateHeight(int basePosY)
        {
            this.basePosY = basePosY;
            int diff = BuffStartHight - abnormalPrefabHight * currentAbnormalButtonEndPosList.Count;
            if (diff <= 0)
            {
                Height -= diff;
                foreach (var bt in stateButtons)
                {
                    bt.ResizeY(-diff);
                }
                foreach (var bt2 in buffButtons)
                {
                    bt2.ResizeY(-diff);
                }
            }

        }
        public void AddStateButtons(int start, int startReal, int end, int endReal, int stateInt, System.Action action = null)
        {
            int length1 = (int)Math.Round((end - start) * scaleValue);
            int length2 = (int)Math.Round((endReal - startReal) * scaleValue);
            int length = timeType == TimeLabelType.REAL ? length2 : length1;
            ButtonEX buttonEX = new ButtonEX();
            int posx = (int)Math.Round((timeType == TimeLabelType.REAL ?start:startReal)*scaleValue + startPos);
            int scale = Math.Max(minLength, length);
            int posy = Height + basePosY;
            buttonEX.Init(parent, timeType, 1, stateColors[stateInt%stateColors.Length], (int)posx, posy, scale, stateNames[stateInt%stateNames.Length], start == 0, start, startReal, true, end, endReal, action);
            stateButtons.Add(buttonEX);
        }
        public void AddAbnormalStateButtons(UnitAbnormalStateChangeData changeData, System.Action action = null)
        {
            int i = 0;
            bool flag1 = false;
            bool flag2 = changeData.endFrameCountLogic == (int)PCRBattle.Instance.battleTotalTime * 60;
            if (flag2)
            {
                var toendData = toEndAbnormalData.Find(a0 => a0.changeData.GetDescription() == changeData.GetDescription());
                if (toendData != null)
                {
                    i = toendData.position;
                    flag1 = true;
                }
            }
            if (!flag1)
            {
                for (; i < currentAbnormalButtonEndPosList.Count; i++)
                {
                    if (currentAbnormalButtonEndPosList[i] <= changeData.startFrameCountLogic)
                    {
                        currentAbnormalButtonEndPosList[i] = changeData.endFrameCountLogic;
                        break;
                    }
                }
                if (i >= currentAbnormalButtonEndPosList.Count)
                {
                    currentAbnormalButtonEndPosList.Add(changeData.endFrameCountLogic);
                }
            }
            if (flag2 && !flag1)
            {
                toEndAbnormalData.Add(new ToEndAbnormalState(changeData, i));
            }
            //a.transform.localPosition = new Vector3(changeData.startFrameCountLogic * scaleValue, abnormalPrefabHight * i, 0);
            //MaxHight = Mathf.Max(MaxHight, abnormalPrefabHight * i);
            int length1 = (int)(Math.Max(minLength, changeData.endFrameCountLogic - changeData.startFrameCountLogic) * scaleValue);
            int length2 = (int)(Math.Max(minLength, changeData.endFrameCountReal - changeData.startFrameCountReal) * scaleValue);
            int length = timeType == TimeLabelType.REAL ? length2 : length1;
            //a.GetComponent<RectTransform>().sizeDelta = new Vector2(length, abnormalPrefabHight);
            //a.GetComponent<GuildCalcButton>().SetButton(GetButtonColor(changeData), changeData.GetDescription(), changeData.startFrameCountLogic, changeData.endFrameCountLogic, !flag2, changeData.ShowDetail);
            //prefabs_2.Add(a);
            ButtonEX buttonEX = new ButtonEX();
            float posx = changeData.startFrameCountLogic * scaleValue + startPos;
            int posy = BuffStartHight - abnormalPrefabHight * i + basePosY;
            buttonEX.Init(parent, timeType, 1, GetButtonColor(changeData), (int)posx, posy, length, changeData.GetDescription(), true, changeData.startFrameCountLogic, changeData.startFrameCountReal, true, changeData.endFrameCountLogic, changeData.endFrameCountReal, action);
            buffButtons.Add(buttonEX);
        }
        private Color GetButtonColor(UnitAbnormalStateChangeData data)
        {
            if (data.isBuff)
            {
                return buffColors[data.BUFF_Type % buffColors.Length];
            }
            else
            {
                return abnormalStateColors[(int)data.CurrentAbnormalState % abnormalStateColors.Length];
            }
        }

    }
    public class ButtonEX
    {
        public Button button1;
        public Label leftLabel;
        public Label rightLabel;
        public int type;
        public int leftValueLogic;
        public int leftValueReal;
        public int rightValueLogic;
        public int rightValuereal;
        public static int[] sizey = new int[3] { 35, 20, 25 };
        const int LabelFix_x = 6;
        int localy = 0;

        Panel parent;
        public void ChangeLabeType(TimeLabelType type)
        {
            switch (type)
            {
                case TimeLabelType.LOGIC:
                    SetLabel(leftValueLogic + "", rightValueLogic + "");
                    break;
                case TimeLabelType.REAL:
                    SetLabel(leftValueReal + "", rightValuereal + "");
                    break;
                case TimeLabelType.LOGICINT:
                    float left = PCRBattle.Instance.battleTotalTime - leftValueLogic / 60.0f;
                    float right = PCRBattle.Instance.battleTotalTime - rightValueLogic / 60.0f;
                    SetLabel(left.ToString("F1"), right.ToString("F1"));
                    break;

            }
        }
        private void SetLabel(string a, string b)
        {
            if (leftLabel != null)
                leftLabel.Text = a;
            if (rightLabel != null)
                rightLabel.Text = b;

        }
        public void ResizeY(int addFix)
        {
            int posy = button1.Location.Y + addFix;
            button1.Location = new Point(button1.Location.X, posy);
            if (leftLabel != null)
                leftLabel.Location = new Point(leftLabel.Location.X, localy + posy);
            if (rightLabel != null)
                rightLabel.Location = new Point(rightLabel.Location.X, localy + posy);
        }
        public void Clear()
        {
            parent.Controls.Remove(button1);
            if (leftLabel != null)
                parent.Controls.Remove(leftLabel);
            if (rightLabel != null)
                parent.Controls.Remove(rightLabel);
        }
        public void Init(Panel parent, TimeLabelType type, int buttonType, Color color, int posx, int posy, int sizex, string buttonName, bool showLeftText = false, int leftvalue1 = 0, int leftvalue2 = 0, bool showRightText = true, int rightValue1 = 100, int rightValue2 = 100, Action OnClick = null)
        {
            this.parent = parent;
            localy = sizey[buttonType];
            leftValueLogic = leftvalue1;
            leftValueReal = leftvalue2;
            rightValueLogic = rightValue1;
            rightValuereal = rightValue2;
            button1 = new Button();
            button1.BackColor = color;
            button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            button1.Location = new System.Drawing.Point(posx, posy);
            button1.Size = new System.Drawing.Size(sizex, sizey[buttonType]);
            button1.Text = buttonName;
            button1.UseVisualStyleBackColor = false;
            button1.Click += new System.EventHandler((a, b) => { OnClick?.Invoke(); });
            parent.Controls.Add(button1);
            if (showLeftText)
            {
                leftLabel = new Label();
                leftLabel.AutoSize = true;
                leftLabel.Location = new System.Drawing.Point(posx - LabelFix_x, localy + posy);
                leftLabel.Size = new System.Drawing.Size(29, 12);
                parent.Controls.Add(leftLabel);
            }
            if (showRightText)
            {
                rightLabel = new Label();
                rightLabel.AutoSize = true;
                rightLabel.Location = new System.Drawing.Point(posx - LabelFix_x + sizex, localy + posy);
                rightLabel.Size = new System.Drawing.Size(29, 12);
                parent.Controls.Add(rightLabel);
            }
            ChangeLabeType(type);
        }
    }*/
    public class UnitTimeLineGroupImage
    {
        public string unitName;
        public List<ButtonEXImage> stateButtons = new List<ButtonEXImage>();
        public List<ButtonEXImage> buffButtons = new List<ButtonEXImage>();
        public List<ImageDrawData> otherTexts = new List<ImageDrawData>();
        public TimeLabelType timeType;
        public int basePosY;
        public int Height = 110;
        public int StartHeight = 110;
        public int BuffStartHight => Height - ButtonEXImage.sizey[0];
        public static float scaleValue = 0.7f;
        public static int startPos = 100;
        public static int abnormalPrefabHight = 20;
        public static int minLength = 10;


        public static string[] stateNames = new string[] { "等待", "普攻", "UB", "技能", "走路", "无法行动", "召唤","死亡", "开局","失败","???" };

        public Color[] stateColors => PCRBattle.StateColors;
        public Color[] buffColors => PCRBattle.BuffColors;
        public Color[] abnormalStateColors => PCRBattle.AbnormalColors;

        private List<ToEndAbnormalState> toEndAbnormalData = new List<ToEndAbnormalState>();
        private List<int> currentAbnormalButtonEndPosList = new List<int>();
        List<ClickEvevtData> clickEventList;
        public void Init(string name, TimeLabelType timeType, int basePosY, bool showFuff,int showType, List<UnitStateInputData> unitStates, List<UnitAbnormalStateChangeData> unitAbnormals, List<ClickEvevtData> clickEventDic, bool simpleBuff = true)
        {
            this.clickEventList = clickEventDic;
            unitName = name;
            this.timeType = timeType;
            this.basePosY = basePosY;
            ImageDrawData nameText = new ImageDrawData(2, 20, basePosY + Height , Color.Black, 0, 0, 12, unitName);
            otherTexts.Add(nameText);
            /*nameLabel = new Label();
            nameLabel.AutoSize = true;
            nameLabel.Location = new System.Drawing.Point(20, basePosY + 50);
            nameLabel.Size = new System.Drawing.Size(29, 12);
            nameLabel.Font = new System.Drawing.Font("宋体", 12F);
            parent.Controls.Add(nameLabel);*/
            foreach (var data in unitStates)
            {
                AddStateButtons(data.from, data.fromReal, data.to, data.toReal, data.type,()=> BattleStatePage.OpenStateDetailPage(data));
            }
            if (showFuff)
                foreach (var data in unitAbnormals)
                {
                    if (showType == 0 || (data.isBuff && data.BUFF_Type == showType))
                        AddAbnormalStateButtons(data, () => { data.ShowDetail(); }, simpleBuff);
                }
        }        
        public void UpdateHeight(int basePosY)
        {
            this.basePosY = basePosY;
            int diff = BuffStartHight - abnormalPrefabHight *( currentAbnormalButtonEndPosList.Count+1);
            if (diff <= 0)
            {
                Height -= diff;
                foreach (var bt in stateButtons)
                {
                    bt.ResizeY(-diff);
                }
                foreach (var bt2 in buffButtons)
                {
                    bt2.ResizeY(-diff);
                }
                foreach(var ddd in otherTexts)
                {
                    ddd.posY -= diff;
                }
            }

        }
        public void AddStateButtons(int start, int startReal, int end, int endReal, int stateInt, System.Action action = null)
        {
            if (start == end && startReal == endReal)
                return;
            int length1 = (int)Math.Round((end - start) * scaleValue);
            int length2 = (int)Math.Round((endReal - startReal) * scaleValue);
            int length = timeType == TimeLabelType.REAL ? length2 : length1;
            ButtonEXImage buttonEX = new ButtonEXImage();
            int posx = (int)Math.Round((timeType == TimeLabelType.REAL ? startReal:start) * scaleValue + startPos);
            int scale = Math.Max(minLength, length);
            int posy = Height + basePosY;
            buttonEX.Init(timeType, 1, stateColors[stateInt % stateColors.Length], (int)posx, posy, scale, stateNames[stateInt % stateNames.Length], start == 0, start, startReal, true, end, endReal,clickEventList,action);
            stateButtons.Add(buttonEX);
        }
        public void AddAbnormalStateButtons(UnitAbnormalStateChangeData changeData, System.Action action = null,bool simple = true)
        {
            if (changeData.startFrameCountLogic == changeData.endFrameCountLogic && changeData.startFrameCountReal == changeData.endFrameCountReal)
                return;
            int i = 0;
            bool flag1 = false;
            bool flag2 = changeData.endFrameCountLogic == (int)PCRBattle.Instance.battleTotalTime * 60;
            if (flag2&&simple)
            {
                var toendData = toEndAbnormalData.Find(a0 => a0.changeData.GetDescription() == changeData.GetDescription());
                if (toendData != null)
                {
                    i = toendData.position;
                    flag1 = true;
                }
            }
            if (!flag1)
            {
                for (; i < currentAbnormalButtonEndPosList.Count; i++)
                {
                    if (currentAbnormalButtonEndPosList[i] <= changeData.startFrameCountLogic)
                    {
                        currentAbnormalButtonEndPosList[i] = changeData.endFrameCountLogic;
                        break;
                    }
                }
                if (i >= currentAbnormalButtonEndPosList.Count)
                {
                    currentAbnormalButtonEndPosList.Add(changeData.endFrameCountLogic);
                }
            }
            if (flag2 && !flag1)
            {
                toEndAbnormalData.Add(new ToEndAbnormalState(changeData, i));
            }
            //a.transform.localPosition = new Vector3(changeData.startFrameCountLogic * scaleValue, abnormalPrefabHight * i, 0);
            //MaxHight = Mathf.Max(MaxHight, abnormalPrefabHight * i);
            int length1 = (int)(Math.Max(minLength, changeData.endFrameCountLogic - changeData.startFrameCountLogic) * scaleValue);
            int length2 = (int)(Math.Max(minLength, changeData.endFrameCountReal - changeData.startFrameCountReal) * scaleValue);
            int length = timeType == TimeLabelType.REAL ? length2 : length1;
            //a.GetComponent<RectTransform>().sizeDelta = new Vector2(length, abnormalPrefabHight);
            //a.GetComponent<GuildCalcButton>().SetButton(GetButtonColor(changeData), changeData.GetDescription(), changeData.startFrameCountLogic, changeData.endFrameCountLogic, !flag2, changeData.ShowDetail);
            //prefabs_2.Add(a);
            ButtonEXImage buttonEX = new ButtonEXImage();
            float posx = (timeType == TimeLabelType.REAL ? changeData.startFrameCountReal:changeData.startFrameCountLogic )* scaleValue + startPos;
            int posy = BuffStartHight - abnormalPrefabHight * i + basePosY;
            buttonEX.Init(timeType, 1, GetButtonColor(changeData), (int)posx, posy, length, changeData.GetDescription(), true, changeData.startFrameCountLogic, changeData.startFrameCountReal, true, changeData.endFrameCountLogic, changeData.endFrameCountReal,clickEventList,action);
            buffButtons.Add(buttonEX);

        }
        private Color GetButtonColor(UnitAbnormalStateChangeData data)
        {
            if (data.isBuff)
            {
                return buffColors[data.BUFF_Type % buffColors.Length];
            }
            else
            {
                return abnormalStateColors[(int)data.CurrentAbnormalState % abnormalStateColors.Length];
            }
        }

    }
    public class ButtonEXImage
    {
        public int type;
        public int leftValueLogic;
        public int leftValueReal;
        public int rightValueLogic;
        public int rightValuereal;
        public static int[] sizey = new int[3] { 35, 20, 25 };
        const int LabelFix_x = 6;
        const int TextPosYFix = 3;
        int localy = 0;
        public List<ImageDrawData> ImageDraws;
        public ClickEvevtData clickEvevtData;

        public void Init(TimeLabelType type, int buttonType, Color color, int posx, int posy, int sizex, string buttonName, bool showLeftText = false, int leftvalue1 = 0, int leftvalue2 = 0, bool showRightText = true, int rightValue1 = 100, int rightValue2 = 100, List<ClickEvevtData> clickEventList=null,Action action=null)
        {
            ImageDraws = new List<ImageDrawData>();
            localy = sizey[buttonType];
            leftValueLogic = leftvalue1;
            leftValueReal = leftvalue2;
            rightValueLogic = rightValue1;
            rightValuereal = rightValue2;
            bool isReal = type == TimeLabelType.REAL;
            bool isIntTime = type == TimeLabelType.LOGICINT;
            ImageDrawData buttonImage = new ImageDrawData(1, posx, posy, color, sizex, sizey[buttonType]);
            ImageDrawData buttonText = new ImageDrawData(2, (int)(posx+sizex*0.3), posy+TextPosYFix, Color.Black, 0, 0,10,buttonName);
            ImageDraws.Add(buttonImage);
            ImageDraws.Add(buttonText);

            if (showLeftText)
            {
                string text = isIntTime ? ((int)((BattleStatePage.endframelogic - leftValueLogic) / 60)).ToString() : ((isReal ? leftValueReal : leftValueLogic).ToString());
                ImageDrawData left = new ImageDrawData(2, posx - LabelFix_x, localy + posy, Color.Black, 0, 0, 10, text);
                ImageDraws.Add(left);
            }
            if (showRightText)
            {
                string text2 = isIntTime ? ((int)((BattleStatePage.endframelogic - rightValueLogic) / 60)).ToString() : ((isReal ? rightValuereal : rightValueLogic).ToString());
                ImageDrawData right = new ImageDrawData(2, posx - LabelFix_x + sizex, localy + posy, Color.Black, 0, 0, 10, text2);
                ImageDraws.Add(right);
            }

            if (clickEventList != null && action != null)
            {
                Rectangle rectangle = new Rectangle(posx, posy, sizex, sizey[buttonType]);
                clickEvevtData = new ClickEvevtData(rectangle, action);
                clickEventList.Add(clickEvevtData);
            }
        }
        public void ResizeY(int diff)
        {
            foreach(var data in ImageDraws)
            {
                data.posY += diff;
            }
            clickEvevtData?.ResizeY(diff);
        }
    }
    public class ImageDrawData
    {
        public int ImageType;
        public int posX;
        public int posY;
        public int sizeX;
        public int sizeY;
        public Color color;
        public int fontSize;
        public string text;
        public ImageDrawData() { }
        public ImageDrawData(int imageType, int posX, int posY, Color color, int sizeX = 0, int sizeY = 0, int fontSize = 0, string text = null)
        {
            ImageType = imageType;
            this.posX = posX;
            this.posY = posY;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.color = color;
            this.fontSize = fontSize;
            this.text = text;
        }
    }
    public class ClickEvevtData
    {
        public Rectangle rectangle1;
        public Action action1;
        public ClickEvevtData() { }

        public ClickEvevtData(Rectangle rectangle1, Action action1)
        {
            this.rectangle1 = rectangle1;
            this.action1 = action1;
        }
        public void ResizeY(int diff)
        {
            rectangle1.Y += diff;
        }
    }

    public class ToEndAbnormalState
    {
        public UnitAbnormalStateChangeData changeData;
        public int position;

        public ToEndAbnormalState(UnitAbnormalStateChangeData changeData, int position)
        {
            this.changeData = changeData;
            this.position = position;
        }
    }

}
