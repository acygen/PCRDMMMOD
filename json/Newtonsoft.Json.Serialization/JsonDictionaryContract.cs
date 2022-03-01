using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Serialization
{
	public class JsonDictionaryContract : JsonContract
	{
		private readonly Type _genericCollectionDefinitionType;

		private Type _genericWrapperType;

		private MethodCall<object, object> _genericWrapperCreator;

		internal Type DictionaryKeyType
		{
			get;
			private set;
		}

		internal Type DictionaryValueType
		{
			get;
			private set;
		}

		public JsonDictionaryContract(Type underlyingType)
			: base(underlyingType)
		{
			Type keyType;
			Type valueType;
			if (ReflectionUtils.ImplementsGenericDefinition(underlyingType, typeof(IDictionary<, >), out _genericCollectionDefinitionType))
			{
				keyType = _genericCollectionDefinitionType.GetGenericArguments()[0];
				valueType = _genericCollectionDefinitionType.GetGenericArguments()[1];
			}
			else
			{
				ReflectionUtils.GetDictionaryKeyValueTypes(base.UnderlyingType, out keyType, out valueType);
			}
			DictionaryKeyType = keyType;
			DictionaryValueType = valueType;
			if (IsTypeGenericDictionaryInterface(base.UnderlyingType))
			{
				base.CreatedType = ReflectionUtils.MakeGenericType(typeof(Dictionary<, >), keyType, valueType);
			}
		}

		internal IWrappedDictionary CreateWrapper(object dictionary)
		{
			if (dictionary is IDictionary)
			{
				return new DictionaryWrapper<object, object>((IDictionary)dictionary);
			}
			if (_genericWrapperType == null)
			{
				_genericWrapperType = ReflectionUtils.MakeGenericType(typeof(DictionaryWrapper<, >), DictionaryKeyType, DictionaryValueType);
				ConstructorInfo constructor = _genericWrapperType.GetConstructor(new Type[1]
				{
					_genericCollectionDefinitionType
				});
				_genericWrapperCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(constructor);
			}
			return (IWrappedDictionary)_genericWrapperCreator(null, dictionary);
		}

		private bool IsTypeGenericDictionaryInterface(Type type)
		{
			if (!type.IsGenericType)
			{
				return false;
			}
			Type genericTypeDefinition = type.GetGenericTypeDefinition();
			return genericTypeDefinition == typeof(IDictionary<, >);
		}
	}
}
