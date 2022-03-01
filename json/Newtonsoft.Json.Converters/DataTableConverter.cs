using System;
using System.Data;

namespace Newtonsoft0.Json.Converters
{
	public class DataTableConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			DataTable dataTable = (DataTable)value;
			writer.WriteStartArray();
			foreach (DataRow row in dataTable.Rows)
			{
				writer.WriteStartObject();
				foreach (DataColumn column in row.Table.Columns)
				{
					writer.WritePropertyName(column.ColumnName);
					serializer.Serialize(writer, row[column]);
				}
				writer.WriteEndObject();
			}
			writer.WriteEndArray();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			DataTable dataTable;
			if (reader.TokenType == JsonToken.PropertyName)
			{
				dataTable = new DataTable((string)reader.Value);
				reader.Read();
			}
			else
			{
				dataTable = new DataTable();
			}
			reader.Read();
			while (reader.TokenType == JsonToken.StartObject)
			{
				DataRow dataRow = dataTable.NewRow();
				reader.Read();
				while (reader.TokenType == JsonToken.PropertyName)
				{
					string text = (string)reader.Value;
					reader.Read();
					if (!dataTable.Columns.Contains(text))
					{
						Type columnDataType = GetColumnDataType(reader.TokenType);
						dataTable.Columns.Add(new DataColumn(text, columnDataType));
					}
					dataRow[text] = reader.Value;
					reader.Read();
				}
				dataRow.EndEdit();
				dataTable.Rows.Add(dataRow);
				reader.Read();
			}
			reader.Read();
			return dataTable;
		}

		private static Type GetColumnDataType(JsonToken tokenType)
		{
			switch (tokenType)
			{
			case JsonToken.Integer:
				return typeof(long);
			case JsonToken.Float:
				return typeof(double);
			case JsonToken.String:
			case JsonToken.Null:
			case JsonToken.Undefined:
				return typeof(string);
			case JsonToken.Boolean:
				return typeof(bool);
			case JsonToken.Date:
				return typeof(DateTime);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		public override bool CanConvert(Type valueType)
		{
			return valueType == typeof(DataTable);
		}
	}
}
