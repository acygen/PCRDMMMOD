using System;

namespace UnityEngine
{
	[Flags]
	public enum ComputeBufferType
	{
		Default = 0x0,
		Raw = 0x1,
		Append = 0x2,
		Counter = 0x4,
		[Obsolete("Enum member DrawIndirect has been deprecated. Use IndirectArguments instead (UnityUpgradable) -> IndirectArguments", false)]
		DrawIndirect = 0x100,
		IndirectArguments = 0x100,
		[Obsolete("Enum member GPUMemory has been deprecated. All compute buffers now follow the behavior previously defined by this member.", false)]
		GPUMemory = 0x200
	}
}
