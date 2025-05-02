// dnSpy decompiler from Assembly-CSharp.dll class: MainMenuUIController
using System;
using System.Collections;
using Lean;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
	private void OnEnable()
	{
		Singleton<GameController>.Instance.shopOpened = false;
		base.Invoke("ShowRateUs", 1.5f);
		OpenIABEventManager.UIEventTrigger += this.PurchasedCB;
	}

	private void OnDisable()
	{
		OpenIABEventManager.UIEventTrigger -= this.PurchasedCB;
	}

	private void ShowRateUs()
	{
		if (Singleton<GameController>.Instance.rateUsController != null)
		{
			Singleton<GameController>.Instance.rateUsController.showWindow(base.transform.parent);
		}
	}

	private void Awake()
	{
		this.setLanguage();
	}

	private void Start()
	{
		//StartCoroutine(Singleton<GameController>.Instance.adsManager.ShowAppOpenAd());

        Time.timeScale = 1f;
		this.SetInitialPrefs();
		base.Invoke("showBanner", 2f);
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
			adsManager.StartSession();
			adsManager.LogScreen("MainMenu- ");
		}
		this.setLanguage();
	}

	private void SetInitialPrefs()
	{
		int @int = PlayerPrefs.GetInt("AddsBought", 0);
		if (@int == 1)
		{
		}
	}

	private void showBanner()
	{
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
			adsManager.ShowBanner("AdMob");
		}
	}

	public void ButtonClick(string act)
	{
		switch (act)
		{
		case "Play":
			Singleton<GameController>.Instance.soundController.PlayButtonClick();
			if (Singleton<GameController>.Instance.getUnlockedMissions() > 1 || Singleton<GameController>.Instance.getUnlockedLevels() > 1)
			{
				UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(2);
			}
			else if (this.loadingGamePlay != null)
			{
				this.loadingGamePlay.LoadScene();
			}
			break;
		case "Store":
			UnityEngine.SceneManagement.SceneManager.LoadScene("Store");
			Singleton<GameController>.Instance.soundController.PlayButtonClick();
			break;
		case "StoreInApp":
			Singleton<GameController>.Instance.shopOpened = true;
			UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(6);
			Singleton<GameController>.Instance.soundController.PlayButtonClick();
			break;
		case "Settings":
			UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(3);
			Singleton<GameController>.Instance.soundController.PlayButtonClick();
			break;
		case "Exit":
			this.ExitPressed();
			Singleton<GameController>.Instance.soundController.PlayButtonClick();
			break;
		case "WatchVideo":
			this.watchAdPanel.SetActive(true);
			break;
		case "WatchVideoOk":
			UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(0);
			Singleton<GameController>.Instance.soundController.PlayButtonClick();
			break;
		case "RemoveAds":
			this.PurchaseRemoveAds();
			Singleton<GameController>.Instance.soundController.PlayButtonClick();
			break;
		case "Weapon":
			UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(8);
			Singleton<GameController>.Instance.soundController.PlayButtonClick();
			break;
		case "DailyLogin":
			UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(9);
			Singleton<GameController>.Instance.soundController.PlayButtonClick();
			break;
		case "DailyTask":
			UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(10);
			Singleton<GameController>.Instance.soundController.PlayButtonClick();
			break;
		}
	}

	public void StartRecording()
	{
	}

	public void StopRecording()
	{
	}

	public void PurchaseRemoveAds()
	{
		OpenIABPurchaseHandler openIABPurchaseHandler = UnityEngine.Object.FindObjectOfType<OpenIABPurchaseHandler>();
		if (openIABPurchaseHandler != null)
		{
			openIABPurchaseHandler.PurchasePressed(4);
		}
	}

	public void Play()
	{
		UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(1);
	}

	public void ratePressed()
	{
		Application.OpenURL(Constants.RATE_US_LINK);
	}

	public void settingPressed()
	{
		base.StartCoroutine(this.waitforSound(1));
	}

	public void moreGamesPressed()
	{
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
		Application.OpenURL(Constants.MORE_GAMES_LINK);
	}

	public void InfosPressed()
	{
		base.StartCoroutine(this.waitforSound(4));
	}

	public void ExitPressed()
	{
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
		UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(4);
	}

	private IEnumerator waitforSound(int id)
	{
		if (Constants.isSoundOn())
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.clickClip, 1f);
		}
		yield return new WaitForSeconds(0.1f);
		if (id != 0)
		{
			if (id != 2)
			{
				if (id == 3)
				{
					Application.OpenURL(string.Empty);
				}
			}
			else
			{
				Application.OpenURL(string.Empty);
			}
		}
		else
		{
			yield return new WaitForSeconds(0.31f);
		}
		yield break;
	}

	public void WatchVideo()
	{
		Singleton<GameController>.Instance.adsManager.rewardedVideoCB = new UKAdsManager.videoCallBack(this.OnFinished);
		if (Singleton<GameController>.Instance.adsManager.ShowRewardedVideo(2))
		{
			this.watchAdPanel.SetActive(false);
		}
		else if (Singleton<GameController>.Instance.adsManager.ShowRewardedVideo(1))
		{
			this.watchAdPanel.SetActive(false);
		}
		else
		{
			Singleton<GameController>.Instance.adsManager.rewardedVideoCB = null;
			GameObject gameObject = Singleton<GameController>.Instance.uiPopUpManager.NotEnoughText("Video Not Available !!!");
		}
	}

	public void InstantiateCongratsWindow()
	{
		GameObject gameObject = Singleton<GameController>.Instance.uiPopUpManager.InitAndGetRewardPopup();
		CongratsUIController component = gameObject.GetComponent<CongratsUIController>();
		component.reward.text = 500 + string.Empty;
	}

	private void OnFinished(int type)
	{
		UnityEngine.Debug.Log(" rewarded video complete main menu");
		Singleton<GameController>.Instance.storeManager.addCash(500);
		Singleton<GameController>.Instance.eventManager.UpdateStoreUI();
		Singleton<GameController>.Instance.adsManager.rewardedVideoCB = null;
		this.InstantiateCongratsWindow();
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
		}
	}

	private void setLanguage()
	{
		if (!PlayerPrefs.HasKey("Language"))
		{
			this.Language = "English";
			PlayerPrefs.SetString("Language", this.Language);
		}
		else
		{
			this.Language = PlayerPrefs.GetString("Language", "English");
		}
		LeanLocalization.GetInstance().SetLanguage(this.Language);
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

	public void PurchasedCB(int id)
	{
		if (id == 4)
		{
			MonoBehaviour.print("RemoveAds CB");
			Constants.setAddsPurchasedStatus();
			this.CallToStopAllAds();
		}
	}

	private void CallToStopAllAds()
	{
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
			adsManager.HideBanner("AdMob");
		}
		this.removeAds.GetComponent<Button>().interactable = false;
	}

	public AudioClip clickClip;

	public GameObject removeAds;

	public CanvasGroup playSelectorCG;

	public Image watchVdoBtn;

	public Button freeCashBtn;

	public Text res;

	private float AlphaVal;

	private bool forward = true;

	public CanvasGroup cg;

	public GameObject hzText;

	public GameObject admobtext;

	public GameObject watchAdPanel;

	public LoadingGamePlay loadingGamePlay;

	public string Language;

	public Dropdown dropdown;

	public int change;
}
