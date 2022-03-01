using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Converters
{
	public class XmlNodeConverter : JsonConverter
	{
		private const string TextName = "#text";

		private const string CommentName = "#comment";

		private const string CDataName = "#cdata-section";

		private const string WhitespaceName = "#whitespace";

		private const string SignificantWhitespaceName = "#significant-whitespace";

		private const string DeclarationName = "?xml";

		private const string JsonNamespaceUri = "http://james.newtonking.com/projects/json";

		public string DeserializeRootElementName
		{
			get;
			set;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value is XmlNode)
			{
				IXmlNode node = new XmlNodeWrapper((XmlNode)value);
				XmlNamespaceManager manager = new XmlNamespaceManager(new NameTable());
				PushParentNamespaces(node, manager);
				writer.WriteStartObject();
				SerializeNode(writer, node, manager, writePropertyName: true);
				writer.WriteEndObject();
				return;
			}
			throw new ArgumentException("Value must be an XmlNode", "value");
		}

		private void PushParentNamespaces(IXmlNode node, XmlNamespaceManager manager)
		{
			List<IXmlNode> list = null;
			IXmlNode xmlNode = node;
			while ((xmlNode = xmlNode.ParentNode) != null)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					if (list == null)
					{
						list = new List<IXmlNode>();
					}
					list.Add(xmlNode);
				}
			}
			if (list == null)
			{
				return;
			}
			list.Reverse();
			foreach (IXmlNode item in list)
			{
				manager.PushScope();
				foreach (IXmlNode attribute in item.Attributes)
				{
					if (attribute.NamespaceURI == "http://www.w3.org/2000/xmlns/" && attribute.LocalName != "xmlns")
					{
						manager.AddNamespace(attribute.LocalName, attribute.Value);
					}
				}
			}
		}

		private string ResolveFullName(IXmlNode node, XmlNamespaceManager manager)
		{
			string text = ((node.NamespaceURI == null || (node.LocalName == "xmlns" && node.NamespaceURI == "http://www.w3.org/2000/xmlns/")) ? null : manager.LookupPrefix(node.NamespaceURI));
			if (!string.IsNullOrEmpty(text))
			{
				return text + ":" + node.LocalName;
			}
			return node.LocalName;
		}

		private string GetPropertyName(IXmlNode node, XmlNamespaceManager manager)
		{
			switch (node.NodeType)
			{
			case XmlNodeType.Attribute:
				if (node.NamespaceURI == "http://james.newtonking.com/projects/json")
				{
					return "$" + node.LocalName;
				}
				return "@" + ResolveFullName(node, manager);
			case XmlNodeType.CDATA:
				return "#cdata-section";
			case XmlNodeType.Comment:
				return "#comment";
			case XmlNodeType.Element:
				return ResolveFullName(node, manager);
			case XmlNodeType.ProcessingInstruction:
				return "?" + ResolveFullName(node, manager);
			case XmlNodeType.XmlDeclaration:
				return "?xml";
			case XmlNodeType.SignificantWhitespace:
				return "#significant-whitespace";
			case XmlNodeType.Text:
				return "#text";
			case XmlNodeType.Whitespace:
				return "#whitespace";
			default:
				throw new JsonSerializationException("Unexpected XmlNodeType when getting node name: " + node.NodeType);
			}
		}

		private void SerializeGroupedNodes(JsonWriter writer, IXmlNode node, XmlNamespaceManager manager)
		{
			Dictionary<string, List<IXmlNode>> dictionary = new Dictionary<string, List<IXmlNode>>();
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				IXmlNode xmlNode = node.ChildNodes[i];
				string propertyName = GetPropertyName(xmlNode, manager);
				if (!dictionary.TryGetValue(propertyName, out var value))
				{
					value = new List<IXmlNode>();
					dictionary.Add(propertyName, value);
				}
				value.Add(xmlNode);
			}
			foreach (KeyValuePair<string, List<IXmlNode>> item in dictionary)
			{
				List<IXmlNode> value2 = item.Value;
				bool flag;
				if (value2.Count == 1)
				{
					IXmlNode xmlNode2 = value2[0];
					IXmlNode xmlNode3 = ((xmlNode2.Attributes != null) ? xmlNode2.Attributes.SingleOrDefault((IXmlNode a) => a.LocalName == "Array" && a.NamespaceURI == "http://james.newtonking.com/projects/json") : null);
					flag = xmlNode3 != null && XmlConvert.ToBoolean(xmlNode3.Value);
				}
				else
				{
					flag = true;
				}
				if (!flag)
				{
					SerializeNode(writer, value2[0], manager, writePropertyName: true);
					continue;
				}
				string key = item.Key;
				writer.WritePropertyName(key);
				writer.WriteStartArray();
				for (int j = 0; j < value2.Count; j++)
				{
					SerializeNode(writer, value2[j], manager, writePropertyName: false);
				}
				writer.WriteEndArray();
			}
		}

		private void SerializeNode(JsonWriter writer, IXmlNode node, XmlNamespaceManager manager, bool writePropertyName)
		{
			switch (node.NodeType)
			{
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
				SerializeGroupedNodes(writer, node, manager);
				break;
			case XmlNodeType.Element:
			{
				foreach (IXmlNode attribute in node.Attributes)
				{
					if (attribute.NamespaceURI == "http://www.w3.org/2000/xmlns/")
					{
						string prefix = ((attribute.LocalName != "xmlns") ? attribute.LocalName : string.Empty);
						manager.AddNamespace(prefix, attribute.Value);
					}
				}
				if (writePropertyName)
				{
					writer.WritePropertyName(GetPropertyName(node, manager));
				}
				if (ValueAttributes(node.Attributes).Count() == 0 && node.ChildNodes.Count == 1 && node.ChildNodes[0].NodeType == XmlNodeType.Text)
				{
					writer.WriteValue(node.ChildNodes[0].Value);
					break;
				}
				if (node.ChildNodes.Count == 0 && CollectionUtils.IsNullOrEmpty(node.Attributes))
				{
					writer.WriteNull();
					break;
				}
				writer.WriteStartObject();
				for (int i = 0; i < node.Attributes.Count; i++)
				{
					SerializeNode(writer, node.Attributes[i], manager, writePropertyName: true);
				}
				SerializeGroupedNodes(writer, node, manager);
				writer.WriteEndObject();
				break;
			}
			case XmlNodeType.Comment:
				if (writePropertyName)
				{
					writer.WriteComment(node.Value);
				}
				break;
			case XmlNodeType.Attribute:
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				if ((!(node.NamespaceURI == "http://www.w3.org/2000/xmlns/") || !(node.Value == "http://james.newtonking.com/projects/json")) && (!(node.NamespaceURI == "http://james.newtonking.com/projects/json") || !(node.LocalName == "Array")))
				{
					if (writePropertyName)
					{
						writer.WritePropertyName(GetPropertyName(node, manager));
					}
					writer.WriteValue(node.Value);
				}
				break;
			case XmlNodeType.XmlDeclaration:
			{
				IXmlDeclaration xmlDeclaration = (IXmlDeclaration)node;
				writer.WritePropertyName(GetPropertyName(node, manager));
				writer.WriteStartObject();
				if (!string.IsNullOrEmpty(xmlDeclaration.Version))
				{
					writer.WritePropertyName("@version");
					writer.WriteValue(xmlDeclaration.Version);
				}
				if (!string.IsNullOrEmpty(xmlDeclaration.Encoding))
				{
					writer.WritePropertyName("@encoding");
					writer.WriteValue(xmlDeclaration.Encoding);
				}
				if (!string.IsNullOrEmpty(xmlDeclaration.Standalone))
				{
					writer.WritePropertyName("@standalone");
					writer.WriteValue(xmlDeclaration.Standalone);
				}
				writer.WriteEndObject();
				break;
			}
			default:
				throw new JsonSerializationException("Unexpected XmlNodeType when serializing nodes: " + node.NodeType);
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			XmlNamespaceManager manager = new XmlNamespaceManager(new NameTable());
			if (typeof(XmlNode).IsAssignableFrom(objectType))
			{
				if (objectType != typeof(XmlDocument))
				{
					throw new JsonSerializationException("XmlNodeConverter only supports deserializing XmlDocuments");
				}
				XmlDocument document = new XmlDocument();
				IXmlDocument xmlDocument = new XmlDocumentWrapper(document);
				IXmlNode currentNode = xmlDocument;
				if (reader.TokenType != JsonToken.StartObject)
				{
					throw new JsonSerializationException("XmlNodeConverter can only convert JSON that begins with an object.");
				}
				if (!string.IsNullOrEmpty(DeserializeRootElementName))
				{
					ReadElement(reader, xmlDocument, currentNode, DeserializeRootElementName, manager);
				}
				else
				{
					reader.Read();
					DeserializeNode(reader, xmlDocument, manager, currentNode);
				}
				return xmlDocument.WrappedNode;
			}
			throw new JsonSerializationException("Unexpected type when converting XML: " + objectType);
		}

		private void DeserializeValue(JsonReader reader, IXmlDocument document, XmlNamespaceManager manager, string propertyName, IXmlNode currentNode)
		{
			switch (propertyName)
			{
			case "#text":
				currentNode.AppendChild(document.CreateTextNode(reader.Value.ToString()));
				return;
			case "#cdata-section":
				currentNode.AppendChild(document.CreateCDataSection(reader.Value.ToString()));
				return;
			case "#whitespace":
				currentNode.AppendChild(document.CreateWhitespace(reader.Value.ToString()));
				return;
			case "#significant-whitespace":
				currentNode.AppendChild(document.CreateSignificantWhitespace(reader.Value.ToString()));
				return;
			}
			if (!string.IsNullOrEmpty(propertyName) && propertyName[0] == '?')
			{
				CreateInstruction(reader, document, currentNode, propertyName);
			}
			else if (reader.TokenType == JsonToken.StartArray)
			{
				ReadArrayElements(reader, document, propertyName, currentNode, manager);
			}
			else
			{
				ReadElement(reader, document, currentNode, propertyName, manager);
			}
		}

		private void ReadElement(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string propertyName, XmlNamespaceManager manager)
		{
			Dictionary<string, string> dictionary = ReadAttributeElements(reader, manager);
			string prefix = MiscellaneousUtils.GetPrefix(propertyName);
			IXmlElement xmlElement = CreateElement(propertyName, document, prefix, manager);
			currentNode.AppendChild(xmlElement);
			foreach (KeyValuePair<string, string> item in dictionary)
			{
				string prefix2 = MiscellaneousUtils.GetPrefix(item.Key);
				IXmlNode attributeNode = ((!string.IsNullOrEmpty(prefix2)) ? document.CreateAttribute(item.Key, manager.LookupNamespace(prefix2), item.Value) : document.CreateAttribute(item.Key, item.Value));
				xmlElement.SetAttributeNode(attributeNode);
			}
			if (reader.TokenType == JsonToken.String)
			{
				xmlElement.AppendChild(document.CreateTextNode(reader.Value.ToString()));
			}
			else if (reader.TokenType == JsonToken.Integer)
			{
				xmlElement.AppendChild(document.CreateTextNode(XmlConvert.ToString((long)reader.Value)));
			}
			else if (reader.TokenType == JsonToken.Float)
			{
				xmlElement.AppendChild(document.CreateTextNode(XmlConvert.ToString((double)reader.Value)));
			}
			else if (reader.TokenType == JsonToken.Boolean)
			{
				xmlElement.AppendChild(document.CreateTextNode(XmlConvert.ToString((bool)reader.Value)));
			}
			else if (reader.TokenType == JsonToken.Date)
			{
				DateTime value = (DateTime)reader.Value;
				xmlElement.AppendChild(document.CreateTextNode(XmlConvert.ToString(value, DateTimeUtils.ToSerializationMode(value.Kind))));
			}
			else if (reader.TokenType != JsonToken.Null && reader.TokenType != JsonToken.EndObject)
			{
				manager.PushScope();
				DeserializeNode(reader, document, manager, xmlElement);
				manager.PopScope();
			}
		}

		private void ReadArrayElements(JsonReader reader, IXmlDocument document, string propertyName, IXmlNode currentNode, XmlNamespaceManager manager)
		{
			string prefix = MiscellaneousUtils.GetPrefix(propertyName);
			IXmlElement xmlElement = CreateElement(propertyName, document, prefix, manager);
			currentNode.AppendChild(xmlElement);
			while (reader.Read() && reader.TokenType != JsonToken.EndArray)
			{
				DeserializeValue(reader, document, manager, propertyName, xmlElement);
			}
		}

		private Dictionary<string, string> ReadAttributeElements(JsonReader reader, XmlNamespaceManager manager)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			bool flag = false;
			bool flag2 = false;
			if (reader.TokenType != JsonToken.String && reader.TokenType != JsonToken.Null && reader.TokenType != JsonToken.Boolean && reader.TokenType != JsonToken.Integer && reader.TokenType != JsonToken.Float && reader.TokenType != JsonToken.Date && reader.TokenType != JsonToken.StartConstructor)
			{
				while (!flag && !flag2 && reader.Read())
				{
					switch (reader.TokenType)
					{
					case JsonToken.PropertyName:
					{
						string text = reader.Value.ToString();
						switch (text[0])
						{
						case '@':
						{
							text = text.Substring(1);
							reader.Read();
							string value = reader.Value.ToString();
							dictionary.Add(text, value);
							if (IsNamespaceAttribute(text, out var prefix))
							{
								manager.AddNamespace(prefix, value);
							}
							break;
						}
						case '$':
						{
							text = text.Substring(1);
							reader.Read();
							string value = reader.Value.ToString();
							string text2 = manager.LookupPrefix("http://james.newtonking.com/projects/json");
							if (text2 == null)
							{
								int? num = null;
								while (manager.LookupNamespace("json" + num) != null)
								{
									num = num.GetValueOrDefault() + 1;
								}
								text2 = "json" + num;
								dictionary.Add("xmlns:" + text2, "http://james.newtonking.com/projects/json");
								manager.AddNamespace(text2, "http://james.newtonking.com/projects/json");
							}
							dictionary.Add(text2 + ":" + text, value);
							break;
						}
						default:
							flag = true;
							break;
						}
						break;
					}
					case JsonToken.EndObject:
						flag2 = true;
						break;
					default:
						throw new JsonSerializationException("Unexpected JsonToken: " + reader.TokenType);
					}
				}
			}
			return dictionary;
		}

		private void CreateInstruction(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string propertyName)
		{
			if (propertyName == "?xml")
			{
				string version = null;
				string encoding = null;
				string standalone = null;
				while (reader.Read() && reader.TokenType != JsonToken.EndObject)
				{
					switch (reader.Value.ToString())
					{
					case "@version":
						reader.Read();
						version = reader.Value.ToString();
						break;
					case "@encoding":
						reader.Read();
						encoding = reader.Value.ToString();
						break;
					case "@standalone":
						reader.Read();
						standalone = reader.Value.ToString();
						break;
					default:
						throw new JsonSerializationException("Unexpected property name encountered while deserializing XmlDeclaration: " + reader.Value);
					}
				}
				IXmlNode newChild = document.CreateXmlDeclaration(version, encoding, standalone);
				currentNode.AppendChild(newChild);
			}
			else
			{
				IXmlNode newChild2 = document.CreateProcessingInstruction(propertyName.Substring(1), reader.Value.ToString());
				currentNode.AppendChild(newChild2);
			}
		}

		private IXmlElement CreateElement(string elementName, IXmlDocument document, string elementPrefix, XmlNamespaceManager manager)
		{
			if (string.IsNullOrEmpty(elementPrefix))
			{
				return document.CreateElement(elementName);
			}
			return document.CreateElement(elementName, manager.LookupNamespace(elementPrefix));
		}

		private void DeserializeNode(JsonReader reader, IXmlDocument document, XmlNamespaceManager manager, IXmlNode currentNode)
		{
			do
			{
				switch (reader.TokenType)
				{
				case JsonToken.PropertyName:
				{
					if (currentNode.NodeType == XmlNodeType.Document && document.DocumentElement != null)
					{
						throw new JsonSerializationException("JSON root object has multiple properties. The root object must have a single property in order to create a valid XML document. Consider specifing a DeserializeRootElementName.");
					}
					string propertyName2 = reader.Value.ToString();
					reader.Read();
					if (reader.TokenType == JsonToken.StartArray)
					{
						while (reader.Read() && reader.TokenType != JsonToken.EndArray)
						{
							DeserializeValue(reader, document, manager, propertyName2, currentNode);
						}
					}
					else
					{
						DeserializeValue(reader, document, manager, propertyName2, currentNode);
					}
					break;
				}
				case JsonToken.StartConstructor:
				{
					string propertyName = reader.Value.ToString();
					while (reader.Read() && reader.TokenType != JsonToken.EndConstructor)
					{
						DeserializeValue(reader, document, manager, propertyName, currentNode);
					}
					break;
				}
				case JsonToken.Comment:
					currentNode.AppendChild(document.CreateComment((string)reader.Value));
					break;
				case JsonToken.EndObject:
				case JsonToken.EndArray:
					return;
				default:
					throw new JsonSerializationException("Unexpected JsonToken when deserializing node: " + reader.TokenType);
				}
			}
			while (reader.TokenType == JsonToken.PropertyName || reader.Read());
		}

		private bool IsNamespaceAttribute(string attributeName, out string prefix)
		{
			if (attributeName.StartsWith("xmlns", StringComparison.Ordinal))
			{
				if (attributeName.Length == 5)
				{
					prefix = string.Empty;
					return true;
				}
				if (attributeName[5] == ':')
				{
					prefix = attributeName.Substring(6, attributeName.Length - 6);
					return true;
				}
			}
			prefix = null;
			return false;
		}

		private IEnumerable<IXmlNode> ValueAttributes(IEnumerable<IXmlNode> c)
		{
			return c.Where((IXmlNode a) => a.NamespaceURI != "http://james.newtonking.com/projects/json");
		}

		public override bool CanConvert(Type valueType)
		{
			if (typeof(XmlNode).IsAssignableFrom(valueType))
			{
				return true;
			}
			return false;
		}
	}
}
