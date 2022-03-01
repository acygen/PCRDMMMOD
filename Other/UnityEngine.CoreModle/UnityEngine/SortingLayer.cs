using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	[NativeHeader("Runtime/BaseClasses/TagManager.h")]
	public struct SortingLayer
	{
		private int m_Id;

		public int id => m_Id;

		public string name => IDToName(m_Id);

		public int value => GetLayerValueFromID(m_Id);

		public static SortingLayer[] layers
		{
			get
			{
				int[] sortingLayerIDsInternal = GetSortingLayerIDsInternal();
				SortingLayer[] array = new SortingLayer[sortingLayerIDsInternal.Length];
				for (int i = 0; i < sortingLayerIDsInternal.Length; i++)
				{
					array[i].m_Id = sortingLayerIDsInternal[i];
				}
				return array;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetTagManager().GetSortingLayerIDs")]
		private static extern int[] GetSortingLayerIDsInternal();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetTagManager().GetSortingLayerValueFromUniqueID")]
		public static extern int GetLayerValueFromID(int id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetTagManager().GetSortingLayerValueFromName")]
		public static extern int GetLayerValueFromName(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetTagManager().GetSortingLayerUniqueIDFromName")]
		public static extern int NameToID(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetTagManager().GetSortingLayerNameFromUniqueID")]
		public static extern string IDToName(int id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetTagManager().IsSortingLayerUniqueIDValid")]
		public static extern bool IsValid(int id);
	}
}
