using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft0.Json.Converters;
using Newtonsoft0.Json.Linq;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Serialization
{
	public class DefaultContractResolver : IContractResolver
	{
		internal static readonly IContractResolver Instance = new DefaultContractResolver(shareCache: true);

		private static readonly IList<JsonConverter> BuiltInConverters = new List<JsonConverter>
		{
			new BinaryConverter(),
			new KeyValuePairConverter(),
			new XmlNodeConverter(),
			new DataSetConverter(),
			new DataTableConverter(),
			new BsonObjectIdConverter()
		};

		private static Dictionary<ResolverContractKey, JsonContract> _sharedContractCache;

		private static readonly object _typeContractCacheLock = new object();

		private Dictionary<ResolverContractKey, JsonContract> _instanceContractCache;

		private readonly bool _sharedCache;

		public bool DynamicCodeGeneration => JsonTypeReflector.DynamicCodeGeneration;

		public BindingFlags DefaultMembersSearchFlags
		{
			get;
			set;
		}

		public bool SerializeCompilerGeneratedMembers
		{
			get;
			set;
		}

		public DefaultContractResolver()
			: this(shareCache: false)
		{
		}

		public DefaultContractResolver(bool shareCache)
		{
			DefaultMembersSearchFlags = BindingFlags.Instance | BindingFlags.Public;
			_sharedCache = shareCache;
		}

		private Dictionary<ResolverContractKey, JsonContract> GetCache()
		{
			if (_sharedCache)
			{
				return _sharedContractCache;
			}
			return _instanceContractCache;
		}

		private void UpdateCache(Dictionary<ResolverContractKey, JsonContract> cache)
		{
			if (_sharedCache)
			{
				_sharedContractCache = cache;
			}
			else
			{
				_instanceContractCache = cache;
			}
		}

		public virtual JsonContract ResolveContract(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			ResolverContractKey key = new ResolverContractKey(GetType(), type);
			Dictionary<ResolverContractKey, JsonContract> cache = GetCache();
			if (cache == null || !cache.TryGetValue(key, out var value))
			{
				value = CreateContract(type);
				lock (_typeContractCacheLock)
				{
					cache = GetCache();
					Dictionary<ResolverContractKey, JsonContract> dictionary = ((cache != null) ? new Dictionary<ResolverContractKey, JsonContract>(cache) : new Dictionary<ResolverContractKey, JsonContract>());
					dictionary[key] = value;
					UpdateCache(dictionary);
					return value;
				}
			}
			return value;
		}

		protected virtual List<MemberInfo> GetSerializableMembers(Type objectType)
		{
			List<MemberInfo> list = (from m in ReflectionUtils.GetFieldsAndProperties(objectType, DefaultMembersSearchFlags)
				where !ReflectionUtils.IsIndexedProperty(m)
				select m).ToList();
			List<MemberInfo> list2 = (from m in ReflectionUtils.GetFieldsAndProperties(objectType, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
				where !ReflectionUtils.IsIndexedProperty(m)
				select m).ToList();
			List<MemberInfo> list3 = new List<MemberInfo>();
			foreach (MemberInfo item in list2)
			{
				if (SerializeCompilerGeneratedMembers || !item.IsDefined(typeof(CompilerGeneratedAttribute), inherit: true))
				{
					if (list.Contains(item))
					{
						list3.Add(item);
					}
					else if (JsonTypeReflector.GetAttribute<JsonPropertyAttribute>(item) != null)
					{
						list3.Add(item);
					}
				}
			}
			return list3;
		}

		protected virtual JsonObjectContract CreateObjectContract(Type objectType)
		{
			JsonObjectContract jsonObjectContract = new JsonObjectContract(objectType);
			InitializeContract(jsonObjectContract);
			jsonObjectContract.MemberSerialization = JsonTypeReflector.GetObjectMemberSerialization(objectType);
			jsonObjectContract.Properties.AddRange(CreateProperties(jsonObjectContract));
			if (jsonObjectContract.DefaultCreator == null || jsonObjectContract.DefaultCreatorNonPublic)
			{
				jsonObjectContract.ParametrizedConstructor = GetParametrizedConstructor(objectType);
			}
			return jsonObjectContract;
		}

		private ConstructorInfo GetParametrizedConstructor(Type objectType)
		{
			ConstructorInfo[] constructors = objectType.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
			if (constructors.Length == 1)
			{
				return constructors[0];
			}
			return null;
		}

		protected virtual JsonConverter ResolveContractConverter(Type objectType)
		{
			return JsonTypeReflector.GetJsonConverter(objectType, objectType);
		}

		private Func<object> GetDefaultCreator(Type createdType)
		{
			return JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(createdType);
		}

		private void InitializeContract(JsonContract contract)
		{
			JsonContainerAttribute jsonContainerAttribute = JsonTypeReflector.GetJsonContainerAttribute(contract.UnderlyingType);
			if (jsonContainerAttribute != null)
			{
				contract.IsReference = jsonContainerAttribute._isReference;
			}
			contract.Converter = ResolveContractConverter(contract.UnderlyingType);
			contract.InternalConverter = JsonSerializer.GetMatchingConverter(BuiltInConverters, contract.UnderlyingType);
			if (ReflectionUtils.HasDefaultConstructor(contract.CreatedType, nonPublic: true) || contract.CreatedType.IsValueType)
			{
				contract.DefaultCreator = GetDefaultCreator(contract.CreatedType);
				contract.DefaultCreatorNonPublic = !contract.CreatedType.IsValueType && ReflectionUtils.GetDefaultConstructor(contract.CreatedType) == null;
			}
			MethodInfo[] methods = contract.UnderlyingType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (MethodInfo methodInfo in methods)
			{
				if (!methodInfo.ContainsGenericParameters)
				{
					Type prevAttributeType = null;
					ParameterInfo[] parameters = methodInfo.GetParameters();
					if (IsValidCallback(methodInfo, parameters, typeof(OnSerializingAttribute), contract.OnSerializing, ref prevAttributeType))
					{
						contract.OnSerializing = methodInfo;
					}
					if (IsValidCallback(methodInfo, parameters, typeof(OnSerializedAttribute), contract.OnSerialized, ref prevAttributeType))
					{
						contract.OnSerialized = methodInfo;
					}
					if (IsValidCallback(methodInfo, parameters, typeof(OnDeserializingAttribute), contract.OnDeserializing, ref prevAttributeType))
					{
						contract.OnDeserializing = methodInfo;
					}
					if (IsValidCallback(methodInfo, parameters, typeof(OnDeserializedAttribute), contract.OnDeserialized, ref prevAttributeType))
					{
						contract.OnDeserialized = methodInfo;
					}
					if (IsValidCallback(methodInfo, parameters, typeof(OnErrorAttribute), contract.OnError, ref prevAttributeType))
					{
						contract.OnError = methodInfo;
					}
				}
			}
		}

		protected virtual JsonDictionaryContract CreateDictionaryContract(Type objectType)
		{
			JsonDictionaryContract jsonDictionaryContract = new JsonDictionaryContract(objectType);
			InitializeContract(jsonDictionaryContract);
			return jsonDictionaryContract;
		}

		protected virtual JsonArrayContract CreateArrayContract(Type objectType)
		{
			JsonArrayContract jsonArrayContract = new JsonArrayContract(objectType);
			InitializeContract(jsonArrayContract);
			return jsonArrayContract;
		}

		protected virtual JsonPrimitiveContract CreatePrimitiveContract(Type objectType)
		{
			JsonPrimitiveContract jsonPrimitiveContract = new JsonPrimitiveContract(objectType);
			InitializeContract(jsonPrimitiveContract);
			return jsonPrimitiveContract;
		}

		protected virtual JsonLinqContract CreateLinqContract(Type objectType)
		{
			JsonLinqContract jsonLinqContract = new JsonLinqContract(objectType);
			InitializeContract(jsonLinqContract);
			return jsonLinqContract;
		}

		protected virtual JsonISerializableContract CreateISerializableContract(Type objectType)
		{
			JsonISerializableContract jsonISerializableContract = new JsonISerializableContract(objectType);
			InitializeContract(jsonISerializableContract);
			ConstructorInfo constructor = objectType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[2]
			{
				typeof(SerializationInfo),
				typeof(StreamingContext)
			}, null);
			if (constructor != null)
			{
				MethodCall<object, object> methodCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(constructor);
				jsonISerializableContract.ISerializableCreator = (object[] args) => methodCall(null, args);
			}
			return jsonISerializableContract;
		}

		protected virtual JsonStringContract CreateStringContract(Type objectType)
		{
			JsonStringContract jsonStringContract = new JsonStringContract(objectType);
			InitializeContract(jsonStringContract);
			return jsonStringContract;
		}

		protected virtual JsonContract CreateContract(Type objectType)
		{
			if (JsonConvert.IsJsonPrimitiveType(objectType))
			{
				return CreatePrimitiveContract(objectType);
			}
			if (JsonTypeReflector.GetJsonObjectAttribute(objectType) != null)
			{
				return CreateObjectContract(objectType);
			}
			if (JsonTypeReflector.GetJsonArrayAttribute(objectType) != null)
			{
				return CreateArrayContract(objectType);
			}
			if (objectType.IsSubclassOf(typeof(JToken)))
			{
				return CreateLinqContract(objectType);
			}
			if (CollectionUtils.IsDictionaryType(objectType))
			{
				return CreateDictionaryContract(objectType);
			}
			if (typeof(IEnumerable).IsAssignableFrom(objectType))
			{
				return CreateArrayContract(objectType);
			}
			if (CanConvertToString(objectType))
			{
				return CreateStringContract(objectType);
			}
			if (typeof(ISerializable).IsAssignableFrom(objectType))
			{
				return CreateISerializableContract(objectType);
			}
			return CreateObjectContract(objectType);
		}

		internal static bool CanConvertToString(Type type)
		{
			TypeConverter converter = ConvertUtils.GetConverter(type);
			if (converter != null && !(converter is ComponentConverter) && !(converter is ReferenceConverter) && converter.GetType() != typeof(TypeConverter) && converter.CanConvertTo(typeof(string)))
			{
				return true;
			}
			if (type == typeof(Type) || type.IsSubclassOf(typeof(Type)))
			{
				return true;
			}
			return false;
		}

		private static bool IsValidCallback(MethodInfo method, ParameterInfo[] parameters, Type attributeType, MethodInfo currentCallback, ref Type prevAttributeType)
		{
			if (!method.IsDefined(attributeType, inherit: false))
			{
				return false;
			}
			if (currentCallback != null)
			{
				throw new Exception("Invalid attribute. Both '{0}' and '{1}' in type '{2}' have '{3}'.".FormatWith(CultureInfo.InvariantCulture, method, currentCallback, GetClrTypeFullName(method.DeclaringType), attributeType));
			}
			if (prevAttributeType != null)
			{
				throw new Exception("Invalid Callback. Method '{3}' in type '{2}' has both '{0}' and '{1}'.".FormatWith(CultureInfo.InvariantCulture, prevAttributeType, attributeType, GetClrTypeFullName(method.DeclaringType), method));
			}
			if (method.IsVirtual)
			{
				throw new Exception("Virtual Method '{0}' of type '{1}' cannot be marked with '{2}' attribute.".FormatWith(CultureInfo.InvariantCulture, method, GetClrTypeFullName(method.DeclaringType), attributeType));
			}
			if (method.ReturnType != typeof(void))
			{
				throw new Exception("Serialization Callback '{1}' in type '{0}' must return void.".FormatWith(CultureInfo.InvariantCulture, GetClrTypeFullName(method.DeclaringType), method));
			}
			if (attributeType == typeof(OnErrorAttribute))
			{
				if (parameters == null || parameters.Length != 2 || parameters[0].ParameterType != typeof(StreamingContext) || parameters[1].ParameterType != typeof(ErrorContext))
				{
					throw new Exception("Serialization Error Callback '{1}' in type '{0}' must have two parameters of type '{2}' and '{3}'.".FormatWith(CultureInfo.InvariantCulture, GetClrTypeFullName(method.DeclaringType), method, typeof(StreamingContext), typeof(ErrorContext)));
				}
			}
			else if (parameters == null || parameters.Length != 1 || parameters[0].ParameterType != typeof(StreamingContext))
			{
				throw new Exception("Serialization Callback '{1}' in type '{0}' must have a single parameter of type '{2}'.".FormatWith(CultureInfo.InvariantCulture, GetClrTypeFullName(method.DeclaringType), method, typeof(StreamingContext)));
			}
			prevAttributeType = attributeType;
			return true;
		}

		internal static string GetClrTypeFullName(Type type)
		{
			if (type.IsGenericTypeDefinition || !type.ContainsGenericParameters)
			{
				return type.FullName;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", type.Namespace, type.Name);
		}

		protected virtual IList<JsonProperty> CreateProperties(JsonObjectContract contract)
		{
			List<MemberInfo> serializableMembers = GetSerializableMembers(contract.UnderlyingType);
			if (serializableMembers == null)
			{
				throw new JsonSerializationException("Null collection of seralizable members returned.");
			}
			JsonPropertyCollection jsonPropertyCollection = new JsonPropertyCollection(contract);
			foreach (MemberInfo item in serializableMembers)
			{
				JsonProperty jsonProperty = CreateProperty(contract, item);
				if (jsonProperty != null)
				{
					jsonPropertyCollection.AddProperty(jsonProperty);
				}
			}
			return jsonPropertyCollection;
		}

		protected virtual IValueProvider CreateMemberValueProvider(MemberInfo member)
		{
			if (DynamicCodeGeneration)
			{
				return new DynamicValueProvider(member);
			}
			return new ReflectionValueProvider(member);
		}

		protected virtual JsonProperty CreateProperty(JsonObjectContract contract, MemberInfo member)
		{
			JsonProperty jsonProperty = new JsonProperty();
			jsonProperty.PropertyType = ReflectionUtils.GetMemberUnderlyingType(member);
			jsonProperty.ValueProvider = CreateMemberValueProvider(member);
			jsonProperty.Converter = JsonTypeReflector.GetJsonConverter(member, jsonProperty.PropertyType);
			JsonPropertyAttribute attribute = JsonTypeReflector.GetAttribute<JsonPropertyAttribute>(member);
			bool flag = JsonTypeReflector.GetAttribute<JsonIgnoreAttribute>(member) != null;
			string propertyName = ((attribute == null || attribute.PropertyName == null) ? member.Name : attribute.PropertyName);
			jsonProperty.PropertyName = ResolvePropertyName(propertyName);
			if (attribute != null)
			{
				jsonProperty.Required = attribute.Required;
			}
			else
			{
				jsonProperty.Required = Required.Default;
			}
			jsonProperty.Ignored = flag || (contract.MemberSerialization == MemberSerialization.OptIn && attribute == null);
			bool nonPublic = false;
			if ((DefaultMembersSearchFlags & BindingFlags.NonPublic) == BindingFlags.NonPublic)
			{
				nonPublic = true;
			}
			if (attribute != null)
			{
				nonPublic = true;
			}
			jsonProperty.Readable = ReflectionUtils.CanReadMemberValue(member, nonPublic);
			jsonProperty.Writable = ReflectionUtils.CanSetMemberValue(member, nonPublic);
			jsonProperty.MemberConverter = JsonTypeReflector.GetJsonConverter(member, ReflectionUtils.GetMemberUnderlyingType(member));
			jsonProperty.DefaultValue = JsonTypeReflector.GetAttribute<DefaultValueAttribute>(member)?.Value;
			jsonProperty.NullValueHandling = attribute?._nullValueHandling;
			jsonProperty.DefaultValueHandling = attribute?._defaultValueHandling;
			jsonProperty.ReferenceLoopHandling = attribute?._referenceLoopHandling;
			jsonProperty.ObjectCreationHandling = attribute?._objectCreationHandling;
			jsonProperty.TypeNameHandling = attribute?._typeNameHandling;
			jsonProperty.IsReference = attribute?._isReference;
			jsonProperty.ShouldSerialize = CreateShouldSerializeTest(member);
			return jsonProperty;
		}

		private Predicate<object> CreateShouldSerializeTest(MemberInfo member)
		{
			MethodInfo method = member.DeclaringType.GetMethod("ShouldSerialize" + member.Name, new Type[0]);
			if (method == null || method.ReturnType != typeof(bool))
			{
				return null;
			}
			MethodCall<object, object> shouldSerializeCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
			return (object o) => (bool)shouldSerializeCall(o);
		}

		protected virtual string ResolvePropertyName(string propertyName)
		{
			return propertyName;
		}
	}
}
