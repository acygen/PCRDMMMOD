using System.Collections;

namespace Newtonsoft0.Json.Utilities
{
	internal interface IWrappedDictionary : IDictionary, ICollection, IEnumerable
	{
		object UnderlyingDictionary
		{
			get;
		}
	}
}
