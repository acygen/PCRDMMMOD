using System;
using UnityEngine;

namespace UnityEngineInternal
{
	public sealed class APIUpdaterRuntimeServices
	{
		[Obsolete("Method is not meant to be used at runtime. Please, replace this call with GameObject.AddComponent<T>()/GameObject.AddComponent(Type).", true)]
		public static Component AddComponent(GameObject go, string sourceInfo, string name)
		{
			throw new Exception();
		}
	}
}
