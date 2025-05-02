// dnSpy decompiler from Assembly-CSharp.dll class: AchievmentsHandlerAnimation
using System;
using System.Collections;
using UnityEngine;

public class AchievmentsHandlerAnimation : MonoBehaviour
{
	private void OnEnable()
	{
		base.StartCoroutine(this.CallAchievements());
	}

	public void OnCrossClick()
	{
		this.ach_Panel.SetActive(false);
	}

	private IEnumerator CallAchievements()
	{
		if (this.anim_value == 1)
		{
			yield return new WaitForSeconds(0.4f);
			yield return new WaitForSeconds(0.1f);
			yield return new WaitForSeconds(2f);
			this.effect[0].gameObject.SetActive(true);
		}
		else if (this.anim_value == 2)
		{
			yield return new WaitForSeconds(0.5f);
			this.effect[0].gameObject.SetActive(true);
		}
		yield break;
	}

	public int anim_value;

	public ParticleSystem[] effect;

	public GameObject ach_Panel;
}
