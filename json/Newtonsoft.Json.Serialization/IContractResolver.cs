using System;

namespace Newtonsoft0.Json.Serialization
{
	public interface IContractResolver
	{
		JsonContract ResolveContract(Type type);
	}
}
