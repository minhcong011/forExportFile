// dnSpy decompiler from Assembly-CSharp.dll class: DailyBonusRewardItemUI
using System;
using UnityEngine;
using UnityEngine.UI;

public class DailyBonusRewardItemUI : MonoBehaviour
{
	private void Start()
	{
	}

	public void setData(DailyBonusRewardItem bItem)
	{
		if (bItem == null)
		{
			UnityEngine.Debug.Log("UK :  Daily bonus Reward Item is null");
			return;
		}
		this.refItem = bItem;
		this.dayText.text = "DAY " + bItem.id.ToString();
		int num = 0;
		for (int i = 0; i < this.refItem.reward.Count; i++)
		{
			if (this.refItem.reward[i] != 0)
			{
				num = i;
				break;
			}
		}
		this.rewardImg.sprite = this.rewardImages[num];
		this.rewardVal.text = this.refItem.reward[num].ToString();
	}

	public Text dayText;

	public Text rewardVal;

	public Image rewardImg;

	private DailyBonusRewardItem refItem;

	public Sprite[] rewardImages;
}
