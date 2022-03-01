using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	[UsedByNativeCode]
	public struct CameraProperties
	{
		private const int kNumLayers = 32;

		private Rect screenRect;

		private Vector3 viewDir;

		private float projectionNear;

		private float projectionFar;

		private float cameraNear;

		private float cameraFar;

		private float cameraAspect;

		private Matrix4x4 cameraToWorld;

		private Matrix4x4 actualWorldToClip;

		private Matrix4x4 cameraClipToWorld;

		private Matrix4x4 cameraWorldToClip;

		private Matrix4x4 implicitProjection;

		private Matrix4x4 stereoWorldToClipLeft;

		private Matrix4x4 stereoWorldToClipRight;

		private Matrix4x4 worldToCamera;

		private Vector3 up;

		private Vector3 right;

		private Vector3 transformDirection;

		private Vector3 cameraEuler;

		private Vector3 velocity;

		private float farPlaneWorldSpaceLength;

		private uint rendererCount;

		internal unsafe fixed float _shadowCullPlanes[24];

		internal unsafe fixed float _cameraCullPlanes[24];

		private float baseFarDistance;

		private Vector3 shadowCullCenter;

		internal unsafe fixed float layerCullDistances[32];

		private int layerCullSpherical;

		private CoreCameraValues coreCameraValues;

		private uint cameraType;

		private int projectionIsOblique;

		private int isImplicitProjectionMatrix;

		public unsafe Plane GetShadowCullingPlane(int index)
		{
			if (index < 0 || index >= 6)
			{
				throw new IndexOutOfRangeException("Invalid plane index");
			}
			fixed (float* ptr = _shadowCullPlanes)
			{
				return new Plane(new Vector3(System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4), System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 1), System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 2)), System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 3));
			}
		}

		public unsafe void SetShadowCullingPlane(int index, Plane plane)
		{
			if (index < 0 || index >= 6)
			{
				throw new IndexOutOfRangeException("Invalid plane index");
			}
			fixed (float* ptr = _shadowCullPlanes)
			{
				System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4) = plane.normal.x;
				System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 1) = plane.normal.y;
				System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 2) = plane.normal.z;
				System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 3) = plane.distance;
			}
		}

		public unsafe Plane GetCameraCullingPlane(int index)
		{
			if (index < 0 || index >= 6)
			{
				throw new IndexOutOfRangeException("Invalid plane index");
			}
			fixed (float* ptr = _cameraCullPlanes)
			{
				return new Plane(new Vector3(System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4), System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 1), System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 2)), System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 3));
			}
		}

		public unsafe void SetCameraCullingPlane(int index, Plane plane)
		{
			if (index < 0 || index >= 6)
			{
				throw new IndexOutOfRangeException("Invalid plane index");
			}
			fixed (float* ptr = _cameraCullPlanes)
			{
				System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4) = plane.normal.x;
				System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 1) = plane.normal.y;
				System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 2) = plane.normal.z;
				System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 3) = plane.distance;
			}
		}
	}
}
