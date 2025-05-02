// dnSpy decompiler from Assembly-CSharp.dll class: CameraShake
using System;
using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	private void Start()
	{
		if (this.debugMode)
		{
			this.ShakeCamera();
		}
	}

	private void ShakeCamera()
	{
		this.startAmount = this.shakeAmount;
		this.startDuration = this.shakeDuration;
		if (!this.isRunning)
		{
			base.StartCoroutine(this.Shake());
		}
	}

	public void ShakeCamera(float amount, float duration)
	{
		this.shakeAmount += amount;
		this.startAmount = this.shakeAmount;
		this.shakeDuration += duration;
		this.startDuration = this.shakeDuration;
		if (!this.isRunning)
		{
			base.StartCoroutine(this.Shake());
		}
	}

	private IEnumerator Shake()
	{
		this.isRunning = true;
		while (this.shakeDuration > 0.01f)
		{
			Vector3 rotationAmount = UnityEngine.Random.insideUnitSphere * this.shakeAmount;
			rotationAmount.z = 0f;
			this.shakePercentage = this.shakeDuration / this.startDuration;
			this.shakeAmount = this.startAmount * this.shakePercentage;
			this.shakeDuration = Mathf.Lerp(this.shakeDuration, 0f, Time.deltaTime);
			if (this.smooth)
			{
				base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, Quaternion.Euler(rotationAmount), Time.deltaTime * this.smoothAmount);
			}
			else
			{
				base.transform.localRotation = Quaternion.Euler(rotationAmount);
			}
			yield return null;
		}
		base.transform.localRotation = Quaternion.identity;
		this.isRunning = false;
		yield break;
	}

	public bool debugMode;

	public float shakeAmount;

	public float shakeDuration;

	private float shakePercentage;

	private float startAmount;

	private float startDuration;

	private bool isRunning;

	public bool smooth;

	public float smoothAmount = 5f;
}
