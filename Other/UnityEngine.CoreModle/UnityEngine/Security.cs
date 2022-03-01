using System;
using System.ComponentModel;
using System.Reflection;
using UnityEngine.Internal;

namespace UnityEngine
{
	public sealed class Security
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This was an internal method which is no longer used", true)]
		public static Assembly LoadAndVerifyAssembly(byte[] assemblyData, string authorizationKey)
		{
			return null;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This was an internal method which is no longer used", true)]
		public static Assembly LoadAndVerifyAssembly(byte[] assemblyData)
		{
			return null;
		}

		[Obsolete("Security.PrefetchSocketPolicy is no longer supported, since the Unity Web Player is no longer supported by Unity.", true)]
		[ExcludeFromDocs]
		public static bool PrefetchSocketPolicy(string ip, int atPort)
		{
			int timeout = 3000;
			return PrefetchSocketPolicy(ip, atPort, timeout);
		}

		[Obsolete("Security.PrefetchSocketPolicy is no longer supported, since the Unity Web Player is no longer supported by Unity.", true)]
		public static bool PrefetchSocketPolicy(string ip, int atPort, [UnityEngine.Internal.DefaultValue("3000")] int timeout)
		{
			return false;
		}
	}
}
