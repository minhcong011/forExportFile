// dnSpy decompiler from Assembly-CSharp.dll class: BaseUI
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseUI : MonoBehaviour
{
	private void Start()
	{
	}

	public void UIClicks(string action)
	{
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
		}
		if (Singleton<GameController>.Instance.soundController != null)
		{
			Singleton<GameController>.Instance.soundController.PlayButtonClick();
		}
		if (action != null)
		{
			if (!(action == "Replay"))
			{
				if (!(action == "Menu"))
				{
					if (!(action == "Resume"))
					{
						if (!(action == "Next"))
						{
							if (action == "Store")
							{
								int num = UnityEngine.Random.Range(0, 2);
								if (num == 1 && adsManager != null && !Constants.getAddsPurchasedStatus())
								{
									adsManager.showInterstitial("Store");
								}
								this.ShowStore();
							}
						}
						else
						{
							if (adsManager != null && !Constants.getAddsPurchasedStatus())
							{
								adsManager.showInterstitial("Next");
							}
							UnityEngine.Object.FindObjectOfType<UIController>().showLoading();
							SceneManager.LoadScene("GamePlayFPSMain");
							Time.timeScale = 1f;
						}
					}
					else
					{
						base.gameObject.SetActive(false);
						Time.timeScale = 1f;
					}
				}
				else
				{
					UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuNew");
					if (adsManager != null && !Constants.getAddsPurchasedStatus())
					{
						adsManager.showInterstitial("Home");
					}
				}
			}
			else
			{
				UnityEngine.Object.FindObjectOfType<UIController>().showLoading();
				if (adsManager != null && !Constants.getAddsPurchasedStatus())
				{
					adsManager.showInterstitial("Replay");
				}
				SceneManager.LoadScene("GamePlayFPSMain");
				Time.timeScale = 1f;
			}
		}
	}

	private void ShowStore()
	{
		UIController uicontroller = UnityEngine.Object.FindObjectOfType<UIController>();
		if (uicontroller != null)
		{
			uicontroller.showStore();
		}
	}

	public void showRecordedVideo()
	{
	}

	public void stopRecordingVideo()
	{
	}
}
