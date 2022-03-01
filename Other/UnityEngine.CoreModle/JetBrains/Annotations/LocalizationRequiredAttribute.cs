using System;

namespace JetBrains.Annotations
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	public sealed class LocalizationRequiredAttribute : Attribute
	{
		public bool Required { get; private set; }

		public LocalizationRequiredAttribute()
			: this(required: true)
		{
		}

		public LocalizationRequiredAttribute(bool required)
		{
			Required = required;
		}
	}
}
