using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;

namespace PCRCalculator.Tool
{
    public static class ShowPCRUI
    {
        public static void LoadUI()
        {
            if (PCRSettings.Instance.battleSetting.showUI)
            {
                LoadAB("pcrui.ab");
                LoadAB("pcr.ab");

            }
        }
        private static void LoadAB(string name)
        {
            string path = UnityEngine.Application.streamingAssetsPath + "/UI/" + name;
            if (File.Exists(path))
            {
                byte[] bytrs = File.ReadAllBytes(path);
                AssetBundle assetBundle = AssetBundle.LoadFromMemory(bytrs);
                if (assetBundle == null)
                {
                    Cute.ClientLog.AccumulateClientLog("ab包加载失败!" + bytrs.Length);
                    return;
                }
                GameObject fkGame = assetBundle.LoadAsset("PCRUI") as GameObject;
                Cute.ClientLog.AccumulateClientLog(name + "加载成功！");

            }
            else
            {
                Cute.ClientLog.AccumulateClientLog("UI文件不存在!");
            }

        }
    }
}
