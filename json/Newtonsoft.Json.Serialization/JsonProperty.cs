using System;

namespace Newtonsoft0.Json.Serialization
{
	public class JsonProperty
	{
		public string PropertyName
		{
			get;
			set;
		}

		public IValueProvider ValueProvider
		{
			get;
			set;
		}

		public Type PropertyType
		{
			get;
			set;
		}

		public JsonConverter Converter
		{
			get;
			set;
		}

		public bool Ignored
		{
			get;
			set;
		}

		public bool Readable
		{
			get;
			set;
		}

		public bool Writable
		{
			get;
			set;
		}

		public JsonConverter MemberConverter
		{
			get;
			set;
		}

		public object DefaultValue
		{
			get;
			set;
		}

		public Required Required
		{
			get;
			set;
		}

		public bool? IsReference
		{
			get;
			set;
		}

		public NullValueHandling? NullValueHandling
		{
			get;
			set;
		}

		public DefaultValueHandling? DefaultValueHandling
		{
			get;
			set;
		}

		public ReferenceLoopHandling? ReferenceLoopHandling
		{
			get;
			set;
		}

		public ObjectCreationHandling? ObjectCreationHandling
		{
			get;
			set;
		}

		public TypeNameHandling? TypeNameHandling
		{
			get;
			set;
		}

		public Predicate<object> ShouldSerialize
		{
			get;
			set;
		}

		public override string ToString()
		{
			return PropertyName;
		}
	}
}
