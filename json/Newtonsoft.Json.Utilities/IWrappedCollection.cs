using System.Collections;

namespace Newtonsoft0.Json.Utilities
{
	internal interface IWrappedCollection : IList, ICollection, IEnumerable
	{
		object UnderlyingCollection
		{
			get;
		}
	}
}
