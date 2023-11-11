using System;

namespace CI.QuickSave
{
	// Token: 0x02000097 RID: 151
	public class QuickSaveSettings
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060002BD RID: 701 RVA: 0x000123AE File Offset: 0x000105AE
		// (set) Token: 0x060002BE RID: 702 RVA: 0x000123B6 File Offset: 0x000105B6
		public SecurityMode SecurityMode { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060002BF RID: 703 RVA: 0x000123BF File Offset: 0x000105BF
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x000123C7 File Offset: 0x000105C7
		public string Password { get; set; }

		// Token: 0x060002C1 RID: 705 RVA: 0x000123D0 File Offset: 0x000105D0
		public QuickSaveSettings()
		{
			this.SecurityMode = SecurityMode.None;
		}
	}
}
