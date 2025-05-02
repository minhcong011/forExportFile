// dnSpy decompiler from Assembly-CSharp.dll class: DailyTaskUIController
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyTaskUIController : MonoBehaviour
{
	private void Awake()
	{
		this.taskModels = Singleton<GameController>.Instance.taskManager.getCurrentDayModel().dailyTaskModelsList;
		this.dayCount.text = "DAY " + Singleton<GameController>.Instance.taskManager.currentDayId;
	}

	private void Start()
	{
		this.initializeModels();
		this.isInitialised = true;
		this.UpdateUIDetails();
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
			adsManager.LogScreen("Daily Task Menu");
		}
	}

	private void OnEnable()
	{
		if (!this.isInitialised)
		{
			return;
		}
		bool flag = Singleton<GameController>.Instance.taskManager.checkForDay();
		if (flag)
		{
			this.taskModels = Singleton<GameController>.Instance.taskManager.getCurrentDayModel().dailyTaskModelsList;
			this.dayCount.text = "DAY " + Singleton<GameController>.Instance.taskManager.currentDayId;
			this.initializeModels();
		}
		this.UpdateUIDetails();
	}

	private void initializeModels()
	{
		this.taskModelsUIList.Clear();
		this.scroller.VerticalScrollar(this.taskModels.Count, this.taskItemPrefab);
		TaskModelUI[] componentsInChildren = this.scroller.gameObject.GetComponentsInChildren<TaskModelUI>();
		UnityEngine.Debug.Log(" uk : TaskModel UI Array initiated " + componentsInChildren.Length);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			this.taskModelsUIList.Add(componentsInChildren[i]);
		}
		int num = 0;
		while (num < this.taskModelsUIList.Count && num < this.taskModels.Count)
		{
			this.taskModelsUIList[num].setModel(this.taskModels[num]);
			num++;
		}
	}

	public void UpdateUIDetails()
	{
		int num = 0;
		while (num < this.taskModelsUIList.Count && num < this.taskModels.Count)
		{
			this.taskModelsUIList[num].updateUI();
			num++;
		}
	}

	public List<TaskModel> taskModels;

	public List<TaskModelUI> taskModelsUIList;

	public Text dayCount;

	private bool isInitialised;

	public GameObject taskItemPrefab;

	public ScrollableUi scroller;
}
