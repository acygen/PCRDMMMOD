using System;
using UnityEngine.Scripting;

namespace Unity.Collections.LowLevel.Unsafe
{
	[RequiredByNativeCode]
	[AttributeUsage(AttributeTargets.Struct)]
	public sealed class NativeContainerSupportsMinMaxWriteRestrictionAttribute : Attribute
	{
	}
}
