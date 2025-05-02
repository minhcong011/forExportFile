// dnSpy decompiler from Assembly-CSharp.dll class: AnimationSpeedDebuff
using System;
using UnityEngine;

public class AnimationSpeedDebuff : MonoBehaviour
{
	private void GetAnimatorOnParent(Transform t)
	{
		Animator component = t.parent.GetComponent<Animator>();
		if (component == null)
		{
			if (this.root == t.parent)
			{
				return;
			}
			this.GetAnimatorOnParent(t.parent);
		}
		else
		{
			this.myAnimator = component;
		}
	}

	private void Start()
	{
		this.root = base.transform.root;
		this.GetAnimatorOnParent(base.transform);
		if (this.myAnimator == null)
		{
			return;
		}
		this.oldSpeed = this.myAnimator.speed;
	}

	private void Update()
	{
		if (this.myAnimator == null || this.AnimationSpeenOnTime.length == 0)
		{
			return;
		}
		this.time += Time.deltaTime;
		this.myAnimator.speed = this.AnimationSpeenOnTime.Evaluate(this.time / this.MaxTime) * this.oldSpeed;
	}

	public AnimationCurve AnimationSpeenOnTime;

	public float MaxTime = 1f;

	private Animator myAnimator;

	private Transform root;

	private float oldSpeed;

	private float time;
}
