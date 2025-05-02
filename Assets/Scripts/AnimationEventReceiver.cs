// dnSpy decompiler from Assembly-CSharp.dll class: AnimationEventReceiver
using System;
using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
	private void FootStep()
	{
		this.footStepsSoundSource.PlayOneShot(this.footStepsClips[UnityEngine.Random.Range(0, this.footStepsClips.Length)]);
	}

	public AudioSource footStepsSoundSource;

	public AudioClip[] footStepsClips;
}
