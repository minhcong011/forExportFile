// dnSpy decompiler from Assembly-CSharp.dll class: TaskModelUI
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskModelUI : MonoBehaviour
{
	private void Start()
	{
	}

	public void setModel(TaskModel m)
	{
		this.model = m;
		MonoBehaviour.print(" reward " + this.model.rewardType.Count);
		this.rewardDetails = this.model.rewardType;
		this.updateUI();
	}

	public void updateUI()
	{
		if (this.model == null)
		{
			UnityEngine.Debug.Log("UK ::  TasksModelUI : updateUI : Model is null : returning");
			return;
		}
		this.details.text = this.model.details.ToString();
		this.heading.text = this.model.name.ToString();
		this.stepsCount.text = this.model.currentStep.ToString() + " / " + this.model.totalSteps;
		this.progressVal.fillAmount = (float)this.model.currentStep / (float)this.model.totalSteps;
		if (this.model.isTaskCompleted())
		{
			if (this.model.getIsClaimed())
			{
				this.claimButton.SetActive(false);
				this.claimed.gameObject.SetActive(true);
				this.claimButton.GetComponent<Button>().interactable = true;
			}
			else
			{
				this.claimButton.SetActive(true);
			}
		}
		else
		{
			this.claimButton.GetComponent<Button>().interactable = false;
		}
		this.setRewardDetails();
	}

	private void setRewardDetails()
	{
		MonoBehaviour.print("setting reward " + this.rewardDetails.Count);
		string text = string.Empty;
		for (int i = 0; i < this.rewardDetails.Count; i++)
		{
			if (this.rewardDetails[i] != 0)
			{
				if (i == 0)
				{
					text = text + string.Empty + this.rewardDetails[i];
					this.rewardImage.sprite = this.rewardSprites[0];
					break;
				}
				if (i == 1)
				{
					text = text + "  " + this.rewardDetails[i];
					this.rewardImage.sprite = this.rewardSprites[1];
				}
			}
		}
		this.reward.text = text;
	}

	public void ClaimPressed()
	{
		this.model.setClaimed();
		for (int i = 0; i < this.rewardDetails.Count; i++)
		{
			if (i == 0)
			{
				Singleton<GameController>.Instance.storeManager.addCash(this.rewardDetails[i]);
			}
			else if (i == 1)
			{
				Singleton<GameController>.Instance.storeManager.addCoins(this.rewardDetails[i]);
			}
		}
		this.updateUI();
		TaskPanelUI taskPanelUI = UnityEngine.Object.FindObjectOfType<TaskPanelUI>();
		if (taskPanelUI != null)
		{
			taskPanelUI.PlayRewardAnimation();
		}
		Singleton<GameController>.Instance.eventManager.UpdateStoreUI();
		Singleton<GameController>.Instance.soundController.PlayClaimSound();
	}

	public GameObject claimButton;

	public GameObject claimed;

	public Text heading;

	public Text details;

	public Text stepsCount;

	public Text reward;

	public List<int> rewardDetails;

	public Sprite[] rewardSprites;

	public Image progressVal;

	public Image rewardImage;

	private TaskModel model;
}
