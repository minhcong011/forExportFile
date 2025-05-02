// dnSpy decompiler from Assembly-CSharp.dll class: LoadingGameUI
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingGameUI : MonoBehaviour
{
	private void Start()
	{
		this.startTime = Time.time;
		this.loadingBar.fillAmount = 0f;
	}

	private IEnumerator load()
	{
		yield return new WaitForSeconds(0.3f);
		if (this.screenName == "Loading")
		{
		}
		yield break;
	}

	private void OnDisable()
	{
		this.loadingBar.fillAmount = 0f;
	}

	public void StartSplash()
	{
		this.startTime = Time.time;
		this.isShowVideo = false;
	}

	private void Update()
	{
		if (this.isShowVideo)
		{
			return;
		}
		this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
		float num = this.deltaTime * 1000f;
		float num2 = 1f / this.deltaTime;
		if (Time.time - this.startTime < this.screenTime)
		{
			this.loadingBar.fillAmount += 1f / (this.screenTime * num2);
		}
		else
		{
			this.loadingBar.fillAmount = 1f;
			if (this.screenName == "Loading")
			{
				SceneManager.LoadScene("MainMenuNew");
			}
		}
	}

	public Image loadingBar;

	public string screenName = "Loading";

	private float deltaTime;

	public float screenTime = 10f;

	public float startTime;

	public bool isShowVideo;
}
