using UnityEngine.Scripting;

namespace UnityEngine
{
	[UsedByNativeCode]
	public struct CachedAssetBundle
	{
		private string m_Name;

		private Hash128 m_Hash;

		public string name
		{
			get
			{
				return m_Name;
			}
			set
			{
				m_Name = value;
			}
		}

		public Hash128 hash
		{
			get
			{
				return m_Hash;
			}
			set
			{
				m_Hash = value;
			}
		}

		public CachedAssetBundle(string name, Hash128 hash)
		{
			m_Name = name;
			m_Hash = hash;
		}
	}
}
