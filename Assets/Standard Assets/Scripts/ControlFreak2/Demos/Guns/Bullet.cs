// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Demos.Guns.Bullet
using System;
using UnityEngine;

namespace ControlFreak2.Demos.Guns
{
	public class Bullet : MonoBehaviour
	{
		private void OnEnable()
		{
			this.rigidBody = base.GetComponent<Rigidbody>();
		}

		public void Init(Gun gun)
		{
			this.gun = gun;
			this.lifetime = 0f;
			this.speed = Mathf.Max(0f, this.initialSpeed);
		}

		public Gun GetGun()
		{
			return this.gun;
		}

		private void FixedUpdate()
		{
			this.speed += Time.deltaTime * this.accel;
			if (this.speed > this.maxSpeed)
			{
				this.speed = this.maxSpeed;
			}
			Transform transform = base.transform;
			Vector3 position = transform.position + transform.forward * this.speed * Time.deltaTime;
			if (this.rigidBody != null)
			{
				this.rigidBody.MovePosition(position);
			}
			else
			{
				transform.position = position;
			}
			if ((this.lifetime += Time.deltaTime) > this.maxLifetime)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		private Gun gun;

		private Rigidbody rigidBody;

		public float maxLifetime = 5f;

		private float lifetime;

		public float initialSpeed = 20f;

		public float maxSpeed = 30f;

		public float accel = 20f;

		private float speed;
	}
}
