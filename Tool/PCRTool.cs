using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elements;
using Cute;
using Newtonsoft0.Json;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace PCRCalculator.Tool
{
    public class PCRTool
    {
        private static PCRTool instance;

        public static void CreateInstance()
        {
            if (instance == null)
            {
                instance = new PCRTool();
                instance.InitTool();
                //ShowPCRUI.LoadUI();
            }
        }
        public SetBox.ManagerForm managerForm;
        private UI.UnitEditForm unitEditForm;
        int selectTarget = 1;

        public static PCRTool Instance
        {
            get
            {
                if (instance == null)
                    CreateInstance();
                return instance;
            }
        }

        void InitTool()
        {
            try
            {
                managerForm = new SetBox.ManagerForm();
                managerForm.Show();
                managerForm.Reflash();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("启动插件时发生错误：" + ex.Message);
            }
            PCRSettings.Onlog += managerForm.OnLog;

        }
        public static string GetNetworkresult(string URL, string uploadJson)
        {
            CreateInstance();
            PCRBattle.CreateInstance();
            string result = "ERROR";
            if (URL.Contains("check/game_start"))
            {
                GameStartData gameStartData = new GameStartData(PCRSettings.Version);
                result = JsonConvert.SerializeObject(gameStartData);
            }
            else if (URL.Contains("load/index"))
            {
                LoadData loadData = PCRSettings.Instance.loadData;
                //if (!PCRSettings.Instance.globalSetting.use1701)
                //    PCRSettings.Instance.loadData.data.Remove1701();
                result = JsonConvert.SerializeObject(loadData);
            }
            else if (URL.Contains("home/index"))
            {
                PCRSettings.Instance.SetTopDataAtFirst();
                HomeData homeData = PCRSettings.Instance.homeData;
                result = JsonConvert.SerializeObject(homeData);

            }
            else if (URL.Contains("tool/signup"))
            {
                ToolSign data = new ToolSign();
                result = JsonConvert.SerializeObject(data);
            }
            else if (URL.Contains("payment/item_list"))
            {
                PaymentData data = new PaymentData();
                result = JsonConvert.SerializeObject(data);
            }
            else if (URL.Contains("clan/info"))
            {
                ClanData clanData = PCRSettings.Instance.clanData;
                result = JsonConvert.SerializeObject(clanData);
            }
            else if (URL.Contains("clan/chat_info_list"))
            {
                ClanChatListData chatListData = new ClanChatListData();
                result = JsonConvert.SerializeObject(chatListData);
            }
            else if (URL.Contains("clan_battle/top"))
            {
                //ClientLog.AccumulateClientLog("Upload json:\n" + uploadJson + "\n URL:" + URL);
                ClanBattleTopData data = PCRSettings.Instance.topData;
                result = JsonConvert.SerializeObject(data);
            }
            else if (URL.Contains("clan_battle/boss_info"))
            {
                //ClientLog.AccumulateClientLog("Upload json:\n" + uploadJson + "\n URL:" + URL);
                PCRSettings.Instance.SetClanBattleBossInfo(uploadJson);
                ClanBattleBossInfo bossInfo = PCRSettings.Instance.bossInfoData;
                result = JsonConvert.SerializeObject(bossInfo);
            }
            else if (URL.Contains("clan_battle/reload_detail_info"))
            {
                //ClientLog.AccumulateClientLog("Upload json:\n" + uploadJson + "\n URL:" + URL);
                ClanBattleBossReload relode = PCRSettings.Instance.bossReloadData;
                result = JsonConvert.SerializeObject(relode);
            }
            else if (URL.Contains("clan_battle/support_unit_list_2"))
            {
                //ClientLog.AccumulateClientLog("Upload json:\n" + uploadJson + "\n URL:" + URL);
                var support = PCRSettings.Instance.SupportListData;
                result = JsonConvert.SerializeObject(support);
            }
            else if (URL.Contains("deck/update"))
            {
                var deck = new DeckData();
                result = JsonConvert.SerializeObject(deck);
                PCRSettings.Instance.UpdateDeck(uploadJson);
            }
            else if (URL.Contains("clan_battle/start") || URL.Contains("clan_battle/rehearsal_start"))
            {
                bool isrehearsal = URL.Contains("rehearsal");
                result = CreateClanBattleStartStr(uploadJson, isrehearsal, out var des);
                PCRBattle.Instance.OnBattleStart(PCRSettings.LastSeed, PCRSettings.battleTime, false, des);
            }
            else if (URL.Contains("clan_battle/finish") || URL.Contains("clan_battle/rehearsal_finish"))
            {
                bool isrehearsal = URL.Contains("rehearsal");
                result = CreateClanBattleFinishStr(uploadJson, isrehearsal);
            }
            else if (URL.Contains("log/battle_log2"))
            {
                var logdata = new BattleLodData();
                result = JsonConvert.SerializeObject(logdata);
                PCRBattle.Instance.ReceiveBattleLog(uploadJson);
            }
            else if (URL.Contains("arena/info"))
            {
                result = CreateAreaInfoData(uploadJson);
            }
            else if (URL.Contains("arena/apply"))
            {
                Instance.selectTarget = GetSelectOppoent(uploadJson);
                result = JsonConvert.SerializeObject(new AreaApply());
            }
            else if (URL.Contains("arena/start"))
            {
                result = CreateAreaStartData(uploadJson);
                PCRBattle.Instance.OnBattleStart(PCRSettings.LastSeed, isJJC: true);
            }
            else if (URL.Contains("arena/cancel"))
            {
                result = CreateAreaCancelData(uploadJson);
            }
            else if (URL.Contains("arena/search"))
            {
                result = CreateAreaSearchData(uploadJson);
            }
            else if (URL.Contains("arena/finish"))
            {
                var fin = new AreaFinish();
                result = JsonConvert.SerializeObject(fin);
            }
            else if (URL.Contains("my_party/set_party"))
            {
                PCRSettings.Instance.OnSetMyParty(uploadJson);
                var party = new MyPartyData();
                result = JsonConvert.SerializeObject(party);
            }
            else if (URL.Contains("my_party/set_tab"))
            {

                //var party = new MyPartyData();
                //result = JsonConvert.SerializeObject(party);
            }
            else if (URL.Contains("music/top"))
            {
                var deck = new MusicTop();
                result = JsonConvert.SerializeObject(deck);
            }
            else if (URL.Contains("my_page/set_my_page"))
            {
                PCRSettings.Instance.OnSetMyPage(uploadJson);
                var deck = new MyPageSet();
                result = JsonConvert.SerializeObject(deck);

            }
            else if (URL.Contains("unit/automatic_enhance"))
            {
                int unitid = JsonConvert.DeserializeObject<AutomaticEnhancePostParam>(uploadJson).unit_id;
                //instance.OpenUnitEditForm(unitid);
                AutomaticEnhanceReceiveParam receiveParam = new AutomaticEnhanceReceiveParam();
                //MessageBox.Show("请先设置角色属性，设置完毕再关闭此提示！");
                receiveParam.data.unit_data = PCRSettings.Instance.GetUnitDataS(unitid);
                result = JsonConvert.SerializeObject(receiveParam);
            }

            if (result == "ERROR")
            {
                ClientLog.AccumulateClientLog("Upload json:\n" + uploadJson + "\n NOT SET YET!\n URL:" + URL);
            }
            else
            {
                if (PCRSettings.Instance.forceLogBattleStart)
                {
                    ClientLog.AccumulateClientLog(URL);
                    ClientLog.AccumulateClientLog(uploadJson);
                    ClientLog.AccumulateClientLog(result);
                }
            }
            return result;
        }
        public static string CreateClanBattleStartStr(string json, bool isre, out string des)
        {
            try
            {
                PCRSettings settings = PCRSettings.Instance;
                ClanBattleStartSendData data = JsonConvert.DeserializeObject<ClanBattleStartSendData>(json);
                des = data.GetBossDes();
                var masterDataManager = Elements.ManagerSingleton<Elements.MasterDataManager>.Instance;
                int num = 0;
                int bossid = data.clan_battle_id * 10000 + 100 + data.order_num;
                num = masterDataManager.masterClanBattle2MapData.GetWaveGroupId(data.clan_battle_id, bossid, data.lap_num);
                int enemyid = Elements.ClanBattleUtil.GetWaveGroupDataOne(num).enemy_id_1;
                MasterEnemyParameter.EnemyParameter fromAllKind = ManagerSingleton<MasterDataManager>.Instance.masterEnemyParameter.GetFromAllKind(enemyid);
                var skillData0 = ManagerSingleton<MasterDataManager>.Instance.masterUnitSkillData[fromAllKind.unit_id];
                var enemydata = new UnitDataC(fromAllKind);
                if (!settings.battleSetting.useLogBarrier)
                {
                    foreach (int skill in skillData0.MainSkillIds)
                    {
                        var actiondata = ManagerSingleton<MasterDataManager>.Instance.masterSkillData.Get(skill);
                        if (actiondata != null)
                        {
                            foreach (int actionid in actiondata.ActionIds)
                            {
                                var ac = ManagerSingleton<MasterDataManager>.Instance.masterSkillAction.Get(actionid);
                                if (ac != null && ac.action_type == 73)
                                {
                                    var sk = enemydata.main_skill.Find(a => a.skill_id == skill);
                                    if (sk != null)
                                        sk.skill_level = 0;
                                }
                            }
                        }
                    }
                }
                if (fromAllKind == null)
                {
                    ClientLog.AccumulateClientLog("ERROR! can't find enemydata " + enemyid);
                }
                var obj = new ClanBattleStartData(enemydata, PCRSettings.battleSeed, PCRSettings.battleTime, isre ? (int)fromAllKind.hp : settings.GetBossHp(data.order_num));
                string result = JsonConvert.SerializeObject(obj);
                return result;
            }
            catch (System.Exception ex)
            {
                ClientLog.AccumulateClientLog("ERROR:" + ex.Message + "\n" + ex.StackTrace);
                des = "??";
                return "ERROR";
            }
        }
        public static string CreateClanBattleFinishStr(string json, bool isrehearsal)
        {
            string result = "ERROR";
            try
            {
                ClanBattleFinishReceiveData data = JsonConvert.DeserializeObject<ClanBattleFinishReceiveData>(json);
                PCRBattle.Instance.UpdateClanBattleFinish(data);
                int dead = data.boss_hp > 0 ? 0 : 1;
                int carryTime = 0;
                if (dead == 1)
                {
                    carryTime = (90 - data.remain_time) + PCRSettings.Instance.clanSetting.overkillBackTime;
                    if (carryTime > 90)
                        carryTime = 90;
                }
                ClanBattleFinishData returndata = new ClanBattleFinishData(data.boss_hp, dead, carryTime);
                result = JsonConvert.SerializeObject(returndata);
                if (!isrehearsal)
                    PCRSettings.Instance.UpdateClanBattle(data);
            }
            catch (System.Exception ex)
            {
                ClientLog.AccumulateClientLog("ERROR:" + ex.Message + "\n" + ex.StackTrace);
                ClanBattleFinishData returndata = new ClanBattleFinishData(0, 1, 90);
                result = JsonConvert.SerializeObject(returndata);
            }
            return result;


        }
        public static string CreateAreaInfoData(string json)
        {
            AreaInfo area = new AreaInfo();
            var list = PCRSettings.Instance.GetAreaOppoentList();
            foreach (var dd in list)
            {
                var o1 = new OppoentData();
                o1.SetUnit(dd.party_number, dd.party_name, dd.GetUnits());
                area.data.search_opponent.Add(o1);
            }
            area.data.search_opponent.Sort((a, b) => a.rank - b.rank);
            return JsonConvert.SerializeObject(area);
            /*string path = UnityEngine.Application.streamingAssetsPath + "/" + "area_info" + ".json";

            string result = File.ReadAllText(path);

            return result;*/
        }
        public static string CreateAreaCancelData(string json)
        {
            AreaCancel cancel = new AreaCancel();
            var list = PCRSettings.Instance.GetAreaOppoentList();
            foreach (var dd in list)
            {
                var o1 = new OppoentData();
                o1.SetUnit(dd.party_number, dd.party_name, dd.GetUnits());
                cancel.data.search_opponent.Add(o1);
            }
            cancel.data.search_opponent.Sort((a, b) => a.rank - b.rank);
            return JsonConvert.SerializeObject(cancel);

        }
        public static string CreateAreaSearchData(string json)
        {
            AreaSearch search = new AreaSearch();
            var list = PCRSettings.Instance.GetAreaOppoentList();
            foreach (var dd in list)
            {
                var o1 = new OppoentData();
                o1.SetUnit(dd.party_number, dd.party_name, dd.GetUnits());
                search.data.search_opponent.Add(o1);
            }
            search.data.search_opponent.Sort((a, b) => a.rank - b.rank);
            return JsonConvert.SerializeObject(search);

        }
        public static string CreateAreaStartData(string json)
        {
            AreaStart areaStart = new AreaStart();
            List<int> user = new List<int>();// { 100601, 100701, 100801, 100901, 101001 };
            List<int> other = new List<int>();// { 100101, 100201, 100301, 100401, 100501 };
            areaStart.Set(user, other, PCRSettings.battleSeed);
            return JsonConvert.SerializeObject(areaStart);
        }
        public static int GetSelectOppoent(string json)
        {
            var aa = JsonConvert.DeserializeObject<AreaSelectData>(json);
            return aa.opponent_rank;
        }
        private static List<string> downloadingURLS = new List<string>();
        public static void DownloadFile(string url, string save)
        {
            try
            {
                if (downloadingURLS.Contains(url))
                    return;
                if (!File.Exists(save))
                {
                    downloadingURLS.Add(url);
                    //先下载到临时文件
                    var tmp = save + ".tmp";
                    using (var web = new WebClient())
                    {
                        web.DownloadFile(url, tmp);
                    }
                    File.Move(tmp, save);
                    ClientLog.AccumulateClientLog($"成功下载文件{save}");
                    downloadingURLS.Remove(url);
                }
            }
            catch(Exception ex)
            {
                ClientLog.AccumulateClientLog($"下载文件{save}时出错:{ex}");
                downloadingURLS.Remove(url);
            }
        }
        public void OpenUnitEditForm(int unitid,Action callBack)
        {
            ClientLog.AccumulateClientLog($"open{unitid}");
            var data = PCRSettings.Instance.GetUnitDataS(unitid);
            unitEditForm = new UI.UnitEditForm(data, PCRSettings.Instance.GetUnitLove(unitid), callBack);
            //unitEditForm.Show();
            //unitEditForm.Init(data, PCRSettings.Instance.GetUnitLove(unitid), () => { unitEditForm = null; });
            unitEditForm.Show();
        }
    }
}
