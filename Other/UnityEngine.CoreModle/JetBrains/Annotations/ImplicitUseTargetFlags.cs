using System;

namespace JetBrains.Annotations
{
	[Flags]
	public enum ImplicitUseTargetFlags
	{
		Default = 0x1,
		Itself = 0x1,
		Members = 0x2,
		WithMembers = 0x3
	}
}
