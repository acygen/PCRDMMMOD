using System;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Schema
{
	public class ValidationEventArgs : EventArgs
	{
		private readonly JsonSchemaException _ex;

		public JsonSchemaException Exception => _ex;

		public string Message => _ex.Message;

		internal ValidationEventArgs(JsonSchemaException ex)
		{
			ValidationUtils.ArgumentNotNull(ex, "ex");
			_ex = ex;
		}
	}
}
