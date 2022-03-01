using System;

namespace Newtonsoft0.Json.Serialization
{
	public class JsonISerializableContract : JsonContract
	{
		public ObjectConstructor<object> ISerializableCreator
		{
			get;
			set;
		}

		public JsonISerializableContract(Type underlyingType)
			: base(underlyingType)
		{
		}
	}
}
