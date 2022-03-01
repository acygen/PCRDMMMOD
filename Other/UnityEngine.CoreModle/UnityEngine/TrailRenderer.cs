using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Graphics/TrailRenderer.h")]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public sealed class TrailRenderer : Renderer
	{
		[Obsolete("Use positionCount instead (UnityUpgradable) -> positionCount", false)]
		public int numPositions => positionCount;

		public extern float time
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float startWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float endWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float widthMultiplier
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool autodestruct
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool emitting
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int numCornerVertices
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int numCapVertices
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float minVertexDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Color startColor
		{
			get
			{
				get_startColor_Injected(out var ret);
				return ret;
			}
			set
			{
				set_startColor_Injected(ref value);
			}
		}

		public Color endColor
		{
			get
			{
				get_endColor_Injected(out var ret);
				return ret;
			}
			set
			{
				set_endColor_Injected(ref value);
			}
		}

		[NativeProperty("PositionsCount")]
		public extern int positionCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float shadowBias
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool generateLightingData
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern LineTextureMode textureMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern LineAlignment alignment
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public AnimationCurve widthCurve
		{
			get
			{
				return GetWidthCurveCopy();
			}
			set
			{
				SetWidthCurve(value);
			}
		}

		public Gradient colorGradient
		{
			get
			{
				return GetColorGradientCopy();
			}
			set
			{
				SetColorGradient(value);
			}
		}

		public void SetPosition(int index, Vector3 position)
		{
			SetPosition_Injected(index, ref position);
		}

		public Vector3 GetPosition(int index)
		{
			GetPosition_Injected(index, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Clear();

		public void BakeMesh(Mesh mesh, bool useTransform = false)
		{
			BakeMesh(mesh, Camera.main, useTransform);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void BakeMesh([NotNull] Mesh mesh, [NotNull] Camera camera, bool useTransform = false);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationCurve GetWidthCurveCopy();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetWidthCurve([NotNull] AnimationCurve curve);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Gradient GetColorGradientCopy();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetColorGradient([NotNull] Gradient curve);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "TrailRendererScripting::GetPositions", HasExplicitThis = true)]
		public extern int GetPositions([Out][NotNull] Vector3[] positions);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "TrailRendererScripting::SetPositions", HasExplicitThis = true)]
		public extern void SetPositions([NotNull] Vector3[] positions);

		[FreeFunction(Name = "TrailRendererScripting::AddPosition", HasExplicitThis = true)]
		public void AddPosition(Vector3 position)
		{
			AddPosition_Injected(ref position);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "TrailRendererScripting::AddPositions", HasExplicitThis = true)]
		public extern void AddPositions([NotNull] Vector3[] positions);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_startColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_startColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_endColor_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_endColor_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPosition_Injected(int index, ref Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPosition_Injected(int index, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddPosition_Injected(ref Vector3 position);
	}
}
