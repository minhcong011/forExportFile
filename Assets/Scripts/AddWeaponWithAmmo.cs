// dnSpy decompiler from Assembly-CSharp.dll class: AddWeaponWithAmmo
using System;
using System.Collections.Generic;
using UnityEngine;

public class AddWeaponWithAmmo : MonoBehaviour
{
	private void Start()
	{
		WeaponManagerStore weaponManager = Singleton<GameController>.Instance.weaponManager;
		List<GunModel> gunModels = weaponManager.gunModels;
		if (weaponManager.GetIsGunUnlocked(this.gunId) && this.shouldUnlockFromStore)
		{
			base.gameObject.SetActive(false);
		}
	}

	public void OnTriggerEnter(Collider col)
	{
		if (this.isCollected)
		{
			return;
		}
		if (col.gameObject.tag == "Player")
		{
			this.isCollected = true;
			if (Singleton<GameController>.Instance.soundController != null)
			{
				Singleton<GameController>.Instance.soundController.PlayGunCollectSound();
			}
			weaponselector component = col.gameObject.GetComponent<weaponselector>();
			if (component != null)
			{
				if (!this.shouldUnlockFromStore)
				{
					component.AddWeaponWithAmmos(this.gunId, this.ammos, this.clipSize);
					PopUpBase popUpBase = new PopUpBase();
					popUpBase.description = this.gunName + " Collected";
					popUpBase.screenStayTime = 2f;
					popUpBase.isCancelBtn = false;
					Singleton<GameController>.Instance.uiPopUpManager.ShowPopUp("SmallPopUp", popUpBase);
				}
				else
				{
					component.AddWeapon(this.gunId);
				}
			}
			if (this.shouldUnlockFromStore)
			{
				WeaponManagerStore weaponManager = Singleton<GameController>.Instance.weaponManager;
				List<GunModel> gunModels = weaponManager.gunModels;
				if (weaponManager.GetIsGunUnlocked(this.gunId))
				{
					PopUpBase popUpBase2 = new PopUpBase();
					popUpBase2.description = this.gunName + " Collected";
					popUpBase2.screenStayTime = 2f;
					popUpBase2.isCancelBtn = false;
					Singleton<GameController>.Instance.uiPopUpManager.ShowPopUp("SmallPopUp", popUpBase2);
				}
				else
				{
					PopUpBase popUpBase3 = new PopUpBase();
					popUpBase3.description = "New Weapon Unlocked";
					popUpBase3.screenStayTime = 2f;
					popUpBase3.isCancelBtn = false;
					Singleton<GameController>.Instance.uiPopUpManager.ShowPopUp("SmallPopUp", popUpBase3);
					weaponManager.PurchaseGun(this.gunId);
				}
			}
			base.gameObject.SetActive(false);
		}
	}

	private void SetGunName()
	{
		int num = this.gunId;
		if (num != 0)
		{
			if (num == 8)
			{
				this.gunName = "Remote Bomb";
			}
		}
		else
		{
			this.gunName = "Pistol";
		}
	}

	public bool isCollected;

	public int gunId;

	public int ammos = 20;

	public int clipSize = 5;

	public bool shouldUnlockFromStore;

	public string gunName = "Default";
}
