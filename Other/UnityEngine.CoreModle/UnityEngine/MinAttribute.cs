using System;

namespace UnityEngine
{
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class MinAttribute : PropertyAttribute
	{
		public readonly float min;

		public MinAttribute(float min)
		{
			this.min = min;
		}
	}
}
