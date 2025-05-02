// dnSpy decompiler from Assembly-CSharp.dll class: SingleLocationWaveMeta
using System;

[Serializable]
public class SingleLocationWaveMeta
{
	public int GetTotalEnemiesCountInThisLocation()
	{
		this.totalCount = 0;
		this.totalCount += this.soldierEnemiesMeta.enemiesCount + this.tankParams.enemiesCount + this.HeliParams.enemiesCount + this.HumveeParams.enemiesCount;
		return this.totalCount;
	}

	public int GetGeneratedEnemiesCountInThisLocation()
	{
		this.generatedCount = 0;
		this.generatedCount += this.soldierEnemiesMeta.generatedCount + this.tankParams.generatedCount + this.HeliParams.generatedCount + this.HumveeParams.generatedCount;
		return this.generatedCount;
	}

	public void InitFillIndexes()
	{
		this.soldierEnemiesMeta.InitFillIndex();
		this.tankParams.InitFillIndex();
		this.HeliParams.InitFillIndex();
		this.HumveeParams.InitFillIndex();
	}

	public void MarkLocationAsCleared()
	{
		this.isLocationCleared = true;
	}

	public string locName = "Loc";

	public int totalCount;

	public EnemyLevelMeta soldierEnemiesMeta;

	public EnemyLevelMeta tankParams;

	public EnemyLevelMeta HeliParams;

	public EnemyLevelMeta HumveeParams;

	public PlayerLevelMeta playerLevelMeta;

	public int locationClearedAction;

	public int generatedCount;

	public bool isLocationCleared;

	public int objectiveTaskCount;
}
