using System;

namespace UnityEngine
{
	[AttributeUsage(AttributeTargets.Method)]
	public class BeforeRenderOrderAttribute : Attribute
	{
		public int order { get; private set; }

		public BeforeRenderOrderAttribute(int order)
		{
			this.order = order;
		}
	}
}
