// dnSpy decompiler from Assembly-CSharp.dll class: DailyLoginUIController
using System;
using UnityEngine;
using UnityEngine.UI;

public class DailyLoginUIController : MonoBehaviour
{
	private void Start()
	{
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
			adsManager.LogScreen("Daily Spin Menu-");
		}
	}

	private void OnEnable()
	{
		this.spinsAvailable = Singleton<GameController>.Instance.storeManager.GetTotalSpins();
		if (Singleton<GameController>.Instance.dailyLoginManager.isSpinAvailable || this.spinsAvailable > 0)
		{
			this.playBtn.interactable = true;
		}
		else
		{
			this.playBtn.interactable = false;
		}
		this.buyspinBtn.interactable = true;
		int num = (!Singleton<GameController>.Instance.dailyLoginManager.isSpinAvailable) ? 0 : 1;
		this.spinsCountText.text = "X " + (this.spinsAvailable + num).ToString();
	}

	private void OnDisable()
	{
		Singleton<GameController>.Instance.eventManager.UpdateStoreUI();
	}

	public void ButtonPressed(string action)
	{
		if (action != null)
		{
			if (!(action == "Exit"))
			{
				if (!(action == "Play"))
				{
					if (action == "buySpin")
					{
						this.checkAndSpin();
					}
				}
				else
				{
					this.playBtn.interactable = false;
					this.buyspinBtn.interactable = false;
					this.refBonus.ChoiceRandom(true);
				}
			}
			else
			{
				UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(0);
			}
		}
	}

	private void checkAndSpin()
	{
		if (Singleton<GameController>.Instance.storeManager.GetTotalCoinsCount() >= 3)
		{
			this.refBonus.ChoiceRandom(false);
			this.buyspinBtn.interactable = false;
			this.playBtn.interactable = false;
			Singleton<GameController>.Instance.storeManager.consumeCoins(3);
			Singleton<GameController>.Instance.eventManager.UpdateStoreUI();
		}
		else
		{
			UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(11);
		}
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
	}

	public void spinComplete(int choice, bool val = true)
	{
		if (val)
		{
			if (this.spinsAvailable <= 0)
			{
				Singleton<GameController>.Instance.dailyLoginManager.UpdateNextDay();
				if (Singleton<GameController>.Instance.achievementManager != null)
				{
					Singleton<GameController>.Instance.achievementManager.updateAchievement(3);
				}
			}
			else
			{
				Singleton<GameController>.Instance.storeManager.consumeSpin();
				this.spinsAvailable = Singleton<GameController>.Instance.storeManager.GetTotalSpins();
			}
		}
		if (Singleton<GameController>.Instance.dailyLoginManager.isSpinAvailable || this.spinsAvailable > 0)
		{
			this.playBtn.interactable = true;
		}
		this.buyspinBtn.interactable = true;
		this.showRewardPopUp(choice);
		int num = (!Singleton<GameController>.Instance.dailyLoginManager.isSpinAvailable) ? 0 : 1;
		this.spinsCountText.text = "X " + (this.spinsAvailable + num).ToString();
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
		}
	}

	private void showRewardPopUp(int id)
	{
		switch (id)
		{
		case 1:
			this.rewardText.text = "You have got 300 cash";
			Singleton<GameController>.Instance.storeManager.addCash(300);
			break;
		case 2:
			this.rewardText.text = "You have got a shield";
			Singleton<GameController>.Instance.storeManager.setArmourPurchased(0);
			break;
		case 3:
			this.rewardText.text = "You have got 500 cash";
			Singleton<GameController>.Instance.storeManager.addCash(500);
			break;
		case 4:
			this.rewardText.text = "You have got 3 MediKits";
			Singleton<GameController>.Instance.storeManager.GiveMediPack(2);
			break;
		case 5:
			this.rewardText.text = "You have got 2 MediKits";
			Singleton<GameController>.Instance.storeManager.GiveMediPack(1);
			break;
		case 6:
			this.rewardText.text = "You have got free spin";
			Singleton<GameController>.Instance.storeManager.addSpins(1);
			break;
		case 7:
			this.rewardText.text = "You have got 5 golds";
			Singleton<GameController>.Instance.storeManager.addCoins(5);
			break;
		case 8:
			this.rewardText.text = "You have got 10 golds";
			Singleton<GameController>.Instance.storeManager.addCoins(10);
			break;
		}
		this.rewardPanel.SetActive(true);
	}

	public void rewardOK()
	{
		this.rewardPanel.SetActive(false);
		Singleton<GameController>.Instance.soundController.PlayClaimSound();
		Singleton<GameController>.Instance.eventManager.UpdateStoreUI();
	}

	public DailyBonusController refBonus;

	public Button playBtn;

	public Button buyspinBtn;

	public GameObject rewardPanel;

	public Text rewardText;

	public Text spinsCountText;

	private int spinsAvailable;
}
