using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Cute;
using Elements;
using PluginLoader;
using UnityEngine;
using System.Collections;
using LitJson;
using HarmonyLib;
using System.Text;
using System.Collections.Generic;
using Elements.Data;
using CodeStage.AntiCheat;
using CodeStage.AntiCheat.ObscuredTypes;

namespace PCRCalculator.Tool
{
	//[HarmonyPatch(typeof(NetworkManager))]
	[HarmonyPatch(typeof(Cute.NetworkManager),"Connect",new Type[] { typeof(bool)})]
	public class Net
    {
		public static bool Prefix(Cute.NetworkManager __instance, ref IEnumerator __result,bool FCPNJHAJLBH)
        {
            if (PCRSettings.useRealNet)
            {
				return true;
            }
			__result = NetClass2.Connect0(__instance, FCPNJHAJLBH);
			return false;
        }
	}
	public class NetClass2
    {
		public static IEnumerator Connect0(Cute.NetworkManager _this, bool FCPNJHAJLBH)
		{
			AccessTools.FieldRef<Cute.NetworkManager, bool> isConnectRef =
		AccessTools.FieldRefAccess<Cute.NetworkManager, bool>("isConnect");
			//AccessTools.FieldRef<Cute.NetworkManager, LDDELEOONIB> lastRequestTaskRef =
//AccessTools.FieldRefAccess<Cute.NetworkManager, LDDELEOONIB>("lastRequestTask");
			LDDELEOONIB lastRequestTask = Traverse.Create(_this).Field("lastRequestTask").GetValue<LDDELEOONIB>();
			//LDDELEOONIB lastRequestTask = lastRequestTaskRef(_this);
			while (isConnectRef(_this))
			{
				yield return 0;
			}
			isConnectRef(_this) = true;
			string resultText = FakeNetwork(lastRequestTask.IKALIPAFPEC, lastRequestTask.CreateBodyJson);
			if (resultText.Contains("ERROR"))
			{
				if (!lastRequestTask.isSkipCommonTimeOutPopUp())
				{
					NetworkUI.GetInstance().OpenTimeOutErrorPopUp(lastRequestTask.AOJABCFFPLM);
				}
				if (lastRequestTask.GHNFMJOMHNN != null)
				{
					lastRequestTask.GHNFMJOMHNN(LDDELEOONIB.GGEABGPENAL.TimeOut);
				}
				ClientLog.AccumulateClientLog("UNSET URL TYPE ERROR: url-" + lastRequestTask.IKALIPAFPEC + " Not set yet!");
			}
			else
			{
				string text = resultText;
				try
				{
					lastRequestTask.SetResponseData(JsonMapper.ToObject(text));
					lastRequestTask.CheckResultCode();
				}
				catch (Exception ex)
				{
					ClientLog.AccumulateClientLog("Error when trans received text to json:" + ex.Message);
					ClientLog.AccumulateClientLog("received text:" + text);

				}
			}
			_this.ClearLastRequestTask();
			isConnectRef(_this) = false;
		}
		public static string FakeNetwork(string URL, string uploadJson)
		{
			return PCRTool.GetNetworkresult(URL, uploadJson);
		}

	}
	[HarmonyPatch(typeof(AssetManager), "BuildAssetDownloadURL",new Type[] { typeof(LFEKLJKFNPE) })]
    public class URLHook
    {		
		static void Postfix(ref string __result, LFEKLJKFNPE JJOBDIMBAIC)
        {
			ClientLog.AccumulateClientLog("原始下载地址->" + __result);
			if (!PCRSettings.replaceManifestURL)
				return;
			string[] urls = __result.Split('/');
			bool flag = false;
            if (urls.Length > 0 & urls[urls.Length - 1].Contains("manifest"))
            {
				string path = PCRSettings.ManifestPath + "/" + urls[urls.Length - 1];
				if (File.Exists(path))
                {
					__result = path;
					flag = true;
                }
                else
                {
					PCRTool.DownloadFile(__result, path);
                }
            }
			ClientLog.AccumulateClientLog(flag?("重定向->" + __result):"重定向失败！");
			
		}
	}
	[HarmonyPatch(typeof(BaseTask), "errorDialogByParseFailed")]
	public class TaskErrorHook
    {
		static bool Prefix(BaseTask __instance)
        {
			string errorName = $"[ERROR]:Error from {__instance.ToString()}";
			ManagerSingleton<DialogManager>.Instance.OpenFrontSubmit(eTextId.SERVER_ERROR_0002, errorName, DialogSubmit.eButtonLabel.OK, delegate ()
			{
				;
			}, true, true, false);
			ClientLog.AccumulateClientLog(errorName);
			return false;
		}

	}
	[HarmonyPatch(typeof(HomeTask), "ParseHomeIndexPartial")]
	public class HomeTaskErrorHook
	{
		static bool Prefix(HomeTask __instance,  int _resultCode, JsonData _header, JsonData _data)
		{
			try
			{
				ClientLog.AccumulateClientLog("HOME-0");
				HomeIndexReceiveParam homeIndexReceiveParam = new HomeIndexReceiveParam(_data);
				Hook.Hook.CallVoidMethod(typeof(HomeTask), __instance, "parseBaseReceiveParam", new object[] { homeIndexReceiveParam });
				ClientLog.AccumulateClientLog("HOME-1000");
				Hook.Hook.CallVoidMethod(typeof(HomeTask), __instance, "ParseHomeIndexImpl", new object[] { _resultCode, _header, homeIndexReceiveParam });
				//ParseHomeIndexImpl(__instance, _resultCode, _header, homeIndexReceiveParam);
				ClientLog.AccumulateClientLog("HOME-2000");
				var userData = Singleton<UserData>.Instance;
				var PresentCount = userData.PresentInfo.PresentCount;
				ClientLog.AccumulateClientLog($"HOME-2010-{PresentCount}");

				var MissionCount = userData.UsualMissionData.EnableReceiveMissionCount;
				ClientLog.AccumulateClientLog($"HOME-2020-{MissionCount}");
				var clanBattleRemainingCount = homeIndexReceiveParam.ClanBattleRemainingCount;
				ClientLog.AccumulateClientLog($"HOME-2040-{clanBattleRemainingCount}");

				ClientLog.AccumulateClientLog($"HOME-2050-{homeIndexReceiveParam==null}");
				
				ManagerSingleton<ApiManager>.Instance.PartialCallbackHomeIndexReceiveParam.Call(homeIndexReceiveParam);
				ClientLog.AccumulateClientLog("HOME-3000");
			}
            catch(Exception ex)
            {
				ClientLog.AccumulateClientLog($"[ERROR]:{ex.Message}\n{ex.StackTrace}");

			}
			return false;

		}
		private static void ParseHomeIndexImpl(HomeTask __instance, int _resultCode, JsonData _header, HomeIndexReceiveParam _receiveParam)
		{
			var userData = Singleton<UserData>.Instance;
			TaskImplUtility.CommonHomeIndexTask(new TaskImplUtility.ParamCommonHomeIndex(ref _receiveParam));
			if (_receiveParam.UserClan != null)
			{
				ParamQuestInfo param = new ParamQuestInfo(ref _receiveParam);
				userData.InitQuestInfo(param);
				if (_receiveParam.QuestList.Count > 0)
				{
					//this.parseDungeonInfo(_receiveParam.DungeonInfo);
				}
			}
			ClientLog.AccumulateClientLog("HOME-1100");

			EHPLBCOOOPK instance = Singleton<EHPLBCOOOPK>.Instance;
			instance.NotReceivedEquipDonation = (_receiveParam.NewEquipDonation != null);
			if (instance.NotReceivedEquipDonation)
			{
				instance.SelfEquipRequests = _receiveParam.NewEquipDonation;
			}
			/*if (_receiveParam.Missions != null && _receiveParam.Missions.Count > 0)
			{
				MasterCampaignMissionData masterCampaignMissionData = ManagerSingleton<MasterDataManager>.Instance.masterCampaignMissionData;
				List<UserMissionInfo> list = _receiveParam.Missions.FindAll((UserMissionInfo data) => !masterCampaignMissionData.HasKey(data.MissionId));
				long decrypted = _receiveParam.DailyResetTime.GetDecrypted();
				List<UserSeasonPackInfo> seasonPack = _receiveParam.SeasonPack;
				TaskImplUtility.CommonMissionIndexTask(new TaskImplUtility.ParamCommonMissionIndex(MissionData.eMissionGroup.USUAL, ref list, ref decrypted, ref seasonPack));
				this.createFriendCampaignMissionDataList(_receiveParam.Missions.FindAll((UserMissionInfo data) => masterCampaignMissionData.HasKey(data.MissionId)), _receiveParam.CampaignTargetFlag);
			}
			else
			{
				this.createFriendCampaignMissionDataList(new List<UserMissionInfo>(), _receiveParam.CampaignTargetFlag);
			}*/
			//this.parseLastFriendTime(_receiveParam.LastFriendTime);
			SaveDataManager instance2 = ManagerSingleton<SaveDataManager>.Instance;
			if (_receiveParam.AlchemyRewardTime != 0L)
			{
				if (instance2.GoldShopLastBuyUnixTime < _receiveParam.AlchemyRewardTime && _receiveParam.AlchemyRewardList != null && _receiveParam.Gold != null && (_receiveParam.PaidJewel > 0 || _receiveParam.FreeJewel > 0))
				{
					int num = Mathf.Min(_receiveParam.Gold.Count, _receiveParam.AlchemyRewardList.Count);
					DialogGoldShop.JewelType jewelType = (_receiveParam.FreeJewel > 0) ? DialogGoldShop.JewelType.Free : DialogGoldShop.JewelType.Paid;
					int buyCount = (userData.DailyGoldShopBuyCount - num + 1 > 0) ? (userData.DailyGoldShopBuyCount - num + 1) : 0;
					MasterGoldsetData2 masterGoldsetData = ManagerSingleton<MasterDataManager>.Instance.masterGoldsetData2;
					MasterGoldsetData2.GoldsetData2 goldSetData = ShopUtility.GetGoldSetData(buyCount);
					int num2 = goldSetData.BuyCount;
					List<int> list2 = new List<int>();
					List<int> list3 = new List<int>();
					List<DialogGoldShop.AlchemyRewardData> list4 = new List<DialogGoldShop.AlchemyRewardData>();
					for (int i = 0; i < num; i++)
					{
						int num3 = num2 + i;
						MasterGoldsetData2.GoldsetData2 goldsetData = masterGoldsetData.Get(num3);
						if (goldsetData == null)
						{
							break;
						}
						int num4 = _receiveParam.Gold[i];
						int useJewel = goldsetData.UserJewelCount;
						int num5 = ShopUtility.CalcLowerGetGold(goldSetData, 1);
						int num6 = num4 - num5;
						if (jewelType == DialogGoldShop.JewelType.Paid)
						{
							list2.Add(num5);
							list2.Add(num6);
						}
						else
						{
							list2.Add(0);
							list3.Add(num4);
						}
						DialogGoldShop.AlchemyRewardData alchemyRewardData = new DialogGoldShop.AlchemyRewardData();
						alchemyRewardData.UseJewelType = jewelType;
						alchemyRewardData.UseJewel = useJewel;
						alchemyRewardData.LowerGold = num5;
						alchemyRewardData.BonusFreeGold = num6;
						foreach (InventoryInfo inventoryInfo in _receiveParam.AlchemyRewardList[i].RewardInfoList)
						{
							alchemyRewardData.InventoryDatas.Add(new DialogGoldShop.AlchemyInventoryData(inventoryInfo));
						}
						int num7 = ShopUtility.GetGoldSetData(num3).TrainingQuestCount;
						alchemyRewardData.ObtainedTrainingCount = ((num3 == 1) ? num7 : (num7 - ShopUtility.GetGoldSetData(num3 - 1).TrainingQuestCount));
						list4.Add(alchemyRewardData);
					}
					string goldShopBuyHistory = instance2.GoldShopBuyHistory;
					DialogGoldShop.AlchemyRewardSaveData alchemyRewardSaveData;
					if (!goldShopBuyHistory.IsNullOrEmpty())
					{
						alchemyRewardSaveData = JsonUtility.FromJson<DialogGoldShop.AlchemyRewardSaveData>(goldShopBuyHistory);
					}
					else
					{
						alchemyRewardSaveData = new DialogGoldShop.AlchemyRewardSaveData();
					}
					alchemyRewardSaveData.RewardDatas.AddRange(list4);
					string goldShopBuyHistory2 = JsonUtility.ToJson(alchemyRewardSaveData);
					instance2.GoldShopBuyHistory = goldShopBuyHistory2;
					instance2.GoldShopResetTime = userData.DailyResetTime;
					instance2.GoldShopLastBuyUnixTime = _receiveParam.AlchemyRewardTime;
					instance2.GoldShopGetBuyHistoryListFlag = 0;
				}
			}
			else if (instance2.GoldShopGetBuyHistoryListFlag == 1)
			{
				instance2.GoldShopGetBuyHistoryListFlag = 0;
			}
			ClientLog.AccumulateClientLog("HOME-1200");

			instance2.Save();
			userData.DailyJewelAlert = eSeasonPackAlertType.ExpireAlertNone;
			userData.DailyJewelPackExpiredUnixTime = 0L;
			if (_receiveParam.SeasonPackAlert > 0)
			{
				userData.DailyJewelAlert = (eSeasonPackAlertType)(int)_receiveParam.SeasonPackAlert;
				userData.DailyJewelPackExpiredUnixTime = _receiveParam.SeasonPackEndTime;
			}
			long num8 = _receiveParam.DailyJewelPackEnd;
			if (num8 != 0L)
			{
				userData.DailyJewelPackEndUnixTime = num8;
			}
			userData.IsBoughtDailyResetJewel = _receiveParam.EverydayJewelPackBuy;
			if (_receiveParam.CharaETicketPurchasedTimes != null)
			{
				userData.UpdateCharaExchangeTicketProductData(_receiveParam.CharaETicketPurchasedTimes);
			}
			ClientLog.AccumulateClientLog("HOME-1400");

			if (_receiveParam.PurchasedArcadeIdList != null)
			{
				List<int> list5 = _receiveParam.PurchasedArcadeIdList.ConvertAll<int>((ObscuredInt x) => x);
				if (list5.Count > userData.ArcadeIdList.Count)
				{
					userData.SetArcadeIdList(list5);
				}
			}
			/*if (userData.TrainingQuestMaxCountExp == 0 || _receiveParam.TrainingQuestMaxCount != null)
			{
				this.parseTrainingInfo(_receiveParam.TrainingQuestMaxCount, _receiveParam.TrainingQuestPackEndTime);
			}*/
			if (_receiveParam.ShioriQuestInfo != null)
			{
				if (_receiveParam.ShioriQuestInfo.DeadBossList != null)
				{
					_receiveParam.ShioriQuestInfo.DeadBossList.ForEach(delegate (ObscuredInt bossId)
					{
						userData.SetShioriBossDead(bossId);
					});
				}
				if (_receiveParam.ShioriQuestInfo.QuestList != null)
				{
					_receiveParam.ShioriQuestInfo.QuestList.ForEach(delegate (UserQuestInfo questInfo)
					{
						userData.SetEventQuestInfo(questInfo);
					});
				}
				userData.UpdateShioriQuestInfo();
			}
			TutorialUtility.ReplaceOldReserveContentReleaseId();
			if (ShopUtility.IsLimitedShopOpen() && instance2.TutorialLimitedShopOpen)
			{
				instance2.TutorialLimitedShopOpen = false;
				instance2.Save();
			}
			ClientLog.AccumulateClientLog("HOME-1600");

			if (_receiveParam.SrtStoryIdList != null)
			{
				int jeepmmadcai = 1004;
				List<int> nohafdpnili = _receiveParam.SrtStoryIdList.ConvertAll<int>((ObscuredInt x) => x);
				MCGGMDDPKGA mcggmddpkga = Singleton<MCGGMDDPKGA>.Instance;
				if (mcggmddpkga == null)
				{
					mcggmddpkga = Singleton<MCGGMDDPKGA>.CreateInstance();
				}
				mcggmddpkga.AddArcadeStoryIdDic(jeepmmadcai, nohafdpnili);
			}
		}

	}
	//[HarmonyPatch(typeof(Coneshell.NLAMANJEALE), "Unpack")]
	public class UnpackPatch
    {
		static void Postfix(byte[] __result, byte[] LNIJKNGEPAJ)
        {
			ClientLog.AccumulateClientLog(Encoding.UTF8.GetString(__result));
		}
    }
	[HarmonyPatch(typeof(ViewHome), "setHomeContentsButtonState")]
	public class HomeUtility0
	{
		static void Postfix(ViewHome __instance)
		{
			ClientLog.AccumulateClientLog("HOME-2900");
			var banner = Traverse.Create(__instance).Field("partsBanner").GetValue<PartsBanner>();
			ClientLog.AccumulateClientLog($"HOME-2999{banner==null}");
		}
	}
}
