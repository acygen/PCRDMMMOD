using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
//using Cute;
using Cute.Download;
using Elements;
using System.Windows.Forms;
using PCRCalculator.Tool;
using CodeStage.AntiCheat.ObscuredTypes;
using System.Reflection;
using Coneshell;
using Cute;

namespace PCRCalculator.Hook
{
    public class Hook
    {
        public static void CallVoidMethod(Type type, object instance, string methodstr, object[] parammter)
        {
            var method = type.GetMethod(methodstr, BindingFlags.Instance | BindingFlags.NonPublic);
            //私有方法传参调用
            method.Invoke(instance, parammter);
        }
        public static T CallstaticMethod<T>(Type staticType, string methodstr, object[] parammter)
        {
            var method = staticType.GetMethod(methodstr, BindingFlags.Static | BindingFlags.NonPublic);
            object result = method.Invoke(null, parammter);
            return (T)result;
        }
    }

    [HarmonyPatch(typeof(Cute.PABCCELMCAJ), "LHMAODLAGNH", MethodType.Getter)]
    class ChangePath
    {
        static bool Prefix(ref string __result)
        {
            if (PCRSettings.DVGGWVBMH)
            {
                __result = UnityEngine.Application.streamingAssetsPath + "/Data";
                return false;
            }
            if (!PCRSettings.useUserAssestPath)
                return true;
            __result = PCRSettings.UserAssestPath;
            return false;
        }
    }
    [HarmonyPatch(typeof(Cute.JFDEDPMIGGN), "ApplicationQuit")]
    class QuitGame
    {
        static bool Prefix()
        {
            //MessageBox.Show("exit?");
            PCRSettings.Instance.OnQuitGame();
            PCRBattle.Instance.OnQiutGame();
            return true;
        }
    }
    [HarmonyPatch(typeof(Cute.ClientLog), "encryptLog")]
    class Decy
    {
        static bool Prefix(ref string __result, string KDLBCCGHJBN)
        {
            __result = KDLBCCGHJBN;
            return false;
        }

    }
    //[HarmonyPatch(typeof(Cute.BootApp), "Start")]
    class bootapp
    {
        static void Postfix(Cute.BootApp __instance)
        {
            //PCRTool.CreateInstance();
            //PCRBattle.CreateInstance();
        }
    }
    [HarmonyPatch(typeof(Cute.ClientLog), "WriteAccumulateClientLog")]
    class log0
    {
        static bool Prefix(string HDAJOEOLHGG, string NCIBMGNGHPO)
        {
            if (PCRSettings.useLog)
                PCRSettings.Onlog?.Invoke(NCIBMGNGHPO);
            return true;
        }
    }
    //[HarmonyPatch(typeof(Elements.DataUtility), "SetupNewCharacterIntroduction")]
    class setup0
    {
        static bool Prefix()
        {
            if (PCRSettings.ignoreNewCharacterIntroduction)
                return false;
            return true;
        }
    }
    //[HarmonyPatch(typeof(Elements.DownloadManager), "CheckAssetDiffList")]
    class download0
    {
        static bool Prefix(DownloadManager __instance, AssetBundleEditTag.eDlType _dlType, List<string> _extList = null)
        {
            if (PCRSettings.ignoreManifestCheck)
            {
                Traverse.Create(__instance).Property("DiffAssetList").SetValue(null);
                return false;
            }
            return true;
        }
        /*static bool Prefix(DownloadManager __instance, AssetBundleEditTag.eDlType _dlType, List<string> _extList = null)
        {
            if (PCRSettings.ignoreManifestCheck)
            {
                Traverse.Create(__instance).Property("DiffAssetList").SetValue(null);
                return false;
            }
            return true;
        }*/
    }
    //[HarmonyPatch(typeof(Cute.AssetManager), "ShouldDownloadKnownBundleManifests")]
    class main0
    {
        static bool Prefix(ref bool __result)
        {
            __result = false;
            return !PCRSettings.ignoreManifestCheck;
        }
    }
    //[HarmonyPatch(typeof(Cute.AssetManager), "DownloadCommonShader")]
    class main05
    {
        static bool Prefix(Cute.AssetManager __instance, ref IEnumerator __result)
        {
            __result = DownloadCommonShader();
            return !PCRSettings.ignoreManifestCheck;
        }
        static IEnumerator DownloadCommonShader()
        {
            yield break;
        }
    }
    [HarmonyPatch(typeof(Cute.AssetManager), "PrepareManifestList")]
    class main1
    {
        /*static bool Prefix(Cute.AssetManager __instance, out List<string> OJIGJPJBCIK, out List<string> BEFPMFMIJAJ, bool MHGAAPHOMIG)
        {
            OJIGJPJBCIK = new List<string>();
            BEFPMFMIJAJ = new List<string>();
            if (!PCRSettings.ignoreManifestCheck)
                return true;

            var categoryNameList = Traverse.Create(__instance).Field("categoryNameList").GetValue<List<string>>();
            BEFPMFMIJAJ.Add("manifest/bdl_assetmanifest");
            BEFPMFMIJAJ.AddRange(categoryNameList);
            BEFPMFMIJAJ.Add("manifest/sound2manifest");
            BEFPMFMIJAJ.Add("manifest/moviemanifest");
            BEFPMFMIJAJ.Add("manifest/soundmanifest");
            BEFPMFMIJAJ.Add("manifest/movie2manifest");
            return !PCRSettings.ignoreManifestCheck;
        }*/
        static void Postfix(Cute.AssetManager __instance, List<string> OJIGJPJBCIK, List<string> BEFPMFMIJAJ, bool MHGAAPHOMIG)
        {
            if (OJIGJPJBCIK != null)
            {
                foreach (string str in OJIGJPJBCIK)
                {
                    ClientLog.AccumulateClientLog($"PrepareManifest {str}");
                }
            }
            if (BEFPMFMIJAJ != null)
            {
                foreach (string str in OJIGJPJBCIK)
                {
                    ClientLog.AccumulateClientLog($"PrepareManifest2 {str}");
                }
            }
        }

    }
    //[HarmonyPatch(typeof(Cute.AssetManager), "DownloadAndLoadManifestOfManifest")]
    class main10
    {
        static bool Prefix(Cute.AssetManager __instance, ref IEnumerator __result, bool MHGAAPHOMIG)
        {
            if (!PCRSettings.ignoreManifestCheck)
            {
                return true;
            }
            __result = DownloadAndLoadManifestOfManifest(__instance, MHGAAPHOMIG);
            return false;
        }
        static IEnumerator DownloadAndLoadManifestOfManifest(Cute.AssetManager __instance, bool MHGAAPHOMIG)
        {
            Cute.LFEKLJKFNPE jJOBDIMBAIC = new Cute.LFEKLJKFNPE("manifest/manifest_assetmanifest", "", null, null, Cute.LFEKLJKFNPE.OHMMHCICKCF.Manifests);
            //_ = Traverse.Create(__instance).Method("RegistHandle", new object[] { jJOBDIMBAIC }).GetValue();
            Hook.CallVoidMethod(typeof(Cute.AssetManager), __instance, "RegistHandle", new object[] { "manifest/manifest_assetmanifest", jJOBDIMBAIC });
            //Dictionary<string, LFEKLJKFNPE> handleDictionary = Traverse.Create(__instance).Field("handleDictionary").GetValue<Dictionary<string, LFEKLJKFNPE>>();
            //handleDictionary.Add("manifest/manifest_assetmanifest", jJOBDIMBAIC);
            //RegistHandle("manifest/manifest_assetmanifest", jJOBDIMBAIC);
            if (false)
            {
                yield return null;
            }
            //_ = Traverse.Create(__instance).Method("LoadManifestOfManifest", new object[] { __instance.LoadFileToString("manifest/manifest_assetmanifest") }).GetValue();
            Hook.CallVoidMethod(typeof(Cute.AssetManager), __instance, "LoadManifestOfManifest", new object[] { __instance.LoadFileToString("manifest/manifest_assetmanifest") });
            //LoadManifestOfManifest(__instance.LoadFileToString("manifest/manifest_assetmanifest"));
        }

    }
    //[HarmonyPatch(typeof(AssetManager), "DownloadAndLoadManifests")]
    class main11
    {
        static bool Prefix(Cute.AssetManager __instance, ref IEnumerator __result, bool MHGAAPHOMIG)
        {
            if (!PCRSettings.ignoreManifestCheck)
            {
                return true;
            }
            __result = DownloadAndLoadManifests(__instance, MHGAAPHOMIG);
            return false;
        }
        static IEnumerator DownloadAndLoadManifests(Cute.AssetManager __instance, bool MHGAAPHOMIG)
        {

            List<string> OJIGJPJBCIK = null;
            List<string> loadList = null;
            //PrepareManifestList(out OJIGJPJBCIK, out loadList, MHGAAPHOMIG);
            var categoryNameList = Traverse.Create(__instance).Field("categoryNameList").GetValue<List<string>>();
            loadList.Add("manifest/bdl_assetmanifest");
            loadList.AddRange(categoryNameList);
            loadList.Add("manifest/sound2manifest");
            loadList.Add("manifest/moviemanifest");
            try
            {
                loadList.ForEach(delegate (string NKICMENDFFA)
                {
                    var pp = Traverse.Create(__instance).Method("GetManifestAssetType", new object[] { NKICMENDFFA }).GetValue<Cute.LFEKLJKFNPE.OHMMHCICKCF>();
                    object[] para = new object[] { __instance.LoadFileToString(NKICMENDFFA), pp };
                    Hook.CallVoidMethod(typeof(Cute.AssetManager), __instance, "LoadManifest", para);
            //LoadManifest(LoadFileToString(NKICMENDFFA), GetManifestAssetType(NKICMENDFFA));
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Traverse.Create(__instance).Property("BDMIAMJFAJK").SetValue(true);
            //BDMIAMJFAJK = true;
            yield break;
        }

    }
    [HarmonyPatch(typeof(Cute.AssetManager), "LoadManifest")]
    class main12
    {
        static bool Prefix(Cute.AssetManager __instance, string IPMEMPDNLNM, Cute.LFEKLJKFNPE.OHMMHCICKCF LILCMDCAFLM)
        {
            try
            {
                int filepathColumn = 0;
                int hashColumn = 1;
                int downloadCategoryColumn = 2;
                int sizeColumn = 3;
                Cute.FMDCHHJPHIN.IterateCSV(IPMEMPDNLNM, delegate (ArrayList LNNHPLNDHHH)
                {
                    string text = (string)LNNHPLNDHHH[filepathColumn];
                    LFEKLJKFNPE jJOBDIMBAIC = new LFEKLJKFNPE(text, (string)LNNHPLNDHHH[hashColumn], (string)LNNHPLNDHHH[downloadCategoryColumn], (string)LNNHPLNDHHH[sizeColumn], LILCMDCAFLM);
                    Hook.CallVoidMethod(typeof(AssetManager), __instance, "RegistHandle", new object[] { text, jJOBDIMBAIC });
            //RegistHandle(text, jJOBDIMBAIC);
                }, FGPPOMPOAMD: false);
            }
            catch (Exception ex)
            {
                ClientLog.AccumulateClientLog($"ERROR at {IPMEMPDNLNM}:{ex.Message}\n{ex.StackTrace}");
            }
            return false;
        }

    }

    [HarmonyPatch(typeof(UnitUtility), "settingUnitUniqueEquipEnhanceStatus")]
    class unique
    {
        static bool Prefix(ref UniqueNoticeEquipStatus __result, UnitParameter _unitParam, Elements.EquipSlot _equipSlot, int _slotIndex, int _unitLevel)
        {
            __result = new UniqueNoticeEquipStatus();

            return false;

        }
    }
    //[HarmonyPatch(typeof(UnitUtility), "setUnitHighRarityEquipNotificationInfo")]
    class rarity
    {
        static bool Prefix(bool _isUpdateEquip, UnitParameter _unitParam)
        {
            return PCRSettings.DVGGWVBMH;
        }
    }
    [HarmonyPatch(typeof(ClanBattleUtil), "GetCurrentEventBossDifficulity")]
    class clanBattleDifficulty
    {
        static bool Prefix(ref ClanBattleDefine.eClanAuraEffectType __result, int _battleId, int _lapNum)
        {
            __result = ClanBattleDefine.eClanAuraEffectType.NORMAL;
            Cute.ClientLog.AccumulateClientLog("_battleid:" + _battleId + "  _lapNum:" + _lapNum);

            return true;
        }
    }
    //[HarmonyPatch(typeof(UnitUtility), "CalcUnitBaseParameter")]
    class calc0
    {
        static bool Prefix(ref int __result, eParamType _paramType, int _unitId, int _level, int _rarity, int _rank)
        {
            try
            {
                if (_paramType == eParamType.INVALID_VALUE || _paramType == eParamType.NONE)
                {
                    __result = 0;
                }
                MasterDataManager instance = ManagerSingleton<MasterDataManager>.Instance;
                float num = 0f;
                if (_rank > 1)
                {
                    num = instance.masterUnitPromotionStatus.Get(_unitId, _rank).GetFloatValue(_paramType);
                }
                float growthParam = Traverse.Create(typeof(UnitUtility)).Method("getGrowthParam", new object[] { _paramType, _unitId, _rarity }).GetValue<float>();
                //float growthParam = getGrowthParam(_paramType, _unitId, _rarity);
                float initialParam = Traverse.Create(typeof(UnitUtility)).Method("getInitialParam", new object[] { _paramType, _unitId, _rarity }).GetValue<float>();
                //float initialParam = getInitialParam(_paramType, _unitId, _rarity);
                float num2 = 0f;
                bool flag = Traverse.Create(typeof(UnitUtility)).Method("isRankUpBonusParameter", new object[] { _paramType }).GetValue<bool>();
                //if (isRankUpBonusParameter(_paramType))
                if (flag)
                {
                    num2 = growthParam * (float)_rank;
                }
                float promotionBonus = Traverse.Create(typeof(UnitUtility)).Method("getPromotionBonus", new object[] { _paramType, _unitId, _rarity }).GetValue<float>();

                //float promotionBonus = getPromotionBonus(_paramType, _unitId, _rank);
                __result = Traverse.Create(typeof(UnitUtility)).Method("round", new object[] { (float)_level * growthParam + initialParam + num + num2 + promotionBonus }).GetValue<int>();
                //__result = round((float)_level * growthParam + initialParam + num + num2 + promotionBonus);
            }
            catch (Exception ex)
            {
                Cute.ClientLog.AccumulateClientLog("计算角色：" + _unitId + "等级" + _level + "星" + _rarity + "RANK" + _rank + "的属性" + _paramType.GetDescription() + "时出错！");
                __result = 0;
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(DKFNICOAOBC), "OpenCustomVFS")]
    public class db
    {
        public static DKFNICOAOBC Instance;
        static bool Prefix(DKFNICOAOBC __instance, ref bool __result, string KCCLLCOPPHF, byte[] AIAPAJCMNHD)
        {
            Instance = __instance;
            __result = false;
            if (PCRSettings.DVGGWVBMH)
                return false;
            if (PCRSettings.useDBinStreamingAssestPath)
            {
                string path = PCRSettings.DBPath;
                if (!__instance.OpenWritable(path))
                {
                    __instance.CloseDB();
                    Cute.ClientLog.AccumulateClientLog("读取db失败！！！");
                    return true;
                }
                __result = true;
                return false;
            }

            return true;
        }
        public static bool ExecDB(string value, bool isPath)
        {
            if (Instance == null)
                return false;
            if (!PCRSettings.useDBinStreamingAssestPath)
            {
                Cute.ClientLog.AccumulateClientLog("无法更改原始db！");
                return false;
            }
            try
            {
                string dbName = System.IO.Path.GetFileNameWithoutExtension(PCRSettings.DBPath);
                string text;
                if (System.IO.Path.GetFileNameWithoutExtension(PCRSettings.DBPath).Contains("_changed1"))
                {
                    text = PCRSettings.DBPath.Replace("_changed1", "_changed2");
                }
                else if (System.IO.Path.GetFileNameWithoutExtension(PCRSettings.DBPath).Contains("_changed2"))
                {
                    text = PCRSettings.DBPath.Replace("_changed2", "_changed1");
                }
                else
                    text = PCRSettings.DBPath.Replace(dbName, dbName + "_changed1");
                //string text = PCRSettings.DBPath + ".tmp";
                System.IO.File.Copy(PCRSettings.DBPath, text, overwrite: true);
                Instance.OpenWritable(text);
                Instance.Begin();
                try
                {
                    string str = isPath ? System.IO.File.ReadAllText(value) : value;
                    Instance.Exec(str);
                    Instance.Commit();
                }
                catch (Exception ex)
                {
                    Instance.Rollback();
                    Instance.CloseDB();
                    Cute.ClientLog.AccumulateClientLog($"db patch failed:\n{ex}");
                    return false;
                }
                Instance.CloseDB();
                PCRSettings.DBPath = System.IO.Path.GetFileName(text);
                //ClientLog.AccumulateClientLog("db更改成功！");
                return true;
            }
            catch (Exception arg)
            {
                Cute.ClientLog.AccumulateClientLog($"db patch failed:\n{arg}");
                return false;
            }
        }
    }

    #region
    /*[HarmonyPatch(typeof(UnityEngine.PlayerPrefs), "DeleteKey")]
    class playerPrefab0
    {
    static bool Prefix(string key)
    {
        FakePlayerPrefab.DeleteKey(key);
        return false;
    }

    }
    [HarmonyPatch(typeof(UnityEngine.PlayerPrefs), "GetFloat", new Type[] { typeof(string), typeof(float) })]
    class playerPrefab1
    {
    static bool Prefix(ref float __result, string key, float value)
    {
        __result = FakePlayerPrefab.GetFloat(key, value);
        return false;
    }
    }
    [HarmonyPatch(typeof(UnityEngine.PlayerPrefs), "GetFloat", new Type[] { typeof(string) })]
    class playerPrefab2
    {
    static bool Prefix(ref float __result, string key)
    {
        __result = FakePlayerPrefab.GetFloat(key);
        return false;
    }
    }
    [HarmonyPatch(typeof(UnityEngine.PlayerPrefs), "GetInt", new Type[] { typeof(string), typeof(int) })]
    class playerPrefab3
    {
    static bool Prefix(ref int __result, string key, int value)
    {
        __result = FakePlayerPrefab.GetInt(key, value);
        return false;
    }
    }
    [HarmonyPatch(typeof(UnityEngine.PlayerPrefs), "GetInt", new Type[] { typeof(string) })]
    class playerPrefab4
    {
    static bool Prefix(ref int __result, string key)
    {
        __result = FakePlayerPrefab.GetInt(key);
        return false;
    }
    }
    [HarmonyPatch(typeof(UnityEngine.PlayerPrefs), "GetString", new Type[] { typeof(string), typeof(string) })]
    class playerPrefab5
    {
    static bool Prefix(ref string __result, string key, string value)
    {
        __result = FakePlayerPrefab.GetString(key, value);
        return false;
    }
    }
    [HarmonyPatch(typeof(UnityEngine.PlayerPrefs), "GetString", new Type[] { typeof(string)})]
    class playerPrefab6
    {
    static bool Prefix(ref string __result, string key)
    {
        __result = FakePlayerPrefab.GetString(key);
        return false;
    }
    }
    [HarmonyPatch(typeof(UnityEngine.PlayerPrefs), "HasKey")]
    class playerPrefab7
    {
    static bool Prefix(ref bool __result, string key)
    {
        __result = FakePlayerPrefab.HasKey(key);
        return false;
    }
    }
    [HarmonyPatch(typeof(UnityEngine.PlayerPrefs), "Save")]
    class playerPrefab8
    {
    static bool Prefix()
    {
        FakePlayerPrefab.Save();
        return false;
    }
    }
    [HarmonyPatch(typeof(UnityEngine.PlayerPrefs), "SetFloat")]
    class playerPrefab9
    {
    static bool Prefix(string key,float value)
    {
        FakePlayerPrefab.SetFloat(key, value);
        return false;
    }
    }
    [HarmonyPatch(typeof(UnityEngine.PlayerPrefs), "SetInt")]
    class playerPrefab10
    {
    static bool Prefix(string key, int value)
    {
        FakePlayerPrefab.SetInt(key, value);
        return false;
    }
    }
    [HarmonyPatch(typeof(UnityEngine.PlayerPrefs), "SetString")]
    class playerPrefab11
    {
    static bool Prefix(string key, string value)
    {
        FakePlayerPrefab.SetString(key, value);
        return false;
    }
    }*/
    #endregion
    [HarmonyPatch(typeof(Cute.JMFOFKDCHEC), "GetNowTime")]
    class time0
    {
        static bool Prefix(ref DateTime __result, long ENHKIFKKOCH, long PMBPHMNBLLF)
        {
            __result = PCRSettings.Instance.globalSetting.GetTime();
            return false;
        }
    }
    [HarmonyPatch(typeof(MasterClanBattleSchedule.ClanBattleSchedule), "end_time", MethodType.Getter)]
    class time1
    {
        static bool Prefix(MasterClanBattleSchedule.ClanBattleSchedule __instance, ref ObscuredString __result)
        {
            __result = "2025/06/25 4:59:59";
            return false;
        }
    }

}
