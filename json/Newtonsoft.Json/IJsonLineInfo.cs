namespace Newtonsoft0.Json
{
	public interface IJsonLineInfo
	{
		int LineNumber
		{
			get;
		}

		int LinePosition
		{
			get;
		}

		bool HasLineInfo();
	}
}
