using System.Collections;

namespace Newtonsoft0.Json.Utilities
{
	internal interface IWrappedList : IList, ICollection, IEnumerable
	{
		object UnderlyingList
		{
			get;
		}
	}
}
