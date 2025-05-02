// dnSpy decompiler from Assembly-CSharp.dll class: TaskPanelUI
using System;
using UnityEngine;

public class TaskPanelUI : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnEnable()
	{
		MonoBehaviour.print("enable called");
		this.enableById();
	}

	private void OnDisable()
	{
		MonoBehaviour.print("Disable called");
		this.panelsArray[this.screenId].SetActive(false);
		this.selectedPanel[this.screenId].SetActive(false);
		this.rewardScreen.SetActive(false);
	}

	private void enableById()
	{
		this.panelsArray[this.screenId].SetActive(true);
		this.selectedPanel[this.screenId].SetActive(true);
	}

	public void setSelectedBtn(int id)
	{
		if (id == this.screenId)
		{
			return;
		}
		this.panelsArray[this.screenId].SetActive(false);
		this.selectedPanel[this.screenId].SetActive(false);
		this.screenId = id;
		this.panelsArray[this.screenId].SetActive(true);
		this.selectedPanel[this.screenId].SetActive(true);
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
	}

	public void Exit()
	{
		UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(0);
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
	}

	public void PlayRewardAnimation()
	{
		this.rewardScreen.GetComponent<RewardpopupUIController>().enableAndSet(this.rewardSprites[0], 0, 0);
	}

	public int screenId;

	public GameObject[] panelsArray;

	public GameObject[] selectedPanel;

	public GameObject rewardScreen;

	public Sprite[] rewardSprites;
}
