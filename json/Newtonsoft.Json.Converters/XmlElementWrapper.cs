using System.Xml;

namespace Newtonsoft0.Json.Converters
{
	internal class XmlElementWrapper : XmlNodeWrapper, IXmlElement, IXmlNode
	{
		private XmlElement _element;

		public XmlElementWrapper(XmlElement element)
			: base(element)
		{
			_element = element;
		}

		public void SetAttributeNode(IXmlNode attribute)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)attribute;
			_element.SetAttributeNode((XmlAttribute)xmlNodeWrapper.WrappedNode);
		}
	}
}
