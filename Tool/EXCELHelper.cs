using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Security.Cryptography;
using Newtonsoft0.Json;
using ExcelDataReader;
using System.Runtime.InteropServices;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using UnityEngine;
using System.Text.RegularExpressions;


namespace PCRCalculator.Tool
{
    public static class EXCELHelper
    {
        public static GuildTimelineData TimelineData;
        public static List<System.Drawing.Color> stateColors;

        public static GuildTimelineData ReadSingleLineData(out List<string> errorMessage)
        {
            errorMessage = new List<string>();
            OpenFileName ofn = new OpenFileName();
            ofn.structSize = System.Runtime.InteropServices.Marshal.SizeOf(ofn);
            ofn.filter = "Excel Files(*.xlsx)\0*.xlsx\0";  //指定打开格式
            ofn.file = new string(new char[256]);
            ofn.maxFile = ofn.file.Length;
            ofn.fileTitle = new string(new char[64]);
            ofn.maxFileTitle = ofn.fileTitle.Length;
            ofn.initialDir = UnityEngine.Application.dataPath;//默认路径
            ofn.title = "打开Excel";
            ofn.defExt = "xlsx";
            ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR
            //打开windows框
            if (DllTest.GetOpenFileName(ofn))
            {
                string[] names = ofn.file.Split('\\');
                var result = ReadExcel_3(ofn.file, names[names.Length - 1], 1, errorMessage);
                return result;
            }
            else
                return null;
        }
        public static string OpenSQLFile()
        {
            OpenFileName ofn = new OpenFileName();
            ofn.structSize = System.Runtime.InteropServices.Marshal.SizeOf(ofn);
            ofn.filter = "SQL Files(*.sql)\0*.SQL\0*.txt\0";  //指定打开格式
            ofn.file = new string(new char[256]);
            ofn.maxFile = ofn.file.Length;
            ofn.fileTitle = new string(new char[64]);
            ofn.maxFileTitle = ofn.fileTitle.Length;
            ofn.initialDir = UnityEngine.Application.dataPath;//默认路径
            ofn.title = "打开SQL";
            ofn.defExt = "sql";
            ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR
            //打开windows框
            if (DllTest.GetOpenFileName(ofn))
            {
                return ofn.file;
            }
            else
                return null;
        }

        public static GuildTimelineData ReadfromExcel(out List<string> errorMessage)
        {
            errorMessage = new List<string>();
            string filepath = System_Windows_Froms.GetChoosedPath();
            string name = Path.GetFileNameWithoutExtension(filepath);
            var result = ReadExcel_3(filepath, name, 1,errorMessage);
            return result;
        }

        private static GuildTimelineData ReadExcel_3(string filePath, string name, int idx, List<string> Message)
        {
            FileStream stream = null;
            GuildTimelineData lineData = null;
            try
            {
                stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            }
            catch (IOException e)
            {
                Message.Add("读取错误," + e.Message + "\n请保证excel文件没有被其他程序占用！");

                return lineData;
            }
            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
            {
                var result = excelReader.AsDataSet();
                if (result != null)
                {
                    DataTable dataTable = result.Tables["savePage"];
                    if (dataTable != null)
                    {
                        int i = dataTable.Rows.Count;
                        int j = dataTable.Columns.Count;
                        string dataStr = dataTable.Rows[1][0].ToString();
                        if (dataStr != null && dataStr != "")
                        {
                            string jsonStr = "";
                            try
                            {
                                jsonStr = VEDGVVNOVN(dataStr);
                                var guildTimelineData = JsonConvert.DeserializeObject<GuildTimelineData>(jsonStr);

                                guildTimelineData.timeLineName = name;
                                return guildTimelineData;
                            }
                            catch (System.Exception e)
                            {
                                Message.Add("发生错误：" + e.Message);
                                File.WriteAllText("ERROR", jsonStr);
                            }
                        }
                        else
                        {
                            Message.Add("没有数据！");
                        }
                    }
                    else
                    {
                        Message.Add("未找到savePage页！");
                    }
                }
                else
                {
                    Message.Add("文件读取失败！");
                }
            }
            return lineData;
        }
        public static void OutputGuildTimeLine(GuildTimelineData timelineData, string defaultName = "")
        {
            TimelineData = timelineData;
            AddStateColors();
            SaveExcel(2, defaultName);
        }
        private static void AddStateColors()
        {
            stateColors = new List<System.Drawing.Color>();
            stateColors.Add(System.Drawing.Color.FromArgb(178, 178, 178));
            stateColors.Add(System.Drawing.Color.FromArgb(255, 134, 134));
            stateColors.Add(System.Drawing.Color.FromArgb(255, 173, 95));
            stateColors.Add(System.Drawing.Color.FromArgb(151, 168, 255));
            stateColors.Add(System.Drawing.Color.FromArgb(190, 190, 190));
            stateColors.Add(System.Drawing.Color.FromArgb(185, 120, 100));
            stateColors.Add(System.Drawing.Color.FromArgb(135, 135, 135));
            stateColors.Add(System.Drawing.Color.FromArgb(172, 255, 167));
            stateColors.Add(System.Drawing.Color.FromArgb(215, 213, 255));

        }

        public static void SaveExcel(int type, string defaultName = "")
        {
            bool isSuccess = true;
            string filePath = "";
                OpenFileName ofn = new OpenFileName();

                ofn.structSize = Marshal.SizeOf(ofn);

                //ofn.filter = "All Files\0*.*\0\0";
                //ofn.filter = "Image Files(*.jpg;*.png)\0*.jpg;*.png\0";
                //ofn.filter = "Txt Files(*.txt)\0*.txt\0";

                //ofn.filter = "Word Files(*.docx)\0*.docx\0";
                //ofn.filter = "Word Files(*.doc)\0*.doc\0";
                //ofn.filter = "Word Files(*.doc:*.docx)\0*.doc:*.docx\0";

                //ofn.filter = "Excel Files(*.xls)\0*.xls\0";
                ofn.filter = "Excel Files(*.xlsx)\0*.xlsx\0";  //指定打开格式
                                                               //ofn.filter = "Excel Files(*.xls:*.xlsx)\0*.xls:*.xlsx\0";
                                                               //ofn.filter = "Excel Files(*.xlsx:*.xls)\0*.xlsx:*.xls\0";

                ofn.file = new string(new char[256]);

                ofn.maxFile = ofn.file.Length;

                ofn.fileTitle = new string(new char[64]);

                ofn.maxFileTitle = ofn.fileTitle.Length;

                //ofn.fileTitle = "B1狼狗智克yls200w";

                ofn.initialDir = UnityEngine.Application.dataPath;//默认路径

                ofn.title = "选择保存路径";

                ofn.defExt = "xlsx";
                ofn.file = defaultName;
                //注意 一下项目不一定要全选 但是0x00000008项不要缺少
                ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR

                isSuccess = DllTest.GetSaveFileName(ofn);
                filePath = ofn.file.Replace("\\", "/");

            //打开windows框
            if (isSuccess)
            {
                //TODO

                //把文件路径格式替换一下
                //ofn.file = ofn.file.Replace("\\", "/");
                //Debug.Log(ofn.file);

                FileInfo newFile = new FileInfo(filePath);
                if (newFile.Exists)
                {
                    newFile.Delete();  // ensures we create a new workbook
                    newFile = new FileInfo(filePath);
                }
                switch (type)
                {
                    case 2:
                        CreateGuildTimeLineExcel(newFile);
                        break;

                }



            }

        }
        private static void CreateGuildTimeLineExcel(FileInfo newFile)
        {
            int debugPos = 0;
            try
            {

                using (ExcelPackage package = new ExcelPackage(newFile))
                {
                    AddedPlayerData playerDatas = TimelineData.playerGroupData.playerData;
                    ExcelWorksheet worksheet0 = package.Workbook.Worksheets.Add("轴模板");
                    int[] backColotInt_1 = new int[3] { 253, 233, 217 };
                    int[] backColotInt_2 = new int[3] { 250, 191, 143 };
                    int[] lineColor = new int[3] { 151, 71, 6 };
                    worksheet0.Cells[1, 2, 2, 9].Merge = true;
                    worksheet0.MySetValue(1, 2, TimelineData.timeLineName, 16, blod: true, backColor: backColotInt_1);
                    worksheet0.MySetValue(3, 2,
                        PCRBattle.Instance.enemyDes, 12, blod: true, fontColor: new int[3] { 226, 107, 10 }, backColor: backColotInt_1);
                    worksheet0.MySetValue(4, 2, TimelineData.exceptDamage + "w", blod: true, fontColor: new int[3] { 26, 36, 242 }, backColor: backColotInt_1);
                    worksheet0.MySetValue(5, 2, TimelineData.backDamage + "w", blod: true, backColor: backColotInt_1);
                    worksheet0.MySetValue(6, 2, "等级", blod: true, backColor: backColotInt_2);
                    worksheet0.MySetValue(7, 2, "RANK", blod: true, backColor: backColotInt_2);
                    worksheet0.MySetValue(8, 2, "星级", blod: true, backColor: backColotInt_2);
                    debugPos = 100;
                    int count = 0;
                    foreach (UnitData unitData in playerDatas.playrCharacters)
                    {
                        worksheet0.Cells[3, 7 - count, 5, 7 - count].Merge = true;
                        worksheet0.MySetValue(3, 7 - count, unitData.GetNicName(), backColor: backColotInt_1);
                        //worksheet0.InsertImage(TimelineData.charImages[count], 3, 7 - count, false, 1, 3);
                        worksheet0.MySetValue(6, 7 - count, unitData.GetLevelDescribe(), backColor: backColotInt_1);
                        worksheet0.MySetValue(7, 7 - count, unitData.GetRankDescribe(), backColor: backColotInt_1);
                        worksheet0.MySetValue(8, 7 - count, unitData.rarity, backColor: backColotInt_1);
                        count++;
                    }
                    debugPos = 200;
                    worksheet0.Cells[3, 8, 8, 9].Merge = true;
                    //worksheet0.InsertImage(TimelineData.charImages[5], 3, 8, false, 1.8f, 5);
                    worksheet0.MySetValue(9, 1, "帧数");
                    worksheet0.MySetValue(9, 2, "秒数", blod: true, backColor: backColotInt_2);
                    worksheet0.MySetValue(9, 3, "角色", blod: true, backColor: backColotInt_2);
                    worksheet0.MySetValue(9, 4, "操作", blod: true, backColor: backColotInt_2);
                    worksheet0.MySetValue(9, 5, "伤害", blod: true, backColor: backColotInt_2);
                    worksheet0.Cells[9, 6, 9, 9].Merge = true;
                    worksheet0.MySetValue(9, 6, "说明", blod: true, backColor: backColotInt_2);

                    int currentLineNum = 10;
                    debugPos = 300;
                    List<UBDetail> UBList = TimelineData.uBDetails;
                    UBList.Sort((a, b) => a.UBTime - b.UBTime);
                    foreach (var a in UBList)
                    {
                        worksheet0.MySetValue(currentLineNum, 1, a.UBTime);
                        if (a.isBossUB)
                        {
                            worksheet0.Cells[currentLineNum, 2, currentLineNum, 9].Merge = true;
                            worksheet0.MySetValue(currentLineNum, 2, "BOSS  UB", backColor: backColotInt_1);
                        }
                        else
                        {
                            worksheet0.Cells[currentLineNum, 6, currentLineNum, 9].Merge = true;
                            int second = (90 - a.UBTime / 60);
                            if (second >= 60)
                            {
                                second -= 60;
                                if (second >= 10)
                                    worksheet0.MySetValue(currentLineNum, 2, "1:" + second);
                                else
                                    worksheet0.MySetValue(currentLineNum, 2, "1:0" + second);
                            }
                            else
                                worksheet0.MySetValue(currentLineNum, 2, second);
                            worksheet0.MySetValue(currentLineNum, 3, PCRSettings.Instance.GetUnitNameByID(a.unitid));
                            worksheet0.MySetValue(currentLineNum, 5, a.Damage, fontColor: a.Critical ? new int[3] { 255, 0, 0 } : new int[3] { 0, 0, 0 });
                        }
                        currentLineNum++;
                    }
                    debugPos = 400;
                    worksheet0.Cells[currentLineNum, 2, currentLineNum, 9].Merge = true;
                    worksheet0.MySetValue(currentLineNum, 2, "TIME UP", backColor: backColotInt_1);
                    using (ExcelRange r = worksheet0.Cells[1, 2, currentLineNum, 9])
                    {
                        r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        System.Drawing.Color color0 = System.Drawing.Color.FromArgb(lineColor[0], lineColor[1], lineColor[2]);
                        r.Style.Border.Top.Color.SetColor(color0);
                        r.Style.Border.Bottom.Color.SetColor(color0);
                        r.Style.Border.Left.Color.SetColor(color0);
                        r.Style.Border.Right.Color.SetColor(color0);
                    }
                    //从这里向存档写入ub顺序数据
                    /*for (int ii = 0; ii < TimelineData.AllUnitUBList.Count; ii++)
                    {
                        var data00 = TimelineData.AllUnitUBList[ii];
                        worksheet0.Cells[10 + ii, 10].Value = data00[0];
                        worksheet0.Cells[10 + ii, 11].Value = data00[1];
                        worksheet0.Cells[10 + ii, 12].Value = data00[2];
                    
                    }*/


                    debugPos = 1000;
                    //package.Save();

                    // 添加一个sheet
                    ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("基础数据");
                    string[] HeadNames = new string[18]
                    {
                    "角色ID","角色名字","角色等级","角色星级","角色好感度","角色Rank",
                    "装备等级(左上)", "装备等级(右上)", "装备等级(左中)","装备等级(右中)","装备等级(左下)","装备等级(右下)",
                "UB技能等级","技能1等级","技能2等级","EX技能等级","专武等级","高级设置"};
                    worksheet1.Cells[1, 1, 1, HeadNames.Length].Merge = true;//合并单元格(1行1列到1行6列)
                    worksheet1.Cells["A1"].Style.Font.Size = 16; //字体大小
                    worksheet1.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; //对其方式
                    worksheet1.Cells["A1"].Value = "角色基础参数"; //显示
                    int lineNum = 2;
                    for (int i = 0; i < HeadNames.Length; i++)
                    {
                        worksheet1.Cells[lineNum, i + 1].Value = HeadNames[i];
                    }
                    lineNum++;
                    foreach (UnitData unitData in playerDatas.playrCharacters)
                    {
                        worksheet1.Cells[lineNum, 1].Value = unitData.unitId;
                        worksheet1.Cells[lineNum, 2].Value = unitData.GetUnitName();
                        worksheet1.Cells[lineNum, 3].Value = unitData.level;
                        worksheet1.Cells[lineNum, 4].Value = unitData.rarity;
                        worksheet1.Cells[lineNum, 5].Value = unitData.love;
                        worksheet1.Cells[lineNum, 6].Value = unitData.rank;
                        for (int i = 0; i < unitData.equipLevel.Length; i++)
                        {
                            if (unitData.equipLevel[i] == -1)
                                worksheet1.Cells[lineNum, 7 + i].Value = "未装备";
                            else
                                worksheet1.Cells[lineNum, 7 + i].Value = unitData.equipLevel[i];
                        }
                        for (int i = 0; i < unitData.skillLevel.Length; i++)
                        {
                            worksheet1.Cells[lineNum, 13 + i].Value = unitData.skillLevel[i];
                        }
                        worksheet1.Cells[lineNum, 17].Value = unitData.uniqueEqLv;
                        if (unitData.playLoveDic != null)
                        {
                            worksheet1.Cells[lineNum, 18].Value = JsonConvert.SerializeObject(unitData.playLoveDic);
                        }
                        lineNum++;
                    }
                    lineNum++;
                    worksheet1.Cells[lineNum, 1, lineNum, HeadNames.Length].Merge = true;
                    worksheet1.Cells[lineNum, 1].Style.Font.Size = 16;
                    worksheet1.Cells[lineNum, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet1.Cells[lineNum, 1].Value = "BOSS详情";
                    lineNum++;
                    //EnemyData enemyData = Elements.MyGameCtrl.Instance.tempData.guildEnemy;
                    worksheet1.Cells[lineNum, 1].Value = PCRBattle.Instance.enemyUnitid;
                    string enemyName = PCRSettings.Instance.GetUnitNameByID(PCRBattle.Instance.enemyUnitid);
                    worksheet1.Cells[lineNum, 2].Value = enemyName;
                    /*string str = "";
                    switch (TimelineData.playerGroupData.currentTurn)
                    {
                        case 1:
                            str = "一周目";
                            break;
                        case 2:
                            str = "二周目";
                            break;
                        case 3:
                            str = "三周目";
                            break;
                        case 4:
                            str = "四周目";
                            break;
                    }*/
                    worksheet1.Cells[lineNum, 3].Value = PCRBattle.Instance.enemyDes;
                    //worksheet1.Cells[lineNum, 4].Value = TimelineData.playerGroupData.isViolent ? "狂暴" : "普通";
                    worksheet1.Cells[lineNum, 4].Value = "普通";
                    worksheet1.Cells[lineNum, 6].Value = "随机数种子：";
                    worksheet1.Cells[lineNum, 7].Value = TimelineData.currentRandomSeed;

                    lineNum++;
                    lineNum++;
                    worksheet1.Cells[lineNum, 1, lineNum, 5].Merge = true;
                    worksheet1.Cells[lineNum, 1].Style.Font.Size = 16;
                    worksheet1.Cells[lineNum, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet1.Cells[lineNum, 1].Value = "角色UB时间";
                    lineNum++;
                    for (int i = 0; i < playerDatas.playrCharacters.Count; i++)
                    {
                        worksheet1.Cells[lineNum, 1 + i].Value = playerDatas.playrCharacters[i].GetUnitName();
                    }
                    lineNum++;
                    bool flag = true;
                    int count0 = 0;
                    List<List<float>> UBexecTimeList = TimelineData.UBExecTime;
                    while (flag)
                    {
                        flag = false;
                        for (int i = 0; i < playerDatas.playrCharacters.Count; i++)
                        {
                            if (UBexecTimeList[i] != null && count0 < UBexecTimeList[i].Count)
                            {
                                worksheet1.Cells[lineNum, 1 + i].Value = (90.0f - UBexecTimeList[i][count0] / 60.0f);
                                flag = true;
                            }
                        }
                        count0++;
                        lineNum++;
                    }
                    debugPos = 2000;
                    ExcelWorksheet worksheet2 = package.Workbook.Worksheets.Add("技能循环");
                    lineNum = 1;
                    worksheet2.Cells[lineNum, 1, lineNum, 20].Merge = true;
                    worksheet2.Cells[lineNum, 1].Style.Font.Size = 16;
                    worksheet2.Cells[lineNum, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet2.Cells[lineNum, 1].Value = "角色技能循环";
                    lineNum++;
                    foreach (var unit in TimelineData.allUnitStateChangeDic)
                    {
                        lineNum++;
                        worksheet2.Cells[lineNum - 1, 1, lineNum + 1, 2].Merge = true;
                        if (unit.Key >= 300000)
                            worksheet2.Cells[lineNum - 1, 1].Value = enemyName;
                        else
                        {
                            var a00 = playerDatas.playrCharacters.Find(a => a.unitId == unit.Key);
                            string str00 = a00 == null ? "???" : a00.GetUnitName();
                            worksheet2.Cells[lineNum - 1, 1].Value = str00;
                        }
                        worksheet2.Cells[lineNum - 1, 1].Style.Font.Size = 16;
                        worksheet2.Cells[lineNum - 1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet2.Cells[lineNum - 1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        int currentLoc = 3;
                        int lastFrameCount = 0;
                        foreach (var exec in unit.Value)
                        {
                            //int length = Mathf.RoundToInt((exec.currentFrameCount - lastFrameCount) / 60.0f);
                            int endLoc = Mathf.RoundToInt(exec.currentFrameCount / 30.0f) + 3;
                            int length = endLoc - currentLoc;
                            if (length < 1 && exec.changStateFrom == Elements.UnitCtrl.ActionState.SKILL_1)
                            {
                                worksheet2.Cells[lineNum - 1, currentLoc].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                worksheet2.Cells[lineNum - 1, currentLoc].Style.Fill.PatternType = ExcelFillStyle.DarkGray;
                                worksheet2.Cells[lineNum - 1, currentLoc].Style.Fill.BackgroundColor.SetColor(stateColors[(int)exec.changStateFrom]);
                                worksheet2.Cells[lineNum - 1, currentLoc].Value = exec.changStateFrom.GetDescription();
                                lastFrameCount = exec.currentFrameCount;
                            }
                            else
                            {
                                if (length < 1 && exec.currentFrameCount != lastFrameCount)
                                {
                                    length = 1;
                                }
                                if (length >= 1)
                                {
                                    worksheet2.Cells[lineNum, currentLoc, lineNum, currentLoc + length].Merge = true;
                                    worksheet2.Cells[lineNum, currentLoc].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    worksheet2.Cells[lineNum, currentLoc].Style.Fill.PatternType = ExcelFillStyle.DarkGray;
                                    worksheet2.Cells[lineNum, currentLoc].Style.Fill.BackgroundColor.SetColor(stateColors[(int)exec.changStateFrom]);
                                    worksheet2.Cells[lineNum, currentLoc].Value = exec.changStateFrom.GetDescription();
                                    worksheet2.Cells[lineNum + 1, currentLoc].Value = lastFrameCount;
                                    worksheet2.Cells[lineNum + 1, currentLoc].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    lastFrameCount = exec.currentFrameCount;
                                    currentLoc += length + 1;
                                }
                            }
                        }
                        lineNum++;
                        lineNum++;
                    }
                    lineNum++;
                    worksheet2.Cells[lineNum, 1, lineNum, 12].Merge = true;
                    worksheet2.Cells[lineNum, 1].Style.Font.Size = 16;
                    worksheet2.Cells[lineNum, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet2.Cells[lineNum, 1].Value = "角色技能循环详情";
                    lineNum++;
                    int charLong = 5;
                    int detailAlong = 2;
                    int detailBlong = 3;
                    worksheet2.Cells[lineNum, 2, lineNum, 2 + charLong - 1].Merge = true;
                    worksheet2.Cells[lineNum, 2].Value = enemyName;
                    worksheet2.Cells[lineNum, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    for (int i = 0; i < 5; i++)
                    {
                        if (i < playerDatas.playrCharacters.Count)
                        {
                            worksheet2.Cells[lineNum, 2 + charLong * (i + 1), lineNum, 2 + charLong * (i + 2) - 1].Merge = true;
                            worksheet2.Cells[lineNum, 2 + charLong * (i + 1)].Value = playerDatas.playrCharacters[i].GetUnitName();
                            worksheet2.Cells[lineNum, 2 + charLong * (i + 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }
                    }
                    lineNum++;
                    List<TimeLineDataA> timedata = CreateTimeLineDataA(1);
                    //Regex regex = new Regex(@"<[^>]*>",RegexOptions.IgnoreCase);
                    //string regex = 
                    foreach (var timeData in timedata)
                    {
                        int pos = 2 + charLong * timeData.idx;
                        worksheet2.Cells[lineNum, 1].Value = timeData.frame;
                        //worksheet2.Cells[lineNum, pos].Value = timeData.describeA;//.Replace(/<[^>] +>/ g, "");
                        AddStringWithJudge(worksheet2, lineNum, pos, timeData.describeA);
                        /*if (timeData.describeA != null)
                        {
                            worksheet2.Cells[lineNum, pos].Value = regex.Replace(timeData.describeA, "");
                            if (timeData.describeA.Contains("暴击"))
                            {
                                worksheet2.Cells[lineNum, pos].Style.Font.Color.SetColor(System.Drawing.Color.Yellow);
                            }

                        }*/
                        worksheet2.Cells[lineNum, pos, lineNum, pos + detailAlong - 1].Merge = true;
                        AddStringWithJudge(worksheet2, lineNum, pos + detailAlong, timeData.describeB);
                        //worksheet2.Cells[lineNum, pos + detailAlong].RichText.Add(timeData.describeB);
                        /*if (timeData.describeB != null)
                        {
                            worksheet2.Cells[lineNum, pos + detailAlong].Value = regex.Replace(timeData.describeB,"");
                            if (timeData.describeB.Contains("暴击"))
                            {
                                worksheet2.Cells[lineNum, pos + detailAlong].Style.Font.Color.SetColor(System.Drawing.Color.Yellow);
                            }
                        }*/
                        worksheet2.Cells[lineNum, pos + detailAlong, lineNum, pos + detailAlong + detailBlong - 1].Merge = true;

                        lineNum++;
                    }
                    debugPos = 3000;
                    //package.Save();

                    void MyAction0(string headName, string head2, int detailType, int charLong0 = 3, int detailAlong0 = 1, int detailBlong0 = 2)
                    {
                        ExcelWorksheet worksheet3 = package.Workbook.Worksheets.Add(headName);
                        lineNum = 1;
                        worksheet3.Cells[lineNum, 1, lineNum, 20].Merge = true;
                        worksheet3.Cells[lineNum, 1].Style.Font.Size = 16;
                        worksheet3.Cells[lineNum, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet3.Cells[lineNum, 1].Value = head2;
                        lineNum++;
                        //charLong = 3;
                        //detailAlong = 1;
                        //detailBlong = 2;
                        if (detailType < 4 || detailType == 6)
                        {
                            worksheet3.Cells[lineNum, 2, lineNum, 2 + charLong0 - 1].Merge = true;
                            worksheet3.Cells[lineNum, 2].Value = enemyName;
                            worksheet3.Cells[lineNum, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            for (int i = 0; i < 5; i++)
                            {
                                if (i < playerDatas.playrCharacters.Count)
                                {
                                    worksheet3.Cells[lineNum, 2 + charLong0 * (i + 1), lineNum, 2 + charLong0 * (i + 2) - 1].Merge = true;
                                    worksheet3.Cells[lineNum, 2 + charLong0 * (i + 1)].Value = playerDatas.playrCharacters[i].GetUnitName();
                                    worksheet3.Cells[lineNum, 2 + charLong0 * (i + 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                }
                            }
                        }
                        else
                        {
                            worksheet3.Cells[lineNum, 2, lineNum, 2 + charLong0 - 1].Merge = true;
                            worksheet3.Cells[lineNum, 2].Value = enemyName;
                            worksheet3.Cells[lineNum, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        }
                        lineNum++;
                        timedata = CreateTimeLineDataA(detailType);
                        int currentFream0 = 0;
                        TimeLineDataA lasttimeline = new TimeLineDataA();
                        foreach (var timeData in timedata)
                        {

                            int pos = 2 + charLong0 * (timeData.idx + timeData.mutiIndex);
                            //if (timeData.frame != currentFream0)
                            //{
                            if (timeData.mutiIndex > 0 && timeData.frame == lasttimeline.frame)
                            {

                            }
                            else
                            {
                                lineNum++;
                            }
                            currentFream0 = timeData.frame;
                            worksheet3.Cells[lineNum, 1].Value = timeData.frame;
                            //}
                            //worksheet2.Cells[lineNum, pos].Value = timeData.describeA;//.Replace(/<[^>] +>/ g, "");
                            worksheet3.Cells[lineNum, pos].Value = (detailType == 2 || detailType == 6) ? timeData.valueA : timeData.valueB;
                            if (detailAlong0 > 1)
                                worksheet3.Cells[lineNum, pos, lineNum, pos + detailAlong0 - 1].Merge = true;
                            //worksheet2.Cells[lineNum, pos + detailAlong].RichText.Add(timeData.describeB);
                            if (detailBlong0 > 0)
                            {
                                AddStringWithJudge(worksheet3, lineNum, pos + detailAlong0, timeData.describeB);
                                //if (timeData.describeB != null)
                                //    worksheet3.Cells[lineNum, pos + detailAlong0].Value = regex.Replace(timeData.describeB, "");
                                if (detailBlong0 > 1)
                                    worksheet3.Cells[lineNum, pos + detailAlong0, lineNum, pos + detailAlong0 + detailBlong0 - 1].Merge = true;
                            }
                        }
                        lineNum++;
                    }
                    MyAction0("HP变化", "角色HP", 2);
                    debugPos = 4000;
                    MyAction0("TP变化", "角色TP", 3);
                    debugPos = 5000;
                    MyAction0("Boss防御", "Boss防御变化", 4);
                    debugPos = 6000;
                    MyAction0("Boss魔防", "Boss魔防变化", 5);
                    debugPos = 7000;
                    //MyAction0("角色伤害统计", "角色伤害统计", 6, 1, 1, 0);
                    debugPos = 8000;
                    //package.Save();

                    ExcelWorksheet worksheet4 = package.Workbook.Worksheets.Add("savePage");
                    lineNum = 1;
                    worksheet4.Cells[lineNum, 1, lineNum, 20].Merge = true;
                    worksheet4.Cells[lineNum, 1].Style.Font.Size = 16;
                    worksheet4.Cells[lineNum, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet4.Cells[lineNum, 1].Value = "存档数据页，请勿修改此页面的任何数据，否则无法导入！";
                    lineNum++;
                    string TimeLineDataStr = JsonConvert.SerializeObject(TimelineData);
                    PCRSettings.Instance.SaveDataToFile("excelTemp", TimelineData);
                    string hideData = DGBVSABFBF(TimeLineDataStr);
                    worksheet4.Cells[lineNum, 1, lineNum, 20].Merge = true;
                    worksheet4.Cells[lineNum, 1].Style.Font.Size = 12;
                    worksheet4.Cells[lineNum, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet4.Cells[lineNum, 1].Value = hideData;
                    debugPos = 9000;
                    /*ExcelWorksheet worksheet5 = package.Workbook.Worksheets.Add("随机判定");
                    lineNum = 1;
                    worksheet5.Cells[lineNum, 1, lineNum, 20].Merge = true;
                    worksheet5.Cells[lineNum, 1].Style.Font.Size = 16;
                    worksheet5.Cells[lineNum, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet5.Cells[lineNum, 1].Value = "随机判定页面";
                    lineNum++;
                    worksheet5.Cells[lineNum, 1].Value = "id";
                    worksheet5.Cells[lineNum, 2].Value = "帧数";
                    worksheet5.Cells[lineNum, 3].Value = "源";
                    worksheet5.Cells[lineNum, 4].Value = "目标";
                    worksheet5.Cells[lineNum, 5].Value = "技能id";
                    worksheet5.Cells[lineNum, 6].Value = "类型";
                    worksheet5.Cells[lineNum, 7].Value = "结果";
                    worksheet5.Cells[lineNum, 8].Value = "目标值";
                    worksheet5.Cells[lineNum, 9].Value = "参数2";
                    worksheet5.Cells[lineNum, 10, lineNum, 15].Merge = true;
                    worksheet5.Cells[lineNum, 10].Value = "状态";
                    worksheet5.Cells[lineNum, 16].Value = "seed";

                    lineNum++;
                    for (int i = 0; i < TimelineData.AllRandomList.Count; i++)
                    {
                        var data = TimelineData.AllRandomList[i];
                        worksheet5.Cells[lineNum, 1].Value = data.id;
                        worksheet5.Cells[lineNum, 2].Value = data.frame;
                        worksheet5.Cells[lineNum, 3].Value = data.ownerID;
                        worksheet5.Cells[lineNum, 4].Value = data.targetID;
                        worksheet5.Cells[lineNum, 5].Value = data.actionID;
                        worksheet5.Cells[lineNum, 6].Value = data.type;
                        worksheet5.Cells[lineNum, 7].Value = data.randomResult / 10;
                        worksheet5.Cells[lineNum, 8].Value = data.targetResult * 100;
                        worksheet5.Cells[lineNum, 9].Value = data.criticalDamageRate;
                        worksheet5.Cells[lineNum, 10, lineNum, 15].Merge = true;
                        worksheet5.Cells[lineNum, 10].Value = data.GetDescribe();
                        if (data.JudgeColored())
                        {
                            worksheet5.Cells[lineNum, 1, lineNum, 20].Style.Fill.PatternType = ExcelFillStyle.DarkGray;
                            worksheet5.Cells[lineNum, 1, lineNum, 20].Style.Fill.BackgroundColor.SetColor(stateColors[2]);

                        }
                        lineNum++;
                    }*/
                    //package.Save();

                    ExcelWorksheet worksheet6 = package.Workbook.Worksheets.Add("伤害曲线");
                    lineNum = 1;
                    worksheet6.Cells[lineNum, 1, lineNum, 20].Merge = true;
                    worksheet6.Cells[lineNum, 1].Style.Font.Size = 16;
                    worksheet6.Cells[lineNum, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet6.Cells[lineNum, 1].Value = "累计伤害曲线";
                    lineNum++;
                    worksheet6.Cells[lineNum, 1].Value = "帧数";
                    worksheet6.Cells[lineNum, 2].Value = "时间";
                    worksheet6.Cells[lineNum, 3].Value = "总伤害";
                    worksheet6.Cells[lineNum, 4].Value = "总伤害占比";
                    worksheet6.Cells[lineNum, 5].Value = "总期望伤害";
                    worksheet6.Cells[lineNum, 6].Value = "总期望伤害占比";
                    worksheet6.Cells[lineNum, 7].Value = "时间占比";

                    var dataList = TimelineData.clanTotalDamageList;
                    long alltotalDamage = dataList[dataList.Count - 1].totalDamage;
                    long alltotalDamageEX = dataList[dataList.Count - 1].totalDamageExcept;
                    foreach (var data in dataList)
                    {
                        lineNum++;
                        worksheet6.Cells[lineNum, 1].Value = data.frame;
                        worksheet6.Cells[lineNum, 2].Value = 90 - Math.Floor((5400 - data.frame) / 60.0f);
                        worksheet6.Cells[lineNum, 3].Value = data.totalDamage;
                        worksheet6.Cells[lineNum, 4].Value = Math.Round((data.totalDamage / (float)alltotalDamage) * 100) + "%";
                        worksheet6.Cells[lineNum, 5].Value = data.totalDamageExcept;
                        worksheet6.Cells[lineNum, 6].Value = Math.Round((data.totalDamageExcept / (float)alltotalDamageEX) * 100) + "%";
                        worksheet6.Cells[lineNum, 7].Value = Math.Round(data.frame / 54.0) + "%";
                    }
                    debugPos = 10000;
                    package.Save();
                }
            }
            catch(System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("ERROR:[" + debugPos + "]" + ex.Message + ex.StackTrace);

            }
        }
        private static void AddStringWithJudge(ExcelWorksheet worksheet, int x, int y, string describe)
        {
            Regex regex = new Regex(@"<[^>]*>", RegexOptions.IgnoreCase);
            if (describe != null)
            {
                worksheet.Cells[x, y].Value = regex.Replace(describe, "");
                if (describe.Contains("暴击"))
                {
                    worksheet.Cells[x, y].Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(199, 203, 0));
                }
                if (describe.Contains("MISS"))
                {
                    worksheet.Cells[x, y].Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(255, 0, 0));
                }

            }

        }

        private static void MySetValue(this ExcelWorksheet worksheet, int posx, int posy, string value,
    int size = 11, bool centre = true, bool blod = false, int[] fontColor = null, int[] backColor = null)
        {
            worksheet.Cells[posx, posy].Style.Font.Size = size; //字体大小
            if (blod)
                worksheet.Cells[posx, posy].Style.Font.Bold = true; //字体大小
            if (fontColor != null && fontColor.Length >= 3)
                worksheet.Cells[posx, posy].Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(fontColor[0], fontColor[1], fontColor[2]));
            if (centre)
            {
                worksheet.Cells[posx, posy].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; //对其方式
                worksheet.Cells[posx, posy].Style.VerticalAlignment = ExcelVerticalAlignment.Center; //对其方式

            }
            worksheet.Cells[posx, posy].Value = value; //显示
            if (backColor != null && backColor.Length >= 3)
            {
                worksheet.Cells[posx, posy].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[posx, posy].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(backColor[0], backColor[1], backColor[2]));
            }
        }
        private static void MySetValue(this ExcelWorksheet worksheet, int posx, int posy, int value,
    int size = 11, bool centre = true, bool blod = false, int[] fontColor = null, int[] backColor = null)
        {
            worksheet.Cells[posx, posy].Style.Font.Size = size; //字体大小
            if (blod)
                worksheet.Cells[posx, posy].Style.Font.Bold = true; //字体大小
            if (fontColor != null && fontColor.Length >= 3)
                worksheet.Cells[posx, posy].Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(fontColor[0], fontColor[1], fontColor[2]));
            if (centre)
            {
                worksheet.Cells[posx, posy].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; //对其方式
                worksheet.Cells[posx, posy].Style.VerticalAlignment = ExcelVerticalAlignment.Center; //对其方式

            }
            worksheet.Cells[posx, posy].Value = value; //显示
            if (backColor != null && backColor.Length >= 3)
            {
                worksheet.Cells[posx, posy].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[posx, posy].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(backColor[0], backColor[1], backColor[2]));
            }
        }
        private static int ReRangeIndex(int idx,int total)
        {
            return (idx >=total-1) ? 0 : (idx + 1);
        }
        private static List<TimeLineDataA> CreateTimeLineDataA(int type)
        {
            List<TimeLineDataA> timeLines = new List<TimeLineDataA>();
            int idx = 0;
            switch (type)
            {
                case 1:
                    foreach (var data0 in TimelineData.allUnitStateChangeDic)
                    {
                        foreach (var data1 in data0.Value)
                        {
                            timeLines.Add(new TimeLineDataA(data1.currentFrameCount, data0.Key, ReRangeIndex(idx,TimelineData.allUnitStateChangeDic.Count), data1.GetMainDescribe(), data1.describe));
                        }
                        idx++;
                    }
                    idx = 0;
                    foreach (var data0 in TimelineData.allUnitSkillExecDic)
                    {
                        foreach (var data1 in data0.Value)
                        {
                            foreach (var data2 in data1.actionExecDatas)
                            {
                                timeLines.Add(new TimeLineDataA(data2.execTime, data0.Key, ReRangeIndex(idx, TimelineData.allUnitSkillExecDic.Count), data1.GetDescribeA(), data2.GetDescribe()));
                            }
                        }
                        idx++;
                    }
                    break;
                case 2:
                    foreach (var data0 in TimelineData.allUnitHPDic)
                    {
                        foreach (var data1 in data0.Value)
                        {
                            timeLines.Add(new TimeLineDataA(Mathf.RoundToInt(data1.xValue), data0.Key, ReRangeIndex(idx, TimelineData.allUnitHPDic.Count), "", data1.describe, valueA: (int)data1.yValue));
                        }
                        idx++;
                    }
                    break;
                case 3:
                    foreach (var data0 in TimelineData.allUnitTPDic)
                    {
                        foreach (var data1 in data0.Value)
                        {
                            timeLines.Add(new TimeLineDataA(Mathf.RoundToInt(data1.xValue), data0.Key, ReRangeIndex(idx, TimelineData.allUnitTPDic.Count), "", data1.describe, valueB: data1.yValue));
                        }
                        idx++;
                    }
                    break;
                case 4:
                    foreach (var data0 in TimelineData.bossDefChangeDic)
                    {
                        timeLines.Add(new TimeLineDataA(Mathf.RoundToInt(data0.xValue), 0, idx, "", data0.describe, valueB: data0.yValue, muIndex: data0.Index));
                    }
                    break;
                case 5:
                    foreach (var data0 in TimelineData.bossMgcDefChangeDic)
                    {
                        timeLines.Add(new TimeLineDataA(Mathf.RoundToInt(data0.xValue), 0, idx, "", data0.describe, valueB: data0.yValue, muIndex: data0.Index));
                    }
                    break;
                case 6:
                    idx = 1;
                    foreach (var data0 in TimelineData.playerUnitDamageDic)
                    {
                        if (data0.Key <= 200000)
                        {
                            foreach (var data1 in data0.Value)
                            {
                                if (data1.canAddToTotal)
                                    timeLines.Add(new TimeLineDataA(data1.frame, data0.Key, idx, "", "", valueA: (int)data1.damage));
                            }
                            idx++;
                        }
                    }
                    break;

            }
            timeLines.Sort((a, b) => a.frame - b.frame);
            return timeLines;
        }

        private static byte[] Keys = { 0x20, 0x20, 0x78, 0x25, 0xCE, 0x37, 0x66, 0xFF };
        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string VEDGVVNOVN(string decryptString, string decryptKey = "PCRGuild")
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = System.Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch (System.Exception)
            {
                return decryptString;
            }
        }
        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串 </returns>
        public static string DGBVSABFBF(string encryptString, string encryptKey = "PCRGuild")
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));//转换为字节
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();//实例化数据加密标准
                MemoryStream mStream = new MemoryStream();//实例化内存流
                //将数据流链接到加密转换的流
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return System.Convert.ToBase64String(mStream.ToArray());
            }
            catch (System.Exception e)
            {
                return encryptString;
            }
        }
        public struct TimeLineDataA
        {
            public int frame;
            public int unitid;
            public int idx;
            public int valueA;
            public float valueB;
            public string describeA;
            public string describeB;
            public int mutiIndex;

            public TimeLineDataA(int frame, int unitid, int idx, string describeA, string describeB, int valueA = 0, float valueB = 0, int muIndex = 0)
            {
                this.frame = frame;
                this.unitid = unitid;
                this.idx = idx;
                this.describeA = describeA;
                this.describeB = describeB;
                this.valueA = valueA;
                this.valueB = valueB;
                mutiIndex = muIndex;
            }
        }

    }
}
