// dnSpy decompiler from Assembly-CSharp.dll class: OpenIABPurchaseHandlerDummy
using System;
using System.Collections.Generic;
using OnePF;
using UnityEngine;

public class OpenIABPurchaseHandlerDummy : MonoBehaviour
{
	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Start()
	{
		OpenIAB.mapSku("sku_removead", OpenIAB_Android.STORE_GOOGLE, "sku_removead");
		OpenIAB.mapSku("sku_coinspack1", OpenIAB_Android.STORE_GOOGLE, "sku_coinspack1");
		OpenIAB.mapSku("sku_coinspack2", OpenIAB_Android.STORE_GOOGLE, "sku_coinspack2");
		OpenIAB.mapSku("sku_coinspack3", OpenIAB_Android.STORE_GOOGLE, "sku_coinspack3");
		OpenIAB.mapSku("sku_coinspack4", OpenIAB_Android.STORE_GOOGLE, "sku_coinspack4");
		OpenIAB.mapSku("sku_cashpack1", OpenIAB_Android.STORE_GOOGLE, "sku_cashpack1");
		OpenIAB.mapSku("sku_cashpack2", OpenIAB_Android.STORE_GOOGLE, "sku_cashpack2");
		OpenIAB.mapSku("sku_cashpack3", OpenIAB_Android.STORE_GOOGLE, "sku_cashpack3");
		OpenIAB.mapSku("sku_cashpack4", OpenIAB_Android.STORE_GOOGLE, "sku_cashpack4");
		this.init();
	}

	private void init()
	{
		OpenIAB.init(new Options
		{
			verifyMode = OptionsVerifyMode.VERIFY_SKIP,
			storeKeys = new Dictionary<string, string>
			{
				{
					OpenIAB_Android.STORE_GOOGLE,
					this.public_key
				}
			},
			prefferedStoreNames = new string[]
			{
				OpenIAB_Android.STORE_GOOGLE
			}
		});
	}

	public void purchaseRemoveAdds()
	{
		OpenIAB.purchaseProduct("sku_removead", string.Empty);
	}

	public void PurchasePressed(int id)
	{
		MonoBehaviour.print("pressed " + id);
		switch (id)
		{
		case 0:
			OpenIAB.purchaseProduct("sku_coinspack1", string.Empty);
			break;
		case 1:
			OpenIAB.purchaseProduct("sku_coinspack2", string.Empty);
			break;
		case 2:
			OpenIAB.purchaseProduct("sku_coinspack3", string.Empty);
			break;
		case 3:
			OpenIAB.purchaseProduct("sku_coinspack4", string.Empty);
			break;
		case 4:
			this.purchaseRemoveAdds();
			break;
		case 5:
			OpenIAB.purchaseProduct("sku_cashpack1", string.Empty);
			break;
		case 6:
			OpenIAB.purchaseProduct("sku_cashpack2", string.Empty);
			break;
		case 7:
			OpenIAB.purchaseProduct("sku_cashpack3", string.Empty);
			break;
		case 8:
			OpenIAB.purchaseProduct("sku_cashpack4", string.Empty);
			break;
		}
	}

	private void OnGUI()
	{
	}

	private void billingSupportedEvent()
	{
		UnityEngine.Debug.Log("billingSupportedEvent");
		OpenIAB.queryInventory();
	}

	private void billingNotSupportedEvent(string error)
	{
		UnityEngine.Debug.Log("billingNotSupportedEvent: " + error);
	}

	private void queryInventorySucceededEvent(Inventory inventory)
	{
		UnityEngine.Debug.Log("queryInventorySucceededEvent: " + inventory);
		if (inventory != null)
		{
			this._inventory = inventory;
		}
		if (this._inventory != null && this._inventory.HasPurchase("android.test.purchased"))
		{
			OpenIAB.consumeProduct(this._inventory.GetPurchase("android.test.purchased"));
		}
		if (this._inventory != null && this._inventory.HasPurchase("sku_removead"))
		{
			OpenIAB.consumeProduct(this._inventory.GetPurchase("sku_removead"));
		}
		if (this._inventory != null && this._inventory.HasPurchase("sku_coinspack1"))
		{
			OpenIAB.consumeProduct(this._inventory.GetPurchase("sku_coinspack1"));
		}
		if (this._inventory != null && this._inventory.HasPurchase("sku_coinspack2"))
		{
			OpenIAB.consumeProduct(this._inventory.GetPurchase("sku_coinspack2"));
		}
		if (this._inventory != null && this._inventory.HasPurchase("sku_coinspack3"))
		{
			OpenIAB.consumeProduct(this._inventory.GetPurchase("sku_coinspack3"));
		}
		if (this._inventory != null && this._inventory.HasPurchase("sku_coinspack4"))
		{
			OpenIAB.consumeProduct(this._inventory.GetPurchase("sku_coinspack4"));
		}
		if (this._inventory != null && this._inventory.HasPurchase("sku_cashpack1"))
		{
			OpenIAB.consumeProduct(this._inventory.GetPurchase("sku_cashpack1"));
		}
		if (this._inventory != null && this._inventory.HasPurchase("sku_cashpack2"))
		{
			OpenIAB.consumeProduct(this._inventory.GetPurchase("sku_cashpack2"));
		}
		if (this._inventory != null && this._inventory.HasPurchase("sku_cashpack3"))
		{
			OpenIAB.consumeProduct(this._inventory.GetPurchase("sku_cashpack3"));
		}
		if (this._inventory != null && this._inventory.HasPurchase("sku_cashpack4"))
		{
			OpenIAB.consumeProduct(this._inventory.GetPurchase("sku_cashpack4"));
		}
	}

	private void queryInventoryFailedEvent(string error)
	{
		UnityEngine.Debug.Log("queryInventoryFailedEvent: " + error);
	}

	private void purchaseSucceededEvent(Purchase purchase)
	{
		UnityEngine.Debug.Log("purchaseSucceededEvent: " + purchase);
		string sku = purchase.Sku;
		switch (sku)
		{
		case "sku_removead":
			OpenIAB.EventManager.SendMessage("PurchaseSuccesCallBackToUI", 4, SendMessageOptions.DontRequireReceiver);
			goto IL_209;
		case "sku_coinspack1":
			OpenIAB.EventManager.SendMessage("PurchaseSuccesCallBackToUI", 0, SendMessageOptions.DontRequireReceiver);
			goto IL_209;
		case "sku_coinspack2":
			OpenIAB.EventManager.SendMessage("PurchaseSuccesCallBackToUI", 1, SendMessageOptions.DontRequireReceiver);
			goto IL_209;
		case "sku_coinspack3":
			OpenIAB.EventManager.SendMessage("PurchaseSuccesCallBackToUI", 2, SendMessageOptions.DontRequireReceiver);
			goto IL_209;
		case "sku_coinspack4":
			OpenIAB.EventManager.SendMessage("PurchaseSuccesCallBackToUI", 3, SendMessageOptions.DontRequireReceiver);
			goto IL_209;
		case "sku_cashpack1":
			OpenIAB.EventManager.SendMessage("PurchaseSuccesCallBackToUI", 5, SendMessageOptions.DontRequireReceiver);
			goto IL_209;
		case "sku_cashpack2":
			OpenIAB.EventManager.SendMessage("PurchaseSuccesCallBackToUI", 6, SendMessageOptions.DontRequireReceiver);
			goto IL_209;
		case "sku_cashpack3":
			OpenIAB.EventManager.SendMessage("PurchaseSuccesCallBackToUI", 7, SendMessageOptions.DontRequireReceiver);
			goto IL_209;
		case "sku_cashpack4":
			OpenIAB.EventManager.SendMessage("PurchaseSuccesCallBackToUI", 8, SendMessageOptions.DontRequireReceiver);
			goto IL_209;
		case "android.test.purchased":
			goto IL_209;
		}
		UnityEngine.Debug.LogWarning("Unknown SKU: " + purchase.Sku);
		IL_209:
		OpenIAB.queryInventory();
		UnityEngine.Debug.Log("purchaseSucceededEvent: " + purchase);
	}

	private void purchaseFailedEvent(string error)
	{
		UnityEngine.Debug.Log("purchaseFailedEvent: " + error);
	}

	private void consumePurchaseSucceededEvent(Purchase purchase)
	{
		UnityEngine.Debug.Log("consumePurchaseSucceededEvent: " + purchase);
	}

	private void consumePurchaseFailedEvent(string error)
	{
		UnityEngine.Debug.Log("consumePurchaseFailedEvent: " + error);
	}

	private const string SKU_RemoveAdd = "sku_removead";

	private const string SKU_COINSPACK1 = "sku_coinspack1";

	private const string SKU_COINSPACK2 = "sku_coinspack2";

	private const string SKU_COINSPACK3 = "sku_coinspack3";

	private const string SKU_COINSPACK4 = "sku_coinspack4";

	private const string SKU_CASHPACK1 = "sku_cashpack1";

	private const string SKU_CASHPACK2 = "sku_cashpack2";

	private const string SKU_CASHPACK3 = "sku_cashpack3";

	private const string SKU_CASHPACK4 = "sku_cashpack4";

	private const string SKU_UNLIMITED = "sku_unlimitedCoins";

	private Inventory _inventory;

	public string public_key = string.Empty;
}
