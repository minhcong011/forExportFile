// dnSpy decompiler from Assembly-CSharp.dll class: EventManager
using System;
using System.Diagnostics;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action UIUpdate;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action achievementUIUpdate;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action achievementRewardClaimed;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action bulletFiredAlert;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action gameOver;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action gameStarted;

	//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public static event Action gameRetried;

	private void Awake()
	{
		if (!EventManager.isCreated)
		{
			EventManager.isCreated = true;
			UnityEngine.Object.DontDestroyOnLoad(this);
			Singleton<GameController>.Instance.eventManager = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Start()
	{
	}

	public void UpdateStoreUI()
	{
		if (EventManager.UIUpdate != null)
		{
			EventManager.UIUpdate();
		}
	}

	public void UpdateAchievementUI()
	{
		if (EventManager.achievementUIUpdate != null)
		{
			EventManager.achievementUIUpdate();
		}
	}

	public void rewardClaimed()
	{
		if (EventManager.achievementRewardClaimed != null)
		{
			EventManager.achievementRewardClaimed();
		}
	}

	public void GameOver()
	{
		if (EventManager.gameOver != null)
		{
			EventManager.gameOver();
		}
	}

	public void GameStarted()
	{
		if (EventManager.gameStarted != null)
		{
			EventManager.gameStarted();
		}
	}

	public void GameRetried()
	{
		if (EventManager.gameRetried != null)
		{
			EventManager.gameRetried();
		}
	}

	public void BulletFired()
	{
		if (EventManager.bulletFiredAlert != null)
		{
			EventManager.bulletFiredAlert();
		}
	}

	private static bool isCreated;
}
