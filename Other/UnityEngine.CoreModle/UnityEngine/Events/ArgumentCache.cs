using System;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	[Serializable]
	internal class ArgumentCache : ISerializationCallbackReceiver
	{
		[SerializeField]
		[FormerlySerializedAs("objectArgument")]
		private Object m_ObjectArgument;

		[FormerlySerializedAs("objectArgumentAssemblyTypeName")]
		[SerializeField]
		private string m_ObjectArgumentAssemblyTypeName;

		[FormerlySerializedAs("intArgument")]
		[SerializeField]
		private int m_IntArgument;

		[FormerlySerializedAs("floatArgument")]
		[SerializeField]
		private float m_FloatArgument;

		[FormerlySerializedAs("stringArgument")]
		[SerializeField]
		private string m_StringArgument;

		[SerializeField]
		private bool m_BoolArgument;

		public Object unityObjectArgument
		{
			get
			{
				return m_ObjectArgument;
			}
			set
			{
				m_ObjectArgument = value;
				m_ObjectArgumentAssemblyTypeName = ((!(value != null)) ? string.Empty : value.GetType().AssemblyQualifiedName);
			}
		}

		public string unityObjectArgumentAssemblyTypeName => m_ObjectArgumentAssemblyTypeName;

		public int intArgument
		{
			get
			{
				return m_IntArgument;
			}
			set
			{
				m_IntArgument = value;
			}
		}

		public float floatArgument
		{
			get
			{
				return m_FloatArgument;
			}
			set
			{
				m_FloatArgument = value;
			}
		}

		public string stringArgument
		{
			get
			{
				return m_StringArgument;
			}
			set
			{
				m_StringArgument = value;
			}
		}

		public bool boolArgument
		{
			get
			{
				return m_BoolArgument;
			}
			set
			{
				m_BoolArgument = value;
			}
		}

		private void TidyAssemblyTypeName()
		{
			if (!string.IsNullOrEmpty(m_ObjectArgumentAssemblyTypeName))
			{
				int num = int.MaxValue;
				int num2 = m_ObjectArgumentAssemblyTypeName.IndexOf(", Version=");
				if (num2 != -1)
				{
					num = Math.Min(num2, num);
				}
				num2 = m_ObjectArgumentAssemblyTypeName.IndexOf(", Culture=");
				if (num2 != -1)
				{
					num = Math.Min(num2, num);
				}
				num2 = m_ObjectArgumentAssemblyTypeName.IndexOf(", PublicKeyToken=");
				if (num2 != -1)
				{
					num = Math.Min(num2, num);
				}
				if (num != int.MaxValue)
				{
					m_ObjectArgumentAssemblyTypeName = m_ObjectArgumentAssemblyTypeName.Substring(0, num);
				}
				num2 = m_ObjectArgumentAssemblyTypeName.IndexOf(", UnityEngine.");
				if (num2 != -1 && m_ObjectArgumentAssemblyTypeName.EndsWith("Module"))
				{
					m_ObjectArgumentAssemblyTypeName = m_ObjectArgumentAssemblyTypeName.Substring(0, num2) + ", UnityEngine";
				}
			}
		}

		public void OnBeforeSerialize()
		{
			TidyAssemblyTypeName();
		}

		public void OnAfterDeserialize()
		{
			TidyAssemblyTypeName();
		}
	}
}
