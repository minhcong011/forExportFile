// dnSpy decompiler from Assembly-CSharp.dll class: DailyLoginManager
using System;
using UnityEngine;

public class DailyLoginManager : MonoBehaviour
{
	private void Awake()
	{
		if (!DailyLoginManager.isCreated)
		{
			DailyLoginManager.isCreated = true;
			Singleton<GameController>.Instance.dailyLoginManager = this;
			UnityEngine.Object.DontDestroyOnLoad(this);
			this.initialize();
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
		if (PlayerPrefs.GetInt("DailyLoginStarted", 0) == 0)
		{
			PlayerPrefs.SetString("DailyLoginTime", DateTime.Now.ToBinary().ToString());
			PlayerPrefs.SetInt("DailyLoginStarted", 1);
			MonoBehaviour.print("started" + DateTime.Now.ToBinary().ToString());
			Singleton<GameController>.Instance.customNotificationManager.setNotificationWithId(Constants.DAILY_SPIN_NOTIFICATION_ID, 86400, "Free Spin Bonus", "Exciting bonus is ready for you", false);
			this.isSpinAvailable = true;
		}
		else
		{
			long dateData = Convert.ToInt64(PlayerPrefs.GetString("DailyLoginTime", DateTime.Now.ToBinary().ToString()));
			this.currentDate = DateTime.Now;
			DateTime dateTime = DateTime.FromBinary(dateData);
			this.difference = this.currentDate.Subtract(dateTime);
			MonoBehaviour.print("oldDate: " + dateTime);
			MonoBehaviour.print("Difference: " + this.difference.Seconds);
			if (this.difference.Days >= 1)
			{
				if (this.isClaimed)
				{
					this.isSpinAvailable = false;
					this.UpdateNextDay();
				}
				else
				{
					this.isSpinAvailable = true;
				}
			}
			else if (!this.isClaimed)
			{
				this.isSpinAvailable = true;
			}
		}
	}

	private void initialize()
	{
		if (!PlayerPrefs.HasKey("DailyLoginDay"))
		{
			PlayerPrefs.SetInt("DailyLoginDay", 1);
		}
		this.currentDay = PlayerPrefs.GetInt("DailyLoginDay", 1);
		if (PlayerPrefs.GetInt("DailyLoginDay" + this.currentDay.ToString() + "isClaimed", 0) == 0)
		{
			this.isClaimed = false;
		}
		else
		{
			this.isClaimed = true;
		}
	}

	public void UpdateNextDay()
	{
		this.consumeTodaysBonus();
		this.currentDay = PlayerPrefs.GetInt("DailyLoginDay", 1);
		this.currentDay++;
		PlayerPrefs.SetInt("DailyLoginDay", this.currentDay);
		PlayerPrefs.SetString("DailyLoginTime", DateTime.Now.ToBinary().ToString());
		Singleton<GameController>.Instance.customNotificationManager.setNotificationWithId(Constants.DAILY_SPIN_NOTIFICATION_ID, 86400, "Free Spin Bonus", "Exciting bonus is ready for you", false);
		if (this.currentDay == 4)
		{
		}
	}

	public void consumeTodaysBonus()
	{
		PlayerPrefs.SetInt("DailyLoginDay" + this.currentDay.ToString() + "isClaimed", 1);
		this.isClaimed = true;
		this.isSpinAvailable = false;
	}

	private static bool isCreated;

	public int currentDay = 1;

	public bool isClaimed;

	public bool isSpinAvailable;

	private DateTime currentDate;

	private DateTime oldDate;

	private TimeSpan difference;
}
