// dnSpy decompiler from Assembly-CSharp.dll class: UpgradeItem
using System;
using UnityEngine;

[Serializable]
public class UpgradeItem
{
	public void setPurchasedIndex(int index)
	{
		this.purchasedIndex = index;
	}

	public void incrementPurchasedIndex(int gunId)
	{
		this.purchasedIndex++;
		if (this.purchasedIndex >= this.values.Length)
		{
			this.purchasedIndex = this.values.Length - 1;
		}
		PlayerPrefs.SetInt("Gun" + gunId.ToString() + this.name, this.purchasedIndex);
	}

	public void initializeItem(int gunId)
	{
		if (!PlayerPrefs.HasKey("Gun" + gunId.ToString() + this.name))
		{
			PlayerPrefs.SetInt("Gun" + gunId.ToString() + this.name, 0);
		}
		this.setPurchasedIndex(PlayerPrefs.GetInt("Gun" + gunId.ToString() + this.name, 0));
	}

	public float GetCurrentItemValues()
	{
		return this.values[this.purchasedIndex];
	}

	public int GetCurrentItemCost()
	{
		return this.cost[this.purchasedIndex];
	}

	public float GetItemValuesById(int id)
	{
		return this.values[id];
	}

	public int GetItemCostById(int id)
	{
		return this.cost[id];
	}

	public string name = string.Empty;

	public int id;

	public float[] values = new float[0];

	public int[] cost = new int[0];

	public int purchasedIndex;
}
