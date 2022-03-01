using System;
using System.Globalization;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Converters
{
	public class StringEnumConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			Enum @enum = (Enum)value;
			string text = @enum.ToString("G");
			if (char.IsNumber(text[0]) || text[0] == '-')
			{
				writer.WriteValue(value);
			}
			else
			{
				writer.WriteValue(text);
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			Type type = (ReflectionUtils.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType);
			if (reader.TokenType == JsonToken.Null)
			{
				if (!ReflectionUtils.IsNullableType(objectType))
				{
					throw new Exception("Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
				}
				return null;
			}
			if (reader.TokenType == JsonToken.String)
			{
				return Enum.Parse(type, reader.Value.ToString(), ignoreCase: true);
			}
			if (reader.TokenType == JsonToken.Integer)
			{
				return ConvertUtils.ConvertOrCast(reader.Value, CultureInfo.InvariantCulture, type);
			}
			throw new Exception("Unexpected token when parsing enum. Expected String or Integer, got {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
		}

		public override bool CanConvert(Type objectType)
		{
			Type type = (ReflectionUtils.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType);
			return type.IsEnum;
		}
	}
}
