// dnSpy decompiler from Assembly-CSharp.dll class: DailyTaskManager
using System;
using System.Collections.Generic;
using UnityEngine;

public class DailyTaskManager : MonoBehaviour
{
	private void Awake()
	{
		if (!DailyTaskManager.isCreated)
		{
			Singleton<GameController>.Instance.taskManager = this;
			DailyTaskManager.isCreated = true;
			UnityEngine.Object.DontDestroyOnLoad(this);
			this.checkForDay();
			this.InitializePlayerPrefs();
			this.registerTimeTasks();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Start()
	{
	}

	private void registerTimeTasks()
	{
		this.startedTime = Convert.ToInt64(DateTime.Now.ToBinary().ToString());
		base.InvokeRepeating("checkForTime", 10f, 20f);
	}

	private void checkForTime()
	{
		DateTime now = DateTime.Now;
		DateTime dateTime = DateTime.FromBinary(this.startedTime);
		this.difference = now.Subtract(dateTime);
		MonoBehaviour.print("oldDate: " + dateTime);
		MonoBehaviour.print("Difference: " + this.difference.TotalSeconds);
		if (this.difference.TotalMinutes >= 10.0)
		{
			this.updateTask(1);
		}
		if (this.difference.TotalMinutes >= 20.0)
		{
			this.updateTask(8);
		}
	}

	private void online10Mints()
	{
		this.updateTask(1);
	}

	private void online20Mints()
	{
		this.updateTask(8);
	}

	public bool checkForDay()
	{
		bool result = false;
		int @int = PlayerPrefs.GetInt("DailyTaskDay", 1);
		this.currentDayId = @int;
		if (!PlayerPrefs.HasKey("DailyTaskStartedTime"))
		{
			PlayerPrefs.SetString("DailyTaskStartedTime", DateTime.Now.ToBinary().ToString());
			Singleton<GameController>.Instance.customNotificationManager.setNotificationWithId(Constants.DAILYTASK_NOTIFICATION_ID, 86400, "Daily Task", "Complete the tasks to get free gifts", false);
			UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
			if (adsManager != null)
			{
			}
		}
		long dateData = Convert.ToInt64(PlayerPrefs.GetString("DailyTaskStartedTime", DateTime.Now.ToBinary().ToString()));
		this.currentDate = DateTime.Now;
		DateTime dateTime = DateTime.FromBinary(dateData);
		this.difference = this.currentDate.Subtract(dateTime);
		MonoBehaviour.print("oldDate: " + dateTime);
		MonoBehaviour.print("Difference: " + this.difference.Seconds);
		if (this.difference.Days >= 1 && this.getCurrentDayModel().isAllClaimed() && this.currentDayId < this.dailyTaskGroupList.Count)
		{
			this.currentDayId++;
			PlayerPrefs.SetString("DailyTaskStartedTime", DateTime.Now.ToBinary().ToString());
			PlayerPrefs.SetInt("DailyTaskDay", this.currentDayId);
			result = true;
			Singleton<GameController>.Instance.customNotificationManager.setNotificationWithId(Constants.DAILYTASK_NOTIFICATION_ID, 86400, "Daily Task", "Complete the tasks to get free gifts", false);
			UKAdsManager adsManager2 = Singleton<GameController>.Instance.adsManager;
			if (adsManager2 != null)
			{
			}
		}
		return result;
	}

	private void InitializePlayerPrefs()
	{
		foreach (DailyTaskGroup dailyTaskGroup in this.dailyTaskGroupList)
		{
			dailyTaskGroup.initializeModel();
		}
	}

	public void setModelFromJson(List<DailyTaskGroup> allModelsFromJson)
	{
		this.dailyTaskGroupList.Clear();
		this.dailyTaskGroupList = allModelsFromJson;
	}

	public void updateTask(int mtype)
	{
		DailyTaskGroup modelWithId = this.getModelWithId(this.currentDayId);
		if (modelWithId != null)
		{
			modelWithId.updateTask(mtype);
		}
		if (modelWithId.isAllClaimed() && this.currentDayId < this.dailyTaskGroupList.Count)
		{
			long dateData = Convert.ToInt64(PlayerPrefs.GetString("DailyTaskStartedTime", DateTime.Now.ToBinary().ToString()));
			this.currentDate = DateTime.Now;
			DateTime dateTime = DateTime.FromBinary(dateData);
			this.difference = this.currentDate.Subtract(dateTime);
			MonoBehaviour.print("oldDate: " + dateTime);
			MonoBehaviour.print("Difference: " + this.difference.Seconds);
			if (this.difference.Days >= 1)
			{
				this.currentDayId++;
				PlayerPrefs.SetString("DailyTaskStartedTime", DateTime.Now.ToBinary().ToString());
				PlayerPrefs.SetInt("DailyTaskDay", this.currentDayId);
				Singleton<GameController>.Instance.customNotificationManager.setNotificationWithId(Constants.DAILYTASK_NOTIFICATION_ID, 86400, "Daily Task", "Complete the tasks to get free gifts", false);
				UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
				if (adsManager != null)
				{
				}
			}
		}
	}

	public DailyTaskGroup getModelWithId(int mId)
	{
		for (int i = 0; i < this.dailyTaskGroupList.Count; i++)
		{
			if (this.dailyTaskGroupList[i].id == mId)
			{
				return this.dailyTaskGroupList[i];
			}
		}
		return null;
	}

	public DailyTaskGroup getCurrentDayModel()
	{
		for (int i = 0; i < this.dailyTaskGroupList.Count; i++)
		{
			if (this.dailyTaskGroupList[i].id == this.currentDayId)
			{
				return this.dailyTaskGroupList[i];
			}
		}
		return null;
	}

	public void clearCurrentDayStats()
	{
	}

	public List<DailyTaskGroup> dailyTaskGroupList;

	public int currentDayId = 1;

	private static bool isCreated;

	private DateTime currentDate;

	private DateTime oldDate;

	private TimeSpan difference;

	private long startedTime;
}
