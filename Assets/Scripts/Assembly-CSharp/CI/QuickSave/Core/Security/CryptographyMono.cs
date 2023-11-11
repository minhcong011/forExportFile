using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CI.QuickSave.Core.Security
{
	// Token: 0x020000A1 RID: 161
	public class CryptographyMono : ICryptography
	{
		// Token: 0x060002F3 RID: 755 RVA: 0x00012ADB File Offset: 0x00010CDB
		public string Encrypt(string value, SecurityMode securityMode, string password)
		{
			if (securityMode == SecurityMode.Aes)
			{
				return this.AesEncrypt(password, value);
			}
			if (securityMode != SecurityMode.Base64)
			{
				return value;
			}
			return CryptographyMono.Base64Encode(value);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00012AF8 File Offset: 0x00010CF8
		public string Decrypt(string value, SecurityMode securityMode, string password)
		{
			if (securityMode == SecurityMode.Aes)
			{
				return this.AesDecrypt(password, value);
			}
			if (securityMode != SecurityMode.Base64)
			{
				return value;
			}
			return CryptographyMono.Base64Decode(value);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00012B18 File Offset: 0x00010D18
		private string AesEncrypt(string encryptionKey, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			string result;
			using (Aes aes = Aes.Create())
			{
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(encryptionKey, new byte[]
				{
					73,
					118,
					97,
					110,
					32,
					77,
					101,
					100,
					118,
					101,
					100,
					101,
					118
				});
				aes.Key = rfc2898DeriveBytes.GetBytes(32);
				aes.IV = rfc2898DeriveBytes.GetBytes(16);
				byte[] bytes = Encoding.UTF8.GetBytes(value);
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cryptoStream.Write(bytes, 0, bytes.Length);
					}
					result = Convert.ToBase64String(memoryStream.ToArray());
				}
			}
			return result;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00012BF4 File Offset: 0x00010DF4
		private string AesDecrypt(string encryptionKey, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			string result;
			using (Aes aes = Aes.Create())
			{
				Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(encryptionKey, new byte[]
				{
					73,
					118,
					97,
					110,
					32,
					77,
					101,
					100,
					118,
					101,
					100,
					101,
					118
				});
				aes.Key = rfc2898DeriveBytes.GetBytes(32);
				aes.IV = rfc2898DeriveBytes.GetBytes(16);
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(value)))
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
					{
						using (StreamReader streamReader = new StreamReader(cryptoStream))
						{
							result = streamReader.ReadToEnd();
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00012CD4 File Offset: 0x00010ED4
		private static string Base64Encode(string value)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00012CE6 File Offset: 0x00010EE6
		private static string Base64Decode(string value)
		{
			return Encoding.UTF8.GetString(Convert.FromBase64String(value));
		}
	}
}
