// dnSpy decompiler from Assembly-CSharp.dll class: FlashLight
using System;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
	private void Update()
	{
		if (!base.GetComponent<Light>())
		{
			return;
		}
		base.GetComponent<Light>().intensity -= this.LightMult * Time.deltaTime;
	}

	public float LightMult = 1f;
}
