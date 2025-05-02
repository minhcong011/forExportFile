// dnSpy decompiler from Assembly-CSharp.dll class: OpenIABPurchaseHandler
using System;
using System.Collections.Generic;
using OnePF;
using UnityEngine;

public class OpenIABPurchaseHandler : MonoBehaviour
{
	private void OnEnable()
	{
		OpenIABEventManager.billingSupportedEvent += this.billingSupportedEvent;
		OpenIABEventManager.billingNotSupportedEvent += this.billingNotSupportedEvent;
		OpenIABEventManager.queryInventorySucceededEvent += this.queryInventorySucceededEvent;
		OpenIABEventManager.queryInventoryFailedEvent += this.queryInventoryFailedEvent;
		OpenIABEventManager.purchaseSucceededEvent += this.purchaseSucceededEvent;
		OpenIABEventManager.purchaseFailedEvent += this.purchaseFailedEvent;
		OpenIABEventManager.consumePurchaseSucceededEvent += this.consumePurchaseSucceededEvent;
		OpenIABEventManager.consumePurchaseFailedEvent += this.consumePurchaseFailedEvent;
		OpenIABEventManager.restoreSucceededEvent += this.restoreSuccededEvent;
		OpenIABEventManager.restoreFailedEvent += this.restoreFailedEvent;
		OpenIABEventManager.transactionRestoredEvent += this.transactionRestoredSuccededEvent;
	}

	private void OnDisable()
	{
		OpenIABEventManager.billingSupportedEvent -= this.billingSupportedEvent;
		OpenIABEventManager.billingNotSupportedEvent -= this.billingNotSupportedEvent;
		OpenIABEventManager.queryInventorySucceededEvent -= this.queryInventorySucceededEvent;
		OpenIABEventManager.queryInventoryFailedEvent -= this.queryInventoryFailedEvent;
		OpenIABEventManager.purchaseSucceededEvent -= this.purchaseSucceededEvent;
		OpenIABEventManager.purchaseFailedEvent -= this.purchaseFailedEvent;
		OpenIABEventManager.consumePurchaseSucceededEvent -= this.consumePurchaseSucceededEvent;
		OpenIABEventManager.consumePurchaseFailedEvent -= this.consumePurchaseFailedEvent;
		OpenIABEventManager.restoreSucceededEvent -= this.restoreSuccededEvent;
		OpenIABEventManager.restoreFailedEvent -= this.restoreFailedEvent;
		OpenIABEventManager.transactionRestoredEvent -= this.transactionRestoredSuccededEvent;
	}

	private void Start()
	{
		OpenIAB.mapSku("sku_removead", OpenIAB_Android.STORE_GOOGLE, "sku_removead");
		this.init();
	}

	private bool Button(string text)
	{
		float num = (float)Screen.width / 2f - 20f;
		float num2 = (float)((Screen.width < 800 && Screen.height < 800) ? 40 : 100);
		bool result = GUI.Button(new Rect(10f + (float)this._column * 10f * 2f + (float)this._column * num, 10f + (float)this._row * 10f + (float)this._row * num2, num, num2), text);
		this._column++;
		if (this._column > 1)
		{
			this._column = 0;
			this._row++;
		}
		return result;
	}

	private void init()
	{
		Options options = new Options();
		string value = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgcGouvzNjp8dK80nRKaibCoUPmxV16H+J6Cl6QkoS+MdsfVcr7YPYxKyf/KiTpssTTIFTNuorhHfQwdQgHCZyWS0W+wWjne/ZgitzBCrVaxDycJM5cNf9QXmKe1ZDpaITHyBb3xMUY+Tpkvq0GsvpVEFeXvfK7cx/i7uo/q9VvXLcrV4vsY/Bo99yrTuzT7SkIRQKB8H1l8t9Z63CZylJldK/hjPdp3ZmGE4qTyPEVY1kfh5Y8rHPWQruhTn8Q75rC3o87x8Y7UQtlnuPz+Jn1eP+5Yc6+/Kze22ToGtZw9YWfZYrdrPrxRAZdtcksBa5OYBIDmiS4nIKMmoNGy1UQIDAQAB";
		options.checkInventoryTimeoutMs = 20000;
		options.discoveryTimeoutMs = 10000;
		options.checkInventory = false;
		options.verifyMode = OptionsVerifyMode.VERIFY_SKIP;
		options.prefferedStoreNames = new string[]
		{
			OpenIAB_Android.STORE_GOOGLE
		};
		options.availableStoreNames = new string[]
		{
			OpenIAB_Android.STORE_GOOGLE
		};
		options.storeKeys = new Dictionary<string, string>
		{
			{
				OpenIAB_Android.STORE_GOOGLE,
				value
			}
		};
		options.storeSearchStrategy = SearchStrategy.INSTALLER_THEN_BEST_FIT;
		OpenIAB.init(options);
	}

	public void purchaseRemoveAdds()
	{
		OpenIAB.purchaseProduct("sku_removead", string.Empty);
	}

	public void purchaseCoinsPack1()
	{
		OpenIAB.purchaseProduct("sku_coinspack1", string.Empty);
	}

	public void purchaseCoinsPack2()
	{
		OpenIAB.purchaseProduct("sku_coinspack2", string.Empty);
	}

	public void purchaseCoinsPack3()
	{
		OpenIAB.purchaseProduct("sku_coinspack3", string.Empty);
	}

	public void purchaseCoinsPack4()
	{
		OpenIAB.purchaseProduct("sku_coinspack4", string.Empty);
	}

	private void OnGUI()
	{
	}

	private void queryInventorySucceededEvent(Inventory inventory)
	{
		UnityEngine.Debug.Log("queryInventorySucceededEvent: " + inventory);
		if (inventory != null)
		{
			this._label = inventory.ToString();
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
			goto IL_21F;
		case "sku_coinspack1":
			OpenIAB.EventManager.SendMessage("PurchaseSuccesCallBackToUI", 0, SendMessageOptions.DontRequireReceiver);
			goto IL_21F;
		case "sku_coinspack2":
			OpenIAB.EventManager.SendMessage("PurchaseSuccesCallBackToUI", 1, SendMessageOptions.DontRequireReceiver);
			goto IL_21F;
		case "sku_coinspack3":
			OpenIAB.EventManager.SendMessage("PurchaseSuccesCallBackToUI", 2, SendMessageOptions.DontRequireReceiver);
			goto IL_21F;
		case "sku_coinspack4":
			OpenIAB.EventManager.SendMessage("PurchaseSuccesCallBackToUI", 3, SendMessageOptions.DontRequireReceiver);
			goto IL_21F;
		case "sku_cashpack1":
			OpenIAB.EventManager.SendMessage("PurchaseSuccesCallBackToUI", 5, SendMessageOptions.DontRequireReceiver);
			goto IL_21F;
		case "sku_cashpack2":
			OpenIAB.EventManager.SendMessage("PurchaseSuccesCallBackToUI", 6, SendMessageOptions.DontRequireReceiver);
			goto IL_21F;
		case "sku_cashpack3":
			OpenIAB.EventManager.SendMessage("PurchaseSuccesCallBackToUI", 7, SendMessageOptions.DontRequireReceiver);
			goto IL_21F;
		case "sku_cashpack4":
			OpenIAB.EventManager.SendMessage("PurchaseSuccesCallBackToUI", 8, SendMessageOptions.DontRequireReceiver);
			goto IL_21F;
		case "android.test.purchased":
			OpenIAB.EventManager.SendMessage("PurchaseSuccesCallBackToUI", 4, SendMessageOptions.DontRequireReceiver);
			goto IL_21F;
		}
		UnityEngine.Debug.LogWarning("Unknown SKU: " + purchase.Sku);
		IL_21F:
		OpenIAB.queryInventory();
		UnityEngine.Debug.Log("purchaseSucceededEvent: " + purchase);
	}

	private void billingSupportedEvent()
	{
		this._isInitialized = true;
		UnityEngine.Debug.Log("billingSupportedEvent");
	}

	private void billingNotSupportedEvent(string error)
	{
		UnityEngine.Debug.Log("billingNotSupportedEvent: " + error);
	}

	private void purchaseFailedEvent(int errorCode, string errorMessage)
	{
		UnityEngine.Debug.Log("purchaseFailedEvent: " + errorMessage);
		this._label = "Purchase Failed: " + errorMessage;
	}

	private void consumePurchaseSucceededEvent(Purchase purchase)
	{
		UnityEngine.Debug.Log("consumePurchaseSucceededEvent: " + purchase);
		this._label = "CONSUMED: " + purchase.ToString();
	}

	private void consumePurchaseFailedEvent(string error)
	{
		UnityEngine.Debug.Log("consumePurchaseFailedEvent: " + error);
		this._label = "Consume Failed: " + error;
	}

	private void restoreFailedEvent(string error)
	{
		UnityEngine.Debug.Log("restoreFailedEvent: " + error);
	}

	private void restoreSuccededEvent()
	{
		UnityEngine.Debug.Log("restoreSuccededEvent: ");
	}

	private void transactionRestoredSuccededEvent(string s)
	{
		PlayerPrefs.SetInt("isOnce", 1);
		UnityEngine.Debug.Log("transactionRestoredSuccededEvent: " + s);
		OpenIAB.EventManager.SendMessage("purchaseDelegate", 4, SendMessageOptions.DontRequireReceiver);
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
		default:
			if (id == 100)
			{
				OpenIAB.purchaseProduct("android.test.purchased", string.Empty);
			}
			break;
		}
	}

	public void restorePressed()
	{
	}

	private const string SKU = "sku";

	private const string SKU_RemoveAdd = "sku_removead";

	private const string SKU_COINSPACK1 = "sku_coinspack1";

	private const string SKU_COINSPACK2 = "sku_coinspack2";

	private const string SKU_COINSPACK3 = "sku_coinspack3";

	private const string SKU_COINSPACK4 = "sku_coinspack4";

	private const string SKU_CASHPACK1 = "sku_cashpack1";

	private const string SKU_CASHPACK2 = "sku_cashpack2";

	private const string SKU_CASHPACK3 = "sku_cashpack3";

	private const string SKU_CASHPACK4 = "sku_cashpack4";

	private string _label = string.Empty;

	private bool _isInitialized;

	private Inventory _inventory;

	private const float X_OFFSET = 10f;

	private const float Y_OFFSET = 10f;

	private const int SMALL_SCREEN_SIZE = 800;

	private const int LARGE_FONT_SIZE = 34;

	private const int SMALL_FONT_SIZE = 24;

	private const int LARGE_WIDTH = 380;

	private const int SMALL_WIDTH = 160;

	private const int LARGE_HEIGHT = 100;

	private const int SMALL_HEIGHT = 40;

	private int _column;

	private int _row;
}
