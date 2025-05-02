// dnSpy decompiler from Assembly-CSharp.dll class: UIPopUpManager
using System;
using UnityEngine;

public class UIPopUpManager : MonoBehaviour
{
	private void Awake()
	{
		Singleton<GameController>.Instance.uiPopUpManager = this;
	}

	private void Start()
	{
	}

	public void ShowPauseUI()
	{
		Time.timeScale = 0f;
	}

	public void ShowLevelCompleteUI()
	{
		Time.timeScale = 0f;
	}

	public void ShowPopUp(string name, PopUpBase basePopUp = null)
	{
		if (basePopUp != null)
		{
			basePopUp.callback = new Action<string>(this.ActionCallBack);
		}
		if (name != null)
		{
			if (!(name == "Pause"))
			{
				if (!(name == "LevelComplete"))
				{
					if (!(name == "SmallPopUp"))
					{
						if (!(name == "MediumPopUp"))
						{
							if (name == "LargePopUp")
							{
								PopUpBaseUI component = this.largePopUpBar.GetComponent<PopUpBaseUI>();
								if (component != null)
								{
									component.setBase(basePopUp);
									this.largePopUpBar.SetActive(true);
								}
							}
						}
						else
						{
							PopUpBaseUI component2 = this.mediumPopUpBar.GetComponent<PopUpBaseUI>();
							if (component2 != null)
							{
								component2.setBase(basePopUp);
								this.mediumPopUpBar.SetActive(true);
							}
						}
					}
					else
					{
						PopUpBaseUI component3 = this.smallPopUpBar.GetComponent<PopUpBaseUI>();
						if (component3 != null)
						{
							component3.setBase(basePopUp);
							this.smallPopUpBar.SetActive(true);
						}
					}
				}
				else
				{
					this.ShowLevelCompleteUI();
				}
			}
			else
			{
				this.ShowPauseUI();
			}
		}
	}

	public void ActionCallBack(string action)
	{
		UnityEngine.Debug.Log("Action" + action);
	}

	public GameObject InitAndGetRewardPopup()
	{
		UnityEngine.Debug.Log(" in init popup");
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("CongratsScreen")) as GameObject;
		gameObject.transform.SetParent(base.gameObject.transform);
		gameObject.transform.SetSiblingIndex(base.transform.childCount - 1);
		gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
		gameObject.transform.localScale = Vector3.one;
		gameObject.SetActive(true);
		return gameObject;
	}

	public GameObject InitNotEnoughCashPopUp()
	{
		UnityEngine.Debug.Log(" in init popup");
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("NotEnoughCashUI")) as GameObject;
		gameObject.transform.SetParent(base.gameObject.transform);
		gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
		gameObject.transform.localScale = Vector3.one;
		gameObject.SetActive(true);
		return gameObject;
	}

	public GameObject NotEnoughText(string desc = "Not Available")
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("NotEnoughAnimatedText")) as GameObject;
		gameObject.transform.SetParent(base.gameObject.transform);
		gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
		gameObject.transform.localPosition = Vector3.zero;
		Vector2 v;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(base.gameObject.transform as RectTransform, UnityEngine.Input.mousePosition, Camera.main, out v);
		gameObject.transform.position = base.gameObject.transform.TransformPoint(v);
		gameObject.transform.localScale = Vector3.one;
		AnimatedText component = gameObject.GetComponent<AnimatedText>();
		component.desc.text = desc;
		gameObject.SetActive(true);
		return gameObject;
	}

	public GameObject levelClearPopUp;

	public GameObject pausePopUp;

	public GameObject failedPopUp;

	public GameObject smallPopUpBar;

	public GameObject mediumPopUpBar;

	public GameObject largePopUpBar;

	public PopUpBase refPopUp;
}
