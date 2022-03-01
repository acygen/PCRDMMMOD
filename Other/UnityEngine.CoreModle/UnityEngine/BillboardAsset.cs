using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/Billboard/BillboardAsset.h")]
	[NativeHeader("Runtime/Export/BillboardRenderer.bindings.h")]
	public sealed class BillboardAsset : Object
	{
		public extern float width
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float height
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float bottom
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int imageCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetNumImages")]
			get;
		}

		public extern int vertexCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetNumVertices")]
			get;
		}

		public extern int indexCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetNumIndices")]
			get;
		}

		public extern Material material
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public BillboardAsset()
		{
			Internal_Create(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "BillboardRenderer_Bindings::Internal_Create")]
		private static extern void Internal_Create([Writable] BillboardAsset obj);

		public void GetImageTexCoords(List<Vector4> imageTexCoords)
		{
			if (imageTexCoords == null)
			{
				throw new ArgumentNullException("imageTexCoords");
			}
			GetImageTexCoordsInternal(imageTexCoords);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetBillboardDataReadonly().GetImageTexCoords")]
		public extern Vector4[] GetImageTexCoords();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "BillboardRenderer_Bindings::GetImageTexCoordsInternal", HasExplicitThis = true)]
		internal extern void GetImageTexCoordsInternal(object list);

		public void SetImageTexCoords(List<Vector4> imageTexCoords)
		{
			if (imageTexCoords == null)
			{
				throw new ArgumentNullException("imageTexCoords");
			}
			SetImageTexCoordsInternalList(imageTexCoords);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetImageTexCoords", HasExplicitThis = true)]
		public extern void SetImageTexCoords([NotNull] Vector4[] imageTexCoords);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetImageTexCoordsInternalList", HasExplicitThis = true)]
		internal extern void SetImageTexCoordsInternalList(object list);

		public void GetVertices(List<Vector2> vertices)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("vertices");
			}
			GetVerticesInternal(vertices);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetBillboardDataReadonly().GetVertices")]
		public extern Vector2[] GetVertices();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "BillboardRenderer_Bindings::GetVerticesInternal", HasExplicitThis = true)]
		internal extern void GetVerticesInternal(object list);

		public void SetVertices(List<Vector2> vertices)
		{
			if (vertices == null)
			{
				throw new ArgumentNullException("vertices");
			}
			SetVerticesInternalList(vertices);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetVertices", HasExplicitThis = true)]
		public extern void SetVertices([NotNull] Vector2[] vertices);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetVerticesInternalList", HasExplicitThis = true)]
		internal extern void SetVerticesInternalList(object list);

		public void GetIndices(List<ushort> indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			GetIndicesInternal(indices);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetBillboardDataReadonly().GetIndices")]
		public extern ushort[] GetIndices();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "BillboardRenderer_Bindings::GetIndicesInternal", HasExplicitThis = true)]
		internal extern void GetIndicesInternal(object list);

		public void SetIndices(List<ushort> indices)
		{
			if (indices == null)
			{
				throw new ArgumentNullException("indices");
			}
			SetIndicesInternalList(indices);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetIndices", HasExplicitThis = true)]
		public extern void SetIndices([NotNull] ushort[] indices);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "BillboardRenderer_Bindings::SetIndicesInternalList", HasExplicitThis = true)]
		internal extern void SetIndicesInternalList(object list);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "BillboardRenderer_Bindings::MakeMaterialProperties", HasExplicitThis = true)]
		internal extern void MakeMaterialProperties(MaterialPropertyBlock properties, Camera camera);
	}
}
