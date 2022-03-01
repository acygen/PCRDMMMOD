using System;

namespace Newtonsoft0.Json
{
	[Flags]
	public enum PreserveReferencesHandling
	{
		None = 0x0,
		Objects = 0x1,
		Arrays = 0x2,
		All = 0x3
	}
}
