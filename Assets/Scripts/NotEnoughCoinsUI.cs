// dnSpy decompiler from Assembly-CSharp.dll class: NotEnoughCoinsUI
using System;
using UnityEngine;
using UnityEngine.UI;

public class NotEnoughCoinsUI : MonoBehaviour
{
	private void Start()
	{
		if (this.isAddAvailable())
		{
			this.videoBtn.color = Color.white;
		}
		else
		{
			this.videoBtn.color = Color.grey;
		}
	}

	private void OnDisable()
	{
		this.congratsWindow.SetActive(false);
		this.refStoreUI.UpdateUI();
	}

	private bool isAddAvailable()
	{
		return true;
	}

	public void WatchVideo()
	{
		if (!this.isAddAvailable())
		{
			return;
		}
	}

	public void videoCompleteCB()
	{
		if (UnityEngine.Object.FindObjectOfType<StoreManager>() == null)
		{
			UnityEngine.Object.FindObjectOfType<StoreManager>().addCoins(1);
		}
		this.congratsWindow.SetActive(true);
		this.refStoreUI.UpdateUI();
	}

	public void OkBtnClicked()
	{
		this.refStoreUI.BackClicked();
	}

	public Image videoBtn;

	public StoreUIController refStoreUI;

	public GameObject congratsWindow;
}
