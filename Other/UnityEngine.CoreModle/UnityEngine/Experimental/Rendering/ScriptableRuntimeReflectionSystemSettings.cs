using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	[NativeHeader("Runtime/Camera/ScriptableRuntimeReflectionSystem.h")]
	[RequiredByNativeCode]
	public static class ScriptableRuntimeReflectionSystemSettings
	{
		private static ScriptableRuntimeReflectionSystemWrapper s_Instance = new ScriptableRuntimeReflectionSystemWrapper();

		public static IScriptableRuntimeReflectionSystem system
		{
			get
			{
				return Internal_ScriptableRuntimeReflectionSystemSettings_system;
			}
			set
			{
				if (value == null || value.Equals(null))
				{
					Debug.LogError("'null' cannot be assigned to ScriptableRuntimeReflectionSystemSettings.system");
					return;
				}
				if (!(system is BuiltinRuntimeReflectionSystem) && !(value is BuiltinRuntimeReflectionSystem) && system != value)
				{
					Debug.LogWarningFormat("ScriptableRuntimeReflectionSystemSettings.system is assigned more than once. Only a the last instance will be used. (Last instance {0}, New instance {1})", system, value);
				}
				Internal_ScriptableRuntimeReflectionSystemSettings_system = value;
			}
		}

		private static IScriptableRuntimeReflectionSystem Internal_ScriptableRuntimeReflectionSystemSettings_system
		{
			get
			{
				return s_Instance.implementation;
			}
			[RequiredByNativeCode]
			set
			{
				if (s_Instance.implementation != value && s_Instance.implementation != null)
				{
					s_Instance.implementation.Dispose();
				}
				s_Instance.implementation = value;
			}
		}

		private static ScriptableRuntimeReflectionSystemWrapper Internal_ScriptableRuntimeReflectionSystemSettings_instance
		{
			[RequiredByNativeCode]
			get
			{
				return s_Instance;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("ScriptableRuntimeReflectionSystem", StaticAccessorType.DoubleColon)]
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static extern void ScriptingDirtyReflectionSystemInstance();
	}
}
