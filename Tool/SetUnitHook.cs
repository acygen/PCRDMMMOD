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
    [HarmonyPatch(typeof(PartsViewUnitDetailStatus), "onClickRecommended")]
    public class SetUnitHook
    {
        static bool Prefix(PartsViewUnitDetailStatus __instance)
        {
            //int unitid = Traverse.Create(__instance).Field("unitid").GetValue<int>();
            int unitid = Singleton<Elements.Data.EHPLBCOOOPK>.Instance.DGLHJHFMGJD;
            var action = Traverse.Create(__instance).Field("onRequestRecommendedEnhance").GetValue<Action>();
            PCRTool.Instance.OpenUnitEditForm(unitid,()=>
            {
                /*UserData userData = Singleton<UserData>.Instance;
                string jsonSTR = Newtonsoft0.Json.JsonConvert.SerializeObject(PCRSettings.Instance.GetUnitDataS(unitid));
                JsonData jsonData = JsonMapper.ToObject(jsonSTR);
                userData.UpdateUnitParameter(new Elements.UnitData(jsonData));
                action?.Invoke();*/
            });




            return true;
        }
        
    }
}
