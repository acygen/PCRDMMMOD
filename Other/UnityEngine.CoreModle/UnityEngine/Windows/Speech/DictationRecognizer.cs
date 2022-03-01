using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Windows.Speech
{
	public sealed class DictationRecognizer : IDisposable
	{
		public delegate void DictationHypothesisDelegate(string text);

		public delegate void DictationResultDelegate(string text, ConfidenceLevel confidence);

		public delegate void DictationCompletedDelegate(DictationCompletionCause cause);

		public delegate void DictationErrorHandler(string error, int hresult);

		private IntPtr m_Recognizer;

		public SpeechSystemStatus Status => (m_Recognizer != IntPtr.Zero) ? GetStatus(m_Recognizer) : SpeechSystemStatus.Stopped;

		public float AutoSilenceTimeoutSeconds
		{
			get
			{
				if (m_Recognizer == IntPtr.Zero)
				{
					return 0f;
				}
				return GetAutoSilenceTimeoutSeconds(m_Recognizer);
			}
			set
			{
				if (!(m_Recognizer == IntPtr.Zero))
				{
					SetAutoSilenceTimeoutSeconds(m_Recognizer, value);
				}
			}
		}

		public float InitialSilenceTimeoutSeconds
		{
			get
			{
				if (m_Recognizer == IntPtr.Zero)
				{
					return 0f;
				}
				return GetInitialSilenceTimeoutSeconds(m_Recognizer);
			}
			set
			{
				if (!(m_Recognizer == IntPtr.Zero))
				{
					SetInitialSilenceTimeoutSeconds(m_Recognizer, value);
				}
			}
		}

		public event DictationHypothesisDelegate DictationHypothesis;

		public event DictationResultDelegate DictationResult;

		public event DictationCompletedDelegate DictationComplete;

		public event DictationErrorHandler DictationError;

		public DictationRecognizer()
			: this(ConfidenceLevel.Medium, DictationTopicConstraint.Dictation)
		{
		}

		public DictationRecognizer(ConfidenceLevel confidenceLevel)
			: this(confidenceLevel, DictationTopicConstraint.Dictation)
		{
		}

		public DictationRecognizer(DictationTopicConstraint topic)
			: this(ConfidenceLevel.Medium, topic)
		{
		}

		public DictationRecognizer(ConfidenceLevel minimumConfidence, DictationTopicConstraint topic)
		{
			m_Recognizer = Create(this, minimumConfidence, topic);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		private static extern IntPtr Create(object self, ConfidenceLevel minimumConfidence, DictationTopicConstraint topicConstraint);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		private static extern void Start(IntPtr self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		private static extern void Stop(IntPtr self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		private static extern void Destroy(IntPtr self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		private static extern void DestroyThreaded(IntPtr self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		private static extern SpeechSystemStatus GetStatus(IntPtr self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		private static extern float GetAutoSilenceTimeoutSeconds(IntPtr self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		private static extern void SetAutoSilenceTimeoutSeconds(IntPtr self, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		private static extern float GetInitialSilenceTimeoutSeconds(IntPtr self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		private static extern void SetInitialSilenceTimeoutSeconds(IntPtr self, float value);

		~DictationRecognizer()
		{
			if (m_Recognizer != IntPtr.Zero)
			{
				DestroyThreaded(m_Recognizer);
				m_Recognizer = IntPtr.Zero;
				GC.SuppressFinalize(this);
			}
		}

		public void Start()
		{
			if (!(m_Recognizer == IntPtr.Zero))
			{
				Start(m_Recognizer);
			}
		}

		public void Stop()
		{
			if (!(m_Recognizer == IntPtr.Zero))
			{
				Stop(m_Recognizer);
			}
		}

		public void Dispose()
		{
			if (m_Recognizer != IntPtr.Zero)
			{
				Destroy(m_Recognizer);
				m_Recognizer = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeHypothesisGeneratedEvent(string keyword)
		{
			this.DictationHypothesis?.Invoke(keyword);
		}

		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeResultGeneratedEvent(string keyword, ConfidenceLevel minimumConfidence)
		{
			this.DictationResult?.Invoke(keyword, minimumConfidence);
		}

		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeCompletedEvent(DictationCompletionCause cause)
		{
			this.DictationComplete?.Invoke(cause);
		}

		[RequiredByNativeCode]
		private void DictationRecognizer_InvokeErrorEvent(string error, int hresult)
		{
			this.DictationError?.Invoke(error, hresult);
		}
	}
}
