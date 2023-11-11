using System;
using System.Collections.Generic;

namespace CI.QuickSave.Core.Storage
{
	// Token: 0x0200009A RID: 154
	public static class FileAccess
	{
		// Token: 0x060002CE RID: 718 RVA: 0x000126B8 File Offset: 0x000108B8
		public static bool SaveString(string filename, bool includesExtension, string value)
		{
			return FileAccess._storage.SaveString(FileAccess.GetFilenameWithExtension(filename, includesExtension), value);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000126CC File Offset: 0x000108CC
		public static bool SaveBytes(string filename, bool includesExtension, byte[] value)
		{
			return FileAccess._storage.SaveBytes(FileAccess.GetFilenameWithExtension(filename, includesExtension), value);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000126E0 File Offset: 0x000108E0
		public static string LoadString(string filename, bool includesExtension)
		{
			return FileAccess._storage.LoadString(FileAccess.GetFilenameWithExtension(filename, includesExtension));
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000126F3 File Offset: 0x000108F3
		public static byte[] LoadBytes(string filename, bool includesExtension)
		{
			return FileAccess._storage.LoadBytes(FileAccess.GetFilenameWithExtension(filename, includesExtension));
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00012706 File Offset: 0x00010906
		public static void Delete(string filename, bool includesExtension)
		{
			FileAccess._storage.Delete(FileAccess.GetFilenameWithExtension(filename, includesExtension));
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00012719 File Offset: 0x00010919
		public static bool Exists(string filename, bool includesExtension)
		{
			return FileAccess._storage.Exists(FileAccess.GetFilenameWithExtension(filename, includesExtension));
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0001272C File Offset: 0x0001092C
		private static string GetFilenameWithExtension(string filename, bool includesExtension)
		{
			if (!includesExtension)
			{
				return filename + FileAccess._defaultExtension;
			}
			return filename;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0001273E File Offset: 0x0001093E
		public static IEnumerable<string> Files(bool includeExtensions)
		{
			return FileAccess._storage.Files(includeExtensions);
		}

		// Token: 0x04000314 RID: 788
		private static readonly string _defaultExtension = ".json";

		// Token: 0x04000315 RID: 789
		private static IFileAccess _storage = new FileAccessMono();
	}
}
