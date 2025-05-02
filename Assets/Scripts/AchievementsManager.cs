// dnSpy decompiler from Assembly-CSharp.dll class: AchievementsManager
using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
	private void Awake()
	{
		if (!AchievementsManager.isCreated)
		{
			Singleton<GameController>.Instance.achievementManager = this;
			AchievementsManager.isCreated = true;
			UnityEngine.Object.DontDestroyOnLoad(this);
			this.InitializePlayerPrefs();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Start()
	{
		base.Invoke("register20MintsTimeAchievement", 1200f);
		base.Invoke("register30MintsTimeAchievement", 1800f);
	}

	private void InitializePlayerPrefs()
	{
		foreach (AchievementModel achievementModel in this.achievementModels)
		{
			achievementModel.initializeModel();
		}
	}

	public void setModelFromJson(List<AchievementModel> allModelsFromJson)
	{
		this.achievementModels.Clear();
		this.achievementModels = allModelsFromJson;
	}

	public void updateAchievement(int mtype)
	{
		List<AchievementModel> allModelsWithType = this.getAllModelsWithType(mtype);
		for (int i = 0; i < allModelsWithType.Count; i++)
		{
			allModelsWithType[i].updateAchievementModel(mtype);
		}
		if (allModelsWithType.Count == 0)
		{
			MonoBehaviour.print("UK :: no achievement found with type " + mtype);
		}
	}

	public AchievementModel getModelWithType(int mType)
	{
		for (int i = 0; i < this.achievementModels.Count; i++)
		{
			if (this.achievementModels[i].type == mType)
			{
				return this.achievementModels[i];
			}
		}
		return null;
	}

	public AchievementModel getModelWithId(int mId)
	{
		for (int i = 0; i < this.achievementModels.Count; i++)
		{
			if (this.achievementModels[i].id == mId)
			{
				return this.achievementModels[i];
			}
		}
		return null;
	}

	public List<AchievementModel> getAllModelsWithType(int mType)
	{
		List<AchievementModel> list = new List<AchievementModel>();
		for (int i = 0; i < this.achievementModels.Count; i++)
		{
			if (this.achievementModels[i].type == mType)
			{
				list.Add(this.achievementModels[i]);
			}
		}
		return list;
	}

	private void register20MintsTimeAchievement()
	{
		this.updateAchievement(6);
	}

	private void register30MintsTimeAchievement()
	{
		this.updateAchievement(7);
	}

	public List<AchievementModel> achievementModels;

	private static bool isCreated;
}
