// dnSpy decompiler from Assembly-CSharp.dll class: GoldItemsInApp
using System;
using UnityEngine;

public class GoldItemsInApp : MonoBehaviour
{
	private void OnEnable()
	{
		OpenIABEventManager.UIEventTrigger += this.PurchasedCB;
	}

	private void OnDisable()
	{
		OpenIABEventManager.UIEventTrigger -= this.PurchasedCB;
	}

	private void Start()
	{
		this.storeManager = Singleton<GameController>.Instance.storeManager;
	}

	public void PurchasedCB(int id)
	{
		StoreInAppUI storeInAppUI = UnityEngine.Object.FindObjectOfType<StoreInAppUI>();
		if (storeInAppUI != null && storeInAppUI.gameObject.activeInHierarchy)
		{
			return;
		}
		switch (id)
		{
		case 0:
			MonoBehaviour.print("Item CB");
			this.storeManager.addCoins(this.storeManager.coinsPackItemCount[0]);
			break;
		case 1:
			MonoBehaviour.print("Item CB");
			this.storeManager.addCoins(this.storeManager.coinsPackItemCount[1]);
			break;
		case 2:
			MonoBehaviour.print("Item CB");
			this.storeManager.addCoins(this.storeManager.coinsPackItemCount[2]);
			break;
		case 3:
			this.storeManager.addCoins(this.storeManager.coinsPackItemCount[3]);
			break;
		}
		this.UpdateCoins();
	}

	private void UpdateCoins()
	{
		Singleton<GameController>.Instance.eventManager.UpdateStoreUI();
	}

	private StoreManager storeManager;
}
