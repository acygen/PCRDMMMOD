using System.Collections.Generic;
using System.Linq;

namespace Newtonsoft0.Json.Schema
{
	public class JsonSchemaResolver
	{
		public IList<JsonSchema> LoadedSchemas
		{
			get;
			protected set;
		}

		public JsonSchemaResolver()
		{
			LoadedSchemas = new List<JsonSchema>();
		}

		public virtual JsonSchema GetSchema(string id)
		{
			return LoadedSchemas.SingleOrDefault((JsonSchema s) => s.Id == id);
		}
	}
}
