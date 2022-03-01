using System;

namespace Newtonsoft0.Json.Serialization
{
	public class JsonPrimitiveContract : JsonContract
	{
		public JsonPrimitiveContract(Type underlyingType)
			: base(underlyingType)
		{
		}
	}
}
