using System.Collections.Generic;
using System.Linq;

namespace Newtonsoft0.Json.Schema
{
	internal class JsonSchemaModelBuilder
	{
		private JsonSchemaNodeCollection _nodes = new JsonSchemaNodeCollection();

		private Dictionary<JsonSchemaNode, JsonSchemaModel> _nodeModels = new Dictionary<JsonSchemaNode, JsonSchemaModel>();

		private JsonSchemaNode _node;

		public JsonSchemaModel Build(JsonSchema schema)
		{
			_nodes = new JsonSchemaNodeCollection();
			_node = AddSchema(null, schema);
			_nodeModels = new Dictionary<JsonSchemaNode, JsonSchemaModel>();
			return BuildNodeModel(_node);
		}

		public JsonSchemaNode AddSchema(JsonSchemaNode existingNode, JsonSchema schema)
		{
			string id;
			if (existingNode != null)
			{
				if (existingNode.Schemas.Contains(schema))
				{
					return existingNode;
				}
				id = JsonSchemaNode.GetId(existingNode.Schemas.Union(new JsonSchema[1]
				{
					schema
				}));
			}
			else
			{
				id = JsonSchemaNode.GetId(new JsonSchema[1]
				{
					schema
				});
			}
			if (_nodes.Contains(id))
			{
				return _nodes[id];
			}
			JsonSchemaNode jsonSchemaNode = ((existingNode != null) ? existingNode.Combine(schema) : new JsonSchemaNode(schema));
			_nodes.Add(jsonSchemaNode);
			if (schema.Properties != null)
			{
				foreach (KeyValuePair<string, JsonSchema> property in schema.Properties)
				{
					AddProperty(jsonSchemaNode, property.Key, property.Value);
				}
			}
			if (schema.Items != null)
			{
				for (int i = 0; i < schema.Items.Count; i++)
				{
					AddItem(jsonSchemaNode, i, schema.Items[i]);
				}
			}
			if (schema.AdditionalProperties != null)
			{
				AddAdditionalProperties(jsonSchemaNode, schema.AdditionalProperties);
			}
			if (schema.Extends != null)
			{
				jsonSchemaNode = AddSchema(jsonSchemaNode, schema.Extends);
			}
			return jsonSchemaNode;
		}

		public void AddProperty(JsonSchemaNode parentNode, string propertyName, JsonSchema schema)
		{
			parentNode.Properties.TryGetValue(propertyName, out var value);
			parentNode.Properties[propertyName] = AddSchema(value, schema);
		}

		public void AddItem(JsonSchemaNode parentNode, int index, JsonSchema schema)
		{
			JsonSchemaNode existingNode = ((parentNode.Items.Count > index) ? parentNode.Items[index] : null);
			JsonSchemaNode jsonSchemaNode = AddSchema(existingNode, schema);
			if (parentNode.Items.Count <= index)
			{
				parentNode.Items.Add(jsonSchemaNode);
			}
			else
			{
				parentNode.Items[index] = jsonSchemaNode;
			}
		}

		public void AddAdditionalProperties(JsonSchemaNode parentNode, JsonSchema schema)
		{
			parentNode.AdditionalProperties = AddSchema(parentNode.AdditionalProperties, schema);
		}

		private JsonSchemaModel BuildNodeModel(JsonSchemaNode node)
		{
			if (_nodeModels.TryGetValue(node, out var value))
			{
				return value;
			}
			value = JsonSchemaModel.Create(node.Schemas);
			_nodeModels[node] = value;
			foreach (KeyValuePair<string, JsonSchemaNode> property in node.Properties)
			{
				if (value.Properties == null)
				{
					value.Properties = new Dictionary<string, JsonSchemaModel>();
				}
				value.Properties[property.Key] = BuildNodeModel(property.Value);
			}
			for (int i = 0; i < node.Items.Count; i++)
			{
				if (value.Items == null)
				{
					value.Items = new List<JsonSchemaModel>();
				}
				value.Items.Add(BuildNodeModel(node.Items[i]));
			}
			if (node.AdditionalProperties != null)
			{
				value.AdditionalProperties = BuildNodeModel(node.AdditionalProperties);
			}
			return value;
		}
	}
}
