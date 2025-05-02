// dnSpy decompiler from Assembly-CSharp.dll class: PlayerDamageEffect
using System;
using System.Collections;
using UnityEngine;

public class PlayerDamageEffect : MonoBehaviour
{
	private void Start()
	{
		this.lastShown = Time.time - 2f;
	}

	public void Showeffect()
	{
		if (Time.time - this.lastShown > 2.5f)
		{
			this.lastShown = Time.time;
			this.cg[this.id].gameObject.SetActive(false);
			this.id = UnityEngine.Random.Range(0, this.cg.Length);
			this.cg[this.id].alpha = 0f;
			this.cg[this.id].gameObject.SetActive(true);
			base.StartCoroutine(this.effect());
			return;
		}
	}

	private IEnumerator effect()
	{
		while (this.cg[this.id].alpha < 1f)
		{
			this.cg[this.id].alpha += 0.055f;
			yield return new WaitForSeconds(0.02f);
		}
		yield return new WaitForSeconds(0.5f);
		while (this.cg[this.id].alpha > 0f)
		{
			this.cg[this.id].alpha -= 0.05f;
			yield return new WaitForSeconds(0.025f);
		}
		this.cg[this.id].alpha = 0f;
		yield break;
	}

	private void Update()
	{
	}

	public CanvasGroup[] cg;

	private int id;

	private float lastShown;
}
