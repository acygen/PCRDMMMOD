using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[RequiredByNativeCode]
	[Obsolete("Use SerializeField on the private variables that you want to be serialized instead")]
	public sealed class SerializePrivateVariables : Attribute
	{
	}
}
