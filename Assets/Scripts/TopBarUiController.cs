// dnSpy decompiler from Assembly-CSharp.dll class: TopBarUiController
using System;
using UnityEngine;
using UnityEngine.UI;

public class TopBarUiController : MonoBehaviour
{
	private void OnEnable()
	{
		EventManager.UIUpdate += this.updateEventCB;
		OpenIABEventManager.UIEventTrigger += this.PurchasedCB;
	}

	private void OnDisable()
	{
		EventManager.UIUpdate -= this.updateEventCB;
		OpenIABEventManager.UIEventTrigger -= this.PurchasedCB;
	}

	private void updateEventCB()
	{
		this.updateTopBar();
	}

	private void Start()
	{
		this.refStoreManager = UnityEngine.Object.FindObjectOfType<StoreManager>();
		this.refScreenHandler = UnityEngine.Object.FindObjectOfType<UIScreensHandler>();
		this.ShowBackBtn(false);
		this.updateTopBar();
		if (!Constants.getAddsPurchasedStatus())
		{
			this.removeAds.GetComponent<Button>().interactable = true;
		}
		else
		{
			this.removeAds.GetComponent<Button>().interactable = false;
		}
	}

	public void updateTopBar()
	{
		this.updateTotalCoinsText();
		this.updateTotalCashText();
		this.updateTotalXpText();
	}

	public void updateTotalCoinsText()
	{
		this.coinsCount.text = this.refStoreManager.GetTotalCoinsCount().ToString();
	}

	public void updateTotalCashText()
	{
		this.cashCount.text = this.refStoreManager.GetTotalCashCount().ToString();
	}

	public void updateTotalXpText()
	{
		this.xpCount.text = this.refStoreManager.GetTotalXPCount().ToString();
	}

	public void ShowBackBtn(bool val = true)
	{
		this.backBtn.SetActive(val);
	}

	public void ShowExitBtn(bool val = true)
	{
		this.exitBtn.SetActive(val);
	}

	public void showBtnAccordingToScreen(int id)
	{
		if (id == 0)
		{
			this.ShowBackBtn(false);
			this.ShowExitBtn(true);
		}
		else
		{
			this.ShowBackBtn(true);
			this.ShowExitBtn(false);
		}
	}

	public void updateUI()
	{
	}

	public void ButtonClicks(string action)
	{
		if (action != null)
		{
			if (!(action == "InappCash"))
			{
				if (!(action == "InappGold"))
				{
					if (!(action == "RateUs"))
					{
						if (!(action == "Back"))
						{
							if (!(action == "LeaderBoard"))
							{
								if (action == "Achievement")
								{
									Singleton<GameController>.Instance.soundController.PlayButtonClick();
								}
							}
							else
							{
								Singleton<GameController>.Instance.soundController.PlayButtonClick();
							}
						}
						else
						{
							this.refScreenHandler.BackPressed(0);
							Singleton<GameController>.Instance.soundController.PlayButtonClick();
						}
					}
					else
					{
						Singleton<GameController>.Instance.soundController.PlayButtonClick();
						Application.OpenURL(Constants.RATE_US_LINK);
					}
				}
				else
				{
					if (this.refScreenHandler.screenId == 11)
					{
						return;
					}
					Singleton<GameController>.Instance.inAppId = 1;
					this.refScreenHandler.setScreen(11);
					Singleton<GameController>.Instance.soundController.PlayButtonClick();
				}
			}
			else
			{
				if (this.refScreenHandler.screenId == 11)
				{
					return;
				}
				this.refScreenHandler.setScreen(11);
				Singleton<GameController>.Instance.soundController.PlayButtonClick();
			}
		}
	}

	public void PurchaseRemoveAds()
	{
		OpenIABPurchaseHandler openIABPurchaseHandler = UnityEngine.Object.FindObjectOfType<OpenIABPurchaseHandler>();
		if (openIABPurchaseHandler != null)
		{
			openIABPurchaseHandler.PurchasePressed(4);
		}
	}

	public void PurchasedCB(int id)
	{
		if (id == 4)
		{
			MonoBehaviour.print("RemoveAds CB");
			Constants.setAddsPurchasedStatus();
			this.CallToStopAllAds();
			UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
			if (adsManager != null)
			{
			}
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

	public void WatchVideo()
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

	public GameObject exitBtn;

	public GameObject backBtn;

	public GameObject removeAds;

	public Text coinsCount;

	public Text cashCount;

	public Text xpCount;

	[HideInInspector]
	public StoreManager refStoreManager;

	private UIScreensHandler refScreenHandler;
}
