// dnSpy decompiler from Assembly-CSharp.dll class: AnimationController
using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (this.can_move)
		{
			this.player.transform.localEulerAngles = new Vector3(0f, Mathf.MoveTowardsAngle(this.player.transform.localEulerAngles.y, this.target, 180f * Time.deltaTime), 0f);
		}
	}

	public void PlayWinAnimation()
	{
		this.can_move = true;
		MonoBehaviour.print("function called");
	}

	public void PlayLoseAnimation()
	{
	}

	public GameObject player;

	public bool can_move = true;

	private float target = -130f;

	private float speed = 40f;
}
