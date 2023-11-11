using System;
using System.Collections.Generic;

namespace CI.QuickSave.Core.Storage
{
	// Token: 0x0200009C RID: 156
	public interface IFileAccess
	{
		// Token: 0x060002E1 RID: 737
		bool SaveString(string filename, string value);

		// Token: 0x060002E2 RID: 738
		bool SaveBytes(string filename, byte[] value);

		// Token: 0x060002E3 RID: 739
		string LoadString(string filename);

		// Token: 0x060002E4 RID: 740
		byte[] LoadBytes(string filename);

		// Token: 0x060002E5 RID: 741
		void Delete(string filename);

		// Token: 0x060002E6 RID: 742
		bool Exists(string filename);

		// Token: 0x060002E7 RID: 743
		IEnumerable<string> Files(bool includeExtensions);
	}
}
