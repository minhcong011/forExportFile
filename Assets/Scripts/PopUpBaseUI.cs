// dnSpy decompiler from Assembly-CSharp.dll class: PopUpBaseUI
using System;
using UnityEngine;
using UnityEngine.UI;

public class PopUpBaseUI : MonoBehaviour
{
	private void OnEnable()
	{
		if (this.tween != null)
		{
			this.tween.Play(true);
		}
		if (this.refPopUpBase != null && this.refPopUpBase.screenStayTime > 0f)
		{
			base.CancelInvoke("disableSelf");
			base.Invoke("disableSelf", this.refPopUpBase.screenStayTime);
		}
	}

	private void disableSelf()
	{
		if (this.tween != null)
		{
			this.tween.Play(false);
		}
		base.Invoke("Disable", 1f);
	}

	private void Disable()
	{
		base.gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		this.refPopUpBase = null;
		this.title.text = string.Empty;
		this.description.text = string.Empty;
	}

	public void setBase(PopUpBase refBase)
	{
		this.refPopUpBase = refBase;
		if (this.refPopUpBase.title == string.Empty)
		{
			this.title.gameObject.SetActive(false);
		}
		else
		{
			this.title.gameObject.SetActive(true);
			this.title.text = this.refPopUpBase.title;
		}
		if (this.refPopUpBase.description == string.Empty)
		{
			this.description.gameObject.SetActive(false);
		}
		else
		{
			this.description.gameObject.SetActive(true);
			this.description.text = this.refPopUpBase.description;
		}
		if (this.refPopUpBase != null && this.refPopUpBase.isCancelBtn && this.cancelBtn)
		{
			this.cancelBtn.SetActive(true);
		}
		else if (this.cancelBtn)
		{
			this.cancelBtn.SetActive(false);
		}
	}

	public void ButtonAction(string btn)
	{
		if (this.refPopUpBase != null)
		{
			this.refPopUpBase.callback(btn);
		}
		if (btn != null)
		{
			if (!(btn == "Ok"))
			{
				if (btn == "Cancel")
				{
					this.disableSelf();
				}
			}
			else
			{
				this.disableSelf();
			}
		}
	}

	public PopUpBase refPopUpBase;

	public Text title;

	public Text description;

	public GameObject cancelBtn;

	public GameObject okBtn;

	public TweenPosition tween;
}
