// dnSpy decompiler from Assembly-CSharp.dll class: SettingsUIController
using System;
using Lean;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUIController : MonoBehaviour
{
	private void OnEnable()
	{
		this.setLanguage();
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		int num = UnityEngine.Random.Range(0, 2);
		if (adsManager != null && num == 1)
		{
			adsManager.showInterstitial("Exit");
		}
	}

	private void Awake()
	{
	}

	private void Start()
	{
		this.setBtns();
	}

	private void setBtns()
	{
		if (!PlayerPrefs.HasKey("music"))
		{
			PlayerPrefs.SetInt("music", 0);
			this.Music.sprite = this.onOffSprite[0];
		}
		else if (!Constants.isMusicOn())
		{
			this.Music.sprite = this.onOffSprite[1];
		}
		else
		{
			this.Music.sprite = this.onOffSprite[0];
		}
		if (!PlayerPrefs.HasKey("sound"))
		{
			PlayerPrefs.SetInt("sound", 0);
			this.Sound.sprite = this.onOffSprite[0];
		}
		else if (!Constants.isSoundOn())
		{
			this.Sound.sprite = this.onOffSprite[1];
		}
		else
		{
			this.Sound.sprite = this.onOffSprite[0];
		}
		float sensitivity = Constants.getSensitivity();
		this.sensitivitySlider.value = sensitivity - 0.5f;
	}

	public void SettingBtnsClicked(string action)
	{
		if (action != null)
		{
			if (action == "Back")
			{
				Singleton<GameController>.Instance.soundController.PlayButtonClick();
				UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(0);
			}
		}
	}

	public void MusicClicked()
	{
		if (Constants.isMusicOn())
		{
			this.Music.sprite = this.onOffSprite[1];
			Constants.setMusic(1);
			Singleton<GameController>.Instance.soundController.StopMusic();
		}
		else
		{
			this.Music.sprite = this.onOffSprite[0];
			Constants.setMusic(0);
			Singleton<GameController>.Instance.soundController.PlayMainMenuMusic();
		}
	}

	public void SoundClicked()
	{
		if (Constants.isSoundOn())
		{
			this.Sound.sprite = this.onOffSprite[1];
			Constants.setSound(1);
		}
		else
		{
			this.Sound.sprite = this.onOffSprite[0];
			Constants.setSound(0);
		}
	}

	public void sensitivityChanged()
	{
		Constants.setSensitivity(this.sensitivitySlider.value + 0.5f);
	}

	private void setLanguage()
	{
		this.change = PlayerPrefs.GetInt("DDValue", 0);
		this.dropdown.value = this.change;
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
		else if (this.change == 8)
		{
			LeanLocalization.Instance.SetLanguage("Dutch");
			this.Language = "Dutch";
			PlayerPrefs.SetString("Language", this.Language);
		}
		else if (this.change == 9)
		{
			LeanLocalization.Instance.SetLanguage("Italian");
			this.Language = "Italian";
			PlayerPrefs.SetString("Language", this.Language);
		}
		else if (this.change == 10)
		{
			LeanLocalization.Instance.SetLanguage("Turkish");
			this.Language = "Turkish";
			PlayerPrefs.SetString("Language", this.Language);
		}
		else if (this.change == 11)
		{
			LeanLocalization.Instance.SetLanguage("Spanish");
			this.Language = "Spanish";
			PlayerPrefs.SetString("Language", this.Language);
		}
	}

	public Sprite[] onOffSprite;

	public Image Sound;

	public Image Music;

	public Slider sensitivitySlider;

	public AudioClip clickClip;

	public AudioSource source;

	public string Language;

	public Dropdown dropdown;

	public int change;
}
