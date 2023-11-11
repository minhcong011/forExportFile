using System;

namespace CI.QuickSave.Core.Security
{
	// Token: 0x020000A0 RID: 160
	public static class Cryptography
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x00012AB1 File Offset: 0x00010CB1
		public static string Encrypt(string value, SecurityMode securityMode, string password)
		{
			return Cryptography._cryptography.Encrypt(value, securityMode, password);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00012AC0 File Offset: 0x00010CC0
		public static string Decrypt(string value, SecurityMode securityMode, string password)
		{
			return Cryptography._cryptography.Decrypt(value, securityMode, password);
		}

		// Token: 0x04000318 RID: 792
		private static ICryptography _cryptography = new CryptographyMono();
	}
}
