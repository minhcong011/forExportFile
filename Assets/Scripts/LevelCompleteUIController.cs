// dnSpy decompiler from Assembly-CSharp.dll class: LevelCompleteUIController
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUIController : BaseUI
{
	private void Awake()
	{
		this.successFullText.gameObject.SetActive(false);
	}

	private void Start()
	{
		if (Singleton<GameController>.Instance.SelectedLevel == 1)
		{
			if (Singleton<GameController>.Instance.achievementManager != null)
			{
				Singleton<GameController>.Instance.achievementManager.updateAchievement(2);
			}
			if (Singleton<GameController>.Instance.taskManager != null)
			{
				Singleton<GameController>.Instance.taskManager.getCurrentDayModel().updateTask(2);
				Singleton<GameController>.Instance.taskManager.getCurrentDayModel().updateTask(9);
			}
		}
		else if (Singleton<GameController>.Instance.SelectedLevel != 3)
		{
			if (Singleton<GameController>.Instance.SelectedLevel == 5)
			{
				if (Singleton<GameController>.Instance.achievementManager != null)
				{
					Singleton<GameController>.Instance.achievementManager.updateAchievement(7);
				}
			}
			else if (Singleton<GameController>.Instance.SelectedLevel != 6)
			{
				if (Singleton<GameController>.Instance.SelectedLevel == 10 && Singleton<GameController>.Instance.achievementManager != null)
				{
					Singleton<GameController>.Instance.achievementManager.updateAchievement(8);
				}
			}
		}
		if (Singleton<GameController>.Instance.taskManager != null)
		{
			Singleton<GameController>.Instance.taskManager.getCurrentDayModel().updateTask(9);
		}
	}

	private IEnumerator playWinAnimation()
	{
		GameSceneController sceneRef = Singleton<GameController>.Instance.gameSceneController;
		if (sceneRef.PlayerHealth > 50f)
		{
		}
		if (sceneRef.elapsedTime < 150f)
		{
		}
		yield return new WaitForSeconds(0.1f);
		yield break;
	}

	public void calculateScores()
	{
		ScoreManager scoreManager = Singleton<GameController>.Instance.scoreManager;
		StoreManager storeManager = Singleton<GameController>.Instance.storeManager;
		scoreManager.calculateScores();
		this.enemiesPointLbl.text = scoreManager.totalCashEarned.ToString();
		int num = UnityEngine.Random.Range(4, 20);
		num *= 10;
		this.headShotLbl.text = num.ToString();
		int num2 = (int)Convert.ToInt16(Singleton<GameController>.Instance.gameSceneController.PlayerHealth * 2f);
		this.healthlbl.text = num2.ToString();
		int quantity = num + scoreManager.totalCashEarned + num2;
		this.totalPoints.text = quantity.ToString();
		storeManager.addCash(quantity);
		storeManager.addXP(scoreManager.getTotalXp());
	}

	public void PlayAnimation()
	{
		this.completeRoot.SetActive(true);
		Sequence s = DOTween.Sequence();
		s.Append(this.rewards[0].DOAnchorPos(new Vector2(0f, this.rewards[0].anchoredPosition.y), 0.6f, false).SetEase(Ease.InOutBounce));
		s.Append(this.rewards[1].DOAnchorPos(new Vector2(0f, this.rewards[1].anchoredPosition.y), 0.6f, false).SetEase(Ease.InOutBounce));
		s.Append(this.rewards[2].DOAnchorPos(new Vector2(0f, this.rewards[2].anchoredPosition.y), 0.6f, false).SetEase(Ease.InOutBounce));
		s.Append(this.rewards[3].DOAnchorPos(new Vector2(0f, this.rewards[3].anchoredPosition.y), 0.6f, false).SetEase(Ease.InOutBounce).OnComplete(delegate
		{
		}));
		s.Append(this.bottomBtns.DOAnchorPos(new Vector2(this.bottomBtns.anchoredPosition.x, 100f), 0.8f, false).SetEase(Ease.OutBounce));
	}

	private void PlayStarSound()
	{
		Singleton<GameController>.Instance.soundController.PlayCollectStar();
	}

	private void OnEnable()
	{
		this.calculateScores();
		this.MarkComplete();
		base.StartCoroutine(this.playWinAnimation());
	}

	public void MarkComplete()
	{
		Singleton<GameController>.Instance.UnlockNextStage();
	}

	public void Next()
	{
		Singleton<GameController>.Instance.SelectedLevel++;
		if (Singleton<GameController>.Instance.SelectedLevel > Singleton<GameController>.Instance.totalLevels)
		{
			base.UIClicks("Menu");
		}
		else
		{
			base.UIClicks("Next");
		}
	}

	public Text enemiesPointLbl;

	public Text headShotLbl;

	public Text healthlbl;

	public Text totalPoints;

	public Text tankPointLbl;

	public Text totalPointLbl;

	public Text coinsEarnedLbl;

	public Text choppersEarnedLbl;

	public RectTransform[] stars;

	public RectTransform[] rewards;

	public RectTransform bottomBtns;

	public RectTransform topBar;

	public RectTransform successFullText;

	public GameObject completeRoot;
}
