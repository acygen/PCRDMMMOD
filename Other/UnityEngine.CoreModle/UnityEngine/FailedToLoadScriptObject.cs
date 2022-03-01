using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	[RequiredByNativeCode]
	[NativeClass(null)]
	[ExcludeFromObjectFactory]
	internal class FailedToLoadScriptObject : Object
	{
		private FailedToLoadScriptObject()
		{
		}
	}
}
