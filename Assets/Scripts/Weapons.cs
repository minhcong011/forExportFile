// dnSpy decompiler from Assembly-CSharp.dll class: Weapons
using System;
using System.Collections.Generic;

public class Weapons
{
	public static WeaponData GetDataByID_Fast(int id, Weapons.wSlotsType slotType)
	{
		WeaponData weaponData = new WeaponData();
		switch (slotType)
		{
		case Weapons.wSlotsType.Melee:
			if (id == 0)
			{
				weaponData.name = "Hands";
				weaponData.path_TPV = string.Empty;
				weaponData.path_FPV = "Prefabs/Weapons/FPV/v_Knife";
				weaponData.path_Sound_Attack = "Sounds/Weapons/Knife/attack";
				weaponData.attackRate = 1.5f;
				weaponData.reloadTime = 0f;
				weaponData._bullets = 0;
				weaponData._clip = 0;
				weaponData._magzine = 0;
				weaponData._damage = 10f;
				weaponData._range = 10f;
				return weaponData;
			}
			if (id != 1)
			{
				return null;
			}
			weaponData.name = "Knife";
			weaponData.path_TPV = "Prefabs/Weapons/TPV/Knife";
			weaponData.path_FPV = "Prefabs/Weapons/FPV/v_Knife";
			weaponData.path_Sound_Attack = "Sounds/Weapons/Knife/attack";
			weaponData._bullets = 0;
			weaponData._clip = 0;
			weaponData._magzine = 0;
			weaponData._damage = 10f;
			weaponData._range = 10f;
			weaponData.attackRate = 1.5f;
			weaponData.reloadTime = 0f;
			return weaponData;
		case Weapons.wSlotsType.Shotguns:
			if (id == 0)
			{
				weaponData.name = "Glock18";
				weaponData.path_TPV = "Prefabs/Weapons/TPV/Glock18";
				weaponData.path_FPV = "Prefabs/Weapons/FPV/v_glock18";
				weaponData.path_Sound_Attack = "Sounds/Weapons/Glock18/shot1";
				weaponData.attackRate = 0.3f;
				weaponData.reloadTime = 2.7f;
				weaponData._bullets = 1;
				weaponData._clip = 8;
				weaponData._magzine = 20;
				weaponData._damage = 20f;
				weaponData._range = 50f;
				return weaponData;
			}
			if (id != 1)
			{
				return null;
			}
			weaponData.name = "Gernade";
			weaponData.path_FPV = "Prefabs/Weapons/FPV/UK_Gernade";
			weaponData.path_TPV = "Prefabs/Weapons/TPV/Awp";
			weaponData.attackRate = 2f;
			weaponData.reloadTime = 3f;
			weaponData._bullets = 1;
			weaponData._clip = 1;
			weaponData._magzine = 5;
			weaponData._damage = 110f;
			weaponData._range = 200f;
			return weaponData;
		case Weapons.wSlotsType.Assault_Rifle:
			if (id == 0)
			{
				weaponData.name = "M4";
				weaponData.path_TPV = "Prefabs/Weapons/TPV/M4";
				weaponData.path_FPV = "Prefabs/Weapons/FPV/v_M4";
				weaponData.path_Sound_Attack = "Sounds/Weapons/AK47/singleshot1";
				weaponData.attackRate = 0.2f;
				weaponData.reloadTime = 3f;
				weaponData._bullets = 1;
				weaponData._clip = 30;
				weaponData._magzine = 5;
				weaponData._damage = 50f;
				weaponData._range = 100f;
				return weaponData;
			}
			if (id != 1)
			{
				return null;
			}
			weaponData.name = "AK47";
			weaponData.path_TPV = "Prefabs/Weapons/TPV/AK47";
			weaponData.path_FPV = "Prefabs/Weapons/FPV/v_AK47";
			weaponData.path_Sound_Attack = "Sounds/Weapons/AK47/singleshot1";
			weaponData.attackRate = 0.22f;
			weaponData.reloadTime = 3f;
			weaponData._bullets = 1;
			weaponData._clip = 20;
			weaponData._damage = 75f;
			weaponData._range = 100f;
			weaponData._magzine = 5;
			return weaponData;
		case Weapons.wSlotsType.Sniper_Rifle:
			if (id != 0)
			{
				return null;
			}
			weaponData.name = "AWP";
			weaponData.path_FPV = "Prefabs/Weapons/FPV/v_AWP";
			weaponData.path_TPV = "Prefabs/Weapons/TPV/Awp";
			weaponData.attackRate = 0.3f;
			weaponData.reloadTime = 1f;
			weaponData._bullets = 1;
			weaponData._clip = 1;
			weaponData._magzine = 5;
			weaponData._damage = 110f;
			weaponData._range = 200f;
			return weaponData;
		case Weapons.wSlotsType.Gernade:
			if (id != 0)
			{
				return null;
			}
			weaponData.name = "Gernade";
			weaponData.path_FPV = "Prefabs/Weapons/FPV/UK_Gernade";
			weaponData.path_TPV = "Prefabs/Weapons/TPV/Awp";
			weaponData.attackRate = 2f;
			weaponData.reloadTime = 3f;
			weaponData._bullets = 1;
			weaponData._clip = 10;
			weaponData._magzine = 10;
			weaponData._damage = 110f;
			weaponData._range = 200f;
			return weaponData;
		default:
			return null;
		}
	}

	public void setGunSpecs(int gunId, GunModel model)
	{
		int[] array = new int[6];
		for (int i = 0; i < array.Length - 1; i++)
		{
		}
		array[5] = gunId;
	}

	public static WeaponData GetDataByID_WeaponStore(int id, Weapons.wSlotsType slotType)
	{
		WeaponManagerStore weaponManager = Singleton<GameController>.Instance.weaponManager;
		List<GunModel> gunModels = weaponManager.gunModels;
		WeaponData weaponData = new WeaponData();
		switch (slotType)
		{
		case Weapons.wSlotsType.Melee:
			if (id == 0)
			{
				weaponData.name = "Hands";
				weaponData.path_TPV = string.Empty;
				weaponData.path_FPV = "Prefabs/Weapons/FPV/v_Knife";
				weaponData.path_Sound_Attack = "Sounds/Weapons/Knife/attack";
				weaponData.attackRate = 1.5f;
				weaponData.reloadTime = 0f;
				weaponData._bullets = 0;
				weaponData._clip = 0;
				weaponData._magzine = 0;
				weaponData._damage = 10f;
				weaponData._range = 10f;
				return weaponData;
			}
			if (id != 1)
			{
				return null;
			}
			weaponData.name = "Knife";
			weaponData.path_TPV = "Prefabs/Weapons/TPV/Knife";
			weaponData.path_FPV = "Prefabs/Weapons/FPV/v_Knife";
			weaponData.path_Sound_Attack = "Sounds/Weapons/Knife/attack";
			weaponData._bullets = 0;
			weaponData._clip = 0;
			weaponData._magzine = 0;
			weaponData._damage = 10f;
			weaponData._range = 10f;
			weaponData.attackRate = 1.5f;
			weaponData.reloadTime = 0f;
			return weaponData;
		case Weapons.wSlotsType.Shotguns:
			if (id == 0)
			{
				weaponData.name = "Glock18";
				weaponData.path_TPV = "Prefabs/Weapons/TPV/Glock18";
				weaponData.path_FPV = "Prefabs/Weapons/FPV/v_glock18";
				weaponData.path_Sound_Attack = "Sounds/Weapons/Glock18/shot1";
				weaponData._bullets = 1;
				weaponData._clip = 12;
				weaponData._magzine = 30;
				weaponData._range = 50f;
				weaponData.attackRate = gunModels[0].GetUpgradeItemById(4).GetCurrentItemValues();
				weaponData._damage = gunModels[0].GetUpgradeItemById(1).GetCurrentItemValues();
				weaponData.reloadTime = gunModels[0].GetUpgradeItemById(2).GetCurrentItemValues();
				weaponData._clip = (int)Convert.ToInt16(gunModels[0].GetUpgradeItemById(0).GetCurrentItemValues());
				return weaponData;
			}
			if (id != 1)
			{
				return null;
			}
			weaponData.name = "Rocketlauncher";
			weaponData.path_TPV = "Prefabs/Weapons/TPV/Glock18";
			weaponData.path_FPV = "Prefabs/Weapons/FPV/v_RocketLauncher";
			weaponData.path_Sound_Attack = "Sounds/Weapons/Glock18/shot1";
			weaponData.attackRate = 0.3f;
			weaponData.reloadTime = 1.3f;
			weaponData._bullets = 1;
			weaponData._clip = 1;
			weaponData._magzine = 100;
			weaponData._damage = 110f;
			weaponData._range = 200f;
			weaponData._zoomVal = 40f;
			return weaponData;
		case Weapons.wSlotsType.Assault_Rifle:
			if (id == 0)
			{
				weaponData.name = "M4";
				weaponData.path_TPV = "Prefabs/Weapons/TPV/M4";
				weaponData.path_FPV = "Prefabs/Weapons/FPV/v_M4";
				weaponData.path_Sound_Attack = "Sounds/Weapons/AK47/singleshot1";
				weaponData.attackRate = 0.2f;
				weaponData.reloadTime = 3f;
				weaponData._bullets = 1;
				weaponData._clip = 30;
				weaponData._magzine = 50;
				weaponData._damage = 50f;
				weaponData._range = 100f;
				weaponData.attackRate = gunModels[0].GetUpgradeItemById(4).GetCurrentItemValues();
				weaponData._damage = gunModels[0].GetUpgradeItemById(1).GetCurrentItemValues();
				weaponData.reloadTime = gunModels[0].GetUpgradeItemById(2).GetCurrentItemValues();
				weaponData._clip = (int)Convert.ToInt16(gunModels[0].GetUpgradeItemById(0).GetCurrentItemValues());
				return weaponData;
			}
			if (id != 1)
			{
				return null;
			}
			weaponData.name = "AK47";
			weaponData.path_TPV = "Prefabs/Weapons/TPV/AK47";
			weaponData.path_FPV = "Prefabs/Weapons/FPV/v_AK47";
			weaponData.path_Sound_Attack = "Sounds/Weapons/AK47/singleshot1";
			weaponData.attackRate = 0.22f;
			weaponData.reloadTime = 3f;
			weaponData._bullets = 1;
			weaponData._clip = 20;
			weaponData._damage = 75f;
			weaponData._range = 100f;
			weaponData._magzine = 100;
			weaponData.attackRate = gunModels[1].GetUpgradeItemById(4).GetCurrentItemValues();
			weaponData._damage = gunModels[1].GetUpgradeItemById(1).GetCurrentItemValues();
			weaponData.reloadTime = gunModels[1].GetUpgradeItemById(2).GetCurrentItemValues();
			weaponData._clip = (int)Convert.ToInt16(gunModels[1].GetUpgradeItemById(0).GetCurrentItemValues());
			return weaponData;
		case Weapons.wSlotsType.Sniper_Rifle:
			if (id != 0)
			{
				return null;
			}
			weaponData.name = "AWP";
			weaponData.path_FPV = "Prefabs/Weapons/FPV/v_AWP";
			weaponData.path_TPV = "Prefabs/Weapons/TPV/Awp";
			weaponData.attackRate = 1f;
			weaponData.reloadTime = 1f;
			weaponData._bullets = 1;
			weaponData._clip = 1;
			weaponData._magzine = 100;
			weaponData._damage = 110f;
			weaponData._range = 200f;
			weaponData.attackRate = gunModels[2].GetUpgradeItemById(4).GetCurrentItemValues();
			weaponData._damage = gunModels[2].GetUpgradeItemById(1).GetCurrentItemValues();
			weaponData.reloadTime = gunModels[2].GetUpgradeItemById(2).GetCurrentItemValues();
			weaponData._clip = (int)Convert.ToInt16(gunModels[2].GetUpgradeItemById(0).GetCurrentItemValues());
			weaponData._zoomVal = gunModels[2].GetUpgradeItemById(3).GetCurrentItemValues();
			return weaponData;
		case Weapons.wSlotsType.Gernade:
			if (id != 0)
			{
				return null;
			}
			weaponData.name = "Gernade";
			weaponData.path_FPV = "Prefabs/Weapons/FPV/UK_Gernade";
			weaponData.path_TPV = "Prefabs/Weapons/TPV/Awp";
			weaponData.attackRate = 2.2f;
			weaponData.reloadTime = 3f;
			weaponData._bullets = 1;
			weaponData._clip = 5;
			weaponData._magzine = 1;
			weaponData._damage = 110f;
			weaponData._range = 200f;
			weaponData._zoomVal = 40f;
			return weaponData;
		default:
			return null;
		}
	}

	public enum wSlotsType
	{
		Melee,
		Shotguns,
		Assault_Rifle,
		Sniper_Rifle,
		Gernade
	}
}
