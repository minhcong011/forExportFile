using System;

namespace CI.QuickSave
{
	// Token: 0x02000093 RID: 147
	public class QuickSaveException : Exception
	{
		// Token: 0x0600029D RID: 669 RVA: 0x00011E9B File Offset: 0x0001009B
		public QuickSaveException()
		{
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00011EA3 File Offset: 0x000100A3
		public QuickSaveException(string message) : base(message)
		{
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00011EAC File Offset: 0x000100AC
		public QuickSaveException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
