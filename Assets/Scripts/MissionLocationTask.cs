// dnSpy decompiler from Assembly-CSharp.dll class: MissionLocationTask
using System;
using System.Collections.Generic;

[Serializable]
public class MissionLocationTask
{
	public MissionLocationTask(LocationWavesMeta locMeta, bool isFromMeta = true)
	{
		this.listOfLocations = new List<SingleLocationTask>();
		for (int i = 0; i < locMeta.locationsMeta.Count; i++)
		{
			SingleLocationTask item;
			if (isFromMeta)
			{
				item = new SingleLocationTask(locMeta.locationsMeta[i]);
			}
			else
			{
				item = new SingleLocationTask();
			}
			this.listOfLocations.Add(item);
		}
	}

	public MissionLocationTask()
	{
	}

	public void setCurrentLocation(int id)
	{
		this.currentLocation = id;
	}

	public void incrementCurrentLocation()
	{
		this.currentLocation++;
	}

	public bool getIsCleared(MissionLocationTask totalTasks)
	{
		bool flag = true;
		for (int i = 0; i < this.listOfLocations.Count; i++)
		{
			flag = this.listOfLocations[i].isCleared(totalTasks.listOfLocations[i]);
			if (!flag)
			{
				break;
			}
		}
		return flag;
	}

	public bool getCurrentLocIsCleared(MissionLocationTask totalTasks)
	{
		return this.listOfLocations[this.currentLocation - 1].isCleared(totalTasks.listOfLocations[this.currentLocation - 1]);
	}

	public SingleLocationTask getLocationWithIndex(int index)
	{
		if (index <= this.listOfLocations.Count)
		{
			return this.listOfLocations[index - 1];
		}
		return null;
	}

	public SingleLocationTask getCurrentLocation()
	{
		return this.listOfLocations[this.currentLocation - 1];
	}

	public List<SingleLocationTask> listOfLocations;

	public int currentLocation = 1;
}
