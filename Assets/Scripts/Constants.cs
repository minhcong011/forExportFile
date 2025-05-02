// dnSpy decompiler from Assembly-CSharp.dll class: Constants
using System;
using UnityEngine;

public static class Constants
{
	public static bool isSoundOn()
	{
		return PlayerPrefs.GetInt("sound", 0) == 0;
	}

	public static bool isMusicOn()
	{
		return PlayerPrefs.GetInt("music", 0) == 0;
	}

	public static void setMusic(int val)
	{
		PlayerPrefs.SetInt("music", val);
	}

	public static void setSound(int val)
	{
		PlayerPrefs.SetInt("sound", val);
	}

	public static void setSensitivity(float val)
	{
		PlayerPrefs.SetFloat("sensitivity", val);
	}

	public static float getSensitivity()
	{
		return PlayerPrefs.GetFloat("sensitivity", 1f);
	}

	public static void setAddsPurchasedStatus()
	{
		PlayerPrefs.SetInt("AdsPurchased", 1);
	}

	public static bool getAddsPurchasedStatus()
	{
		return PlayerPrefs.GetInt("AdsPurchased", 0) != 0;
	}

	public static void setTutorialComplete()
	{
		PlayerPrefs.SetInt("tutorialCompleted", 1);
	}

	public static bool isTutorialCompleted()
	{
		return PlayerPrefs.GetInt("tutorialCompleted", 0) != 0;
	}

	public static int GetCurrentTutorialStep()
	{
		return PlayerPrefs.GetInt("tutorialStep", 1);
	}

	public static void SetCurrentTutorialStep(int s)
	{
		PlayerPrefs.SetInt("tutorialStep", s);
	}

	public static string unityRewardedId = "rewardedVideoZone";

	public static string GAME_NAME = string.Empty + Application.productName;

	public static string RATE_US_LINK = "https://play.google.com/store/apps/details?id=" + Application.identifier;

	public static string MORE_GAMES_LINK = "https://play.google.com/store/apps/developer?id=GrimBots";

	public static string HOT_GAME_LINK1 = "https://play.google.com/store/apps/details?id=grimbots_action_game.mega_army_survival.offline_sniper_shooting";

	public static string HOT_GAME_LINK2 = "https://play.google.com/store/apps/details?id=grimbots_action_game.sniperroyale.shooter.android.gun_war";

	public static string HOT_GAME_LINK3 = "https://play.google.com/store/apps/details?id=grimbots_action_games.car.clash.epic.shooting.io";

	public static int DAILY_BONUS_NOTIFICATION_ID = 2;

	public static int DAILY_SPIN_NOTIFICATION_ID = 3;

	public static int ACHIEVEMENTS_NOTIFICATION_ID = 4;

	public static int DAILYTASK_NOTIFICATION_ID = 5;

	public static int QUARTER_NOTIFICATION_ID = 9;

	public static int ALLTIME_NOTIFICATION_ID = 10;
}
