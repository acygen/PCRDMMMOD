using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Cute;
using Elements;
//using PluginLoader;
using UnityEngine;
using System.Collections;
using LitJson;
using HarmonyLib;

namespace PCRCalculator.Tool
{
    [HarmonyPatch(typeof(Elements.Battle.BattleManager), "updateFrame")]
    public class Battle
    {
        public static void Postfix(Elements.Battle.BattleManager __instance)
        {
            PCRBattle.Instance.Update();
        }

    }

    //[HarmonyPatch(typeof(Elements.Battle.BattleManager), "Random",new Type[] {typeof(float),typeof(float)})]
    public class BattleManagerHook
    {
        public static int RandomState;
        public static bool Prefix(ref float __result, float LEKNHCFFAHO, float DPNHFKEOKIO)
        {
            float value = 0;
            switch (RandomState)
            {
                case 0:
                    return true;
                case 1:
                    value = (float)Elements.Battle.BattleManager.Random((int)(LEKNHCFFAHO * 1000f), (int)(DPNHFKEOKIO * 1000f)) / 1000f;
                    float value_0 = (value - LEKNHCFFAHO) / (DPNHFKEOKIO - LEKNHCFFAHO);
                    float value_1 = Mathf.Pow(value_0, 2f);
                    __result = value_1 * (DPNHFKEOKIO - LEKNHCFFAHO) + LEKNHCFFAHO;
                    break;
                case 2:
                    __result = LEKNHCFFAHO;
                    break;
                case -1:
                    value = (float)Elements.Battle.BattleManager.Random((int)(LEKNHCFFAHO * 1000f), (int)(DPNHFKEOKIO * 1000f)) / 1000f;
                    float value1_0 = (value - LEKNHCFFAHO) / (DPNHFKEOKIO - LEKNHCFFAHO);
                    float value1_1 = Mathf.Pow(value1_0, 0.5f);
                    __result = value1_1 * (DPNHFKEOKIO - LEKNHCFFAHO) + LEKNHCFFAHO;
                    break;
                case -2:
                    __result = DPNHFKEOKIO;
                    break;
            }
            RandomState = 0;
            Cute.ClientLog.AccumulateClientLog($"随机改变：{value}->{__result}");
            return false;
        }
        
    }
}
