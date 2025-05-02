// dnSpy decompiler from Assembly-CSharp.dll class: ObjectiveAnimationHandler
using System;
using System.Collections;
using UnityEngine;

public class ObjectiveAnimationHandler : MonoBehaviour
{
	private void OnEnable()
	{
		base.StartCoroutine(this.CallObjective());
	}

	public void OnCrossClick()
	{
		this.obj_panel.SetActive(false);
	}

	private IEnumerator CallObjective()
	{
		yield return new WaitForSeconds(0.1f);
		if (this.AnimationCount == 1)
		{
			yield return new WaitForSeconds(0.1f);
		}
		yield break;
	}

	public GameObject[] desc;

	public int AnimationCount;

	public GameObject obj_panel;
}
