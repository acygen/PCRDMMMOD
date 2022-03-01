using System;
using System.ComponentModel;

namespace Newtonsoft0.Json.Linq.ComponentModel
{
	internal class JTypeDescriptionProvider : TypeDescriptionProvider
	{
		public override ICustomTypeDescriptor GetTypeDescriptor(Type type, object instance)
		{
			JObject jObject = instance as JObject;
			if (jObject != null)
			{
				return new JTypeDescriptor(jObject);
			}
			return base.GetTypeDescriptor(type, instance);
		}
	}
}
