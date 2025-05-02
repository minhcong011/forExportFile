// dnSpy decompiler from Assembly-CSharp.dll class: AnimalAnimationController
using System;
using UnityEngine;

public class AnimalAnimationController : MonoBehaviour
{
	private void Awake()
	{
		this.animator = base.GetComponent<Animator>();
		int num = UnityEngine.Random.Range(0, this.runTimeAnimators.Length);
		if (this.runTimeAnimators.Length > 0)
		{
			this.animator.runtimeAnimatorController = this.runTimeAnimators[num];
		}
	}

	public void PlayIdle()
	{
		this.StopRunning();
		this.StopWalk();
		this.StopAttackAnim();
	}

	public void PlayRunning()
	{
		this.StopWalk();
		this.StopAttackAnim();
		this.animator.SetBool("Run", true);
	}

	public void StopRunning()
	{
		this.animator.SetBool("Run", false);
	}

	public void PlayWalk()
	{
		this.StopRunning();
		this.StopAttackAnim();
		this.animator.SetBool("Walk", true);
	}

	public void StopWalk()
	{
		this.animator.SetBool("Walk", false);
	}

	public void PlayEat()
	{
		this.animator.SetBool("Eat", true);
	}

	public void StopEat()
	{
		this.animator.SetBool("Eat", false);
	}

	public void PlayDieAnim()
	{
		if (this.animator == null)
		{
			this.animator = base.GetComponent<Animator>();
		}
		this.animator.SetTrigger("Die");
		UnityEngine.Debug.Log("Aya to hy");
	}

	public void PlayHitAnim()
	{
		this.animator.SetTrigger("Hit");
	}

	public void PlayAttackAnim()
	{
		this.StopRunning();
		this.animator.SetBool("Attack", true);
	}

	public void StopAttackAnim()
	{
		this.animator.SetBool("Attack", false);
	}

	private Animator animator;

	public RuntimeAnimatorController[] runTimeAnimators;
}
