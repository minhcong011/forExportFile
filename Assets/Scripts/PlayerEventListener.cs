// dnSpy decompiler from Assembly-CSharp.dll class: PlayerEventListener
using System;
using UnityEngine;

public class PlayerEventListener : MonoBehaviour
{
	public void MovePlayer(PathManager pMan)
	{
		this.path = pMan;
		this.hMove.pathContainer = this.path;
		this.hMove.enabled = true;
		this.hMove.StartMove();
	}

	public void reachedAtPoint(int point)
	{
		if (point == this.path.waypoints.Length)
		{
			UnityEngine.Debug.Log("reached At last point " + this.path.waypoints.Length);
			this.canMove = false;
			this.PerformReachedOperation();
		}
	}

	private void PerformReachedOperation()
	{
		Singleton<GameController>.Instance.gameSceneController.PlayerMoveEnd();
	}

	public void SetHeliPos(Transform pos)
	{
		if (this.heliObject != null)
		{
			this.heliObject.localPosition = pos.localPosition;
			this.heliObject.localEulerAngles = new Vector3(pos.localEulerAngles.x, pos.localEulerAngles.y, pos.localEulerAngles.z);
		}
	}

	public PathManager path;

	public bool canMove;

	public hoMove hMove;

	public Transform heliObject;
}
