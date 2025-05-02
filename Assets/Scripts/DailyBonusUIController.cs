// dnSpy decompiler from Assembly-CSharp.dll class: DailyBonusUIController
using System;
using System.Collections.Generic;
using UnityEngine;

public class DailyBonusUIController : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnEnable()
	{
		this.dailyBonusManager = Singleton<GameController>.Instance.dailyBonusManager;
		if (this.dailyBonusManager == null)
		{
			return;
		}
		this.currentDay = this.dailyBonusManager.currentDay;
		this.initialiseItems();
	}

	private void OnDisable()
	{
		this.rewardClaimUI.SetActive(false);
	}

	private void initialiseItems()
	{
		int num = this.currentDay;
		for (int i = 0; i < this.listOfItemUI.Count; i++)
		{
			this.listOfItemUI[i].setData(this.dailyBonusManager.getItemByDay(num));
			num++;
		}
	}

	public void CollectReward()
	{
		DailyBonusRewardItem itemByDay = this.dailyBonusManager.getItemByDay(this.currentDay);
		if (itemByDay == null)
		{
			UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(0);
			return;
		}
		List<int> reward = itemByDay.reward;
		for (int i = 0; i < reward.Count; i++)
		{
			if (reward[i] != 0)
			{
				if (i == 0)
				{
					Singleton<GameController>.Instance.storeManager.addCash(reward[0]);
					this.rewardClaimUI.GetComponent<RewardpopupUIController>().enableAndSet(this.rewardImages[0], 0, 0);
				}
				else if (i == 1)
				{
					Singleton<GameController>.Instance.storeManager.addCoins(reward[1]);
					this.rewardClaimUI.GetComponent<RewardpopupUIController>().enableAndSet(this.rewardImages[1], 1, 0);
				}
				else if (i == 2)
				{
					Singleton<GameController>.Instance.storeManager.GiveMediPack(reward[2]);
					this.rewardClaimUI.GetComponent<RewardpopupUIController>().enableAndSet(this.rewardImages[2], 0, 0);
				}
				else if (i == 3)
				{
					Singleton<GameController>.Instance.storeManager.addSpins(reward[3]);
					this.rewardClaimUI.GetComponent<RewardpopupUIController>().enableAndSet(this.rewardImages[3], 0, 0);
				}
			}
		}
		this.dailyBonusManager.UpdateNextDay();
		Singleton<GameController>.Instance.soundController.PlayClaimSound();
		Singleton<GameController>.Instance.eventManager.UpdateStoreUI();
	}

	public List<DailyBonusRewardItemUI> listOfItemUI;

	private int currentDay = 1;

	private DailyBonusManager dailyBonusManager;

	public GameObject rewardClaimUI;

	public Sprite[] rewardImages;
}
