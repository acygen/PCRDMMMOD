using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[RequiredByNativeCode]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class PreferBinarySerialization : Attribute
	{
	}
}
