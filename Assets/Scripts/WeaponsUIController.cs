// dnSpy decompiler from Assembly-CSharp.dll class: WeaponsUIController
using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsUIController : MonoBehaviour
{
	private void Start()
	{
		this.weaponManager = UnityEngine.Object.FindObjectOfType<WeaponManagerStore>();
		this.storeManager = Singleton<GameController>.Instance.storeManager;
		this.UpdateUIDetails();
	}

	private void OnEnable()
	{
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
			adsManager.LogScreen("Weapon Selection Menu");
		}
	}

	private bool checkIsGunUnlocked()
	{
		return this.weaponManager.GetIsGunUnlocked(this.currentGunId);
	}

	public void RightClicked()
	{
		this.currentGunId++;
		if (this.currentGunId >= this.guns.Length)
		{
			this.currentGunId = 0;
		}
		this.UpdateUIDetails();
		MonoBehaviour.print(this.currentGunId);
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
	}

	public void LeftClicked()
	{
		this.currentGunId--;
		if (this.currentGunId < 0)
		{
			this.currentGunId = this.guns.Length - 1;
		}
		this.UpdateUIDetails();
		MonoBehaviour.print(this.currentGunId);
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
	}

	public void GunClicked(int id)
	{
		if (this.currentGunId == id)
		{
			return;
		}
		this.currentGunId = id;
		this.UpdateUIDetails();
		MonoBehaviour.print(this.currentGunId);
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
	}

	private void setSelectedItem()
	{
		for (int i = 0; i < this.selectedItem.Length; i++)
		{
			this.selectedItem[i].SetActive(false);
		}
		this.selectedItem[this.currentGunId].SetActive(true);
	}

	private void UpdateUIDetails()
	{
		foreach (GameObject gameObject in this.guns)
		{
			gameObject.SetActive(false);
		}
		if (this.guns.Length > 0)
		{
			this.guns[this.currentGunId].SetActive(true);
		}
		GunModel gunModelWithId = this.weaponManager.GetGunModelWithId(this.currentGunId);
		if (this.checkIsGunUnlocked())
		{
			this.purchaseBtn.SetActive(false);
			for (int j = 0; j < this.upgradeItemsArray.Length; j++)
			{
				this.upgradeItemsArray[j].setDetails(gunModelWithId.GetUpgradeItemById(j));
			}
		}
		else
		{
			this.purchaseBtn.SetActive(true);
			this.purchaseCost.text = gunModelWithId.weaponCost.ToString();
			for (int k = 0; k < this.upgradeItemsArray.Length; k++)
			{
				this.upgradeItemsArray[k].ShowLockedDetails(gunModelWithId.GetUpgradeItemById(k));
			}
		}
		this.GunName.text = gunModelWithId.name;
	}

	public void GunBuyClicked()
	{
		GunModel gunModelWithId = this.weaponManager.GetGunModelWithId(this.currentGunId);
		int weaponCost = gunModelWithId.weaponCost;
		int totalCashCount = this.storeManager.GetTotalCashCount();
		if (weaponCost <= totalCashCount)
		{
			this.weaponManager.PurchaseGun(this.currentGunId);
			this.storeManager.consumeCash(weaponCost);
			this.UpdateUIDetails();
			Singleton<GameController>.Instance.soundController.PlayStoreProduct();
		}
		else
		{
			this.NotEnoughCoins();
		}
		Singleton<GameController>.Instance.eventManager.UpdateStoreUI();
		StoreUIController storeUIController = UnityEngine.Object.FindObjectOfType<StoreUIController>();
		if (storeUIController != null)
		{
			storeUIController.UpdateUI();
		}
	}

	public void UpgradePressed(UpgradeItem item)
	{
		GunModel gunModelWithId = this.weaponManager.GetGunModelWithId(this.currentGunId);
		int itemCostById = item.GetItemCostById(item.purchasedIndex + 1);
		int totalCashCount = this.storeManager.GetTotalCashCount();
		if (itemCostById <= totalCashCount)
		{
			gunModelWithId.BuyUpgradeById(item.id);
			this.UpdateUIDetails();
			this.storeManager.consumeCash(itemCostById);
			Singleton<GameController>.Instance.soundController.PlayStoreProduct();
			if (Singleton<GameController>.Instance.taskManager != null)
			{
				Singleton<GameController>.Instance.taskManager.getCurrentDayModel().updateTask(4);
			}
			UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
			if (adsManager != null)
			{
			}
		}
		else
		{
			this.NotEnoughCoins();
		}
		Singleton<GameController>.Instance.eventManager.UpdateStoreUI();
		StoreUIController storeUIController = UnityEngine.Object.FindObjectOfType<StoreUIController>();
		if (storeUIController != null)
		{
			storeUIController.UpdateUI();
		}
	}

	private void NotEnoughCoins()
	{
		UnityEngine.Object.FindObjectOfType<StoreUIController>().notEnoughCoins();
	}

	private void Update()
	{
	}

	public int currentGunId;

	private WeaponManagerStore weaponManager;

	private StoreManager storeManager;

	public GameObject[] guns;

	public GameObject purchaseBtn;

	public Text purchaseCost;

	public Text GunName;

	public UpgradeItemUI[] upgradeItemsArray;

	public GameObject[] selectedItem;
}
