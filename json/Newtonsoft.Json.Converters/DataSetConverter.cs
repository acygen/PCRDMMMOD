using System;
using System.Data;

namespace Newtonsoft0.Json.Converters
{
	public class DataSetConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			DataSet dataSet = (DataSet)value;
			DataTableConverter dataTableConverter = new DataTableConverter();
			writer.WriteStartObject();
			foreach (DataTable table in dataSet.Tables)
			{
				writer.WritePropertyName(table.TableName);
				dataTableConverter.WriteJson(writer, table, serializer);
			}
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			DataSet dataSet = new DataSet();
			DataTableConverter dataTableConverter = new DataTableConverter();
			reader.Read();
			while (reader.TokenType == JsonToken.PropertyName)
			{
				DataTable table = (DataTable)dataTableConverter.ReadJson(reader, typeof(DataTable), null, serializer);
				dataSet.Tables.Add(table);
			}
			reader.Read();
			return dataSet;
		}

		public override bool CanConvert(Type valueType)
		{
			return valueType == typeof(DataSet);
		}
	}
}
