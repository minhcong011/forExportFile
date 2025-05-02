// dnSpy decompiler from Assembly-CSharp.dll class: HitEffectPooling
using System;
using UnityEngine;

public class HitEffectPooling : MonoBehaviour
{
	private void OnEnable()
	{
		base.CancelInvoke("Deactivate");
		base.Invoke("Deactivate", this.lifeTime);
	}

	private void Deactivate()
	{
		ObjectPool.instance.PoolObject(base.gameObject);
	}

	private void OnDisable()
	{
		base.transform.position = Vector3.zero;
	}

	public float lifeTime = 0.5f;
}
