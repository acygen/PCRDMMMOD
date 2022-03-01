using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft0.Json.Linq;
using Newtonsoft0.Json.Schema;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json
{
	public class JsonValidatingReader : JsonReader, IJsonLineInfo
	{
		private class SchemaScope
		{
			private readonly JTokenType _tokenType;

			private readonly JsonSchemaModel _schema;

			private readonly Dictionary<string, bool> _requiredProperties;

			public string CurrentPropertyName
			{
				get;
				set;
			}

			public int ArrayItemCount
			{
				get;
				set;
			}

			public JsonSchemaModel Schema => _schema;

			public Dictionary<string, bool> RequiredProperties => _requiredProperties;

			public JTokenType TokenType => _tokenType;

			public SchemaScope(JTokenType tokenType, JsonSchemaModel schema)
			{
				_tokenType = tokenType;
				_schema = schema;
				if (_schema != null && _schema.Properties != null)
				{
					_requiredProperties = GetRequiredProperties(_schema).Distinct().ToDictionary((string p) => p, (string p) => false);
				}
				else
				{
					_requiredProperties = new Dictionary<string, bool>();
				}
			}

			private IEnumerable<string> GetRequiredProperties(JsonSchemaModel schema)
			{
				return from p in schema.Properties
					where !p.Value.Optional
					select p.Key;
			}
		}

		private readonly JsonReader _reader;

		private readonly Stack<SchemaScope> _stack;

		private JsonSchema _schema;

		private JsonSchemaModel _model;

		private SchemaScope _currentScope;

		public override object Value => _reader.Value;

		public override int Depth => _reader.Depth;

		public override char QuoteChar
		{
			get
			{
				return _reader.QuoteChar;
			}
			protected internal set
			{
			}
		}

		public override JsonToken TokenType => _reader.TokenType;

		public override Type ValueType => _reader.ValueType;

		private JsonSchemaModel CurrentSchema => _currentScope.Schema;

		private JsonSchemaModel CurrentMemberSchema
		{
			get
			{
				if (_currentScope == null)
				{
					return _model;
				}
				if (_currentScope.Schema == null)
				{
					return null;
				}
				switch (_currentScope.TokenType)
				{
				case JTokenType.None:
					return _currentScope.Schema;
				case JTokenType.Object:
				{
					if (_currentScope.CurrentPropertyName == null)
					{
						throw new Exception("CurrentPropertyName has not been set on scope.");
					}
					if (CurrentSchema.Properties != null && CurrentSchema.Properties.TryGetValue(_currentScope.CurrentPropertyName, out var value))
					{
						return value;
					}
					if (!CurrentSchema.AllowAdditionalProperties)
					{
						return null;
					}
					return CurrentSchema.AdditionalProperties;
				}
				case JTokenType.Array:
					if (!CollectionUtils.IsNullOrEmpty(CurrentSchema.Items))
					{
						if (CurrentSchema.Items.Count == 1)
						{
							return CurrentSchema.Items[0];
						}
						if (CurrentSchema.Items.Count > _currentScope.ArrayItemCount - 1)
						{
							return CurrentSchema.Items[_currentScope.ArrayItemCount - 1];
						}
					}
					if (!CurrentSchema.AllowAdditionalProperties)
					{
						return null;
					}
					return CurrentSchema.AdditionalProperties;
				case JTokenType.Constructor:
					return null;
				default:
					throw new ArgumentOutOfRangeException("TokenType", "Unexpected token type: {0}".FormatWith(CultureInfo.InvariantCulture, _currentScope.TokenType));
				}
			}
		}

		public JsonSchema Schema
		{
			get
			{
				return _schema;
			}
			set
			{
				if (TokenType != 0)
				{
					throw new Exception("Cannot change schema while validating JSON.");
				}
				_schema = value;
				_model = null;
			}
		}

		public JsonReader Reader => _reader;

		int IJsonLineInfo.LineNumber => (_reader as IJsonLineInfo)?.LineNumber ?? 0;

		int IJsonLineInfo.LinePosition => (_reader as IJsonLineInfo)?.LinePosition ?? 0;

		public event ValidationEventHandler ValidationEventHandler;

		private void Push(SchemaScope scope)
		{
			_stack.Push(scope);
			_currentScope = scope;
		}

		private SchemaScope Pop()
		{
			SchemaScope result = _stack.Pop();
			_currentScope = ((_stack.Count != 0) ? _stack.Peek() : null);
			return result;
		}

		private void RaiseError(string message, JsonSchemaModel schema)
		{
			string message2 = (((IJsonLineInfo)this).HasLineInfo() ? (message + " Line {0}, position {1}.".FormatWith(CultureInfo.InvariantCulture, ((IJsonLineInfo)this).LineNumber, ((IJsonLineInfo)this).LinePosition)) : message);
			OnValidationEvent(new JsonSchemaException(message2, null, ((IJsonLineInfo)this).LineNumber, ((IJsonLineInfo)this).LinePosition));
		}

		private void OnValidationEvent(JsonSchemaException exception)
		{
			ValidationEventHandler validationEventHandler = this.ValidationEventHandler;
			if (validationEventHandler != null)
			{
				validationEventHandler(this, new ValidationEventArgs(exception));
				return;
			}
			throw exception;
		}

		public JsonValidatingReader(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			_reader = reader;
			_stack = new Stack<SchemaScope>();
		}

		private void ValidateInEnumAndNotDisallowed(JsonSchemaModel schema)
		{
			if (schema != null)
			{
				JToken jToken = new JValue(_reader.Value);
				if (schema.Enum != null && !schema.Enum.ContainsValue(jToken, new JTokenEqualityComparer()))
				{
					RaiseError("Value {0} is not defined in enum.".FormatWith(CultureInfo.InvariantCulture, jToken), schema);
				}
				JsonSchemaType? currentNodeSchemaType = GetCurrentNodeSchemaType();
				if (currentNodeSchemaType.HasValue && JsonSchemaGenerator.HasFlag(schema.Disallow, currentNodeSchemaType.Value))
				{
					RaiseError("Type {0} is disallowed.".FormatWith(CultureInfo.InvariantCulture, currentNodeSchemaType), schema);
				}
			}
		}

		private JsonSchemaType? GetCurrentNodeSchemaType()
		{
			switch (_reader.TokenType)
			{
			case JsonToken.StartObject:
				return JsonSchemaType.Object;
			case JsonToken.StartArray:
				return JsonSchemaType.Array;
			case JsonToken.Integer:
				return JsonSchemaType.Integer;
			case JsonToken.Float:
				return JsonSchemaType.Float;
			case JsonToken.String:
				return JsonSchemaType.String;
			case JsonToken.Boolean:
				return JsonSchemaType.Boolean;
			case JsonToken.Null:
				return JsonSchemaType.Null;
			default:
				return null;
			}
		}

		public override byte[] ReadAsBytes()
		{
			byte[] result = _reader.ReadAsBytes();
			ValidateCurrentToken();
			return result;
		}

		public override bool Read()
		{
			if (!_reader.Read())
			{
				return false;
			}
			if (_reader.TokenType == JsonToken.Comment)
			{
				return true;
			}
			ValidateCurrentToken();
			return true;
		}

		private void ValidateCurrentToken()
		{
			if (_model == null)
			{
				JsonSchemaModelBuilder jsonSchemaModelBuilder = new JsonSchemaModelBuilder();
				_model = jsonSchemaModelBuilder.Build(_schema);
			}
			switch (_reader.TokenType)
			{
			case JsonToken.StartObject:
			{
				ProcessValue();
				JsonSchemaModel schema2 = (ValidateObject(CurrentMemberSchema) ? CurrentMemberSchema : null);
				Push(new SchemaScope(JTokenType.Object, schema2));
				break;
			}
			case JsonToken.StartArray:
			{
				ProcessValue();
				JsonSchemaModel schema = (ValidateArray(CurrentMemberSchema) ? CurrentMemberSchema : null);
				Push(new SchemaScope(JTokenType.Array, schema));
				break;
			}
			case JsonToken.StartConstructor:
				Push(new SchemaScope(JTokenType.Constructor, null));
				break;
			case JsonToken.PropertyName:
				ValidatePropertyName(CurrentSchema);
				break;
			case JsonToken.Integer:
				ProcessValue();
				ValidateInteger(CurrentMemberSchema);
				break;
			case JsonToken.Float:
				ProcessValue();
				ValidateFloat(CurrentMemberSchema);
				break;
			case JsonToken.String:
				ProcessValue();
				ValidateString(CurrentMemberSchema);
				break;
			case JsonToken.Boolean:
				ProcessValue();
				ValidateBoolean(CurrentMemberSchema);
				break;
			case JsonToken.Null:
				ProcessValue();
				ValidateNull(CurrentMemberSchema);
				break;
			case JsonToken.EndObject:
				ValidateEndObject(CurrentSchema);
				Pop();
				break;
			case JsonToken.EndArray:
				ValidateEndArray(CurrentSchema);
				Pop();
				break;
			case JsonToken.EndConstructor:
				Pop();
				break;
			default:
				throw new ArgumentOutOfRangeException();
			case JsonToken.Raw:
			case JsonToken.Undefined:
			case JsonToken.Date:
				break;
			}
		}

		private void ValidateEndObject(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			Dictionary<string, bool> requiredProperties = _currentScope.RequiredProperties;
			if (requiredProperties != null)
			{
				List<string> list = (from kv in requiredProperties
					where !kv.Value
					select kv.Key).ToList();
				if (list.Count > 0)
				{
					RaiseError("Non-optional properties are missing from object: {0}.".FormatWith(CultureInfo.InvariantCulture, string.Join(", ", list.ToArray())), schema);
				}
			}
		}

		private void ValidateEndArray(JsonSchemaModel schema)
		{
			if (schema != null)
			{
				int arrayItemCount = _currentScope.ArrayItemCount;
				if (schema.MaximumItems.HasValue && arrayItemCount > schema.MaximumItems)
				{
					RaiseError("Array item count {0} exceeds maximum count of {1}.".FormatWith(CultureInfo.InvariantCulture, arrayItemCount, schema.MaximumItems), schema);
				}
				if (schema.MinimumItems.HasValue && arrayItemCount < schema.MinimumItems)
				{
					RaiseError("Array item count {0} is less than minimum count of {1}.".FormatWith(CultureInfo.InvariantCulture, arrayItemCount, schema.MinimumItems), schema);
				}
			}
		}

		private void ValidateNull(JsonSchemaModel schema)
		{
			if (schema != null && TestType(schema, JsonSchemaType.Null))
			{
				ValidateInEnumAndNotDisallowed(schema);
			}
		}

		private void ValidateBoolean(JsonSchemaModel schema)
		{
			if (schema != null && TestType(schema, JsonSchemaType.Boolean))
			{
				ValidateInEnumAndNotDisallowed(schema);
			}
		}

		private void ValidateString(JsonSchemaModel schema)
		{
			if (schema == null || !TestType(schema, JsonSchemaType.String))
			{
				return;
			}
			ValidateInEnumAndNotDisallowed(schema);
			string text = _reader.Value.ToString();
			if (schema.MaximumLength.HasValue && text.Length > schema.MaximumLength)
			{
				RaiseError("String '{0}' exceeds maximum length of {1}.".FormatWith(CultureInfo.InvariantCulture, text, schema.MaximumLength), schema);
			}
			if (schema.MinimumLength.HasValue && text.Length < schema.MinimumLength)
			{
				RaiseError("String '{0}' is less than minimum length of {1}.".FormatWith(CultureInfo.InvariantCulture, text, schema.MinimumLength), schema);
			}
			if (schema.Patterns == null)
			{
				return;
			}
			foreach (string pattern in schema.Patterns)
			{
				if (!Regex.IsMatch(text, pattern))
				{
					RaiseError("String '{0}' does not match regex pattern '{1}'.".FormatWith(CultureInfo.InvariantCulture, text, pattern), schema);
				}
			}
		}

		private void ValidateInteger(JsonSchemaModel schema)
		{
			if (schema != null && TestType(schema, JsonSchemaType.Integer))
			{
				ValidateInEnumAndNotDisallowed(schema);
				long num = Convert.ToInt64(_reader.Value, CultureInfo.InvariantCulture);
				if (schema.Maximum.HasValue && (double)num > schema.Maximum)
				{
					RaiseError("Integer {0} exceeds maximum value of {1}.".FormatWith(CultureInfo.InvariantCulture, num, schema.Maximum), schema);
				}
				if (schema.Minimum.HasValue && (double)num < schema.Minimum)
				{
					RaiseError("Integer {0} is less than minimum value of {1}.".FormatWith(CultureInfo.InvariantCulture, num, schema.Minimum), schema);
				}
			}
		}

		private void ProcessValue()
		{
			if (_currentScope != null && _currentScope.TokenType == JTokenType.Array)
			{
				_currentScope.ArrayItemCount++;
				if (CurrentSchema != null && CurrentSchema.Items != null && CurrentSchema.Items.Count > 1 && _currentScope.ArrayItemCount >= CurrentSchema.Items.Count)
				{
					RaiseError("Index {0} has not been defined and the schema does not allow additional items.".FormatWith(CultureInfo.InvariantCulture, _currentScope.ArrayItemCount), CurrentSchema);
				}
			}
		}

		private void ValidateFloat(JsonSchemaModel schema)
		{
			if (schema != null && TestType(schema, JsonSchemaType.Float))
			{
				ValidateInEnumAndNotDisallowed(schema);
				double num = Convert.ToDouble(_reader.Value, CultureInfo.InvariantCulture);
				if (schema.Maximum.HasValue && num > schema.Maximum)
				{
					RaiseError("Float {0} exceeds maximum value of {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Maximum), schema);
				}
				if (schema.Minimum.HasValue && num < schema.Minimum)
				{
					RaiseError("Float {0} is less than minimum value of {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.Minimum), schema);
				}
				if (schema.MaximumDecimals.HasValue && MathUtils.GetDecimalPlaces(num) > schema.MaximumDecimals)
				{
					RaiseError("Float {0} exceeds the maximum allowed number decimal places of {1}.".FormatWith(CultureInfo.InvariantCulture, JsonConvert.ToString(num), schema.MaximumDecimals), schema);
				}
			}
		}

		private void ValidatePropertyName(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return;
			}
			string text = Convert.ToString(_reader.Value, CultureInfo.InvariantCulture);
			if (_currentScope.RequiredProperties.ContainsKey(text))
			{
				_currentScope.RequiredProperties[text] = true;
			}
			if (schema.Properties != null && !schema.Properties.ContainsKey(text))
			{
				IList<string> list = schema.Properties.Select((KeyValuePair<string, JsonSchemaModel> p) => p.Key).ToList();
				if (!schema.AllowAdditionalProperties && !list.Contains(text))
				{
					RaiseError("Property '{0}' has not been defined and the schema does not allow additional properties.".FormatWith(CultureInfo.InvariantCulture, text), schema);
				}
			}
			_currentScope.CurrentPropertyName = text;
		}

		private bool ValidateArray(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return true;
			}
			return TestType(schema, JsonSchemaType.Array);
		}

		private bool ValidateObject(JsonSchemaModel schema)
		{
			if (schema == null)
			{
				return true;
			}
			return TestType(schema, JsonSchemaType.Object);
		}

		private bool TestType(JsonSchemaModel currentSchema, JsonSchemaType currentType)
		{
			if (!JsonSchemaGenerator.HasFlag(currentSchema.Type, currentType))
			{
				RaiseError("Invalid type. Expected {0} but got {1}.".FormatWith(CultureInfo.InvariantCulture, currentSchema.Type, currentType), currentSchema);
				return false;
			}
			return true;
		}

		bool IJsonLineInfo.HasLineInfo()
		{
			return (_reader as IJsonLineInfo)?.HasLineInfo() ?? false;
		}
	}
}
