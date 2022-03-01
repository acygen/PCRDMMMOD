using System;

namespace Newtonsoft0.Json
{
	public class JsonSerializationException : Exception
	{
		public JsonSerializationException()
		{
		}

		public JsonSerializationException(string message)
			: base(message)
		{
		}

		public JsonSerializationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
