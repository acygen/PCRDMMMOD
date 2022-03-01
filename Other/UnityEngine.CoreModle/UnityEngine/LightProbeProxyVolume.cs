using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/Camera/LightProbeProxyVolume.h")]
	public sealed class LightProbeProxyVolume : Behaviour
	{
		public enum ResolutionMode
		{
			Automatic,
			Custom
		}

		public enum BoundingBoxMode
		{
			AutomaticLocal,
			AutomaticWorld,
			Custom
		}

		public enum ProbePositionMode
		{
			CellCorner,
			CellCenter
		}

		public enum RefreshMode
		{
			Automatic,
			EveryFrame,
			ViaScripting
		}

		public enum QualityMode
		{
			Low,
			Normal
		}

		public static extern bool isFeatureSupported
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("IsFeatureSupported")]
			get;
		}

		[NativeName("GlobalAABB")]
		public Bounds boundsGlobal
		{
			get
			{
				get_boundsGlobal_Injected(out var ret);
				return ret;
			}
		}

		[NativeName("BoundingBoxSizeCustom")]
		public Vector3 sizeCustom
		{
			get
			{
				get_sizeCustom_Injected(out var ret);
				return ret;
			}
			set
			{
				set_sizeCustom_Injected(ref value);
			}
		}

		[NativeName("BoundingBoxOriginCustom")]
		public Vector3 originCustom
		{
			get
			{
				get_originCustom_Injected(out var ret);
				return ret;
			}
			set
			{
				set_originCustom_Injected(ref value);
			}
		}

		public extern float probeDensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int gridResolutionX
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int gridResolutionY
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int gridResolutionZ
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern BoundingBoxMode boundingBoxMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ResolutionMode resolutionMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ProbePositionMode probePositionMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern RefreshMode refreshMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern QualityMode qualityMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public void Update()
		{
			SetDirtyFlag(flag: true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetDirtyFlag(bool flag);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_boundsGlobal_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_sizeCustom_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_sizeCustom_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_originCustom_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_originCustom_Injected(ref Vector3 value);
	}
}
