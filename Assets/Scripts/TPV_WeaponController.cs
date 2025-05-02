// dnSpy decompiler from Assembly-CSharp.dll class: TPV_WeaponController
using System;
using System.Collections;
using UnityEngine;

public class TPV_WeaponController : MonoBehaviour
{
	public void Attack()
	{
		if (this.lightOnShot)
		{
			base.StartCoroutine("EnableLight");
		}
	}

	private IEnumerator EnableLight()
	{
		this.lightOnShot.enabled = true;
		yield return new WaitForSeconds(0.05f);
		this.lightOnShot.enabled = false;
		yield break;
	}

	public ParticleSystem[] emittOnAttack;

	public Light lightOnShot;

	public Transform bulletOrigin;

	public string bulletName = "Bullet_Tps";
}
