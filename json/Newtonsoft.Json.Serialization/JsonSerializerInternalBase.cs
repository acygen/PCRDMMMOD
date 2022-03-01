using System;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Serialization
{
	internal abstract class JsonSerializerInternalBase
	{
		private ErrorContext _currentErrorContext;

		internal JsonSerializer Serializer
		{
			get;
			private set;
		}

		protected JsonSerializerInternalBase(JsonSerializer serializer)
		{
			ValidationUtils.ArgumentNotNull(serializer, "serializer");
			Serializer = serializer;
		}

		protected ErrorContext GetErrorContext(object currentObject, object member, Exception error)
		{
			if (_currentErrorContext == null)
			{
				_currentErrorContext = new ErrorContext(currentObject, member, error);
			}
			if (_currentErrorContext.Error != error)
			{
				throw new InvalidOperationException("Current error context error is different to requested error.");
			}
			return _currentErrorContext;
		}

		protected void ClearErrorContext()
		{
			if (_currentErrorContext == null)
			{
				throw new InvalidOperationException("Could not clear error context. Error context is already null.");
			}
			_currentErrorContext = null;
		}

		protected bool IsErrorHandled(object currentObject, JsonContract contract, object keyValue, Exception ex)
		{
			ErrorContext errorContext = GetErrorContext(currentObject, keyValue, ex);
			contract.InvokeOnError(currentObject, Serializer.Context, errorContext);
			if (!errorContext.Handled)
			{
				Serializer.OnError(new ErrorEventArgs(currentObject, errorContext));
			}
			return errorContext.Handled;
		}
	}
}
