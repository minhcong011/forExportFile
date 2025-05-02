// dnSpy decompiler from Assembly-CSharp.dll class: TextAlphaChanger
using System;
using UnityEngine;

public class TextAlphaChanger : MonoBehaviour
{
	private void Awake()
	{
		this.cg = base.GetComponent<CanvasGroup>();
	}

	private void Update()
	{
		if (this.forward)
		{
			this.cg.alpha += this.speed * Time.deltaTime;
			if (this.cg.alpha >= 1f)
			{
				this.forward = false;
			}
		}
		else
		{
			this.cg.alpha -= this.speed * Time.deltaTime;
			if (this.cg.alpha <= 0f)
			{
				this.forward = true;
			}
		}
	}

	private CanvasGroup cg;

	public float speed = 5f;

	private bool forward;
}
