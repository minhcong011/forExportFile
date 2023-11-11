using System;
using System.Collections.Generic;
using CI.QuickSave.Core.Helpers;
using CI.QuickSave.Core.Security;
using CI.QuickSave.Core.Serialisers;
using CI.QuickSave.Core.Storage;

namespace CI.QuickSave
{
	// Token: 0x02000096 RID: 150
	public static class QuickSaveRoot
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x0001227C File Offset: 0x0001047C
		public static void Save<T>(string root, T value)
		{
			QuickSaveRoot.Save<T>(root, value, new QuickSaveSettings());
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0001228C File Offset: 0x0001048C
		public static void Save<T>(string root, T value, QuickSaveSettings settings)
		{
			string value2;
			try
			{
				value2 = JsonSerialiser.Serialise<object>(TypeHelper.ReplaceIfUnityType<T>(value));
			}
			catch (Exception innerException)
			{
				throw new QuickSaveException("Json serialisation failed", innerException);
			}
			string value3;
			try
			{
				value3 = Cryptography.Encrypt(value2, settings.SecurityMode, settings.Password);
			}
			catch (Exception innerException2)
			{
				throw new QuickSaveException("Encryption failed", innerException2);
			}
			if (!FileAccess.SaveString(root, false, value3))
			{
				throw new QuickSaveException("Failed to write to file");
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00012308 File Offset: 0x00010508
		public static T Load<T>(string root)
		{
			return QuickSaveRoot.Load<T>(root, new QuickSaveSettings());
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00012318 File Offset: 0x00010518
		public static T Load<T>(string root, QuickSaveSettings settings)
		{
			string value = FileAccess.LoadString(root, false);
			if (string.IsNullOrEmpty(value))
			{
				throw new QuickSaveException("File either does not exist or is empty");
			}
			string json;
			try
			{
				json = Cryptography.Decrypt(value, settings.SecurityMode, settings.Password);
			}
			catch (Exception innerException)
			{
				throw new QuickSaveException("Decryption failed", innerException);
			}
			T result;
			try
			{
				result = JsonSerialiser.Deserialise<T>(json);
			}
			catch (Exception innerException2)
			{
				throw new QuickSaveException("Failed to deserialise json", innerException2);
			}
			return result;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0001239C File Offset: 0x0001059C
		public static void Delete(string root)
		{
			FileAccess.Delete(root, false);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x000123A5 File Offset: 0x000105A5
		public static bool Exists(string root)
		{
			return FileAccess.Exists(root, false);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00011FC5 File Offset: 0x000101C5
		public static IEnumerable<string> GetAllRoots()
		{
			return FileAccess.Files(false);
		}
	}
}
