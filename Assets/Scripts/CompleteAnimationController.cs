// dnSpy decompiler from Assembly-CSharp.dll class: CompleteAnimationController
using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CompleteAnimationController : MonoBehaviour
{
	private void Awake()
	{
		this.bottomDefaultPos = this.bottom.anchoredPosition;
		this.TopBarDefaultPos = this.top.anchoredPosition;
		this.badgeScaleDefault = this.badge.transform.localScale;
	}

	private void OnEnable()
	{
		this.middleBg.alpha = 0f;
		this.bg.alpha = 0f;
		base.StartCoroutine(this.PlayAnimation());
	}

	private IEnumerator PlayAnimation()
	{
		if (this.completeClips[0] != null)
		{
			Singleton<GameController>.Instance.soundController.PlayGivenSound(this.completeClips[0]);
		}
		this.top.DOAnchorPos(new Vector2(this.top.anchoredPosition.x, 240f), 0.8f, false).SetEase(Ease.Linear).OnComplete(delegate
		{
			this.badge.gameObject.SetActive(true);
			this.badge.DOScale(new Vector2(1f, 1f), 0.8f).SetEase(Ease.OutBounce).OnComplete(delegate
			{
				this.PlayStars();
			});
			if (this.completeClips[1] != null)
			{
				Singleton<GameController>.Instance.soundController.PlayGivenSound(this.completeClips[1]);
			}
		});
		this.bottom.DOAnchorPos(new Vector2(this.bottom.anchoredPosition.x, -240f), 0.8f, false).SetEase(Ease.Linear).OnComplete(delegate
		{
			this.textDesc.gameObject.SetActive(true);
		});
		yield return new WaitForSeconds(1f);
		while (this.middleBg.alpha < 1f)
		{
			this.middleBg.alpha += 0.1f;
			yield return new WaitForSeconds(0.04f);
		}
		Time.timeScale = 1f;
		yield return new WaitForSeconds(1f);
		base.StartCoroutine(this.PlayReverseAnimation());
		yield break;
	}

	private IEnumerator PlayReverseAnimation()
	{
		yield return new WaitForSeconds(2.6f);
		this.textDesc.gameObject.SetActive(false);
		this.top.DOAnchorPos(new Vector2(this.top.anchoredPosition.x, this.TopBarDefaultPos.y), 1f, false).SetEase(Ease.Linear);
		this.bottom.DOAnchorPos(new Vector2(this.bottom.anchoredPosition.x, this.bottomDefaultPos.y), 1f, false).SetEase(Ease.Linear);
		Time.timeScale = 1f;
		yield return new WaitForSeconds(0.5f);
		this.badge.DOAnchorPos(new Vector2(this.badge.anchoredPosition.x, 250f), 0.6f, false).SetEase(Ease.Linear).OnComplete(delegate
		{
		});
		base.StartCoroutine(this.PlayBgAlpha());
		this.middleBg.alpha = 1f;
		while (this.middleBg.alpha > 0f)
		{
			this.middleBg.alpha -= 0.12f;
			yield return new WaitForSeconds(0.02f);
		}
		yield return new WaitForSeconds(0.2f);
		this.SetCallBack();
		yield break;
	}

	private IEnumerator PlayBgAlpha()
	{
		while (this.bg.alpha < 1f)
		{
			this.bg.alpha += 0.1f;
			yield return new WaitForSeconds(0.04f);
		}
		this.bg.alpha = 1f;
		yield return new WaitForSeconds(0.01f);
		yield break;
	}

	private void PlayStars()
	{
		this.PlayStarSound();
		this.stars[0].gameObject.SetActive(true);
		Sequence s = DOTween.Sequence();
		s.Append(this.stars[0].DOScale(new Vector3(1f, 1f, 1f), 0.4f).SetEase(Ease.InBounce).OnComplete(delegate
		{
			this.stars[1].gameObject.SetActive(true);
			this.PlayStarSound();
		}));
		s.PrependInterval(0.2f);
		s.Append(this.stars[1].DOScale(new Vector3(1f, 1f, 1f), 0.4f).SetEase(Ease.InBounce).OnComplete(delegate
		{
			this.stars[2].gameObject.SetActive(true);
			this.PlayStarSound();
		}));
		s.PrependInterval(0.2f);
		s.Append(this.stars[2].DOScale(new Vector3(1f, 1f, 1f), 0.4f).SetEase(Ease.InBounce));
	}

	private void SetCallBack()
	{
		this.callBackRoot.SendMessage("PlayAnimation", SendMessageOptions.DontRequireReceiver);
	}

	private void OnDisable()
	{
		this.bottom.anchoredPosition = this.bottomDefaultPos;
		this.top.anchoredPosition = this.TopBarDefaultPos;
		this.badge.transform.localScale = this.badgeScaleDefault;
	}

	private void PlayStarSound()
	{
		Singleton<GameController>.Instance.soundController.PlayCollectStar();
	}

	public RectTransform top;

	public RectTransform bottom;

	public RectTransform badge;

	public RectTransform textDesc;

	private Vector3 bottomDefaultPos;

	private Vector3 TopBarDefaultPos;

	private Vector3 badgeScaleDefault;

	public AudioClip[] completeClips;

	public GameObject callBackRoot;

	public CanvasGroup middleBg;

	public CanvasGroup bg;

	public RectTransform[] stars;
}
