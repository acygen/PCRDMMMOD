using System;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.U2D
{
	internal struct SpriteChannelInfo
	{
		[NativeName("buffer")]
		private IntPtr m_Buffer;

		[NativeName("count")]
		private int m_Count;

		[NativeName("offset")]
		private int m_Offset;

		[NativeName("stride")]
		private int m_Stride;

		public unsafe void* buffer
		{
			get
			{
				return (void*)m_Buffer;
			}
			set
			{
				m_Buffer = (IntPtr)value;
			}
		}

		public int count
		{
			get
			{
				return m_Count;
			}
			set
			{
				m_Count = value;
			}
		}

		public int offset
		{
			get
			{
				return m_Offset;
			}
			set
			{
				m_Offset = value;
			}
		}

		public int stride
		{
			get
			{
				return m_Stride;
			}
			set
			{
				m_Stride = value;
			}
		}
	}
}
