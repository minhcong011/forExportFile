// dnSpy decompiler from Assembly-CSharp.dll class: Task
using System;
using UnityEngine;

public class Task
{
	public Task()
	{
		this.currentCount = 0;
		this.isCompleted = false;
	}

	public Task(UKMap tasks)
	{
		if (tasks == null)
		{
			UnityEngine.Debug.Log("Mission :: Stage :: tasks :: constructor : enemy map is null");
			return;
		}
		if (tasks.ContainsProperty("objectives"))
		{
			this.objectives = tasks.GetProperty("objectives").ToString();
		}
		if (tasks.ContainsProperty("name"))
		{
			this.name = tasks.GetProperty("name").ToString();
		}
		if (tasks.ContainsProperty("type"))
		{
			this.type = tasks.GetIntValueForKey("type", 1);
		}
		if (tasks.ContainsProperty("count"))
		{
			this.count = tasks.GetIntValueForKey("count", 1);
		}
		this.currentCount = 0;
		this.isCompleted = false;
	}

	public int type { get; set; }

	public string name { get; set; }

	public int count { get; set; }

	public int currentCount { get; set; }

	public bool isCompleted { get; set; }

	public string objectives { get; set; }

	public void copyConstructor(Task t)
	{
		if (t == null)
		{
			UnityEngine.Debug.Log("Mission :: Stage :: tasks :: constructor :task is null");
			return;
		}
		this.objectives = t.objectives;
		this.type = t.type;
		this.count = t.count;
		this.currentCount = 0;
	}
}
