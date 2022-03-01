using System.Diagnostics;
using HarmonyLib;

namespace PluginLoader
{
	public abstract class Plugin
	{
		protected static readonly Harmony harmony = new Harmony("loader");

		public virtual int Priority => 0;

		public abstract void Initialize();

		protected static void Log(string msg)
		{
			Loader.Log(msg);
		}

		protected static void LogTrace(string msg)
		{
			Loader.Log(msg + "\n" + new StackTrace());
		}
	}
}
