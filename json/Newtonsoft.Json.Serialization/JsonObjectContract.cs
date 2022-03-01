using System;
using System.Reflection;

namespace Newtonsoft0.Json.Serialization
{
	public class JsonObjectContract : JsonContract
	{
		public MemberSerialization MemberSerialization
		{
			get;
			set;
		}

		public JsonPropertyCollection Properties
		{
			get;
			private set;
		}

		public ConstructorInfo ParametrizedConstructor
		{
			get;
			set;
		}

		public JsonObjectContract(Type underlyingType)
			: base(underlyingType)
		{
			Properties = new JsonPropertyCollection(this);
		}
	}
}
