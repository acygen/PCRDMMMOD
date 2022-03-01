using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Elements;
using Elements.Battle;
using PCRCalculator.Tool;
using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;

namespace PCRCalculator.Hook
{
    //[HarmonyPatch(typeof(Elements.PartsHomeCharacter), "updateCharacterStill")]
    class HomeFix
    {
        static bool Prefix(PartsHomeCharacter __instance, int _illustId, bool _fadeExec, bool _playsParticle)
        {
            return false;
        }
    }
}
