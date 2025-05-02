// dnSpy decompiler from Assembly-CSharp.dll class: DailyBonusManager
using System;
using System.Collections.Generic;
using UnityEngine;

public class DailyBonusManager : MonoBehaviour
{
	private void Awake()
	{
		if (!DailyBonusManager.isCreated)
		{
			DailyBonusManager.isCreated = true;
			Singleton<GameController>.Instance.dailyBonusManager = this;
			UnityEngine.Object.DontDestroyOnLoad(this);
			this.checkForDay();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Start()
	{
	}

	private void checkForDay()
	{
		int @int = PlayerPrefs.GetInt("DailyBonusStarted", 0);
		this.currentDay = PlayerPrefs.GetInt("DailyBonusDay", 1);
		if (@int == 0)
		{
			this.bonusReady = true;
		}
		else
		{
			if (this.currentDay > this.rewardItemsList.Count)
			{
				return;
			}
			if (PlayerPrefs.GetInt("DailyBonusDay" + this.currentDay + "isclaimed", 0) == 0)
			{
				this.isClaimed = false;
			}
			else
			{
				this.isClaimed = true;
			}
			long dateData = Convert.ToInt64(PlayerPrefs.GetString("DailyBonusTime", DateTime.Now.ToBinary().ToString()));
			this.currentDate = DateTime.Now;
			DateTime dateTime = DateTime.FromBinary(dateData);
			this.difference = this.currentDate.Subtract(dateTime);
			MonoBehaviour.print("oldDate: " + dateTime);
			MonoBehaviour.print("Difference: " + this.difference.Seconds);
			if (this.difference.Days >= 1)
			{
				if (!this.isClaimed)
				{
					this.bonusReady = true;
				}
			}
		}
	}

	public void markStarted()
	{
		if (PlayerPrefs.GetInt("DailyBonusStarted", 0) == 0)
		{
			PlayerPrefs.SetString("DailyBonusTime", DateTime.Now.ToBinary().ToString());
			PlayerPrefs.SetInt("DailyBonusStarted", 1);
		}
		Singleton<GameController>.Instance.customNotificationManager.setNotificationWithId(Constants.DAILY_BONUS_NOTIFICATION_ID, 86400, "Daily Bonus", "Daily Bonus is Ready", false);
	}

	public void UpdateNextDay()
	{
		this.currentDay = PlayerPrefs.GetInt("DailyBonusDay", 1);
		PlayerPrefs.SetInt("DailyBonusDay" + this.currentDay + "isclaimed", 1);
		this.currentDay++;
		PlayerPrefs.SetInt("DailyBonusDay", this.currentDay);
		PlayerPrefs.SetString("DailyBonusTime", DateTime.Now.ToBinary().ToString());
		this.bonusReady = false;
		this.isClaimed = false;
		this.markStarted();
	}

	public DailyBonusRewardItem getItemByDay(int day)
	{
		for (int i = 0; i < this.rewardItemsList.Count; i++)
		{
			if (this.rewardItemsList[i].id == day)
			{
				return this.rewardItemsList[i];
			}
		}
		return null;
	}

	private static bool isCreated;

	public int currentDay = 1;

	public bool isClaimed;

	private DateTime currentDate;

	private DateTime oldDate;

	private TimeSpan difference;

	public bool bonusReady;

	public List<DailyBonusRewardItem> rewardItemsList;
}
