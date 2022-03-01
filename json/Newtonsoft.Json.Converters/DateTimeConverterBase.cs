using System;

namespace Newtonsoft0.Json.Converters
{
	public abstract class DateTimeConverterBase : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			if (objectType == typeof(DateTime) || objectType == typeof(DateTime?))
			{
				return true;
			}
			return false;
		}
	}
}
