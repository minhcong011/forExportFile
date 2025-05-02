// dnSpy decompiler from Assembly-CSharp.dll class: TankWeaponController
using System;
using UnityEngine;

public class TankWeaponController : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKey(KeyCode.C))
		{
		}
		if (UnityEngine.Input.GetKey(KeyCode.V))
		{
		}
	}

	public void machineFire()
	{
		this.CurrentWeapon = 0;
		this.LaunchWeapon();
	}

	public void tankRocket()
	{
		this.CurrentWeapon = 1;
		this.LaunchWeapon();
	}

	public void LaunchWeapon(int index)
	{
		this.CurrentWeapon = index;
		if (this.CurrentWeapon < this.WeaponLists.Length && this.WeaponLists[index] != null)
		{
			this.WeaponLists[index].gameObject.GetComponent<WeaponLauncher>().Shoot(8f);
		}
	}

	public void LaunchWeaponn()
	{
		this.WeaponLists[1].gameObject.GetComponent<WeaponLauncher>().Shoot(8f);
	}

	public void LaunchWeapon()
	{
		if (this.CurrentWeapon < this.WeaponLists.Length && this.WeaponLists[this.CurrentWeapon] != null)
		{
			this.WeaponLists[this.CurrentWeapon].gameObject.GetComponent<WeaponLauncher>().Shoot(0.6f);
		}
	}

	public GameObject[] WeaponLists;

	public int CurrentWeapon;
}
