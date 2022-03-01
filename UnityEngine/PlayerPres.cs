using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCRCalculator.Tool;

namespace UnityEngine0
{
    public class PlayerPrefs
    {
        public static bool HasKey(string key)
        {
            return FakePlayerPrefab.HasKey(key);
        }
        public static void SetInt(string key, int value)
        {
            FakePlayerPrefab.SetInt(key, value);
        }
        public static int GetInt(string key, int value = 0)
        {
            return FakePlayerPrefab.GetInt(key, value);
        }
        public static void SetString(string key, string value)
        {
            FakePlayerPrefab.SetString(key, value);
        }
        public static string GetString(string key, string value = "")
        {
            return FakePlayerPrefab.GetString(key, value);
        }
        public static void SetFloat(string key, float value)
        {
            FakePlayerPrefab.SetFloat(key, value);
        }
        public static float GetFloat(string key, float value = 0)
        {
            return FakePlayerPrefab.GetFloat(key, value);

        }
        public static void DeleteKey(string key)
        {
            FakePlayerPrefab.DeleteKey(key);
        }
        public static void Save()
        {
            FakePlayerPrefab.Save();
        }
        bool T1()
        {
            return false;
        }
        int T2(int a)
        {
            return a;
        }
    }
}
