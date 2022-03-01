using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft0.Json.Linq;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Serialization
{
	internal class JsonSerializerInternalReader : JsonSerializerInternalBase
	{
		internal enum RequiredValue
		{
			None,
			Null,
			Value
		}

		private JsonSerializerProxy _internalSerializer;

		private JsonFormatterConverter _formatterConverter;

		public JsonSerializerInternalReader(JsonSerializer serializer)
			: base(serializer)
		{
		}

		public void Populate(JsonReader reader, object target)
		{
			ValidationUtils.ArgumentNotNull(target, "target");
			Type type = target.GetType();
			JsonContract jsonContract = base.Serializer.ContractResolver.ResolveContract(type);
			if (reader.TokenType == JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == JsonToken.StartArray)
			{
				if (jsonContract is JsonArrayContract)
				{
					PopulateList(CollectionUtils.CreateCollectionWrapper(target), reader, null, (JsonArrayContract)jsonContract);
					return;
				}
				throw new JsonSerializationException("Cannot populate JSON array onto type '{0}'.".FormatWith(CultureInfo.InvariantCulture, type));
			}
			if (reader.TokenType == JsonToken.StartObject)
			{
				CheckedRead(reader);
				string id = null;
				if (reader.TokenType == JsonToken.PropertyName && string.Equals(reader.Value.ToString(), "$id", StringComparison.Ordinal))
				{
					CheckedRead(reader);
					id = reader.Value.ToString();
					CheckedRead(reader);
				}
				if (jsonContract is JsonDictionaryContract)
				{
					PopulateDictionary(CollectionUtils.CreateDictionaryWrapper(target), reader, (JsonDictionaryContract)jsonContract, id);
					return;
				}
				if (jsonContract is JsonObjectContract)
				{
					PopulateObject(target, reader, (JsonObjectContract)jsonContract, id);
					return;
				}
				throw new JsonSerializationException("Cannot populate JSON object onto type '{0}'.".FormatWith(CultureInfo.InvariantCulture, type));
			}
			throw new JsonSerializationException("Unexpected initial token '{0}' when populating object. Expected JSON object or array.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
		}

		private JsonContract GetContractSafe(Type type)
		{
			if (type == null)
			{
				return null;
			}
			return base.Serializer.ContractResolver.ResolveContract(type);
		}

		private JsonContract GetContractSafe(Type type, object value)
		{
			if (value == null)
			{
				return GetContractSafe(type);
			}
			return base.Serializer.ContractResolver.ResolveContract(value.GetType());
		}

		public object Deserialize(JsonReader reader, Type objectType)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				return null;
			}
			return CreateValueNonProperty(reader, objectType, GetContractSafe(objectType));
		}

		private JsonSerializerProxy GetInternalSerializer()
		{
			if (_internalSerializer == null)
			{
				_internalSerializer = new JsonSerializerProxy(this);
			}
			return _internalSerializer;
		}

		private JsonFormatterConverter GetFormatterConverter()
		{
			if (_formatterConverter == null)
			{
				_formatterConverter = new JsonFormatterConverter(GetInternalSerializer());
			}
			return _formatterConverter;
		}

		private JToken CreateJToken(JsonReader reader, JsonContract contract)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			if (contract != null && contract.UnderlyingType == typeof(JRaw))
			{
				return JRaw.Create(reader);
			}
			using (JTokenWriter jTokenWriter = new JTokenWriter())
			{
				jTokenWriter.WriteToken(reader);
				return jTokenWriter.Token;
			}
		}

		private JToken CreateJObject(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			using (JTokenWriter jTokenWriter = new JTokenWriter())
			{
				jTokenWriter.WriteStartObject();
				if (reader.TokenType == JsonToken.PropertyName)
				{
					jTokenWriter.WriteToken(reader, reader.Depth - 1);
				}
				else
				{
					jTokenWriter.WriteEndObject();
				}
				return jTokenWriter.Token;
			}
		}

		private object CreateValueProperty(JsonReader reader, JsonProperty property, object target, bool gottenCurrentValue, object currentValue)
		{
			JsonContract contractSafe = GetContractSafe(property.PropertyType, currentValue);
			Type propertyType = property.PropertyType;
			JsonConverter converter = GetConverter(contractSafe, property.MemberConverter);
			if (converter != null && converter.CanRead)
			{
				if (!gottenCurrentValue && target != null && property.Readable)
				{
					currentValue = property.ValueProvider.GetValue(target);
				}
				return converter.ReadJson(reader, propertyType, currentValue, GetInternalSerializer());
			}
			return CreateValueInternal(reader, propertyType, contractSafe, property, currentValue);
		}

		private object CreateValueNonProperty(JsonReader reader, Type objectType, JsonContract contract)
		{
			JsonConverter converter = GetConverter(contract, null);
			if (converter != null && converter.CanRead)
			{
				return converter.ReadJson(reader, objectType, null, GetInternalSerializer());
			}
			return CreateValueInternal(reader, objectType, contract, null, null);
		}

		private object CreateValueInternal(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, object existingValue)
		{
			if (contract is JsonLinqContract)
			{
				return CreateJToken(reader, contract);
			}
			do
			{
				switch (reader.TokenType)
				{
				case JsonToken.StartObject:
					return CreateObject(reader, objectType, contract, member, existingValue);
				case JsonToken.StartArray:
					return CreateList(reader, objectType, contract, member, existingValue, null);
				case JsonToken.Integer:
				case JsonToken.Float:
				case JsonToken.Boolean:
				case JsonToken.Date:
				case JsonToken.Bytes:
					return EnsureType(reader.Value, objectType);
				case JsonToken.String:
					if (string.IsNullOrEmpty((string)reader.Value) && objectType != null && ReflectionUtils.IsNullableType(objectType))
					{
						return null;
					}
					if (objectType == typeof(byte[]))
					{
						return Convert.FromBase64String((string)reader.Value);
					}
					return EnsureType(reader.Value, objectType);
				case JsonToken.StartConstructor:
				case JsonToken.EndConstructor:
					return reader.Value.ToString();
				case JsonToken.Null:
				case JsonToken.Undefined:
					if (objectType == typeof(DBNull))
					{
						return DBNull.Value;
					}
					return EnsureType(reader.Value, objectType);
				case JsonToken.Raw:
					return new JRaw((string)reader.Value);
				default:
					throw new JsonSerializationException("Unexpected token while deserializing object: " + reader.TokenType);
				case JsonToken.Comment:
					break;
				}
			}
			while (reader.Read());
			throw new JsonSerializationException("Unexpected end when deserializing object.");
		}

		private JsonConverter GetConverter(JsonContract contract, JsonConverter memberConverter)
		{
			JsonConverter result = null;
			if (memberConverter != null)
			{
				result = memberConverter;
			}
			else if (contract != null)
			{
				JsonConverter matchingConverter;
				if (contract.Converter != null)
				{
					result = contract.Converter;
				}
				else if ((matchingConverter = base.Serializer.GetMatchingConverter(contract.UnderlyingType)) != null)
				{
					result = matchingConverter;
				}
				else if (contract.InternalConverter != null)
				{
					result = contract.InternalConverter;
				}
			}
			return result;
		}

		private object CreateObject(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, object existingValue)
		{
			CheckedRead(reader);
			string text = null;
			if (reader.TokenType == JsonToken.PropertyName)
			{
				bool flag;
				do
				{
					string a = reader.Value.ToString();
					if (string.Equals(a, "$ref", StringComparison.Ordinal))
					{
						CheckedRead(reader);
						if (reader.TokenType != JsonToken.String)
						{
							throw new JsonSerializationException("JSON reference {0} property must have a string value.".FormatWith(CultureInfo.InvariantCulture, "$ref"));
						}
						string reference = reader.Value.ToString();
						CheckedRead(reader);
						if (reader.TokenType == JsonToken.PropertyName)
						{
							throw new JsonSerializationException("Additional content found in JSON reference object. A JSON reference object should only have a {0} property.".FormatWith(CultureInfo.InvariantCulture, "$ref"));
						}
						return base.Serializer.ReferenceResolver.ResolveReference(reference);
					}
					if (string.Equals(a, "$type", StringComparison.Ordinal))
					{
						CheckedRead(reader);
						string text2 = reader.Value.ToString();
						CheckedRead(reader);
						if ((member?.TypeNameHandling ?? base.Serializer.TypeNameHandling) != 0)
						{
							ReflectionUtils.SplitFullyQualifiedTypeName(text2, out var typeName, out var assemblyName);
							Type type;
							try
							{
								type = base.Serializer.Binder.BindToType(assemblyName, typeName);
							}
							catch (Exception innerException)
							{
								throw new JsonSerializationException("Error resolving type specified in JSON '{0}'.".FormatWith(CultureInfo.InvariantCulture, text2), innerException);
							}
							if (type == null)
							{
								throw new JsonSerializationException("Type specified in JSON '{0}' was not resolved.".FormatWith(CultureInfo.InvariantCulture, text2));
							}
							if (objectType != null && !objectType.IsAssignableFrom(type))
							{
								throw new JsonSerializationException("Type specified in JSON '{0}' is not compatible with '{1}'.".FormatWith(CultureInfo.InvariantCulture, type.AssemblyQualifiedName, objectType.AssemblyQualifiedName));
							}
							objectType = type;
							contract = GetContractSafe(type);
						}
						flag = true;
					}
					else if (string.Equals(a, "$id", StringComparison.Ordinal))
					{
						CheckedRead(reader);
						text = reader.Value.ToString();
						CheckedRead(reader);
						flag = true;
					}
					else
					{
						if (string.Equals(a, "$values", StringComparison.Ordinal))
						{
							CheckedRead(reader);
							object result = CreateList(reader, objectType, contract, member, existingValue, text);
							CheckedRead(reader);
							return result;
						}
						flag = false;
					}
				}
				while (flag && reader.TokenType == JsonToken.PropertyName);
			}
			if (!HasDefinedType(objectType))
			{
				return CreateJObject(reader);
			}
			if (contract == null)
			{
				throw new JsonSerializationException("Could not resolve type '{0}' to a JsonContract.".FormatWith(CultureInfo.InvariantCulture, objectType));
			}
			JsonDictionaryContract jsonDictionaryContract = contract as JsonDictionaryContract;
			if (jsonDictionaryContract != null)
			{
				if (existingValue == null)
				{
					return CreateAndPopulateDictionary(reader, jsonDictionaryContract, text);
				}
				return PopulateDictionary(jsonDictionaryContract.CreateWrapper(existingValue), reader, jsonDictionaryContract, text);
			}
			JsonObjectContract jsonObjectContract = contract as JsonObjectContract;
			if (jsonObjectContract != null)
			{
				if (existingValue == null)
				{
					return CreateAndPopulateObject(reader, jsonObjectContract, text);
				}
				return PopulateObject(existingValue, reader, jsonObjectContract, text);
			}
			JsonISerializableContract jsonISerializableContract = contract as JsonISerializableContract;
			if (jsonISerializableContract != null)
			{
				return CreateISerializable(reader, jsonISerializableContract, text);
			}
			throw new JsonSerializationException("Cannot deserialize JSON object into type '{0}'.".FormatWith(CultureInfo.InvariantCulture, objectType));
		}

		private JsonArrayContract EnsureArrayContract(Type objectType, JsonContract contract)
		{
			if (contract == null)
			{
				throw new JsonSerializationException("Could not resolve type '{0}' to a JsonContract.".FormatWith(CultureInfo.InvariantCulture, objectType));
			}
			JsonArrayContract jsonArrayContract = contract as JsonArrayContract;
			if (jsonArrayContract == null)
			{
				throw new JsonSerializationException("Cannot deserialize JSON array into type '{0}'.".FormatWith(CultureInfo.InvariantCulture, objectType));
			}
			return jsonArrayContract;
		}

		private void CheckedRead(JsonReader reader)
		{
			if (!reader.Read())
			{
				throw new JsonSerializationException("Unexpected end when deserializing object.");
			}
		}

		private object CreateList(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, object existingValue, string reference)
		{
			if (HasDefinedType(objectType))
			{
				JsonArrayContract jsonArrayContract = EnsureArrayContract(objectType, contract);
				if (existingValue == null)
				{
					return CreateAndPopulateList(reader, reference, jsonArrayContract);
				}
				return PopulateList(jsonArrayContract.CreateWrapper(existingValue), reader, reference, jsonArrayContract);
			}
			return CreateJToken(reader, contract);
		}

		private bool HasDefinedType(Type type)
		{
			if (type != null && type != typeof(object))
			{
				return !type.IsSubclassOf(typeof(JToken));
			}
			return false;
		}

		private object EnsureType(object value, Type targetType)
		{
			if (targetType == null)
			{
				return value;
			}
			Type objectType = ReflectionUtils.GetObjectType(value);
			if (objectType != targetType)
			{
				try
				{
					return ConvertUtils.ConvertOrCast(value, CultureInfo.InvariantCulture, targetType);
				}
				catch (Exception innerException)
				{
					throw new JsonSerializationException("Error converting value {0} to type '{1}'.".FormatWith(CultureInfo.InvariantCulture, FormatValueForPrint(value), targetType), innerException);
				}
			}
			return value;
		}

		private string FormatValueForPrint(object value)
		{
			if (value == null)
			{
				return "{null}";
			}
			if (value is string)
			{
				return string.Concat("\"", value, "\"");
			}
			return value.ToString();
		}

		private void SetPropertyValue(JsonProperty property, JsonReader reader, object target)
		{
			if (property.Ignored)
			{
				reader.Skip();
				return;
			}
			object obj = null;
			bool flag = false;
			bool gottenCurrentValue = false;
			ObjectCreationHandling valueOrDefault = property.ObjectCreationHandling.GetValueOrDefault(base.Serializer.ObjectCreationHandling);
			if ((valueOrDefault == ObjectCreationHandling.Auto || valueOrDefault == ObjectCreationHandling.Reuse) && (reader.TokenType == JsonToken.StartArray || reader.TokenType == JsonToken.StartObject) && property.Readable)
			{
				obj = property.ValueProvider.GetValue(target);
				gottenCurrentValue = true;
				flag = obj != null && !property.PropertyType.IsArray && !ReflectionUtils.InheritsGenericDefinition(property.PropertyType, typeof(ReadOnlyCollection<>));
			}
			if (!property.Writable && !flag)
			{
				reader.Skip();
				return;
			}
			if (property.NullValueHandling.GetValueOrDefault(base.Serializer.NullValueHandling) == NullValueHandling.Ignore && reader.TokenType == JsonToken.Null)
			{
				reader.Skip();
				return;
			}
			if (property.DefaultValueHandling.GetValueOrDefault(base.Serializer.DefaultValueHandling) == DefaultValueHandling.Ignore && JsonReader.IsPrimitiveToken(reader.TokenType) && object.Equals(reader.Value, property.DefaultValue))
			{
				reader.Skip();
				return;
			}
			object currentValue = (flag ? obj : null);
			object obj2 = CreateValueProperty(reader, property, target, gottenCurrentValue, currentValue);
			if ((!flag || obj2 != obj) && ShouldSetPropertyValue(property, obj2))
			{
				property.ValueProvider.SetValue(target, obj2);
			}
		}

		private bool ShouldSetPropertyValue(JsonProperty property, object value)
		{
			if (property.NullValueHandling.GetValueOrDefault(base.Serializer.NullValueHandling) == NullValueHandling.Ignore && value == null)
			{
				return false;
			}
			if (property.DefaultValueHandling.GetValueOrDefault(base.Serializer.DefaultValueHandling) == DefaultValueHandling.Ignore && object.Equals(value, property.DefaultValue))
			{
				return false;
			}
			if (!property.Writable)
			{
				return false;
			}
			return true;
		}

		private object CreateAndPopulateDictionary(JsonReader reader, JsonDictionaryContract contract, string id)
		{
			if (contract.DefaultCreator != null && (!contract.DefaultCreatorNonPublic || base.Serializer.ConstructorHandling == ConstructorHandling.AllowNonPublicDefaultConstructor))
			{
				object dictionary = contract.DefaultCreator();
				IWrappedDictionary wrappedDictionary = contract.CreateWrapper(dictionary);
				PopulateDictionary(wrappedDictionary, reader, contract, id);
				return wrappedDictionary.UnderlyingDictionary;
			}
			throw new JsonSerializationException("Unable to find a default constructor to use for type {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
		}

		private object PopulateDictionary(IWrappedDictionary dictionary, JsonReader reader, JsonDictionaryContract contract, string id)
		{
			if (id != null)
			{
				base.Serializer.ReferenceResolver.AddReference(id, dictionary.UnderlyingDictionary);
			}
			contract.InvokeOnDeserializing(dictionary.UnderlyingDictionary, base.Serializer.Context);
			int depth = reader.Depth;
			do
			{
				switch (reader.TokenType)
				{
				case JsonToken.PropertyName:
				{
					object obj;
					try
					{
						obj = EnsureType(reader.Value, contract.DictionaryKeyType);
					}
					catch (Exception innerException)
					{
						throw new JsonSerializationException("Could not convert string '{0}' to dictionary key type '{1}'. Create a TypeConverter to convert from the string to the key type object.".FormatWith(CultureInfo.InvariantCulture, reader.Value, contract.DictionaryKeyType), innerException);
					}
					CheckedRead(reader);
					try
					{
						dictionary[obj] = CreateValueNonProperty(reader, contract.DictionaryValueType, GetContractSafe(contract.DictionaryValueType));
					}
					catch (Exception ex)
					{
						if (IsErrorHandled(dictionary, contract, obj, ex))
						{
							HandleError(reader, depth);
							break;
						}
						throw;
					}
					break;
				}
				case JsonToken.EndObject:
					contract.InvokeOnDeserialized(dictionary.UnderlyingDictionary, base.Serializer.Context);
					return dictionary.UnderlyingDictionary;
				default:
					throw new JsonSerializationException("Unexpected token when deserializing object: " + reader.TokenType);
				}
			}
			while (reader.Read());
			throw new JsonSerializationException("Unexpected end when deserializing object.");
		}

		private object CreateAndPopulateList(JsonReader reader, string reference, JsonArrayContract contract)
		{
			return CollectionUtils.CreateAndPopulateList(contract.CreatedType, delegate(IList l, bool isTemporaryListReference)
			{
				if (reference != null && isTemporaryListReference)
				{
					throw new JsonSerializationException("Cannot preserve reference to array or readonly list: {0}".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
				}
				if (contract.OnSerializing != null && isTemporaryListReference)
				{
					throw new JsonSerializationException("Cannot call OnSerializing on an array or readonly list: {0}".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
				}
				if (contract.OnError != null && isTemporaryListReference)
				{
					throw new JsonSerializationException("Cannot call OnError on an array or readonly list: {0}".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
				}
				PopulateList(contract.CreateWrapper(l), reader, reference, contract);
			});
		}

		private object PopulateList(IWrappedCollection wrappedList, JsonReader reader, string reference, JsonArrayContract contract)
		{
			object underlyingCollection = wrappedList.UnderlyingCollection;
			if (reference != null)
			{
				base.Serializer.ReferenceResolver.AddReference(reference, underlyingCollection);
			}
			contract.InvokeOnDeserializing(underlyingCollection, base.Serializer.Context);
			int depth = reader.Depth;
			while (reader.Read())
			{
				switch (reader.TokenType)
				{
				case JsonToken.EndArray:
					contract.InvokeOnDeserialized(underlyingCollection, base.Serializer.Context);
					return wrappedList.UnderlyingCollection;
				case JsonToken.Comment:
					continue;
				}
				try
				{
					object value = CreateValueNonProperty(reader, contract.CollectionItemType, GetContractSafe(contract.CollectionItemType));
					wrappedList.Add(value);
				}
				catch (Exception ex)
				{
					if (IsErrorHandled(underlyingCollection, contract, wrappedList.Count, ex))
					{
						HandleError(reader, depth);
						continue;
					}
					throw;
				}
			}
			throw new JsonSerializationException("Unexpected end when deserializing array.");
		}

		private object CreateISerializable(JsonReader reader, JsonISerializableContract contract, string id)
		{
			Type underlyingType = contract.UnderlyingType;
			SerializationInfo serializationInfo = new SerializationInfo(contract.UnderlyingType, GetFormatterConverter());
			bool flag = false;
			do
			{
				switch (reader.TokenType)
				{
				case JsonToken.PropertyName:
				{
					string text = reader.Value.ToString();
					if (!reader.Read())
					{
						throw new JsonSerializationException("Unexpected end when setting {0}'s value.".FormatWith(CultureInfo.InvariantCulture, text));
					}
					serializationInfo.AddValue(text, JToken.ReadFrom(reader));
					break;
				}
				case JsonToken.EndObject:
					flag = true;
					break;
				default:
					throw new JsonSerializationException("Unexpected token when deserializing object: " + reader.TokenType);
				}
			}
			while (!flag && reader.Read());
			if (contract.ISerializableCreator == null)
			{
				throw new JsonSerializationException("ISerializable type '{0}' does not have a valid constructor.".FormatWith(CultureInfo.InvariantCulture, underlyingType));
			}
			object obj = contract.ISerializableCreator(serializationInfo, base.Serializer.Context);
			if (id != null)
			{
				base.Serializer.ReferenceResolver.AddReference(id, obj);
			}
			contract.InvokeOnDeserializing(obj, base.Serializer.Context);
			contract.InvokeOnDeserialized(obj, base.Serializer.Context);
			return obj;
		}

		private object CreateAndPopulateObject(JsonReader reader, JsonObjectContract contract, string id)
		{
			object obj = null;
			if (contract.UnderlyingType.IsInterface || contract.UnderlyingType.IsAbstract)
			{
				throw new JsonSerializationException("Could not create an instance of type {0}. Type is an interface or abstract class and cannot be instantated.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
			}
			if (contract.DefaultCreator != null && (!contract.DefaultCreatorNonPublic || base.Serializer.ConstructorHandling == ConstructorHandling.AllowNonPublicDefaultConstructor))
			{
				obj = contract.DefaultCreator();
			}
			if (obj != null)
			{
				PopulateObject(obj, reader, contract, id);
				return obj;
			}
			return CreateObjectFromNonDefaultConstructor(reader, contract, id);
		}

		private object CreateObjectFromNonDefaultConstructor(JsonReader reader, JsonObjectContract contract, string id)
		{
			Type underlyingType = contract.UnderlyingType;
			if (contract.ParametrizedConstructor == null)
			{
				throw new JsonSerializationException("Unable to find a constructor to use for type {0}. A class should either have a default constructor or only one constructor with arguments.".FormatWith(CultureInfo.InvariantCulture, underlyingType));
			}
			IDictionary<JsonProperty, object> dictionary = contract.Properties.Where((JsonProperty p) => !p.Ignored).ToDictionary((Func<JsonProperty, JsonProperty>)((JsonProperty kv) => kv), (Func<JsonProperty, object>)((JsonProperty kv) => null));
			bool flag = false;
			do
			{
				switch (reader.TokenType)
				{
				case JsonToken.PropertyName:
				{
					string text = reader.Value.ToString();
					if (!reader.Read())
					{
						throw new JsonSerializationException("Unexpected end when setting {0}'s value.".FormatWith(CultureInfo.InvariantCulture, text));
					}
					JsonProperty closestMatchProperty = contract.Properties.GetClosestMatchProperty(text);
					if (closestMatchProperty != null)
					{
						if (!closestMatchProperty.Ignored)
						{
							dictionary[closestMatchProperty] = CreateValueProperty(reader, closestMatchProperty, null, gottenCurrentValue: true, null);
						}
						else
						{
							reader.Skip();
						}
						break;
					}
					if (base.Serializer.MissingMemberHandling == MissingMemberHandling.Error)
					{
						throw new JsonSerializationException("Could not find member '{0}' on object of type '{1}'".FormatWith(CultureInfo.InvariantCulture, text, underlyingType.Name));
					}
					reader.Skip();
					break;
				}
				case JsonToken.EndObject:
					flag = true;
					break;
				default:
					throw new JsonSerializationException("Unexpected token when deserializing object: " + reader.TokenType);
				}
			}
			while (!flag && reader.Read());
			IDictionary<ParameterInfo, object> dictionary2 = ((IEnumerable<ParameterInfo>)contract.ParametrizedConstructor.GetParameters()).ToDictionary((Func<ParameterInfo, ParameterInfo>)((ParameterInfo p) => p), (Func<ParameterInfo, object>)((ParameterInfo p) => null));
			IDictionary<JsonProperty, object> dictionary3 = new Dictionary<JsonProperty, object>();
			foreach (KeyValuePair<JsonProperty, object> item in dictionary)
			{
				ParameterInfo key = dictionary2.ForgivingCaseSensitiveFind((KeyValuePair<ParameterInfo, object> kv) => kv.Key.Name, item.Key.PropertyName).Key;
				if (key != null)
				{
					dictionary2[key] = item.Value;
				}
				else
				{
					dictionary3.Add(item);
				}
			}
			object obj = contract.ParametrizedConstructor.Invoke(dictionary2.Values.ToArray());
			if (id != null)
			{
				base.Serializer.ReferenceResolver.AddReference(id, obj);
			}
			contract.InvokeOnDeserializing(obj, base.Serializer.Context);
			foreach (KeyValuePair<JsonProperty, object> item2 in dictionary3)
			{
				JsonProperty key2 = item2.Key;
				object value = item2.Value;
				if (ShouldSetPropertyValue(item2.Key, item2.Value))
				{
					key2.ValueProvider.SetValue(obj, value);
				}
			}
			contract.InvokeOnDeserialized(obj, base.Serializer.Context);
			return obj;
		}

		private object PopulateObject(object newObject, JsonReader reader, JsonObjectContract contract, string id)
		{
			contract.InvokeOnDeserializing(newObject, base.Serializer.Context);
			Dictionary<JsonProperty, RequiredValue> dictionary = contract.Properties.Where((JsonProperty m) => m.Required != Required.Default).ToDictionary((JsonProperty m) => m, (JsonProperty m) => RequiredValue.None);
			if (id != null)
			{
				base.Serializer.ReferenceResolver.AddReference(id, newObject);
			}
			int depth = reader.Depth;
			do
			{
				switch (reader.TokenType)
				{
				case JsonToken.PropertyName:
				{
					string text = reader.Value.ToString();
					JsonProperty closestMatchProperty = contract.Properties.GetClosestMatchProperty(text);
					if (closestMatchProperty == null)
					{
						if (base.Serializer.MissingMemberHandling == MissingMemberHandling.Error)
						{
							throw new JsonSerializationException("Could not find member '{0}' on object of type '{1}'".FormatWith(CultureInfo.InvariantCulture, text, contract.UnderlyingType.Name));
						}
						reader.Skip();
						break;
					}
					if (closestMatchProperty.PropertyType == typeof(byte[]))
					{
						reader.ReadAsBytes();
					}
					else if (!reader.Read())
					{
						throw new JsonSerializationException("Unexpected end when setting {0}'s value.".FormatWith(CultureInfo.InvariantCulture, text));
					}
					SetRequiredProperty(reader, closestMatchProperty, dictionary);
					try
					{
						SetPropertyValue(closestMatchProperty, reader, newObject);
					}
					catch (Exception ex)
					{
						if (IsErrorHandled(newObject, contract, text, ex))
						{
							HandleError(reader, depth);
							break;
						}
						throw;
					}
					break;
				}
				case JsonToken.EndObject:
					foreach (KeyValuePair<JsonProperty, RequiredValue> item in dictionary)
					{
						if (item.Value == RequiredValue.None)
						{
							throw new JsonSerializationException("Required property '{0}' not found in JSON.".FormatWith(CultureInfo.InvariantCulture, item.Key.PropertyName));
						}
						if (item.Key.Required == Required.Always && item.Value == RequiredValue.Null)
						{
							throw new JsonSerializationException("Required property '{0}' expects a value but got null.".FormatWith(CultureInfo.InvariantCulture, item.Key.PropertyName));
						}
					}
					contract.InvokeOnDeserialized(newObject, base.Serializer.Context);
					return newObject;
				default:
					throw new JsonSerializationException("Unexpected token when deserializing object: " + reader.TokenType);
				case JsonToken.Comment:
					break;
				}
			}
			while (reader.Read());
			throw new JsonSerializationException("Unexpected end when deserializing object.");
		}

		private void SetRequiredProperty(JsonReader reader, JsonProperty property, Dictionary<JsonProperty, RequiredValue> requiredProperties)
		{
			if (property != null)
			{
				requiredProperties[property] = ((reader.TokenType == JsonToken.Null || reader.TokenType == JsonToken.Undefined) ? RequiredValue.Null : RequiredValue.Value);
			}
		}

		private void HandleError(JsonReader reader, int initialDepth)
		{
			ClearErrorContext();
			reader.Skip();
			while (reader.Depth > initialDepth + 1)
			{
				reader.Read();
			}
		}
	}
}
