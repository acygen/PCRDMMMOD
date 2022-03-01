using System;
using Newtonsoft0.Json.Utilities;

namespace Newtonsoft0.Json.Bson
{
	public class BsonObjectId
	{
		public byte[] Value
		{
			get;
			private set;
		}

		public BsonObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw new Exception("An ObjectId must be 12 bytes");
			}
			Value = value;
		}
	}
}
