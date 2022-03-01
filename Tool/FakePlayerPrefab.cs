using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PCRCalculator.Tool;
using Cute;
using CodeStage.AntiCheat.ObscuredTypes;
using System;
using System.Text;

namespace PCRCalculator.Tool
{
    public static class FakePlayerPrefab
    {
        static bool useFake => PCRSettings.useMyPlayerPrefab;
        private static string cryptoKey = "e806f6";

        static MyPlayerPrefab playerPrefab => PCRSettings.Instance.myPlayerPrefab;
        public static bool HasKey(string key)
        {
            if (useFake)
                return playerPrefab.HasKey(key);
            else
                return PlayerPrefs.HasKey(key);
        }
        public static void SetInt(string key, int value)
        {
            if (useFake)
                playerPrefab.SetInt(key, value);
            else
            {
                PlayerPrefs.SetInt(key, value);
                playerPrefab.SetInt(key, value);
            }
        }
        public static int GetInt(string key, int value = 0)
        {
            if (useFake)
                return playerPrefab.GetInt(key, value);
            else
            {
                int vvv = PlayerPrefs.GetInt(key, value);
                playerPrefab.SetInt(key, vvv);
                return vvv;
            }
        }
        public static void SetString(string key, string value)
        {
            playerPrefab.SetStr(key, value);
            if (!useFake)
                PlayerPrefs.SetString(key, value);
        }
        public static string GetString(string key, string value = "")
        {
            if (useFake)
                return playerPrefab.GetStr(key, value);
            else
            {
                string vvv = PlayerPrefs.GetString(key, value);
                playerPrefab.SetStr(key, vvv);
                return vvv;
            }
        }
        public static void SetFloat(string key, float value)
        {
            playerPrefab.SetFloat(key, value);
            if (!useFake)
                PlayerPrefs.SetFloat(key, value);
        }
        public static float GetFloat(string key, float value = 0)
        {
            if (useFake)
                return playerPrefab.GetFloat(key, value);
            else
            {
                float vvv = PlayerPrefs.GetFloat(key, value);
                playerPrefab.SetFloat(key, vvv);
                return vvv;
            }

        }
        public static void DeleteKey(string key)
        {
            if (useFake)
                playerPrefab.DeleteKey(key);
            else
                PlayerPrefs.DeleteKey(key);
        }
        public static void Save()
        {
            PCRSettings.SavePlayerprefabs();
            if (!useFake)
                PlayerPrefs.Save();
        }        

    }
}