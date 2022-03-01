namespace Newtonsoft0.Json.Bson
{
	internal enum BsonBinaryType : byte
	{
		Function = 1,
		Data = 2,
		Uuid = 3,
		Md5 = 5,
		UserDefined = 0x80
	}
}
