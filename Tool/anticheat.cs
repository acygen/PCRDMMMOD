using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeStage.AntiCheat.ObscuredTypes;
using HarmonyLib;
using CodeStage.AntiCheat.Utils;
using PCRCalculator.Tool;


namespace PCRCalculator.Hook
{
    
    [HarmonyPatch(typeof(IKPCKBIAJMO), "SetInt")]
    class anticheat
    {
        static bool Prefix(string HDAJOEOLHGG, int PDGAOEAMDCL)
        {
            FakePlayerPrefab.SetInt(HDAJOEOLHGG, PDGAOEAMDCL);
            return false;
        }

    }
    [HarmonyPatch(typeof(IKPCKBIAJMO), "GetInt",new Type[] { typeof(string),typeof(int)})]
    class anticheat2
    {
        static bool Prefix(ref int __result,string HDAJOEOLHGG, int JLLDHLJOAOI)
        {
            __result =  FakePlayerPrefab.GetInt(HDAJOEOLHGG, JLLDHLJOAOI);
            return false;
        }

    }
    [HarmonyPatch(typeof(IKPCKBIAJMO), "GetInt", new Type[] { typeof(string)})]
    class anticheat20
    {
        static bool Prefix(ref int __result, string HDAJOEOLHGG)
        {
            __result = FakePlayerPrefab.GetInt(HDAJOEOLHGG, 0);
            return false;
        }

    }
    [HarmonyPatch(typeof(IKPCKBIAJMO), "SetString")]
    class anticheat3
    {
        static bool Prefix(string HDAJOEOLHGG, string PDGAOEAMDCL)
        {
            FakePlayerPrefab.SetString(HDAJOEOLHGG, PDGAOEAMDCL);
            return false;
        }

    }
    [HarmonyPatch(typeof(IKPCKBIAJMO), "GetString", new Type[] { typeof(string), typeof(string) })]
    class anticheat4
    {
        static bool Prefix(ref string __result, string HDAJOEOLHGG, string JLLDHLJOAOI)
        {
            __result = FakePlayerPrefab.GetString(HDAJOEOLHGG, JLLDHLJOAOI);
            return false;
        }

    }
    [HarmonyPatch(typeof(IKPCKBIAJMO), "GetString", new Type[] { typeof(string)})]
    class anticheat40
    {
        static bool Prefix(ref string __result, string HDAJOEOLHGG)
        {
            __result = FakePlayerPrefab.GetString(HDAJOEOLHGG, "");
            return false;
        }

    }
    [HarmonyPatch(typeof(IKPCKBIAJMO), "HasKey")]
    class anticheat5
    {
        static bool Prefix(ref bool __result, string HDAJOEOLHGG)
        {
            __result = FakePlayerPrefab.HasKey(HDAJOEOLHGG);
            return false;
        }

    }
}
