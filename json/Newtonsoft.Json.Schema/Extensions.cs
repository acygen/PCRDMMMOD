using Newtonsoft0.Json.Linq;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Schema
{
	public static class Extensions
	{
		public static bool IsValid(this JToken source, JsonSchema schema)
		{
			bool valid = true;
			source.Validate(schema, delegate
			{
				valid = false;
			});
			return valid;
		}

		public static void Validate(this JToken source, JsonSchema schema)
		{
			source.Validate(schema, null);
		}

		public static void Validate(this JToken source, JsonSchema schema, ValidationEventHandler validationEventHandler)
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			ValidationUtils.ArgumentNotNull(schema, "schema");
			using (JsonValidatingReader jsonValidatingReader = new JsonValidatingReader(source.CreateReader()))
			{
				jsonValidatingReader.Schema = schema;
				if (validationEventHandler != null)
				{
					jsonValidatingReader.ValidationEventHandler += validationEventHandler;
				}
				while (jsonValidatingReader.Read())
				{
				}
			}
		}
	}
}
