// dnSpy decompiler from Assembly-CSharp.dll class: UserLevelStatusManager
using System;
using UnityEngine;

public class UserLevelStatusManager : MonoBehaviour
{
	private void Awake()
	{
		if (!UserLevelStatusManager.isCreated)
		{
			UserLevelStatusManager.isCreated = true;
			Singleton<GameController>.Instance.userLevelStatusManager = this;
			UnityEngine.Object.DontDestroyOnLoad(this);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		base.Invoke("InitializePrefs", 5f);
	}

	public void ShowVideoWithType(int type)
	{
		this.rewardedVideoCB(type);
	}

	private void InitializePrefs()
	{
		UnityEngine.Debug.Log("aaho");
	}

	public int GetCurrentLevelPlayedStatus()
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"LevelPlayedStatus_",
			Singleton<GameController>.Instance.SelectedMission,
			"_",
			Singleton<GameController>.Instance.SelectedLevel
		}), 0);
	}

	public void SetCurrentLevelPlayedStatus()
	{
		int currentLevelPlayedStatus = this.GetCurrentLevelPlayedStatus();
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"LevelPlayedStatus_",
			Singleton<GameController>.Instance.SelectedMission,
			"_",
			Singleton<GameController>.Instance.SelectedLevel
		}), currentLevelPlayedStatus + 1);
	}

	public int GetCurrentLevelWinStatus()
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"LevelWinStatus_",
			Singleton<GameController>.Instance.SelectedMission,
			"_",
			Singleton<GameController>.Instance.SelectedLevel
		}), 0);
	}

	public void SetCurrentLevelWinStatus()
	{
		int currentLevelWinStatus = this.GetCurrentLevelWinStatus();
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"LevelWinStatus_",
			Singleton<GameController>.Instance.SelectedMission,
			"_",
			Singleton<GameController>.Instance.SelectedLevel
		}), currentLevelWinStatus + 1);
	}

	public int GetCurrentLevelLoseStatus()
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"LevelLoseStatus_",
			Singleton<GameController>.Instance.SelectedMission,
			"_",
			Singleton<GameController>.Instance.SelectedLevel
		}), 0);
	}

	public void SetCurrentLoseStatus()
	{
		int currentLevelLoseStatus = this.GetCurrentLevelLoseStatus();
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"LevelLoseStatus_",
			Singleton<GameController>.Instance.SelectedMission,
			"_",
			Singleton<GameController>.Instance.SelectedLevel
		}), currentLevelLoseStatus + 1);
	}

	public void IncrementContinueLoseStatus()
	{
		int continueLoseStatus = this.GetContinueLoseStatus();
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"LevelContinueLoseStatus_",
			Singleton<GameController>.Instance.SelectedMission,
			"_",
			Singleton<GameController>.Instance.SelectedLevel
		}), continueLoseStatus + 1);
	}

	public int GetContinueLoseStatus()
	{
		return PlayerPrefs.GetInt(string.Concat(new object[]
		{
			"LevelContinueLoseStatus_",
			Singleton<GameController>.Instance.SelectedMission,
			"_",
			Singleton<GameController>.Instance.SelectedLevel
		}), 0);
	}

	public void ResetContinueLoseStatus()
	{
		PlayerPrefs.SetInt(string.Concat(new object[]
		{
			"LevelContinueLoseStatus_",
			Singleton<GameController>.Instance.SelectedMission,
			"_",
			Singleton<GameController>.Instance.SelectedLevel
		}), 0);
	}

	private static bool isCreated;

	public UserLevelStatusManager.videoCallBack rewardedVideoCB;

	public delegate void videoCallBack(int type);
}
