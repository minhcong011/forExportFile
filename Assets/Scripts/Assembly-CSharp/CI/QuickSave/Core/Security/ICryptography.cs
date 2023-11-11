using System;

namespace CI.QuickSave.Core.Security
{
	// Token: 0x020000A2 RID: 162
	public interface ICryptography
	{
		// Token: 0x060002FA RID: 762
		string Encrypt(string value, SecurityMode securityMode, string password);

		// Token: 0x060002FB RID: 763
		string Decrypt(string value, SecurityMode securityMode, string password);
	}
}
