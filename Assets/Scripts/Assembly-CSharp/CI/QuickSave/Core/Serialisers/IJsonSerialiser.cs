using System;

namespace CI.QuickSave.Core.Serialisers
{
	// Token: 0x0200009D RID: 157
	public interface IJsonSerialiser
	{
		// Token: 0x060002E8 RID: 744
		string Serialise<T>(T value);

		// Token: 0x060002E9 RID: 745
		T Deserialise<T>(string json);
	}
}
