using System;
using System.Linq;
using System.Reflection;
using HarmonyLib;

namespace PCRCalculator
{
	internal static class Extensions
	{
		public static Harmony harmony;

		public static MethodInfo Get(this Type type, string name, params Type[] types)
		{
			return type.GetMethod(name, types);
		}

		public static MethodInfo Get(this Type type, string name)
		{
			return (from m in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
				where m.Name == name && !m.IsGenericMethod
				select m).First();
		}

		public static void Prefix(this MethodInfo origin, MethodInfo prefix)
		{
			harmony.Patch(origin, new HarmonyMethod(prefix));
		}

		public static void Postfix(this MethodInfo origin, MethodInfo postfix)
		{
			harmony.Patch(origin, null, new HarmonyMethod(postfix));
		}
	}
}
