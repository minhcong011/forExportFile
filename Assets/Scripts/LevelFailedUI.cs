// dnSpy decompiler from Assembly-CSharp.dll class: LevelFailedUI
using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelFailedUI : BaseUI
{
	private void Start()
	{
		this.failedString.text = Singleton<GameController>.Instance.gameSceneController.currentLevelMeta.getRandomfailedString();
		if (!Constants.getAddsPurchasedStatus())
		{
			this.videoBtn.SetActive(true);
		}
		this.setFailedStringAccordingToMode();
	}

	public void setFailedStringAccordingToMode()
	{
		int gameOverMode = Singleton<GameController>.Instance.gameSceneController.gameOverMode;
		this.failedString.text = Singleton<GameController>.Instance.gameSceneController.currentLevelMeta.getRandomfailedString();
		switch (gameOverMode)
		{
		case 1:
			this.failedString.text = "The terrorists are strong enough, fight with some plan!!";
			break;
		case 2:
			this.failedString.text = "You run out of Time!!";
			break;
		case 3:
			this.failedString.text = "You have to save animals, not to kill by your own!!";
			break;
		case 4:
			this.failedString.text = "You could not save other animals from Beasts!!";
			break;
		case 5:
			this.failedString.text = "Objectives not achieved!!";
			break;
		}
	}

	private void OnEnable()
	{
		int num = PlayerPrefs.GetInt("failedGiftCount" + Singleton<GameController>.Instance.SelectedLevel.ToString(), 0);
		num++;
		PlayerPrefs.SetInt("failedGiftCount" + Singleton<GameController>.Instance.SelectedLevel.ToString(), num);
	}

	private bool checkToGiveGift()
	{
		int @int = PlayerPrefs.GetInt("failedGiftCount" + Singleton<GameController>.Instance.SelectedLevel.ToString(), 0);
		int num = 3;
		if (@int < num)
		{
			return false;
		}
		PlayerPrefs.SetInt("failedGiftCount" + Singleton<GameController>.Instance.SelectedLevel.ToString(), 0);
		return true;
	}

	private void WatchVideo()
	{
		Singleton<GameController>.Instance.adsManager.rewardedVideoCB = new UKAdsManager.videoCallBack(this.OnFinished);
		if (!Singleton<GameController>.Instance.adsManager.ShowRewardedVideo(2))
		{
			if (!Singleton<GameController>.Instance.adsManager.ShowRewardedVideo(1))
			{
				Singleton<GameController>.Instance.adsManager.rewardedVideoCB = null;
				GameObject gameObject = Singleton<GameController>.Instance.uiPopUpManager.NotEnoughText("Video Not Available !!!");
			}
		}
	}

	public void InstantiateCongratsWindow()
	{
		GameObject gameObject = Singleton<GameController>.Instance.uiPopUpManager.InitAndGetRewardPopup();
		CongratsUIController component = gameObject.GetComponent<CongratsUIController>();
		component.reward.text = 300 + string.Empty;
	}

	private void OnFinished(int type)
	{
		Singleton<GameController>.Instance.storeManager.addCash(300);
		Singleton<GameController>.Instance.eventManager.UpdateStoreUI();
		Singleton<GameController>.Instance.adsManager.rewardedVideoCB = null;
		this.InstantiateCongratsWindow();
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
		}
	}

	public void CongratsOkBtn()
	{
		this.congratsWindow.SetActive(false);
	}

	public void ContinueWithCashOrGold()
	{
		if (Singleton<GameController>.Instance.storeManager.GetTotalCashCount() >= 500)
		{
			Singleton<GameController>.Instance.storeManager.consumeCash(500);
			base.gameObject.SetActive(false);
			return;
		}
		UnityEngine.Debug.Log("Not enough cash");
		this.InstantiateNotEnoughPanel();
	}

	public void ContinueWithVideo()
	{
		Singleton<GameController>.Instance.adsManager.rewardedVideoCB = new UKAdsManager.videoCallBack(this.RewardedVideoCallBack);
		if (!Singleton<GameController>.Instance.adsManager.ShowRewardedVideo(1))
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("NotEnoughAnimatedText")) as GameObject;
			gameObject.transform.SetParent(base.gameObject.transform);
			gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
			gameObject.transform.localScale = Vector3.one;
			gameObject.GetComponent<AnimatedText>().desc.text = "Video Not Available!!!";
			Vector2 v;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(base.gameObject.transform as RectTransform, UnityEngine.Input.mousePosition, null, out v);
			gameObject.transform.position = base.gameObject.transform.TransformPoint(v);
			gameObject.SetActive(true);
		}
	}

	public void RewardedVideoCallBack(int type)
	{
		base.gameObject.SetActive(false);
	}

	public void InstantiateNotEnoughPanel()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("NotEnoughCashUI")) as GameObject;
		gameObject.transform.SetParent(base.gameObject.transform);
		gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
		gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
	}

	public Text failedString;

	public GameObject congratsWindow;

	public GameObject videoBtn;

	private bool onceDone;
}
