using System;
using System.Globalization;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Converters
{
	public class JavaScriptDateTimeConverter : DateTimeConverterBase
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value is DateTime)
			{
				DateTime dateTime = ((DateTime)value).ToUniversalTime();
				long value2 = JsonConvert.ConvertDateTimeToJavaScriptTicks(dateTime);
				writer.WriteStartConstructor("Date");
				writer.WriteValue(value2);
				writer.WriteEndConstructor();
				return;
			}
			throw new Exception("Expected date object value.");
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (ReflectionUtils.IsNullableType(objectType))
			{
				Nullable.GetUnderlyingType(objectType);
			}
			if (reader.TokenType == JsonToken.Null)
			{
				if (!ReflectionUtils.IsNullableType(objectType))
				{
					throw new Exception("Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
				}
				return null;
			}
			if (reader.TokenType != JsonToken.StartConstructor || string.Compare(reader.Value.ToString(), "Date", StringComparison.Ordinal) != 0)
			{
				throw new Exception("Unexpected token or value when parsing date. Token: {0}, Value: {1}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType, reader.Value));
			}
			reader.Read();
			if (reader.TokenType != JsonToken.Integer)
			{
				throw new Exception("Unexpected token parsing date. Expected Integer, got {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			long javaScriptTicks = (long)reader.Value;
			DateTime dateTime = JsonConvert.ConvertJavaScriptTicksToDateTime(javaScriptTicks);
			reader.Read();
			if (reader.TokenType != JsonToken.EndConstructor)
			{
				throw new Exception("Unexpected token parsing date. Expected EndConstructor, got {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			return dateTime;
		}
	}
}
