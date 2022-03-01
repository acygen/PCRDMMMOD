using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.Jobs
{
	public static class IJobParallelForTransformExtensions
	{
		[StructLayout(LayoutKind.Sequential, Size = 1)]
		internal struct TransformParallelForLoopStruct<T> where T : struct, IJobParallelForTransform
		{
			public delegate void ExecuteJobFunction(ref T jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);

			public static IntPtr jobReflectionData;

			public static IntPtr Initialize()
			{
				if (jobReflectionData == IntPtr.Zero)
				{
					jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), JobType.ParallelFor, new ExecuteJobFunction(Execute));
				}
				return jobReflectionData;
			}

			public unsafe static void Execute(ref T jobData, IntPtr jobData2, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				UnsafeUtility.CopyPtrToStructure<IntPtr>((void*)jobData2, out var output);
				int* ptr = (int*)(void*)TransformAccessArray.GetSortedToUserIndex(output);
				TransformAccess* ptr2 = (TransformAccess*)(void*)TransformAccessArray.GetSortedTransformAccess(output);
				JobsUtility.GetJobRange(ref ranges, jobIndex, out var beginIndex, out var endIndex);
				for (int i = beginIndex; i < endIndex; i++)
				{
					int num = i;
					int index = ptr[num];
					jobData.Execute(index, ptr2[num]);
				}
			}
		}

		public unsafe static JobHandle Schedule<T>(this T jobData, TransformAccessArray transforms, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForTransform
		{
			JobsUtility.JobScheduleParameters parameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref jobData), TransformParallelForLoopStruct<T>.Initialize(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelForTransform(ref parameters, transforms.GetTransformAccessArrayForSchedule());
		}
	}
}
