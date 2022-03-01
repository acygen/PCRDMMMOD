using System;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Serialization
{
	public abstract class JsonContract
	{
		public Type UnderlyingType
		{
			get;
			private set;
		}

		public Type CreatedType
		{
			get;
			set;
		}

		public bool? IsReference
		{
			get;
			set;
		}

		public JsonConverter Converter
		{
			get;
			set;
		}

		internal JsonConverter InternalConverter
		{
			get;
			set;
		}

		public MethodInfo OnDeserialized
		{
			get;
			set;
		}

		public MethodInfo OnDeserializing
		{
			get;
			set;
		}

		public MethodInfo OnSerialized
		{
			get;
			set;
		}

		public MethodInfo OnSerializing
		{
			get;
			set;
		}

		public Func<object> DefaultCreator
		{
			get;
			set;
		}

		public bool DefaultCreatorNonPublic
		{
			get;
			set;
		}

		public MethodInfo OnError
		{
			get;
			set;
		}

		internal void InvokeOnSerializing(object o, StreamingContext context)
		{
			if (OnSerializing != null)
			{
				OnSerializing.Invoke(o, new object[1]
				{
					context
				});
			}
		}

		internal void InvokeOnSerialized(object o, StreamingContext context)
		{
			if (OnSerialized != null)
			{
				OnSerialized.Invoke(o, new object[1]
				{
					context
				});
			}
		}

		internal void InvokeOnDeserializing(object o, StreamingContext context)
		{
			if (OnDeserializing != null)
			{
				OnDeserializing.Invoke(o, new object[1]
				{
					context
				});
			}
		}

		internal void InvokeOnDeserialized(object o, StreamingContext context)
		{
			if (OnDeserialized != null)
			{
				OnDeserialized.Invoke(o, new object[1]
				{
					context
				});
			}
		}

		internal void InvokeOnError(object o, StreamingContext context, ErrorContext errorContext)
		{
			if (OnError != null)
			{
				OnError.Invoke(o, new object[2]
				{
					context,
					errorContext
				});
			}
		}

		internal JsonContract(Type underlyingType)
		{
			ValidationUtils.ArgumentNotNull(underlyingType, "underlyingType");
			UnderlyingType = underlyingType;
			CreatedType = underlyingType;
		}
	}
}
