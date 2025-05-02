// dnSpy decompiler from Assembly-CSharp.dll class: RateUsScreen
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RateUsScreen : MonoBehaviour
{
	private void Awake()
	{
		this.defaultTopPos = this.top.anchoredPosition;
		this.defaultBottomPos = this.bottom.anchoredPosition;
		this.defaultHotGamesPos = this.hotGames.anchoredPosition;
	}

	private void PlayAnimation()
	{
		Sequence s = DOTween.Sequence();
		this.textDesc.SetActive(false);
		this.desc.text = "DID YOU LIKE OUR GAME, RATE US !!!";
		s.Append(this.top.DOAnchorPos(new Vector2(40f, -160f), 0.3f, false).SetEase(Ease.Linear).OnComplete(delegate
		{
			this.textDesc.SetActive(true);
			this.hotGames.DOAnchorPos(new Vector2(0f, -54f), 0.8f, false).SetEase(Ease.OutBounce);
		}));
		s.Append(this.bottom.DOAnchorPos(new Vector2(0f, 29f), 1.1f, false).SetEase(Ease.InOutBounce));
	}

	private void OnEnable()
	{
		this.PlayAnimation();
		this.textDesc.SetActive(false);
	}

	private void OnDisable()
	{
		this.top.anchoredPosition = this.defaultTopPos;
		this.bottom.anchoredPosition = this.defaultBottomPos;
		this.hotGames.anchoredPosition = this.defaultHotGamesPos;
		this.textDesc.SetActive(false);
		this.desc.text = "DID YOU LIKE OUR GAME, RATE US !!!";
	}

	public void YesPressed()
	{
		Application.OpenURL(Constants.RATE_US_LINK);
	}

	public void NoPressed()
	{
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
		UnityEngine.Object.Destroy(base.gameObject, 0.13f);
		Resources.UnloadUnusedAssets();
	}

	public void ExitClicked(string action)
	{
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
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
	}

	public void MoreGamesPressed()
	{
		Application.OpenURL(Constants.MORE_GAMES_LINK);
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

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			this.NoPressed();
		}
	}

	public RectTransform top;

	public RectTransform bottom;

	public RectTransform hotGames;

	public GameObject textDesc;

	public Text desc;

	private Vector2 defaultTopPos;

	private Vector2 defaultBottomPos;

	private Vector2 defaultHotGamesPos;
}
