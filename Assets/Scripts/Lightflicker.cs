// dnSpy decompiler from Assembly-CSharp.dll class: Lightflicker
using System;
using UnityEngine;

public class Lightflicker : MonoBehaviour
{
	private void Start()
	{
		this.randomintensity = UnityEngine.Random.Range(0f, 6f);
	}

	private void Update()
	{
		float t = Mathf.PerlinNoise(this.randomintensity, Time.time);
		this.mylight = base.GetComponent<Light>();
		this.mylight.intensity = Mathf.Lerp(this.minFlickerIntensity, this.maxFlickerIntensity, t);
	}

	public float minFlickerIntensity = 0.5f;

	public float maxFlickerIntensity = 2.5f;

	private Light mylight;

	private float randomintensity;
}
