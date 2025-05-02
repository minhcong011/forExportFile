// dnSpy decompiler from Assembly-CSharp.dll class: PlayerBloodEffect
using System;
using System.Collections;
using UnityEngine;

public class PlayerBloodEffect : MonoBehaviour
{
	private void Start()
	{
	}

	public void Showeffect()
	{
		if (this.shown)
		{
			return;
		}
		this.shown = true;
		base.StartCoroutine(this.effect());
	}

	private IEnumerator effect()
	{
		for (int i = 0; i < this.cg.Length; i++)
		{
			this.cg[i].alpha = 0f;
			this.cg[i].gameObject.SetActive(true);
			while (this.cg[i].alpha < 1f)
			{
				this.cg[i].alpha += 0.05f;
				yield return new WaitForSeconds(0.01f);
			}
			yield return new WaitForSeconds(0.1f);
		}
		yield break;
	}

	public CanvasGroup[] cg;

	private bool shown;
}
