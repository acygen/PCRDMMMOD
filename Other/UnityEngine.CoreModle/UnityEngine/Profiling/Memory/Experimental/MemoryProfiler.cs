#define UNITY_ASSERTIONS
using System;
using System.Runtime.CompilerServices;
using UnityEngine.Assertions;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling.Memory.Experimental
{
	[NativeHeader("Modules/Profiler/Public/ProfilerConnection.h")]
	public sealed class MemoryProfiler
	{
		private static event Action<string, bool> snapshotFinished;

		public static event Action<MetaData> createMetaData;

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("TakeMemorySnapshot")]
		[NativeConditional("ENABLE_PLAYERCONNECTION")]
		[StaticAccessor("ProfilerConnection::Get()", StaticAccessorType.Dot)]
		private static extern void TakeSnapshotInternal(string path, uint captureFlag);

		public static void TakeSnapshot(string path, Action<string, bool> finishCallback, CaptureFlags captureFlags = CaptureFlags.ManagedObjects | CaptureFlags.NativeObjects)
		{
			if (MemoryProfiler.snapshotFinished != null)
			{
				Debug.LogWarning("Canceling taking the snapshot. There is already ongoing capture.");
				finishCallback(path, arg2: false);
			}
			else
			{
				snapshotFinished += finishCallback;
				TakeSnapshotInternal(path, (uint)captureFlags);
			}
		}

		public static void TakeTempSnapshot(Action<string, bool> finishCallback, CaptureFlags captureFlags = CaptureFlags.ManagedObjects | CaptureFlags.NativeObjects)
		{
			string[] array = Application.dataPath.Split('/');
			string text = array[array.Length - 2];
			string path = Application.temporaryCachePath + "/" + text + ".snap";
			TakeSnapshot(path, finishCallback, captureFlags);
		}

		[RequiredByNativeCode]
		private static byte[] PrepareMetadata()
		{
			if (MemoryProfiler.createMetaData == null)
			{
				return new byte[0];
			}
			MetaData metaData = new MetaData();
			MemoryProfiler.createMetaData(metaData);
			if (metaData.content == null)
			{
				metaData.content = "";
			}
			if (metaData.platform == null)
			{
				metaData.platform = "";
			}
			int num = 2 * metaData.content.Length;
			int num2 = 2 * metaData.platform.Length;
			int num3 = num + num2 + 12;
			byte[] array = null;
			if (metaData.screenshot != null)
			{
				array = metaData.screenshot.GetRawTextureData();
				num3 += array.Length + 12;
			}
			byte[] array2 = new byte[num3];
			int offset = 0;
			offset = WriteIntToByteArray(array2, offset, metaData.content.Length);
			offset = WriteStringToByteArray(array2, offset, metaData.content);
			offset = WriteIntToByteArray(array2, offset, metaData.platform.Length);
			offset = WriteStringToByteArray(array2, offset, metaData.platform);
			if (metaData.screenshot != null)
			{
				offset = WriteIntToByteArray(array2, offset, array.Length);
				Array.Copy(array, 0, array2, offset, array.Length);
				offset += array.Length;
				offset = WriteIntToByteArray(array2, offset, metaData.screenshot.width);
				offset = WriteIntToByteArray(array2, offset, metaData.screenshot.height);
				offset = WriteIntToByteArray(array2, offset, (int)metaData.screenshot.format);
			}
			else
			{
				offset = WriteIntToByteArray(array2, offset, 0);
			}
			Assert.AreEqual(array2.Length, offset);
			return array2;
		}

		internal unsafe static int WriteIntToByteArray(byte[] array, int offset, int value)
		{
			byte* ptr = (byte*)(&value);
			array[offset++] = *ptr;
			array[offset++] = ptr[1];
			array[offset++] = ptr[2];
			array[offset++] = ptr[3];
			return offset;
		}

		internal unsafe static int WriteStringToByteArray(byte[] array, int offset, string value)
		{
			if (value.Length != 0)
			{
				fixed (string text = value)
				{
					char* ptr = (char*)((nint)text + RuntimeHelpers.OffsetToStringData);
					char* ptr2 = ptr;
					for (char* ptr3 = ptr + value.Length; ptr2 != ptr3; ptr2++)
					{
						for (int i = 0; i < 2; i++)
						{
							array[offset++] = ((byte*)ptr2)[i];
						}
					}
				}
			}
			return offset;
		}

		[RequiredByNativeCode]
		private static void FinalizeSnapshot(string path, bool result)
		{
			if (MemoryProfiler.snapshotFinished != null)
			{
				Action<string, bool> action = MemoryProfiler.snapshotFinished;
				MemoryProfiler.snapshotFinished = null;
				action(path, result);
			}
		}
	}
}
