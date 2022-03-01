using System;
using UnityEngine.Scripting;

namespace Unity.Collections
{
	[AttributeUsage(AttributeTargets.Field)]
	[RequiredByNativeCode]
	public sealed class NativeFixedLengthAttribute : Attribute
	{
		public int FixedLength;

		public NativeFixedLengthAttribute(int fixedLength)
		{
			FixedLength = fixedLength;
		}
	}
}
