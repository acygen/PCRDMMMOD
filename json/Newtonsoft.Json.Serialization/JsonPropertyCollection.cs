using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Serialization
{
	public class JsonPropertyCollection : KeyedCollection<string, JsonProperty>
	{
		private readonly JsonObjectContract _contract;

		public JsonPropertyCollection(JsonObjectContract contract)
		{
			ValidationUtils.ArgumentNotNull(contract, "contract");
			_contract = contract;
		}

		protected override string GetKeyForItem(JsonProperty item)
		{
			return item.PropertyName;
		}

		public void AddProperty(JsonProperty property)
		{
			if (Contains(property.PropertyName))
			{
				if (property.Ignored)
				{
					return;
				}
				JsonProperty jsonProperty = base[property.PropertyName];
				if (!jsonProperty.Ignored)
				{
					throw new JsonSerializationException("A member with the name '{0}' already exists on '{1}'. Use the JsonPropertyAttribute to specify another name.".FormatWith(CultureInfo.InvariantCulture, property.PropertyName, _contract.UnderlyingType));
				}
				Remove(jsonProperty);
			}
			Add(property);
		}

		public JsonProperty GetClosestMatchProperty(string propertyName)
		{
			JsonProperty property = GetProperty(propertyName, StringComparison.Ordinal);
			if (property == null)
			{
				property = GetProperty(propertyName, StringComparison.OrdinalIgnoreCase);
			}
			return property;
		}

		public JsonProperty GetProperty(string propertyName, StringComparison comparisonType)
		{
			using (IEnumerator<JsonProperty> enumerator = GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JsonProperty current = enumerator.Current;
					if (string.Equals(propertyName, current.PropertyName, comparisonType))
					{
						return current;
					}
				}
			}
			return null;
		}
	}
}
