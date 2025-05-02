// dnSpy decompiler from Assembly-CSharp.dll class: GiftUIController
using System;
using UnityEngine;
using UnityEngine.UI;

public class GiftUIController : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnDisable()
	{
		this.defaultId = 0;
	}

	public void ShowGift(int id)
	{
		this.videoBtn.gameObject.SetActive(false);
		this.isVideoBtn = false;
		this.okBtn.gameObject.SetActive(true);
		StoreManager storeManager = Singleton<GameController>.Instance.storeManager;
		this.withVideoBtnsPanel.SetActive(false);
		this.normalGiftBtnsPanel.SetActive(true);
		switch (id)
		{
		case 1:
			this.text.text = "Congratulation, you have been awarded with an extra Medipack";
			storeManager.GiveMediPack(1);
			break;
		case 2:
			this.text.text = "Congratulation, you have been awarded with an extra Medipack";
			storeManager.GiveMediPack(1);
			break;
		case 3:
			this.text.text = "Congratulation, you have been awarded with 500 Cash";
			storeManager.addCash(500);
			break;
		case 4:
			this.text.text = "Congratulation, Sniper Unlocked!!";
			break;
		case 5:
		{
			int num = UnityEngine.Random.Range(1, 4);
			this.text.text = "Congratulation, you have been awarded with " + num + " extra Gernades";
			storeManager.GiveGernades(num);
			break;
		}
		case 6:
			this.text.text = "Congratulation, Sniper Unlocked!!";
			break;
		case 7:
			this.text.text = "Congratulation, Rocket Launcher Unlocked!!";
			break;
		}
		base.gameObject.SetActive(true);
	}

	public void ShowGiftAfterVideo(int id)
	{
		this.defaultId = id;
		StoreManager storeManager = Singleton<GameController>.Instance.storeManager;
		base.gameObject.SetActive(true);
	}

	private void UpdateUIForGift()
	{
	}

	public void OkPressed()
	{
		base.gameObject.SetActive(false);
		UnityEngine.Object.FindObjectOfType<UIController>().GiftCB();
	}

	public void WatchVideo()
	{
		StoreManager storeManager = Singleton<GameController>.Instance.storeManager;
	}

	public Text text;

	private bool isVideoBtn;

	public Image videoBtn;

	public Image okBtn;

	public GameObject withVideoBtnsPanel;

	public GameObject normalGiftBtnsPanel;

	private int defaultId;
}
