using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	public static class IJobParallelForExtensions
	{
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		internal struct ParallelForJobStruct<T> where T : struct, IJobParallelFor
		{
			public delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);

			public static IntPtr jobReflectionData;

			public static IntPtr Initialize()
			{
				if (jobReflectionData == IntPtr.Zero)
				{
					jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), JobType.ParallelFor, new ExecuteJobFunction(Execute));
				}
				return jobReflectionData;
			}

			public static void Execute(ref T jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				int beginIndex;
				int endIndex;
				while (JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out beginIndex, out endIndex))
				{
					for (int i = beginIndex; i < endIndex; i++)
					{
						jobData.Execute(i);
					}
				}
			}
		}

		public unsafe static JobHandle Schedule<T>(this T jobData, int arrayLength, int innerloopBatchCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelFor
		{
			JobsUtility.JobScheduleParameters parameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref jobData), ParallelForJobStruct<T>.Initialize(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelFor(ref parameters, arrayLength, innerloopBatchCount);
		}

		public unsafe static void Run<T>(this T jobData, int arrayLength) where T : struct, IJobParallelFor
		{
			JobsUtility.JobScheduleParameters parameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref jobData), ParallelForJobStruct<T>.Initialize(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelFor(ref parameters, arrayLength, arrayLength);
		}
	}
}
