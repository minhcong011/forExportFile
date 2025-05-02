// dnSpy decompiler from Assembly-CSharp.dll class: Stage
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage
{
	public Stage(UKMap stage)
	{
		this.tasks = new List<Task>();
		this.bonusList = new List<int>();
		this.levelFailedStrings = new List<string>();
		if (stage == null)
		{
			UnityEngine.Debug.Log("stage :: constructor : Mission map is null");
			return;
		}
		if (stage.ContainsProperty("PlayerMeta"))
		{
			UKMap ukmap = stage.GetProperty("PlayerMeta") as UKMap;
			int intValueForKey = ukmap.GetIntValueForKey("index", 1);
			UnityEngine.Debug.Log(" a iss " + intValueForKey);
		}
		if (stage.ContainsProperty("stageBriefings"))
		{
			this.stageBriefings = stage.GetProperty("stageBriefings").ToString();
		}
		if (stage.ContainsProperty("totalEnemies"))
		{
			this.totalEnemies = stage.GetIntValueForKey("totalEnemies", 0);
		}
		if (stage.ContainsProperty("stage"))
		{
			this.stageMode = stage.GetIntValueForKey("stage", 0);
		}
		if (stage.ContainsProperty("fpsY"))
		{
			this.fpsY = (float)stage.GetDoubleValueForKey("fpsY", 3.0);
		}
		if (stage.ContainsProperty("rotationX"))
		{
			this.rotationX = (float)stage.GetDoubleValueForKey("rotationX", 0.0);
		}
		if (stage.ContainsProperty("stageTime"))
		{
			this.stageTime = (float)stage.GetDoubleValueForKey("stageTime", 60.0);
		}
		if (stage.ContainsProperty("saveMeTime"))
		{
			this.saveMeTime = (float)stage.GetDoubleValueForKey("saveMeTime", 5.0);
		}
		if (stage.ContainsProperty("timeForWaveGeneration"))
		{
			this.timeForWaveGeneration = (float)stage.GetDoubleValueForKey("timeForWaveGeneration", 60.0);
		}
		if (stage.ContainsProperty("timeDecreasingFactor"))
		{
			this.timeDecreasingFactor = (float)stage.GetDoubleValueForKey("timeDecreasingFactor", 10.0);
		}
		if (stage.ContainsProperty("playerPosX") && stage.ContainsProperty("playerPosY") && stage.ContainsProperty("playerPosZ"))
		{
			this.playerPos = new Vector3((float)stage.GetDoubleValueForKey("playerPosX", 0.0), (float)stage.GetDoubleValueForKey("playerPosY", 0.0), (float)stage.GetDoubleValueForKey("playerPosZ", 0.0));
		}
		if (stage.ContainsProperty("bonus"))
		{
			this.bonus = stage.GetIntValueForKey("bonus", 100);
		}
		if (stage.ContainsProperty("bonusCoins"))
		{
			this.bonusCoins = stage.GetIntValueForKey("bonusCoins", 1);
		}
		if (stage.ContainsProperty("buyPrice"))
		{
			this.buyPrice = stage.GetIntValueForKey("buyPrice", 10);
		}
		if (stage.ContainsProperty("saveMeCost"))
		{
			this.saveMeCost = stage.GetIntValueForKey("saveMeCost", 2);
		}
		if (stage.ContainsProperty("LevelFailedStrings"))
		{
			IList list = stage.GetProperty("LevelFailedStrings") as IList;
			UnityEngine.Debug.Log("uk ::  levelFailedString " + list.Count);
			IEnumerator enumerator = list.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					UKMap ukmap2 = (UKMap)obj;
					if (ukmap2.ContainsProperty("failedText"))
					{
						this.levelFailedStrings.Add(ukmap2.GetProperty("failedText").ToString());
					}
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
		if (stage.ContainsProperty("Bonus"))
		{
			UKMap ukmap3 = stage.GetProperty("Bonus") as UKMap;
			if (ukmap3.ContainsProperty("gold"))
			{
				this.bonusList.Add(ukmap3.GetIntValueForKey("gold", 0));
			}
			if (ukmap3.ContainsProperty("cash"))
			{
				this.bonusList.Add(ukmap3.GetIntValueForKey("cash", 0));
			}
			if (ukmap3.ContainsProperty("xp"))
			{
				this.bonusList.Add(ukmap3.GetIntValueForKey("xp", 0));
			}
			UnityEngine.Debug.Log("uk :: bonus exist");
		}
		if (stage.ContainsProperty("Tasks"))
		{
			IList list2 = stage.GetProperty("Tasks") as IList;
			IEnumerator enumerator2 = list2.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					object obj2 = enumerator2.Current;
					UKMap ukmap4 = (UKMap)obj2;
					Task item = new Task(ukmap4);
					this.tasks.Add(item);
				}
			}
			finally
			{
				IDisposable disposable2;
				if ((disposable2 = (enumerator2 as IDisposable)) != null)
				{
					disposable2.Dispose();
				}
			}
		}
		if (stage.ContainsProperty("levelOrganiserMeta"))
		{
			this.levelOrganiserMeta = new LevelOrganiserMeta(stage.GetProperty("levelOrganiserMeta") as UKMap);
		}
	}

	public string stageBriefings { get; set; }

	public int stageMode { get; set; }

	public Vector3 playerPos { get; set; }

	public float fpsY { get; set; }

	public float rotationX { get; set; }

	public float stageTime { get; set; }

	public float saveMeTime { get; set; }

	public float timeForWaveGeneration { get; set; }

	public float timeDecreasingFactor { get; set; }

	public int totalEnemies { get; set; }

	public int bonus { get; set; }

	public int bonusCoins { get; set; }

	public int buyPrice { get; set; }

	public int saveMeCost { get; set; }

	public List<int> bonusList;

	public List<Task> tasks;

	public List<string> levelFailedStrings;

	public LevelOrganiserMeta levelOrganiserMeta;
}
