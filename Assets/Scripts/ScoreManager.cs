// dnSpy decompiler from Assembly-CSharp.dll class: ScoreManager
using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	private void Awake()
	{
		if (!ScoreManager.isCreated)
		{
			ScoreManager.isCreated = true;
			UnityEngine.Object.DontDestroyOnLoad(this);
			Singleton<GameController>.Instance.scoreManager = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void SetScoresAccordingToLevel()
	{
		this.totalScore = 0;
		this.enemyPtsEarned = 0;
		this.tankPtsEarned = 0;
		this.heliPtsEarned = 0;
		this.totalCoinsEarned = 0;
		this.destructionPtsEarned = 0;
		int selectedLevel = Singleton<GameController>.Instance.SelectedLevel;
		int selectedMission = Singleton<GameController>.Instance.SelectedMission;
		switch (selectedLevel)
		{
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
			this.enemyPoints = 30;
			this.tankPoints = 60;
			this.heliPoints = 60;
			this.destructionPoints = 30;
			break;
		case 6:
		case 7:
		case 8:
		case 9:
			this.enemyPoints = 40;
			this.tankPoints = 80;
			this.heliPoints = 100;
			this.destructionPoints = 40;
			break;
		case 10:
		case 11:
		case 12:
			this.enemyPoints = 60;
			this.tankPoints = 90;
			this.heliPoints = 110;
			this.destructionPoints = 40;
			break;
		}
		this.enemyPoints += (selectedMission - 1) * 14;
	}

	public void SetScores(int enemyType)
	{
		this.totalScore = 0;
		if (enemyType != 0)
		{
			if (enemyType != 1)
			{
				if (enemyType == 2)
				{
					this.heliPtsEarned += this.heliPoints;
				}
			}
			else
			{
				this.tankPtsEarned += this.tankPoints;
			}
		}
		else
		{
			this.enemyPtsEarned += this.enemyPoints;
		}
	}

	public void calculateScores()
	{
		Stage stageWithMissionandStageId = Singleton<MissionReader>.Instance.getStageWithMissionandStageId(Singleton<GameController>.Instance.SelectedMission, Singleton<GameController>.Instance.SelectedLevel);
		this.totalScore = 0;
		this.totalCoinsEarned = 0;
		this.totalScore = this.enemyPtsEarned + this.tankPtsEarned + this.heliPtsEarned + this.destructionPtsEarned;
		int num = UnityEngine.Random.Range(30, 45);
		this.totalCoinsEarned = this.totalScore * num / 100;
		if (stageWithMissionandStageId != null)
		{
			this.totalCoinsEarned = stageWithMissionandStageId.bonusList[0];
			this.totalCashEarned = stageWithMissionandStageId.bonusList[1];
			this.totalXP = stageWithMissionandStageId.bonusList[2];
		}
		if (Singleton<GameController>.Instance.ukGameDataLoader != null)
		{
			LevelFromInspector levelFromInspector = Singleton<GameController>.Instance.ukGameDataLoader.GetMissionWithIndex(Singleton<GameController>.Instance.SelectedMission).GetLevelFromInspector(Singleton<GameController>.Instance.SelectedLevel);
			this.totalScore = 0;
			if (levelFromInspector != null)
			{
				this.totalCashEarned = levelFromInspector.bonusList[0];
				this.totalCoinsEarned = levelFromInspector.bonusList[1];
				this.totalXP = levelFromInspector.bonusList[2];
			}
		}
	}

	public int getTotalScore()
	{
		return this.totalScore;
	}

	public int getTotalCoinsEarned()
	{
		return this.totalCoinsEarned;
	}

	public void resetAllScore()
	{
	}

	public int getPlayerScore()
	{
		return PlayerPrefs.GetInt("PlayerScore", 0);
	}

	public int setPlayerScore()
	{
		return PlayerPrefs.GetInt("PlayerScore", 0);
	}

	public int getTotalXp()
	{
		return this.totalXP;
	}

	private static bool isCreated;

	public int totalScore;

	public int totalCoinsEarned;

	public int totalCashEarned;

	public int totalXP;

	public int enemyPoints = 50;

	public int tankPoints = 150;

	public int heliPoints = 150;

	public int destructionPoints = 30;

	public int enemyPtsEarned;

	public int tankPtsEarned;

	public int heliPtsEarned;

	public int destructionPtsEarned;
}
