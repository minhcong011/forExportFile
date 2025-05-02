// dnSpy decompiler from Assembly-CSharp.dll class: LevelOrganiserMeta
using System;
using UnityEngine;

public class LevelOrganiserMeta
{
	public LevelOrganiserMeta(UKMap levelDetails)
	{
		if (levelDetails == null)
		{
			UnityEngine.Debug.Log("Mission :: EnemiesLevelData :: constructor : EnemiesLevelData map is null");
			return;
		}
		if (levelDetails.ContainsProperty("shouldReadMeta"))
		{
			this.shouldReadMeta = levelDetails.GetBoolValueForKey("shouldReadMeta", false);
		}
		if (levelDetails.ContainsProperty("totalEnemies"))
		{
			this.totalEnemies = levelDetails.GetIntValueForKey("totalEnemies", 0);
		}
		if (levelDetails.ContainsProperty("soldierEnemies"))
		{
			this.soldiersMeta = new LevelEnemiesMeta(levelDetails.GetProperty("soldierEnemies") as UKMap);
		}
		if (levelDetails.ContainsProperty("tankEnemies"))
		{
			this.tankEnemies = new LevelEnemiesMeta(levelDetails.GetProperty("tankEnemies") as UKMap);
		}
		if (levelDetails.ContainsProperty("heliEnemies"))
		{
			this.heliEnemies = new LevelEnemiesMeta(levelDetails.GetProperty("heliEnemies") as UKMap);
		}
	}

	public void copyLevelOrganiserMeta(LevelOrganiserMeta meta)
	{
	}

	public int totalEnemies;

	public LevelEnemiesMeta soldiersMeta;

	public LevelEnemiesMeta tankEnemies;

	public LevelEnemiesMeta heliEnemies;

	public LevelEnemiesMeta humveeEnemies;

	public bool shouldReadMeta;
}
