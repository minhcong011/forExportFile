// dnSpy decompiler from Assembly-CSharp.dll class: RotateAround
using System;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
	private void Start()
	{
		if (this.UseCollision)
		{
			this.EffectSettings.CollisionEnter += this.EffectSettings_CollisionEnter;
		}
		if (this.TimeDelay > 0f)
		{
			base.Invoke("ChangeUpdate", this.TimeDelay);
		}
		else
		{
			this.canUpdate = true;
		}
	}

	private void OnEnable()
	{
		this.canUpdate = true;
		this.allTime = 0f;
	}

	private void EffectSettings_CollisionEnter(object sender, CollisionInfo e)
	{
		this.canUpdate = false;
	}

	private void ChangeUpdate()
	{
		this.canUpdate = true;
	}

	private void Update()
	{
		if (!this.canUpdate)
		{
			return;
		}
		this.allTime += Time.deltaTime;
		if (this.allTime >= this.LifeTime && this.LifeTime > 0.0001f)
		{
			return;
		}
		if (this.SpeedFadeInTime > 0.001f)
		{
			if (this.currentSpeedFadeIn < this.Speed)
			{
				this.currentSpeedFadeIn += Time.deltaTime / this.SpeedFadeInTime * this.Speed;
			}
			else
			{
				this.currentSpeedFadeIn = this.Speed;
			}
		}
		else
		{
			this.currentSpeedFadeIn = this.Speed;
		}
		base.transform.Rotate(Vector3.forward * Time.deltaTime * this.currentSpeedFadeIn);
	}

	public float Speed = 1f;

	public float LifeTime = 1f;

	public float TimeDelay;

	public float SpeedFadeInTime;

	public bool UseCollision;

	public EffectSettings EffectSettings;

	private bool canUpdate;

	private float currentSpeedFadeIn;

	private float allTime;
}
