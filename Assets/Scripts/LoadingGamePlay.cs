// dnSpy decompiler from Assembly-CSharp.dll class: LoadingGamePlay
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingGamePlay : MonoBehaviour
{
	private void Start()
	{
	}

	public void LoadScene()
	{
		UnityEngine.Object.FindObjectOfType<UIScreensHandler>().setScreen(5);
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
			adsManager.showInterstitial("LoadingGamePlay");
			adsManager.HideBanner("AdMob");
		}
		base.Invoke("LoadNextScene", 1f);
	}

	public void LoadNextScene()
	{
		SceneManager.LoadScene("GamePlayFPSMain");
	}

	public IEnumerator loadGamePlay()
	{
		yield return new WaitForSeconds(0.1f);
		yield break;
	}

	private void FixedUpdate()
	{
		if (!this.check)
		{
			return;
		}
	}

	public Slider loadingSlider;

	private AsyncOperation async;

	private bool check;
}
