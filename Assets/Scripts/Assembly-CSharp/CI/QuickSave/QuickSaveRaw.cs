// ILSpyBased#2
using CI.QuickSave.Core.Security;
using CI.QuickSave.Core.Storage;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CI.QuickSave
{
    public class QuickSaveRaw
    {
        public static void SaveString(string filename, string content)
        {
            QuickSaveRaw.SaveString(filename, content, new QuickSaveSettings());
        }

        public static void SaveString(string filename, string content, QuickSaveSettings settings)
        {
            string value;
            try
            {
                value = Cryptography.Encrypt(content, settings.SecurityMode, settings.Password);
            }
            catch (Exception innerException)
            {
                throw new QuickSaveException("Encryption failed", innerException);
            }
            if (FileAccess.SaveString(filename, true, value))
            {
                return;
            }
            throw new QuickSaveException("Failed to write to file");
        }

        public static void SaveBytes(string filename, byte[] content)
        {
            if (!FileAccess.SaveBytes(filename, true, content))
            {
                return;
            }
            throw new QuickSaveException("Failed to write to file");
        }

        public static string LoadString(string filename)
        {
            return QuickSaveRaw.LoadString(filename, new QuickSaveSettings());
        }

        public static string LoadString(string filename, QuickSaveSettings settings)
        {
            string text = FileAccess.LoadString(filename, true);
            if (text == null)
            {
                throw new QuickSaveException("Failed to load file");
            }
            try
            {
                return Cryptography.Decrypt(text, settings.SecurityMode, settings.Password);
            }
            catch (Exception innerException)
            {
                throw new QuickSaveException("Decryption failed", innerException);
            }
        }

        public static byte[] LoadBytes(string filename)
        {
            byte[] array = FileAccess.LoadBytes(filename, true);
            if (array == null)
            {
                throw new QuickSaveException("Failed to load file");
            }
            return array;
        }

        public static T LoadResource<T>(string filename) where T : UnityEngine.Object
        {
            return Resources.Load<T>(filename);
        }

        public static void Delete(string filename)
        {
            FileAccess.Delete(filename, true);
        }

        public static bool Exists(string filename)
        {
            return FileAccess.Exists(filename, true);
        }

        public static IEnumerable<string> GetAllFiles()
        {
            return FileAccess.Files(false);
        }
    }
}


