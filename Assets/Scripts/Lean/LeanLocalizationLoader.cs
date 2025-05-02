// dnSpy decompiler from Assembly-CSharp.dll class: Lean.LeanLocalizationLoader
using System;
using UnityEngine;

namespace Lean
{
	[AddComponentMenu("Lean/Localization Loader")]
	public class LeanLocalizationLoader : MonoBehaviour
	{
		protected virtual void Start()
		{
			this.LoadFromSource();
		}

		[ContextMenu("Load From Source")]
		public void LoadFromSource()
		{
			if (this.Source != null && !string.IsNullOrEmpty(this.SourceLanguage))
			{
				LeanLocalization instance = LeanLocalization.GetInstance();
				string[] array = this.Source.text.Split(LeanLocalizationLoader.newlineCharacters, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text in array)
				{
					int num = text.IndexOf('=');
					if (num != -1)
					{
						string phraseName = text.Substring(0, num).Trim();
						string text2 = text.Substring(num + 1).Trim();
						text2 = text2.Replace(LeanLocalizationLoader.newlineString, Environment.NewLine);
						int num2 = text2.IndexOf("//");
						if (num2 != -1)
						{
							text2 = text2.Substring(0, num2).Trim();
						}
						LeanTranslation leanTranslation = instance.AddTranslation(this.SourceLanguage, phraseName);
						leanTranslation.Text = text2;
					}
				}
				if (instance.CurrentLanguage == this.SourceLanguage)
				{
					LeanLocalization.UpdateTranslations();
				}
			}
		}

		[LeanLanguageName]
		public string SourceLanguage;

		public TextAsset Source;

		private static readonly char[] newlineCharacters = new char[]
		{
			'\r',
			'\n'
		};

		private static readonly string newlineString = "\\n";
	}
}
