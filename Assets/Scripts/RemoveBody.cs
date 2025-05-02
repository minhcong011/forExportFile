// dnSpy decompiler from Assembly-CSharp.dll class: RemoveBody
using System;
using UnityEngine;

public class RemoveBody : MonoBehaviour
{
	private void Start()
	{
		this.startTime = Time.time;
	}

	private void FixedUpdate()
	{
		if (this.startTime + this.bodyStayTime < Time.time)
		{
			if (this.GunPickup)
			{
				this.GunPickup.transform.parent = null;
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private float startTime;

	[HideInInspector]
	public float bodyStayTime = 2f;

	[Tooltip("Weapon item pickup that should be spawned after NPC dies (used for single capsule collider NPCs which instantiate ragdoll on death).")]
	public GameObject GunPickup;
}
