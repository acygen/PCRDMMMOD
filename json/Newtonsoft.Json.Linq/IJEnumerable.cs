using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft0.Json.Linq
{
	public interface IJEnumerable<T> : IEnumerable<T>, IEnumerable where T : JToken
	{
		IJEnumerable<JToken> this[object key]
		{
			get;
		}
	}
}
