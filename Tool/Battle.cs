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

namespace PCRCalculator.Tool
{
    [HarmonyPatch(typeof(Elements.Battle.BattleManager), "updateFrame")]
    public class Battle
    {
        public static bool Prefix(Elements.Battle.BattleManager __instance)
        {
            if (PCRBattle.Instance.saveData.useUBSet)
            {

            }
            return true;
        }

    }
    

}
