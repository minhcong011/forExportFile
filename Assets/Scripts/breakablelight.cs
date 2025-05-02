// dnSpy decompiler from Assembly-CSharp.dll class: breakablelight
using System;
using UnityEngine;

public class breakablelight : MonoBehaviour
{
	private void Update()
	{
		if (this.hitpoints <= 0f)
		{
			UnityEngine.Object.Instantiate<Transform>(this.brokenobject, base.transform.position, base.transform.rotation);
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Damage(float damage)
	{
		this.hitpoints -= damage;
	}

	public float hitpoints = 50f;

	public Transform brokenobject;
}
