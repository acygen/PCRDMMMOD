using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Linq
{
	public abstract class JToken : IJEnumerable<JToken>, IEnumerable<JToken>, IEnumerable, IJsonLineInfo
	{
		private JContainer _parent;

		internal JToken _next;

		private static JTokenEqualityComparer _equalityComparer;

		private int? _lineNumber;

		private int? _linePosition;

		public static JTokenEqualityComparer EqualityComparer
		{
			get
			{
				if (_equalityComparer == null)
				{
					_equalityComparer = new JTokenEqualityComparer();
				}
				return _equalityComparer;
			}
		}

		public JContainer Parent
		{
			[DebuggerStepThrough]
			get
			{
				return _parent;
			}
			internal set
			{
				_parent = value;
			}
		}

		public JToken Root
		{
			get
			{
				JContainer parent = Parent;
				if (parent == null)
				{
					return this;
				}
				while (parent.Parent != null)
				{
					parent = parent.Parent;
				}
				return parent;
			}
		}

		public abstract JTokenType Type
		{
			get;
		}

		public abstract bool HasValues
		{
			get;
		}

		public JToken Next
		{
			get
			{
				if (_parent != null && _next != _parent.First)
				{
					return _next;
				}
				return null;
			}
			internal set
			{
				_next = value;
			}
		}

		public JToken Previous
		{
			get
			{
				if (_parent == null)
				{
					return null;
				}
				JToken next = _parent.Content._next;
				JToken result = null;
				while (next != this)
				{
					result = next;
					next = next.Next;
				}
				return result;
			}
		}

		public virtual JToken this[object key]
		{
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, GetType()));
			}
			set
			{
				throw new InvalidOperationException("Cannot set child value on {0}.".FormatWith(CultureInfo.InvariantCulture, GetType()));
			}
		}

		public virtual JToken First
		{
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, GetType()));
			}
		}

		public virtual JToken Last
		{
			get
			{
				throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, GetType()));
			}
		}

		IJEnumerable<JToken> IJEnumerable<JToken>.this[object key] => this[key];

		int IJsonLineInfo.LineNumber => _lineNumber ?? 0;

		int IJsonLineInfo.LinePosition => _linePosition ?? 0;

		internal abstract JToken CloneToken();

		internal abstract bool DeepEquals(JToken node);

		public static bool DeepEquals(JToken t1, JToken t2)
		{
			if (t1 != t2)
			{
				if (t1 != null && t2 != null)
				{
					return t1.DeepEquals(t2);
				}
				return false;
			}
			return true;
		}

		internal JToken()
		{
		}

		public void AddAfterSelf(object content)
		{
			if (_parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			_parent.AddInternal(Next == null, this, content);
		}

		public void AddBeforeSelf(object content)
		{
			if (_parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			JToken jToken = Previous;
			if (jToken == null)
			{
				jToken = _parent.Last;
			}
			_parent.AddInternal(isLast: false, jToken, content);
		}

		public IEnumerable<JToken> Ancestors()
		{
			for (JToken parent = Parent; parent != null; parent = parent.Parent)
			{
				yield return parent;
			}
		}

		public IEnumerable<JToken> AfterSelf()
		{
			if (Parent != null)
			{
				for (JToken o = Next; o != null; o = o.Next)
				{
					yield return o;
				}
			}
		}

		public IEnumerable<JToken> BeforeSelf()
		{
			for (JToken o = Parent.First; o != this; o = o.Next)
			{
				yield return o;
			}
		}

		public virtual T Value<T>(object key)
		{
			JToken token = this[key];
			return token.Convert<JToken, T>();
		}

		public virtual JEnumerable<JToken> Children()
		{
			throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, GetType()));
		}

		public JEnumerable<T> Children<T>() where T : JToken
		{
			return new JEnumerable<T>(Children().OfType<T>());
		}

		public virtual IEnumerable<T> Values<T>()
		{
			throw new InvalidOperationException("Cannot access child value on {0}.".FormatWith(CultureInfo.InvariantCulture, GetType()));
		}

		public void Remove()
		{
			if (_parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			_parent.RemoveItem(this);
		}

		public void Replace(JToken value)
		{
			if (_parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			_parent.ReplaceItem(this, value);
		}

		public abstract void WriteTo(JsonWriter writer, params JsonConverter[] converters);

		public override string ToString()
		{
			return ToString(Formatting.Indented);
		}

		public string ToString(Formatting formatting, params JsonConverter[] converters)
		{
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter);
				jsonTextWriter.Formatting = formatting;
				WriteTo(jsonTextWriter, converters);
				return stringWriter.ToString();
			}
		}

		private static JValue EnsureValue(JToken value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value is JProperty)
			{
				value = ((JProperty)value).Value;
			}
			return value as JValue;
		}

		private static string GetType(JToken token)
		{
			ValidationUtils.ArgumentNotNull(token, "token");
			if (token is JProperty)
			{
				token = ((JProperty)token).Value;
			}
			return token.Type.ToString();
		}

		private static bool IsNullable(JToken o)
		{
			if (o.Type != JTokenType.Undefined)
			{
				return o.Type == JTokenType.Null;
			}
			return true;
		}

		private static bool ValidateFloat(JToken o, bool nullable)
		{
			if (o.Type != JTokenType.Float && o.Type != JTokenType.Integer)
			{
				if (nullable)
				{
					return IsNullable(o);
				}
				return false;
			}
			return true;
		}

		private static bool ValidateInteger(JToken o, bool nullable)
		{
			if (o.Type != JTokenType.Integer)
			{
				if (nullable)
				{
					return IsNullable(o);
				}
				return false;
			}
			return true;
		}

		private static bool ValidateDate(JToken o, bool nullable)
		{
			if (o.Type != JTokenType.Date)
			{
				if (nullable)
				{
					return IsNullable(o);
				}
				return false;
			}
			return true;
		}

		private static bool ValidateBoolean(JToken o, bool nullable)
		{
			if (o.Type != JTokenType.Boolean)
			{
				if (nullable)
				{
					return IsNullable(o);
				}
				return false;
			}
			return true;
		}

		private static bool ValidateString(JToken o)
		{
			if (o.Type != JTokenType.String && o.Type != JTokenType.Comment && o.Type != JTokenType.Raw)
			{
				return IsNullable(o);
			}
			return true;
		}

		private static bool ValidateBytes(JToken o)
		{
			if (o.Type != JTokenType.Bytes)
			{
				return IsNullable(o);
			}
			return true;
		}

		public static explicit operator bool(JToken value)
		{
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateBoolean(jValue, nullable: false))
			{
				throw new ArgumentException("Can not convert {0} to Boolean.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			return (bool)jValue.Value;
		}

		public static explicit operator bool?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateBoolean(jValue, nullable: true))
			{
				throw new ArgumentException("Can not convert {0} to Boolean.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			return (bool?)jValue.Value;
		}

		public static explicit operator long(JToken value)
		{
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateInteger(jValue, nullable: false))
			{
				throw new ArgumentException("Can not convert {0} to Int64.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			return (long)jValue.Value;
		}

		public static explicit operator DateTime?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateDate(jValue, nullable: true))
			{
				throw new ArgumentException("Can not convert {0} to DateTime.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			return (DateTime?)jValue.Value;
		}

		public static explicit operator decimal?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateFloat(jValue, nullable: true))
			{
				throw new ArgumentException("Can not convert {0} to Decimal.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value == null)
			{
				return null;
			}
			return Convert.ToDecimal(jValue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator double?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateFloat(jValue, nullable: true))
			{
				throw new ArgumentException("Can not convert {0} to Double.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			return (double?)jValue.Value;
		}

		public static explicit operator int(JToken value)
		{
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateInteger(jValue, nullable: false))
			{
				throw new ArgumentException("Can not convert {0} to Int32.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			return Convert.ToInt32(jValue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator int?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateInteger(jValue, nullable: true))
			{
				throw new ArgumentException("Can not convert {0} to Int32.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value == null)
			{
				return null;
			}
			return Convert.ToInt32(jValue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator DateTime(JToken value)
		{
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateDate(jValue, nullable: false))
			{
				throw new ArgumentException("Can not convert {0} to DateTime.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			return (DateTime)jValue.Value;
		}

		public static explicit operator long?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateInteger(jValue, nullable: true))
			{
				throw new ArgumentException("Can not convert {0} to Int64.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			return (long?)jValue.Value;
		}

		public static explicit operator float?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateFloat(jValue, nullable: true))
			{
				throw new ArgumentException("Can not convert {0} to Single.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			if (jValue.Value == null)
			{
				return null;
			}
			return Convert.ToSingle(jValue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator decimal(JToken value)
		{
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateFloat(jValue, nullable: false))
			{
				throw new ArgumentException("Can not convert {0} to Decimal.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			return Convert.ToDecimal(jValue.Value, CultureInfo.InvariantCulture);
		}

		[CLSCompliant(false)]
		public static explicit operator uint?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateInteger(jValue, nullable: true))
			{
				throw new ArgumentException("Can not convert {0} to UInt32.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			return (uint?)jValue.Value;
		}

		[CLSCompliant(false)]
		public static explicit operator ulong?(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateInteger(jValue, nullable: true))
			{
				throw new ArgumentException("Can not convert {0} to UInt64.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			return (ulong?)jValue.Value;
		}

		public static explicit operator double(JToken value)
		{
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateFloat(jValue, nullable: false))
			{
				throw new ArgumentException("Can not convert {0} to Double.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			return (double)jValue.Value;
		}

		public static explicit operator float(JToken value)
		{
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateFloat(jValue, nullable: false))
			{
				throw new ArgumentException("Can not convert {0} to Single.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			return Convert.ToSingle(jValue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator string(JToken value)
		{
			if (value == null)
			{
				return null;
			}
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateString(jValue))
			{
				throw new ArgumentException("Can not convert {0} to String.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			return (string)jValue.Value;
		}

		[CLSCompliant(false)]
		public static explicit operator uint(JToken value)
		{
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateInteger(jValue, nullable: false))
			{
				throw new ArgumentException("Can not convert {0} to UInt32.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			return Convert.ToUInt32(jValue.Value, CultureInfo.InvariantCulture);
		}

		[CLSCompliant(false)]
		public static explicit operator ulong(JToken value)
		{
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateInteger(jValue, nullable: false))
			{
				throw new ArgumentException("Can not convert {0} to UInt64.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			return Convert.ToUInt64(jValue.Value, CultureInfo.InvariantCulture);
		}

		public static explicit operator byte[](JToken value)
		{
			JValue jValue = EnsureValue(value);
			if (jValue == null || !ValidateBytes(jValue))
			{
				throw new ArgumentException("Can not convert {0} to byte array.".FormatWith(CultureInfo.InvariantCulture, GetType(value)));
			}
			return (byte[])jValue.Value;
		}

		public static implicit operator JToken(bool value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(bool? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(long value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(DateTime? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(decimal? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(double? value)
		{
			return new JValue(value);
		}

		[CLSCompliant(false)]
		public static implicit operator JToken(ushort value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(int value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(int? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(DateTime value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(long? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(float? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(decimal value)
		{
			return new JValue(value);
		}

		[CLSCompliant(false)]
		public static implicit operator JToken(ushort? value)
		{
			return new JValue(value);
		}

		[CLSCompliant(false)]
		public static implicit operator JToken(uint? value)
		{
			return new JValue(value);
		}

		[CLSCompliant(false)]
		public static implicit operator JToken(ulong? value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(double value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(float value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(string value)
		{
			return new JValue(value);
		}

		[CLSCompliant(false)]
		public static implicit operator JToken(uint value)
		{
			return new JValue(value);
		}

		[CLSCompliant(false)]
		public static implicit operator JToken(ulong value)
		{
			return new JValue(value);
		}

		public static implicit operator JToken(byte[] value)
		{
			return new JValue(value);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<JToken>)this).GetEnumerator();
		}

		IEnumerator<JToken> IEnumerable<JToken>.GetEnumerator()
		{
			return Children().GetEnumerator();
		}

		internal abstract int GetDeepHashCode();

		public JsonReader CreateReader()
		{
			return new JTokenReader(this);
		}

		internal static JToken FromObjectInternal(object o, JsonSerializer jsonSerializer)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			ValidationUtils.ArgumentNotNull(jsonSerializer, "jsonSerializer");
			using (JTokenWriter jTokenWriter = new JTokenWriter())
			{
				jsonSerializer.Serialize(jTokenWriter, o);
				return jTokenWriter.Token;
			}
		}

		public static JToken FromObject(object o)
		{
			return FromObjectInternal(o, new JsonSerializer());
		}

		public static JToken FromObject(object o, JsonSerializer jsonSerializer)
		{
			return FromObjectInternal(o, jsonSerializer);
		}

		public static JToken ReadFrom(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw new Exception("Error reading JToken from JsonReader.");
			}
			if (reader.TokenType == JsonToken.StartObject)
			{
				return JObject.Load(reader);
			}
			if (reader.TokenType == JsonToken.StartArray)
			{
				return JArray.Load(reader);
			}
			if (reader.TokenType == JsonToken.PropertyName)
			{
				return JProperty.Load(reader);
			}
			if (reader.TokenType == JsonToken.StartConstructor)
			{
				return JConstructor.Load(reader);
			}
			if (!JsonReader.IsStartToken(reader.TokenType))
			{
				return new JValue(reader.Value);
			}
			throw new Exception("Error reading JToken from JsonReader. Unexpected token: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
		}

		internal void SetLineInfo(IJsonLineInfo lineInfo)
		{
			if (lineInfo != null && lineInfo.HasLineInfo())
			{
				SetLineInfo(lineInfo.LineNumber, lineInfo.LinePosition);
			}
		}

		internal void SetLineInfo(int lineNumber, int linePosition)
		{
			_lineNumber = lineNumber;
			_linePosition = linePosition;
		}

		bool IJsonLineInfo.HasLineInfo()
		{
			if (_lineNumber.HasValue)
			{
				return _linePosition.HasValue;
			}
			return false;
		}

		public JToken SelectToken(string path)
		{
			return SelectToken(path, errorWhenNoMatch: false);
		}

		public JToken SelectToken(string path, bool errorWhenNoMatch)
		{
			JPath jPath = new JPath(path);
			return jPath.Evaluate(this, errorWhenNoMatch);
		}
	}
}
