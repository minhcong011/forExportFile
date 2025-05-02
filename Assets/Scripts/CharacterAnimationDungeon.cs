// dnSpy decompiler from Assembly-CSharp.dll class: CharacterAnimationDungeon
using System;
using UnityEngine;

public class CharacterAnimationDungeon : MonoBehaviour
{
	private void Start()
	{
		this.cc = base.GetComponentInChildren<CharacterController>();
		this.anim = base.GetComponentInChildren<Animation>();
	}

	private void LateUpdate()
	{
		if (this.cc.isGrounded && (ETCInput.GetAxis("Vertical") != 0f || ETCInput.GetAxis("Horizontal") != 0f))
		{
			this.anim.CrossFade("soldierRun");
		}
		if (this.cc.isGrounded && ETCInput.GetAxis("Vertical") == 0f && ETCInput.GetAxis("Horizontal") == 0f)
		{
			this.anim.CrossFade("soldierIdleRelaxed");
		}
		if (!this.cc.isGrounded)
		{
			this.anim.CrossFade("soldierFalling");
		}
	}

	private CharacterController cc;

	private Animation anim;
}
