using System;
using System.Collections.Generic;
using System.Linq;
using CI.QuickSave.Core.Helpers;
using CI.QuickSave.Core.Security;
using CI.QuickSave.Core.Serialisers;
using CI.QuickSave.Core.Storage;

namespace CI.QuickSave
{
	// Token: 0x02000098 RID: 152
	public class QuickSaveWriter
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x000123DF File Offset: 0x000105DF
		private QuickSaveWriter(string root, QuickSaveSettings settings)
		{
			this._root = root;
			this._settings = settings;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000123F5 File Offset: 0x000105F5
		public static QuickSaveWriter Create(string root)
		{
			return QuickSaveWriter.Create(root, new QuickSaveSettings());
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00012402 File Offset: 0x00010602
		public static bool TrySave<T>(string root, string key, T value)
		{
			return QuickSaveWriter.TrySave<T>(root, key, value, new QuickSaveSettings());
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00012414 File Offset: 0x00010614
		public static bool TrySave<T>(string root, string key, T value, QuickSaveSettings settings)
		{
			bool result;
			try
			{
				string value2 = FileAccess.LoadString(root, false);
				Dictionary<string, object> dictionary;
				if (string.IsNullOrEmpty(value2))
				{
					dictionary = new Dictionary<string, object>();
				}
				else
				{
					dictionary = (JsonSerialiser.Deserialise<Dictionary<string, object>>(Cryptography.Decrypt(value2, settings.SecurityMode, settings.Password)) ?? new Dictionary<string, object>());
					if (dictionary.ContainsKey(key))
					{
						dictionary.Remove(key);
					}
				}
				dictionary.Add(key, TypeHelper.ReplaceIfUnityType<T>(value));
				string value3 = Cryptography.Encrypt(JsonSerialiser.Serialise<Dictionary<string, object>>(dictionary), settings.SecurityMode, settings.Password);
				FileAccess.SaveString(root, false, value3);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000124B8 File Offset: 0x000106B8
		public static QuickSaveWriter Create(string root, QuickSaveSettings settings)
		{
			QuickSaveWriter quickSaveWriter = new QuickSaveWriter(root, settings);
			quickSaveWriter.Open();
			return quickSaveWriter;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000124C7 File Offset: 0x000106C7
		public QuickSaveWriter Write<T>(string key, T value)
		{
			if (this._items.ContainsKey(key))
			{
				this._items.Remove(key);
			}
			this._items.Add(key, TypeHelper.ReplaceIfUnityType<T>(value));
			return this;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x000124F7 File Offset: 0x000106F7
		public void Delete(string key)
		{
			if (this._items.ContainsKey(key))
			{
				this._items.Remove(key);
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00012514 File Offset: 0x00010714
		public bool Exists(string key)
		{
			return this._items.ContainsKey(key);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00012522 File Offset: 0x00010722
		public IEnumerable<string> GetAllKeys()
		{
			return this._items.Keys.ToList<string>();
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00012534 File Offset: 0x00010734
		public void Commit()
		{
			string value;
			try
			{
				value = JsonSerialiser.Serialise<Dictionary<string, object>>(this._items);
			}
			catch (Exception innerException)
			{
				throw new QuickSaveException("Json serialisation failed", innerException);
			}
			string value2;
			try
			{
				value2 = Cryptography.Encrypt(value, this._settings.SecurityMode, this._settings.Password);
			}
			catch (Exception innerException2)
			{
				throw new QuickSaveException("Encryption failed", innerException2);
			}
			if (!FileAccess.SaveString(this._root, false, value2))
			{
				throw new QuickSaveException("Failed to write to file");
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000125C0 File Offset: 0x000107C0
		public bool TryCommit()
		{
			bool result;
			try
			{
				string value = Cryptography.Encrypt(JsonSerialiser.Serialise<Dictionary<string, object>>(this._items), this._settings.SecurityMode, this._settings.Password);
				FileAccess.SaveString(this._root, false, value);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0001261C File Offset: 0x0001081C
		private void Open()
		{
			string value = FileAccess.LoadString(this._root, false);
			if (string.IsNullOrEmpty(value))
			{
				this._items = new Dictionary<string, object>();
				return;
			}
			string json;
			try
			{
				json = Cryptography.Decrypt(value, this._settings.SecurityMode, this._settings.Password);
			}
			catch (Exception innerException)
			{
				throw new QuickSaveException("Decryption failed", innerException);
			}
			try
			{
				this._items = (JsonSerialiser.Deserialise<Dictionary<string, object>>(json) ?? new Dictionary<string, object>());
			}
			catch (Exception innerException2)
			{
				throw new QuickSaveException("Failed to deserialise json", innerException2);
			}
		}

		// Token: 0x0400030D RID: 781
		private readonly string _root;

		// Token: 0x0400030E RID: 782
		private readonly QuickSaveSettings _settings;

		// Token: 0x0400030F RID: 783
		private Dictionary<string, object> _items;
	}
}
