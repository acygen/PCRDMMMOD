using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	[AttributeUsage(AttributeTargets.Interface)]
	public sealed class JobProducerTypeAttribute : Attribute
	{
		public Type ProducerType { get; }

		public JobProducerTypeAttribute(Type producerType)
		{
			ProducerType = producerType;
		}
	}
}
