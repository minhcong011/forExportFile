using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace CI.QuickSave.Core.Storage
{
	// Token: 0x0200009B RID: 155
	public class FileAccessMono : IFileAccess
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x00012764 File Offset: 0x00010964
		public bool SaveString(string filename, string value)
		{
			try
			{
				this.CreateRootFolder();
				using (StreamWriter streamWriter = new StreamWriter(Path.Combine(FileAccessMono._basePath, filename)))
				{
					streamWriter.Write(value);
				}
				return true;
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000127C4 File Offset: 0x000109C4
		public bool SaveBytes(string filename, byte[] value)
		{
			try
			{
				this.CreateRootFolder();
				using (FileStream fileStream = new FileStream(Path.Combine(FileAccessMono._basePath, filename), FileMode.Create))
				{
					fileStream.Write(value, 0, value.Length);
				}
				return true;
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00012828 File Offset: 0x00010A28
		public string LoadString(string filename)
		{
			try
			{
				this.CreateRootFolder();
				if (this.Exists(filename))
				{
					using (StreamReader streamReader = new StreamReader(Path.Combine(FileAccessMono._basePath, filename)))
					{
						return streamReader.ReadToEnd();
					}
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0001288C File Offset: 0x00010A8C
		public byte[] LoadBytes(string filename)
		{
			try
			{
				this.CreateRootFolder();
				if (this.Exists(filename))
				{
					using (FileStream fileStream = new FileStream(Path.Combine(FileAccessMono._basePath, filename), FileMode.Open))
					{
						byte[] array = new byte[fileStream.Length];
						fileStream.Read(array, 0, array.Length);
						return array;
					}
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00012908 File Offset: 0x00010B08
		public void Delete(string filename)
		{
			try
			{
				this.CreateRootFolder();
				File.Delete(Path.Combine(FileAccessMono._basePath, filename));
			}
			catch
			{
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00012940 File Offset: 0x00010B40
		public bool Exists(string filename)
		{
			return File.Exists(Path.Combine(FileAccessMono._basePath, filename));
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00012954 File Offset: 0x00010B54
		public IEnumerable<string> Files(bool includeExtensions)
		{
			try
			{
				this.CreateRootFolder();
				if (includeExtensions)
				{
					return from x in Directory.GetFiles(FileAccessMono._basePath, "*.json")
					select Path.GetFileName(x);
				}
				return from x in Directory.GetFiles(FileAccessMono._basePath, "*.json")
				select Path.GetFileNameWithoutExtension(x);
			}
			catch
			{
			}
			return new List<string>();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000129F0 File Offset: 0x00010BF0
		private void CreateRootFolder()
		{
			if (!Directory.Exists(FileAccessMono._basePath))
			{
				Directory.CreateDirectory(FileAccessMono._basePath);
			}
		}

		// Token: 0x04000316 RID: 790
		private static readonly string _basePath = Path.Combine(Application.persistentDataPath, "QuickSave");
	}
}
