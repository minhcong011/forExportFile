// dnSpy decompiler from Assembly-CSharp.dll class: Bullet_FPsTps
using System;
using UnityEngine;

public class Bullet_FPsTps : MonoBehaviour
{
	private void Awake()
	{
		Physics.IgnoreCollision(base.GetComponent<Collider>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>());
	}

	private void Start()
	{
	}

	public void setFinalPosition()
	{
	}

	private void OnEnable()
	{
		this.startTime = Time.time;
	}

	private void Update()
	{
		float maxDistanceDelta = this.speed * Time.deltaTime;
		if (Time.time - this.startTime < 3f)
		{
			if (this.finalPos == Vector3.zero)
			{
				base.transform.Translate(base.transform.forward * this.speed * Time.deltaTime);
			}
			else
			{
				base.transform.position = Vector3.MoveTowards(base.transform.position, this.finalPos, maxDistanceDelta);
			}
		}
		else
		{
			ObjectPool.instance.PoolObject(base.gameObject);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag != "Particle")
		{
		}
	}

	private float startTime;

	public float speed = 0.8f;

	public float lifeTime = 3f;

	public GameObject Effect;

	public Vector3 finalPos = Vector3.zero;
}
