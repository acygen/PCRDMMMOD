using System;
using System.ComponentModel;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Linq.ComponentModel
{
	public class JPropertyDescriptor : PropertyDescriptor
	{
		private readonly Type _propertyType;

		public override Type ComponentType => typeof(JObject);

		public override bool IsReadOnly => false;

		public override Type PropertyType => _propertyType;

		protected override int NameHashCode => base.NameHashCode;

		public JPropertyDescriptor(string name, Type propertyType)
			: base(name, null)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			ValidationUtils.ArgumentNotNull(propertyType, "propertyType");
			_propertyType = propertyType;
		}

		private static JObject CastInstance(object instance)
		{
			return (JObject)instance;
		}

		public override bool CanResetValue(object component)
		{
			return false;
		}

		public override object GetValue(object component)
		{
			return CastInstance(component)[Name];
		}

		public override void ResetValue(object component)
		{
		}

		public override void SetValue(object component, object value)
		{
			JToken value2 = ((value is JToken) ? ((JToken)value) : new JValue(value));
			CastInstance(component)[Name] = value2;
		}

		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}
	}
}
