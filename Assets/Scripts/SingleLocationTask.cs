// dnSpy decompiler from Assembly-CSharp.dll class: SingleLocationTask
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SingleLocationTask
{
	public SingleLocationTask(SingleLocationWaveMeta org)
	{
		this.listOfTask = new List<int>();
		this.listOfTask.Add(org.soldierEnemiesMeta.enemiesCount);
		this.listOfTask.Add(org.tankParams.enemiesCount);
		this.listOfTask.Add(org.HeliParams.enemiesCount);
		this.listOfTask.Add(org.HumveeParams.enemiesCount);
		UnityEngine.Debug.Log("Count " + this.listOfTask.Count);
		this.specialObjectiveTask = org.objectiveTaskCount;
	}

	public SingleLocationTask()
	{
		this.listOfTask = new List<int>();
		this.listOfTask.Add(0);
		this.listOfTask.Add(0);
		this.listOfTask.Add(0);
		this.listOfTask.Add(0);
		this.specialObjectiveTask = 0;
	}

	public bool isCleared(SingleLocationTask singleLocTask)
	{
		bool result = true;
		for (int i = 0; i < singleLocTask.listOfTask.Count; i++)
		{
			if (this.listOfTask[i] < singleLocTask.listOfTask[i])
			{
				result = false;
				break;
			}
		}
		if (this.specialObjectiveTask < singleLocTask.specialObjectiveTask)
		{
			result = false;
		}
		return result;
	}

	public List<int> listOfTask;

	public int specialObjectiveTask;
}
