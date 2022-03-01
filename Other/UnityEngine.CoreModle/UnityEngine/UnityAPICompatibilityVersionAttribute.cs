using System;

namespace UnityEngine
{
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public class UnityAPICompatibilityVersionAttribute : Attribute
	{
		private string _version;

		public string version => _version;

		public UnityAPICompatibilityVersionAttribute(string version)
		{
			_version = version;
		}
	}
}
