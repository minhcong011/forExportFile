// dnSpy decompiler from Assembly-CSharp.dll class: GameController
using System;
using UnityEngine;

public class GameController : Singleton<GameController>
{
	public int unlockedMissions { get; set; }

	public int unlockedLevels { get; set; }

	private void Start()
	{
		if (!PlayerPrefs.HasKey("unlockedMissions"))
		{
			PlayerPrefs.SetString("unlockedMissions", "1_1");
			this.unlockedMissions = 1;
			this.unlockedLevels = 1;
		}
		else
		{
			string @string = PlayerPrefs.GetString("unlockedMissions", "1_1");
			string[] array = @string.Split(new char[]
			{
				'_'
			});
			this.unlockedMissions = (int)Convert.ToInt16(array[0]);
			this.unlockedLevels = (int)Convert.ToInt16(array[1]);
		}
		Application.targetFrameRate = 30;
		Screen.sleepTimeout = -1;
	}

	public int getUnlockedMissions()
	{
		string @string = PlayerPrefs.GetString("unlockedMissions", "1_1");
		string[] array = @string.Split(new char[]
		{
			'_'
		});
		this.unlockedMissions = (int)Convert.ToInt16(array[0]);
		return this.unlockedMissions;
	}

	public int getUnlockedLevels()
	{
		string @string = PlayerPrefs.GetString("unlockedMissions", "1_1");
		string[] array = @string.Split(new char[]
		{
			'_'
		});
		this.unlockedLevels = (int)Convert.ToInt16(array[1]);
		return this.unlockedLevels;
	}

	public int UnlockNextStage()
	{
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			" unlockedMissions : ",
			this.unlockedMissions,
			"  selectedMission :",
			this.SelectedMission
		}));
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			" selectedStage : ",
			this.SelectedLevel,
			"  unlockedStages : ",
			this.unlockedLevels
		}));
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			" selectedStage : ",
			this.SelectedLevel,
			"  unlockedStages : ",
			this.unlockedLevels
		}));
		UnityEngine.Debug.Log(" unlocked  " + PlayerPrefs.GetString("unlockedMissions", "null"));
		int result = 0;
		if (this.unlockedMissions == this.SelectedMission && this.SelectedLevel == this.unlockedLevels)
		{
			if (this.unlockedLevels < this.totalLevels)
			{
				this.unlockedLevels = this.SelectedLevel + 1;
				PlayerPrefs.SetString("unlockedMissions", this.SelectedMission + "_" + this.unlockedLevels);
				result = 1;
			}
			else if (this.unlockedLevels == this.totalLevels)
			{
				this.unlockedMissions = this.SelectedMission + 1;
				this.unlockedLevels = 1;
				if (this.unlockedMissions <= 3)
				{
					PlayerPrefs.SetString("unlockedMissions", this.unlockedMissions + "_1");
				}
				result = 2;
			}
		}
		UnityEngine.Debug.Log(" unlocked after " + PlayerPrefs.GetString("unlockedMissions", "null"));
		return result;
	}

	public bool isCarCollected;

	public bool GunCollected;

	public int SelectedMission = 1;

	public int SelectedLevel = 1;

	public int totalLevels = 5;

	public int videoWatchedTimes;

	public bool canTakeffect;

	public SoundController soundController;

	public ScoreManager scoreManager;

	public StoreManager storeManager;

	public WeaponManagerStore weaponManager;

	public AchievementsManager achievementManager;

	public DailyLoginManager dailyLoginManager;

	public DailyBonusManager dailyBonusManager;

	public CustomNotificationManager customNotificationManager;

	public GameSceneController gameSceneController;

	public AllGameControllers refAllController;

	public AirStrikeController airStrikeController;

	public DailyTaskManager taskManager;

	public EventManager eventManager;

	public UKAdsManager adsManager;

	public bool canTouch;

	public int touchIndex = -1;

	public int rateUsLimitCount;

	public int fromGamePlay;

	public bool shopOpened;

	public int inAppId;

	public UIPopUpManager uiPopUpManager;

	public UserLevelStatusManager userLevelStatusManager;

	public RateUsController rateUsController;

	public UkGameDataLoaderInspector ukGameDataLoader;

	public PromotionalSplashScreenController promotionalSplashes;
}
