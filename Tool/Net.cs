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
		/*static bool Prefix(ref string __result, LFEKLJKFNPE JJOBDIMBAIC)
        {
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			switch (JJOBDIMBAIC.LILCMDCAFLM)
			{
				case LFEKLJKFNPE.OHMMHCICKCF.Sound:
					stringBuilder.Append(AIILLDHPIGL.GetSoundResourceURL());
					flag = true;
					break;
				case LFEKLJKFNPE.OHMMHCICKCF.Movie:
					stringBuilder.Append(AIILLDHPIGL.GetMoiveResourceURL());
					flag = true;
					break;
				case LFEKLJKFNPE.OHMMHCICKCF.AppResBundle:
					stringBuilder.Append(AIILLDHPIGL.GetAppResURL());
					flag = true;
					break;
				case LFEKLJKFNPE.OHMMHCICKCF.AssetBundle:
					stringBuilder.Append(AIILLDHPIGL.GetAssetBundleURL());
					flag = true;
					break;
				default:
					stringBuilder.Append(
						Hook.Hook.CallstaticMethod<string>(typeof(AssetManager), "getManifestURL", new object[] { JJOBDIMBAIC }));
					break;
			}
			if (flag)
			{
				stringBuilder.Append(JJOBDIMBAIC.AIMMFJBHLGE.Substring(0, 2) + "/");
				stringBuilder.Append(JJOBDIMBAIC.AIMMFJBHLGE);
			}
			else
			{
				stringBuilder.Append(JJOBDIMBAIC.CIAJGHECFHJ);
			}
			__result = stringBuilder.ToString();
			ClientLog.AccumulateClientLog("Download url-" + __result);
			return false;
		}*/
		static void Postfix(ref string __result, LFEKLJKFNPE JJOBDIMBAIC)
        {
			ClientLog.AccumulateClientLog("原始下载地址->" + __result);
			if (!PCRSettings.replaceManifestURL)
				return;
			string[] urls = __result.Split('/');
            if (urls.Length > 0 & urls[urls.Length - 1].Contains("manifest"))
            {
				string path = PCRSettings.ManifestPath + "/" + urls[urls.Length - 1];
				if (File.Exists(path))
                {
					__result = path;
                }
            }
			ClientLog.AccumulateClientLog("重定向->" + __result);

		}
	}
}
