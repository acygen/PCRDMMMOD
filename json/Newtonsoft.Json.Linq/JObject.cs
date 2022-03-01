using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft0.Json.Linq.ComponentModel;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Linq
{
	[TypeDescriptionProvider(typeof(JTypeDescriptionProvider))]
	public class JObject : JContainer, IDictionary<string, JToken>, ICollection<KeyValuePair<string, JToken>>, IEnumerable<KeyValuePair<string, JToken>>, IEnumerable, INotifyPropertyChanged
	{
		public override JTokenType Type => JTokenType.Object;

		public override JToken this[object key]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(key, "o");
				string text = key as string;
				if (text == null)
				{
					throw new ArgumentException("Accessed JObject values with invalid key value: {0}. Object property name expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				return this[text];
			}
			set
			{
				ValidationUtils.ArgumentNotNull(key, "o");
				string text = key as string;
				if (text == null)
				{
					throw new ArgumentException("Set JObject values with invalid key value: {0}. Object property name expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				this[text] = value;
			}
		}

		public JToken this[string propertyName]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(propertyName, "propertyName");
				return Property(propertyName)?.Value;
			}
			set
			{
				JProperty jProperty = Property(propertyName);
				if (jProperty != null)
				{
					jProperty.Value = value;
					return;
				}
				Add(new JProperty(propertyName, value));
				OnPropertyChanged(propertyName);
			}
		}

		ICollection<string> IDictionary<string, JToken>.Keys
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		ICollection<JToken> IDictionary<string, JToken>.Values
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int Count => Children().Count();

		bool ICollection<KeyValuePair<string, JToken>>.IsReadOnly => false;

		public event PropertyChangedEventHandler PropertyChanged;

		public JObject()
		{
		}

		public JObject(JObject other)
			: base(other)
		{
		}

		public JObject(params object[] content)
			: this((object)content)
		{
		}

		public JObject(object content)
		{
			Add(content);
		}

		internal override bool DeepEquals(JToken node)
		{
			JObject jObject = node as JObject;
			if (jObject != null)
			{
				return ContentsEqual(jObject);
			}
			return false;
		}

		internal override void ValidateToken(JToken o, JToken existing)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			if (o.Type != JTokenType.Property)
			{
				throw new ArgumentException("Can not add {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, o.GetType(), GetType()));
			}
			JProperty jProperty = (JProperty)o;
			foreach (JProperty item in Children())
			{
				if (item != existing && string.Equals(item.Name, jProperty.Name, StringComparison.Ordinal))
				{
					throw new ArgumentException("Can not add property {0} to {1}. Property with the same name already exists on object.".FormatWith(CultureInfo.InvariantCulture, jProperty.Name, GetType()));
				}
			}
		}

		internal void InternalPropertyChanged(JProperty childProperty)
		{
			OnPropertyChanged(childProperty.Name);
			OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, IndexOfItem(childProperty)));
		}

		internal void InternalPropertyChanging(JProperty childProperty)
		{
		}

		internal override JToken CloneToken()
		{
			return new JObject(this);
		}

		public IEnumerable<JProperty> Properties()
		{
			return Children().Cast<JProperty>();
		}

		public JProperty Property(string name)
		{
			return (from p in Properties()
				where string.Equals(p.Name, name, StringComparison.Ordinal)
				select p).SingleOrDefault();
		}

		public JEnumerable<JToken> PropertyValues()
		{
			return new JEnumerable<JToken>(from p in Properties()
				select p.Value);
		}

		public static JObject Load(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw new Exception("Error reading JObject from JsonReader.");
			}
			if (reader.TokenType != JsonToken.StartObject)
			{
				throw new Exception("Error reading JObject from JsonReader. Current JsonReader item is not an object: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JObject jObject = new JObject();
			jObject.SetLineInfo(reader as IJsonLineInfo);
			if (!reader.Read())
			{
				throw new Exception("Error reading JObject from JsonReader.");
			}
			jObject.ReadContentFrom(reader);
			return jObject;
		}

		public static JObject Parse(string json)
		{
			JsonReader reader = new JsonTextReader(new StringReader(json));
			return Load(reader);
		}

		public new static JObject FromObject(object o)
		{
			return FromObject(o, new JsonSerializer());
		}

		public new static JObject FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jToken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jToken != null && jToken.Type != JTokenType.Object)
			{
				throw new ArgumentException("Object serialized to {0}. JObject instance expected.".FormatWith(CultureInfo.InvariantCulture, jToken.Type));
			}
			return (JObject)jToken;
		}

		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartObject();
			foreach (JProperty item in ChildrenInternal())
			{
				item.WriteTo(writer, converters);
			}
			writer.WriteEndObject();
		}

		public void Add(string propertyName, JToken value)
		{
			Add(new JProperty(propertyName, value));
		}

		bool IDictionary<string, JToken>.ContainsKey(string key)
		{
			return Property(key) != null;
		}

		public bool Remove(string propertyName)
		{
			JProperty jProperty = Property(propertyName);
			if (jProperty == null)
			{
				return false;
			}
			jProperty.Remove();
			return true;
		}

		public bool TryGetValue(string propertyName, out JToken value)
		{
			JProperty jProperty = Property(propertyName);
			if (jProperty == null)
			{
				value = null;
				return false;
			}
			value = jProperty.Value;
			return true;
		}

		void ICollection<KeyValuePair<string, JToken>>.Add(KeyValuePair<string, JToken> item)
		{
			Add(new JProperty(item.Key, item.Value));
		}

		void ICollection<KeyValuePair<string, JToken>>.Clear()
		{
			RemoveAll();
		}

		bool ICollection<KeyValuePair<string, JToken>>.Contains(KeyValuePair<string, JToken> item)
		{
			JProperty jProperty = Property(item.Key);
			if (jProperty == null)
			{
				return false;
			}
			return jProperty.Value == item.Value;
		}

		void ICollection<KeyValuePair<string, JToken>>.CopyTo(KeyValuePair<string, JToken>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", "arrayIndex is less than 0.");
			}
			if (arrayIndex >= array.Length)
			{
				throw new ArgumentException("arrayIndex is equal to or greater than the length of array.");
			}
			if (Count > array.Length - arrayIndex)
			{
				throw new ArgumentException("The number of elements in the source JObject is greater than the available space from arrayIndex to the end of the destination array.");
			}
			int num = 0;
			foreach (JProperty item in Properties())
			{
				ref KeyValuePair<string, JToken> reference = ref array[arrayIndex + num];
				reference = new KeyValuePair<string, JToken>(item.Name, item.Value);
				num++;
			}
		}

		bool ICollection<KeyValuePair<string, JToken>>.Remove(KeyValuePair<string, JToken> item)
		{
			if (!((ICollection<KeyValuePair<string, JToken>>)this).Contains(item))
			{
				return false;
			}
			((IDictionary<string, JToken>)this).Remove(item.Key);
			return true;
		}

		internal override int GetDeepHashCode()
		{
			return ContentsHashCode();
		}

		public IEnumerator<KeyValuePair<string, JToken>> GetEnumerator()
		{
			foreach (JProperty property in Properties())
			{
				yield return new KeyValuePair<string, JToken>(property.Name, property.Value);
			}
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
