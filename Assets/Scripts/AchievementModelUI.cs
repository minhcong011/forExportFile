// dnSpy decompiler from Assembly-CSharp.dll class: AchievementModelUI
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementModelUI : MonoBehaviour
{
	private void Start()
	{
	}

	public void setModel(AchievementModel m)
	{
		this.model = m;
		this.rewardDetails = this.model.rewardType;
		this.updateUI();
	}

	public void updateUI()
	{
		if (this.model == null)
		{
			UnityEngine.Debug.Log("UK ::  achievementsModelUI : updateUI : Model is null : returning");
			return;
		}
		this.details.text = this.model.details.ToString();
		this.heading.text = this.model.name.ToString();
		this.stepsCount.text = this.model.currentStep.ToString() + " / " + this.model.totalSteps;
		this.progressVal.fillAmount = (float)this.model.currentStep / (float)this.model.totalSteps;
		if (this.model.isAchievementCompleted())
		{
			if (this.model.getIsClaimed())
			{
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
		TaskPanelUI taskPanelUI = UnityEngine.Object.FindObjectOfType<TaskPanelUI>();
		if (taskPanelUI != null)
		{
			taskPanelUI.PlayRewardAnimation();
		}
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
		Singleton<GameController>.Instance.soundController.PlayClaimSound();
		Singleton<GameController>.Instance.eventManager.UpdateStoreUI();
	}

	public GameObject claimButton;

	public GameObject claimedText;

	public GameObject claimed;

	public Text heading;

	public Text details;

	public Text stepsCount;

	public Text reward;

	public Image progressVal;

	public Image rewardImage;

	public Sprite[] rewardSprites;

	public List<int> rewardDetails;

	private AchievementModel model;
}
