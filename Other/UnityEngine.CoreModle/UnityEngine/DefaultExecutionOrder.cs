using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	[AttributeUsage(AttributeTargets.Class)]
	public class DefaultExecutionOrder : Attribute
	{
		public int order { get; private set; }

		public DefaultExecutionOrder(int order)
		{
			this.order = order;
		}
	}
}
