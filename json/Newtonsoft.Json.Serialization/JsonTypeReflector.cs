using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Serialization
{
	internal static class JsonTypeReflector
	{
		public const string IdPropertyName = "$id";

		public const string RefPropertyName = "$ref";

		public const string TypePropertyName = "$type";

		public const string ArrayValuesPropertyName = "$values";

		public const string ShouldSerializePrefix = "ShouldSerialize";

		private static readonly ThreadSafeStore<ICustomAttributeProvider, Type> JsonConverterTypeCache = new ThreadSafeStore<ICustomAttributeProvider, Type>(GetJsonConverterTypeFromAttribute);

		private static bool? _dynamicCodeGeneration;

		public static bool DynamicCodeGeneration
		{
			get
			{
				if (!_dynamicCodeGeneration.HasValue)
				{
					try
					{
						new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
						new ReflectionPermission(ReflectionPermissionFlag.RestrictedMemberAccess).Demand();
						new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
						_dynamicCodeGeneration = true;
					}
					catch (Exception)
					{
						_dynamicCodeGeneration = false;
					}
				}
				return _dynamicCodeGeneration.Value;
			}
		}

		public static ReflectionDelegateFactory ReflectionDelegateFactory
		{
			get
			{
				if (DynamicCodeGeneration)
				{
					return DynamicReflectionDelegateFactory.Instance;
				}
				return LateBoundReflectionDelegateFactory.Instance;
			}
		}

		public static JsonContainerAttribute GetJsonContainerAttribute(Type type)
		{
			return CachedAttributeGetter<JsonContainerAttribute>.GetAttribute(type);
		}

		public static JsonObjectAttribute GetJsonObjectAttribute(Type type)
		{
			return GetJsonContainerAttribute(type) as JsonObjectAttribute;
		}

		public static JsonArrayAttribute GetJsonArrayAttribute(Type type)
		{
			return GetJsonContainerAttribute(type) as JsonArrayAttribute;
		}

		public static MemberSerialization GetObjectMemberSerialization(Type objectType)
		{
			return GetJsonObjectAttribute(objectType)?.MemberSerialization ?? MemberSerialization.OptOut;
		}

		private static Type GetJsonConverterType(ICustomAttributeProvider attributeProvider)
		{
			return JsonConverterTypeCache.Get(attributeProvider);
		}

		private static Type GetJsonConverterTypeFromAttribute(ICustomAttributeProvider attributeProvider)
		{
			return GetAttribute<JsonConverterAttribute>(attributeProvider)?.ConverterType;
		}

		public static JsonConverter GetJsonConverter(ICustomAttributeProvider attributeProvider, Type targetConvertedType)
		{
			Type jsonConverterType = GetJsonConverterType(attributeProvider);
			if (jsonConverterType != null)
			{
				JsonConverter jsonConverter = JsonConverterAttribute.CreateJsonConverterInstance(jsonConverterType);
				if (!jsonConverter.CanConvert(targetConvertedType))
				{
					throw new JsonSerializationException("JsonConverter {0} on {1} is not compatible with member type {2}.".FormatWith(CultureInfo.InvariantCulture, jsonConverter.GetType().Name, attributeProvider, targetConvertedType.Name));
				}
				return jsonConverter;
			}
			return null;
		}

		public static TypeConverter GetTypeConverter(Type type)
		{
			return TypeDescriptor.GetConverter(type);
		}

		public static T GetAttribute<T>(ICustomAttributeProvider attributeProvider) where T : Attribute
		{
			return ReflectionUtils.GetAttribute<T>(attributeProvider, inherit: true);
		}
	}
}
