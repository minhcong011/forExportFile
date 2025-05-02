// dnSpy decompiler from Assembly-CSharp.dll class: ScreenFader
using System;
using System.Collections;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
	private void Awake()
	{
		this.cg = base.gameObject.GetComponent<CanvasGroup>();
	}

	private void Start()
	{
	}

	private void OnEnable()
	{
		if (this.isForward)
		{
			this.cg.alpha = 0f;
			base.StartCoroutine(this.playForward());
		}
		else
		{
			this.cg.alpha = 1f;
			base.StartCoroutine(this.playReverse());
		}
	}

	private void OnDisable()
	{
		if (this.isForward)
		{
			this.cg.alpha = 0f;
		}
		else
		{
			this.cg.alpha = 1f;
		}
	}

	private IEnumerator playForward()
	{
		yield return new WaitForSeconds(0.1f);
		while (this.cg.alpha < 1f)
		{
			this.cg.alpha += 0.03f;
			yield return new WaitForSeconds(this.speed);
		}
		if (this.cg.alpha >= 1f && this.autoHide)
		{
			base.gameObject.SetActive(false);
		}
		yield break;
	}

	private IEnumerator playReverse()
	{
		yield return new WaitForSeconds(0.1f);
		while (this.cg.alpha > 0f)
		{
			this.cg.alpha -= 0.03f;
			yield return new WaitForSeconds(this.speed);
		}
		if (this.cg.alpha <= 0f && this.autoHide)
		{
			base.gameObject.SetActive(false);
		}
		yield break;
	}

	private void Update()
	{
	}

	public bool isForward = true;

	public float speed = 0.04f;

	private CanvasGroup cg;

	public bool autoHide;
}
