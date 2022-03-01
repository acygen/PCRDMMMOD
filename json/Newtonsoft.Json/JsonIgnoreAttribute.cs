using System;

namespace Newtonsoft0.Json
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public sealed class JsonIgnoreAttribute : Attribute
	{
	}
}
