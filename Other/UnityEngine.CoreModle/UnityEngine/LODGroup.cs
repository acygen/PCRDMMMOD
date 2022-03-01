using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[StaticAccessor("GetLODGroupManager()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Graphics/LOD/LODUtility.h")]
	[NativeHeader("Runtime/Graphics/LOD/LODGroupManager.h")]
	[NativeHeader("Runtime/Graphics/LOD/LODGroup.h")]
	public class LODGroup : Component
	{
		public Vector3 localReferencePoint
		{
			get
			{
				get_localReferencePoint_Injected(out var ret);
				return ret;
			}
			set
			{
				set_localReferencePoint_Injected(ref value);
			}
		}

		public extern float size
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int lodCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetLODCount")]
			get;
		}

		public extern LODFadeMode fadeMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool animateCrossFading
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetLODGroupManager()")]
		public static extern float crossFadeAnimationDuration
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("UpdateLODGroupBoundingBox", HasExplicitThis = true)]
		public extern void RecalculateBounds();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetLODs_Binding", HasExplicitThis = true)]
		public extern LOD[] GetLODs();

		[Obsolete("Use SetLODs instead.")]
		public void SetLODS(LOD[] lods)
		{
			SetLODs(lods);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("SetLODs_Binding", HasExplicitThis = true)]
		public extern void SetLODs(LOD[] lods);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ForceLODLevel", HasExplicitThis = true)]
		public extern void ForceLOD(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_localReferencePoint_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_localReferencePoint_Injected(ref Vector3 value);
	}
}
