using System.Collections.Generic;
using Newtonsoft0.Json.Linq;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Schema
{
	internal class JsonSchemaModel
	{
		public bool Optional
		{
			get;
			set;
		}

		public JsonSchemaType Type
		{
			get;
			set;
		}

		public int? MinimumLength
		{
			get;
			set;
		}

		public int? MaximumLength
		{
			get;
			set;
		}

		public int? MaximumDecimals
		{
			get;
			set;
		}

		public double? Minimum
		{
			get;
			set;
		}

		public double? Maximum
		{
			get;
			set;
		}

		public int? MinimumItems
		{
			get;
			set;
		}

		public int? MaximumItems
		{
			get;
			set;
		}

		public IList<string> Patterns
		{
			get;
			set;
		}

		public IList<JsonSchemaModel> Items
		{
			get;
			set;
		}

		public IDictionary<string, JsonSchemaModel> Properties
		{
			get;
			set;
		}

		public JsonSchemaModel AdditionalProperties
		{
			get;
			set;
		}

		public bool AllowAdditionalProperties
		{
			get;
			set;
		}

		public IList<JToken> Enum
		{
			get;
			set;
		}

		public JsonSchemaType Disallow
		{
			get;
			set;
		}

		public JsonSchemaModel()
		{
			Type = JsonSchemaType.Any;
			AllowAdditionalProperties = true;
			Optional = true;
		}

		public static JsonSchemaModel Create(IList<JsonSchema> schemata)
		{
			JsonSchemaModel jsonSchemaModel = new JsonSchemaModel();
			foreach (JsonSchema schematum in schemata)
			{
				Combine(jsonSchemaModel, schematum);
			}
			return jsonSchemaModel;
		}

		private static void Combine(JsonSchemaModel model, JsonSchema schema)
		{
			model.Optional = model.Optional && (schema.Optional ?? false);
			model.Type &= schema.Type ?? JsonSchemaType.Any;
			model.MinimumLength = MathUtils.Max(model.MinimumLength, schema.MinimumLength);
			model.MaximumLength = MathUtils.Min(model.MaximumLength, schema.MaximumLength);
			model.MaximumDecimals = MathUtils.Min(model.MaximumDecimals, schema.MaximumDecimals);
			model.Minimum = MathUtils.Max(model.Minimum, schema.Minimum);
			model.Maximum = MathUtils.Max(model.Maximum, schema.Maximum);
			model.MinimumItems = MathUtils.Max(model.MinimumItems, schema.MinimumItems);
			model.MaximumItems = MathUtils.Min(model.MaximumItems, schema.MaximumItems);
			model.AllowAdditionalProperties = model.AllowAdditionalProperties && schema.AllowAdditionalProperties;
			if (schema.Enum != null)
			{
				if (model.Enum == null)
				{
					model.Enum = new List<JToken>();
				}
				model.Enum.AddRangeDistinct(schema.Enum, new JTokenEqualityComparer());
			}
			model.Disallow |= schema.Disallow ?? JsonSchemaType.None;
			if (schema.Pattern != null)
			{
				if (model.Patterns == null)
				{
					model.Patterns = new List<string>();
				}
				model.Patterns.AddDistinct(schema.Pattern);
			}
		}
	}
}
