using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Windows.Speech
{
	public static class PhraseRecognitionSystem
	{
		public delegate void ErrorDelegate(SpeechError errorCode);

		public delegate void StatusDelegate(SpeechSystemStatus status);

		public static extern bool isSupported
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[ThreadSafe]
			[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
			get;
		}

		public static extern SpeechSystemStatus Status
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
			get;
		}

		public static event ErrorDelegate OnError;

		public static event StatusDelegate OnStatusChanged;

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		public static extern void Restart();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeHeader("PlatformDependent/Win/Bindings/SpeechBindings.h")]
		public static extern void Shutdown();

		[RequiredByNativeCode]
		private static void PhraseRecognitionSystem_InvokeErrorEvent(SpeechError errorCode)
		{
			PhraseRecognitionSystem.OnError?.Invoke(errorCode);
		}

		[RequiredByNativeCode]
		private static void PhraseRecognitionSystem_InvokeStatusChangedEvent(SpeechSystemStatus status)
		{
			PhraseRecognitionSystem.OnStatusChanged?.Invoke(status);
		}
	}
}
