// dnSpy decompiler from Assembly-CSharp.dll class: AdsParser
using System;
using UnityEngine;

public class AdsParser
{
	public AdsParser()
	{
		this.MainMenu = -1;
		this.Exit = 0;
		this.LevelSelection = -1;
		this.MissionSelection = -1;
		this.StoreSelection = -1;
		this.LoadingGamePlay = 0;
		this.Pause = 0;
		this.GameOver = 1;
		this.LevelClear = 1;
		this.Replay = 0;
		this.Next = 0;
		this.Home = 0;
	}

	public int MainMenu { get; set; }

	public int LevelSelection { get; set; }

	public int MissionSelection { get; set; }

	public int StoreSelection { get; set; }

	public int LoadingGamePlay { get; set; }

	public int Pause { get; set; }

	public int GameOver { get; set; }

	public int LevelClear { get; set; }

	public int Replay { get; set; }

	public int Next { get; set; }

	public int Exit { get; set; }

	public int Home { get; set; }

	public void SetInitials(UKMap ads)
	{
		if (ads == null)
		{
			UnityEngine.Debug.Log("ads :: constructor : ads map is null");
			return;
		}
		if (ads.ContainsProperty("MainMenu"))
		{
			this.MainMenu = ads.GetIntValueForKey("MainMenu", -1);
		}
		if (ads.ContainsProperty("Exit"))
		{
			this.Exit = ads.GetIntValueForKey("Exit", -1);
		}
		if (ads.ContainsProperty("LevelSelection"))
		{
			this.LevelSelection = ads.GetIntValueForKey("LevelSelection", -1);
		}
		if (ads.ContainsProperty("MissionSelection"))
		{
			this.MissionSelection = ads.GetIntValueForKey("MissionSelection", -1);
		}
		if (ads.ContainsProperty("StoreSelection"))
		{
			this.StoreSelection = ads.GetIntValueForKey("StoreSelection", -1);
		}
		if (ads.ContainsProperty("LoadingGamePlay"))
		{
			this.LoadingGamePlay = ads.GetIntValueForKey("LoadingGamePlay", -1);
		}
		if (ads.ContainsProperty("Pause"))
		{
			this.Pause = ads.GetIntValueForKey("Pause", -1);
		}
		if (ads.ContainsProperty("GameOver"))
		{
			this.GameOver = ads.GetIntValueForKey("GameOver", -1);
		}
		if (ads.ContainsProperty("LevelClear"))
		{
			this.LevelClear = ads.GetIntValueForKey("LevelClear", -1);
		}
		if (ads.ContainsProperty("Replay"))
		{
			this.Replay = ads.GetIntValueForKey("Replay", -1);
		}
		if (ads.ContainsProperty("Next"))
		{
			this.Next = ads.GetIntValueForKey("Next", -1);
		}
		if (ads.ContainsProperty("Home"))
		{
			this.Home = ads.GetIntValueForKey("Home", -1);
		}
	}
}
