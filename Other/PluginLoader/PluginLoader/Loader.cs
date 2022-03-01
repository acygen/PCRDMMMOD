using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using UnityEngine;

namespace PluginLoader
{
	public class Loader
	{
		private static bool loaded;

		private static void ClearLog()
		{
			File.WriteAllText("loader.log", string.Empty);
		}

		internal static void Log(string msg)
		{
			File.AppendAllText("loader.log", msg + "\n");
		}

		private static Assembly Load(string filename)
		{
			Log("loading plugin from " + filename);
			return Assembly.LoadFrom(filename);
		}

		private static Plugin Initiate(Type type)
		{
			Log("initiating " + type.Name + " from " + type.Assembly.GetName().Name);
			return Activator.CreateInstance(type) as Plugin;
		}

		public static void Main()
		{
			if (loaded)
			{
				return;
			}
			loaded = true;
			Dictionary<string, Assembly> plugins;
			new Thread((ThreadStart)delegate
			{
				try
				{
					ClearLog();
					Log("plugin loader is now ready to load plugins");
					Directory.CreateDirectory("Plugins");
					plugins = (from file in Directory.GetFiles("Plugins")
						where file.EndsWith(".dll")
						select Load(file)).ToDictionary((Assembly asm) => asm.FullName, (Assembly asm) => asm);
					AppDomain.CurrentDomain.AssemblyResolve += delegate(object _, ResolveEventArgs args)
					{
						if (plugins.TryGetValue(args.Name, out var value))
						{
							Log("plugin dependency resolved: " + args.Name + " => " + Path.GetFileName(value.Location));
							return value;
						}
						Log("plugin dependency resolve failed: " + args.Name);
						return null;
					};
					Application.logMessageReceivedThreaded += delegate(string condition, string stackTrace, LogType type)
					{
						Log($"[{type}] {condition}\n{stackTrace}{new StackTrace()}");
					};
					foreach (Plugin item in from type in plugins.SelectMany((KeyValuePair<string, Assembly> asm) => asm.Value.GetTypes())
						where type.IsSubclassOf(typeof(Plugin))
						select Initiate(type) into plugin
						orderby plugin.Priority descending
						select plugin)
					{
						Type type2 = item.GetType();
						Log("loading " + type2.Name + " from " + type2.Assembly.GetName().Name);
						try
						{
							item.Initialize();
						}
						catch (Exception arg)
						{
							Log($"ignoring plugin {type2.Name} due to failure in loading.\n{arg}");
						}
					}
					Log("plugin load finished.");
				}
				catch (Exception arg2)
				{
					Log($"Error occurred when trying to load plugins:\n{arg2}");
				}
			}).Start();
		}
	}
}
