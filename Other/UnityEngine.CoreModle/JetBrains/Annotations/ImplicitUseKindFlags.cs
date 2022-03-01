using System;

namespace JetBrains.Annotations
{
	[Flags]
	public enum ImplicitUseKindFlags
	{
		Default = 0x7,
		Access = 0x1,
		Assign = 0x2,
		InstantiatedWithFixedConstructorSignature = 0x4,
		InstantiatedNoFixedConstructorSignature = 0x8
	}
}
