// dnSpy decompiler from Assembly-CSharp.dll class: PlayerAnimationHandler
using System;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
	private void Start()
	{
	}

	public void enableGun(int id)
	{
		this.guns[this.currentGunId].SetActive(false);
		this.currentGunId = id;
		this.guns[this.currentGunId].SetActive(true);
	}

	public void PlayGernadeAnimation()
	{
		this.animator.SetTrigger("Gernade");
	}

	public GameObject[] guns;

	public Animator animator;

	private int currentGunId;
}
