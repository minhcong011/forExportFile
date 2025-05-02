// dnSpy decompiler from Assembly-CSharp.dll class: TestBulletMover
using System;
using UnityEngine;

public class TestBulletMover : MonoBehaviour
{
	private void Awake()
	{
		Physics.IgnoreCollision(base.GetComponent<Collider>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>());
	}

	private void Start()
	{
		this.startTime = Time.realtimeSinceStartup;
	}

	private void Update()
	{
		float maxDistanceDelta = this.speed * Time.deltaTime;
		if (Time.realtimeSinceStartup - this.startTime < 2f)
		{
			if (this.finalPos == Vector3.zero)
			{
				base.transform.Translate(new Vector3(0f, 0f, 1f) * this.speed * Time.deltaTime);
			}
			else
			{
				base.transform.position = Vector3.MoveTowards(base.transform.position, this.finalPos, maxDistanceDelta);
			}
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private float startTime;

	public float speed = 0.8f;

	public GameObject Effect;

	public Vector3 finalPos = Vector3.zero;
}
