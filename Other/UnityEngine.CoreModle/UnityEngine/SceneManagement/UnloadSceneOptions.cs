using System;

namespace UnityEngine.SceneManagement
{
	[Flags]
	public enum UnloadSceneOptions
	{
		None = 0x0,
		UnloadAllEmbeddedSceneObjects = 0x1
	}
}
