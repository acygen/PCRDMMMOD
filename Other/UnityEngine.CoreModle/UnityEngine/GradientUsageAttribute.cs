using System;

namespace UnityEngine
{
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class GradientUsageAttribute : PropertyAttribute
	{
		public readonly bool hdr = false;

		public GradientUsageAttribute(bool hdr)
		{
			this.hdr = hdr;
		}
	}
}
