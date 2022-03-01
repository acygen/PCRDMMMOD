using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	[Serializable]
	internal class PersistentCallGroup
	{
		[SerializeField]
		[FormerlySerializedAs("m_Listeners")]
		private List<PersistentCall> m_Calls;

		public int Count => m_Calls.Count;

		public PersistentCallGroup()
		{
			m_Calls = new List<PersistentCall>();
		}

		public PersistentCall GetListener(int index)
		{
			return m_Calls[index];
		}

		public IEnumerable<PersistentCall> GetListeners()
		{
			return m_Calls;
		}

		public void AddListener()
		{
			m_Calls.Add(new PersistentCall());
		}

		public void AddListener(PersistentCall call)
		{
			m_Calls.Add(call);
		}

		public void RemoveListener(int index)
		{
			m_Calls.RemoveAt(index);
		}

		public void Clear()
		{
			m_Calls.Clear();
		}

		public void RegisterEventPersistentListener(int index, Object targetObj, string methodName)
		{
			PersistentCall listener = GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.EventDefined;
		}

		public void RegisterVoidPersistentListener(int index, Object targetObj, string methodName)
		{
			PersistentCall listener = GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.Void;
		}

		public void RegisterObjectPersistentListener(int index, Object targetObj, Object argument, string methodName)
		{
			PersistentCall listener = GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.Object;
			listener.arguments.unityObjectArgument = argument;
		}

		public void RegisterIntPersistentListener(int index, Object targetObj, int argument, string methodName)
		{
			PersistentCall listener = GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.Int;
			listener.arguments.intArgument = argument;
		}

		public void RegisterFloatPersistentListener(int index, Object targetObj, float argument, string methodName)
		{
			PersistentCall listener = GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.Float;
			listener.arguments.floatArgument = argument;
		}

		public void RegisterStringPersistentListener(int index, Object targetObj, string argument, string methodName)
		{
			PersistentCall listener = GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.String;
			listener.arguments.stringArgument = argument;
		}

		public void RegisterBoolPersistentListener(int index, Object targetObj, bool argument, string methodName)
		{
			PersistentCall listener = GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.Bool;
			listener.arguments.boolArgument = argument;
		}

		public void UnregisterPersistentListener(int index)
		{
			PersistentCall listener = GetListener(index);
			listener.UnregisterPersistentListener();
		}

		public void RemoveListeners(Object target, string methodName)
		{
			List<PersistentCall> list = new List<PersistentCall>();
			for (int i = 0; i < m_Calls.Count; i++)
			{
				if (m_Calls[i].target == target && m_Calls[i].methodName == methodName)
				{
					list.Add(m_Calls[i]);
				}
			}
			m_Calls.RemoveAll(list.Contains);
		}

		public void Initialize(InvokableCallList invokableList, UnityEventBase unityEventBase)
		{
			foreach (PersistentCall call in m_Calls)
			{
				if (call.IsValid())
				{
					BaseInvokableCall runtimeCall = call.GetRuntimeCall(unityEventBase);
					if (runtimeCall != null)
					{
						invokableList.AddPersistentInvokableCall(runtimeCall);
					}
				}
			}
		}
	}
}
