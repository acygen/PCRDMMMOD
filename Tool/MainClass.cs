using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Cute;
using Elements;
using PluginLoader;
using UnityEngine;
using System.Collections;


namespace PCRCalculator
{
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
}
