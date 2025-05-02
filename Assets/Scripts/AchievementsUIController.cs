// dnSpy decompiler from Assembly-CSharp.dll class: AchievementsUIController
using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsUIController : MonoBehaviour
{
	private void Awake()
	{
		this.achievementModels = Singleton<GameController>.Instance.achievementManager.achievementModels;
	}

	private void Start()
	{
		this.initializeModels();
		this.isInitialised = true;
		this.UpdateUIDetails();
	}

	private void OnEnable()
	{
		if (!this.isInitialised)
		{
			return;
		}
		this.UpdateUIDetails();
	}

	private void initializeModels()
	{
		this.achievementModelsUIList.Clear();
		this.scroller.VerticalScrollar(this.achievementModels.Count, this.achievementPrefab);
		AchievementModelUI[] componentsInChildren = this.scroller.gameObject.GetComponentsInChildren<AchievementModelUI>();
		UnityEngine.Debug.Log(" uk : Achievements UI Array initiated " + componentsInChildren.Length);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			this.achievementModelsUIList.Add(componentsInChildren[i]);
		}
		int num = 0;
		while (num < this.achievementModelsUIList.Count && num < this.achievementModels.Count)
		{
			this.achievementModelsUIList[num].setModel(this.achievementModels[num]);
			num++;
		}
	}

	public void UpdateUIDetails()
	{
		int num = 0;
		while (num < this.achievementModelsUIList.Count && num < this.achievementModels.Count)
		{
			this.achievementModelsUIList[num].updateUI();
			num++;
		}
	}

	public List<AchievementModel> achievementModels;

	public List<AchievementModelUI> achievementModelsUIList;

	private bool isInitialised;

	public GameObject achievementPrefab;

	public ScrollableUi scroller;
}
