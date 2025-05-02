// dnSpy decompiler from Assembly-CSharp.dll class: crate
using System;
using UnityEngine;

public class crate : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (this.hitpoints <= 0f)
		{
			UnityEngine.Object.Instantiate<Transform>(this.spawnobject, base.transform.position, base.transform.rotation);
			Vector3 position = base.transform.position;
			Collider[] array = Physics.OverlapSphere(position, this.radius);
			foreach (Collider collider in array)
			{
				if (collider.GetComponent<Rigidbody>() != null)
				{
					Rigidbody component = collider.GetComponent<Rigidbody>();
					component.AddExplosionForce(this.power, position, this.radius, 3f);
				}
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Damage(float damage)
	{
		this.hitpoints -= damage;
	}

	public float hitpoints = 100f;

	public Transform spawnobject;

	public float radius = 3f;

	public float power = 100f;
}
