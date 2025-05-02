// dnSpy decompiler from Assembly-CSharp.dll class: RandomStateAnimationBehaviour
using System;
using UnityEngine;

public class RandomStateAnimationBehaviour : StateMachineBehaviour
{
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.SetInteger("IdleAnimId", UnityEngine.Random.Range(0, 2));
	}
}
