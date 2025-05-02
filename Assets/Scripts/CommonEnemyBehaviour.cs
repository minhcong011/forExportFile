// dnSpy decompiler from Assembly-CSharp.dll class: CommonEnemyBehaviour
using System;
using UnityEngine;

public class CommonEnemyBehaviour : EnemyModel
{
	public void ApplyDamage(float damageVal, string part = "normal", float delayDestroyRef = 0.3f, bool useForce = true)
	{
		this.canUseForce = useForce;
		this.delayDestroy = delayDestroyRef;
		if (this.enemyState == EnemyModel.State.dead)
		{
			this.HideBar();
			return;
		}
		if (part == "Head")
		{
			this.isheadShot = true;
			damageVal = 110f;
		}
		this.health -= damageVal;
		if (this.health <= 0f)
		{
			this.health = 0f;
			this.enemyState = EnemyModel.State.dead;
			this.isDead = true;
			if (Constants.isSoundOn() && this.deadSound != null)
			{
				int num = UnityEngine.Random.Range(0, this.deadClips.Length);
				if (this.deadClips.Length > 0)
				{
					AudioSource.PlayClipAtPoint(this.deadClips[num], base.transform.position, 1f);
				}
				else
				{
					AudioSource.PlayClipAtPoint(this.deadSound, base.transform.position);
				}
			}
			this.ObjectDead();
		}
		else
		{
			if (Constants.isSoundOn() && this.hitSound != null)
			{
				AudioSource.PlayClipAtPoint(this.hitSound, base.transform.position);
			}
			this.showBar();
			base.gameObject.SendMessage("gotHitAtBase", SendMessageOptions.DontRequireReceiver);
		}
	}

	public void enemyOnTarget()
	{
		this.targetTime = Time.time;
		if (this.isOntarget)
		{
			return;
		}
		this.isOntarget = true;
		this.showFields();
	}

	private void showFields()
	{
	}

	private void hideFields()
	{
	}

	private void ObjectDead()
	{
		base.gameObject.SendMessage("DiedAtBase", SendMessageOptions.DontRequireReceiver);
		this.HideBar();
	}

	public void showBar()
	{
		if (this.healthSlider == null)
		{
			return;
		}
		if (!this.canShowSlider)
		{
			return;
		}
		this.healthSlider.gameObject.SetActive(true);
		this.healthSlider.value = this.health / 100f;
		base.CancelInvoke("HideBar");
		base.Invoke("HideBar", 1.4f);
	}

	public void HideBar()
	{
		if (this.healthSlider == null)
		{
			return;
		}
		this.healthSlider.gameObject.SetActive(false);
	}

	public void Update()
	{
		if (this.isOntarget && Time.time - this.targetTime > 1f)
		{
			this.isOntarget = false;
			this.hideFields();
		}
	}

	public GameObject bullet;

	public GameObject muzzleFlash;

	public GameObject hitEffect;

	public Transform fireOrigin;

	public AudioClip hitSound;

	public AudioClip deadSound;

	public AudioClip fireSound;

	public AudioClip[] deadClips;

	public UISlider healthSlider;

	private bool isOntarget;

	private float targetTime;

	public bool isheadShot;

	public bool canShowSlider;

	public bool canUseForce;

	public bool isDead;

	public float delayDestroy = 0.1f;
}
