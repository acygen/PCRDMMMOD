using System;
using UnityEngine.Scripting;

namespace UnityEngine.Serialization
{
	[RequiredByNativeCode]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	public class FormerlySerializedAsAttribute : Attribute
	{
		private string m_oldName;

		public string oldName => m_oldName;

		public FormerlySerializedAsAttribute(string oldName)
		{
			m_oldName = oldName;
		}
	}
}
