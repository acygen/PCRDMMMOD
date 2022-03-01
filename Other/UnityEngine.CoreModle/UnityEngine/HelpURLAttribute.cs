using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class HelpURLAttribute : Attribute
	{
		internal readonly string m_Url;

		public string URL => m_Url;

		public HelpURLAttribute(string url)
		{
			m_Url = url;
		}
	}
}
