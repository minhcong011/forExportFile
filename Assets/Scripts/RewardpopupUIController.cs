// dnSpy decompiler from Assembly-CSharp.dll class: RewardpopupUIController
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RewardpopupUIController : MonoBehaviour
{
	private void Awake()
	{
		this.defaultImage = this.reward.sprite;
	}

	private void OnEnable()
	{
		base.StartCoroutine(this.playAnimationLoop());
		Singleton<GameController>.Instance.soundController.PlayGiftPopUp();
		if (this.cg != null)
		{
			this.cg.alpha = 1f;
		}
	}

	private void OnDisable()
	{
		this.reward.GetComponent<TweenScale>().ResetToBeginning();
		this.reward.sprite = this.defaultImage;
		for (int i = 0; i < this.spritesToMove.Length; i++)
		{
			this.spritesToMove[i].ResetToBeginning();
			this.spritesToMove[i].GetComponent<TweenScale>().ResetToBeginning();
			this.spritesToMove[i].gameObject.SetActive(false);
		}
		this.rewardType = 0;
		if (this.cg != null)
		{
			this.cg.alpha = 1f;
		}
	}

	private IEnumerator playAnimationLoop()
	{
		yield return new WaitForSeconds(0.5f);
		this.reward.gameObject.SetActive(true);
		this.reward.GetComponent<TweenScale>().PlayForward();
		this.reward.GetComponent<TweenRotation>().PlayForward();
		yield return new WaitForSeconds(1.7f);
		if (this.spritesToMove.Length > 0 && this.rewardType <= 1)
		{
			this.playMovingAnim();
			yield return new WaitForSeconds(1.2f);
		}
		MonoBehaviour.print("before Time");
		this.okPressed();
		yield break;
	}

	private void playMovingAnim()
	{
		if (this.rewardType == 0)
		{
			this.spritesToMove[0].PlayForward();
			this.spritesToMove[0].GetComponent<TweenScale>().PlayForward();
			this.spritesToMove[0].gameObject.SetActive(true);
		}
		else if (this.rewardType == 1)
		{
			this.spritesToMove[1].PlayForward();
			this.spritesToMove[1].GetComponent<TweenScale>().PlayForward();
			this.spritesToMove[1].gameObject.SetActive(true);
		}
	}

	public void enableWithDefault()
	{
		this.rewardCount = 10;
		base.gameObject.SetActive(true);
	}

	public void enableAndSet(Sprite img, int rewardType1 = 0, int rewardC = 0)
	{
		this.reward.sprite = img;
		this.rewardCount = rewardC;
		this.rewardType = rewardType1;
		base.gameObject.SetActive(true);
	}

	public void okPressed()
	{
		MonoBehaviour.print("called");
		if (this.endCB == 0)
		{
			MonoBehaviour.print("Got it");
			UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(0);
		}
		else if (this.endCB == 1)
		{
			base.gameObject.SetActive(false);
		}
	}

	public Image reward;

	private int rewardCount;

	private int rewardType;

	private Sprite defaultImage;

	public int endCB;

	public TweenPosition[] spritesToMove;

	public CanvasGroup cg;
}
