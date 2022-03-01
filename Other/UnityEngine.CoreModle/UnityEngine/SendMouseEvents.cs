using UnityEngine.Scripting;

namespace UnityEngine
{
	internal class SendMouseEvents
	{
		private struct HitInfo
		{
			public GameObject target;

			public Camera camera;

			public void SendMessage(string name)
			{
				target.SendMessage(name, null, SendMessageOptions.DontRequireReceiver);
			}

			public static implicit operator bool(HitInfo exists)
			{
				return exists.target != null && exists.camera != null;
			}

			public static bool Compare(HitInfo lhs, HitInfo rhs)
			{
				return lhs.target == rhs.target && lhs.camera == rhs.camera;
			}
		}

		private const int m_HitIndexGUI = 0;

		private const int m_HitIndexPhysics3D = 1;

		private const int m_HitIndexPhysics2D = 2;

		private static bool s_MouseUsed = false;

		private static readonly HitInfo[] m_LastHit = new HitInfo[3]
		{
			default(HitInfo),
			default(HitInfo),
			default(HitInfo)
		};

		private static readonly HitInfo[] m_MouseDownHit = new HitInfo[3]
		{
			default(HitInfo),
			default(HitInfo),
			default(HitInfo)
		};

		private static readonly HitInfo[] m_CurrentHit = new HitInfo[3]
		{
			default(HitInfo),
			default(HitInfo),
			default(HitInfo)
		};

		private static Camera[] m_Cameras;

		[RequiredByNativeCode]
		private static void SetMouseMoved()
		{
			s_MouseUsed = true;
		}

		private static void HitTestLegacyGUI(Camera camera, Vector3 mousePosition, ref HitInfo hitInfo)
		{
			GUILayer component = camera.GetComponent<GUILayer>();
			if ((bool)component)
			{
				GUIElement gUIElement = component.HitTest(mousePosition);
				if ((bool)gUIElement)
				{
					hitInfo.target = gUIElement.gameObject;
					hitInfo.camera = camera;
				}
				else
				{
					hitInfo.target = null;
					hitInfo.camera = null;
				}
			}
		}

		[RequiredByNativeCode]
		private static void DoSendMouseEvents(int skipRTCameras)
		{
			Vector3 mousePosition = Input.mousePosition;
			int allCamerasCount = Camera.allCamerasCount;
			if (m_Cameras == null || m_Cameras.Length != allCamerasCount)
			{
				m_Cameras = new Camera[allCamerasCount];
			}
			Camera.GetAllCameras(m_Cameras);
			for (int i = 0; i < m_CurrentHit.Length; i++)
			{
				m_CurrentHit[i] = default(HitInfo);
			}
			if (!s_MouseUsed)
			{
				Camera[] cameras = m_Cameras;
				foreach (Camera camera in cameras)
				{
					if (camera == null || (skipRTCameras != 0 && camera.targetTexture != null))
					{
						continue;
					}
					int targetDisplay = camera.targetDisplay;
					Vector3 vector = Display.RelativeMouseAt(mousePosition);
					if (vector != Vector3.zero)
					{
						int num = (int)vector.z;
						if (num != targetDisplay)
						{
							continue;
						}
						float num2 = Screen.width;
						float num3 = Screen.height;
						if (targetDisplay > 0 && targetDisplay < Display.displays.Length)
						{
							num2 = Display.displays[targetDisplay].systemWidth;
							num3 = Display.displays[targetDisplay].systemHeight;
						}
						Vector2 vector2 = new Vector2(vector.x / num2, vector.y / num3);
						if (vector2.x < 0f || vector2.x > 1f || vector2.y < 0f || vector2.y > 1f)
						{
							continue;
						}
					}
					else
					{
						vector = mousePosition;
					}
					if (!camera.pixelRect.Contains(vector))
					{
						continue;
					}
					HitTestLegacyGUI(camera, vector, ref m_CurrentHit[0]);
					if (camera.eventMask != 0)
					{
						Ray ray = camera.ScreenPointToRay(vector);
						float z = ray.direction.z;
						float distance = ((!Mathf.Approximately(0f, z)) ? Mathf.Abs((camera.farClipPlane - camera.nearClipPlane) / z) : float.PositiveInfinity);
						GameObject gameObject = camera.RaycastTry(ray, distance, camera.cullingMask & camera.eventMask);
						if (gameObject != null)
						{
							m_CurrentHit[1].target = gameObject;
							m_CurrentHit[1].camera = camera;
						}
						else if (camera.clearFlags == CameraClearFlags.Skybox || camera.clearFlags == CameraClearFlags.Color)
						{
							m_CurrentHit[1].target = null;
							m_CurrentHit[1].camera = null;
						}
						GameObject gameObject2 = camera.RaycastTry2D(ray, distance, camera.cullingMask & camera.eventMask);
						if (gameObject2 != null)
						{
							m_CurrentHit[2].target = gameObject2;
							m_CurrentHit[2].camera = camera;
						}
						else if (camera.clearFlags == CameraClearFlags.Skybox || camera.clearFlags == CameraClearFlags.Color)
						{
							m_CurrentHit[2].target = null;
							m_CurrentHit[2].camera = null;
						}
					}
				}
			}
			for (int k = 0; k < m_CurrentHit.Length; k++)
			{
				SendEvents(k, m_CurrentHit[k]);
			}
			s_MouseUsed = false;
		}

		private static void SendEvents(int i, HitInfo hit)
		{
			bool mouseButtonDown = Input.GetMouseButtonDown(0);
			bool mouseButton = Input.GetMouseButton(0);
			if (mouseButtonDown)
			{
				if ((bool)hit)
				{
					m_MouseDownHit[i] = hit;
					m_MouseDownHit[i].SendMessage("OnMouseDown");
				}
			}
			else if (!mouseButton)
			{
				if ((bool)m_MouseDownHit[i])
				{
					if (HitInfo.Compare(hit, m_MouseDownHit[i]))
					{
						m_MouseDownHit[i].SendMessage("OnMouseUpAsButton");
					}
					m_MouseDownHit[i].SendMessage("OnMouseUp");
					m_MouseDownHit[i] = default(HitInfo);
				}
			}
			else if ((bool)m_MouseDownHit[i])
			{
				m_MouseDownHit[i].SendMessage("OnMouseDrag");
			}
			if (HitInfo.Compare(hit, m_LastHit[i]))
			{
				if ((bool)hit)
				{
					hit.SendMessage("OnMouseOver");
				}
			}
			else
			{
				if ((bool)m_LastHit[i])
				{
					m_LastHit[i].SendMessage("OnMouseExit");
				}
				if ((bool)hit)
				{
					hit.SendMessage("OnMouseEnter");
					hit.SendMessage("OnMouseOver");
				}
			}
			m_LastHit[i] = hit;
		}
	}
}
