using UnityEngine.Scripting;

namespace UnityEngine
{
	internal class EnumInfo
	{
		public string[] names;

		public int[] values;

		public string[] annotations;

		public bool isFlags;

		[UsedByNativeCode]
		internal static EnumInfo CreateEnumInfoFromNativeEnum(string[] names, int[] values, string[] annotations, bool isFlags)
		{
			EnumInfo enumInfo = new EnumInfo();
			enumInfo.names = names;
			enumInfo.values = values;
			enumInfo.annotations = annotations;
			enumInfo.isFlags = isFlags;
			return enumInfo;
		}
	}
}
