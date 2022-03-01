using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Runtime/Export/CullingGroup.bindings.h")]
	public class CullingGroup : IDisposable
	{
		public delegate void StateChanged(CullingGroupEvent sphere);

		internal IntPtr m_Ptr;

		private StateChanged m_OnStateChanged = null;

		public StateChanged onStateChanged
		{
			get
			{
				return m_OnStateChanged;
			}
			set
			{
				m_OnStateChanged = value;
			}
		}

		public extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Camera targetCamera
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public CullingGroup()
		{
			m_Ptr = Init(this);
		}

		~CullingGroup()
		{
			if (m_Ptr != IntPtr.Zero)
			{
				FinalizerFailure();
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CullingGroup_Bindings::Dispose", HasExplicitThis = true)]
		private extern void DisposeInternal();

		public void Dispose()
		{
			DisposeInternal();
			m_Ptr = IntPtr.Zero;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBoundingSpheres(BoundingSphere[] array);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBoundingSphereCount(int count);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void EraseSwapBack(int index);

		public static void EraseSwapBack<T>(int index, T[] myArray, ref int size)
		{
			size--;
			myArray[index] = myArray[size];
		}

		public int QueryIndices(bool visible, int[] result, int firstIndex)
		{
			return QueryIndices(visible, -1, CullingQueryOptions.IgnoreDistance, result, firstIndex);
		}

		public int QueryIndices(int distanceIndex, int[] result, int firstIndex)
		{
			return QueryIndices(visible: false, distanceIndex, CullingQueryOptions.IgnoreVisibility, result, firstIndex);
		}

		public int QueryIndices(bool visible, int distanceIndex, int[] result, int firstIndex)
		{
			return QueryIndices(visible, distanceIndex, CullingQueryOptions.Normal, result, firstIndex);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CullingGroup_Bindings::QueryIndices", HasExplicitThis = true)]
		[NativeThrows]
		private extern int QueryIndices(bool visible, int distanceIndex, CullingQueryOptions options, int[] result, int firstIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CullingGroup_Bindings::IsVisible", HasExplicitThis = true)]
		[NativeThrows]
		public extern bool IsVisible(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CullingGroup_Bindings::GetDistance", HasExplicitThis = true)]
		[NativeThrows]
		public extern int GetDistance(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CullingGroup_Bindings::SetBoundingDistances", HasExplicitThis = true)]
		public extern void SetBoundingDistances(float[] distances);

		[FreeFunction("CullingGroup_Bindings::SetDistanceReferencePoint", HasExplicitThis = true)]
		private void SetDistanceReferencePoint_InternalVector3(Vector3 point)
		{
			SetDistanceReferencePoint_InternalVector3_Injected(ref point);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("SetDistanceReferenceTransform")]
		private extern void SetDistanceReferencePoint_InternalTransform(Transform transform);

		public void SetDistanceReferencePoint(Vector3 point)
		{
			SetDistanceReferencePoint_InternalVector3(point);
		}

		public void SetDistanceReferencePoint(Transform transform)
		{
			SetDistanceReferencePoint_InternalTransform(transform);
		}

		[RequiredByNativeCode]
		[SecuritySafeCritical]
		private unsafe static void SendEvents(CullingGroup cullingGroup, IntPtr eventsPtr, int count)
		{
			CullingGroupEvent* ptr = (CullingGroupEvent*)eventsPtr.ToPointer();
			if (cullingGroup.m_OnStateChanged != null)
			{
				for (int i = 0; i < count; i++)
				{
					cullingGroup.m_OnStateChanged(ptr[i]);
				}
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CullingGroup_Bindings::Init")]
		private static extern IntPtr Init(object scripting);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("CullingGroup_Bindings::FinalizerFailure", HasExplicitThis = true, IsThreadSafe = true)]
		private extern void FinalizerFailure();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetDistanceReferencePoint_InternalVector3_Injected(ref Vector3 point);
	}
}
