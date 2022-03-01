using System;
using System.Globalization;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class JsonConverterAttribute : Attribute
	{
		private readonly Type _converterType;

		public Type ConverterType => _converterType;

		public JsonConverterAttribute(Type converterType)
		{
			if (converterType == null)
			{
				throw new ArgumentNullException("converterType");
			}
			_converterType = converterType;
		}

		internal static JsonConverter CreateJsonConverterInstance(Type converterType)
		{
			try
			{
				return (JsonConverter)Activator.CreateInstance(converterType);
			}
			catch (Exception innerException)
			{
				throw new Exception("Error creating {0}".FormatWith(CultureInfo.InvariantCulture, converterType), innerException);
			}
		}
	}
}
