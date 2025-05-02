// dnSpy decompiler from Assembly-CSharp.dll class: PickUpHandler
using System;
using UnityEngine;

public class PickUpHandler : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnTriggerEnter(Collider col)
	{
		MonoBehaviour.print("collisionENter " + col.tag);
		if (col.tag == "Player")
		{
			col.GetComponent<WeaponAmmoManagerCS>().AddAmmos(8);
			if (Constants.isSoundOn() && this.pickUpSound != null)
			{
				AudioSource.PlayClipAtPoint(this.pickUpSound, base.transform.position);
			}
		}
	}

	public int ammosQuantity = 10;

	public PickUpHandler.PickUpType pickupType;

	public AudioClip pickUpSound;

	public enum PickUpType
	{
		ammos,
		weapon,
		points,
		health
	}
}
