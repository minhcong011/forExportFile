// dnSpy decompiler from Assembly-CSharp.dll class: GunReloadHandler
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunReloadHandler : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnEnable()
	{
		this.image.fillAmount = 0f;
		this.change = true;
	}

	private IEnumerator fillBar()
	{
		this.image.fillAmount = 0f;
		yield return new WaitForSeconds(0.02f);
		float val = 0f;
		while (val <= 1f)
		{
			val += 0.025f;
			this.image.fillAmount += val;
			yield return new WaitForSeconds(this.time / 40f);
		}
		base.gameObject.SetActive(false);
		yield break;
	}

	private void Update()
	{
		if (this.change)
		{
			float num = this.image.fillAmount;
			num = Mathf.MoveTowards(num, 1f, this.time * Time.deltaTime);
			num += 0.025f;
			this.image.fillAmount = num;
			if (num >= 1f)
			{
				this.image.fillAmount = 0f;
				this.change = false;
				base.gameObject.SetActive(false);
			}
		}
	}

	public Image image;

	public float time = 2f;

	private bool change;
}
