using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Serialization
{
	public class DefaultSerializationBinder : SerializationBinder
	{
		internal static readonly DefaultSerializationBinder Instance = new DefaultSerializationBinder();

		public override Type BindToType(string assemblyName, string typeName)
		{
			if (assemblyName != null)
			{
				Assembly assembly = Assembly.LoadWithPartialName(assemblyName);
				if (assembly == null)
				{
					throw new JsonSerializationException("Could not load assembly '{0}'.".FormatWith(CultureInfo.InvariantCulture, assemblyName));
				}
				Type type = assembly.GetType(typeName);
				if (type == null)
				{
					throw new JsonSerializationException("Could not find type '{0}' in assembly '{1}'.".FormatWith(CultureInfo.InvariantCulture, typeName, assembly.FullName));
				}
				return type;
			}
			return Type.GetType(typeName);
		}
	}
}
