// dnSpy decompiler from Assembly-CSharp.dll class: Lean.LeanLocalization
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lean
{
	[ExecuteInEditMode]
	[AddComponentMenu("Lean/Localization")]
	public class LeanLocalization : MonoBehaviour
	{
		public static LeanLocalization GetInstance()
		{
			if (LeanLocalization.Instance == null)
			{
				LeanLocalization.Instance = new GameObject(typeof(LeanLocalization).Name).AddComponent<LeanLocalization>();
			}
			return LeanLocalization.Instance;
		}

		public void SetLanguage(string newLanguage)
		{
			if (this.CurrentLanguage != newLanguage)
			{
				this.CurrentLanguage = newLanguage;
				LeanLocalization.UpdateTranslations();
			}
		}

		public static LeanTranslation GetTranslation(string phraseName)
		{
			LeanTranslation result = null;
			if (phraseName != null)
			{
				LeanLocalization.Translations.TryGetValue(phraseName, out result);
			}
			return result;
		}

		public static string GetTranslationText(string phraseName)
		{
			LeanTranslation translation = LeanLocalization.GetTranslation(phraseName);
			if (translation != null)
			{
				return translation.Text;
			}
			return null;
		}

		public static UnityEngine.Object GetTranslationObject(string phraseName)
		{
			LeanTranslation translation = LeanLocalization.GetTranslation(phraseName);
			if (translation != null)
			{
				return translation.Object;
			}
			return null;
		}

		public void AddLanguage(string language)
		{
			if (!this.Languages.Contains(language))
			{
				this.Languages.Add(language);
			}
		}

		public LeanPhrase AddPhrase(string phraseName)
		{
			LeanPhrase leanPhrase = this.Phrases.Find((LeanPhrase p) => p.Name == phraseName);
			if (leanPhrase == null)
			{
				leanPhrase = new LeanPhrase();
				leanPhrase.Name = phraseName;
				this.Phrases.Add(leanPhrase);
			}
			return leanPhrase;
		}

		public LeanTranslation AddTranslation(string language, string phraseName)
		{
			this.AddLanguage(language);
			return this.AddPhrase(phraseName).AddTranslation(language);
		}

		public static void UpdateTranslations()
		{
			LeanLocalization.Translations.Clear();
			if (LeanLocalization.Instance != null)
			{
				for (int i = LeanLocalization.Instance.Phrases.Count - 1; i >= 0; i--)
				{
					LeanPhrase leanPhrase = LeanLocalization.Instance.Phrases[i];
					if (!LeanLocalization.Translations.ContainsKey(leanPhrase.Name))
					{
						LeanTranslation leanTranslation = leanPhrase.FindTranslation(LeanLocalization.Instance.CurrentLanguage);
						if (leanTranslation != null)
						{
							LeanLocalization.Translations.Add(leanPhrase.Name, leanTranslation);
						}
					}
				}
			}
			if (LeanLocalization.OnLocalizationChanged != null)
			{
				LeanLocalization.OnLocalizationChanged();
			}
		}

		protected virtual void OnEnable()
		{
			if (LeanLocalization.Instance != null && LeanLocalization.Instance != this)
			{
				LeanLocalization.MergeLocalizations(LeanLocalization.Instance, this);
				UnityEngine.Debug.LogWarning("Your scene already contains a " + typeof(LeanLocalization).Name + ", merging & destroying the old one...");
				UnityEngine.Object.DestroyImmediate(LeanLocalization.Instance.gameObject);
			}
			LeanLocalization.Instance = this;
			LeanLocalization.UpdateTranslations();
		}

		protected virtual void OnDisable()
		{
			if (LeanLocalization.Instance == this)
			{
				LeanLocalization.Instance = null;
			}
			LeanLocalization.UpdateTranslations();
		}

		private static void MergeLocalizations(LeanLocalization oldLocalization, LeanLocalization newLocalization)
		{
			for (int i = oldLocalization.Phrases.Count - 1; i >= 0; i--)
			{
				LeanPhrase leanPhrase = oldLocalization.Phrases[i];
				LeanPhrase leanPhrase2 = newLocalization.AddPhrase(leanPhrase.Name);
				for (int j = leanPhrase.Translations.Count - 1; j >= 0; j--)
				{
					LeanTranslation leanTranslation = leanPhrase.Translations[j];
					LeanTranslation leanTranslation2 = leanPhrase2.AddTranslation(leanTranslation.Language);
					leanTranslation2.Text = leanTranslation.Text;
					leanTranslation2.Object = leanTranslation.Object;
				}
			}
		}

		public static LeanLocalization Instance;

		public static Dictionary<string, LeanTranslation> Translations = new Dictionary<string, LeanTranslation>();

		public static Action OnLocalizationChanged;

		public List<string> Languages = new List<string>();

		public List<LeanPhrase> Phrases = new List<LeanPhrase>();

		[LeanLanguageName]
		public string CurrentLanguage;
	}
}
