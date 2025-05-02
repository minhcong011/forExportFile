// dnSpy decompiler from Assembly-CSharp.dll class: RetryUIController
using System;
using UnityEngine;
using UnityEngine.UI;

public class RetryUIController : MonoBehaviour
{
	private void Start()
	{
		int totalCoinsCount = Singleton<GameController>.Instance.storeManager.GetTotalCoinsCount();
		if (totalCoinsCount < 1)
		{
		}
	}

	private void OnEnable()
	{
		this.windowActiveTime = Time.time;
		int gameOverMode = Singleton<GameController>.Instance.gameSceneController.gameOverMode;
		if (gameOverMode == 1)
		{
			this.detail.text = "Out of Health, Enemies attacked on you badly";
		}
		else if (gameOverMode == 2)
		{
			this.detail.text = "Times Up !!!";
		}
		this.isTimerOn = true;
		this.timerSprite.fillAmount = 1f;
	}

	private void OnDisable()
	{
		this.windowActiveTime = Time.time;
		int gameOverMode = Singleton<GameController>.Instance.gameSceneController.gameOverMode;
		if (gameOverMode == 1)
		{
			this.detail.text = "Out of Health, Enemies attacked on you badly";
		}
		else if (gameOverMode == 2)
		{
			this.detail.text = "Time Over !!!";
		}
		this.isTimerOn = false;
	}

	private void Update()
	{
		if (this.isTimerOn)
		{
			this.timerSprite.fillAmount -= Time.deltaTime * 0.2f;
			if (this.timerSprite.fillAmount <= 0f)
			{
				this.isTimerOn = false;
				this.cancel();
			}
		}
	}

	public void retryPressed()
	{
		this.ContinueWithCashOrGold();
	}

	public void cancel()
	{
		Singleton<GameController>.Instance.gameSceneController.FinalGameOver();
		base.gameObject.SetActive(false);
	}

	public void ContinueWithCashOrGold()
	{
		int num = 500 + this.retryCount * 500;
		if (Singleton<GameController>.Instance.storeManager.GetTotalCashCount() >= num)
		{
			Singleton<GameController>.Instance.gameSceneController.retryGame(Time.time - this.windowActiveTime);
			Singleton<GameController>.Instance.storeManager.consumeCash(num);
			base.gameObject.SetActive(false);
			return;
		}
		UnityEngine.Debug.Log("Not enough cash");
		this.InstantiateNotEnoughPanel();
	}

	public void ContinueWithVideo()
	{
		Singleton<GameController>.Instance.adsManager.rewardedVideoCB = new UKAdsManager.videoCallBack(this.RewardedVideoCallBack);
		if (!Singleton<GameController>.Instance.adsManager.ShowRewardedVideo(1))
		{
			if (!Singleton<GameController>.Instance.adsManager.ShowRewardedVideo(2))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("NotEnoughAnimatedText")) as GameObject;
				gameObject.transform.SetParent(base.gameObject.transform);
				gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
				gameObject.transform.localScale = Vector3.one;
				gameObject.GetComponent<AnimatedText>().desc.text = "Video Not Available!!!";
				Vector2 v;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(base.gameObject.transform as RectTransform, UnityEngine.Input.mousePosition, null, out v);
				gameObject.transform.position = base.gameObject.transform.TransformPoint(v);
				gameObject.SetActive(true);
			}
		}
	}

	public void RewardedVideoCallBack(int type)
	{
		Singleton<GameController>.Instance.gameSceneController.retryGame(Time.time - this.windowActiveTime);
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
		}
		base.gameObject.SetActive(false);
	}

	public void InstantiateNotEnoughPanel()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("NotEnoughCashUI")) as GameObject;
		gameObject.transform.SetParent(base.gameObject.transform);
		gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
		gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
	}

	private float windowActiveTime;

	public Text detail;

	public Image timerSprite;

	private bool isTimerOn;

	public Button watchVideo;

	public GameObject retryBtn;

	private int retryCount;
}
