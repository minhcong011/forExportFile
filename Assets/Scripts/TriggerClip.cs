// dnSpy decompiler from Assembly-CSharp.dll class: TriggerClip
using System;
using UnityEngine;

public class TriggerClip : MonoBehaviour
{
	private void Start()
	{
		if (this.player == null)
		{
			this.player = GameObject.FindGameObjectWithTag("Player");
		}
		this.inventory = this.player.GetComponent<weaponselector>();
	}

	private void OnTriggerEnter(Collider other)
	{
	}

	public int weaponNumber;

	public int ammo;

	private weaponselector inventory;

	public GameObject player;
}
