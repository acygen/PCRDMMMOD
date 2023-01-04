using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft0.Json;
using System.IO;
using System.Data;
using Elements;
using Elements.Battle;
using Cute;
using HarmonyLib;
using System.Windows.Forms;

namespace PCRCalculator.Tool
{
    public class PCRSettings
    {
        private static PCRSettings instance;
        public static PCRSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    CreateInstance();
                }
                return instance;
            }
        }
        public static System.Action<string> Onlog;

        public LoadData loadData;
        public HomeData homeData;
        public ClanData clanData;
        public ClanBattleTopData topData;
        public ClanBattleBossInfo bossInfoData;
        public ClanBattleBossReload bossReloadData;
        public ClanBattleSupportListData SupportListData;
        public static bool useRealNet = false;

        public static bool ignoreManifestCheck => Instance.globalSetting.ignoreManifestCheck;
        public static bool replaceManifestURL => instance.globalSetting.replaceManifestURL;
        public static bool ignoreNewCharacterIntroduction => Instance.globalSetting.ignoreNewCharacterIntroduction;
        public static bool useUserAssestPath => Instance.globalSetting.useUserAssestPath;
        public static string UserAssestPath => Instance.globalSetting.userAssestPath;
        public bool forceLogBattleStart => globalSetting.forceLogBattleStart;
        public static bool fileNameEncrypted => Instance.globalSetting.fileNameEnrcypted;
        public static bool useDBinStreamingAssestPath => Instance.globalSetting.useDBinStreamingAssestPath;
        public static bool useMyPlayerPrefab => Instance.globalSetting.useMyPlayerPrefab;

        public static float battleSpeed => Instance.battleSetting.battleSpeed;
        public static bool showExectTime => Instance.battleSetting.showExectTime;
        public static bool showExectTimeReal => Instance.battleSetting.showRealFrame;
        public static bool showBossDEF => Instance.battleSetting.showBossDEF;
        public static bool mutiTargetShowMDEF => Instance.battleSetting.mutiTargetShowMDEF;
        public static int battleSeed 
        { 
            get 
            { 
                LastSeed = Instance.clanSetting.GetSeed();
                return LastSeed; 
            }
        }
        public static int LastSeed = 0;
        public static int battleTime => Instance.clanSetting.battleTime;
        public static bool useLog => Instance.globalSetting.useLog;
        public static Dictionary<int, string> unitNameDic = new Dictionary<int, string>();
        public static bool DVGGWVBMH = false;
        public bool FirstReset => firstReset;

        public static Elements.Battle.BattleManager staticBattleBanager;
        public static string DBPath 
        {
            get 
            { 
                return Instance.globalSetting.GetDBPath();
            }
            set
            {
                Instance.globalSetting.SetDBPath(value);
            }
        }
        public static int Version
        {
            get
            {
                return Instance.globalSetting.version;
            }
            set
            {
                Instance.globalSetting.SetVersion(value);
            }
        }
        public static string ManifestPath => UnityEngine.Application.streamingAssetsPath + "/Manifest";
        private static readonly string[] DataNames = new string[17]
{
            "id","角色名字","星级","好感度","等级",
            "UB等级","技能1","技能2","EX技能","RANK",
            "左上","右上","左中","右中","左下","右下","专武"
};


        public ClanBattleSetting clanSetting;
        public GlobalSetting globalSetting;
        public BattleSetting battleSetting;
        public MyPlayerPrefab myPlayerPrefab;
        public Action OtherToolButton;
        private bool firstReset = false;
        private List<string[]> usedPath = new List<string[]>();
        Dictionary<int, UserChara> lovedic = new Dictionary<int, UserChara>();

        public static void CreateInstance()
        {
            if (instance == null)
            {
                PCRSettings settings = new PCRSettings();
                instance = settings;
                settings.Load();
            }
        }
        public static void SavePlayerprefabs()
        {
            Instance.Save(6);
        }
        public void Load()
        {
            loadData = GetDataFromFile<LoadData>("loadIndex");
            topData = GetDataFromFile<ClanBattleTopData>("clan_battletop");
            bossInfoData = GetDataFromFile<ClanBattleBossInfo>("clan_battleboss_info");
            bossReloadData = GetDataFromFile<ClanBattleBossReload>("clan_battlereload_detail_info");
            homeData = GetDataFromFile<HomeData>("HomeIndex");
            clanData = new ClanData();
            SupportListData = new ClanBattleSupportListData();

            clanSetting = GetDataFromFile<ClanBattleSetting>("clanSetting", true);
            if (clanSetting == null)
            {
                clanSetting = new ClanBattleSetting();
                Save(3);
            }
            globalSetting = GetDataFromFile<GlobalSetting>("globalSetting", true);
            if (globalSetting == null)
            {
                globalSetting = new GlobalSetting();
                Save(4);
            }
            battleSetting = GetDataFromFile<BattleSetting>("battleSetting", true);
            if (battleSetting == null)
            {
                battleSetting = new BattleSetting();
                Save(5);
            }
            myPlayerPrefab = GetDataFromFile<MyPlayerPrefab>("playerPrefab", true);
            if (myPlayerPrefab == null)
            {
                myPlayerPrefab = new MyPlayerPrefab();
                Save(6);
            }
            unitNameDic = GetDataFromFile<Dictionary<int, string>>("unitNameDic");
            if (unitNameDic == null)
                unitNameDic = new Dictionary<int, string>();
            //SetTopData();
            string result = EXCELHelper.DGBVSABFBF(CalcCheck.CNEVGLDNMH(true));
            //SaveDataToFile("Version", EXCELHelper.DGBVSABFBF(result));
            //myPlayerPrefab.SetStr("VBFADKJVNFLEBAN", EXCELHelper.DGBVSABFBF(result));
           // if (myPlayerPrefab.GetInt("ASDFGHJK", 0) == 0)
            //    DVGGWVBMH = myPlayerPrefab.GetStr("VBFADKJVNFLEBAN", "ERROR") != EXCELHelper.DGBVSABFBF(result);
        }
        public void SetTopData()
        {
            clanSetting.Reset();
            topData.data.clan_battle_id = clanSetting.clanbattleid;
            topData.data.period = clanSetting.period;
            topData.data.lap_num = clanSetting.lap;
            topData.data.boss_info = clanSetting.boss_info;
        }
        public void ReflashClanSetting()
        {
            topData.data.clan_battle_id = clanSetting.clanbattleid;
            topData.data.period = clanSetting.period;
            topData.data.lap_num = clanSetting.lap;
            topData.data.boss_info = clanSetting.boss_info;

        }
        public void Save(int type)
        {
            switch (type)
            {
                case 1:
                    SaveDataToFile("loadIndex", loadData);
                    break;
                case 2:
                    SaveDataToFile("clan_battletop", bossInfoData);
                    break;
                case 3:
                    SaveDataToFile("clanSetting", clanSetting);
                    break;
                case 4:
                    SaveDataToFile("globalSetting", globalSetting);
                    break;
                case 5:
                    SaveDataToFile("battleSetting", battleSetting);
                    break;
                case 6:
                    SaveDataToFile("playerPrefab", myPlayerPrefab);
                    break;
                case 7:
                    SaveDataToFile("unitNameDic", unitNameDic);
                    break;
                case 10:
                    SaveDataToFile("usedPath", usedPath);
                    break;

            }
        }
        public void OnQuitGame()
        {
            try
            {
                Save(6);
                PCRTool.Instance.managerForm?.Close();
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("ERROR:" + ex.Message);
            }
        }
        public void UpdateClanBattle(ClanBattleFinishReceiveData data)
        {
            topData.data.boss_info[data.order_num-1].current_hp = data.boss_hp;
            /*if (data.boss_hp == 0)
            {

            }*/
        }
        public void SetTopDataAtFirst()
        {
            if (!firstReset)
            {
                SetTopData();
                firstReset = true;
                PCRTool.Instance.managerForm.Reflash();
            }

        }
        public void AddUsedABPath(string path, string path_encrypt)
        {
            usedPath.Add(new string[2] { path, path_encrypt });
        }
        public int GetBossHp(int order)
        {
            return clanSetting.boss_info[order - 1].current_hp;
        }
        public void SetClanBattleBossInfo(string json)
        {
            ClanBattleBossInfoReceiveData data = JsonConvert.DeserializeObject<ClanBattleBossInfoReceiveData>(json);
            bossInfoData.data.current_hp = topData.data.boss_info[data.order_num - 1].current_hp;
        }
        public bool SetBattleTimeScale(float scale)
        {
            if (staticBattleBanager != null)
            {
                FHDHEIDIPOI battleTimeScale = Traverse.Create(staticBattleBanager).Field("battleTimeScale").GetValue<FHDHEIDIPOI>();
                Traverse.Create(battleTimeScale).Field("speedUpRate").SetValue(scale);
                battleTimeScale.MCMKAPDKGKE = true;
                return true;
            }
            return false;
        }
        public void OnSetMyParty(string json)
        {
            MyPartyReceiveData data = JsonConvert.DeserializeObject<MyPartyReceiveData>(json);
            if (loadData.data.user_my_party == null)
            {
                loadData.data.user_my_party = new List<UserMyParty>();
            }
            var dd = loadData.data.user_my_party.Find(a => a.tab_number == data.tab_number && a.party_number == data.party_number);
            if (dd != null)
                loadData.data.user_my_party.Remove(dd);
            UserMyParty party = new UserMyParty(data.tab_number, data.party_number, data.party_label_type,
                data.party_name, data.unit_id_1, data.unit_id_2, data.unit_id_3, data.unit_id_4, data.unit_id_5);
            loadData.data.user_my_party.Add(party);
            Save(1);
        }
        public void OnSetMyPartyTab(string json)
        {

        }
        public void OnSetMyPage(string json)
        {
            MyPageReceive myPage = JsonConvert.DeserializeObject<MyPageReceive>(json);
            loadData.data.my_page = myPage.my_page_info;
            Save(1);
        }
        public List<UserMyParty> GetAreaOppoentList()
        {
            if (loadData.data.user_my_party == null)
            {
                loadData.data.user_my_party = new List<UserMyParty>();
            }
            List<UserMyParty> list = new List<UserMyParty>();
            foreach (var data in loadData.data.user_my_party)
            {
                if (!data.AllZero() && data.tab_number==1)
                {
                    list.Add(data);
                }
            }
            List<UserMyParty> result = new List<UserMyParty>();
            while (list.Count > 0 && result.Count < 3)
            {
                int idx = UnityEngine.Random.Range(0, list.Count);
                result.Add(list[idx]);
                list.RemoveAt(idx);
            }
            return result;
        }
        public void UpdateDeck(string json)
        {
            DeckReceiveData receiveData = JsonConvert.DeserializeObject<DeckReceiveData>(json);
            Deck deck1 = JsonConvert.DeserializeObject<Deck>(json);
            if (receiveData.deck_list.Count > 0)
            {
                foreach (var deck in receiveData.deck_list)
                {
                    var dd = loadData.data.deck_list.Find(a => a.deck_number == deck.deck_number);
                    if (dd == null)
                    {
                        dd = new Deck();
                        dd.deck_number = deck.deck_number;
                        loadData.data.deck_list.Add(dd);
                    }
                    dd.unit_id_1 = deck.TryGetId(0);
                    dd.unit_id_2 = deck.TryGetId(1);
                    dd.unit_id_3 = deck.TryGetId(2);
                    dd.unit_id_4 = deck.TryGetId(3);
                    dd.unit_id_5 = deck.TryGetId(4);
                }
            }
            if (deck1.deck_number > 0)
            {
                int idx = loadData.data.deck_list.FindIndex(a => a.deck_number == deck1.deck_number);
                if (idx > 0)
                {
                    loadData.data.deck_list[idx] = deck1;
                }
                else
                {
                    loadData.data.deck_list.Add(deck1);
                }
            }
            Save(1);
        }
        public T GetDataFromFile<T>(string file, bool ignoreMissing = false)
        {
            string path = UnityEngine.Application.streamingAssetsPath + "/" + file + ".json";
            T resultT = default;
            if (File.Exists(path))
            {
                string result = File.ReadAllText(path);
                resultT = JsonConvert.DeserializeObject<T>(result);
            }
            else
            {
                if (!ignoreMissing)
                    ClientLog.AccumulateClientLog("File missing! Cant find file:" + path + " !");
            }
            return resultT;
        }
        public void SaveDataToFile(string file, object data)
        {
            string path = UnityEngine.Application.streamingAssetsPath + "/" + file + ".json";
            string json = JsonConvert.SerializeObject(data);
            try
            {
                File.WriteAllText(path, json);
            }
            catch (System.Exception ex)
            {
                ClientLog.AccumulateClientLog("ERROR:" + ex.Message + "\n" + ex.StackTrace);
            }
        }
        public int GetTeamExp(int teamLevel)
        {
            try
            {
                if (CanSetCharData())
                {
                    return Elements.ManagerSingleton<Elements.MasterDataManager>.Instance.masterExperienceTeam[teamLevel].total_exp + 1;
                }
            }
            catch
            {
                return 0;
            }
            return 0;
        }
        public bool CanSetCharData()
        {
            return Elements.ManagerSingleton<Elements.MasterDataManager>.Instance != null;
        }
        public static int GetOrinUnitid(int unitid)
        {
            switch (unitid)
            {
                case 170101:
                    return 105701;
                default:
                    return unitid;
            }
        }
        /*public bool GetUnitNameBYDic(int unitid,out string value)
        {
            if (unitNameDic.TryGetValue(GetOrinUnitid(unitid), out value))
                return true;
            return false;
        }*/
        public string GetUnitNameByID(int unitid)
        {
            try
            {
                if (unitNameDic.TryGetValue(GetOrinUnitid(unitid), out string value))
                {
                    return value;
                }
                if (unitid <= 200000 || (unitid >= 400000 && unitid <= 499999))
                {
                    var data1 = ManagerSingleton<Elements.MasterDataManager>.Instance.masterUnitData;
                    if (data1.ContainsKey(unitid))
                        return data1.Get(unitid).UnitName;
                }
                else
                {
                    var data2 = ManagerSingleton<Elements.MasterDataManager>.Instance.masterEnemyParameter;
                    string name2 = data2.GetFromAllKind(unitid)?.name;
                    if (!string.IsNullOrEmpty(name2))
                        return name2;
                }
            }
            catch(Exception ex)
            {
                Debug.Log(ex);
            }
            return unitid + "(UNKNOWN)";
        }
        public AddedPlayerData CreateAddPlayerData(List<int> unitids)
        {
            AddedPlayerData playerData = new AddedPlayerData();
            foreach (int unitid in unitids)
            {
                UnitDataS unitDataS = loadData.data.unit_list.Find(a => a.id == unitid);
                if (unitDataS == null)
                {
                    int orid = Elements.UnitUtility.GetOriginalUnitId(unitid);
                    unitDataS = loadData.data.unit_list.Find(a => a.id == orid);
                }
                if (unitDataS != null)
                {
                    var love0 = loadData.data.user_chara_info.Find(a => a.chara_id == unitid / 100);
                    int love = 1;
                    if (love0 != null)
                        love = love0.love_level;
                    UnitData unitData = new UnitData(unitDataS, love);
                    playerData.playrCharacters.Add(unitData);
                }
            }
            return playerData;
        }
        public DataTable CreateDateTable()
        {
            DataTable dataTable = new DataTable();
            int debugpoint = 0;
            for (int i = 0; i < DataNames.Length; i++)
            {
                //new DataColumn(DataNames[i]);
                if (i == 1)
                {
                    dataTable.Columns.Add(DataNames[i], typeof(object));
                }
                else
                {
                    dataTable.Columns.Add(DataNames[i], typeof(int));
                }
            }
            debugpoint = 10;
            lovedic = new Dictionary<int, UserChara>();
            foreach (var lov in loadData.data.user_chara_info)
            {
                lovedic.Add(lov.chara_id, lov);
            }
            foreach (Elements.MasterUnitData.UnitData item in Elements.ManagerSingleton<Elements.MasterDataManager>.Instance.masterUnitData.dictionary.Values)
            {
                try
                {
                    int unitid = item.UnitId;
                    DataRow dataRow = dataTable.NewRow();
                    debugpoint = 20;
                    dataRow[DataNames[0]] = unitid;
                    if (unitNameDic.TryGetValue(unitid, out string cn_name))
                    {
                        dataRow[DataNames[1]] = cn_name;
                    }
                    else
                        dataRow[DataNames[1]] = (string)item.UnitName;
                    UnitDataS unitDataS = loadData.data.unit_list.Find(a => a.id == unitid);
                    if (unitDataS == null)
                        unitDataS = new UnitDataS(unitid, item.Rarity);
                    dataRow[DataNames[2]] = unitDataS.unit_rarity;
                    if (lovedic.TryGetValue(unitid / 100, out var value))
                    {
                        dataRow[DataNames[3]] = value.love_level;
                    }
                    else
                    {
                        dataRow[DataNames[3]] = 1;
                        var lv = new UserChara(unitid, 1);
                        lovedic.Add(unitid / 100, lv);
                        loadData.data.user_chara_info.Add(lv);
                    }
                    dataRow[DataNames[4]] = unitDataS.unit_level;
                    debugpoint = 30;
                    unitDataS.CheckAndCreat();
                    dataRow[DataNames[5]] = unitDataS.union_burst[0].skill_level;
                    dataRow[DataNames[6]] = unitDataS.main_skill[0].skill_level;
                    dataRow[DataNames[7]] = unitDataS.main_skill[1].skill_level;
                    dataRow[DataNames[8]] = unitDataS.ex_skill[0].skill_level;
                    debugpoint = 40;

                    dataRow[DataNames[9]] = unitDataS.promotion_level;
                    dataRow[DataNames[10]] = unitDataS.equip_slot[0].GetLv();
                    dataRow[DataNames[11]] = unitDataS.equip_slot[1].GetLv();
                    dataRow[DataNames[12]] = unitDataS.equip_slot[2].GetLv();
                    dataRow[DataNames[13]] = unitDataS.equip_slot[3].GetLv();
                    dataRow[DataNames[14]] = unitDataS.equip_slot[4].GetLv();
                    dataRow[DataNames[15]] = unitDataS.equip_slot[5].GetLv();
                    debugpoint = 50;

                    if (unitDataS.unique_equip_slot != null && unitDataS.unique_equip_slot.Count > 0)
                        dataRow[DataNames[16]] = unitDataS.unique_equip_slot[0].GetLv();
                    else
                        dataRow[DataNames[16]] = -1;
                    debugpoint = 60;
                    dataTable.Rows.Add(dataRow);
                }
                catch (System.Exception ex)
                {
                    ClientLog.AccumulateClientLog("CreateDateTable ERROR at " + debugpoint + "---" + ex.Message + "\n" + ex.StackTrace);
                }
            }
            return dataTable;
        }
        public void ChangeCharacterBox(int unitid, int rarity, int love, int lv, int ub, int main1, int main2, int ex, int rank,
            int l1 = -1, int l2 = -1, int l3 = -1, int l4 = -1, int l5 = -1, int l6 = -1, int eq = 0)
        {
            UnitDataS unitDataS = loadData.data.unit_list.Find(a => a.id == unitid);
            if (unitDataS == null)
            {
                unitDataS = new UnitDataS(unitid, 3);
                loadData.data.unit_list.Add(unitDataS);
            }
            unitDataS.ChangeAll(rarity, lv, ub, main1, main2, ex, rank, l1, l2, l3, l4, l5, l6, eq);
            var newlove = new UserChara(unitid / 100, love);
            if (lovedic.TryGetValue(unitid / 100, out var value))
            {
                value.Update(love);
            }
            else
            {
                var dd = loadData.data.user_chara_info.Find(a => a.chara_id == unitid / 100);
                if (dd == null)
                    loadData.data.user_chara_info.Add(newlove);
                else
                    dd.Update(love);
            }
            ChangeLoveStoryInfo(unitid, love);
            unitDataS.CreateExp();
        }
        public void ChangeLoveStoryInfo(int unitid, int love)
        {
            Dictionary<int, MasterCharaStoryStatus.CharaStoryStatus> dictionary2 = ManagerSingleton<MasterDataManager>.Instance.masterCharaStoryStatus.dictionary;
            int storyid = unitid * 10 - 10;
            List<int> addList = new List<int>();
            List<int> removeList = new List<int>();
            for (int i = 2; i <= 12; i++)
            {
                int ss = storyid + i;
                if (dictionary2.ContainsKey(ss))
                {
                    if (i <= love)
                        addList.Add(ss);
                    else
                        removeList.Add(ss);
                }
            }
            if (!dictionary2.ContainsKey(storyid + 1))
                addList.Add(storyid + 1);
            loadData.data.UpdataStoryId(addList, removeList);
        }
        public UnitDataS GetUnitDataS(int unitid)
        {
            return loadData.data.unit_list.Find(a => a.id == unitid);
        }
        public int GetUnitLove(int unitid)
        {
            if (lovedic.Count == 0)
            {
                foreach (var lov in loadData.data.user_chara_info)
                {
                    lovedic.Add(lov.chara_id, lov);
                }
            }
            if (lovedic.TryGetValue(unitid / 100, out var value))
            {
                return value.love_level;
            }
            return 0;
        }
        public void UpdateUnitName(int unitid,string name)
        {
            if (unitNameDic.ContainsKey(unitid))
                unitNameDic[unitid] = name;
            else
                unitNameDic.Add(unitid, name);
        }
        public void SetRankBounsRank(int rank)
        {
            string SQLWORD = CreateRankChangeSQL(globalSetting.rankbounsRank, 999);
            SQLWORD += "\n" + CreateRankChangeSQL(globalSetting.rankbounsRank - 1, rank - 1);
            SQLWORD += "\n" + CreateRankChangeSQL(999, rank);
            bool flag = PCRCalculator.Hook.db.ExecDB(SQLWORD,false);
            if (flag)
            {
                globalSetting.rankbounsRank = rank;
                Save(4);
                MessageBox.Show("成功，重启游戏后生效！");
            }
            else
            {
                MessageBox.Show("失败！");
            }

        }
        public static string CreateRankChangeSQL(int from,int to)
        {
            return "UPDATE promotion_bonus SET promotion_level = " + to + " WHERE promotion_level = " + from + ";";
        }
    }
    public class ClanBattleSetting
    {
        public int clanbattleid = 1016;
        public int period = 1;
        public int lap = 1;
        public int order = 1;
        public int overkillBackTime = 20;
        public int battleTime = 90;
        public int battleSeed = 666;
        public bool fixSeed = false;
        [JsonIgnore]
        public List<BossInfo> boss_info = new List<BossInfo>();
        public static readonly int[] defaultHP = new int[] { 6000000, 8000000, 10000000, 12000000, 20000000 };
        public void Reset()
        {
            boss_info.Clear();
            for (int i = 0; i < 5; i++)
            {
                BossInfo bossInfo = new BossInfo(i + 1, 401021601 + i, defaultHP[i], defaultHP[i],lap);
                try
                {
                    var masterDataManager = Elements.ManagerSingleton<Elements.MasterDataManager>.Instance;
                    int num = 0;
                    int bossid = clanbattleid * 10000 + 100 + i + 1;
                    num = masterDataManager.masterClanBattle2MapData.GetWaveGroupId(clanbattleid, bossid, lap);
                    int enemyid = Elements.ClanBattleUtil.GetWaveGroupDataOne(num).enemy_id_1;
                    Elements.MasterEnemyParameter.EnemyParameter fromAllKind = Elements.ManagerSingleton<Elements.MasterDataManager>.Instance.masterEnemyParameter.GetFromAllKind(enemyid);
                    bossInfo.max_hp = fromAllKind.hp;
                    if (i < order - 1)
                        bossInfo.current_hp = 0;
                    else
                        bossInfo.current_hp = fromAllKind.hp;
                    bossInfo.enemy_id = enemyid;
                }
                catch (System.Exception ex)
                {
                    ClientLog.AccumulateClientLog("ERROR:" + ex.Message + "\n" + ex.StackTrace);
                }
                boss_info.Add(bossInfo);
            }

        }
        public int GetSeed()
        {
            if (fixSeed)
                return battleSeed;
            return UnityEngine.Random.Range(0, int.MaxValue);
        }

    }
    public class GlobalSetting
    {
        //public string DMMPath = "";
        public bool ignoreManifestCheck = true;
        public bool ignoreNewCharacterIntroduction = true;
        public bool forceLogBattleStart = false;
        public bool useUserAssestPath = false;
        public string userAssestPath = "";
        public bool fileNameEnrcypted = true;
        public bool useDBinStreamingAssestPath = false;
        public bool useMyPlayerPrefab = true;
        public bool useLog = true;
        public bool useFixTime = false;
        public long fixTime;
        public string dbPath;
        public bool use1701 = false;
        public int rankbounsRank = 22;
        public bool replaceManifestURL = false;
        public int version = 10038100;//版本号
        public bool SetFixTime(bool enable, string timestr)
        {
            useFixTime = enable;
            try
            {
                fixTime = JMFOFKDCHEC.ToUnixTime(timestr);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
        public (bool,string) GetTimeStr()
        {
            return (useFixTime, JMFOFKDCHEC.FromUnixTime(fixTime).ToString());
        }
        public DateTime GetTime()
        {
            if (useFixTime)
                return JMFOFKDCHEC.FromUnixTime(fixTime);
            return DateTime.Now;
        }
        public string GetDBPath()
        {
            string fullPath = UnityEngine.Application.streamingAssetsPath + "/" + dbPath;
            if (string.IsNullOrEmpty(dbPath) || !File.Exists(fullPath))
            {
                dbPath = "redive_jp.db";
            }
            fullPath = UnityEngine.Application.streamingAssetsPath + "/" + dbPath;
            return fullPath;
        }
        public void SetDBPath(string path)
        {
            dbPath = path;
            PCRSettings.Instance.Save(4);
        }
        public void SetVersion(int version)
        {
            this.version = version;
            PCRSettings.Instance.Save(4);
        }
    }
    public class BattleSetting
    {
        public float battleSpeed = 1;
        public bool showExectTime = true;
        public bool showRealFrame = false;
        public bool showBossDEF = true;
        public bool useLogBarrier = true;
        public bool showUI = false;
        public bool mutiTargetShowMDEF = false;
    }
    public class MyPlayerPrefab
    {
        public Dictionary<string, string> dic_str = new Dictionary<string, string>();
        public Dictionary<string, int> dic_int = new Dictionary<string, int>();
        public Dictionary<string, float> dic_float = new Dictionary<string, float>();
        public void SetStr(string key, string value)
        {
            if (dic_str.ContainsKey(key))
                dic_str[key] = value;
            else
                dic_str.Add(key, value);
        }
        public string GetStr(string key, string defaultValue)
        {
            if (dic_str.ContainsKey(key))
                return dic_str[key];
            return defaultValue;
        }
        public bool HasKey(string key)
        {
            return dic_str.ContainsKey(key) || dic_int.ContainsKey(key);
        }
        public void DeleteKey(string key)
        {
            if (dic_str.ContainsKey(key))
                dic_str.Remove(key);
            if (dic_int.ContainsKey(key))
                dic_int.Remove(key);
            if (dic_float.ContainsKey(key))
                dic_float.Remove(key);
        }
        public void SetInt(string key, int value)
        {
            if (dic_int.ContainsKey(key))
                dic_int[key] = value;
            else
                dic_int.Add(key, value);
        }
        public int GetInt(string key, int defaultValue = 0)
        {
            if (dic_int.ContainsKey(key))
                return dic_int[key];
            return defaultValue;
        }
        public void SetFloat(string key, float value)
        {
            if (dic_float.ContainsKey(key))
                dic_float[key] = value;
            else
                dic_float.Add(key, value);
        }
        public float GetFloat(string key, float defaultValue = 0)
        {
            if (dic_float.ContainsKey(key))
                return dic_float[key];
            return defaultValue;
        }
    }
}