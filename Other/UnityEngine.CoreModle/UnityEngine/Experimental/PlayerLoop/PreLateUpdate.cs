using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.PlayerLoop
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	[RequiredByNativeCode]
	public struct PreLateUpdate
	{
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct AIUpdatePostScript
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct DirectorUpdateAnimationBegin
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct LegacyAnimationUpdate
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct DirectorUpdateAnimationEnd
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct DirectorDeferredEvaluate
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct UpdateNetworkManager
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct UpdateMasterServerInterface
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct UNetUpdate
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct EndGraphicsJobsAfterScriptUpdate
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct ParticleSystemBeginUpdateAll
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct ScriptRunBehaviourLateUpdate
		{
		}

		[StructLayout(LayoutKind.Sequential, Size = 1)]
		[RequiredByNativeCode]
		public struct ConstraintManagerUpdate
		{
		}
	}
}
