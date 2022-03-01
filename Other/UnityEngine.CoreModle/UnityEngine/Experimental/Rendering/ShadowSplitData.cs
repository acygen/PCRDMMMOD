using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	[UsedByNativeCode]
	public struct ShadowSplitData
	{
		public int cullingPlaneCount;

		internal unsafe fixed float _cullingPlanes[40];

		public Vector4 cullingSphere;

		public unsafe Plane GetCullingPlane(int index)
		{
			if (index < 0 || index >= cullingPlaneCount || index >= 10)
			{
				throw new IndexOutOfRangeException("Invalid plane index");
			}
			fixed (float* ptr = _cullingPlanes)
			{
				return new Plane(new Vector3(System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4), System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 1), System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 2)), System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 3));
			}
		}

		public unsafe void SetCullingPlane(int index, Plane plane)
		{
			if (index < 0 || index >= cullingPlaneCount || index >= 10)
			{
				throw new IndexOutOfRangeException("Invalid plane index");
			}
			fixed (float* ptr = _cullingPlanes)
			{
				System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4) = plane.normal.x;
				System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 1) = plane.normal.y;
				System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 2) = plane.normal.z;
				System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 3) = plane.distance;
			}
		}
	}
}
