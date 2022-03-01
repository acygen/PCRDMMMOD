using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	public struct JobRanges
	{
		public int BatchSize;

		public int NumJobs;

		public int TotalIterationCount;

		public int NumPhases;

		public int IndicesPerPhase;

		public IntPtr StartEndIndex;

		public IntPtr PhaseData;
	}
}
