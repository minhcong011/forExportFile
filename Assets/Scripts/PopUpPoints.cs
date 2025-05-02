// dnSpy decompiler from Assembly-CSharp.dll class: PopUpPoints
using System;
using UnityEngine;

public class PopUpPoints : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnTriggerEnter(Collider collision)
	{
		if (this.isCrossed)
		{
			return;
		}
		if (collision.gameObject.tag == "Player" || collision.gameObject.GetComponent<PlayerEventListener>() != null)
		{
			if (this.isEventBased)
			{
				this.isCrossed = true;
				UnityEngine.Object.FindObjectOfType<UIController>().ShowRunTimePopup(this.msg, base.gameObject);
				return;
			}
			if (this.msg != string.Empty)
			{
				UnityEngine.Object.FindObjectOfType<UIController>().ShowRunTimePopup(this.msg);
			}
			else
			{
				UnityEngine.Object.FindObjectOfType<UIController>().ShowRunTimePopup(3);
			}
			this.isCrossed = true;
		}
	}

	public void EventreceiverCB()
	{
		UnityEngine.Debug.Log("cabbsabdbsdc");
		int num = this.id;
		if (num != 1)
		{
			if (num != 2)
			{
				if (num == 3)
				{
					UnityEngine.Object.FindObjectOfType<UIController>().WeaponSwitcher(3);
				}
			}
			else
			{
				UnityEngine.Object.FindObjectOfType<UIController>().WeaponSwitcher(2);
			}
		}
		else
		{
			UnityEngine.Object.FindObjectOfType<UIController>().WeaponSwitcher(1);
		}
	}

	public bool isCrossed;

	public string msg = string.Empty;

	public bool isEventBased;

	public int id;
}
