using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Linq
{
	public class JProperty : JContainer
	{
		private readonly string _name;

		public string Name
		{
			[DebuggerStepThrough]
			get
			{
				return _name;
			}
		}

		public new JToken Value
		{
			[DebuggerStepThrough]
			get
			{
				return base.Content;
			}
			set
			{
				CheckReentrancy();
				JToken jToken = value ?? new JValue((object)null);
				if (base.Content == null)
				{
					jToken = (base.Content = EnsureParentToken(jToken));
					base.Content.Parent = this;
					base.Content.Next = base.Content;
				}
				else
				{
					base.Content.Replace(jToken);
				}
			}
		}

		public override JTokenType Type
		{
			[DebuggerStepThrough]
			get
			{
				return JTokenType.Property;
			}
		}

		internal override void ReplaceItem(JToken existing, JToken replacement)
		{
			if (!JContainer.IsTokenUnchanged(existing, replacement))
			{
				if (base.Parent != null)
				{
					((JObject)base.Parent).InternalPropertyChanging(this);
				}
				base.ReplaceItem(existing, replacement);
				if (base.Parent != null)
				{
					((JObject)base.Parent).InternalPropertyChanged(this);
				}
			}
		}

		public JProperty(JProperty other)
			: base(other)
		{
			_name = other.Name;
		}

		internal override void AddItem(bool isLast, JToken previous, JToken item)
		{
			if (Value != null)
			{
				throw new Exception("{0} cannot have multiple values.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
			}
			Value = item;
		}

		internal override JToken GetItem(int index)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return Value;
		}

		internal override void SetItem(int index, JToken item)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			Value = item;
		}

		internal override bool RemoveItem(JToken item)
		{
			throw new Exception("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		internal override void RemoveItemAt(int index)
		{
			throw new Exception("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		internal override void InsertItem(int index, JToken item)
		{
			throw new Exception("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		internal override bool ContainsItem(JToken item)
		{
			return Value == item;
		}

		internal override void ClearItems()
		{
			throw new Exception("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		public override JEnumerable<JToken> Children()
		{
			return new JEnumerable<JToken>(GetValueEnumerable());
		}

		private IEnumerable<JToken> GetValueEnumerable()
		{
			yield return Value;
		}

		internal override bool DeepEquals(JToken node)
		{
			JProperty jProperty = node as JProperty;
			if (jProperty != null && _name == jProperty.Name)
			{
				return ContentsEqual(jProperty);
			}
			return false;
		}

		internal override JToken CloneToken()
		{
			return new JProperty(this);
		}

		internal JProperty(string name)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			_name = name;
		}

		public JProperty(string name, params object[] content)
			: this(name, (object)content)
		{
		}

		public JProperty(string name, object content)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			_name = name;
			Value = (IsMultiContent(content) ? new JArray(content) : CreateFromContent(content));
		}

		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WritePropertyName(_name);
			Value.WriteTo(writer, converters);
		}

		internal override int GetDeepHashCode()
		{
			return _name.GetHashCode() ^ ((Value != null) ? Value.GetDeepHashCode() : 0);
		}

		public static JProperty Load(JsonReader reader)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw new Exception("Error reading JProperty from JsonReader.");
			}
			if (reader.TokenType != JsonToken.PropertyName)
			{
				throw new Exception("Error reading JProperty from JsonReader. Current JsonReader item is not a property: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JProperty jProperty = new JProperty((string)reader.Value);
			jProperty.SetLineInfo(reader as IJsonLineInfo);
			if (!reader.Read())
			{
				throw new Exception("Error reading JProperty from JsonReader.");
			}
			jProperty.ReadContentFrom(reader);
			return jProperty;
		}
	}
}
