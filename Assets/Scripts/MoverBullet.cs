// dnSpy decompiler from Assembly-CSharp.dll class: MoverBullet
using System;
using UnityEngine;

public class MoverBullet : WeaponBase
{
	private void Start()
	{
		if (!this.IsPooled)
		{
			UnityEngine.Object.Destroy(base.gameObject, (float)this.Lifetime);
		}
	}

	private void OnDisable()
	{
		if (base.GetComponent<Rigidbody>())
		{
			base.GetComponent<Rigidbody>().velocity = Vector3.zero;
			base.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}
		if (base.GetComponent<TrailRenderer>() != null)
		{
			base.GetComponent<TrailRenderer>().Clear();
		}
	}

	private void FixedUpdate()
	{
		if (!base.GetComponent<Rigidbody>())
		{
			return;
		}
		if (!this.RigidbodyProjectile)
		{
			base.GetComponent<Rigidbody>().velocity = base.transform.forward * this.Speed;
		}
		else if (base.GetComponent<Rigidbody>().velocity.normalized != Vector3.zero)
		{
			base.transform.forward = base.GetComponent<Rigidbody>().velocity.normalized;
		}
		if (this.Speed < this.SpeedMax)
		{
			this.Speed += this.SpeedMult * Time.fixedDeltaTime;
		}
	}

	public int Lifetime;

	public float Speed = 80f;

	public float SpeedMax = 80f;

	public float SpeedMult = 1f;

	public bool IsPooled;
}
