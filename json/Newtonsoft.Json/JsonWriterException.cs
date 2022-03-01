using System;

namespace Newtonsoft0.Json
{
	public class JsonWriterException : Exception
	{
		public JsonWriterException()
		{
		}

		public JsonWriterException(string message)
			: base(message)
		{
		}

		public JsonWriterException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
