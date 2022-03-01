using System;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	[UsedByNativeCode]
	public struct DrawShadowsSettings
	{
		private IntPtr _cullResults;

		public int lightIndex;

		public ShadowSplitData splitData;

		public CullResults cullResults
		{
			set
			{
				_cullResults = value.cullResults;
			}
		}

		public DrawShadowsSettings(CullResults cullResults, int lightIndex)
		{
			_cullResults = cullResults.cullResults;
			this.lightIndex = lightIndex;
			splitData.cullingPlaneCount = 0;
			splitData.cullingSphere = Vector4.zero;
		}
	}
}
