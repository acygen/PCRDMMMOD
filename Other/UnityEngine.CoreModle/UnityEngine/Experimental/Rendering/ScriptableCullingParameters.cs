using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	[UsedByNativeCode]
	public struct ScriptableCullingParameters
	{
		private int m_IsOrthographic;

		private LODParameters m_LodParameters;

		internal unsafe fixed float m_CullingPlanes[40];

		private int m_CullingPlaneCount;

		private int m_CullingMask;

		private long m_SceneMask;

		internal unsafe fixed float m_LayerFarCullDistances[32];

		private int m_LayerCull;

		private Matrix4x4 m_CullingMatrix;

		private Vector3 m_Position;

		private float m_shadowDistance;

		private CullFlag m_CullingFlags;

		private ReflectionProbeSortOptions m_ReflectionProbeSortOptions;

		private CameraProperties m_CameraProperties;

		private float m_AccurateoOcclusionThreshold;

		public Matrix4x4 cullStereoView;

		public Matrix4x4 cullStereoProj;

		public float cullStereoSeparation;

		private int padding2;

		public int cullingPlaneCount
		{
			get
			{
				return m_CullingPlaneCount;
			}
			set
			{
				if (value < 0 || value > 10)
				{
					throw new IndexOutOfRangeException("Invalid plane count (0 <= count <= 10)");
				}
				m_CullingPlaneCount = value;
			}
		}

		public bool isOrthographic
		{
			get
			{
				return Convert.ToBoolean(m_IsOrthographic);
			}
			set
			{
				m_IsOrthographic = Convert.ToInt32(value);
			}
		}

		public LODParameters lodParameters
		{
			get
			{
				return m_LodParameters;
			}
			set
			{
				m_LodParameters = value;
			}
		}

		public int cullingMask
		{
			get
			{
				return m_CullingMask;
			}
			set
			{
				m_CullingMask = value;
			}
		}

		public long sceneMask
		{
			get
			{
				return m_SceneMask;
			}
			set
			{
				m_SceneMask = value;
			}
		}

		public int layerCull
		{
			get
			{
				return m_LayerCull;
			}
			set
			{
				m_LayerCull = value;
			}
		}

		public Matrix4x4 cullingMatrix
		{
			get
			{
				return m_CullingMatrix;
			}
			set
			{
				m_CullingMatrix = value;
			}
		}

		public Vector3 position
		{
			get
			{
				return m_Position;
			}
			set
			{
				m_Position = value;
			}
		}

		public float shadowDistance
		{
			get
			{
				return m_shadowDistance;
			}
			set
			{
				m_shadowDistance = value;
			}
		}

		public CullFlag cullingFlags
		{
			get
			{
				return m_CullingFlags;
			}
			set
			{
				m_CullingFlags = value;
			}
		}

		public ReflectionProbeSortOptions reflectionProbeSortOptions
		{
			get
			{
				return m_ReflectionProbeSortOptions;
			}
			set
			{
				m_ReflectionProbeSortOptions = value;
			}
		}

		public CameraProperties cameraProperties
		{
			get
			{
				return m_CameraProperties;
			}
			set
			{
				m_CameraProperties = value;
			}
		}

		public float accurateOcclusionThreshold
		{
			get
			{
				return m_AccurateoOcclusionThreshold;
			}
			set
			{
				m_AccurateoOcclusionThreshold = Mathf.Max(-1f, value);
			}
		}

		public unsafe float GetLayerCullDistance(int layerIndex)
		{
			if (layerIndex < 0 || layerIndex >= 32)
			{
				throw new IndexOutOfRangeException("Invalid layer index");
			}
			fixed (float* ptr = m_LayerFarCullDistances)
			{
				return System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, layerIndex);
			}
		}

		public unsafe void SetLayerCullDistance(int layerIndex, float distance)
		{
			if (layerIndex < 0 || layerIndex >= 32)
			{
				throw new IndexOutOfRangeException("Invalid layer index");
			}
			fixed (float* ptr = m_LayerFarCullDistances)
			{
				System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, layerIndex) = distance;
			}
		}

		public unsafe Plane GetCullingPlane(int index)
		{
			if (index < 0 || index >= cullingPlaneCount || index >= 10)
			{
				throw new IndexOutOfRangeException("Invalid plane index");
			}
			fixed (float* ptr = m_CullingPlanes)
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
			fixed (float* ptr = m_CullingPlanes)
			{
				System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4) = plane.normal.x;
				System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 1) = plane.normal.y;
				System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 2) = plane.normal.z;
				System.Runtime.CompilerServices.Unsafe.Add(ref *ptr, index * 4 + 3) = plane.distance;
			}
		}
	}
}
