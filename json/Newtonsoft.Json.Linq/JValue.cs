using System;
using System.Globalization;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Linq
{
	public class JValue : JToken, IEquatable<JValue>
	{
		private JTokenType _valueType;

		private object _value;

		public override bool HasValues => false;

		public override JTokenType Type => _valueType;

		public new object Value
		{
			get
			{
				return _value;
			}
			set
			{
				Type type = ((_value != null) ? _value.GetType() : null);
				Type type2 = value?.GetType();
				if (type != type2)
				{
					_valueType = GetValueType(_valueType, value);
				}
				_value = value;
			}
		}

		internal JValue(object value, JTokenType type)
		{
			_value = value;
			_valueType = type;
		}

		public JValue(JValue other)
			: this(other.Value, other.Type)
		{
		}

		public JValue(long value)
			: this(value, JTokenType.Integer)
		{
		}

		[CLSCompliant(false)]
		public JValue(ulong value)
			: this(value, JTokenType.Integer)
		{
		}

		public JValue(double value)
			: this(value, JTokenType.Float)
		{
		}

		public JValue(DateTime value)
			: this(value, JTokenType.Date)
		{
		}

		public JValue(bool value)
			: this(value, JTokenType.Boolean)
		{
		}

		public JValue(string value)
			: this(value, JTokenType.String)
		{
		}

		public JValue(object value)
			: this(value, GetValueType(null, value))
		{
		}

		internal override bool DeepEquals(JToken node)
		{
			JValue jValue = node as JValue;
			if (jValue == null)
			{
				return false;
			}
			return ValuesEquals(this, jValue);
		}

		private static bool Compare(JTokenType valueType, object objA, object objB)
		{
			if (objA == null && objB == null)
			{
				return true;
			}
			if (objA == null || objB == null)
			{
				return false;
			}
			switch (valueType)
			{
			case JTokenType.Integer:
				if (objA is ulong || objB is ulong)
				{
					return Convert.ToDecimal(objA, CultureInfo.InvariantCulture).Equals(Convert.ToDecimal(objB, CultureInfo.InvariantCulture));
				}
				return Convert.ToInt64(objA, CultureInfo.InvariantCulture).Equals(Convert.ToInt64(objB, CultureInfo.InvariantCulture));
			case JTokenType.Float:
				return Convert.ToDouble(objA, CultureInfo.InvariantCulture).Equals(Convert.ToDouble(objB, CultureInfo.InvariantCulture));
			case JTokenType.Comment:
			case JTokenType.String:
			case JTokenType.Boolean:
			case JTokenType.Raw:
				return objA.Equals(objB);
			case JTokenType.Date:
				return objA.Equals(objB);
			case JTokenType.Bytes:
			{
				byte[] array = objA as byte[];
				byte[] array2 = objB as byte[];
				if (array == null || array2 == null)
				{
					return false;
				}
				return MiscellaneousUtils.ByteArrayCompare(array, array2);
			}
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("valueType", valueType, "Unexpected value type: {0}".FormatWith(CultureInfo.InvariantCulture, valueType));
			}
		}

		internal override JToken CloneToken()
		{
			return new JValue(this);
		}

		public static JValue CreateComment(string value)
		{
			return new JValue(value, JTokenType.Comment);
		}

		public static JValue CreateString(string value)
		{
			return new JValue(value, JTokenType.String);
		}

		private static JTokenType GetValueType(JTokenType? current, object value)
		{
			if (value == null)
			{
				return JTokenType.Null;
			}
			if (value == DBNull.Value)
			{
				return JTokenType.Null;
			}
			if (value is string)
			{
				return GetStringValueType(current);
			}
			if (value is long || value is int || value is short || value is sbyte || value is ulong || value is uint || value is ushort || value is byte)
			{
				return JTokenType.Integer;
			}
			if (value is Enum)
			{
				return JTokenType.Integer;
			}
			if (value is double || value is float || value is decimal)
			{
				return JTokenType.Float;
			}
			if (value is DateTime)
			{
				return JTokenType.Date;
			}
			if (value is byte[])
			{
				return JTokenType.Bytes;
			}
			if (value is bool)
			{
				return JTokenType.Boolean;
			}
			throw new ArgumentException("Could not determine JSON object type for type {0}.".FormatWith(CultureInfo.InvariantCulture, value.GetType()));
		}

		private static JTokenType GetStringValueType(JTokenType? current)
		{
			if (!current.HasValue)
			{
				return JTokenType.String;
			}
			JTokenType value = current.Value;
			if (value == JTokenType.Comment || value == JTokenType.String || value == JTokenType.Raw)
			{
				return current.Value;
			}
			return JTokenType.String;
		}

		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			switch (_valueType)
			{
			case JTokenType.Comment:
				writer.WriteComment(_value.ToString());
				return;
			case JTokenType.Raw:
				writer.WriteRawValue((_value != null) ? _value.ToString() : null);
				return;
			case JTokenType.Null:
				writer.WriteNull();
				return;
			case JTokenType.Undefined:
				writer.WriteUndefined();
				return;
			}
			JsonConverter matchingConverter;
			if (_value != null && (matchingConverter = JsonSerializer.GetMatchingConverter(converters, _value.GetType())) != null)
			{
				matchingConverter.WriteJson(writer, _value, new JsonSerializer());
				return;
			}
			switch (_valueType)
			{
			case JTokenType.Integer:
				writer.WriteValue(Convert.ToInt64(_value, CultureInfo.InvariantCulture));
				break;
			case JTokenType.Float:
				writer.WriteValue(Convert.ToDouble(_value, CultureInfo.InvariantCulture));
				break;
			case JTokenType.String:
				writer.WriteValue((_value != null) ? _value.ToString() : null);
				break;
			case JTokenType.Boolean:
				writer.WriteValue(Convert.ToBoolean(_value, CultureInfo.InvariantCulture));
				break;
			case JTokenType.Date:
				writer.WriteValue(Convert.ToDateTime(_value, CultureInfo.InvariantCulture));
				break;
			case JTokenType.Bytes:
				writer.WriteValue((byte[])_value);
				break;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("TokenType", _valueType, "Unexpected token type.");
			}
		}

		internal override int GetDeepHashCode()
		{
			int num = ((_value != null) ? _value.GetHashCode() : 0);
			return _valueType.GetHashCode() ^ num;
		}

		private static bool ValuesEquals(JValue v1, JValue v2)
		{
			if (v1 != v2)
			{
				if (v1._valueType == v2._valueType)
				{
					return Compare(v1._valueType, v1._value, v2._value);
				}
				return false;
			}
			return true;
		}

		public bool Equals(JValue other)
		{
			if (other == null)
			{
				return false;
			}
			return ValuesEquals(this, other);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			JValue jValue = obj as JValue;
			if (jValue != null)
			{
				return Equals(jValue);
			}
			return base.Equals(obj);
		}

		public override int GetHashCode()
		{
			if (_value == null)
			{
				return 0;
			}
			return _value.GetHashCode();
		}
	}
}
