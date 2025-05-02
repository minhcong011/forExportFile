// dnSpy decompiler from Assembly-CSharp.dll class: Lean.MainMenuLocalization
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lean
{
	public class MainMenuLocalization : MonoBehaviour
	{
		private void Awake()
		{
			if (PlayerPrefs.GetString("Language") == null)
			{
				this.Language = "English";
			}
			else
			{
				this.Language = PlayerPrefs.GetString("Language");
			}
			LeanLocalization.Instance.CurrentLanguage = this.Language;
			this.change = PlayerPrefs.GetInt("DDValue", 0);
			this.dropdown.value = this.change;
		}

		private void Start()
		{
		}

		private void Update()
		{
		}

		public void FuctionDropdown(Dropdown value)
		{
			this.change = value.value;
			PlayerPrefs.SetInt("DDValue", this.change);
			if (this.change == 0)
			{
				LeanLocalization.Instance.SetLanguage("English");
				this.Language = "English";
				PlayerPrefs.SetString("Language", this.Language);
			}
			else if (this.change == 1)
			{
				LeanLocalization.Instance.SetLanguage("Chinese");
				this.Language = "Chinese";
				PlayerPrefs.SetString("Language", this.Language);
			}
			else if (this.change == 2)
			{
				LeanLocalization.Instance.SetLanguage("French");
				this.Language = "French";
				PlayerPrefs.SetString("Language", this.Language);
			}
			else if (this.change == 3)
			{
				LeanLocalization.Instance.SetLanguage("Russian");
				this.Language = "Russian";
				PlayerPrefs.SetString("Language", this.Language);
			}
			else if (this.change == 4)
			{
				LeanLocalization.Instance.SetLanguage("German");
				this.Language = "German";
				PlayerPrefs.SetString("Language", this.Language);
			}
			else if (this.change == 5)
			{
				LeanLocalization.Instance.SetLanguage("Korean");
				this.Language = "Korean";
				PlayerPrefs.SetString("Language", this.Language);
			}
			else if (this.change == 6)
			{
				LeanLocalization.Instance.SetLanguage("Indonesian");
				this.Language = "Indonesian";
				PlayerPrefs.SetString("Language", this.Language);
			}
			else if (this.change == 7)
			{
				LeanLocalization.Instance.SetLanguage("Japanese");
				this.Language = "Japanese";
				PlayerPrefs.SetString("Language", this.Language);
			}
			if (this.change == 8)
			{
				LeanLocalization.Instance.SetLanguage("Dutch");
				this.Language = "Dutch";
				PlayerPrefs.SetString("Language", this.Language);
			}
			if (this.change == 9)
			{
				LeanLocalization.Instance.SetLanguage("Italian");
				this.Language = "Italian";
				PlayerPrefs.SetString("Language", this.Language);
			}
			this.dropdown.value = this.change;
		}

		public void MainMenuFunction(string value)
		{
			if (value != null)
			{
				if (!(value == "MissionSelection"))
				{
					if (value == "MainMenu")
					{
						UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
					}
				}
				else
				{
					UnityEngine.SceneManagement.SceneManager.LoadScene("MissionSelection");
				}
			}
		}

		public string Language;

		public Dropdown dropdown;

		public int change;
	}
}
