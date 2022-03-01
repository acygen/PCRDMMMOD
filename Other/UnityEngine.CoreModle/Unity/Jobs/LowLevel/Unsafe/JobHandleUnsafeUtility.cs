namespace Unity.Jobs.LowLevel.Unsafe
{
	public static class JobHandleUnsafeUtility
	{
		public unsafe static JobHandle CombineDependencies(JobHandle* jobs, int count)
		{
			return JobHandle.CombineDependenciesInternalPtr(jobs, count);
		}
	}
}
