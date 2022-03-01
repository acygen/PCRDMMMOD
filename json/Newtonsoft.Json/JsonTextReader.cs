using System;
using System.Globalization;
using System.IO;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json
{
	public class JsonTextReader : JsonReader, IJsonLineInfo
	{
		private enum ReadType
		{
			Read,
			ReadAsBytes
		}

		private const int LineFeedValue = 10;

		private const int CarriageReturnValue = 13;

		private readonly TextReader _reader;

		private readonly StringBuffer _buffer;

		private char? _lastChar;

		private int _currentLinePosition;

		private int _currentLineNumber;

		private bool _end;

		private ReadType _readType;

		public int LineNumber
		{
			get
			{
				if (base.CurrentState == State.Start)
				{
					return 0;
				}
				return _currentLineNumber;
			}
		}

		public int LinePosition => _currentLinePosition;

		public JsonTextReader(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			_reader = reader;
			_buffer = new StringBuffer(4096);
			_currentLineNumber = 1;
		}

		private void ParseString(char quote)
		{
			ReadStringIntoBuffer(quote);
			if (_readType == ReadType.ReadAsBytes)
			{
				byte[] value;
				if (_buffer.Position == 0)
				{
					value = new byte[0];
				}
				else
				{
					value = Convert.FromBase64CharArray(_buffer.GetInternalBuffer(), 0, _buffer.Position);
					_buffer.Position = 0;
				}
				SetToken(JsonToken.Bytes, value);
				return;
			}
			string text = _buffer.ToString();
			_buffer.Position = 0;
			if (text.StartsWith("/Date(", StringComparison.Ordinal) && text.EndsWith(")/", StringComparison.Ordinal))
			{
				ParseDate(text);
				return;
			}
			SetToken(JsonToken.String, text);
			QuoteChar = quote;
		}

		private void ReadStringIntoBuffer(char quote)
		{
			while (true)
			{
				char c = MoveNext();
				switch (c)
				{
				case '\0':
					if (_end)
					{
						throw CreateJsonReaderException("Unterminated string. Expected delimiter: {0}. Line {1}, position {2}.", quote, _currentLineNumber, _currentLinePosition);
					}
					_buffer.Append('\0');
					break;
				case '\\':
					if ((c = MoveNext()) != 0 || !_end)
					{
						switch (c)
						{
						case 'b':
							_buffer.Append('\b');
							break;
						case 't':
							_buffer.Append('\t');
							break;
						case 'n':
							_buffer.Append('\n');
							break;
						case 'f':
							_buffer.Append('\f');
							break;
						case 'r':
							_buffer.Append('\r');
							break;
						case '\\':
							_buffer.Append('\\');
							break;
						case '"':
						case '\'':
						case '/':
							_buffer.Append(c);
							break;
						case 'u':
						{
							char[] array = new char[4];
							for (int i = 0; i < array.Length; i++)
							{
								if ((c = MoveNext()) != 0 || !_end)
								{
									array[i] = c;
									continue;
								}
								throw CreateJsonReaderException("Unexpected end while parsing unicode character. Line {0}, position {1}.", _currentLineNumber, _currentLinePosition);
							}
							char value = Convert.ToChar(int.Parse(new string(array), NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo));
							_buffer.Append(value);
							break;
						}
						default:
							throw CreateJsonReaderException("Bad JSON escape sequence: {0}. Line {1}, position {2}.", "\\" + c, _currentLineNumber, _currentLinePosition);
						}
						break;
					}
					throw CreateJsonReaderException("Unterminated string. Expected delimiter: {0}. Line {1}, position {2}.", quote, _currentLineNumber, _currentLinePosition);
				case '"':
				case '\'':
					if (c == quote)
					{
						return;
					}
					_buffer.Append(c);
					break;
				default:
					_buffer.Append(c);
					break;
				}
			}
		}

		private JsonReaderException CreateJsonReaderException(string format, params object[] args)
		{
			string message = format.FormatWith(CultureInfo.InvariantCulture, args);
			return new JsonReaderException(message, null, _currentLineNumber, _currentLinePosition);
		}

		private void ParseDate(string text)
		{
			string text2 = text.Substring(6, text.Length - 8);
			DateTimeKind dateTimeKind = DateTimeKind.Utc;
			int num = text2.IndexOf('+', 1);
			if (num == -1)
			{
				num = text2.IndexOf('-', 1);
			}
			if (num != -1)
			{
				dateTimeKind = DateTimeKind.Local;
				text2 = text2.Substring(0, num);
			}
			long javaScriptTicks = long.Parse(text2, NumberStyles.Integer, CultureInfo.InvariantCulture);
			DateTime dateTime = JsonConvert.ConvertJavaScriptTicksToDateTime(javaScriptTicks);
			DateTime dateTime2;
			switch (dateTimeKind)
			{
			case DateTimeKind.Unspecified:
				dateTime2 = DateTime.SpecifyKind(dateTime.ToLocalTime(), DateTimeKind.Unspecified);
				break;
			case DateTimeKind.Local:
				dateTime2 = dateTime.ToLocalTime();
				break;
			default:
				dateTime2 = dateTime;
				break;
			}
			SetToken(JsonToken.Date, dateTime2);
		}

		private char MoveNext()
		{
			int num = _reader.Read();
			switch (num)
			{
			case -1:
				_end = true;
				return '\0';
			case 13:
				if (_reader.Peek() == 10)
				{
					_reader.Read();
				}
				_currentLineNumber++;
				_currentLinePosition = 0;
				break;
			case 10:
				_currentLineNumber++;
				_currentLinePosition = 0;
				break;
			default:
				_currentLinePosition++;
				break;
			}
			return (char)num;
		}

		private bool HasNext()
		{
			return _reader.Peek() != -1;
		}

		private int PeekNext()
		{
			return _reader.Peek();
		}

		public override bool Read()
		{
			_readType = ReadType.Read;
			return ReadInternal();
		}

		public override byte[] ReadAsBytes()
		{
			_readType = ReadType.ReadAsBytes;
			if (!ReadInternal())
			{
				throw CreateJsonReaderException("Unexpected end when reading bytes: Line {0}, position {1}.", _currentLineNumber, _currentLinePosition);
			}
			if (TokenType == JsonToken.Null)
			{
				return null;
			}
			if (TokenType == JsonToken.Bytes)
			{
				return (byte[])Value;
			}
			throw CreateJsonReaderException("Unexpected token when reading bytes: {0}. Line {1}, position {2}.", TokenType, _currentLineNumber, _currentLinePosition);
		}

		private bool ReadInternal()
		{
			while (true)
			{
				char c;
				if (((int?)_lastChar).HasValue)
				{
					c = _lastChar.Value;
					_lastChar = null;
				}
				else
				{
					c = MoveNext();
				}
				if (c == '\0' && _end)
				{
					break;
				}
				switch (base.CurrentState)
				{
				case State.Complete:
				case State.Closed:
				case State.Error:
					break;
				case State.Start:
				case State.Property:
				case State.ArrayStart:
				case State.Array:
				case State.ConstructorStart:
				case State.Constructor:
					return ParseValue(c);
				case State.ObjectStart:
				case State.Object:
					return ParseObject(c);
				case State.PostValue:
					if (ParsePostValue(c))
					{
						return true;
					}
					break;
				default:
					throw CreateJsonReaderException("Unexpected state: {0}. Line {1}, position {2}.", base.CurrentState, _currentLineNumber, _currentLinePosition);
				}
			}
			return false;
		}

		private bool ParsePostValue(char currentChar)
		{
			do
			{
				switch (currentChar)
				{
				case '}':
					SetToken(JsonToken.EndObject);
					return true;
				case ']':
					SetToken(JsonToken.EndArray);
					return true;
				case ')':
					SetToken(JsonToken.EndConstructor);
					return true;
				case '/':
					ParseComment();
					return true;
				case ',':
					SetStateBasedOnCurrent();
					return false;
				case '\t':
				case '\n':
				case '\r':
				case ' ':
					continue;
				}
				if (!char.IsWhiteSpace(currentChar))
				{
					throw CreateJsonReaderException("After parsing a value an unexpected character was encountered: {0}. Line {1}, position {2}.", currentChar, _currentLineNumber, _currentLinePosition);
				}
			}
			while ((currentChar = MoveNext()) != 0 || !_end);
			return false;
		}

		private bool ParseObject(char currentChar)
		{
			do
			{
				switch (currentChar)
				{
				case '}':
					SetToken(JsonToken.EndObject);
					return true;
				case '/':
					ParseComment();
					return true;
				case '\t':
				case '\n':
				case '\r':
				case ' ':
					continue;
				}
				if (!char.IsWhiteSpace(currentChar))
				{
					return ParseProperty(currentChar);
				}
			}
			while ((currentChar = MoveNext()) != 0 || !_end);
			return false;
		}

		private bool ParseProperty(char firstChar)
		{
			char c = firstChar;
			char c2;
			if (ValidIdentifierChar(c))
			{
				c2 = '\0';
				c = ParseUnquotedProperty(c);
			}
			else
			{
				if (c != '"' && c != '\'')
				{
					throw CreateJsonReaderException("Invalid property identifier character: {0}. Line {1}, position {2}.", c, _currentLineNumber, _currentLinePosition);
				}
				c2 = c;
				ReadStringIntoBuffer(c2);
				c = MoveNext();
			}
			if (c != ':')
			{
				c = MoveNext();
				EatWhitespace(c, oneOrMore: false, out c);
				if (c != ':')
				{
					throw CreateJsonReaderException("Invalid character after parsing property name. Expected ':' but got: {0}. Line {1}, position {2}.", c, _currentLineNumber, _currentLinePosition);
				}
			}
			SetToken(JsonToken.PropertyName, _buffer.ToString());
			QuoteChar = c2;
			_buffer.Position = 0;
			return true;
		}

		private bool ValidIdentifierChar(char value)
		{
			if (!char.IsLetterOrDigit(value) && value != '_')
			{
				return value == '$';
			}
			return true;
		}

		private char ParseUnquotedProperty(char firstChar)
		{
			_buffer.Append(firstChar);
			char c;
			while ((c = MoveNext()) != 0 || !_end)
			{
				if (char.IsWhiteSpace(c) || c == ':')
				{
					return c;
				}
				if (ValidIdentifierChar(c))
				{
					_buffer.Append(c);
					continue;
				}
				throw CreateJsonReaderException("Invalid JavaScript property identifier character: {0}. Line {1}, position {2}.", c, _currentLineNumber, _currentLinePosition);
			}
			throw CreateJsonReaderException("Unexpected end when parsing unquoted property name. Line {0}, position {1}.", _currentLineNumber, _currentLinePosition);
		}

		private bool ParseValue(char currentChar)
		{
			do
			{
				switch (currentChar)
				{
				case '"':
				case '\'':
					ParseString(currentChar);
					return true;
				case 't':
					ParseTrue();
					return true;
				case 'f':
					ParseFalse();
					return true;
				case 'n':
					if (HasNext())
					{
						switch ((ushort)PeekNext())
						{
						case 117:
							ParseNull();
							break;
						case 101:
							ParseConstructor();
							break;
						default:
							throw CreateJsonReaderException("Unexpected character encountered while parsing value: {0}. Line {1}, position {2}.", currentChar, _currentLineNumber, _currentLinePosition);
						}
						return true;
					}
					throw CreateJsonReaderException("Unexpected end. Line {0}, position {1}.", _currentLineNumber, _currentLinePosition);
				case 'N':
					ParseNumberNaN();
					return true;
				case 'I':
					ParseNumberPositiveInfinity();
					return true;
				case '-':
					if (PeekNext() == 73)
					{
						ParseNumberNegativeInfinity();
					}
					else
					{
						ParseNumber(currentChar);
					}
					return true;
				case '/':
					ParseComment();
					return true;
				case 'u':
					ParseUndefined();
					return true;
				case '{':
					SetToken(JsonToken.StartObject);
					return true;
				case '[':
					SetToken(JsonToken.StartArray);
					return true;
				case '}':
					SetToken(JsonToken.EndObject);
					return true;
				case ']':
					SetToken(JsonToken.EndArray);
					return true;
				case ',':
					SetToken(JsonToken.Undefined);
					return true;
				case ')':
					SetToken(JsonToken.EndConstructor);
					return true;
				case '\t':
				case '\n':
				case '\r':
				case ' ':
					continue;
				}
				if (!char.IsWhiteSpace(currentChar))
				{
					if (char.IsNumber(currentChar) || currentChar == '-' || currentChar == '.')
					{
						ParseNumber(currentChar);
						return true;
					}
					throw CreateJsonReaderException("Unexpected character encountered while parsing value: {0}. Line {1}, position {2}.", currentChar, _currentLineNumber, _currentLinePosition);
				}
			}
			while ((currentChar = MoveNext()) != 0 || !_end);
			return false;
		}

		private bool EatWhitespace(char initialChar, bool oneOrMore, out char finalChar)
		{
			bool result = false;
			char c = initialChar;
			while (c == ' ' || char.IsWhiteSpace(c))
			{
				result = true;
				c = MoveNext();
			}
			finalChar = c;
			if (oneOrMore)
			{
				return result;
			}
			return true;
		}

		private void ParseConstructor()
		{
			if (!MatchValue('n', "new", noTrailingNonSeperatorCharacters: true))
			{
				return;
			}
			char finalChar = MoveNext();
			if (EatWhitespace(finalChar, oneOrMore: true, out finalChar))
			{
				while (char.IsLetter(finalChar))
				{
					_buffer.Append(finalChar);
					finalChar = MoveNext();
				}
				EatWhitespace(finalChar, oneOrMore: false, out finalChar);
				if (finalChar != '(')
				{
					throw CreateJsonReaderException("Unexpected character while parsing constructor: {0}. Line {1}, position {2}.", finalChar, _currentLineNumber, _currentLinePosition);
				}
				string value = _buffer.ToString();
				_buffer.Position = 0;
				SetToken(JsonToken.StartConstructor, value);
			}
		}

		private void ParseNumber(char firstChar)
		{
			char c = firstChar;
			bool flag = false;
			do
			{
				if (IsSeperator(c))
				{
					flag = true;
					_lastChar = c;
				}
				else
				{
					_buffer.Append(c);
				}
			}
			while (!flag && ((c = MoveNext()) != 0 || !_end));
			string text = _buffer.ToString();
			object value;
			JsonToken newToken;
			if (firstChar == '0' && !text.StartsWith("0.", StringComparison.OrdinalIgnoreCase))
			{
				value = (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? Convert.ToInt64(text, 16) : Convert.ToInt64(text, 8));
				newToken = JsonToken.Integer;
			}
			else if (text.IndexOf(".", StringComparison.OrdinalIgnoreCase) != -1 || text.IndexOf("e", StringComparison.OrdinalIgnoreCase) != -1)
			{
				value = Convert.ToDouble(text, CultureInfo.InvariantCulture);
				newToken = JsonToken.Float;
			}
			else
			{
				try
				{
					value = Convert.ToInt64(text, CultureInfo.InvariantCulture);
				}
				catch (OverflowException innerException)
				{
					throw new JsonReaderException("JSON integer {0} is too large or small for an Int64.".FormatWith(CultureInfo.InvariantCulture, text), innerException);
				}
				newToken = JsonToken.Integer;
			}
			_buffer.Position = 0;
			SetToken(newToken, value);
		}

		private void ParseComment()
		{
			char c = MoveNext();
			if (c == '*')
			{
				while ((c = MoveNext()) != 0 || !_end)
				{
					if (c == '*')
					{
						if ((c = MoveNext()) != 0 || !_end)
						{
							if (c == '/')
							{
								break;
							}
							_buffer.Append('*');
							_buffer.Append(c);
						}
					}
					else
					{
						_buffer.Append(c);
					}
				}
				SetToken(JsonToken.Comment, _buffer.ToString());
				_buffer.Position = 0;
				return;
			}
			throw CreateJsonReaderException("Error parsing comment. Expected: *. Line {0}, position {1}.", _currentLineNumber, _currentLinePosition);
		}

		private bool MatchValue(char firstChar, string value)
		{
			char c = firstChar;
			int num = 0;
			while (c == value[num])
			{
				num++;
				if (num >= value.Length || ((c = MoveNext()) == '\0' && _end))
				{
					break;
				}
			}
			return num == value.Length;
		}

		private bool MatchValue(char firstChar, string value, bool noTrailingNonSeperatorCharacters)
		{
			bool flag = MatchValue(firstChar, value);
			if (!noTrailingNonSeperatorCharacters)
			{
				return flag;
			}
			int num = PeekNext();
			char c = ((num != -1) ? ((char)num) : '\0');
			return flag && (c == '\0' || IsSeperator(c));
		}

		private bool IsSeperator(char c)
		{
			switch (c)
			{
			case ',':
			case ']':
			case '}':
				return true;
			case '/':
				if (HasNext())
				{
					return PeekNext() == 42;
				}
				return false;
			case ')':
				if (base.CurrentState == State.Constructor || base.CurrentState == State.ConstructorStart)
				{
					return true;
				}
				break;
			case '\t':
			case '\n':
			case '\r':
			case ' ':
				return true;
			default:
				if (char.IsWhiteSpace(c))
				{
					return true;
				}
				break;
			}
			return false;
		}

		private void ParseTrue()
		{
			if (MatchValue('t', JsonConvert.True, noTrailingNonSeperatorCharacters: true))
			{
				SetToken(JsonToken.Boolean, true);
				return;
			}
			throw CreateJsonReaderException("Error parsing boolean value. Line {0}, position {1}.", _currentLineNumber, _currentLinePosition);
		}

		private void ParseNull()
		{
			if (MatchValue('n', JsonConvert.Null, noTrailingNonSeperatorCharacters: true))
			{
				SetToken(JsonToken.Null);
				return;
			}
			throw CreateJsonReaderException("Error parsing null value. Line {0}, position {1}.", _currentLineNumber, _currentLinePosition);
		}

		private void ParseUndefined()
		{
			if (MatchValue('u', JsonConvert.Undefined, noTrailingNonSeperatorCharacters: true))
			{
				SetToken(JsonToken.Undefined);
				return;
			}
			throw CreateJsonReaderException("Error parsing undefined value. Line {0}, position {1}.", _currentLineNumber, _currentLinePosition);
		}

		private void ParseFalse()
		{
			if (MatchValue('f', JsonConvert.False, noTrailingNonSeperatorCharacters: true))
			{
				SetToken(JsonToken.Boolean, false);
				return;
			}
			throw CreateJsonReaderException("Error parsing boolean value. Line {0}, position {1}.", _currentLineNumber, _currentLinePosition);
		}

		private void ParseNumberNegativeInfinity()
		{
			if (MatchValue('-', JsonConvert.NegativeInfinity, noTrailingNonSeperatorCharacters: true))
			{
				SetToken(JsonToken.Float, double.NegativeInfinity);
				return;
			}
			throw CreateJsonReaderException("Error parsing negative infinity value. Line {0}, position {1}.", _currentLineNumber, _currentLinePosition);
		}

		private void ParseNumberPositiveInfinity()
		{
			if (MatchValue('I', JsonConvert.PositiveInfinity, noTrailingNonSeperatorCharacters: true))
			{
				SetToken(JsonToken.Float, double.PositiveInfinity);
				return;
			}
			throw CreateJsonReaderException("Error parsing positive infinity value. Line {0}, position {1}.", _currentLineNumber, _currentLinePosition);
		}

		private void ParseNumberNaN()
		{
			if (MatchValue('N', JsonConvert.NaN, noTrailingNonSeperatorCharacters: true))
			{
				SetToken(JsonToken.Float, double.NaN);
				return;
			}
			throw CreateJsonReaderException("Error parsing NaN value. Line {0}, position {1}.", _currentLineNumber, _currentLinePosition);
		}

		public override void Close()
		{
			base.Close();
			if (_reader != null)
			{
				_reader.Close();
			}
			if (_buffer != null)
			{
				_buffer.Clear();
			}
		}

		public bool HasLineInfo()
		{
			return true;
		}
	}
}
