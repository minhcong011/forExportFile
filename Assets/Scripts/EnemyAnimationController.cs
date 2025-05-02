// dnSpy decompiler from Assembly-CSharp.dll class: EnemyAnimationController
using System;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
	private void Awake()
	{
		this.hitLastTime = Time.time;
	}

	public void PlayHit()
	{
		if (Time.time - this.hitLastTime > 1.2f)
		{
			this.animator.SetTrigger("Hit");
			this.hitLastTime = Time.time;
		}
	}

	public void PlayDieAnimation()
	{
		this.StopHideAnimation();
		this.StopWalking();
		this.StopHideAnimation();
		int num = UnityEngine.Random.Range(1, 5);
		if (num == 1)
		{
			this.animator.SetTrigger("Die1");
		}
		else if (num == 2)
		{
			this.animator.SetTrigger("Die2");
		}
		else if (num == 3)
		{
			this.animator.SetTrigger("Die3");
		}
		else if (num == 4)
		{
			this.animator.SetTrigger("Die4");
		}
	}

	public void PlayHideAnimation()
	{
		this.animator.SetBool("canHide", true);
	}

	public void PlayReloadAnimation()
	{
		this.animator.SetTrigger("Reload");
	}

	public void PlayForwardRole()
	{
		this.animator.SetTrigger("forwardRole");
	}

	public void StopHideAnimation()
	{
		this.animator.SetBool("canHide", false);
	}

	public void StartWalking()
	{
		this.animator.SetBool("Walk", true);
	}

	public void StopWalking()
	{
		this.animator.SetBool("Walk", false);
	}

	public void StartRunning()
	{
		this.animator.SetBool("canRun", true);
	}

	public void StopRunning()
	{
		this.animator.SetBool("canRun", false);
	}

	public void PlayAttackAnimation()
	{
		this.StopWalking();
		this.animator.SetTrigger("AttackP");
	}

	public void PlayFireAnimation()
	{
		this.StopWalking();
		this.animator.SetTrigger("Fire");
	}

	public Animator animator;

	private float hitLastTime;
}
