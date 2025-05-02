// dnSpy decompiler from Assembly-CSharp.dll class: NotEnoughCreditUI
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NotEnoughCreditUI : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnEnable()
	{
		this.leftBarDefaultPos = this.leftBar.anchoredPosition;
		this.bottomBarDefault = this.bottomBar.anchoredPosition;
		Sequence sequence = DOTween.Sequence();
		this.leftBar.DOAnchorPos(new Vector2(394f, this.leftBar.anchoredPosition.y), 1f, false).SetEase(Ease.InQuad);
		this.bottomBar.DOAnchorPos(new Vector2(this.bottomBar.anchoredPosition.x, 34f), 1f, false).SetEase(Ease.InOutBounce).OnComplete(delegate
		{
			this.crossBtn.gameObject.SetActive(true);
			this.videoBtn.gameObject.transform.DOPunchScale(this.crossBtn.gameObject.transform.localScale, 1.2f, 6, 1f);
		});
	}

	private void OnDisable()
	{
		this.crossBtn.gameObject.SetActive(false);
		this.bottomBar.anchoredPosition = this.bottomBarDefault;
		this.leftBar.anchoredPosition = this.leftBarDefaultPos;
	}

	public void WatchVideo()
	{
		if (Singleton<GameController>.Instance.adsManager.ShowRewardedVideo(1))
		{
			Singleton<GameController>.Instance.adsManager.rewardedVideoCB = new UKAdsManager.videoCallBack(this.RewardedVideoCB);
		}
		else if (Singleton<GameController>.Instance.adsManager.ShowRewardedVideo(2))
		{
			Singleton<GameController>.Instance.adsManager.rewardedVideoCB = new UKAdsManager.videoCallBack(this.RewardedVideoCB);
		}
		else
		{
			this.InstantiateVideoNotAvailable();
		}
	}

	public void RewardedVideoCB(int type)
	{
		Singleton<GameController>.Instance.storeManager.addCash(500);
		this.InstantiateCongratsWindow();
		Singleton<GameController>.Instance.eventManager.UpdateStoreUI();
	}

	public void InstantiateCongratsWindow()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.congratsWindow);
		gameObject.transform.SetParent(base.transform.parent);
		gameObject.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
		gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
		gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		gameObject.transform.localScale = Vector3.one;
		gameObject.SetActive(true);
		CongratsUIController component = gameObject.GetComponent<CongratsUIController>();
		component.reward.text = 500 + string.Empty;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void InstantiateVideoNotAvailable()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.notAvailVideoText);
		gameObject.GetComponent<AnimatedText>().desc.text = "Video Not Available !!!!";
		Vector2 v;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(base.gameObject.transform as RectTransform, UnityEngine.Input.mousePosition, null, out v);
		gameObject.transform.position = base.gameObject.transform.TransformPoint(v);
		gameObject.transform.SetParent(base.gameObject.transform);
		gameObject.SetActive(true);
	}

	public void Cancel()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public RectTransform leftBar;

	public RectTransform bottomBar;

	public RectTransform crossBtn;

	public RectTransform videoBtn;

	public Text desc;

	public GameObject notAvailVideoText;

	public GameObject congratsWindow;

	private Vector2 leftBarDefaultPos;

	private Vector2 bottomBarDefault;
}
