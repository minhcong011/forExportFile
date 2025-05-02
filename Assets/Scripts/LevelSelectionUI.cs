// dnSpy decompiler from Assembly-CSharp.dll class: LevelSelectionUI
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionUI : MonoBehaviour
{
	private void Start()
	{
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
			adsManager.LogScreen("LevelSelection");
			adsManager.showInterstitial("LevelSelection");
		}
	}

	private void InstantiateLevels()
	{
		this.unlockedMissions = Singleton<GameController>.Instance.getUnlockedMissions();
		this.currentLevelId = 0;
		this.unlockedLevels = Singleton<GameController>.Instance.getUnlockedLevels();
		for (int i = 0; i < this.maps.Length; i++)
		{
			if (i <= this.unlockedMissions - 1)
			{
				if (i == this.unlockedMissions - 1)
				{
					this.maps[i].UnlockLevels(this.unlockedLevels);
				}
				else
				{
					this.maps[i].setAllUnLocked();
				}
			}
			else
			{
				this.maps[i].setAllLocked();
			}
		}
		this.isInitialised = true;
	}

	private IEnumerator ShowMaps()
	{
		yield return new WaitForSeconds(0.2f);
		for (int i = 0; i < this.maps.Length; i++)
		{
			this.maps[i].ShowMap();
			yield return new WaitForSeconds(0.6f);
		}
		yield break;
	}

	private void OnEnable()
	{
		Time.timeScale = 1f;
		this.currentLevelId = 0;
		if (!this.isInitialised)
		{
			this.InstantiateLevels();
		}
		base.StartCoroutine(this.ShowMaps());
	}

	private void OnDisable()
	{
		this.currentMissionId = 0;
		this.currentLevelId = 0;
		this.lastMissionId = 0;
		this.lastLevelId = 0;
	}

	private void setPanelPos()
	{
	}

	private void showDescription(string s)
	{
		this.objectives.gameObject.SetActive(false);
		this.objectives.text = s;
		this.objectives.gameObject.SetActive(true);
	}

	public void LevelClicked(int missionNumber = 0, int lvlNumb = 0)
	{
		UnityEngine.Debug.Log(lvlNumb);
		UnityEngine.Debug.Log("unlockedStages " + this.unlockedLevels);
		if (missionNumber >= this.unlockedMissions)
		{
			if (missionNumber == this.unlockedMissions && lvlNumb > this.unlockedLevels)
			{
				this.showDescription("Level is locked");
				return;
			}
			if (missionNumber > this.unlockedMissions)
			{
				this.showDescription("Level is locked");
				return;
			}
		}
		if (this.currentLevelId == lvlNumb && this.currentMissionId == missionNumber)
		{
			return;
		}
		this.lastMissionId = this.currentMissionId;
		this.lastLevelId = this.currentLevelId;
		this.currentLevelId = lvlNumb;
		this.currentMissionId = missionNumber;
		this.setDetails();
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
	}

	public void startClicked()
	{
		UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(6);
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
	}

	public void NextButton()
	{
		if (this.currentLevelId == 0)
		{
			Singleton<GameController>.Instance.soundController.PlayButtonClick();
			this.showDescription("Please select level");
			return;
		}
		if (this.currentMissionId > this.unlockedMissions)
		{
			return;
		}
		if (this.currentMissionId == this.unlockedMissions && this.currentLevelId > this.unlockedLevels)
		{
			return;
		}
		Singleton<GameController>.Instance.SelectedLevel = this.currentLevelId;
		Singleton<GameController>.Instance.SelectedMission = this.currentMissionId;
		this.startClicked();
	}

	private void setDetails()
	{
		this.lvlNumbText.text = "# " + this.currentLevelId.ToString();
		if (Singleton<GameController>.Instance.ukGameDataLoader != null)
		{
			LevelFromInspector levelFromInspector = Singleton<GameController>.Instance.ukGameDataLoader.GetMissionWithIndex(this.currentMissionId).GetLevelFromInspector(this.currentLevelId);
			if (levelFromInspector != null)
			{
				this.objectives.text = levelFromInspector.lvlBriefings;
				this.showDescription(levelFromInspector.lvlBriefings);
				this.cash.text = levelFromInspector.bonusList[0].ToString();
			}
			else
			{
				UnityEngine.Debug.Log(" Level Selection ::  levelmeta is NUllllllll");
			}
		}
		if (this.lastMissionId != 0 && this.lastLevelId != 0)
		{
			this.maps[this.lastMissionId - 1].SetAsNormal(this.lastLevelId);
		}
		this.maps[this.currentMissionId - 1].SetAsSelected(this.currentLevelId);
	}

	public void BackClicked()
	{
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
		UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(1);
	}

	private int unlockedLevels = 1;

	private int unlockedMissions = 1;

	private int currentMissionId;

	private int currentLevelId = 1;

	private int lastMissionId;

	private int lastLevelId;

	public LoadingGamePlay loadingGamePlay;

	public Text objectives;

	public Text cash;

	public Text gold;

	public Text xps;

	public Text lvlNumbText;

	public Sprite selectedSprite;

	public Sprite clearedLevel;

	public Sprite lockedLvl;

	public RectTransform panelRect;

	private bool isInitialised;

	public MapSelectionItem[] maps;
}
