using System;
using System.Collections.Generic;
using System.Linq;
using CI.QuickSave.Core.Security;
using CI.QuickSave.Core.Serialisers;
using CI.QuickSave.Core.Storage;

namespace CI.QuickSave
{
	// Token: 0x02000095 RID: 149
	public class QuickSaveReader
	{
		// Token: 0x060002AB RID: 683 RVA: 0x00011FD5 File Offset: 0x000101D5
		private QuickSaveReader(string root, QuickSaveSettings settings)
		{
			this._root = root;
			this._settings = settings;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00011FEB File Offset: 0x000101EB
		public static QuickSaveReader Create(string root)
		{
			return QuickSaveReader.Create(root, new QuickSaveSettings());
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00011FF8 File Offset: 0x000101F8
		public static QuickSaveReader Create(string root, QuickSaveSettings settings)
		{
			QuickSaveReader quickSaveReader = new QuickSaveReader(root, settings);
			quickSaveReader.Open();
			return quickSaveReader;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00012007 File Offset: 0x00010207
		public static bool TryLoad<T>(string root, string key, out T result)
		{
			return QuickSaveReader.TryLoad<T>(root, key, new QuickSaveSettings(), out result);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00012018 File Offset: 0x00010218
		public static bool TryLoad<T>(string root, string key, QuickSaveSettings settings, out T result)
		{
			result = default(T);
			bool result2;
			try
			{
				string value = FileAccess.LoadString(root, false);
				if (string.IsNullOrEmpty(value))
				{
					result2 = false;
				}
				else
				{
					Dictionary<string, object> dictionary = JsonSerialiser.Deserialise<Dictionary<string, object>>(Cryptography.Decrypt(value, settings.SecurityMode, settings.Password)) ?? new Dictionary<string, object>();
					if (!dictionary.ContainsKey(key))
					{
						result2 = false;
					}
					else
					{
						string json = JsonSerialiser.Serialise<object>(dictionary[key]);
						result = JsonSerialiser.Deserialise<T>(json);
						result2 = true;
					}
				}
			}
			catch
			{
				result2 = false;
			}
			return result2;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000120A4 File Offset: 0x000102A4
		public T Read<T>(string key)
		{
			if (!this._items.ContainsKey(key))
			{
				throw new QuickSaveException("Key does not exists");
			}
			T result;
			try
			{
				result = JsonSerialiser.Deserialise<T>(JsonSerialiser.Serialise<object>(this._items[key]));
			}
			catch
			{
				throw new QuickSaveException("Unable to deserialise json");
			}
			return result;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00012104 File Offset: 0x00010304
		public QuickSaveReader Read<T>(string key, Action<T> result)
		{
			if (!this._items.ContainsKey(key))
			{
				throw new QuickSaveException("Key does not exists");
			}
			try
			{
				string json = JsonSerialiser.Serialise<object>(this._items[key]);
				result(JsonSerialiser.Deserialise<T>(json));
			}
			catch
			{
				throw new QuickSaveException("Unable to deserialise json");
			}
			return this;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00012168 File Offset: 0x00010368
		public bool TryRead<T>(string key, out T result)
		{
			result = default(T);
			if (!this._items.ContainsKey(key))
			{
				return false;
			}
			bool result2;
			try
			{
				string json = JsonSerialiser.Serialise<object>(this._items[key]);
				result = JsonSerialiser.Deserialise<T>(json);
				result2 = true;
			}
			catch
			{
				result2 = false;
			}
			return result2;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000121C8 File Offset: 0x000103C8
		public bool Exists(string key)
		{
			return this._items.ContainsKey(key);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000121D6 File Offset: 0x000103D6
		public IEnumerable<string> GetAllKeys()
		{
			return this._items.Keys.ToList<string>();
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000121E8 File Offset: 0x000103E8
		private void Open()
		{
			string value = FileAccess.LoadString(this._root, false);
			if (string.IsNullOrEmpty(value))
			{
				throw new QuickSaveException("Root does not exist");
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
				this._items = JsonSerialiser.Deserialise<Dictionary<string, object>>(json);
			}
			catch (Exception innerException2)
			{
				throw new QuickSaveException("Failed to deserialise json", innerException2);
			}
		}

		// Token: 0x04000308 RID: 776
		private readonly string _root;

		// Token: 0x04000309 RID: 777
		private readonly QuickSaveSettings _settings;

		// Token: 0x0400030A RID: 778
		private Dictionary<string, object> _items;
	}
}
