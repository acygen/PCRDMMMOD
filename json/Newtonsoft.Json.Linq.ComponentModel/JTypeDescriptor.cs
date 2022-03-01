using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Linq.ComponentModel
{
	public class JTypeDescriptor : ICustomTypeDescriptor
	{
		private readonly JObject _value;

		public JTypeDescriptor(JObject value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			_value = value;
		}

		public virtual PropertyDescriptorCollection GetProperties()
		{
			return GetProperties(null);
		}

		private static Type GetTokenPropertyType(JToken token)
		{
			if (token is JValue)
			{
				JValue jValue = (JValue)token;
				if (jValue.Value == null)
				{
					return typeof(object);
				}
				return jValue.Value.GetType();
			}
			return token.GetType();
		}

		public virtual PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
			if (_value != null)
			{
				foreach (KeyValuePair<string, JToken> item in _value)
				{
					propertyDescriptorCollection.Add(new JPropertyDescriptor(item.Key, GetTokenPropertyType(item.Value)));
				}
				return propertyDescriptorCollection;
			}
			return propertyDescriptorCollection;
		}

		public AttributeCollection GetAttributes()
		{
			return AttributeCollection.Empty;
		}

		public string GetClassName()
		{
			return null;
		}

		public string GetComponentName()
		{
			return null;
		}

		public TypeConverter GetConverter()
		{
			return new TypeConverter();
		}

		public EventDescriptor GetDefaultEvent()
		{
			return null;
		}

		public PropertyDescriptor GetDefaultProperty()
		{
			return null;
		}

		public object GetEditor(Type editorBaseType)
		{
			return null;
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return EventDescriptorCollection.Empty;
		}

		public EventDescriptorCollection GetEvents()
		{
			return EventDescriptorCollection.Empty;
		}

		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return null;
		}
	}
}
