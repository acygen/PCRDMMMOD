using System;
using System.ComponentModel;

namespace UnityEngine
{
	public static class Types
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This was an internal method which is no longer used", true)]
		public static Type GetType(string typeName, string assemblyName)
		{
			return null;
		}
	}
}
