// dnSpy decompiler from Assembly-CSharp.dll class: HeliWeaponsController
using System;
using UnityEngine;

public class HeliWeaponsController : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void Awake()
	{
	}

	public void helimachineFiring()
	{
		this.CurrentWeapon = 0;
		this.LaunchWeapon();
	}

	public void LaunchWeapon(int index)
	{
		this.CurrentWeapon = index;
		if (this.CurrentWeapon < this.WeaponLists.Length && this.WeaponLists[index] != null)
		{
			this.WeaponLists[index].gameObject.GetComponent<WeaponLauncher>().Shoot(0f);
		}
	}

	public void LaunchWeaponn()
	{
		this.WeaponLists[0].gameObject.GetComponent<WeaponLauncher>().Shoot(0f);
	}

	public void LaunchWeapon()
	{
		if (this.CurrentWeapon < this.WeaponLists.Length && this.WeaponLists[this.CurrentWeapon] != null)
		{
			this.WeaponLists[this.CurrentWeapon].gameObject.GetComponent<WeaponLauncher>().Shoot(0f);
		}
	}

	public GameObject[] WeaponLists;

	public int CurrentWeapon;

	public AudioClip helisound;
}
