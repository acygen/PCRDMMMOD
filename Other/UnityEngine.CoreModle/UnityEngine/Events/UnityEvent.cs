using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine.Events
{
	[Serializable]
	public class UnityEvent : UnityEventBase
	{
		private object[] m_InvokeArray = null;

		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		public void AddListener(UnityAction call)
		{
			AddCall(GetDelegate(call));
		}

		public void RemoveListener(UnityAction call)
		{
			RemoveListener(call.Target, NetFxCoreExtensions.GetMethodInfo(call));
		}

		protected override MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return UnityEventBase.GetValidMethodInfo(targetObj, name, new Type[0]);
		}

		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall(target, theFunction);
		}

		private static BaseInvokableCall GetDelegate(UnityAction action)
		{
			return new InvokableCall(action);
		}

		public void Invoke()
		{
			List<BaseInvokableCall> list = PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				InvokableCall invokableCall = list[i] as InvokableCall;
				if (invokableCall != null)
				{
					invokableCall.Invoke();
					continue;
				}
				InvokableCall invokableCall2 = list[i] as InvokableCall;
				if (invokableCall2 != null)
				{
					invokableCall2.Invoke();
					continue;
				}
				BaseInvokableCall baseInvokableCall = list[i];
				if (m_InvokeArray == null)
				{
					m_InvokeArray = new object[0];
				}
				baseInvokableCall.Invoke(m_InvokeArray);
			}
		}
	}
	[Serializable]
	public abstract class UnityEvent<T0> : UnityEventBase
	{
		private object[] m_InvokeArray = null;

		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		public void AddListener(UnityAction<T0> call)
		{
			AddCall(GetDelegate(call));
		}

		public void RemoveListener(UnityAction<T0> call)
		{
			RemoveListener(call.Target, NetFxCoreExtensions.GetMethodInfo(call));
		}

		protected override MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return UnityEventBase.GetValidMethodInfo(targetObj, name, new Type[1] { typeof(T0) });
		}

		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0>(target, theFunction);
		}

		private static BaseInvokableCall GetDelegate(UnityAction<T0> action)
		{
			return new InvokableCall<T0>(action);
		}

		public void Invoke(T0 arg0)
		{
			List<BaseInvokableCall> list = PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				InvokableCall<T0> invokableCall = list[i] as InvokableCall<T0>;
				if (invokableCall != null)
				{
					invokableCall.Invoke(arg0);
					continue;
				}
				InvokableCall invokableCall2 = list[i] as InvokableCall;
				if (invokableCall2 != null)
				{
					invokableCall2.Invoke();
					continue;
				}
				BaseInvokableCall baseInvokableCall = list[i];
				if (m_InvokeArray == null)
				{
					m_InvokeArray = new object[1];
				}
				m_InvokeArray[0] = arg0;
				baseInvokableCall.Invoke(m_InvokeArray);
			}
		}
	}
	[Serializable]
	public abstract class UnityEvent<T0, T1> : UnityEventBase
	{
		private object[] m_InvokeArray = null;

		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		public void AddListener(UnityAction<T0, T1> call)
		{
			AddCall(GetDelegate(call));
		}

		public void RemoveListener(UnityAction<T0, T1> call)
		{
			RemoveListener(call.Target, NetFxCoreExtensions.GetMethodInfo(call));
		}

		protected override MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return UnityEventBase.GetValidMethodInfo(targetObj, name, new Type[2]
			{
				typeof(T0),
				typeof(T1)
			});
		}

		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0, T1>(target, theFunction);
		}

		private static BaseInvokableCall GetDelegate(UnityAction<T0, T1> action)
		{
			return new InvokableCall<T0, T1>(action);
		}

		public void Invoke(T0 arg0, T1 arg1)
		{
			List<BaseInvokableCall> list = PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				InvokableCall<T0, T1> invokableCall = list[i] as InvokableCall<T0, T1>;
				if (invokableCall != null)
				{
					invokableCall.Invoke(arg0, arg1);
					continue;
				}
				InvokableCall invokableCall2 = list[i] as InvokableCall;
				if (invokableCall2 != null)
				{
					invokableCall2.Invoke();
					continue;
				}
				BaseInvokableCall baseInvokableCall = list[i];
				if (m_InvokeArray == null)
				{
					m_InvokeArray = new object[2];
				}
				m_InvokeArray[0] = arg0;
				m_InvokeArray[1] = arg1;
				baseInvokableCall.Invoke(m_InvokeArray);
			}
		}
	}
	[Serializable]
	public abstract class UnityEvent<T0, T1, T2> : UnityEventBase
	{
		private object[] m_InvokeArray = null;

		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		public void AddListener(UnityAction<T0, T1, T2> call)
		{
			AddCall(GetDelegate(call));
		}

		public void RemoveListener(UnityAction<T0, T1, T2> call)
		{
			RemoveListener(call.Target, NetFxCoreExtensions.GetMethodInfo(call));
		}

		protected override MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return UnityEventBase.GetValidMethodInfo(targetObj, name, new Type[3]
			{
				typeof(T0),
				typeof(T1),
				typeof(T2)
			});
		}

		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0, T1, T2>(target, theFunction);
		}

		private static BaseInvokableCall GetDelegate(UnityAction<T0, T1, T2> action)
		{
			return new InvokableCall<T0, T1, T2>(action);
		}

		public void Invoke(T0 arg0, T1 arg1, T2 arg2)
		{
			List<BaseInvokableCall> list = PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				InvokableCall<T0, T1, T2> invokableCall = list[i] as InvokableCall<T0, T1, T2>;
				if (invokableCall != null)
				{
					invokableCall.Invoke(arg0, arg1, arg2);
					continue;
				}
				InvokableCall invokableCall2 = list[i] as InvokableCall;
				if (invokableCall2 != null)
				{
					invokableCall2.Invoke();
					continue;
				}
				BaseInvokableCall baseInvokableCall = list[i];
				if (m_InvokeArray == null)
				{
					m_InvokeArray = new object[3];
				}
				m_InvokeArray[0] = arg0;
				m_InvokeArray[1] = arg1;
				m_InvokeArray[2] = arg2;
				baseInvokableCall.Invoke(m_InvokeArray);
			}
		}
	}
	[Serializable]
	public abstract class UnityEvent<T0, T1, T2, T3> : UnityEventBase
	{
		private object[] m_InvokeArray = null;

		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		public void AddListener(UnityAction<T0, T1, T2, T3> call)
		{
			AddCall(GetDelegate(call));
		}

		public void RemoveListener(UnityAction<T0, T1, T2, T3> call)
		{
			RemoveListener(call.Target, NetFxCoreExtensions.GetMethodInfo(call));
		}

		protected override MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return UnityEventBase.GetValidMethodInfo(targetObj, name, new Type[4]
			{
				typeof(T0),
				typeof(T1),
				typeof(T2),
				typeof(T3)
			});
		}

		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0, T1, T2, T3>(target, theFunction);
		}

		private static BaseInvokableCall GetDelegate(UnityAction<T0, T1, T2, T3> action)
		{
			return new InvokableCall<T0, T1, T2, T3>(action);
		}

		public void Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
			List<BaseInvokableCall> list = PrepareInvoke();
			for (int i = 0; i < list.Count; i++)
			{
				InvokableCall<T0, T1, T2, T3> invokableCall = list[i] as InvokableCall<T0, T1, T2, T3>;
				if (invokableCall != null)
				{
					invokableCall.Invoke(arg0, arg1, arg2, arg3);
					continue;
				}
				InvokableCall invokableCall2 = list[i] as InvokableCall;
				if (invokableCall2 != null)
				{
					invokableCall2.Invoke();
					continue;
				}
				BaseInvokableCall baseInvokableCall = list[i];
				if (m_InvokeArray == null)
				{
					m_InvokeArray = new object[4];
				}
				m_InvokeArray[0] = arg0;
				m_InvokeArray[1] = arg1;
				m_InvokeArray[2] = arg2;
				m_InvokeArray[3] = arg3;
				baseInvokableCall.Invoke(m_InvokeArray);
			}
		}
	}
}
