// dnSpy decompiler from Assembly-CSharp.dll class: RunTimePopHandler
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RunTimePopHandler : MonoBehaviour
{
	private void OnEnable()
	{
		this.topBar.DOAnchorPos(new Vector2(this.topBar.anchoredPosition.x, -125f), 0.6f, false).SetEase(Ease.OutBounce).OnComplete(delegate
		{
			this.text.gameObject.SetActive(true);
		});
	}

	private void OnDisable()
	{
		this.text.gameObject.SetActive(false);
	}

	public void showPopUp(int popUpId)
	{
		this.btn.SetActive(false);
		base.CancelInvoke("disablePopup");
		base.CancelInvoke("Disable");
		if (popUpId != 1)
		{
			if (popUpId != 2)
			{
				if (popUpId == 3)
				{
					this.text.text = "weldone larkay!!!";
					base.Invoke("disablePopup", 1.2f);
				}
			}
			else
			{
				this.text.text = "You are Dead!!!";
			}
		}
		else
		{
			this.text.text = "Great Work, You Won!!!";
			int num = UnityEngine.Random.Range(0, this.winStrings.Length);
			this.text.text = this.winStrings[num];
		}
	}

	public void showPopUp(string msg)
	{
		this.btn.SetActive(false);
		this.text.text = msg;
		base.CancelInvoke("disablePopup");
		base.CancelInvoke("Disable");
		base.Invoke("disablePopup", 2.2f);
	}

	public void showPopUp(string msg, GameObject g)
	{
		base.CancelInvoke("disablePopup");
		base.CancelInvoke("Disable");
		MonoBehaviour.print("timescale 0");
		this.btn.SetActive(true);
		this.text.text = msg;
		this.sendingObj = g;
		Time.timeScale = 0f;
	}

	private void disablePopup()
	{
		Time.timeScale = 1f;
		base.Invoke("Disable", 1f);
	}

	private void Disable()
	{
		base.gameObject.SetActive(false);
		this.btn.SetActive(false);
	}

	public void OnClick()
	{
		if (this.sendingObj != null)
		{
			this.sendingObj.SendMessage("EventreceiverCB");
		}
		this.sendingObj = null;
		this.disablePopup();
		Time.timeScale = 1f;
	}

	public TweenPosition pos;

	public Text text;

	public string[] winStrings;

	public GameObject btn;

	private GameObject sendingObj;

	public RectTransform bottomBtns;

	public RectTransform topBar;
}
