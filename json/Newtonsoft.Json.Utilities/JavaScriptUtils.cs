using System.IO;

namespace Newtonsoft0.Json.Utilities
{
	internal static class JavaScriptUtils
	{
		public static void WriteEscapedJavaScriptString(TextWriter writer, string value, char delimiter, bool appendDelimiters)
		{
			if (appendDelimiters)
			{
				writer.Write(delimiter);
			}
			if (value != null)
			{
				int num = 0;
				int num2 = 0;
				char[] array = null;
				for (int i = 0; i < value.Length; i++)
				{
					char c = value[i];
					string text;
					switch (c)
					{
					case '\t':
						text = "\\t";
						break;
					case '\n':
						text = "\\n";
						break;
					case '\r':
						text = "\\r";
						break;
					case '\f':
						text = "\\f";
						break;
					case '\b':
						text = "\\b";
						break;
					case '\\':
						text = "\\\\";
						break;
					case '\u0085':
						text = "\\u0085";
						break;
					case '\u2028':
						text = "\\u2028";
						break;
					case '\u2029':
						text = "\\u2029";
						break;
					case '\'':
						text = ((delimiter == '\'') ? "\\'" : null);
						break;
					case '"':
						text = ((delimiter == '"') ? "\\\"" : null);
						break;
					default:
						text = ((c <= '\u001f') ? StringUtils.ToCharAsUnicode(c) : null);
						break;
					}
					if (text != null)
					{
						if (array == null)
						{
							array = value.ToCharArray();
						}
						if (num2 > 0)
						{
							writer.Write(array, num, num2);
							num2 = 0;
						}
						writer.Write(text);
						num = i + 1;
					}
					else
					{
						num2++;
					}
				}
				if (num2 > 0)
				{
					if (num == 0)
					{
						writer.Write(value);
					}
					else
					{
						writer.Write(array, num, num2);
					}
				}
			}
			if (appendDelimiters)
			{
				writer.Write(delimiter);
			}
		}

		public static string ToEscapedJavaScriptString(string value)
		{
			return ToEscapedJavaScriptString(value, '"', appendDelimiters: true);
		}

		public static string ToEscapedJavaScriptString(string value, char delimiter, bool appendDelimiters)
		{
			using (StringWriter stringWriter = StringUtils.CreateStringWriter(StringUtils.GetLength(value) ?? 16))
			{
				WriteEscapedJavaScriptString(stringWriter, value, delimiter, appendDelimiters);
				return stringWriter.ToString();
			}
		}
	}
}
