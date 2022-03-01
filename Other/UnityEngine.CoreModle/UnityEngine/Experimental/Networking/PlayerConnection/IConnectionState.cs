using System;

namespace UnityEngine.Experimental.Networking.PlayerConnection
{
	public interface IConnectionState : IDisposable
	{
		ConnectionTarget connectedToTarget { get; }

		string connectionName { get; }
	}
}
