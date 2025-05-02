// dnSpy decompiler from Assembly-CSharp.dll class: UpgradeItemUI
using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemUI : MonoBehaviour
{
	private void Start()
	{
		this.weaponsUI = UnityEngine.Object.FindObjectOfType<WeaponsUIController>();
	}

	public void setDetails(UpgradeItem refItem)
	{
		this.item = refItem;
		if (this.item.purchasedIndex < this.item.values.Length - 1)
		{
			this.upgradeBtn.SetActive(true);
			float itemValuesById = this.item.GetItemValuesById(this.item.values.Length - 1);
			this.currentVal.fillAmount = this.item.GetCurrentItemValues() / itemValuesById;
			this.afterUpgradeVal.fillAmount = this.item.GetItemValuesById(this.item.purchasedIndex + 1) / itemValuesById;
			this.afterUpgradeVal.gameObject.SetActive(true);
			this.upgradeCost.text = this.item.GetItemCostById(this.item.purchasedIndex + 1).ToString();
		}
		else
		{
			this.upgradeBtn.SetActive(false);
			float itemValuesById2 = this.item.GetItemValuesById(this.item.values.Length - 1);
			this.currentVal.fillAmount = this.item.GetCurrentItemValues() / itemValuesById2;
			this.afterUpgradeVal.gameObject.SetActive(false);
		}
	}

	public void ShowLockedDetails(UpgradeItem refItem)
	{
		this.item = refItem;
		this.upgradeBtn.SetActive(false);
		this.afterUpgradeVal.gameObject.SetActive(false);
		float itemValuesById = this.item.GetItemValuesById(this.item.values.Length - 1);
		this.currentVal.fillAmount = this.item.GetCurrentItemValues() / itemValuesById;
	}

	public void upgradeBtnPressed()
	{
		this.weaponsUI.UpgradePressed(this.item);
	}

	private WeaponsUIController weaponsUI;

	public UpgradeItem item;

	public GameObject upgradeBtn;

	public Text upgradeCost;

	public Image currentVal;

	public Image afterUpgradeVal;
}
