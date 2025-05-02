// dnSpy decompiler from Assembly-CSharp.dll class: DamageManager
using System;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
	private void Start()
	{
	}

	public void ApplyDamage(float damage)
	{
		if (this.HP < 0f)
		{
			return;
		}
		if (this.HitSound.Length > 0)
		{
			AudioSource.PlayClipAtPoint(this.HitSound[UnityEngine.Random.Range(0, this.HitSound.Length)], base.transform.position);
		}
		this.HP -= damage;
		if (this.HP <= 0f)
		{
			this.Dead();
		}
	}

	private void Dead()
	{
		if (this.Effect)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.Effect, base.transform.position, base.transform.rotation);
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public AudioClip[] HitSound;

	public GameObject Effect;

	public float HP = 100f;
}
