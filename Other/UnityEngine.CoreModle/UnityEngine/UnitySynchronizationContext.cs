using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine.Scripting;

namespace UnityEngine
{
	internal sealed class UnitySynchronizationContext : SynchronizationContext
	{
		private struct WorkRequest
		{
			private readonly SendOrPostCallback m_DelagateCallback;

			private readonly object m_DelagateState;

			private readonly ManualResetEvent m_WaitHandle;

			public WorkRequest(SendOrPostCallback callback, object state, ManualResetEvent waitHandle = null)
			{
				m_DelagateCallback = callback;
				m_DelagateState = state;
				m_WaitHandle = waitHandle;
			}

			public void Invoke()
			{
				try
				{
					m_DelagateCallback(m_DelagateState);
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
				if (m_WaitHandle != null)
				{
					m_WaitHandle.Set();
				}
			}
		}

		private const int kAwqInitialCapacity = 20;

		private readonly List<WorkRequest> m_AsyncWorkQueue;

		private readonly List<WorkRequest> m_CurrentFrameWork = new List<WorkRequest>(20);

		private readonly int m_MainThreadID;

		private int m_TrackedCount = 0;

		private UnitySynchronizationContext(int mainThreadID)
		{
			m_AsyncWorkQueue = new List<WorkRequest>(20);
			m_MainThreadID = mainThreadID;
		}

		private UnitySynchronizationContext(List<WorkRequest> queue, int mainThreadID)
		{
			m_AsyncWorkQueue = queue;
			m_MainThreadID = mainThreadID;
		}

		public override void Send(SendOrPostCallback callback, object state)
		{
			if (m_MainThreadID == Thread.CurrentThread.ManagedThreadId)
			{
				callback(state);
				return;
			}
			using ManualResetEvent manualResetEvent = new ManualResetEvent(initialState: false);
			lock (m_AsyncWorkQueue)
			{
				m_AsyncWorkQueue.Add(new WorkRequest(callback, state, manualResetEvent));
			}
			manualResetEvent.WaitOne();
		}

		public override void OperationStarted()
		{
			Interlocked.Increment(ref m_TrackedCount);
		}

		public override void OperationCompleted()
		{
			Interlocked.Decrement(ref m_TrackedCount);
		}

		public override void Post(SendOrPostCallback callback, object state)
		{
			lock (m_AsyncWorkQueue)
			{
				m_AsyncWorkQueue.Add(new WorkRequest(callback, state));
			}
		}

		public override SynchronizationContext CreateCopy()
		{
			return new UnitySynchronizationContext(m_AsyncWorkQueue, m_MainThreadID);
		}

		private void Exec()
		{
			lock (m_AsyncWorkQueue)
			{
				m_CurrentFrameWork.AddRange(m_AsyncWorkQueue);
				m_AsyncWorkQueue.Clear();
			}
			foreach (WorkRequest item in m_CurrentFrameWork)
			{
				item.Invoke();
			}
			m_CurrentFrameWork.Clear();
		}

		private bool HasPendingTasks()
		{
			return m_AsyncWorkQueue.Count != 0 || m_TrackedCount != 0;
		}

		[RequiredByNativeCode]
		private static void InitializeSynchronizationContext()
		{
			if (SynchronizationContext.Current == null)
			{
				SynchronizationContext.SetSynchronizationContext(new UnitySynchronizationContext(Thread.CurrentThread.ManagedThreadId));
			}
		}

		[RequiredByNativeCode]
		private static void ExecuteTasks()
		{
			(SynchronizationContext.Current as UnitySynchronizationContext)?.Exec();
		}

		[RequiredByNativeCode]
		private static bool ExecutePendingTasks(long millisecondsTimeout)
		{
			UnitySynchronizationContext unitySynchronizationContext = SynchronizationContext.Current as UnitySynchronizationContext;
			if (unitySynchronizationContext == null)
			{
				return true;
			}
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			while (unitySynchronizationContext.HasPendingTasks() && stopwatch.ElapsedMilliseconds <= millisecondsTimeout)
			{
				unitySynchronizationContext.Exec();
				Thread.Sleep(1);
			}
			return !unitySynchronizationContext.HasPendingTasks();
		}
	}
}
