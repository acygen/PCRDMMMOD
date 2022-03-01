using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	[Obsolete("This component is part of the legacy UI system and will be removed in a future release.")]
	[RequireComponent(typeof(Camera))]
	public class GUILayer : Behaviour
	{
		public GUIElement HitTest(Vector3 screenPosition)
		{
			return HitTest(new Vector2(screenPosition.x, screenPosition.y));
		}

		private GUIElement HitTest(Vector2 screenPosition)
		{
			return HitTest_Injected(ref screenPosition);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern GUIElement HitTest_Injected(ref Vector2 screenPosition);
	}
}
