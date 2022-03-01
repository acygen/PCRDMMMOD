using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.Collections.LowLevel.Unsafe
{
	[NativeHeader("Runtime/Export/Unsafe/UnsafeUtility.bindings.h")]
	[StaticAccessor("UnsafeUtility", StaticAccessorType.DoubleColon)]
	public static class UnsafeUtility
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		private static extern int GetFieldOffsetInStruct(FieldInfo field);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		private static extern int GetFieldOffsetInClass(FieldInfo field);

		public static int GetFieldOffset(FieldInfo field)
		{
			if (field.DeclaringType.IsValueType)
			{
				return GetFieldOffsetInStruct(field);
			}
			if (field.DeclaringType.IsClass)
			{
				return GetFieldOffsetInClass(field);
			}
			return -1;
		}

		public unsafe static void* PinGCObjectAndGetAddress(object target, out ulong gcHandle)
		{
			return PinSystemObjectAndGetAddress(target, out gcHandle);
		}

		public unsafe static void* PinGCArrayAndGetDataAddress(Array target, out ulong gcHandle)
		{
			return PinSystemArrayAndGetAddress(target, out gcHandle);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		private unsafe static extern void* PinSystemArrayAndGetAddress(object target, out ulong gcHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		private unsafe static extern void* PinSystemObjectAndGetAddress(object target, out ulong gcHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern void ReleaseGCObject(ulong gcHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern void CopyObjectAddressToPtr(object target, void* dstPtr);

		public static bool IsBlittable<T>() where T : struct
		{
			return IsBlittable(typeof(T));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern void* Malloc(long size, int alignment, Allocator allocator);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern void Free(void* memory, Allocator allocator);

		public static bool IsValidAllocator(Allocator allocator)
		{
			return allocator > Allocator.None;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern void MemCpy(void* destination, void* source, long size);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern void MemCpyReplicate(void* destination, void* source, int size, int count);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern void MemCpyStride(void* destination, int destinationStride, void* source, int sourceStride, int elementSize, int count);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern void MemMove(void* destination, void* source, long size);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern void MemClear(void* destination, long size);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public unsafe static extern int MemCmp(void* ptr1, void* ptr2, long size);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern int SizeOf(Type type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		public static extern bool IsBlittable(Type type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		internal static extern void LogError(string msg, string filename, int linenumber);

		private static bool IsValueType(Type t)
		{
			return t.IsValueType;
		}

		private static bool IsPrimitive(Type t)
		{
			return t.IsPrimitive;
		}

		private static bool IsBlittableValueType(Type t)
		{
			return IsValueType(t) && IsBlittable(t);
		}

		private static string GetReasonForTypeNonBlittableImpl(Type t, string name)
		{
			if (!IsValueType(t))
			{
				return $"{name} is not blittable because it is not of value type ({t})\n";
			}
			if (IsPrimitive(t))
			{
				return $"{name} is not blittable ({t})\n";
			}
			string text = "";
			FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (FieldInfo fieldInfo in fields)
			{
				if (!IsBlittableValueType(fieldInfo.FieldType))
				{
					text += GetReasonForTypeNonBlittableImpl(fieldInfo.FieldType, $"{name}.{fieldInfo.Name}");
				}
			}
			return text;
		}

		internal static bool IsArrayBlittable(Array arr)
		{
			return IsBlittableValueType(arr.GetType().GetElementType());
		}

		internal static bool IsGenericListBlittable<T>() where T : struct
		{
			return IsBlittable<T>();
		}

		internal static string GetReasonForArrayNonBlittable(Array arr)
		{
			Type elementType = arr.GetType().GetElementType();
			return GetReasonForTypeNonBlittableImpl(elementType, elementType.Name);
		}

		internal static string GetReasonForGenericListNonBlittable<T>() where T : struct
		{
			Type typeFromHandle = typeof(T);
			return GetReasonForTypeNonBlittableImpl(typeFromHandle, typeFromHandle.Name);
		}

		internal static string GetReasonForTypeNonBlittable(Type t)
		{
			return GetReasonForTypeNonBlittableImpl(t, t.Name);
		}

		internal static string GetReasonForValueTypeNonBlittable<T>() where T : struct
		{
			Type typeFromHandle = typeof(T);
			return GetReasonForTypeNonBlittableImpl(typeFromHandle, typeFromHandle.Name);
		}

		public unsafe static void CopyPtrToStructure<T>(void* ptr, out T output) where T : struct
		{
			output = *(T*)ptr;
		}

		public unsafe static void CopyStructureToPtr<T>(ref T input, void* ptr) where T : struct
		{
			*(T*)ptr = input;
		}

		public unsafe static T ReadArrayElement<T>(void* source, int index)
		{
			return *(T*)((byte*)source + index * sizeof(T));
		}

		public unsafe static T ReadArrayElementWithStride<T>(void* source, int index, int stride)
		{
			return *(T*)((byte*)source + index * stride);
		}

		public unsafe static void WriteArrayElement<T>(void* destination, int index, T value)
		{
			*(T*)((byte*)destination + index * sizeof(T)) = value;
		}

		public unsafe static void WriteArrayElementWithStride<T>(void* destination, int index, int stride, T value)
		{
			*(T*)((byte*)destination + index * stride) = value;
		}

		public unsafe static void* AddressOf<T>(ref T output) where T : struct
		{
			return System.Runtime.CompilerServices.Unsafe.AsPointer(ref output);
		}

		public unsafe static int SizeOf<T>() where T : struct
		{
			return sizeof(T);
		}

		public static int AlignOf<T>() where T : struct
		{
			return 4;
		}
	}
}
