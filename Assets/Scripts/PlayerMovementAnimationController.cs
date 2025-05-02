// dnSpy decompiler from Assembly-CSharp.dll class: PlayerMovementAnimationController
using System;
using UnityEngine;

public class PlayerMovementAnimationController : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void setPath(PathManager p)
	{
		this.hMove.pathContainer = p;
		this.StartHoMove();
	}

	public void PauseHoMove()
	{
		if (this.hMove != null && this.hMove.enabled)
		{
			this.hMove.Pause();
		}
	}

	public void StopHoMove()
	{
		if (this.hMove != null)
		{
			this.hMove.Stop();
			this.hMove.speed = 0f;
			this.hMove.enabled = false;
		}
	}

	public void StartHoMove()
	{
		if (this.hMove != null && this.hMove.enabled)
		{
			this.hMove.StartMove();
			this.hMove.Resume();
		}
	}

	public PlayerMovementAnimationController.PlayerType playerType;

	public hoMove hMove;

	public enum PlayerType
	{
		FPS,
		thirdPS,
		vehicle
	}
}
