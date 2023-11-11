using System;
using JsonFx.Json;
using JsonFx.Json.Resolvers;
using JsonFx.Model;
using JsonFx.Serialization;
using JsonFx.Serialization.Filters;
using JsonFx.Serialization.Resolvers;

namespace CI.QuickSave.Core.Serialisers
{
	// Token: 0x0200009F RID: 159
	public class JsonSerialiserMono : IJsonSerialiser
	{
		// Token: 0x060002ED RID: 749 RVA: 0x00012A58 File Offset: 0x00010C58
		public string Serialise<T>(T value)
		{
			return new JsonWriter(new DataWriterSettings(new CombinedResolverStrategy(new IResolverStrategy[]
			{
				new JsonResolverStrategy()
			}), Array.Empty<IDataFilter<ModelTokenType>>())).Write(value);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00012A87 File Offset: 0x00010C87
		public T Deserialise<T>(string json)
		{
			return new JsonReader(new DataReaderSettings(new CombinedResolverStrategy(new IResolverStrategy[]
			{
				new JsonResolverStrategy()
			}), Array.Empty<IDataFilter<ModelTokenType>>())).Read<T>(json);
		}
	}
}
