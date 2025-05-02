// dnSpy decompiler from Assembly-CSharp.dll class: UkGameDataLoaderInspector
using System;
using System.Collections.Generic;
using UnityEngine;

public class UkGameDataLoaderInspector : MonoBehaviour
{
	private void Awake()
	{
		if (!UkGameDataLoaderInspector.isCreated)
		{
			Singleton<GameController>.Instance.ukGameDataLoader = this;
			UkGameDataLoaderInspector.isCreated = true;
			UnityEngine.Object.DontDestroyOnLoad(this);
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}
	}

	public MissionsFromInspector GetMissionWithIndex(int mId)
	{
		if (mId <= this.mission.Count)
		{
			return this.mission[mId - 1];
		}
		return null;
	}

	public List<MissionsFromInspector> mission;

	private static bool isCreated;
}
