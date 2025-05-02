// dnSpy decompiler from Assembly-CSharp.dll class: PlayerLevelMeta
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerLevelMeta
{
	public Transform GetStartPositionOnIndex(int i = 0)
	{
		i = UnityEngine.Random.Range(0, this.startPositions.Count);
		if (i >= this.startPositions.Count)
		{
			return null;
		}
		return this.startPositions[i];
	}

	public Transform GetEndPositionOnIndex(int i = 0)
	{
		if (i >= this.endPositions.Count)
		{
			return null;
		}
		return this.endPositions[i];
	}

	public PathManager GetPathOnIndex(int i)
	{
		if (i >= this.path.Count)
		{
			return null;
		}
		return this.path[i];
	}

	public List<Transform> startPositions;

	public List<Transform> endPositions;

	public List<PathManager> path;
}
