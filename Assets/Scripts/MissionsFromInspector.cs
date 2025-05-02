// dnSpy decompiler from Assembly-CSharp.dll class: MissionsFromInspector
using System;
using System.Collections.Generic;

[Serializable]
public class MissionsFromInspector
{
	public MissionsFromInspector(UKMap mission)
	{
	}

	public LevelFromInspector GetLevelFromInspector(int lvlId)
	{
		if (lvlId <= this.levels.Count)
		{
			return this.levels[lvlId - 1];
		}
		return null;
	}

	public string missionNumber;

	public string missionBriefings;

	public int missionBonusPoints;

	public List<LevelFromInspector> levels;
}
