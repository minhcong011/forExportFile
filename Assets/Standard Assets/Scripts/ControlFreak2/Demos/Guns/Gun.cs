// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Demos.Guns.Gun
using System;
using UnityEngine;

namespace ControlFreak2.Demos.Guns
{
	public class Gun : MonoBehaviour
	{
		private void OnEnable()
		{
			this.audioSource = base.GetComponent<AudioSource>();
			this.isFiring = false;
		}

		public void SetTriggerState(bool fire)
		{
			if (fire != this.isFiring)
			{
				this.isFiring = fire;
				if (fire)
				{
					this.FireBullet();
				}
			}
		}

		private void FixedUpdate()
		{
			if (this.isFiring)
			{
				this.FireBullet();
			}
		}

		public void Reload()
		{
			this.bulletCount = this.bulletCapacity;
			if (this.audioSource != null && this.reloadSound != null)
			{
				this.audioSource.loop = false;
				this.audioSource.PlayOneShot(this.reloadSound);
			}
		}

		private void FireBullet()
		{
			if (Time.time - this.lastShotTime >= this.shotInterval)
			{
				this.lastShotTime = Time.time;
				if (this.unlimitedAmmo || this.bulletCount > 0)
				{
					if (!this.unlimitedAmmo)
					{
						this.bulletCount--;
					}
					if (this.shotParticles != null)
					{
						this.shotParticles.Play();
					}
					if (this.projectileOrigin != null && this.bulletPrefab != null)
					{
						Bullet bullet = UnityEngine.Object.Instantiate<Bullet>(this.bulletPrefab, this.projectileOrigin.position, this.projectileOrigin.rotation);
						if (bullet != null)
						{
							bullet.Init(this);
						}
					}
					if (this.audioSource != null && this.shotSound != null)
					{
						this.audioSource.loop = false;
						this.audioSource.PlayOneShot(this.shotSound);
					}
				}
				else if (this.audioSource != null && this.emptySound != null)
				{
					this.audioSource.loop = false;
					this.audioSource.PlayOneShot(this.emptySound);
				}
			}
		}

		public ParticleSystem shotParticles;

		public AudioClip shotSound;

		public AudioClip emptySound;

		public AudioClip reloadSound;

		private bool isFiring;

		public float shotInterval = 0.175f;

		private float lastShotTime;

		public bool unlimitedAmmo;

		public int bulletCapacity = 40;

		public int bulletCount = 40;

		public Transform projectileOrigin;

		public Bullet bulletPrefab;

		private AudioSource audioSource;
	}
}
