using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	[RequiredByNativeCode]
	public class ResourceRequest : AsyncOperation
	{
		internal string m_Path;

		internal Type m_Type;

		public Object asset => Resources.Load(m_Path, m_Type);
	}
}
