// dnSpy decompiler from Assembly-CSharp.dll class: MissionSelectionUI
using System;
using UnityEngine;
using UnityEngine.UI;

public class MissionSelectionUI : MonoBehaviour
{
	private void Awake()
	{
		this.SetMissions();
	}

	private void OnEnable()
	{
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
			adsManager.LogScreen("Mission Selection-");
			adsManager.showInterstitial("MissionSelection");
		}
	}

	private void Start()
	{
		this.startPos = this.MissionRoot.localPosition;
	}

	private void OnDisable()
	{
		this.MissionRoot.localPosition = this.startPos;
		this.currentMissionId = 1;
	}

	private void SetMissions()
	{
		int unlockedMissions = Singleton<GameController>.Instance.getUnlockedMissions();
		for (int i = 0; i < this.missions.Length; i++)
		{
			if (i < unlockedMissions)
			{
				this.missions[i].SetActive(false);
			}
			else
			{
				this.missions[i].SetActive(true);
			}
		}
	}

	public void MissionBtnsClicked(string action)
	{
		if (action != null)
		{
			if (action == "Back")
			{
				Singleton<GameController>.Instance.soundController.PlayButtonClick();
				UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(0);
			}
		}
	}

	public void MissionClicked(int id)
	{
		int unlockedMissions = Singleton<GameController>.Instance.getUnlockedMissions();
		if (id > unlockedMissions)
		{
			UnityEngine.Debug.Log("Sorry Misssion Closed");
			return;
		}
		this.currentMissionId = id;
		if (this.currentMissionId <= unlockedMissions)
		{
			Singleton<GameController>.Instance.SelectedMission = this.currentMissionId;
			UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(2);
		}
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
	}

	private void HideNextBtn()
	{
		this.nextBtn.SetActive(false);
	}

	private void ShowNextBtn()
	{
		this.nextBtn.SetActive(true);
	}

	private void setDetails()
	{
		this.missionsText.text = this.missionDetails[this.currentMissionId - 1];
	}

	public void NextButton()
	{
		int unlockedMissions = Singleton<GameController>.Instance.getUnlockedMissions();
		if (this.currentMissionId <= unlockedMissions)
		{
			Singleton<GameController>.Instance.SelectedMission = this.currentMissionId;
			UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(2);
		}
	}

	private void setSelectedImage()
	{
		for (int i = 0; i < this.selectedImages.Length; i++)
		{
			this.selectedImages[i].SetActive(false);
		}
		this.selectedImages[this.currentMissionId - 1].SetActive(true);
	}

	public GameObject[] missions;

	public GameObject[] selectedImages;

	public Transform MissionRoot;

	public Text missionsText;

	private string[] missionDetails = new string[]
	{
		"Mission 1",
		"Mission 2",
		"Mission 3"
	};

	public GameObject nextBtn;

	private Vector3 startPos;

	private int currentMissionId = 1;
}
