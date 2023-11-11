using System;
using CI.QuickSave.Core.Helpers;

namespace CI.QuickSave.Core.Serialisers
{
	// Token: 0x0200009E RID: 158
	public static class JsonSerialiser
	{
		// Token: 0x060002EA RID: 746 RVA: 0x00012A1F File Offset: 0x00010C1F
		public static string Serialise<T>(T value)
		{
			return JsonSerialiser._serialiser.Serialise<T>(value);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00012A2C File Offset: 0x00010C2C
		public static T Deserialise<T>(string json)
		{
			if (TypeHelper.IsUnityType<T>())
			{
				return TypeHelper.DeserialiseUnityType<T>(json, JsonSerialiser._serialiser);
			}
			return JsonSerialiser._serialiser.Deserialise<T>(json);
		}

		// Token: 0x04000317 RID: 791
		private static IJsonSerialiser _serialiser = new JsonSerialiserMono();
	}
}
