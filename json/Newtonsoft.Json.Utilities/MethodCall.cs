namespace Newtonsoft0.Json.Utilities
{
	internal delegate TResult MethodCall<T, TResult>(T target, params object[] args);
}
