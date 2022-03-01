namespace Newtonsoft0.Json.Serialization
{
	public interface IReferenceResolver
	{
		object ResolveReference(string reference);

		string GetReference(object value);

		bool IsReferenced(object value);

		void AddReference(string reference, object value);
	}
}
