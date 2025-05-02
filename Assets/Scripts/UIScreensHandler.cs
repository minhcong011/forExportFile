// dnSpy decompiler from Assembly-CSharp.dll class: UIScreensHandler
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreensHandler : MonoBehaviour
{
	private void Start()
	{
		this.allScreensCG = new List<CanvasGroup>();
		Singleton<GameController>.Instance.soundController.PlayMainMenuMusic();
	}

	public void setScreen(int id)
	{
		this.screenFader.gameObject.SetActive(true);
		if (this.screenId == 11)
		{
			this.allScreens[this.screenId].SetActive(false);
			this.allScreens[this.previousScreenId].SetActive(true);
			this.screenId = this.previousScreenId;
		}
		else
		{
			this.previousScreenId = this.screenId;
			this.allScreens[this.screenId].SetActive(false);
			this.screenId = id;
			this.allScreens[this.screenId].SetActive(true);
		}
		UnityEngine.Object.FindObjectOfType<TopBarUiController>().showBtnAccordingToScreen(this.screenId);
	}

	public void BackPressed(int id)
	{
		if (this.screenId == 0)
		{
			this.setScreen(4);
		}
		else if (this.screenId == 2)
		{
			this.setScreen(0);
		}
		else if (this.screenId == 1)
		{
			this.setScreen(0);
		}
		else if (this.screenId == 6)
		{
			this.setScreen(2);
		}
		else
		{
			this.setScreen(id);
		}
	}

	private IEnumerator manageAlpha(int id)
	{
		for (int i = 0; i < 20; i++)
		{
			this.allScreensCG[this.screenId].alpha += 0.05f;
			yield return new WaitForSeconds(0.05f);
		}
		this.allScreens[this.screenId].SetActive(false);
		yield return new WaitForSeconds(0.1f);
		this.screenId = id;
		this.allScreensCG[this.screenId].alpha = 1f;
		this.allScreens[this.screenId].SetActive(true);
		yield break;
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			Singleton<GameController>.Instance.soundController.PlayButtonClick();
			switch (this.screenId)
			{
			case 0:
				this.setScreen(4);
				break;
			case 1:
				this.setScreen(6);
				break;
			case 2:
				this.setScreen(0);
				break;
			case 3:
				this.setScreen(0);
				break;
			case 4:
				this.setScreen(0);
				break;
			case 6:
				this.setScreen(2);
				break;
			case 7:
				this.setScreen(0);
				break;
			case 9:
			case 10:
				this.setScreen(0);
				break;
			case 11:
			case 13:
				this.setScreen(0);
				break;
			}
		}
	}

	public List<GameObject> allScreens;

	public int screenId;

	public int previousScreenId;

	private List<CanvasGroup> allScreensCG;

	public ScreenFader screenFader;
}
