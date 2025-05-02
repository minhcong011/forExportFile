// dnSpy decompiler from Assembly-CSharp.dll class: FlyDemo
using System;
using UnityEngine;

public class FlyDemo : MonoBehaviour
{
	private void Start()
	{
		this.t = base.transform;
	}

	private void Update()
	{
		this.time += Time.deltaTime;
		float num = Mathf.Cos(this.time / this.Speed);
		this.t.localPosition = new Vector3(0f, 0f, num * this.Height);
	}

	public float Speed = 1f;

	public float Height = 1f;

	private Transform t;

	private float time;
}
