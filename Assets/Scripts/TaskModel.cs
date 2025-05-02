// dnSpy decompiler from Assembly-CSharp.dll class: TaskModel
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TaskModel
{
	public TaskModel(UKMap taskModelDetail)
	{
		if (taskModelDetail == null)
		{
			UnityEngine.Debug.Log("Daily Task :: Task Model :: constructor : Task Model map is null");
			return;
		}
		if (taskModelDetail.ContainsProperty("name"))
		{
			this.name = taskModelDetail.GetProperty("name").ToString();
		}
		if (taskModelDetail.ContainsProperty("type"))
		{
			this.type = taskModelDetail.GetIntValueForKey("type", 1);
		}
		if (taskModelDetail.ContainsProperty("details"))
		{
			this.details = taskModelDetail.GetProperty("details").ToString();
		}
		if (taskModelDetail.ContainsProperty("totalSteps"))
		{
			this.totalSteps = taskModelDetail.GetIntValueForKey("totalSteps", 0);
		}
		if (taskModelDetail.ContainsProperty("rewardType"))
		{
		}
	}

	public void initializeModel(int dayIdRef)
	{
		this.dayId = dayIdRef;
		this.currentStep = this.getProgress();
		this.isCompleted = this.isTaskCompleted();
		this.isClaimed = this.getIsClaimed();
	}

	public bool isTaskCompleted()
	{
		if (PlayerPrefs.GetInt(string.Concat(new string[]
		{
			"Day",
			this.dayId.ToString(),
			"Task",
			this.id.ToString(),
			"isCompleted"
		}), 0) == 0)
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
		PlayerPrefs.SetInt(string.Concat(new string[]
		{
			"Day",
			this.dayId.ToString(),
			"Task",
			this.id.ToString(),
			"isCompleted"
		}), 1);
	}

	public void setProgress(int p)
	{
		PlayerPrefs.SetInt(string.Concat(new string[]
		{
			"Day",
			this.dayId.ToString(),
			"Task",
			this.id.ToString(),
			"progress"
		}), p);
	}

	public int getProgress()
	{
		return PlayerPrefs.GetInt(string.Concat(new string[]
		{
			"Day",
			this.dayId.ToString(),
			"Task",
			this.id.ToString(),
			"progress"
		}), 0);
	}

	public bool getIsClaimed()
	{
		if (PlayerPrefs.GetInt(string.Concat(new string[]
		{
			"Day",
			this.dayId.ToString(),
			"Task",
			this.id.ToString(),
			"isClaimed"
		}), 0) == 0)
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
		PlayerPrefs.SetInt(string.Concat(new string[]
		{
			"Day",
			this.dayId.ToString(),
			"Task",
			this.id.ToString(),
			"isClaimed"
		}), 1);
		this.isClaimed = true;
		this.getIsClaimed();
		Singleton<GameController>.Instance.taskManager.getCurrentDayModel().isAllClaimed();
	}

	public void updateTaskModel(int reftype)
	{
		if (this.type == reftype)
		{
			if (!this.isCompleted)
			{
				this.currentStep++;
				this.setProgress(this.currentStep);
				UnityEngine.Debug.Log(" p : " + PlayerPrefs.GetInt(string.Concat(new string[]
				{
					"Day",
					this.dayId.ToString(),
					"Task",
					this.id.ToString(),
					"progress"
				}), 0));
				if (this.currentStep == this.totalSteps)
				{
					this.markComplete();
				}
			}
			else
			{
				UnityEngine.Debug.Log(string.Concat(new object[]
				{
					"Day",
					this.dayId.ToString(),
					"Task",
					this.id,
					" already Completed"
				}));
			}
		}
	}

	public string name = string.Empty;

	public int dayId;

	public int id;

	public int type;

	public string details;

	public int totalSteps;

	public int currentStep;

	public List<int> rewardType;

	private bool isCompleted;

	private bool isClaimed;
}
