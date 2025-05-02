// dnSpy decompiler from Assembly-CSharp.dll class: EnemyLevelMeta
using System;
using System.Collections.Generic;

[Serializable]
public class EnemyLevelMeta
{
	public void InitFillIndex()
	{
		this.positionFillIndex = new List<bool>();
		for (int i = 0; i < this.wavesMeta.Count; i++)
		{
			this.positionFillIndex.Add(false);
		}
	}

	public int enemiesCount;

	public List<EnemyMeta> initialMeta;

	public List<EnemyMeta> wavesMeta;

	public List<EnemyLevelMetaPerWave> perWaveMeta;

	public MoveableMeta moveableMeta;

	public List<int> waveGenerationAtCount;

	public List<int> enemiesCountInWave;

	public int generatedCount;

	public List<bool> positionFillIndex;

	public int currentWave;
}
