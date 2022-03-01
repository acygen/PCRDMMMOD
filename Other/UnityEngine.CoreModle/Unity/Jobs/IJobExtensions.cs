using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	public static class IJobExtensions
	{
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		internal struct JobStruct<T> where T : struct, IJob
		{
			public delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);

			public static IntPtr jobReflectionData;

			public static IntPtr Initialize()
			{
				if (jobReflectionData == IntPtr.Zero)
				{
					jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), JobType.Single, new ExecuteJobFunction(Execute));
				}
				return jobReflectionData;
			}

			public static void Execute(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				data.Execute();
			}
		}

		public unsafe static JobHandle Schedule<T>(this T jobData, JobHandle dependsOn = default(JobHandle)) where T : struct, IJob
		{
			JobsUtility.JobScheduleParameters parameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref jobData), JobStruct<T>.Initialize(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.Schedule(ref parameters);
		}

		public unsafe static void Run<T>(this T jobData) where T : struct, IJob
		{
			JobsUtility.JobScheduleParameters parameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref jobData), JobStruct<T>.Initialize(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.Schedule(ref parameters);
		}
	}
}
