// dnSpy decompiler from Assembly-CSharp.dll class: DailyTaskGroup
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DailyTaskGroup
{
	public DailyTaskGroup(UKMap taskModelDetail)
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
		if (taskModelDetail.ContainsProperty("details"))
		{
			this.details = taskModelDetail.GetProperty("details").ToString();
		}
		if (taskModelDetail.ContainsProperty("totalTasks"))
		{
			this.totalTasks = taskModelDetail.GetIntValueForKey("totalTasks", 0);
		}
		if (taskModelDetail.ContainsProperty("rewardType"))
		{
			this.rewardType = taskModelDetail.GetIntValueForKey("rewardType", 1);
		}
	}

	public void initializeModel()
	{
		this.totalTasks = this.dailyTaskModelsList.Count;
		foreach (TaskModel taskModel in this.dailyTaskModelsList)
		{
			taskModel.initializeModel(this.id);
		}
		this.isCompleted = this.isTasksCompleted();
		this.isClaimed = this.isAllClaimed();
	}

	public bool isTasksCompleted()
	{
		bool result = false;
		this.currentTaskCount = 0;
		for (int i = 0; i < this.dailyTaskModelsList.Count; i++)
		{
			if (!this.dailyTaskModelsList[i].isTaskCompleted())
			{
				break;
			}
			this.currentTaskCount++;
		}
		if (this.currentTaskCount >= this.totalTasks)
		{
			result = true;
		}
		this.isCompleted = result;
		return result;
	}

	public void markComplete()
	{
		this.isCompleted = true;
		PlayerPrefs.SetInt("Day" + this.id.ToString() + "TasksisCompleted", 1);
	}

	public bool isAllClaimed()
	{
		bool result = false;
		int num = 0;
		for (int i = 0; i < this.dailyTaskModelsList.Count; i++)
		{
			if (!this.dailyTaskModelsList[i].getIsClaimed())
			{
				break;
			}
			num++;
		}
		if (num >= this.totalTasks)
		{
			result = true;
		}
		this.isClaimed = result;
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"claim count ",
			num,
			"  status : ",
			this.isClaimed
		}));
		return result;
	}

	public bool getIsClaimed()
	{
		if (PlayerPrefs.GetInt("Day" + this.id.ToString() + "TasksisClaimed", 0) == 0)
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
		PlayerPrefs.SetInt("Day" + this.id.ToString() + "TasksisClaimed", 1);
		this.isClaimed = true;
	}

	public void updateTask(int mType)
	{
		if (this.getModelWithType(mType) != null)
		{
			this.getModelWithType(mType).updateTaskModel(mType);
		}
		else
		{
			UnityEngine.Debug.Log("UK :: update model type not exist");
		}
		this.isTasksCompleted();
		this.isAllClaimed();
	}

	public TaskModel getModelWithType(int mType)
	{
		for (int i = 0; i < this.dailyTaskModelsList.Count; i++)
		{
			if (this.dailyTaskModelsList[i].type == mType)
			{
				return this.dailyTaskModelsList[i];
			}
		}
		return null;
	}

	public List<TaskModel> dailyTaskModelsList;

	public string name = string.Empty;

	public int id;

	public string details;

	public int totalTasks;

	public int currentTaskCount;

	public int rewardType;

	private bool isCompleted;

	private bool isClaimed;
}
