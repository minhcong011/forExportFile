// dnSpy decompiler from Assembly-CSharp.dll class: StoreManager
using System;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
	private void Awake()
	{
		if (!StoreManager.isCreated)
		{
			StoreManager.isCreated = true;
			UnityEngine.Object.DontDestroyOnLoad(this);
			Singleton<GameController>.Instance.storeManager = this;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void GiveMediPack(int quantity)
	{
		int @int = PlayerPrefs.GetInt("MedicPackCount", 0);
		PlayerPrefs.SetInt("MedicPackCount", @int + quantity);
	}

	public int GetMediPackCount()
	{
		return PlayerPrefs.GetInt("MedicPackCount", 0);
	}

	public void GiveMissile(int quantity)
	{
		int @int = PlayerPrefs.GetInt("MissilesCount", 0);
		PlayerPrefs.SetInt("MissilesCount", @int + quantity);
	}

	public int GetMissileCount()
	{
		return PlayerPrefs.GetInt("MissilesCount", 0);
	}

	public int GetTotalCoinsCount()
	{
		return PlayerPrefs.GetInt("TotalCoins", 0);
	}

	public void addCoins(int quantity)
	{
		int @int = PlayerPrefs.GetInt("TotalCoins", 0);
		PlayerPrefs.SetInt("TotalCoins", @int + quantity);
	}

	public void consumeCoins(int quantity)
	{
		int num = PlayerPrefs.GetInt("TotalCoins", 0);
		if (num < 0)
		{
			num = 0;
		}
		PlayerPrefs.SetInt("TotalCoins", num - quantity);
	}

	public int GetTotalCashCount()
	{
		return PlayerPrefs.GetInt("TotalCash", 0);
	}

	public void addCash(int quantity)
	{
		int @int = PlayerPrefs.GetInt("TotalCash", 0);
		PlayerPrefs.SetInt("TotalCash", @int + quantity);
	}

	public int GetTotalXPCount()
	{
		return PlayerPrefs.GetInt("TotalXP", 0);
	}

	public void addXP(int quantity)
	{
		int @int = PlayerPrefs.GetInt("TotalXP", 0);
		PlayerPrefs.SetInt("TotalXP", @int + quantity);
	}

	public int GetTotalSpins()
	{
		return PlayerPrefs.GetInt("TotalSpins", 0);
	}

	public void addSpins(int quantity)
	{
		int @int = PlayerPrefs.GetInt("TotalSpins", 0);
		PlayerPrefs.SetInt("TotalSpins", @int + quantity);
	}

	public void consumeSpin()
	{
		int num = PlayerPrefs.GetInt("TotalSpins", 0);
		num--;
		if (num < 0)
		{
			num = 0;
		}
		PlayerPrefs.SetInt("TotalSpins", num);
	}

	public void consumeCash(int quantity)
	{
		int num = PlayerPrefs.GetInt("TotalCash", 0);
		if (num < 0)
		{
			num = 0;
		}
		PlayerPrefs.SetInt("TotalCash", num - quantity);
	}

	public void GiveGernades(int quantity)
	{
		int @int = PlayerPrefs.GetInt("Gernades", 1);
		PlayerPrefs.SetInt("Gernades", @int + quantity);
	}

	public int GetGernadesCount()
	{
		return PlayerPrefs.GetInt("Gernades", 0);
	}

	public void GiveAirStrike(int quantity)
	{
		int @int = PlayerPrefs.GetInt("AirStrikeCount", 0);
		PlayerPrefs.SetInt("AirStrikeCount", @int + quantity);
	}

	public int GetAirStrikeCount()
	{
		return PlayerPrefs.GetInt("AirStrikeCount", 0);
	}

	public int GetSelectedArmour()
	{
		return PlayerPrefs.GetInt("SelectedArmour", -1);
	}

	public void SetSelectedArmour(int id)
	{
		PlayerPrefs.SetInt("SelectedArmour", id);
	}

	public void SetSelectedArmourNone()
	{
		PlayerPrefs.SetInt("SelectedArmour", -1);
	}

	public bool isArmourPurchased(int id)
	{
		return PlayerPrefs.GetInt("PurchaseArmour" + id, 0) != 0;
	}

	public void setArmourPurchased(int id)
	{
		PlayerPrefs.SetInt("PurchaseArmour" + id, 1);
	}

	public void consumeArmour(int id)
	{
		PlayerPrefs.SetInt("PurchaseArmour" + id, 0);
	}

	public int[] mediPackCoinPrice = new int[]
	{
		200,
		500,
		800
	};

	public int[] mediPackItemCount = new int[]
	{
		1,
		3,
		5
	};

	public int[] missilePackCoinPrice = new int[]
	{
		100,
		500,
		1000
	};

	public int[] missilePackItemCount = new int[]
	{
		1,
		3,
		5
	};

	public int[] airStrikePackCoinPrice = new int[]
	{
		100,
		500,
		1000
	};

	public int[] airStrikePackItemCount = new int[]
	{
		1,
		3,
		5
	};

	public int[] armourPackCoinPrice = new int[]
	{
		200,
		400,
		600,
		1000
	};

	public int[] armourPackItemCount = new int[]
	{
		120,
		140,
		160,
		200
	};

	public int[] coinsPackPrice = new int[]
	{
		1,
		2,
		4,
		7
	};

	public int[] coinsPackItemCount = new int[]
	{
		1000,
		2000,
		4000,
		8000
	};

	public int[] cashPackPrice = new int[]
	{
		1,
		2,
		4,
		7
	};

	public int[] cashPackItemCount = new int[]
	{
		1000,
		2000,
		4000,
		8000
	};

	private static bool isCreated;
}
