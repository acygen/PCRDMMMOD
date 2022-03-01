using System;
using System.Globalization;

namespace Newtonsoft0.Json.Utilities
{
	internal static class MiscellaneousUtils
	{
		public static ArgumentOutOfRangeException CreateArgumentOutOfRangeException(string paramName, object actualValue, string message)
		{
			string message2 = message + Environment.NewLine + "Actual value was {0}.".FormatWith(CultureInfo.InvariantCulture, actualValue);
			return new ArgumentOutOfRangeException(paramName, message2);
		}

		public static bool TryAction<T>(Creator<T> creator, out T output)
		{
			ValidationUtils.ArgumentNotNull(creator, "creator");
			try
			{
				output = creator();
				return true;
			}
			catch
			{
				output = default(T);
				return false;
			}
		}

		public static string ToString(object value)
		{
			if (value == null)
			{
				return "{null}";
			}
			if (!(value is string))
			{
				return value.ToString();
			}
			return "\"" + value.ToString() + "\"";
		}

		public static byte[] HexToBytes(string hex)
		{
			string text = hex.Replace("-", string.Empty);
			byte[] array = new byte[text.Length / 2];
			int num = 4;
			int num2 = 0;
			string text2 = text;
			foreach (char c in text2)
			{
				int num3 = (c - 48) % 32;
				if (num3 > 9)
				{
					num3 -= 7;
				}
				array[num2] |= (byte)(num3 << num);
				num ^= 4;
				if (num != 0)
				{
					num2++;
				}
			}
			return array;
		}

		public static string BytesToHex(byte[] bytes)
		{
			return BytesToHex(bytes, removeDashes: false);
		}

		public static string BytesToHex(byte[] bytes, bool removeDashes)
		{
			string text = BitConverter.ToString(bytes);
			if (removeDashes)
			{
				text = text.Replace("-", "");
			}
			return text;
		}

		public static bool ByteArrayCompare(byte[] a1, byte[] a2)
		{
			if (a1.Length != a2.Length)
			{
				return false;
			}
			for (int i = 0; i < a1.Length; i++)
			{
				if (a1[i] != a2[i])
				{
					return false;
				}
			}
			return true;
		}

		public static string GetPrefix(string qualifiedName)
		{
			GetQualifiedNameParts(qualifiedName, out var prefix, out var _);
			return prefix;
		}

		public static string GetLocalName(string qualifiedName)
		{
			GetQualifiedNameParts(qualifiedName, out var _, out var localName);
			return localName;
		}

		public static void GetQualifiedNameParts(string qualifiedName, out string prefix, out string localName)
		{
			int num = qualifiedName.IndexOf(':');
			if (num == -1 || num == 0 || qualifiedName.Length - 1 == num)
			{
				prefix = null;
				localName = qualifiedName;
			}
			else
			{
				prefix = qualifiedName.Substring(0, num);
				localName = qualifiedName.Substring(num + 1);
			}
		}
	}
}
