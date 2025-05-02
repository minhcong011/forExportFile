// dnSpy decompiler from Assembly-CSharp.dll class: flamedamage
using System;
using UnityEngine;

public class flamedamage : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnParticleCollision(GameObject other)
	{
		other.SendMessageUpwards("Damage", 5f, SendMessageOptions.DontRequireReceiver);
	}
}
