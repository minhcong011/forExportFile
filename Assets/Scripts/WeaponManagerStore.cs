// dnSpy decompiler from Assembly-CSharp.dll class: WeaponManagerStore
using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagerStore : MonoBehaviour
{
	private void Awake()
	{
		if (!WeaponManagerStore.isCreated)
		{
			Singleton<GameController>.Instance.weaponManager = this;
			WeaponManagerStore.isCreated = true;
			UnityEngine.Object.DontDestroyOnLoad(this);
			this.InitializePlayerPrefs();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Start()
	{
	}

	private void InitializePlayerPrefs()
	{
		for (int i = 0; i < this.gunModels.Count; i++)
		{
			if (!PlayerPrefs.HasKey("GunUnlocked" + i))
			{
				if (i == 0)
				{
					PlayerPrefs.SetInt("GunUnlocked" + 0, 1);
				}
				else
				{
					PlayerPrefs.SetInt("GunUnlocked" + i, 0);
				}
			}
		}
		foreach (GunModel gunModel in this.gunModels)
		{
			gunModel.initializeGun();
		}
	}

	public bool GetIsGunUnlocked(int id)
	{
		return PlayerPrefs.GetInt("GunUnlocked" + id, 0) != 0;
	}

	public GunModel GetGunModelWithId(int id)
	{
		return this.gunModels[id];
	}

	public void PurchaseGun(int id)
	{
		PlayerPrefs.SetInt("GunUnlocked" + id, 1);
	}

	public List<GunModel> gunModels;

	private static bool isCreated;
}
