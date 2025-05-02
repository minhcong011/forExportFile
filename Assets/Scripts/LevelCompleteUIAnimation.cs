// dnSpy decompiler from Assembly-CSharp.dll class: LevelCompleteUIAnimation
using System;
using System.Collections;
using UnityEngine;

public class LevelCompleteUIAnimation : MonoBehaviour
{
	public void OnEnable()
	{
		base.StartCoroutine(this.playBtnAnim());
	}

	private void OnDisable()
	{
		this.LvlPanel.GetComponent<LevelCompleteUIAnimation>().enabled = false;
	}

	public void OnCrossClick()
	{
		this.LvlPanel.SetActive(false);
	}

	private IEnumerator playBtnAnim()
	{
		yield return new WaitForSeconds(0.2f);
		if (this.animation_value == 1)
		{
			yield return new WaitForSeconds(1.5f);
			this.particles[0].SetActive(true);
			yield return new WaitForSeconds(0.11f);
			this.particles[1].SetActive(true);
		}
		yield break;
	}

	public int animation_value;

	public GameObject Lvltitle;

	public GameObject LvlPanel;

	public GameObject[] particles;
}
