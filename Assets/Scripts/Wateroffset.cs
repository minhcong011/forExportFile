// dnSpy decompiler from Assembly-CSharp.dll class: Wateroffset
using System;
using UnityEngine;

public class Wateroffset : MonoBehaviour
{
	private void Awake()
	{
		this.r = base.GetComponent<MeshRenderer>();
	}

	private void Update()
	{
		float x = Time.time * this.speed;
		this.r.material.mainTextureOffset = new Vector2(x, 0f);
	}

	public float speed = 0.1f;

	private MeshRenderer r;
}
