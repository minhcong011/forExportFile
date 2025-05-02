// dnSpy decompiler from Assembly-CSharp.dll class: StoreInAppUI
using System;
using System.Collections.Generic;
using UnityEngine;

public class StoreInAppUI : MonoBehaviour
{
	private void Awake()
	{
		this.storeManager = Singleton<GameController>.Instance.storeManager;
		this.setData();
	}

	private void OnEnable()
	{
		OpenIABEventManager.UIEventTrigger += this.PurchasedCB;
		this.currentScreenId = Singleton<GameController>.Instance.inAppId;
		this.setItems();
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
			adsManager.LogScreen("InApp Menu");
		}
		Singleton<GameController>.Instance.inAppId = 0;
	}

	private void OnDisable()
	{
		OpenIABEventManager.UIEventTrigger -= this.PurchasedCB;
		this.InappScreens[this.currentScreenId].SetActive(false);
		this.currentScreenId = this.defaultId;
	}

	private void setData()
	{
		int[] cashPackItemCount = this.storeManager.cashPackItemCount;
		int[] cashPackPrice = this.storeManager.cashPackPrice;
		for (int i = 0; i < cashPackItemCount.Length; i++)
		{
			this.cashItems[i].setInitials(cashPackPrice[i], cashPackItemCount[i]);
		}
		int[] coinsPackItemCount = this.storeManager.coinsPackItemCount;
		int[] coinsPackPrice = this.storeManager.coinsPackPrice;
		for (int j = 0; j < coinsPackItemCount.Length; j++)
		{
			this.goldItems[j].setInitials(coinsPackPrice[j], coinsPackItemCount[j]);
		}
	}

	private void setItems()
	{
		this.InappScreens[this.currentScreenId].SetActive(true);
		for (int i = 0; i < this.selectedPanel.Length; i++)
		{
			this.selectedPanel[i].SetActive(false);
		}
		this.selectedPanel[this.currentScreenId].SetActive(true);
	}

	public void MainScreenItemClicked(int id)
	{
		this.InappScreens[this.currentScreenId].SetActive(false);
		this.currentScreenId = id;
		this.InappScreens[this.currentScreenId].SetActive(true);
		if (Singleton<GameController>.Instance.soundController != null)
		{
			Singleton<GameController>.Instance.soundController.PlayButtonClick();
		}
		for (int i = 0; i < this.selectedPanel.Length; i++)
		{
			this.selectedPanel[i].SetActive(false);
		}
		this.selectedPanel[this.currentScreenId].SetActive(true);
	}

	public void PurchasedCB(int id)
	{
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		switch (id)
		{
		case 0:
			MonoBehaviour.print("Item CB");
			this.storeManager.addCoins(this.storeManager.coinsPackItemCount[0]);
			if (adsManager != null)
			{
			}
			break;
		case 1:
			MonoBehaviour.print("Item CB");
			this.storeManager.addCoins(this.storeManager.coinsPackItemCount[1]);
			if (adsManager != null)
			{
			}
			break;
		case 2:
			MonoBehaviour.print("Item CB");
			this.storeManager.addCoins(this.storeManager.coinsPackItemCount[2]);
			if (adsManager != null)
			{
			}
			break;
		case 3:
			this.storeManager.addCoins(this.storeManager.coinsPackItemCount[3]);
			if (adsManager != null)
			{
			}
			break;
		case 5:
			this.storeManager.addCash(this.storeManager.cashPackItemCount[0]);
			if (adsManager != null)
			{
			}
			break;
		case 6:
			this.storeManager.addCash(this.storeManager.cashPackItemCount[1]);
			if (adsManager != null)
			{
			}
			break;
		case 7:
			this.storeManager.addCash(this.storeManager.cashPackItemCount[2]);
			if (adsManager != null)
			{
			}
			break;
		case 8:
			this.storeManager.addCash(this.storeManager.cashPackItemCount[3]);
			if (adsManager != null)
			{
			}
			break;
		}
		this.UpdateCoins();
	}

	private void UpdateCoins()
	{
		Singleton<GameController>.Instance.eventManager.UpdateStoreUI();
	}

	public void ButtonClick(string act)
	{
		OpenIABPurchaseHandler openIABPurchaseHandler = UnityEngine.Object.FindObjectOfType<OpenIABPurchaseHandler>();
		switch (act)
		{
		case "CoinsPack1":
			if (openIABPurchaseHandler != null)
			{
				openIABPurchaseHandler.PurchasePressed(0);
			}
			break;
		case "CoinsPack2":
			if (openIABPurchaseHandler != null)
			{
				openIABPurchaseHandler.PurchasePressed(1);
			}
			break;
		case "CoinsPack3":
			if (openIABPurchaseHandler != null)
			{
				openIABPurchaseHandler.PurchasePressed(2);
			}
			break;
		case "CoinsPack4":
			if (openIABPurchaseHandler != null)
			{
				openIABPurchaseHandler.PurchasePressed(3);
			}
			break;
		case "CashPack1":
			if (openIABPurchaseHandler != null)
			{
				openIABPurchaseHandler.PurchasePressed(5);
			}
			break;
		case "CashPack2":
			if (openIABPurchaseHandler != null)
			{
				openIABPurchaseHandler.PurchasePressed(6);
			}
			break;
		case "CashPack3":
			if (openIABPurchaseHandler != null)
			{
				openIABPurchaseHandler.PurchasePressed(7);
			}
			break;
		case "CashPack4":
			if (openIABPurchaseHandler != null)
			{
				openIABPurchaseHandler.PurchasePressed(8);
			}
			break;
		}
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
	}

	public StoreUIController refStoreUI;

	private StoreManager storeManager;

	public GameObject[] InappScreens;

	public GameObject[] selectedPanel;

	public List<InAppItemUI> cashItems;

	public List<InAppItemUI> goldItems;

	public int currentScreenId;

	public int defaultId;
}
