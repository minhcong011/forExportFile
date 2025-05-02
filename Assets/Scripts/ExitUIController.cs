// dnSpy decompiler from Assembly-CSharp.dll class: ExitUIController
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ExitUIController : MonoBehaviour
{
	private void Awake()
	{
		this.bottomDefaultPos = this.bottomBtns.anchoredPosition;
		this.TopBarDefaultPos = this.topBar.anchoredPosition;
		this.hotGamesDefaultPos = this.hotGames.anchoredPosition;
	}

	private void OnEnable()
	{
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		int num = UnityEngine.Random.Range(0, 3);
		if (adsManager != null && num == 1)
		{
			adsManager.showInterstitial("Exit");
		}
		this.PlayAnimation();
	}

	private void PlayAnimation()
	{
		this.description.GetComponent<Text>().text = "Are you sure, you want to quit !!";
		Sequence s = DOTween.Sequence();
		s.Append(this.topBar.DOAnchorPos(new Vector2(this.topBar.anchoredPosition.x, -125f), 0.6f, false).SetEase(Ease.OutBounce).OnComplete(delegate
		{
			this.description.gameObject.SetActive(true);
		}));
		s.AppendInterval(1f);
		s.Append(this.hotGames.DOAnchorPos(new Vector2(0f, this.hotGames.anchoredPosition.y), 0.6f, false).SetEase(Ease.OutBounce));
		s.Append(this.bottomBtns.DOAnchorPos(new Vector2(this.bottomBtns.anchoredPosition.x, 100f), 0.8f, false).SetEase(Ease.OutBounce));
	}

	private void OnDisable()
	{
		this.description.gameObject.SetActive(false);
		this.bottomBtns.anchoredPosition = this.bottomDefaultPos;
		this.topBar.anchoredPosition = this.TopBarDefaultPos;
		this.hotGames.anchoredPosition = this.hotGamesDefaultPos;
	}

	public void YesPressed()
	{
		Application.Quit();
	}

	public void NoPressed()
	{
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
		UnityEngine.Object.Destroy(base.gameObject, 0.13f);
		Resources.UnloadUnusedAssets();
	}

	public void ExitClicked(string action)
	{
		if (action != null)
		{
			if (!(action == "Cancel"))
			{
				if (!(action == "MoreGames"))
				{
					if (!(action == "RateUs"))
					{
						if (!(action == "Game1"))
						{
							if (!(action == "Game2"))
							{
								if (action == "Game3")
								{
									this.Game3Pressed();
								}
							}
							else
							{
								this.Game2Pressed();
							}
						}
						else
						{
							this.Game1Pressed();
						}
					}
					else
					{
						this.RateUsPressed();
					}
				}
				else
				{
					this.MoreGamesPressed();
				}
			}
			else
			{
				UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(0);
			}
		}
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
	}

	public void MoreGamesPressed()
	{
		Application.OpenURL(string.Empty);
	}

	public void RateUsPressed()
	{
		Application.OpenURL(Constants.RATE_US_LINK);
	}

	public void Game1Pressed()
	{
		Application.OpenURL(Constants.HOT_GAME_LINK1);
	}

	public void Game2Pressed()
	{
		Application.OpenURL(Constants.HOT_GAME_LINK2);
	}

	public void Game3Pressed()
	{
		Application.OpenURL(Constants.HOT_GAME_LINK3);
	}

	public RectTransform bottomBtns;

	public RectTransform topBar;

	public RectTransform hotGames;

	public RectTransform description;

	public Vector3 bottomDefaultPos;

	public Vector3 TopBarDefaultPos;

	public Vector3 hotGamesDefaultPos;
}
