// dnSpy decompiler from Assembly-CSharp.dll class: EndSequenceAnimationController
using System;
using UnityEngine;

public class EndSequenceAnimationController : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void PlayDieAnimation()
	{
		this.playerAnimator.SetBool("Die", true);
	}

	public void PlayWinAnimation()
	{
	}

	public Animator playerAnimator;
}
