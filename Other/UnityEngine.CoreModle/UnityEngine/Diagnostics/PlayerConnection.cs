using System;
using System.ComponentModel;
using UnityEngine.Networking.PlayerConnection;

namespace UnityEngine.Diagnostics
{
	public static class PlayerConnection
	{
		[Obsolete("Use UnityEngine.Networking.PlayerConnection.PlayerConnection.instance.isConnected instead.")]
		public static bool connected => UnityEngine.Networking.PlayerConnection.PlayerConnection.instance.isConnected;

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("PlayerConnection.SendFile is no longer supported.", true)]
		public static void SendFile(string remoteFilePath, byte[] data)
		{
		}
	}
}
