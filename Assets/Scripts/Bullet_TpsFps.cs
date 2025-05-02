// dnSpy decompiler from Assembly-CSharp.dll class: Bullet_TpsFps
using System;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_TpsFps : MonoBehaviour
{
	private void Start()
	{
		this.newPos = base.transform.position;
		this.oldPos = this.newPos;
		this.velocity = (float)this.speed * base.transform.forward;
	}

	private void OnEnable()
	{
		base.Invoke("resendToPool", this.life);
		this.hasHit = false;
		this.newPos = base.transform.position;
		this.oldPos = this.newPos;
		this.velocity = (float)this.speed * base.transform.forward;
	}

	private void Update()
	{
		if (this.hasHit)
		{
			return;
		}
		this.newPos += this.velocity * Time.deltaTime;
		Vector3 direction = this.newPos - this.oldPos;
		this.distance = direction.magnitude;
		RaycastHit raycastHit;
		if (this.distance > 0f && Physics.Raycast(this.oldPos, direction, out raycastHit, this.distance))
		{
			this.newPos = raycastHit.point;
			this.hasHit = true;
		}
		this.oldPos = base.transform.position;
		base.transform.position = this.newPos;
	}

	public void resendToPool()
	{
		base.gameObject.transform.position = Vector3.zero;
		ObjectPool.instance.PoolObject(base.gameObject);
	}

	public int speed = 500;

	public float life = 3f;

	public int damage = 20;

	public int impactForce = 10;

	public bool impactHoles = true;

	public bool doDamage;

	public List<GameObject> impactObjects;

	private Vector3 velocity;

	private Vector3 newPos;

	private Vector3 oldPos;

	private bool hasHit;

	private float distance;
}
