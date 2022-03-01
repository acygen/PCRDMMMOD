using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Editor/Src/Properties/DrivenPropertyManager.h")]
	internal class DrivenPropertyManager
	{
		[Conditional("UNITY_EDITOR")]
		public static void RegisterProperty(Object driver, Object target, string propertyPath)
		{
			RegisterPropertyPartial(driver, target, propertyPath);
		}

		[Conditional("UNITY_EDITOR")]
		public static void UnregisterProperty(Object driver, Object target, string propertyPath)
		{
			UnregisterPropertyPartial(driver, target, propertyPath);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("UNITY_EDITOR")]
		[Conditional("UNITY_EDITOR")]
		[StaticAccessor("GetDrivenPropertyManager()", StaticAccessorType.Dot)]
		public static extern void UnregisterProperties([NotNull] Object driver);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("UNITY_EDITOR")]
		[StaticAccessor("GetDrivenPropertyManager()", StaticAccessorType.Dot)]
		private static extern void RegisterPropertyPartial([NotNull] Object driver, [NotNull] Object target, [NotNull] string propertyPath);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("UNITY_EDITOR")]
		[StaticAccessor("GetDrivenPropertyManager()", StaticAccessorType.Dot)]
		private static extern void UnregisterPropertyPartial([NotNull] Object driver, [NotNull] Object target, [NotNull] string propertyPath);
	}
}
