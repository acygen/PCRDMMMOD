using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	[RequireComponent(typeof(Transform))]
	public class GUIElement : Behaviour
	{
		[ExcludeFromDocs]
		public bool HitTest(Vector3 screenPosition)
		{
			return HitTest(new Vector2(screenPosition.x, screenPosition.y), null);
		}

		public bool HitTest(Vector3 screenPosition, [DefaultValue("null")] Camera camera)
		{
			return HitTest(new Vector2(screenPosition.x, screenPosition.y), GetCameraOrWindowRect(camera));
		}

		public Rect GetScreenRect([DefaultValue("null")] Camera camera)
		{
			return GetScreenRect(GetCameraOrWindowRect(camera));
		}

		[ExcludeFromDocs]
		public Rect GetScreenRect()
		{
			return GetScreenRect(null);
		}

		private Rect GetScreenRect(Rect rect)
		{
			GetScreenRect_Injected(ref rect, out var ret);
			return ret;
		}

		private bool HitTest(Vector2 screenPosition, Rect cameraRect)
		{
			return HitTest_Injected(ref screenPosition, ref cameraRect);
		}

		private static Rect GetCameraOrWindowRect(Camera camera)
		{
			if (camera != null)
			{
				return camera.pixelRect;
			}
			return new Rect(0f, 0f, Screen.width, Screen.height);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetScreenRect_Injected(ref Rect rect, out Rect ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool HitTest_Injected(ref Vector2 screenPosition, ref Rect cameraRect);
	}
}
