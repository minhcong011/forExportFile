// dnSpy decompiler from Assembly-CSharp.dll class: Lean.LeanPhrase
using System;
using System.Collections.Generic;

namespace Lean
{
	[Serializable]
	public class LeanPhrase
	{
		public LeanTranslation FindTranslation(string language)
		{
			return this.Translations.Find((LeanTranslation t) => t.Language == language);
		}

		public LeanTranslation AddTranslation(string language)
		{
			LeanTranslation leanTranslation = this.FindTranslation(language);
			if (leanTranslation == null)
			{
				leanTranslation = new LeanTranslation();
				leanTranslation.Language = language;
				this.Translations.Add(leanTranslation);
			}
			return leanTranslation;
		}

		public string Name;

		public List<LeanTranslation> Translations = new List<LeanTranslation>();
	}
}
