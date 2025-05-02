// dnSpy decompiler from Assembly-CSharp.dll class: AchievementModel
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AchievementModel
{
	public AchievementModel(UKMap achievementModelDetail)
	{
		if (achievementModelDetail == null)
		{
			UnityEngine.Debug.Log("Achievements :: achievementModelDetail :: constructor : achievementModelDetail map is null");
			return;
		}
		if (achievementModelDetail.ContainsProperty("name"))
		{
			this.name = achievementModelDetail.GetProperty("name").ToString();
		}
		if (achievementModelDetail.ContainsProperty("type"))
		{
			this.type = achievementModelDetail.GetIntValueForKey("type", 1);
		}
		if (achievementModelDetail.ContainsProperty("details"))
		{
			this.details = achievementModelDetail.GetProperty("details").ToString();
		}
		if (achievementModelDetail.ContainsProperty("totalSteps"))
		{
			this.totalSteps = achievementModelDetail.GetIntValueForKey("totalSteps", 0);
		}
		if (achievementModelDetail.ContainsProperty("rewardType"))
		{
		}
	}

	public void initializeModel()
	{
		this.currentStep = this.getProgress();
		this.isCompleted = this.isAchievementCompleted();
		this.isClaimed = this.getIsClaimed();
	}

	public bool isAchievementCompleted()
	{
		if (PlayerPrefs.GetInt("AchievementModel" + this.id.ToString() + "isCompleted", 0) == 0)
		{
			this.isCompleted = false;
		}
		else
		{
			this.isCompleted = true;
		}
		return this.isCompleted;
	}

	public void markComplete()
	{
		this.isCompleted = true;
		PlayerPrefs.SetInt("AchievementModel" + this.id.ToString() + "isCompleted", 1);
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
		}
	}

	public void setProgress(int p)
	{
		PlayerPrefs.SetInt("AchievementModel" + this.id.ToString() + "progress", p);
	}

	public int getProgress()
	{
		return PlayerPrefs.GetInt("AchievementModel" + this.id.ToString() + "progress", 0);
	}

	public bool getIsClaimed()
	{
		if (PlayerPrefs.GetInt("AchievementModel" + this.id.ToString() + "isClaimed", 0) == 0)
		{
			this.isClaimed = false;
		}
		else
		{
			this.isClaimed = true;
		}
		return this.isClaimed;
	}

	public void setClaimed()
	{
		PlayerPrefs.SetInt("AchievementModel" + this.id.ToString() + "isClaimed", 1);
		this.isClaimed = true;
	}

	public void updateAchievementModel(int reftype)
	{
		if (this.type == reftype)
		{
			if (!this.isCompleted)
			{
				this.currentStep++;
				this.setProgress(this.currentStep);
				if (this.currentStep == this.totalSteps)
				{
					this.markComplete();
				}
			}
			else
			{
				UnityEngine.Debug.Log(" achievement id : " + this.id + " already Completed");
			}
		}
	}

	public string name = string.Empty;

	public int id;

	public int type;

	public string details;

	public int totalSteps;

	public int currentStep;

	public List<int> rewardType;

	private bool isCompleted;

	private bool isClaimed;
}
