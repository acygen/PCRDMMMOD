using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Cute;
using Elements;
using PluginLoader;
using UnityEngine;
using System.Collections;
using BepInEx;
using HarmonyLib;


namespace PCRCalculator
{
#if true
	public class MainClass : Plugin
	{			

		public override void Initialize()
		{
			Extensions.harmony = Plugin.harmony;
			UnityEngine.Application.logMessageReceivedThreaded += Application_logMessageReceivedThreaded;
			harmony.PatchAll();
		}

		private void Application_logMessageReceivedThreaded(string condition, string stackTrace, LogType type)
		{
			Plugin.Log($"[{type}] {condition}\n{stackTrace}{new StackTrace()}");
		}


	}
#else
	[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
	[BepInProcess("PrincessConnectReDive.exe")]
	public class PrincessInjector : BaseUnityPlugin
	{
		private void Awake()
		{
			this.harmony.PatchAll();
			UnityEngine.Debug.Log("##############################################3");
		}

		public const string PLUGIN_GUID = "3531bffd-9876-4ca0-95a6-bfb64a693b1c";

		public const string PLUGIN_NAME = "ResourceLoader";

		public const string PLUGIN_DESCRIPTION = "ac";

		public const string PLUGIN_VERSION = "1.0.0.0";

		// Token: 0x04000005 RID: 5
		private readonly Harmony harmony = new Harmony(PLUGIN_GUID);
	}
#endif
}
