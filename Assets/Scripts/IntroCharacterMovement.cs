// dnSpy decompiler from Assembly-CSharp.dll class: IntroCharacterMovement
using System;
using UnityEngine;

public class IntroCharacterMovement : MonoBehaviour
{
	private void Awake()
	{
		this.move = base.GetComponent<hoMove>();
	}

	public void MoveOnPath(PathManager path)
	{
		this.move.SetPath(path);
		this.move.pathContainer = path;
		this.move.StartMove();
		base.Invoke("endSequence", 5f);
	}

	private void endSequence()
	{
		Singleton<GameController>.Instance.gameSceneController.endSpecificAnimationOperation();
	}

	public hoMove move;
}
