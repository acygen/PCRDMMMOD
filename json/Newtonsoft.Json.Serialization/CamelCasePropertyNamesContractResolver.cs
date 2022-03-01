using System.Globalization;

namespace Newtonsoft0.Json.Serialization
{
	public class CamelCasePropertyNamesContractResolver : DefaultContractResolver
	{
		public CamelCasePropertyNamesContractResolver()
			: base(shareCache: true)
		{
		}

		protected override string ResolvePropertyName(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				return propertyName;
			}
			if (!char.IsUpper(propertyName[0]))
			{
				return propertyName;
			}
			string text = char.ToLower(propertyName[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
			if (propertyName.Length > 1)
			{
				text += propertyName.Substring(1);
			}
			return text;
		}
	}
}
