// dnSpy decompiler from Assembly-CSharp.dll class: grenadepickup
using System;
using UnityEngine;

public class grenadepickup : MonoBehaviour
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
			this.inventory.grenade++;
			this.inventory.playSwithSound();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private weaponselector inventory;

	public GameObject player;
}
