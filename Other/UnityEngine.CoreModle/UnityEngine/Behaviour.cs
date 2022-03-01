using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	[UsedByNativeCode]
	public class Behaviour : Component
	{
		[RequiredByNativeCode]
		[NativeProperty]
		public extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty]
		public extern bool isActiveAndEnabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("IsAddedToManager")]
			get;
		}
	}
}
