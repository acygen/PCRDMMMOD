using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h")]
	[RequiredByNativeCode]
	public sealed class Mesh : Object
	{
		internal enum InternalVertexChannelType
		{
			Float = 0,
			Color = 2
		}

		public extern IndexFormat indexFormat
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int vertexBufferCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction(Name = "MeshScripting::GetVertexBufferCount", HasExplicitThis = true)]
			get;
		}

		public extern int blendShapeCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "GetBlendShapeChannelCount")]
			get;
		}

		[NativeName("BoneWeightsFromScript")]
		public extern BoneWeight[] boneWeights
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("BindPosesFromScript")]
		public extern Matrix4x4[] bindposes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool isReadable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetIsReadable")]
			get;
		}

		internal extern bool canAccess
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("CanAccessFromScript")]
			get;
		}

		public extern int vertexCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetVertexCount")]
			get;
		}

		public extern int subMeshCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "GetSubMeshCount")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction(Name = "MeshScripting::SetSubMeshCount", HasExplicitThis = true)]
			set;
		}

		public Bounds bounds
		{
			get
			{
				get_bounds_Injected(out var ret);
				return ret;
			}
			set
			{
				set_bounds_Injected(ref value);
			}
		}

		public Vector3[] vertices
		{
			get
			{
				return GetAllocArrayFromChannel<Vector3>(VertexAttribute.Position);
			}
			set
			{
				SetArrayForChannel(VertexAttribute.Position, value);
			}
		}

		public Vector3[] normals
		{
			get
			{
				return GetAllocArrayFromChannel<Vector3>(VertexAttribute.Normal);
			}
			set
			{
				SetArrayForChannel(VertexAttribute.Normal, value);
			}
		}

		public Vector4[] tangents
		{
			get
			{
				return GetAllocArrayFromChannel<Vector4>(VertexAttribute.Tangent);
			}
			set
			{
				SetArrayForChannel(VertexAttribute.Tangent, value);
			}
		}

		public Vector2[] uv
		{
			get
			{
				return GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord0);
			}
			set
			{
				SetArrayForChannel(VertexAttribute.TexCoord0, value);
			}
		}

		public Vector2[] uv2
		{
			get
			{
				return GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord1);
			}
			set
			{
				SetArrayForChannel(VertexAttribute.TexCoord1, value);
			}
		}

		public Vector2[] uv3
		{
			get
			{
				return GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord2);
			}
			set
			{
				SetArrayForChannel(VertexAttribute.TexCoord2, value);
			}
		}

		public Vector2[] uv4
		{
			get
			{
				return GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord3);
			}
			set
			{
				SetArrayForChannel(VertexAttribute.TexCoord3, value);
			}
		}

		public Vector2[] uv5
		{
			get
			{
				return GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord4);
			}
			set
			{
				SetArrayForChannel(VertexAttribute.TexCoord4, value);
			}
		}

		public Vector2[] uv6
		{
			get
			{
				return GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord5);
			}
			set
			{
				SetArrayForChannel(VertexAttribute.TexCoord5, value);
			}
		}

		public Vector2[] uv7
		{
			get
			{
				return GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord6);
			}
			set
			{
				SetArrayForChannel(VertexAttribute.TexCoord6, value);
			}
		}

		public Vector2[] uv8
		{
			get
			{
				return GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord7);
			}
			set
			{
				SetArrayForChannel(VertexAttribute.TexCoord7, value);
			}
		}

		public Color[] colors
		{
			get
			{
				return GetAllocArrayFromChannel<Color>(VertexAttribute.Color);
			}
			set
			{
				SetArrayForChannel(VertexAttribute.Color, value);
			}
		}

		public Color32[] colors32
		{
			get
			{
				return GetAllocArrayFromChannel<Color32>(VertexAttribute.Color, InternalVertexChannelType.Color, 1);
			}
			set
			{
				SetArrayForChannel(VertexAttribute.Color, InternalVertexChannelType.Color, 1, value);
			}
		}

		public int[] triangles
		{
			get
			{
				if (canAccess)
				{
					return GetTrianglesImpl(-1, applyBaseVertex: true);
				}
				PrintErrorCantAccessIndices();
				return new int[0];
			}
			set
			{
				if (canAccess)
				{
					SetTrianglesImpl(-1, value, NoAllocHelpers.SafeLength(value), calculateBounds: true, 0);
				}
				else
				{
					PrintErrorCantAccessIndices();
				}
			}
		}

		[RequiredByNativeCode]
		public Mesh()
		{
			Internal_Create(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("MeshScripting::CreateMesh")]
		private static extern void Internal_Create([Writable] Mesh mono);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("MeshScripting::MeshFromInstanceId")]
		internal static extern Mesh FromInstanceID(int id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::GetIndexStart", HasExplicitThis = true)]
		private extern uint GetIndexStartImpl(int submesh);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::GetIndexCount", HasExplicitThis = true)]
		private extern uint GetIndexCountImpl(int submesh);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::GetBaseVertex", HasExplicitThis = true)]
		private extern uint GetBaseVertexImpl(int submesh);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::GetTriangles", HasExplicitThis = true)]
		private extern int[] GetTrianglesImpl(int submesh, bool applyBaseVertex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::GetIndices", HasExplicitThis = true)]
		private extern int[] GetIndicesImpl(int submesh, bool applyBaseVertex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "SetMeshIndicesFromScript", HasExplicitThis = true)]
		private extern void SetIndicesImpl(int submesh, MeshTopology topology, Array indices, int arraySize, bool calculateBounds, int baseVertex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::ExtractTrianglesToArray", HasExplicitThis = true)]
		private extern void GetTrianglesNonAllocImpl([Out] int[] values, int submesh, bool applyBaseVertex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::ExtractIndicesToArray", HasExplicitThis = true)]
		private extern void GetIndicesNonAllocImpl([Out] int[] values, int submesh, bool applyBaseVertex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::PrintErrorCantAccessChannel", HasExplicitThis = true)]
		private extern void PrintErrorCantAccessChannel(VertexAttribute ch);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::HasChannel", HasExplicitThis = true)]
		internal extern bool HasChannel(VertexAttribute ch);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "SetMeshComponentFromArrayFromScript", HasExplicitThis = true)]
		private extern void SetArrayForChannelImpl(VertexAttribute channel, InternalVertexChannelType format, int dim, Array values, int arraySize);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AllocExtractMeshComponentFromScript", HasExplicitThis = true)]
		private extern Array GetAllocArrayFromChannelImpl(VertexAttribute channel, InternalVertexChannelType format, int dim);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ExtractMeshComponentFromScript", HasExplicitThis = true)]
		private extern void GetArrayFromChannelImpl(VertexAttribute channel, InternalVertexChannelType format, int dim, Array values);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::GetNativeVertexBufferPtr", HasExplicitThis = true)]
		[NativeThrows]
		public extern IntPtr GetNativeVertexBufferPtr(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::GetNativeIndexBufferPtr", HasExplicitThis = true)]
		public extern IntPtr GetNativeIndexBufferPtr();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::ClearBlendShapes", HasExplicitThis = true)]
		public extern void ClearBlendShapes();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::GetBlendShapeName", HasExplicitThis = true)]
		public extern string GetBlendShapeName(int shapeIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::GetBlendShapeIndex", HasExplicitThis = true)]
		public extern int GetBlendShapeIndex(string blendShapeName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::GetBlendShapeFrameCount", HasExplicitThis = true)]
		public extern int GetBlendShapeFrameCount(int shapeIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::GetBlendShapeFrameWeight", HasExplicitThis = true)]
		public extern float GetBlendShapeFrameWeight(int shapeIndex, int frameIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "GetBlendShapeFrameVerticesFromScript", HasExplicitThis = true)]
		public extern void GetBlendShapeFrameVertices(int shapeIndex, int frameIndex, Vector3[] deltaVertices, Vector3[] deltaNormals, Vector3[] deltaTangents);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AddBlendShapeFrameFromScript", HasExplicitThis = true)]
		public extern void AddBlendShapeFrame(string shapeName, float frameWeight, Vector3[] deltaVertices, Vector3[] deltaNormals, Vector3[] deltaTangents);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetBoneWeightCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetBindposeCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::ExtractBoneWeightsIntoArray", HasExplicitThis = true)]
		private extern void GetBoneWeightsNonAllocImpl([Out] BoneWeight[] values);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::ExtractBindPosesIntoArray", HasExplicitThis = true)]
		private extern void GetBindposesNonAllocImpl([Out] Matrix4x4[] values);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("Clear")]
		private extern void ClearImpl(bool keepVertexLayout);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("RecalculateBounds")]
		private extern void RecalculateBoundsImpl();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("RecalculateNormals")]
		private extern void RecalculateNormalsImpl();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("RecalculateTangents")]
		private extern void RecalculateTangentsImpl();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("MarkDynamic")]
		private extern void MarkDynamicImpl();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("UploadMeshData")]
		private extern void UploadMeshDataImpl(bool markNoLongerReadable);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::GetPrimitiveType", HasExplicitThis = true)]
		private extern MeshTopology GetTopologyImpl(int submesh);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetMeshMetric")]
		public extern float GetUVDistributionMetric(int uvSetIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "MeshScripting::CombineMeshes", HasExplicitThis = true)]
		private extern void CombineMeshesImpl(CombineInstance[] combine, bool mergeSubMeshes, bool useMatrices, bool hasLightmapData);

		internal VertexAttribute GetUVChannel(int uvIndex)
		{
			if (uvIndex < 0 || uvIndex > 7)
			{
				throw new ArgumentException("GetUVChannel called for bad uvIndex", "uvIndex");
			}
			return (VertexAttribute)(4 + uvIndex);
		}

		internal static int DefaultDimensionForChannel(VertexAttribute channel)
		{
			switch (channel)
			{
			case VertexAttribute.Position:
			case VertexAttribute.Normal:
				return 3;
			case VertexAttribute.TexCoord0:
			case VertexAttribute.TexCoord1:
			case VertexAttribute.TexCoord2:
			case VertexAttribute.TexCoord3:
			case VertexAttribute.TexCoord4:
			case VertexAttribute.TexCoord5:
			case VertexAttribute.TexCoord6:
			case VertexAttribute.TexCoord7:
				return 2;
			default:
				if (channel == VertexAttribute.Tangent || channel == VertexAttribute.Color)
				{
					return 4;
				}
				throw new ArgumentException("DefaultDimensionForChannel called for bad channel", "channel");
			}
		}

		private T[] GetAllocArrayFromChannel<T>(VertexAttribute channel, InternalVertexChannelType format, int dim)
		{
			if (canAccess)
			{
				if (HasChannel(channel))
				{
					return (T[])GetAllocArrayFromChannelImpl(channel, format, dim);
				}
			}
			else
			{
				PrintErrorCantAccessChannel(channel);
			}
			return new T[0];
		}

		private T[] GetAllocArrayFromChannel<T>(VertexAttribute channel)
		{
			return GetAllocArrayFromChannel<T>(channel, InternalVertexChannelType.Float, DefaultDimensionForChannel(channel));
		}

		private void SetSizedArrayForChannel(VertexAttribute channel, InternalVertexChannelType format, int dim, Array values, int valuesCount)
		{
			if (canAccess)
			{
				SetArrayForChannelImpl(channel, format, dim, values, valuesCount);
			}
			else
			{
				PrintErrorCantAccessChannel(channel);
			}
		}

		private void SetArrayForChannel<T>(VertexAttribute channel, InternalVertexChannelType format, int dim, T[] values)
		{
			SetSizedArrayForChannel(channel, format, dim, values, NoAllocHelpers.SafeLength(values));
		}

		private void SetArrayForChannel<T>(VertexAttribute channel, T[] values)
		{
			SetSizedArrayForChannel(channel, InternalVertexChannelType.Float, DefaultDimensionForChannel(channel), values, NoAllocHelpers.SafeLength(values));
		}

		private void SetListForChannel<T>(VertexAttribute channel, InternalVertexChannelType format, int dim, List<T> values)
		{
			SetSizedArrayForChannel(channel, format, dim, NoAllocHelpers.ExtractArrayFromList(values), NoAllocHelpers.SafeLength(values));
		}

		private void SetListForChannel<T>(VertexAttribute channel, List<T> values)
		{
			SetSizedArrayForChannel(channel, InternalVertexChannelType.Float, DefaultDimensionForChannel(channel), NoAllocHelpers.ExtractArrayFromList(values), NoAllocHelpers.SafeLength(values));
		}

		private void GetListForChannel<T>(List<T> buffer, int capacity, VertexAttribute channel, int dim)
		{
			GetListForChannel(buffer, capacity, channel, dim, InternalVertexChannelType.Float);
		}

		private void GetListForChannel<T>(List<T> buffer, int capacity, VertexAttribute channel, int dim, InternalVertexChannelType channelType)
		{
			buffer.Clear();
			if (!canAccess)
			{
				PrintErrorCantAccessChannel(channel);
			}
			else if (HasChannel(channel))
			{
				NoAllocHelpers.EnsureListElemCount(buffer, capacity);
				GetArrayFromChannelImpl(channel, channelType, dim, NoAllocHelpers.ExtractArrayFromList(buffer));
			}
		}

		public void GetVertices(List<Vector3> vertices)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("The result vertices list cannot be null.", "vertices");
			}
			GetListForChannel(vertices, vertexCount, VertexAttribute.Position, DefaultDimensionForChannel(VertexAttribute.Position));
		}

		public void SetVertices(List<Vector3> inVertices)
		{
			SetListForChannel(VertexAttribute.Position, inVertices);
		}

		public void GetNormals(List<Vector3> normals)
		{
			if (normals == null)
			{
				throw new ArgumentNullException("The result normals list cannot be null.", "normals");
			}
			GetListForChannel(normals, vertexCount, VertexAttribute.Normal, DefaultDimensionForChannel(VertexAttribute.Normal));
		}

		public void SetNormals(List<Vector3> inNormals)
		{
			SetListForChannel(VertexAttribute.Normal, inNormals);
		}

		public void GetTangents(List<Vector4> tangents)
		{
			if (tangents == null)
			{
				throw new ArgumentNullException("The result tangents list cannot be null.", "tangents");
			}
			GetListForChannel(tangents, vertexCount, VertexAttribute.Tangent, DefaultDimensionForChannel(VertexAttribute.Tangent));
		}

		public void SetTangents(List<Vector4> inTangents)
		{
			SetListForChannel(VertexAttribute.Tangent, inTangents);
		}

		public void GetColors(List<Color> colors)
		{
			if (colors == null)
			{
				throw new ArgumentNullException("The result colors list cannot be null.", "colors");
			}
			GetListForChannel(colors, vertexCount, VertexAttribute.Color, DefaultDimensionForChannel(VertexAttribute.Color));
		}

		public void SetColors(List<Color> inColors)
		{
			SetListForChannel(VertexAttribute.Color, inColors);
		}

		public void GetColors(List<Color32> colors)
		{
			if (colors == null)
			{
				throw new ArgumentNullException("The result colors list cannot be null.", "colors");
			}
			GetListForChannel(colors, vertexCount, VertexAttribute.Color, 1, InternalVertexChannelType.Color);
		}

		public void SetColors(List<Color32> inColors)
		{
			SetListForChannel(VertexAttribute.Color, InternalVertexChannelType.Color, 1, inColors);
		}

		private void SetUvsImpl<T>(int uvIndex, int dim, List<T> uvs)
		{
			if (uvIndex < 0 || uvIndex > 7)
			{
				Debug.LogError("The uv index is invalid. Must be in the range 0 to 7.");
			}
			else
			{
				SetListForChannel(GetUVChannel(uvIndex), InternalVertexChannelType.Float, dim, uvs);
			}
		}

		public void SetUVs(int channel, List<Vector2> uvs)
		{
			SetUvsImpl(channel, 2, uvs);
		}

		public void SetUVs(int channel, List<Vector3> uvs)
		{
			SetUvsImpl(channel, 3, uvs);
		}

		public void SetUVs(int channel, List<Vector4> uvs)
		{
			SetUvsImpl(channel, 4, uvs);
		}

		private void GetUVsImpl<T>(int uvIndex, List<T> uvs, int dim)
		{
			if (uvs == null)
			{
				throw new ArgumentNullException("The result uvs list cannot be null.", "uvs");
			}
			if (uvIndex < 0 || uvIndex > 7)
			{
				throw new IndexOutOfRangeException("The uv index is invalid. Must be in the range 0 to 7.");
			}
			GetListForChannel(uvs, vertexCount, GetUVChannel(uvIndex), dim);
		}

		public void GetUVs(int channel, List<Vector2> uvs)
		{
			GetUVsImpl(channel, uvs, 2);
		}

		public void GetUVs(int channel, List<Vector3> uvs)
		{
			GetUVsImpl(channel, uvs, 3);
		}

		public void GetUVs(int channel, List<Vector4> uvs)
		{
			GetUVsImpl(channel, uvs, 4);
		}

		private void PrintErrorCantAccessIndices()
		{
			Debug.LogError($"Not allowed to access triangles/indices on mesh '{base.name}' (isReadable is false; Read/Write must be enabled in import settings)");
		}

		private bool CheckCanAccessSubmesh(int submesh, bool errorAboutTriangles)
		{
			if (!canAccess)
			{
				PrintErrorCantAccessIndices();
				return false;
			}
			if (submesh < 0 || submesh >= subMeshCount)
			{
				Debug.LogError(string.Format("Failed getting {0}. Submesh index is out of bounds.", (!errorAboutTriangles) ? "indices" : "triangles"), this);
				return false;
			}
			return true;
		}

		private bool CheckCanAccessSubmeshTriangles(int submesh)
		{
			return CheckCanAccessSubmesh(submesh, errorAboutTriangles: true);
		}

		private bool CheckCanAccessSubmeshIndices(int submesh)
		{
			return CheckCanAccessSubmesh(submesh, errorAboutTriangles: false);
		}

		public int[] GetTriangles(int submesh)
		{
			return GetTriangles(submesh, applyBaseVertex: true);
		}

		public int[] GetTriangles(int submesh, [UnityEngine.Internal.DefaultValue("true")] bool applyBaseVertex)
		{
			return (!CheckCanAccessSubmeshTriangles(submesh)) ? new int[0] : GetTrianglesImpl(submesh, applyBaseVertex);
		}

		public void GetTriangles(List<int> triangles, int submesh)
		{
			GetTriangles(triangles, submesh, applyBaseVertex: true);
		}

		public void GetTriangles(List<int> triangles, int submesh, [UnityEngine.Internal.DefaultValue("true")] bool applyBaseVertex)
		{
			if (triangles == null)
			{
				throw new ArgumentNullException("The result triangles list cannot be null.", "triangles");
			}
			if (submesh < 0 || submesh >= subMeshCount)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			NoAllocHelpers.EnsureListElemCount(triangles, (int)GetIndexCount(submesh));
			GetTrianglesNonAllocImpl(NoAllocHelpers.ExtractArrayFromListT(triangles), submesh, applyBaseVertex);
		}

		public int[] GetIndices(int submesh)
		{
			return GetIndices(submesh, applyBaseVertex: true);
		}

		public int[] GetIndices(int submesh, [UnityEngine.Internal.DefaultValue("true")] bool applyBaseVertex)
		{
			return (!CheckCanAccessSubmeshIndices(submesh)) ? new int[0] : GetIndicesImpl(submesh, applyBaseVertex);
		}

		public void GetIndices(List<int> indices, int submesh)
		{
			GetIndices(indices, submesh, applyBaseVertex: true);
		}

		public void GetIndices(List<int> indices, int submesh, [UnityEngine.Internal.DefaultValue("true")] bool applyBaseVertex)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("The result indices list cannot be null.", "indices");
			}
			if (submesh < 0 || submesh >= subMeshCount)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			NoAllocHelpers.EnsureListElemCount(indices, (int)GetIndexCount(submesh));
			GetIndicesNonAllocImpl(NoAllocHelpers.ExtractArrayFromListT(indices), submesh, applyBaseVertex);
		}

		public uint GetIndexStart(int submesh)
		{
			if (submesh < 0 || submesh >= subMeshCount)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			return GetIndexStartImpl(submesh);
		}

		public uint GetIndexCount(int submesh)
		{
			if (submesh < 0 || submesh >= subMeshCount)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			return GetIndexCountImpl(submesh);
		}

		public uint GetBaseVertex(int submesh)
		{
			if (submesh < 0 || submesh >= subMeshCount)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			return GetBaseVertexImpl(submesh);
		}

		private void SetTrianglesImpl(int submesh, Array triangles, int arraySize, bool calculateBounds, int baseVertex)
		{
			SetIndicesImpl(submesh, MeshTopology.Triangles, triangles, arraySize, calculateBounds, baseVertex);
		}

		public void SetTriangles(int[] triangles, int submesh)
		{
			SetTriangles(triangles, submesh, calculateBounds: true, 0);
		}

		public void SetTriangles(int[] triangles, int submesh, bool calculateBounds)
		{
			SetTriangles(triangles, submesh, calculateBounds, 0);
		}

		public void SetTriangles(int[] triangles, int submesh, [UnityEngine.Internal.DefaultValue("true")] bool calculateBounds, [UnityEngine.Internal.DefaultValue("0")] int baseVertex)
		{
			if (CheckCanAccessSubmeshTriangles(submesh))
			{
				SetTrianglesImpl(submesh, triangles, NoAllocHelpers.SafeLength(triangles), calculateBounds, baseVertex);
			}
		}

		public void SetTriangles(List<int> triangles, int submesh)
		{
			SetTriangles(triangles, submesh, calculateBounds: true, 0);
		}

		public void SetTriangles(List<int> triangles, int submesh, bool calculateBounds)
		{
			SetTriangles(triangles, submesh, calculateBounds, 0);
		}

		public void SetTriangles(List<int> triangles, int submesh, [UnityEngine.Internal.DefaultValue("true")] bool calculateBounds, [UnityEngine.Internal.DefaultValue("0")] int baseVertex)
		{
			if (CheckCanAccessSubmeshTriangles(submesh))
			{
				SetTrianglesImpl(submesh, NoAllocHelpers.ExtractArrayFromList(triangles), NoAllocHelpers.SafeLength(triangles), calculateBounds, baseVertex);
			}
		}

		public void SetIndices(int[] indices, MeshTopology topology, int submesh)
		{
			SetIndices(indices, topology, submesh, calculateBounds: true, 0);
		}

		public void SetIndices(int[] indices, MeshTopology topology, int submesh, bool calculateBounds)
		{
			SetIndices(indices, topology, submesh, calculateBounds, 0);
		}

		public void SetIndices(int[] indices, MeshTopology topology, int submesh, [UnityEngine.Internal.DefaultValue("true")] bool calculateBounds, [UnityEngine.Internal.DefaultValue("0")] int baseVertex)
		{
			if (CheckCanAccessSubmeshIndices(submesh))
			{
				SetIndicesImpl(submesh, topology, indices, NoAllocHelpers.SafeLength(indices), calculateBounds, baseVertex);
			}
		}

		public void GetBindposes(List<Matrix4x4> bindposes)
		{
			if (bindposes == null)
			{
				throw new ArgumentNullException("The result bindposes list cannot be null.", "bindposes");
			}
			NoAllocHelpers.EnsureListElemCount(bindposes, GetBindposeCount());
			GetBindposesNonAllocImpl(NoAllocHelpers.ExtractArrayFromListT(bindposes));
		}

		public void GetBoneWeights(List<BoneWeight> boneWeights)
		{
			if (boneWeights == null)
			{
				throw new ArgumentNullException("The result boneWeights list cannot be null.", "boneWeights");
			}
			NoAllocHelpers.EnsureListElemCount(boneWeights, GetBoneWeightCount());
			GetBoneWeightsNonAllocImpl(NoAllocHelpers.ExtractArrayFromListT(boneWeights));
		}

		public void Clear(bool keepVertexLayout)
		{
			ClearImpl(keepVertexLayout);
		}

		public void Clear()
		{
			ClearImpl(keepVertexLayout: true);
		}

		public void RecalculateBounds()
		{
			if (canAccess)
			{
				RecalculateBoundsImpl();
			}
			else
			{
				Debug.LogError($"Not allowed to call RecalculateBounds() on mesh '{base.name}'");
			}
		}

		public void RecalculateNormals()
		{
			if (canAccess)
			{
				RecalculateNormalsImpl();
			}
			else
			{
				Debug.LogError($"Not allowed to call RecalculateNormals() on mesh '{base.name}'");
			}
		}

		public void RecalculateTangents()
		{
			if (canAccess)
			{
				RecalculateTangentsImpl();
			}
			else
			{
				Debug.LogError($"Not allowed to call RecalculateTangents() on mesh '{base.name}'");
			}
		}

		public void MarkDynamic()
		{
			if (canAccess)
			{
				MarkDynamicImpl();
			}
		}

		public void UploadMeshData(bool markNoLongerReadable)
		{
			if (canAccess)
			{
				UploadMeshDataImpl(markNoLongerReadable);
			}
		}

		public MeshTopology GetTopology(int submesh)
		{
			if (submesh < 0 || submesh >= subMeshCount)
			{
				Debug.LogError($"Failed getting topology. Submesh index is out of bounds.", this);
				return MeshTopology.Triangles;
			}
			return GetTopologyImpl(submesh);
		}

		public void CombineMeshes(CombineInstance[] combine, [UnityEngine.Internal.DefaultValue("true")] bool mergeSubMeshes, [UnityEngine.Internal.DefaultValue("true")] bool useMatrices, [UnityEngine.Internal.DefaultValue("false")] bool hasLightmapData)
		{
			CombineMeshesImpl(combine, mergeSubMeshes, useMatrices, hasLightmapData);
		}

		public void CombineMeshes(CombineInstance[] combine, bool mergeSubMeshes, bool useMatrices)
		{
			CombineMeshesImpl(combine, mergeSubMeshes, useMatrices, hasLightmapData: false);
		}

		public void CombineMeshes(CombineInstance[] combine, bool mergeSubMeshes)
		{
			CombineMeshesImpl(combine, mergeSubMeshes, useMatrices: true, hasLightmapData: false);
		}

		public void CombineMeshes(CombineInstance[] combine)
		{
			CombineMeshesImpl(combine, mergeSubMeshes: true, useMatrices: true, hasLightmapData: false);
		}

		[Obsolete("This method is no longer supported (UnityUpgradable)", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Optimize()
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_bounds_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_bounds_Injected(ref Bounds value);
	}
}
