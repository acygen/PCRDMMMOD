using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	[NativeHeader("Runtime/Camera/GraphicsSettings.h")]
	[StaticAccessor("GetGraphicsSettings()", StaticAccessorType.Dot)]
	public sealed class GraphicsSettings : Object
	{
		public static extern TransparencySortMode transparencySortMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static Vector3 transparencySortAxis
		{
			get
			{
				get_transparencySortAxis_Injected(out var ret);
				return ret;
			}
			set
			{
				set_transparencySortAxis_Injected(ref value);
			}
		}

		public static extern bool lightsUseLinearIntensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool lightsUseColorTemperature
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool useScriptableRenderPipelineBatching
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool logWhenShaderIsCompiled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("RenderPipeline")]
		private static extern ScriptableObject INTERNAL_renderPipelineAsset
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static RenderPipelineAsset renderPipelineAsset
		{
			get
			{
				return INTERNAL_renderPipelineAsset as RenderPipelineAsset;
			}
			set
			{
				INTERNAL_renderPipelineAsset = value;
			}
		}

		private GraphicsSettings()
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasShaderDefine(GraphicsTier tier, BuiltinShaderDefine defineHash);

		public static bool HasShaderDefine(BuiltinShaderDefine defineHash)
		{
			return HasShaderDefine(Graphics.activeTier, defineHash);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		internal static extern Object GetGraphicsSettings();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("SetShaderModeScript")]
		public static extern void SetShaderMode(BuiltinShaderType type, BuiltinShaderMode mode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetShaderModeScript")]
		public static extern BuiltinShaderMode GetShaderMode(BuiltinShaderType type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("SetCustomShaderScript")]
		public static extern void SetCustomShader(BuiltinShaderType type, Shader shader);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetCustomShaderScript")]
		public static extern Shader GetCustomShader(BuiltinShaderType type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void get_transparencySortAxis_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void set_transparencySortAxis_Injected(ref Vector3 value);
	}
}
