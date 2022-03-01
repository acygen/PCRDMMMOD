using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	[RequiredByNativeCode]
	[ThreadAndSerializationSafe]
	[NativeHeader("Runtime/Export/AsyncOperation.bindings.h")]
	[NativeHeader("Runtime/Misc/AsyncOperation.h")]
	public class AsyncOperation : YieldInstruction
	{
		internal IntPtr m_Ptr;

		private Action<AsyncOperation> m_completeCallback;

		public extern bool isDone
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("IsDone")]
			get;
		}

		public extern float progress
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetProgress")]
			get;
		}

		public extern int priority
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetPriority")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("SetPriority")]
			set;
		}

		public extern bool allowSceneActivation
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetAllowSceneActivation")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("SetAllowSceneActivation")]
			set;
		}

		public event Action<AsyncOperation> completed
		{
			add
			{
				if (isDone)
				{
					value(this);
				}
				else
				{
					m_completeCallback = (Action<AsyncOperation>)Delegate.Combine(m_completeCallback, value);
				}
			}
			remove
			{
				m_completeCallback = (Action<AsyncOperation>)Delegate.Remove(m_completeCallback, value);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("AsyncOperationBindings", StaticAccessorType.DoubleColon)]
		[NativeMethod(IsThreadSafe = true)]
		private static extern void InternalDestroy(IntPtr ptr);

		~AsyncOperation()
		{
			InternalDestroy(m_Ptr);
		}

		[RequiredByNativeCode]
		internal void InvokeCompletionEvent()
		{
			if (m_completeCallback != null)
			{
				m_completeCallback(this);
				m_completeCallback = null;
			}
		}
	}
}
