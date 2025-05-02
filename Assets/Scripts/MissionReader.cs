// dnSpy decompiler from Assembly-CSharp.dll class: MissionReader
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class MissionReader : Singleton<MissionReader>
{
	public List<Mission> missionsList { get; set; }

	public Mission currentMission { get; set; }

	public Stage currentStage { get; set; }

	public void Awake()
	{
		this.missionsList = new List<Mission>();
		this.adsParser = new AdsParser();
	}

	public void ReadFromResources(string t)
	{
		this.allMissions = new UKMap(JsonSerializer.Decode(t) as Hashtable);
		UnityEngine.Debug.Log(":: Missions count " + this.allMissions.count);
		for (int i = 0; i < this.allMissions.count; i++)
		{
			UnityEngine.Debug.Log(":: Mission No:  " + i);
			UKMap ukmap = this.allMissions.GetProperty("Mission" + (i + 1)) as UKMap;
			if (ukmap != null)
			{
				Mission item = new Mission(ukmap);
				this.missionsList.Add(item);
			}
		}
		this.ReadAdsData();
		Stage stageWithMissionandStageId = this.getStageWithMissionandStageId(1, 1);
		if (stageWithMissionandStageId != null)
		{
			UnityEngine.Debug.Log("######### Bonus list " + stageWithMissionandStageId.bonusList[1]);
		}
	}

	public void ReadAdsData()
	{
		UKMap ukmap = this.allMissions.GetProperty("Ads") as UKMap;
		if (ukmap != null)
		{
			MonoBehaviour.print(" Ads MAp " + ukmap.GetIntValueForKey("Home", -1));
			this.adsParser.SetInitials(ukmap);
		}
	}

	public Mission getMissionWithIndex(int missionNumb)
	{
		if (missionNumb <= this.missionsList.Count)
		{
			return this.missionsList[missionNumb - 1];
		}
		return null;
	}

	public Mission getCurrentMission()
	{
		if (this.currentMission != null)
		{
			return this.currentMission;
		}
		this.currentMission = this.getMissionWithIndex(1);
		this.setCurrentMission(1);
		return this.currentMission;
	}

	public void setCurrentMission(int missionNumb)
	{
		UnityEngine.Debug.Log(" selected mission Numb " + missionNumb);
		if (missionNumb <= this.missionsList.Count)
		{
			this.currentMission = this.missionsList[missionNumb - 1];
		}
	}

	public void setCurrentStage(int stageNumb)
	{
		if (stageNumb <= this.currentMission.stages.Count)
		{
			this.currentStage = this.currentMission.stages[stageNumb - 1];
		}
	}

	public Stage getStageWithId(int id)
	{
		if (this.currentMission == null)
		{
			this.currentMission = this.getCurrentMission();
		}
		if (id <= this.currentMission.stages.Count)
		{
			return this.currentMission.stages[id - 1];
		}
		return this.getStageWithId(1);
	}

	public Stage getStageWithMissionandStageId(int mID, int sId)
	{
		Mission missionWithIndex = this.getMissionWithIndex(mID);
		if (missionWithIndex != null)
		{
			return missionWithIndex.stages[sId - 1];
		}
		return null;
	}

	public Stage getCurrentStage()
	{
		if (this.currentStage != null)
		{
			return this.currentStage;
		}
		this.setCurrentStage(1);
		return this.currentStage;
	}

	public UKMap allMissions;

	public AdsParser adsParser;
}
