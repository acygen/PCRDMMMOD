using System;

namespace UnityEngine
{
	[Flags]
	internal enum DiagnosticSwitchFlags
	{
		None = 0x0,
		CanChangeAfterEngineStart = 0x1
	}
}
