using System.Collections.ObjectModel;

namespace Newtonsoft0.Json.Schema
{
	internal class JsonSchemaNodeCollection : KeyedCollection<string, JsonSchemaNode>
	{
		protected override string GetKeyForItem(JsonSchemaNode item)
		{
			return item.Id;
		}
	}
}
