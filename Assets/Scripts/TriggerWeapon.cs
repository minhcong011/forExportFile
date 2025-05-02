// dnSpy decompiler from Assembly-CSharp.dll class: TriggerWeapon
using System;
using UnityEngine;

public class TriggerWeapon : MonoBehaviour
{
	private void Start()
	{
		if (this.player == null)
		{
			this.player = GameObject.Find("playerController");
		}
		this.inventory = this.player.GetComponent<weaponselector>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			this.inventory.HaveWeapons[this.weaponNumber] = true;
			this.inventory.playSwithSound();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public int weaponNumber;

	private weaponselector inventory;

	public GameObject player;
}
