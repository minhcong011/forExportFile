// dnSpy decompiler from Assembly-CSharp.dll class: Mission
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission
{
	public Mission(UKMap mission)
	{
		this.stages = new List<Stage>();
		if (mission == null)
		{
			UnityEngine.Debug.Log("Mission :: constructor : Mission map is null");
			return;
		}
		if (mission.ContainsProperty("MissionBriefings"))
		{
			this.missionBriefings = mission.GetProperty("MissionBriefings").ToString();
		}
		if (mission.ContainsProperty("MissionBonusPoints"))
		{
			this.missionBonusPoints = mission.GetIntValueForKey("MissionBonusPoints", 0);
		}
		if (mission.ContainsProperty("isTimeDependent"))
		{
			this.isTimeDependent = mission.GetIntValueForKey("isTimeDependent", 0);
		}
		if (mission.ContainsProperty("totalEnemies"))
		{
			this.totalEnemies = mission.GetIntValueForKey("totalEnemies", 0);
		}
		if (mission.ContainsProperty("missionMode"))
		{
			this.missionMode = mission.GetIntValueForKey("missionMode", 0);
		}
		if (mission.ContainsProperty("fpsY"))
		{
			this.fpsY = (float)mission.GetDoubleValueForKey("fpsY", 3.0);
		}
		if (mission.ContainsProperty("timeForWaveGeneration"))
		{
			this.timeForWaveGeneration = (float)mission.GetDoubleValueForKey("timeForWaveGeneration", 60.0);
		}
		if (mission.ContainsProperty("timeDecreasingFactor"))
		{
			this.timeDecreasingFactor = (float)mission.GetDoubleValueForKey("timeDecreasingFactor", 10.0);
		}
		if (mission.ContainsProperty("playerPosX") && mission.ContainsProperty("playerPosY") && mission.ContainsProperty("playerPosZ"))
		{
			this.playerPos = new Vector3((float)mission.GetDoubleValueForKey("playerPosX", 0.0), (float)mission.GetDoubleValueForKey("playerPosY", 0.0), (float)mission.GetDoubleValueForKey("playerPosZ", 0.0));
		}
		if (mission.ContainsProperty("Stages"))
		{
			IList list = mission.GetProperty("Stages") as IList;
			IEnumerator enumerator = list.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					UKMap stage = (UKMap)obj;
					Stage item = new Stage(stage);
					this.stages.Add(item);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
	}

	public string missionBriefings { get; set; }

	public int missionBonusPoints { get; set; }

	public int isTimeDependent { get; set; }

	public int totalEnemies { get; set; }

	public Vector3 playerPos { get; set; }

	public float fpsY { get; set; }

	public int missionMode { get; set; }

	public float timeForWaveGeneration { get; set; }

	public float timeDecreasingFactor { get; set; }

	public List<Stage> stages;
}
