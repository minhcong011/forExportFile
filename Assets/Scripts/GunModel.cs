// dnSpy decompiler from Assembly-CSharp.dll class: GunModel
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GunModel
{
	public void initializeGun()
	{
		foreach (UpgradeItem upgradeItem in this.upgradeItems)
		{
			upgradeItem.initializeItem(this.id);
		}
		this.updateAllValues();
	}

	private void updateAllValues()
	{
		this.clipSize = (int)Convert.ToInt16(this.upgradeItems[0].GetCurrentItemValues());
		this.damage = (float)Convert.ToInt16(this.upgradeItems[1].GetCurrentItemValues());
		this.reloadTime = (float)Convert.ToInt16(this.upgradeItems[2].GetCurrentItemValues());
		this.zoomValue = (float)Convert.ToInt16(this.upgradeItems[3].GetCurrentItemValues());
		this.fireRate = this.upgradeItems[4].GetCurrentItemValues();
		this.totalBullets = (int)Convert.ToInt16(this.upgradeItems[5].GetCurrentItemValues());
	}

	public void BuyUpgradeById(int upgradeId)
	{
		this.upgradeItems[upgradeId].incrementPurchasedIndex(this.id);
	}

	public void upgradeClipSize()
	{
		this.upgradeItems[0].incrementPurchasedIndex(this.id);
		this.updateAllValues();
	}

	public void upgradeDamage()
	{
		this.upgradeItems[1].incrementPurchasedIndex(this.id);
		this.updateAllValues();
	}

	public UpgradeItem GetUpgradeItemById(int upgradeId)
	{
		return this.upgradeItems[upgradeId];
	}

	public UpgradeItem GetClipSizeItem()
	{
		return this.upgradeItems[0];
	}

	public UpgradeItem GetDamageItem()
	{
		return this.upgradeItems[1];
	}

	public string name = string.Empty;

	public int id;

	public int weaponCost;

	private int clipSize;

	[HideInInspector]
	public float damage;

	[HideInInspector]
	public float reloadTime;

	[HideInInspector]
	public float zoomValue;

	[HideInInspector]
	public float fireRate;

	[HideInInspector]
	public int totalBullets;

	public List<UpgradeItem> upgradeItems;
}
