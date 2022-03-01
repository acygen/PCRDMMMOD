using System.Collections.ObjectModel;

namespace Newtonsoft0.Json.Utilities
{
	internal class EnumValues<T> : KeyedCollection<string, EnumValue<T>> where T : struct
	{
		protected override string GetKeyForItem(EnumValue<T> item)
		{
			return item.Name;
		}
	}
}
