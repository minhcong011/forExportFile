// dnSpy decompiler from Assembly-CSharp.dll class: WeaponController
using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKey(KeyCode.F))
		{
			this.CurrentWeapon = 0;
			this.LaunchWeapon();
		}
		if (UnityEngine.Input.GetKey(KeyCode.G))
		{
			this.CurrentWeapon = 1;
			this.LaunchWeapon();
			this.CurrentWeapon = 2;
			this.LaunchWeapon();
		}
	}

	public void LaunchWeapon(int index)
	{
		if (this.doubleMachine && index == 1)
		{
			int num = UnityEngine.Random.Range(1, 3);
			this.CurrentWeapon = num;
			if (this.CurrentWeapon < this.WeaponLists.Length && this.WeaponLists[num] != null)
			{
				this.WeaponLists[num].gameObject.GetComponent<WeaponLauncher>().Shoot(0f);
			}
		}
		else
		{
			this.CurrentWeapon = index;
			if (this.CurrentWeapon < this.WeaponLists.Length && this.WeaponLists[index] != null)
			{
				this.WeaponLists[index].gameObject.GetComponent<WeaponLauncher>().Shoot(0f);
			}
		}
	}

	public void LaunchWeaponn()
	{
		this.WeaponLists[1].gameObject.GetComponent<WeaponLauncher>().Shoot(0f);
	}

	public void LaunchWeapon()
	{
		if (this.CurrentWeapon < this.WeaponLists.Length && this.WeaponLists[this.CurrentWeapon] != null)
		{
			this.WeaponLists[this.CurrentWeapon].gameObject.GetComponent<WeaponLauncher>().Shoot(0f);
		}
	}

	public void AirStrikeWeapon()
	{
	}

	public GameObject[] WeaponLists;

	public int CurrentWeapon;

	public bool doubleMachine;

	public int initialMissileCount = 1;

	public Transform targetPoint;
}
