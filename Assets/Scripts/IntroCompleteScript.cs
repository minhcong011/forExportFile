// dnSpy decompiler from Assembly-CSharp.dll class: IntroCompleteScript
using System;
using UnityEngine;

public class IntroCompleteScript : MonoBehaviour
{
	private void Start()
	{
		base.Invoke("startScene", 11f);
	}

	private void startScene()
	{
		UnityEngine.Object.FindObjectOfType<UIController>().ButtonClicks("SkipIntro");
	}

	private void OnTriggerEnter(Collider collision)
	{
	}

	private bool isCrossed;
}
